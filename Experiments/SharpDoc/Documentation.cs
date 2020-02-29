using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace SharpDoc
{
    internal static class Documentation
    {
        // ref: https://github.com/ZacharyPatten/Towel/blob/master/Sources/Towel/Meta.cs

        #region XML Code Documentation

        private static readonly HashSet<Assembly> _loadedAssemblies = new HashSet<Assembly>();
        private static readonly Dictionary<string, string> _loadedXmlDocumentation = new Dictionary<string, string>();

        internal static void LoadXmlDocumentation(Assembly assembly)
        {
            if (_loadedAssemblies.Contains(assembly))
            {
                return;
            }

            var directoryPath = Path.GetDirectoryName(assembly.Location);
            var xmlFilePath = Path.Combine(directoryPath, assembly.GetName().Name + ".xml");

            if (File.Exists(xmlFilePath))
            {
                // Load XML Document
                var doc = new XmlDocument();
                doc.LoadXml(File.ReadAllText(xmlFilePath));

                var members = doc["doc"]["members"];
                foreach (var el in members.ChildNodes)
                {
                    if (el is XmlElement member)
                    {
                        // todo: construct summary better via concating and evaluating <see> <para> etc...?
                        var summary = member.InnerXml.Trim();
                        var name = member.Attributes["name"].Value;

                        _loadedXmlDocumentation[name] = summary;
                    }
                }
            }

            // currently marking assembly as loaded even if the XML file was not found
            // may want to adjust in future, but I think this is good for now
            _loadedAssemblies.Add(assembly);
        }

        /// <summary>Clears the currently loaded XML documentation.</summary>
        public static void ClearXmlDocumentation()
        {
            _loadedAssemblies.Clear();
            _loadedXmlDocumentation.Clear();
        }

        public static string GetDocumentation(string key)
        {
            _loadedXmlDocumentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a type.</summary>
        /// <param name="type">The type to get the XML documentation of.</param>
        /// <returns>The XML documentation on the type.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this Type type)
        {
            LoadXmlDocumentation(type.Assembly);
            var key = "T:" + XmlDocumentationKeyHelper(type.FullName, null);
            _loadedXmlDocumentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a method.</summary>
        /// <param name="methodInfo">The method to get the XML documentation of.</param>
        /// <returns>The XML documentation on the method.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this MethodInfo methodInfo)
        {
            LoadXmlDocumentation(methodInfo.DeclaringType.Assembly);

            var typeGenericMap = new Dictionary<string, int>();
            var tempTypeGeneric = 0;
            Array.ForEach(methodInfo.DeclaringType.GetGenericArguments(), x => typeGenericMap[x.Name] = tempTypeGeneric++);

            var methodGenericMap = new Dictionary<string, int>();
            var tempMethodGeneric = 0;
            Array.ForEach(methodInfo.GetGenericArguments(), x => methodGenericMap.Add(x.Name, tempMethodGeneric++));

            var parameterInfos = methodInfo.GetParameters();

            var memberTypePrefix = "M:";
            var declarationTypeString = GetXmlDocumenationFormattedString(methodInfo.DeclaringType, false, typeGenericMap, methodGenericMap);
            var memberNameString = methodInfo.Name;
            var methodGenericArgumentsString =
                methodGenericMap.Count > 0 ?
                "``" + methodGenericMap.Count :
                string.Empty;
            var parametersString =
                parameterInfos.Length > 0 ?
                "(" + string.Join(",", methodInfo.GetParameters().Select(x => GetXmlDocumenationFormattedString(x.ParameterType, true, typeGenericMap, methodGenericMap))) + ")" :
                string.Empty;

            var key =
                memberTypePrefix +
                declarationTypeString +
                "." +
                memberNameString +
                methodGenericArgumentsString +
                parametersString;

            if (methodInfo.Name == "op_Implicit" ||
                methodInfo.Name == "op_Explicit")
            {
                key += "~" + GetXmlDocumenationFormattedString(methodInfo.ReturnType, true, typeGenericMap, methodGenericMap);
            }

            _loadedXmlDocumentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a constructor.</summary>
        /// <param name="constructorInfo">The constructor to get the XML documentation of.</param>
        /// <returns>The XML documentation on the constructor.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this ConstructorInfo constructorInfo)
        {
            LoadXmlDocumentation(constructorInfo.DeclaringType.Assembly);

            var typeGenericMap = new Dictionary<string, int>();
            var tempTypeGeneric = 0;
            Array.ForEach(constructorInfo.DeclaringType.GetGenericArguments(), x => typeGenericMap[x.Name] = tempTypeGeneric++);

            // constructors don't support generic types so this will always be empty
            var methodGenericMap = new Dictionary<string, int>();

            var parameterInfos = constructorInfo.GetParameters();

            var memberTypePrefix = "M:";
            var declarationTypeString = GetXmlDocumenationFormattedString(constructorInfo.DeclaringType, false, typeGenericMap, methodGenericMap);
            var memberNameString = "#ctor";
            var parametersString =
                parameterInfos.Length > 0 ?
                "(" + string.Join(",", constructorInfo.GetParameters().Select(x => GetXmlDocumenationFormattedString(x.ParameterType, true, typeGenericMap, methodGenericMap))) + ")" :
                string.Empty;

            var key =
                memberTypePrefix +
                declarationTypeString +
                "." +
                memberNameString +
                parametersString;

            _loadedXmlDocumentation.TryGetValue(key, out var documentation);
            return documentation;
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

        /// <summary>Gets the XML documentation on a property.</summary>
        /// <param name="propertyInfo">The property to get the XML documentation of.</param>
        /// <returns>The XML documentation on the property.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this PropertyInfo propertyInfo)
        {
            LoadXmlDocumentation(propertyInfo.DeclaringType.Assembly);
            var key = "P:" + XmlDocumentationKeyHelper(propertyInfo.DeclaringType.FullName, propertyInfo.Name);
            _loadedXmlDocumentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a field.</summary>
        /// <param name="fieldInfo">The field to get the XML documentation of.</param>
        /// <returns>The XML documentation on the field.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this FieldInfo fieldInfo)
        {
            LoadXmlDocumentation(fieldInfo.DeclaringType.Assembly);
            var key = "F:" + XmlDocumentationKeyHelper(fieldInfo.DeclaringType.FullName, fieldInfo.Name);
            _loadedXmlDocumentation.TryGetValue(key, out var documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on an event.</summary>
        /// <param name="eventInfo">The event to get the XML documentation of.</param>
        /// <returns>The XML documentation on the event.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this EventInfo eventInfo)
        {
            LoadXmlDocumentation(eventInfo.DeclaringType.Assembly);
            var key = "E:" + XmlDocumentationKeyHelper(eventInfo.DeclaringType.FullName, eventInfo.Name);
            _loadedXmlDocumentation.TryGetValue(key, out var documentation);
            return documentation;
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

        /// <summary>Gets the XML documentation on a member.</summary>
        /// <param name="memberInfo">The member to get the XML documentation of.</param>
        /// <returns>The XML documentation on the member.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.GetDocumentation();
            }
            else if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetDocumentation();
            }
            else if (memberInfo is EventInfo eventInfo)
            {
                return eventInfo.GetDocumentation();
            }
            else if (memberInfo is ConstructorInfo constructorInfo)
            {
                return constructorInfo.GetDocumentation();
            }
            else if (memberInfo is MethodInfo methodInfo)
            {
                return methodInfo.GetDocumentation();
            }
            else if (memberInfo is Type type) // + TypeInfo
            {
                return type.GetDocumentation();
            }
            else if (memberInfo.MemberType.HasFlag(MemberTypes.Custom))
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
                                                    + memberInfo.GetType().FullName
                                                    + "]. "
                                                    + "Please submit this issue to the Towel GitHub repository. "
                                                    + "https://github.com/ZacharyPatten/Towel/issues/new/choose");
            }
        }

        /// <summary>Gets the XML documentation for a parameter.</summary>
        /// <param name="parameterInfo">The parameter to get the XML documentation for.</param>
        /// <returns>The XML documenation of the parameter.</returns>
        public static string GetDocumentation(this ParameterInfo parameterInfo)
        {
            var memberDocumentation = parameterInfo.Member.GetDocumentation();
            if (!(memberDocumentation is null))
            {
                var regexPattern =
                    Regex.Escape(@"<param name=" + "\"" + parameterInfo.Name + "\"" + @">") +
                    ".*?" +
                    Regex.Escape(@"</param>");

                var match = Regex.Match(memberDocumentation, regexPattern);
                if (match.Success)
                {
                    return match.Value;
                }
            }
            return null;
        }

        #endregion

        #region MethodInfo

        /// <summary>Determines if a MethodInfo is a local function.</summary>
        /// <param name="methodInfo">The method info to determine if it is a local function.</param>
        /// <returns>True if the MethodInfo is a local function. False if not.</returns>
        public static bool IsLocalFunction(this MethodInfo methodInfo)
        {
            return Regex.Match(methodInfo.Name, @"g__.+\|\d+_\d+").Success;
        }

        #endregion
    }
}
