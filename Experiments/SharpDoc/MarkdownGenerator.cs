using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpDoc
{
    public class MarkdownGenerator : DocumentationGenerator
    {
        public MarkdownGenerator()
            : base("md")
        { }

        protected override string EscapeCharacters(string text)
        {
            return text.Replace("<", "\\<");
        }

        protected override string GenerateDocument(Type type)
        {
            // Emit header
            var markdown = $"{type.Assembly.GetName().Name} - {GetName(type)} ({GetTypeAccess(type)})\n";
            markdown += $"{new string('-', markdown.Length - 2)}\n\n";

            // Emit Summary
            var typeSummary = type.GetDocumentation();
            if (typeSummary?.Length > 0)
            {
                markdown += $"{typeSummary}\n\n";
            }

            // Emit
            markdown += $"<sub>**Namespace**: {type.Namespace}</sub>  \n";
            var inherits = WalkInheritedTypes(type).Select(t => GetName(t));
            if (inherits.Any()) { markdown += $"<sub>**Inherits**: {string.Join(", ", inherits)}</sub>  \n"; }
            var interfaces = type.GetInterfaces().Select(t => GetName(t));
            if (interfaces.Any()) { markdown += $"<sub>**Interfaces**: {string.Join(", ", interfaces)}</sub>  \n"; }

            // Emit Divider
            markdown += "\n---\n\n";

            // Emit Enum
            if (type.IsEnum)
            {
                foreach (var name in type.GetEnumNames())
                {
                    // Emit Enum Field
                    markdown += $"### {name}\n";
                    markdown += Documentation.GetDocumentation($"F:{type.FullName}.{name}");
                    markdown += "\n\n";
                }
            }
            // If not a delegate (may need more?), emit properties, methods, etc
            else if (!IsDelegate)
            {
                //
                if (Fields.Count > 0)
                {
                    markdown += "| Field | Summary |\n";
                    markdown += "|-------|---------|\n";
                    foreach (var m in Fields) { markdown += $"| {GetName(m)} | {GetSummary(m)} |\n"; }
                    markdown += "\n";
                }

                if (Properties.Count > 0)
                {
                    markdown += "| Properties | Summary |\n";
                    markdown += "|------------|---------|\n";
                    foreach (var m in Properties) { markdown += $"| {GetName(m)} | {GetSummary(m)} |\n"; }
                    markdown += "\n";
                }

                if (Events.Count > 0)
                {
                    markdown += "| Events | Summary |\n";
                    markdown += "|--------|---------|\n";
                    foreach (var m in Events) { markdown += $"| {GetName(m)} | {GetSummary(m)} |\n"; }
                    markdown += "\n";
                }

                if (Methods.Count > 0)
                {
                    markdown += "| Methods | Summary |\n";
                    markdown += "|---------|---------|\n";
                    foreach (var m in Methods) { markdown += $"| {GetMethodSignature(m)} | {GetSummary(m)} |\n"; }
                    markdown += "\n";
                }

                // Emit Fields
                if (Fields.Count > 0)
                {
                    markdown += $"### Fields\n\n";
                    foreach (var fieldInfo in Fields)
                    {
                        markdown += GenerateField(fieldInfo, false);
                    }
                }

                // Emit Properties
                if (Properties.Count > 0)
                {
                    markdown += $"### Properties\n\n";

                    foreach (var prop in InstanceProperties)
                    {
                        markdown += GenerateProperty(prop, false);
                    }

                    foreach (var prop in StaticProperties)
                    {
                        markdown += GenerateProperty(prop, true);
                    }
                }

                // Emit Events
                if (Events.Count > 0)
                {
                    markdown += $"### Events\n\n";
                    foreach (var eventInfo in Events)
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
                if (Methods.Count > 0)
                {
                    markdown += $"### Methods\n\n";
                    foreach (var methodInfo in Methods)
                    {
                        markdown += $"#### {GetMethodSignature(methodInfo)}";
                        markdown += "\n";

                        // 
                        var summary = methodInfo.GetDocumentation();
                        if (summary?.Length > 0)
                        {
                            markdown += $"{summary}\n";
                        }

                        var ps = methodInfo.GetParameters();
                        if (ps.Length > 0)
                        {
                            markdown += "\n";

                            foreach (var p in ps)
                            {
                                var pSummary = p.GetDocumentation();
                                if (pSummary != null) { markdown += $"<sub>**{GetName(p)}**: {pSummary}</sub><br/>\n"; }
                            }
                        }

                        markdown += "\n";
                    }
                }
            }

            return markdown;
        }

        private string GetMethodSignature(MethodInfo methodInfo)
        {
            return $"{GetName(methodInfo)}{GetParameterSignature(methodInfo)} : {GetName(methodInfo.ReturnType)}";
        }

        private string GetParameterSignature(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            if (parameters.Length > 0)
            {
                return $" ({string.Join(", ", parameters.Select(p => $"{GetSignature(p)}"))})";
            }
            else
            {
                return "()";
            }
        }

        private string GenerateField(FieldInfo fieldInfo, bool isStatic)
        {
            var output = $"#### {GetName(fieldInfo)} : {GetName(fieldInfo.FieldType)}";
            if (isStatic) { output += " `Static`"; }
            output += "\n";

            // 
            var summary = fieldInfo.GetDocumentation();
            if (summary?.Length > 0)
            {
                output += $"{summary}\n\n";
            }

            return output;
        }

        private string GenerateProperty(PropertyInfo property, bool isStatic)
        {
            // Emit Name
            var output = $"#### {GetName(property)} : {GetName(property.PropertyType)}";

            var badges = new List<string>();

            // Emit Static
            if (isStatic) { badges.Add("Static"); }

            // Emit GET 
            if (property.CanRead && (property.GetMethod.IsPublic || property.GetMethod.IsFamily))
            {
                badges.Add("Get");
            }

            // Emit SET
            if (property.CanWrite && (property.SetMethod.IsPublic || property.SetMethod.IsFamily))
            {
                badges.Add("Set");
            }

            // 
            output += GenerateBadgeList(badges);
            output += $"\n";

            // 
            var summary = property.GetDocumentation();
            if (summary?.Length > 0)
            {
                output += $"{summary}\n\n";
            }

            return output;
        }

        private string GenerateBadgeList(IEnumerable<string> tokens)
        {
            return tokens.Any()
                ? $" <small>{string.Join(" ", tokens.Select(s => $"`{s}`")).Trim()}</small>"
                : string.Empty;
        }
    }
}
