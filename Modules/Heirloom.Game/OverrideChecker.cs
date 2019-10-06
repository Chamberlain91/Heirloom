using System;
using System.Reflection;

namespace Heirloom.Game
{
    internal static class OverrideChecker
    {
        private const BindingFlags InstanceAllBindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static bool IsMethodOverridden(Type baseType, Type type, string name)
        {
            if (type is null) { throw new ArgumentNullException(nameof(type)); }

            // 
            var method = type.GetMethod(name, InstanceAllBindings);
            return method.DeclaringType != baseType;
        }
    }
}
