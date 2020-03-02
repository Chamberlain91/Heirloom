using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Heirloom.GenDoc
{
    internal static class TypeDatabase
    {
        private static readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        static TypeDatabase()
        {
            PopulateTypes(GetAssemblyTypes(typeof(int).Assembly));
            PopulateTypes(GetAssemblyTypes(typeof(IEnumerable<int>).Assembly));
            PopulateTypes(GetAssemblyTypes(typeof(IEnumerable).Assembly));
            PopulateTypes(GetAssemblyTypes(typeof(Enumerable).Assembly));
        }

        public static IEnumerable<string> Keys => _types.Keys;

        public static bool TryGetType(string key, out Type type)
        {
            if (_types.TryGetValue(key, out type))
            {
                return true;
            }

            // Unable to find
            type = default;
            return false;
        }

        public static IEnumerable<Type> GetAssemblyTypes(Assembly assembly)
        {
            // public or protected
            return assembly.DefinedTypes.Where(t => t.IsPublic || t.IsNestedFamORAssem);
        }

        public static void PopulateTypes(Assembly assembly)
        {
            PopulateTypes(GetAssemblyTypes(assembly));
        }

        public static void PopulateTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                // Store type by key and toString()
                _types[Documentation.GetTypeKey(type)] = type;
                _types[type.ToString()] = type;
            }
        }
    }
}
