using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpDoc
{
    public abstract class DocumentationGenerator
    {
        private const BindingFlags PublicDeclared = BindingFlags.Public | BindingFlags.DeclaredOnly;
        private const BindingFlags InstanceBinding = BindingFlags.Instance | PublicDeclared;
        private const BindingFlags StaticBinding = BindingFlags.Static | PublicDeclared;

        protected DocumentationGenerator(string extension)
        {
            // Remove '.'
            if (extension.StartsWith('.')) { extension = extension.Substring(1); }
            Extension = extension;
        }

        protected IReadOnlyList<FieldInfo> InstanceFields { get; private set; }
        protected IReadOnlyList<PropertyInfo> InstanceProperties { get; private set; }
        protected IReadOnlyList<MethodInfo> InstanceMethods { get; private set; }
        protected IReadOnlyList<EventInfo> InstanceEvents { get; private set; }

        protected IReadOnlyList<FieldInfo> StaticFields { get; private set; }
        protected IReadOnlyList<PropertyInfo> StaticProperties { get; private set; }
        protected IReadOnlyList<MethodInfo> StaticMethods { get; private set; }
        protected IReadOnlyList<EventInfo> StaticEvents { get; private set; }

        protected IReadOnlyList<FieldInfo> Fields { get; private set; }
        protected IReadOnlyList<PropertyInfo> Properties { get; private set; }
        protected IReadOnlyList<MethodInfo> Methods { get; private set; }
        protected IReadOnlyList<EventInfo> Events { get; private set; }

        protected bool IsDelegate { get; private set; }

        protected string Extension { get; }

        public IEnumerable<(string path, string text)> Generate(Assembly assembly, string dir)
        {
            // Emit files for each type
            foreach (var type in assembly.ExportedTypes)
            {
                // Emit Type
                QueryTypeInformation(type);
                var text = GenerateDocument(type);

                // Emit filename + text pair
                var path = Path.Combine(dir, GetFileName(type, Extension));
                yield return (path, text);
            }
        }

        protected abstract string GenerateDocument(Type type);

        public string GetFileName(Type type, string ext)
        {
            var path = $"{type.Namespace}.{GetName(type)}.txt";
            path = Path.ChangeExtension(path, ext);
            return SanitizePath(path);
        }

        private static string SanitizePath(string path)
        {
            // 
            path = path.Replace("\\<", "[");
            path = path.Replace('<', '[');
            path = path.Replace('>', ']');

            // 
            var filename = Path.GetFileName(path);
            foreach (var ch in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(ch, '_');
            }

            // 
            var dirname = Path.GetDirectoryName(path);
            foreach (var ch in Path.GetInvalidPathChars())
            {
                dirname = dirname.Replace(ch, '_');
            }

            // 
            return Path.Combine(dirname, filename);
        }

        protected static IEnumerable<Type> WalkInheritedTypes(Type type)
        {
            while (true)
            {
                type = type.BaseType;
                if (type == typeof(object)
                 || type == typeof(ValueType)
                 || type == typeof(Enum)
                 || type == null)
                {
                    // Reached the top of the inherits
                    break;
                }

                yield return type;
            }
        }

        protected abstract string EscapeCharacters(string text);

        private void QueryTypeInformation(Type type)
        {
            // Query instance members
            InstanceFields = type.GetFields(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();
            InstanceProperties = type.GetProperties(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();
            InstanceMethods = type.GetMethods(InstanceBinding).Where(m => !m.IsSpecialName && !IsIgnoredMethodName(m.Name)).ToArray();
            InstanceEvents = type.GetEvents(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();

            // Query static members
            StaticFields = type.GetFields(StaticBinding).Where(m => !m.IsSpecialName).ToArray();
            StaticProperties = type.GetProperties(StaticBinding).Where(m => !m.IsSpecialName).ToArray();
            StaticMethods = type.GetMethods(StaticBinding).Where(m => !m.IsSpecialName && !IsIgnoredMethodName(m.Name)).ToArray();
            StaticEvents = type.GetEvents(StaticBinding).Where(m => !m.IsSpecialName).ToArray();

            // Combine instance and static members
            Fields = InstanceFields.Concat(StaticFields).ToArray();
            Properties = InstanceProperties.Concat(StaticProperties).ToArray();
            Methods = InstanceMethods.Concat(StaticMethods).ToArray();
            Events = InstanceEvents.Concat(StaticEvents).ToArray();

            // 
            IsDelegate = type.IsSubclassOf(typeof(Delegate));
        }

        protected static bool IsIgnoredMethodName(string name)
        {
            // return name == "Equals" || name == "ToString" || name == "GetHashCode";
            return false;
        }

        #region Get Documentation Parts

        protected string GetSummary(MemberInfo member)
        {
            var escaped = EscapeCharacters(member.GetDocumentation() ?? string.Empty);
            return Summarize(escaped, 100);
        }

        private string Summarize(string summary, int len)
        {
            summary = summary.Replace("\r", " ");
            summary = summary.Replace("\n", " ");
            if (summary.Length > len)
            {
                summary = $"{summary.Substring(0, len - 3)}...";
            }
            return summary;
        }

        #endregion

        protected string GetTypeAccess(Type type)
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

        protected string GetName(Type type)
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
                pre += GetName(type.DeclaringType);
                pre += ".";
            }

            // Pointer or reference
            if (type.IsByRef)
            {
                return GetName(type.GetElementType());
            }
            else
            // Array
            if (type.IsArray)
            {
                var c = new string(',', type.GetArrayRank() - 1);
                return EscapeCharacters($"{pre}{GetName(type.GetElementType())}[{c}]");
            }
            else
            // Generic
            if (type.IsGenericType)
            {
                var name = type.Name; // Get name and strip grave character
                name = name.Substring(0, name.IndexOf("`"));

                if (type.IsConstructedGenericType)
                {
                    var genericTypes = type.GenericTypeArguments.Select(t => GetName(t));
                    return EscapeCharacters($"{pre}{name}<{string.Join("|", genericTypes)}>");
                }
                else
                {
                    var genericArgs = type.GetGenericArguments().Select(t => GetName(t));
                    return EscapeCharacters($"{pre}{name}<{string.Join("|", genericArgs)}>");
                }
            }
            // 
            else
            {
                return EscapeCharacters($"{pre}{type.Name}");
            }
        }

        protected string GetName(FieldInfo p)
        {
            return p.Name;
        }

        protected string GetName(PropertyInfo p)
        {
            return p.Name;
        }

        protected string GetName(EventInfo p)
        {
            return p.Name;
        }

        protected string GetName(MethodInfo method)
        {
            if (method.IsGenericMethod)
            {
                var name = method.Name;
                var indTick = name.IndexOf("`");
                if (indTick >= 0) { name = name.Substring(0, indTick); }

                var genericTypes = method.GetGenericArguments().Select(t => GetName(t));
                return $"{name}<{string.Join("|", genericTypes)}>";
            }
            else
            {
                return $"{method.Name}";
            }
        }

        protected string GetSignature(ParameterInfo p)
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
            var paramTypeName = GetName(p.ParameterType);
            if (paramTypeName.EndsWith('&')) { paramTypeName = paramTypeName[0..^1]; }
            return $"{pre}{paramTypeName} {GetName(p)}";
        }

        protected string GetName(ParameterInfo p)
        {
            return p.Name;
        }
    }
}
