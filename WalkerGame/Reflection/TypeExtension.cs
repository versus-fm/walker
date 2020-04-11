using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace WalkerGame.Reflection
{
    public static class TypeExtension
    {
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T)) != null;
        }
        public static bool TryAttribute<T>(this Type type, out T item) where T : Attribute
        {
            item = (T)type.GetCustomAttribute(typeof(T));
            return item != null;
        }

        public static bool HasAttribute<T>(this ConstructorInfo constructorInfo) where T : Attribute
        {
            return constructorInfo.GetCustomAttribute(typeof(T)) != null;
        }
        
        public static bool Implements<T>(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(T));
        }

        public static void InvokeGenericMethod<T>(this T t, string name, Type[] genericTypeParams, params object[] arguments)
        {
            var type = typeof(T);
            var methodInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.IsGenericMethod && x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (methodInfo != null)
            {
                methodInfo.MakeGenericMethod(genericTypeParams).Invoke(t, arguments);
            }
        }
    }
}