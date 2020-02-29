using System;
using System.Linq;
using System.Reflection;

namespace SharpDoc
{
    internal static class TypeHelper
    {
        public static string GetTypeAccess(this Type type)
        {
            var pre = "";

            // Class
            if (type.IsClass)
            {
                // Delegate
                if (type.IsSubclassOf(typeof(Delegate))) { return "Delegate"; }
                else
                {
                    //
                    if (type.IsAbstract && type.IsSealed) { pre += "Static "; }
                    else
                    {
                        if (type.IsAbstract) { pre += "Abstract "; }
                        if (type.IsSealed) { pre += "Sealed "; }
                    }

                    return $"{pre}Class";
                }
            }
            // Enum
            else if (type.IsEnum) { return $"{pre}Enum"; }
            // Interface
            else if (type.IsInterface) { return $"{pre}Interface"; }
            // Struct
            else if (type.IsValueType) { return $"{pre}Struct"; }
            // ???
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static string GetHumanName(this Type type)
        {
            // Primitive Types
            if (type == typeof(bool)) { return "bool"; }
            else if (type == typeof(int)) { return "int"; }
            else if (type == typeof(uint)) { return "uint"; }
            else if (type == typeof(short)) { return "short"; }
            else if (type == typeof(ushort)) { return "ushort"; }
            else if (type == typeof(byte)) { return " byte"; }
            else if (type == typeof(sbyte)) { return "sbyte"; }
            else if (type == typeof(long)) { return "long"; }
            else if (type == typeof(ulong)) { return "ulong"; }
            else if (type == typeof(float)) { return "float"; }
            else if (type == typeof(double)) { return "double"; }
            else if (type == typeof(decimal)) { return "decimal"; }
            else if (type == typeof(char)) { return "char"; }
            else if (type == typeof(string)) { return "string"; }
            else if (type == typeof(object)) { return "object"; }
            else if (type == typeof(void)) { return "void"; }

            // Get the "A" of "A.B" types
            var pre = "";
            if (type.IsNested && !type.IsGenericParameter && !type.IsGenericTypeParameter && !type.IsGenericMethodParameter)
            {
                pre += GetHumanName(type.DeclaringType);
                pre += ".";
            }

            // Pointer or reference
            if (type.IsByRef)
            {
                return GetHumanName(type.GetElementType());
            }
            else
            // Array
            if (type.IsArray)
            {
                var c = new string(',', type.GetArrayRank() - 1);
                return $"{pre}{GetHumanName(type.GetElementType())}[{c}]";
            }
            else
            // Generic
            if (type.IsGenericType)
            {
                var name = type.Name; // Get name and strip grave character
                name = name.Substring(0, name.IndexOf("`"));

                if (type.IsConstructedGenericType)
                {
                    var genericTypes = type.GenericTypeArguments.Select(t => GetHumanName(t));
                    return $"{pre}{name}<{string.Join("|", genericTypes)}>";
                }
                else
                {
                    var genericArgs = type.GetGenericArguments().Select(t => GetHumanName(t));
                    return $"{pre}{name}<{string.Join("|", genericArgs)}>";
                }
            }
            // 
            else
            {
                return $"{pre}{type.Name}";
            }
        }

        public static string GetHumanName(this MethodInfo method)
        {
            var pre = "";
            if (method.IsStatic) { pre = "static "; }

            if (method.IsGenericMethod)
            {
                var name = method.Name;
                var indTick = name.IndexOf("`");
                if (indTick >= 0) { name = name.Substring(0, indTick); }

                var genericTypes = method.GetGenericArguments().Select(t => GetHumanName(t));
                return $"{pre}{name}<{string.Join("|", genericTypes)}>";
            }
            else
            {
                return $"{pre}{method.Name}";
            }
        }

        public static string GetHumanSignature(this ParameterInfo p)
        {
            var pre = "";

            // Is Ref
            if (p.ParameterType.IsByRef)
            {
                if (p.IsOut) { pre += "out "; }
                else if (p.IsIn) { pre += "in "; }
                else { pre += "ref "; }
            }

            // 
            if (p.IsOptional) { pre += "optional "; }

            // 
            var paramTypeName = GetHumanName(p.ParameterType);
            if (paramTypeName.EndsWith('&')) { paramTypeName = paramTypeName[0..^1]; }
            return $"{pre}{paramTypeName} {p.GetHumanName()}";
        }

        public static string GetHumanName(this ParameterInfo p)
        {
            return p.Name;
        }

        public static string GetHumanName(this FieldInfo p)
        {
            if (p.IsStatic) { return $"static {p.Name}"; }
            else { return p.Name; }
        }

        public static string GetHumanName(this PropertyInfo p)
        {
            var cn = p.GetConstantValue();
            if (cn != null) { return $"{p.Name} = {cn}"; }
            else { return p.Name; }
        }
    }
}
