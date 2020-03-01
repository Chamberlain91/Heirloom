using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpDoc
{
    public abstract class DocumentationGenerator
    {
        private const BindingFlags Declared = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        private const BindingFlags InstanceBinding = BindingFlags.Instance | Declared;
        private const BindingFlags StaticBinding = BindingFlags.Static | Declared;

        protected DocumentationGenerator(string extension)
        {
            // Remove '.'
            if (extension.StartsWith('.')) { extension = extension.Substring(1); }
            Extension = extension;
        }

        protected IReadOnlyList<ConstructorInfo> Constructors { get; private set; }

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

        protected Assembly CurrentAssembly { get; private set; }

        protected Type CurrentType { get; private set; }

        protected string Extension { get; }

        public IEnumerable<(string path, string text)> Generate(Assembly assembly, string dir)
        {
            CurrentAssembly = assembly;

            // Emit files for each type
            foreach (var type in assembly.ExportedTypes)
            {
                QueryTypeInformation(type);
                CurrentType = type;

                // Emit Type
                var text = GenerateDocument();

                // Emit filename + text pair
                var path = Path.Combine(dir, GetPath(type));

                // 
                yield return (path, text);
            }

            void QueryTypeInformation(Type type)
            {
                Constructors = type.GetConstructors(InstanceBinding).ToArray();

                // Query instance members
                InstanceFields = type.GetFields(InstanceBinding).Where(m => !m.IsSpecialName && (m.IsFamily || m.IsPublic)).ToArray();
                InstanceProperties = type.GetProperties(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();
                InstanceMethods = type.GetMethods(InstanceBinding).Where(m => !m.IsSpecialName && (m.IsFamily || m.IsPublic) && !IsIgnoredMethodName(m.Name)).ToArray();
                InstanceEvents = type.GetEvents(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();

                // Query static members
                StaticFields = type.GetFields(StaticBinding).Where(m => !m.IsSpecialName && (m.IsFamily || m.IsPublic)).ToArray();
                StaticProperties = type.GetProperties(StaticBinding).Where(m => !m.IsSpecialName).ToArray();
                StaticMethods = type.GetMethods(StaticBinding).Where(m => !m.IsSpecialName && (m.IsFamily || m.IsPublic) && !IsIgnoredMethodName(m.Name)).ToArray();
                StaticEvents = type.GetEvents(StaticBinding).Where(m => !m.IsSpecialName).ToArray();

                // Combine instance and static members
                Fields = InstanceFields.Concat(StaticFields).ToArray();
                Properties = InstanceProperties.Concat(StaticProperties).ToArray();
                Methods = InstanceMethods.Concat(StaticMethods).ToArray();
                Events = InstanceEvents.Concat(StaticEvents).ToArray();

                // 
                IsDelegate = type.IsSubclassOf(typeof(Delegate));
            }
        }

        protected virtual string GenerateDocument()
        {
            var document = GenerateTypeSummary();

            // Emit Divider
            document += GenerateSeparator();

            // Emit Enum
            if (CurrentType.IsEnum)
            {
                document += GenerateEnumBody();
            }
            else if (IsDelegate)
            {
                document += GenerateObjectBody();
            }
            else
            {
                document += GenerateMembersTable();
                document += GenerateObjectBody();
            }

            // 
            return document;
        }

        protected static bool IsIgnoredMethodName(string name)
        {
            return name == "Equals"
                || name == "ToString"
                || name == "GetHashCode"
                || name == "Finalize";
        }

        protected abstract string GenerateTypeSummary();

        protected abstract string GenerateSeparator();

        protected abstract string GenerateMembersTable();

        protected abstract string GenerateObjectBody();

        protected abstract string GenerateEnumBody();

        protected abstract string EscapeCharacters(string text);

        protected abstract string Badge(string text);

        protected abstract string Header(string text, int level);

        protected abstract string CodeBlock(string text);

        protected abstract string Code(string text);

        protected abstract string Small(string text);

        #region Get Documentation Parts

        protected string GetName(Assembly assembly)
        {
            return assembly.GetName().Name;
        }

        protected string GetSummary(MemberInfo member)
        {
            return EscapeCharacters(member.GetDocumentation() ?? string.Empty);
        }

        #region Type Info

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

            // Pointer or Reference
            if (type.IsByRef)
            {
                return GetName(type.GetElementType());
            }
            else
            // Array of Type
            if (type.IsArray)
            {
                var c = new string(',', type.GetArrayRank() - 1);
                return EscapeCharacters($"{pre}{GetName(type.GetElementType())}[{c}]");
            }
            else
            // Generic Type
            if (type.IsGenericType)
            {
                var name = type.Name;

                // Strip generic grave character
                var index = name.IndexOf("`");
                if (index >= 0) { name = name.Substring(0, index); }

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
            // Simple Type
            else
            {
                return EscapeCharacters($"{pre}{type.Name}");
            }
        }

        #region Type Link

        protected abstract string GenerateLink(Type type);

        protected string GetLink(Type type)
        {
            if (type.Assembly == typeof(int).Assembly) { return GetName(type); }
            if (type.IsGenericParameter) { return GetName(type); }
            return GenerateLink(type);
        }

        #endregion

        #endregion

        protected string GetName(ConstructorInfo p)
        {
            var name = p.DeclaringType.Name;

            if (p.DeclaringType.IsGenericType)
            {
                // Strip generic grave character
                var index = name.IndexOf("`");
                if (index >= 0) { name = name.Substring(0, index); }
                return name;
            }
            else
            {
                // 
                return name;
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

        #region Method Info

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

        protected string GetSignature(MethodInfo method)
        {
            return $"{GetName(method)}{GetParameterSignature(method)} : {GetLink(method.ReturnType)}";

        }

        protected string GetParameterSignature(MethodInfo method)
        {
            var parameters = method.GetParameters();
            return $"({string.Join(", ", parameters.Select(param => $"{GetSignature(param)}")).Trim()})";
        }

        protected string GetParameterSignature(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            return $"({string.Join(", ", parameters.Select(param => $"{GetSignature(param)}")).Trim()})";
        }

        #endregion

        #region Parameter Info

        protected string GetName(ParameterInfo p)
        {
            return p.Name;
        }

        protected string GetSignature(ParameterInfo p)
        {
            var pre = "";
            var pos = "";

            // Is Ref
            if (p.ParameterType.IsByRef)
            {
                if (p.IsOut) { pre += "out "; }
                else if (p.IsIn) { pre += "in "; }
                else { pre += "ref "; }
            }

            // Pointer?

            // Optional
            if (p.IsOptional)
            {
                var defval = p.RawDefaultValue;
                pos += $" = {defval}";
            }

            // Params
            if (p.GetCustomAttribute<ParamArrayAttribute>() != null)
            {
                pre += "params ";
            }

            // 
            var paramTypeName = GetLink(p.ParameterType);
            if (paramTypeName.EndsWith('&')) { paramTypeName = paramTypeName[0..^1]; }
            return $"{pre}{paramTypeName} {GetName(p)}{pos}";
        }

        #endregion

        #endregion

        protected Type GetSimpleType(Type type)
        {
            if (type.IsArray || type.IsByRef || type.IsPointer)
            {
                return type.GetElementType();
            }
            else
            {
                return type.UnderlyingSystemType;
            }
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

        public string GetPath(Type type)
        {
            var path = $"{type.Namespace}.{GetName(type)}.txt";
            path = Path.ChangeExtension(path, Extension);
            path = SanitizePath(path);

            if (type.Assembly != CurrentAssembly)
            {
                // ie, ../Heirloom.Math/Heirloom.Math.Matrix.md
                return $"../{GetName(type.Assembly)}/{path}";
            }
            else
            {
                // ie, Heirloom.Drawing.Graphics.md
                return path;
            }
        }

        protected static string Shorten(string text, int maxLength = 100)
        {
            text = text.Replace("\r", " ");
            text = text.Replace("\n", " ");

            if (text.Length > maxLength)
            {
                text = $"{text.Substring(0, maxLength - 3)}...";
            }

            return text;
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
    }
}
