using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WalkerGame.Reflection
{
    public static class ReflectionUtil
    {
        public static List<Type> GetTypesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes().ToList();
        }
    }
}