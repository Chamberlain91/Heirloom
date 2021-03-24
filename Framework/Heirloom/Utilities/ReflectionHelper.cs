using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Heirloom.Utilities
{
    /// <summary>
    /// Utilities to assist with reflection tasks.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Gets a collection of assemblies known the current <see cref="AppDomain"/>.
        /// </summary>
        public static ICollection<Assembly> GetAssemblies()
        {
            return new HashSet<Assembly>(Generate());

            static IEnumerable<Assembly> Generate()
            {
                // Try all known assemblies to the current domain
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!IsIgnoredAssembly(assembly))
                    {
                        yield return assembly;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a collection of all types known to the current <see cref="AppDomain"/>.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllTypes()
        {
            foreach (var assembly in GetAssemblies())
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Gets a collection of all types known to the current <see cref="AppDomain"/> that are a subclass of <typeparamref name="T"/>.
        /// </summary>
        public static IEnumerable<Type> GetSubclassTypes<T>()
        {
            var baseClass = typeof(T);
            foreach (var type in GetAllTypes())
            {
                if (type.IsSubclassOf(baseClass))
                {
                    yield return type;
                }
            }
        }

        internal static bool IsIgnoredAssembly(Assembly assembly)
        {
            // Ignore .NET Framework Assemblies
            var productAttribute = assembly.GetCustomAttributes<AssemblyProductAttribute>().FirstOrDefault();
            if (productAttribute?.Product == "Microsoft® .NET Framework") { return true; }
            if (assembly.FullName.StartsWith("System.")) { return true; }

            // Not ignored
            return false;
        }
    }
}
