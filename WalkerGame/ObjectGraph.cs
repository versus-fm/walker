using System;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using WalkerGame.Metadata;
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

            var parameters = ctor.GetParameters();
            var arguments = new object[parameters.Length];
            for (var index = 0; index < parameters.Length; index++)
            {
                var parameter = parameters[index];
                var instance = container.GetInstance(parameter.ParameterType);
                arguments[index] = instance;
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