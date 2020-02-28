using System;
using System.Linq;
using System.Reflection;

namespace SharpDoc
{
    public static class MarkdownGenerator
    {
        public static string GenerateMarkdown(Assembly assembly)
        {
            // 
            var assemblyName = assembly.GetName().Name;
            var markdown = $"{assemblyName}\n";
            markdown += $"{new string('-', 60)}\n\n";

            foreach (var type in assembly.ExportedTypes)
            {
                markdown += GenerateMarkdown(type);
            }

            return markdown;
        }

        public static string GenerateMarkdown(Type type)
        {
            // Emit type
            var markdown = $"## {GetHumanName(type)} ({GetTypeKindString(type)})\n\n";
            var typeSummary = type.GetDocumentation();
            if (typeSummary?.Length > 0)
            {
                markdown += $"{typeSummary}\n\n";
            }

            if (type.IsEnum)
            {
                // todo: special emission of enum
            }

            // If not a delegate (may need more?), emit properties, methods, etc
            if (!type.IsSubclassOf(typeof(Delegate)))
            {
                var fields = type.GetFields().Where(m => !m.IsSpecialName && m.DeclaringType == type).ToArray();
                var properties = type.GetProperties().Where(m => !m.IsSpecialName && m.DeclaringType == type).ToArray();
                var methods = type.GetMethods().Where(m =>
                {
                    if (m.IsSpecialName || m.Name == "Equals" || m.Name == "ToString" || m.Name == "GetHashCode") { return false; }
                    return m.DeclaringType == type;
                }).ToArray();
                var events = type.GetEvents().Where(m => !m.IsSpecialName && m.DeclaringType == type).ToArray();

                // Emit Fields
                if (fields.Length > 0)
                {
                    markdown += $"### Fields\n\n";
                    foreach (var fieldInfo in fields)
                    {
                        markdown += $"#### {fieldInfo.Name}";
                        markdown += " : " + GetHumanName(fieldInfo.FieldType);
                        markdown += "\n";

                        // 
                        var summary = fieldInfo.GetDocumentation();
                        if (summary?.Length > 0)
                        {
                            markdown += $"{summary}\n\n";
                        }
                    }
                }

                // Emit Properties
                if (properties.Length > 0)
                {
                    markdown += $"### Properties\n\n";
                    foreach (var propertyInfo in properties)
                    {
                        markdown += $"#### {propertyInfo.Name}";

                        var getset = "";
                        getset += " { ";
                        if (propertyInfo.CanRead) { getset += "get; "; }
                        if (propertyInfo.CanWrite) { getset += "set; "; }
                        markdown += getset.TrimEnd() + " }";

                        markdown += " : " + GetHumanName(propertyInfo.PropertyType);
                        markdown += "\n";

                        // 
                        var summary = propertyInfo.GetDocumentation();
                        if (summary?.Length > 0)
                        {
                            markdown += $"{summary}\n\n";
                        }
                    }
                }

                // Emit Events
                if (events.Length > 0)
                {
                    markdown += $"### Events\n\n";
                    foreach (var eventInfo in events)
                    {
                        markdown += $"#### {eventInfo.Name}\n";

                        // 
                        var summary = eventInfo.GetDocumentation();
                        if (summary?.Length > 0)
                        {
                            markdown += $"{summary}\n\n";
                        }
                    }
                }

                // Emit Methods
                if (methods.Length > 0)
                {
                    markdown += $"### Methods\n\n";
                    foreach (var methodInfo in methods)
                    {
                        markdown += $"#### {GetHumanName(methodInfo)}";

                        var parameters = methodInfo.GetParameters();
                        if (parameters.Length > 0)
                        {
                            markdown += "( ";
                            markdown += string.Join(", ", parameters.Select(p => $"{p.GetHumanSignature()}"));
                            markdown += " )";
                        }
                        else
                        {
                            markdown += "()";
                        }

                        markdown += $" : {GetHumanName(methodInfo.ReturnType)}";
                        markdown += "\n";

                        // 
                        var summary = methodInfo.GetDocumentation();
                        if (summary?.Length > 0)
                        {
                            markdown += $"{summary}\n";
                        }

                        if (parameters.Length > 0)
                        {
                            markdown += "\n";

                            foreach (var p in parameters)
                            {
                                var pSummary = p.GetDocumentation();
                                if (pSummary != null) { markdown += $"<sub>**{p.GetHumanName()}**: {pSummary}</sub><br/>\n"; }
                            }
                        }

                        markdown += "\n";
                    }
                }
            }

            return markdown;
        }

        private static string GetTypeKindString(Type type)
        {
            var pre = "";

            // 
            if (type.IsEnum) { return $"{pre}Enum"; }
            else if (type.IsClass)
            {
                if (type.IsSubclassOf(typeof(Delegate))) { return "Delegate"; }
                else
                {
                    // 
                    if (type.IsAbstract) { pre += "Abstract "; }
                    if (type.IsSealed) { pre += "Sealed "; }

                    return $"{pre}Class";
                }
            }
            else if (type.IsValueType) { return $"{pre}Struct"; }
            else { return "WHAT"; }
        }

        public static string GetHumanName(this Type type)
        {
            if (type.IsArray)
            {
                var c = new string(',', type.GetArrayRank() - 1);
                return $"{GetHumanName(type.GetElementType())}[{c}]";
            }
            else if (type == typeof(bool)) { return "bool"; }
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
            else
            {
                if (type.IsGenericType)
                {
                    var name = type.Name;
                    name = name.Substring(0, name.IndexOf("`"));

                    if (type.IsConstructedGenericType)
                    {
                        var genericTypes = type.GenericTypeArguments.Select(t => GetHumanName(t));
                        return $"{name}<{string.Join("|", genericTypes)}>";
                    }
                    else
                    {
                        var genericArgs = type.GetGenericArguments().Select(t => GetHumanName(t));
                        return $"{name}<{string.Join("|", genericArgs)}>";
                    }
                }
                else
                {
                    return type.Name;
                }
            }
        }

        public static string GetHumanName(this MethodInfo method)
        {
            if (method.IsGenericMethod)
            {
                var name = method.Name;
                var indTick = name.IndexOf("`");
                if (indTick >= 0) { name = name.Substring(0, indTick); }

                var genericTypes = method.GetGenericArguments().Select(t => GetHumanName(t));
                return $"{name}<{string.Join("|", genericTypes)}>";
            }
            else
            {
                return method.Name;
            }
        }

        public static string GetHumanSignature(this ParameterInfo p)
        {
            var pre = "";
            if (p.IsIn) { pre += "in "; }
            return $"{pre}{GetHumanName(p.ParameterType)} {p.GetHumanName()}";
        }

        public static string GetHumanName(this ParameterInfo p)
        {
            return p.Name;
        }
    }
}
