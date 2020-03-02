using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Heirloom.GenDoc
{
    public abstract class DocumentationGenerator
    {
        private const BindingFlags Declared = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        private const BindingFlags InstanceBinding = BindingFlags.Instance | Declared;
        private const BindingFlags StaticBinding = BindingFlags.Static | Declared;

        private static readonly Regex _spaces = new Regex(@"\s+", RegexOptions.Compiled);

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

        public void Generate(Assembly assembly, string dir)
        {
            CurrentAssembly = assembly;

            // Generate TOC
            {
                var text = GenerateAssemblySummary();
                var path = Path.Combine(dir, $"{GetName(assembly)}.{Extension}");
                File.WriteAllText(path, text);
            }

            // Emit files for each type
            foreach (var type in TypeDatabase.GetAssemblyTypes(assembly))
            {
                TypeDatabase.TryGetType(type.ToString(), out var t);
                if (t != type) { Console.WriteLine(type.ToString().ToUpper()); }

                var text = GenerateDocument(type);
                var path = Path.Combine(dir, GetTypePath(type));
                File.WriteAllText(path, text);
            }
        }

        public void GenerateIndex(IEnumerable<Assembly> assemblies, string dir)
        {
            var text = Header("Documentation", 1);

            foreach (var assembly in assemblies)
            {
                var name = GetName(assembly);
                text += Link(name, GetAssemblyPath(assembly, false)) + "  \n";
            }

            // 
            var path = Path.Combine(dir, $"Documentation.{Extension}");
            File.WriteAllText(path, text);
        }

        protected string GenerateDocument(Type type)
        {
            CurrentType = type;
            QueryTypeInformation(type);

            // Emit Assembly Header
            var text = GenerateAssemblyHeader();

            // Emit Type Summary
            text += GenerateTypeSummary();

            // Emit Enum
            if (CurrentType.IsEnum)
            {
                text += GenerateEnumBody();
            }
            else if (IsDelegate)
            {
                text += GenerateObjectBody();
            }
            else
            {
                text += GenerateMembersTable();
                text += GenerateObjectBody();
            }

            // 
            return text;

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

        protected static bool IsIgnoredMethodName(string name)
        {
            return name == "Equals"
                || name == "ToString"
                || name == "GetHashCode"
                || name == "Finalize";
        }

        protected string GenerateAssemblyHeader()
        {
            // Emit header
            var text = Header(GetName(CurrentAssembly), 1);
            text += "\n";

            // Emit Framework Text
            {
                var framework = CurrentAssembly.GetCustomAttribute<TargetFrameworkAttribute>();
                var displayName = framework.FrameworkDisplayName;
                if (string.IsNullOrWhiteSpace(displayName)) { displayName = framework.FrameworkName; }
                text += Small(Bold("Framework") + ": " + displayName) + "  \n";
            }

            // Emit Assembly Link
            {
                var path = GetAssemblyPath(CurrentAssembly);
                var link = Link(GetName(CurrentAssembly), path);
                text += Small(Bold("Assembly") + ": " + link) + "  \n";
            }

            // Emit assembly names where internals are visible
            var internalVisibleAttrs = CurrentAssembly.GetCustomAttributes<InternalsVisibleToAttribute>();
            if (internalVisibleAttrs.Any())
            {
                var list = new List<string>();
                foreach (var attr in internalVisibleAttrs)
                {
                    var path = GetAssemblyPath(attr.AssemblyName);
                    var link = Link(attr.AssemblyName, path);
                    list.Add(link);
                }

                text += Small(Bold("Internals Visible To") + $": {string.Join(", ", list)}");
                text += "  \n";
            }

            // Emit assembly names where internals are visible
            var references = CurrentAssembly.GetReferencedAssemblies();
            if (references.Length > 1)
            {
                var list = new List<string>();
                foreach (var referenceName in references)
                {
                    if (referenceName.Name == "netstandard") { continue; }

                    var path = GetAssemblyPath(referenceName.Name);
                    var link = Link(referenceName.Name, path);
                    list.Add(link);
                }

                text += Small(Bold("Dependancies") + $": {string.Join(", ", list)}");
                text += "  \n";
            }

            text += "\n";
            return text;
        }

        protected string GenerateAssemblySummary()
        {
            var text = GenerateAssemblyHeader();

            // 
            text += GenerateSeparator();

            var types = TypeDatabase.GetAssemblyTypes(CurrentAssembly);

            var classes = types.Where(t => t.IsClass && !t.IsSubclassOf(typeof(Delegate)));
            if (classes.Any())
            {
                text += Header("Classes", 4);

                foreach (var type in classes)
                {
                    text += $"{Link(type)}  \n";
                }

                text += "\n";
            }

            var structs = types.Where(t => t.IsValueType && !t.IsEnum);
            if (structs.Any())
            {
                text += Header("Structs", 4);

                foreach (var type in structs)
                {
                    text += $"{Link(type)}  \n";
                }

                text += "\n";
            }

            var enums = types.Where(t => t.IsEnum);
            if (enums.Any())
            {
                text += Header("Enums", 4);

                foreach (var type in enums)
                {
                    text += $"{Link(type)}  \n";
                }

                text += "\n";
            }

            var delegates = types.Where(t => t.IsSubclassOf(typeof(Delegate)));
            if (delegates.Any())
            {
                text += Header("Delegates", 4);

                foreach (var type in delegates)
                {
                    text += $"{Link(type)}  \n";
                }
            }

            return text;
        }

        protected abstract string GenerateTypeSummary();

        protected abstract string GenerateSeparator();

        protected abstract string GenerateMembersTable();

        protected abstract string GenerateObjectBody();

        protected abstract string GenerateEnumBody();

        #region Generate Elements

        protected abstract string Link(string text, string target);

        protected abstract string EscapeCharacters(string text);

        protected abstract string Badge(string text);

        protected abstract string Header(string text, int level);

        protected abstract string CodeBlock(string text);

        protected abstract string Code(string text);

        protected abstract string Small(string text);

        protected abstract string Bold(string text);

        #endregion

        #region Get Documentation Parts

        protected string GetSummary(Type type)
        {
            var summary = Documentation.GetDocumentation(type)?.Element("summary");
            return ExtractText(summary);
        }

        protected string GetRemarks(Type type)
        {
            var remarks = Documentation.GetDocumentation(type)?.Element("remarks");
            return ExtractText(remarks);
        }

        protected string GetSummary(MemberInfo member)
        {
            var summary = Documentation.GetDocumentation(member)?.Element("summary");
            return ExtractText(summary);
        }

        protected string GetRemarks(MemberInfo member)
        {
            var remarks = Documentation.GetDocumentation(member)?.Element("remarks");
            return ExtractText(remarks);
        }

        private string ExtractText(XElement element)
        {
            if (element != null)
            {
                var text = "";

                foreach (var node in element.Nodes())
                {
                    text += EscapeCharacters(GetNodeText(node));
                }

                return text.Trim();
            }
            else
            {
                // No documentation found
                return string.Empty;
            }
        }

        #region Assembly

        public string GetAssemblyPath(string assemblyName, bool back = true)
        {
            var oath = $"{assemblyName}/{assemblyName}.{Extension}";
            return back ? $"../{oath}" : $"./{oath}";
        }

        public string GetAssemblyPath(Assembly assembly, bool back = true)
        {
            return GetAssemblyPath(GetName(assembly), back);
        }

        protected string GetName(Assembly assembly)
        {
            return assembly.GetName().Name;
        }

        #endregion

        #region Types

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
                    return EscapeCharacters($"{pre}{name}<{string.Join(", ", genericTypes)}>");
                }
                else
                {
                    var genericArgs = type.GetGenericArguments().Select(t => GetName(t));
                    return EscapeCharacters($"{pre}{name}<{string.Join(", ", genericArgs)}>");
                }
            }
            // Simple Type
            else
            {
                return EscapeCharacters($"{pre}{type.Name}");
            }
        }

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

        protected string Link(Type type)
        {
            var sType = GetSimpleType(type);

            if (sType.Assembly == typeof(int).Assembly) { return GetName(sType); }
            if (sType.IsGenericParameter) { return GetName(sType); }

            return Link(GetName(type), GetTypePath(sType));
        }

        protected static IEnumerable<Type> WalkTypeInheritance(Type type)
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

        public string GetTypePath(Type type)
        {
            var path = $"{type.Namespace}.{GetName(type)}.txt";
            path = Path.ChangeExtension(path, Extension);
            path = SanitizePath(path);

            if (type.Assembly != CurrentAssembly)
            {
                // ie, ../Heirloom.Math/Heirloom.Math.Matrix.md
                var dir = Path.GetDirectoryName(GetAssemblyPath(type.Assembly));
                return SanitizePath(Path.Combine(dir, path));
            }
            else
            {
                // ie, Heirloom.Drawing.Graphics.md
                return path;
            }
        }

        #endregion

        #region Constructor

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

        #endregion

        #region Field

        protected string GetName(FieldInfo p)
        {
            return p.Name;
        }

        #endregion

        #region Property

        protected string GetName(PropertyInfo p)
        {
            return p.Name;
        }

        #endregion

        #region Event

        protected string GetName(EventInfo p)
        {
            return p.Name;
        }

        #endregion

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
            return $"{GetName(method)}{GetParameterSignature(method)} : {Link(method.ReturnType)}";

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
                var defval = p.DefaultValue;
                if (defval == null) { defval = "null"; }
                else if (defval is string) { defval = $"\"{defval}\""; }
                pos += $" = {defval}";
            }

            // Params
            if (p.GetCustomAttribute<ParamArrayAttribute>() != null)
            {
                pre += "params ";
            }

            // 
            var paramTypeName = Link(p.ParameterType);
            if (paramTypeName.EndsWith('&')) { paramTypeName = paramTypeName[0..^1]; }
            return $"{pre}{paramTypeName} {GetName(p)}{pos}";
        }

        #endregion

        private string GetNodeText(XNode node)
        {
            if (node is XElement element)
            {
                if (element.Name == "para")
                {
                    if (element.IsEmpty)
                    {
                        return "  \n";
                    }
                    else
                    {
                        var text = NormalizeSpaces(GetNodeText(element.FirstNode));
                        return $"{text}  \n";
                    }
                }
                else if (element.Name == "see")
                {
                    var cref = element.Attribute("cref").Value;
                    return TypeKeyLink(cref);
                }
                else if (element.Name == "paramref")
                {
                    var name = element.Attribute("name")?.Value ?? string.Empty;
                    return Code(name);
                }
            }

            // No other known extraction path.
            return NormalizeSpaces(node.ToString());
        }

        private string TypeKeyLink(string key)
        {
            var parts = key.Split(":");
            var k = parts[0];
            var p = parts[1];

            switch (k)
            {
                case "T":
                {
                    if (TypeDatabase.TryGetType(p, out var type))
                    {
                        return Link(type);
                    }

                    break;
                }

                case "F":
                    break;

                case "P":
                    break;

                case "M":
                    break;

                case "E":
                    break;
            }

            // 
            return Code(p);

        }

        #endregion

        protected static string NormalizeSpaces(string text)
        {
            return _spaces.Replace(text, " ");
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
            path = path.Replace(" ", string.Empty);

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
            return Path.Combine(dirname, filename)
                       .Replace("\\", "/");
        }
    }
}
