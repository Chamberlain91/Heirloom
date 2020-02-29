using System;
using System.Linq;
using System.Reflection;

namespace SharpDoc
{
    public static class MarkdownGenerator
    {
        private const BindingFlags PublicDeclared = BindingFlags.Public | BindingFlags.DeclaredOnly;
        private const BindingFlags InstanceBinding = BindingFlags.Instance | PublicDeclared;
        private const BindingFlags StaticBinding = BindingFlags.Static | PublicDeclared;

        public static bool IsIgnoredMethodName(string name)
        {
            // return name == "Equals" || name == "ToString" || name == "GetHashCode";
            return false;
        }

        public static string GenerateMarkdown(Type type)
        {
            // Emit type
            var markdown = $"## {type.GetHumanName()} ({type.GetTypeAccess()})\n";
            markdown += $"<sub>Namespace: {type.Namespace}</sub>\n\n";

            var typeSummary = type.GetDocumentation();
            if (typeSummary?.Length > 0)
            {
                markdown += $"{typeSummary}\n\n";
            }

            if (type.IsEnum)
            {
                markdown += $"\n";

                // todo: special emission of enum
                foreach (var name in type.GetEnumNames())
                {
                    var value = Convert.ToInt64(Enum.Parse(type, name));
                    markdown += $"#### {name} = 0x{value.ToString("X")}\n";
                    markdown += Documentation.GetDocumentation($"F:{type.FullName}.{name}");
                    markdown += "\n\n";
                }
            }

            // If not a delegate (may need more?), emit properties, methods, etc
            if (!type.IsSubclassOf(typeof(Delegate)))
            {
                // Query instance members
                var fields = type.GetFields(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();
                var properties = type.GetProperties(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();
                var methods = type.GetMethods(InstanceBinding).Where(m => !m.IsSpecialName && !IsIgnoredMethodName(m.Name)).ToArray();
                var events = type.GetEvents(InstanceBinding).Where(m => !m.IsSpecialName).ToArray();

                // Query static members
                var staticFields = type.GetFields(StaticBinding).Where(m => !m.IsSpecialName).ToArray();
                var staticProperties = type.GetProperties(StaticBinding).Where(m => !m.IsSpecialName).ToArray();
                var staticMethods = type.GetMethods(StaticBinding).Where(m => !m.IsSpecialName && !IsIgnoredMethodName(m.Name)).ToArray();
                var staticEvents = type.GetEvents(StaticBinding).Where(m => !m.IsSpecialName).ToArray();

                // Emit Static Fields
                if (staticFields.Length > 0)
                {
                    markdown += $"### Static Fields\n\n";
                    foreach (var fieldInfo in staticFields)
                    {
                        markdown += $"#### {fieldInfo.GetHumanName()}";
                        markdown += " : " + fieldInfo.FieldType.GetHumanName();
                        markdown += "\n";

                        // 
                        var summary = fieldInfo.GetDocumentation();
                        if (summary?.Length > 0)
                        {
                            markdown += $"{summary}\n\n";
                        }
                    }
                }

                // Emit Static Properties
                if (staticProperties.Length > 0)
                {
                    markdown += $"### Static Properties\n\n";
                    foreach (var propertyInfo in staticProperties)
                    {
                        markdown += $"#### {propertyInfo.Name}";

                        var getset = "";
                        getset += " { ";
                        if (propertyInfo.CanRead) { getset += "get; "; }
                        if (propertyInfo.CanWrite) { getset += "set; "; }
                        markdown += getset.TrimEnd() + " }";

                        markdown += " : " + propertyInfo.PropertyType.GetHumanName();
                        markdown += "\n";

                        // 
                        var summary = propertyInfo.GetDocumentation();
                        if (summary?.Length > 0)
                        {
                            markdown += $"{summary}\n\n";
                        }
                    }
                }

                // Emit Fields
                if (fields.Length > 0)
                {
                    markdown += $"### Fields\n\n";
                    foreach (var fieldInfo in fields)
                    {
                        markdown += $"#### {fieldInfo.GetHumanName()}";
                        markdown += " : " + fieldInfo.FieldType.GetHumanName();
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

                        markdown += " : " + propertyInfo.PropertyType.GetHumanName();
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
                        markdown += $"#### {methodInfo.GetHumanName()}";

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

                        markdown += $" : {methodInfo.ReturnType.GetHumanName()}";
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
    }
}
