using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using Svelto.Common.Internal;
using WalkerGame.Metadata;
using WalkerGame.Metadata.Hinting;
using WalkerGame.Reflection;

namespace WalkerGame
{
    public class ObjectGraph : IDisposable
    {
        private readonly Assembly assembly;
        private readonly Container container;

        public ObjectGraph(Assembly assembly)
        {
            this.assembly = assembly;
            container = new Container();
            container.RegisterInstance(this);
            container.ResolveUnregisteredType +=
                (sender, args) =>
                {
                    var serviceType = args.UnregisteredServiceType;
                    if (args.UnregisteredServiceType.TryInheritedAttribute<ServiceAttribute>(out var attribute))
                        serviceType = attribute.ForType ?? args.UnregisteredServiceType;
                    var registration = Lifestyle.Singleton.CreateRegistration(serviceType,
                        () => Construct(args.UnregisteredServiceType),
                        container);
                    Console.WriteLine($"Registering unknown singleton\n\t{args.UnregisteredServiceType}\n\t{registration.ImplementationType}");
                    args.Register(registration);
                };
        }

        public void DiscoverServices()
        {
            ReflectionUtil.GetTypesInAssembly(assembly).ForEach(type =>
            {
                if (type.TryAttribute<ServiceAttribute>(out var attribute))
                {
                    Type serviceType = type;
                    if (attribute.ForType != null)
                        serviceType = attribute.ForType;
                    
                    container.RegisterSingleton(serviceType, () => Construct(type));
                }
            });
        }

        public void RegisterInstance<T>(T obj) where T : class
        {
            container.RegisterInstance(obj);
        }
        
        public void RegisterInstance<T>(Type serviceType, T obj) where T : class
        {
            container.RegisterInstance(serviceType, obj);
        }


        public void RegisterInstance<TService, TImplementation>(TImplementation implementation)
            where TImplementation : class
        {
            container.RegisterInstance(typeof(TService), implementation);
        }

        private object AttemptPostConstruct(object target)
        {
            if (target.GetType().Implements<PostConstruct>())
            {
                var pctor = (PostConstruct) target;
                pctor.Post();
            }

            return target;
        }

        public T Construct<T>()
        {
            return (T)Construct(typeof(T));
        }

        public object ConstructAndRegister(Type type)
        {
            var instance = Construct(type);
            RegisterInstance(instance);
            return instance;
        }

        public object GetOrConstructAndRegister(Type type)
        {
            return Get(type) ?? ConstructAndRegister(type);
        }

        public void DoOnAttribute<T>(Action<T, Type> action) where T : Attribute
        {
            ReflectionUtil.GetTypesInAssembly(assembly).ForEach(type =>
            {
                if (type.TryAttribute<T>(out var attribute))
                {
                    action(attribute, type);
                }
            });
        }
        
        public object Construct(Type type)
        {
            var ctors = type.GetConstructors();
            var ctor = ctors.FirstOrDefault(info => info.HasAttribute<InjectAttribute>()) ??
                       ctors.FirstOrDefault();
            
            if (ctor == null)
                throw new ArgumentException("Inject failed: No public constructor for type " + type.Name);

            var parameters = ctor.GetParameters();
            var arguments = new object[parameters.Length];
            for (var index = 0; index < parameters.Length; index++)
            {
                var parameter = parameters[index];
                if (parameter.TryAttribute<HintAttribute>(out var attribute))
                {
                    if (attribute.HintType == HintType.Literal)
                    {
                        arguments[index] = attribute.GetLiteral();
                    }
                    else if (attribute.HintType == HintType.ServiceHint)
                    {
                        if (!parameter.ParameterType.Implements(attribute.ServiceType))
                            throw new ArgumentException(
                                $"Hinting failed while constructing {type.Name}:\n" +
                                        $"\t{parameter.ParameterType.Name} does not implement {attribute.ServiceType.Name}\n" +
                                        $"\tAttempted constructor:\n" +
                                        $"\t\t{ctor}");
                        arguments[index] = container.GetInstance(attribute.ServiceType);
                    }
                }
                else
                {
                    if (parameter.ParameterType.IsPrimitive)
                        throw new ArgumentException($"Construct failed for {type.Name}\n" +
                                                    $"\t{parameter.Name} is a primitive type.\n" +
                                                    $"\tDid you mean to hint the parameter (see {typeof(HintAttribute).FullName})");
                    arguments[index] = container.GetInstance(parameter.ParameterType);
                }
            }

            var obj = ctor.Invoke(arguments);
            return AttemptPostConstruct(obj);
        }

        public void Verify()
        {
            container.Verify();
        }

        public T Get<T>() where T : class
        {
            return container.GetInstance<T>();
        }
        
        public object Get(Type type)
        {
            return container.GetInstance(type);
        }

        public void Dispose()
        {
            container?.Dispose();
        }
    }
}