using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Heirloom.GenDoc
{
    internal static class Documentation
    // based on: https://github.com/ZacharyPatten/Towel/blob/master/Sources/Towel/Meta.cs 
    {
        #region XML Code Documentation

        private static readonly Dictionary<string, XElement> _documentation = new Dictionary<string, XElement>();
        private static readonly HashSet<Assembly> _assemblies = new HashSet<Assembly>();

        public static IEnumerable<Assembly> Assemblies => _assemblies;

        internal static void LoadDocumentation(Assembly assembly)
        {
            if (_assemblies.Contains(assembly))
            {
                return;
            }

            // Get assembly directory and related .xml
            var dirPath = Path.GetDirectoryName(assembly.Location);
            var xmlPath = Path.Combine(dirPath, assembly.GetName().Name + ".xml");

            // Does the XML exist?
            if (File.Exists(xmlPath))
            {
                // Load XML Document
                var xmldoc = XDocument.Parse(File.ReadAllText(xmlPath));
                var members = xmldoc.Descendants("member");

                // Get child of the members node
                foreach (var member in members)
                {
                    var name = member.Attribute("name").Value;
                    _documentation[name] = member;
                }
            }

            // Mark assembly as visited
            _assemblies.Add(assembly);
        }

        public static bool TryGetType(string name, out Type type)
        {
            // Find type
            foreach (var assembly in _assemblies)
            {
                type = assembly.GetType(name);
                if (type != null) { return true; }
            }

            // Couldn't find type
            type = default;
            return false;
        }

        /// <summary>
        /// Gets loaded documentation by the key encoded in the XML documentation.
        /// </summary>
        public static XElement GetDocumentation(string key)
        {
            _documentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>
        /// Gets the XML documentation for the specified type.
        /// </summary>
        public static XElement GetDocumentation(Type type)
        {
            LoadDocumentation(type.Assembly);
            var key = "T:" + XmlDocumentationKeyHelper(type.FullName, null);
            _documentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>
        /// Gets the XML documentation for the specified member.
        /// </summary>
        public static XElement GetDocumentation(MemberInfo member)
        {
            if (member is FieldInfo fieldInfo)
            {
                return GetDocumentation(fieldInfo);
            }
            else if (member is PropertyInfo propertyInfo)
            {
                return GetDocumentation(propertyInfo);
            }
            else if (member is EventInfo eventInfo)
            {
                return GetDocumentation(eventInfo);
            }
            else if (member is ConstructorInfo constructorInfo)
            {
                return GetDocumentation(constructorInfo);
            }
            else if (member is MethodInfo methodInfo)
            {
                return GetDocumentation(methodInfo);
            }
            else if (member is Type type) // + TypeInfo
            {
                return GetDocumentation(type);
            }
            else if (member.MemberType.HasFlag(MemberTypes.Custom))
            {
                // This represents a cutom type that is not part of
                // the standard .NET languages as far as I'm aware.
                // This will never be supported so return null.
                return null;
            }
            else
            {
                // Hopefully this will never hit. At the time of writing
                // this code, I am only aware of the following Member types:
                // FieldInfo, PropertyInfo, EventInfo, ConstructorInfo,
                // MethodInfo, and Type.
                throw new InvalidOperationException(nameof(GetDocumentation)
                                                    + " encountered an unhandled type ["
                                                    + member.GetType().FullName
                                                    + "]. "
                                                    + "Please submit this issue to the Towel GitHub repository. "
                                                    + "https://github.com/ZacharyPatten/Towel/issues/new/choose");
            }
        }

        /// <summary>
        /// Gets the XML documentation for the specified method.
        /// </summary>
        private static XElement GetDocumentation(MethodInfo method)
        {
            LoadDocumentation(method.DeclaringType.Assembly);

            var typeGenericMap = new Dictionary<string, int>();
            var tempTypeGeneric = 0;
            Array.ForEach(method.DeclaringType.GetGenericArguments(), x => typeGenericMap[x.Name] = tempTypeGeneric++);

            var methodGenericMap = new Dictionary<string, int>();
            var tempMethodGeneric = 0;
            Array.ForEach(method.GetGenericArguments(), x => methodGenericMap.Add(x.Name, tempMethodGeneric++));

            var parameterInfos = method.GetParameters();

            var memberTypePrefix = "M:";
            var declarationTypeString = GetXmlDocumenationFormattedString(method.DeclaringType, false, typeGenericMap, methodGenericMap);
            var memberNameString = method.Name;
            var methodGenericArgumentsString =
                methodGenericMap.Count > 0 ?
                "``" + methodGenericMap.Count :
                string.Empty;
            var parametersString =
                parameterInfos.Length > 0 ?
                "(" + string.Join(",", method.GetParameters().Select(x => GetXmlDocumenationFormattedString(x.ParameterType, true, typeGenericMap, methodGenericMap))) + ")" :
                string.Empty;

            var key =
                memberTypePrefix +
                declarationTypeString +
                "." +
                memberNameString +
                methodGenericArgumentsString +
                parametersString;

            if (method.Name == "op_Implicit" ||
                method.Name == "op_Explicit")
            {
                key += "~" + GetXmlDocumenationFormattedString(method.ReturnType, true, typeGenericMap, methodGenericMap);
            }

            _documentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>
        /// Gets the XML documentation for the specified constructor method.
        /// </summary>
        private static XElement GetDocumentation(ConstructorInfo constructor)
        {
            LoadDocumentation(constructor.DeclaringType.Assembly);

            var typeGenericMap = new Dictionary<string, int>();
            var tempTypeGeneric = 0;
            Array.ForEach(constructor.DeclaringType.GetGenericArguments(), x => typeGenericMap[x.Name] = tempTypeGeneric++);

            // constructors don't support generic types so this will always be empty
            var methodGenericMap = new Dictionary<string, int>();

            var parameterInfos = constructor.GetParameters();

            var memberTypePrefix = "M:";
            var declarationTypeString = GetXmlDocumenationFormattedString(constructor.DeclaringType, false, typeGenericMap, methodGenericMap);
            var memberNameString = "#ctor";
            var parametersString =
                parameterInfos.Length > 0 ?
                "(" + string.Join(",", constructor.GetParameters().Select(x => GetXmlDocumenationFormattedString(x.ParameterType, true, typeGenericMap, methodGenericMap))) + ")" :
                string.Empty;

            var key =
                memberTypePrefix +
                declarationTypeString +
                "." +
                memberNameString +
                parametersString;

            _documentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>
        /// Gets the XML documentation for the specified property.
        /// </summary>
        private static XElement GetDocumentation(PropertyInfo property)
        {
            LoadDocumentation(property.DeclaringType.Assembly);
            var key = "P:" + XmlDocumentationKeyHelper(property.DeclaringType.FullName, property.Name);
            _documentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>
        /// Gets the XML documentation for the specified field.
        /// </summary>
        private static XElement GetDocumentation(FieldInfo field)
        {
            LoadDocumentation(field.DeclaringType.Assembly);
            var key = "F:" + XmlDocumentationKeyHelper(field.DeclaringType.FullName, field.Name);
            _documentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>
        /// Gets the XML documentation for the specified event.
        /// </summary>
        private static XElement GetDocumentation(EventInfo @event)
        {
            LoadDocumentation(@event.DeclaringType.Assembly);
            var key = "E:" + XmlDocumentationKeyHelper(@event.DeclaringType.FullName, @event.Name);
            _documentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>
        /// Gets the XML documentation for the specified parameter.
        /// </summary>
        public static XElement GetDocumentation(ParameterInfo parameter)
        {
            var member = GetDocumentation(parameter.Member);
            if (member != null)
            {
                var @params = member.Descendants("param")
                                    .Where(e => e.Attribute("name").Value == parameter.Name);

                // 
                return @params.FirstOrDefault();
            }

            // No known documentation for parent member
            return null;
        }

        /// <summary>
        /// Determines if a MethodInfo is a local function.
        /// </summary>
        public static bool IsLocalFunction(MethodInfo methodInfo)
        {
            return Regex.Match(methodInfo.Name, @"g__.+\|\d+_\d+").Success;
        }

        internal static string XmlDocumentationKeyHelper(string typeFullNameString, string memberNameString)
        {
            var key = Regex.Replace(typeFullNameString, @"\[.*\]", string.Empty).Replace('+', '.');
            if (!(memberNameString is null))
            {
                key += "." + memberNameString;
            }
            return key;
        }

        internal static string GetXmlDocumenationFormattedString(
            Type type,
            bool isMethodParameter,
            Dictionary<string, int> typeGenericMap,
            Dictionary<string, int> methodGenericMap)
        {
            if (type.IsGenericParameter)
            {
                return methodGenericMap.TryGetValue(type.Name, out var methodIndex)
                    ? "``" + methodIndex
                    : "`" + typeGenericMap[type.Name];
            }
            else if (type.HasElementType)
            {
                var elementTypeString = GetXmlDocumenationFormattedString(
                    type.GetElementType(),
                    isMethodParameter,
                    typeGenericMap,
                    methodGenericMap);

                if (type.IsPointer)
                {
                    return elementTypeString + "*";
                }
                else if (type.IsArray)
                {
                    var rank = type.GetArrayRank();
                    var arrayDimensionsString = rank > 1
                        ? "[" + string.Join(",", Enumerable.Repeat("0:", rank)) + "]"
                        : "[]";
                    return elementTypeString + arrayDimensionsString;
                }
                else if (type.IsByRef)
                {
                    return elementTypeString + "@";
                }
                else
                {
                    // Hopefully this will never hit. At the time of writing
                    // this code, type.HasElementType is only true if the type
                    // is a pointer, array, or by reference.
                    throw new InvalidOperationException(nameof(GetXmlDocumenationFormattedString)
                                                        + " encountered an unhandled element type. "
                                                        + "Please submit this issue to the Towel GitHub repository. "
                                                        + "https://github.com/ZacharyPatten/Towel/issues/new/choose");
                }
            }
            else
            {
                var prefaceString = type.IsNested
                    ? GetXmlDocumenationFormattedString(
                        type.DeclaringType,
                        isMethodParameter,
                        typeGenericMap,
                        methodGenericMap) + "."
                    : type.Namespace + ".";

                string typeNameString = isMethodParameter
                    ? typeNameString = Regex.Replace(type.Name, @"`\d+", string.Empty)
                    : typeNameString = type.Name;

                var genericArgumentsString = type.IsGenericType && isMethodParameter
                    ? "{" + string.Join(",",
                        type.GetGenericArguments().Select(argument =>
                            GetXmlDocumenationFormattedString(
                                argument,
                                isMethodParameter,
                                typeGenericMap,
                                methodGenericMap))
                        ) + "}"
                    : string.Empty;

                return prefaceString + typeNameString + genericArgumentsString;
            }
        }

        #endregion
    }
}
