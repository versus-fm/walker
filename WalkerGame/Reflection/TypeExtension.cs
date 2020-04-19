using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        
        public static bool TryInheritedAttribute<T>(this Type type, out T item) where T : Attribute
        {
            item = (T)type.GetCustomAttribute(typeof(T), true);
            return item != null;
        }
        
        public static bool TryInheritedAttribute<T>(this ParameterInfo param, out T item) where T : Attribute
        {
            item = (T)param.GetCustomAttribute(typeof(T), true);
            return item != null;
        }
        
        public static bool TryAttribute<T>(this ParameterInfo param, out T item) where T : Attribute
        {
            item = (T)param.GetCustomAttribute(typeof(T));
            return item != null;
        }

        public static bool HasAttribute<T>(this ConstructorInfo constructorInfo) where T : Attribute
        {
            return constructorInfo.GetCustomAttribute(typeof(T)) != null;
        }
        
        public static bool Implements<T>(this Type type)
        {
            return Implements(type, typeof(T));
        }
        
        public static bool Implements(this Type type, Type implements)
        {
            return type.GetInterfaces().Contains(implements);
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

        public static void SaveAsPng(this Texture2D texture2D, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create);
            texture2D.SaveAsPng(fs, texture2D.Width, texture2D.Height);
        }
    }
}