using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Heirloom.GenDoc
{
    internal static class Documentation
    // inspired by: https://github.com/ZacharyPatten/Towel/blob/master/Sources/Towel/Meta.cs 
    {
        private static readonly Dictionary<string, XElement> _documentation = new Dictionary<string, XElement>();
        private static readonly HashSet<Assembly> _assemblies = new HashSet<Assembly>();

        #region Get Documentation

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
            return GetDocumentation($"T:{GetTypeKey(type)}");
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
            else if (member is Type type)
            {
                return GetDocumentation(type);
            }
            else
            {
                // Should not happen, but just being thorough
                var message = $"{nameof(GetDocumentation)} encountered an unhandled type [{member.GetType().FullName}].";
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Gets the XML documentation for the specified method.
        /// </summary>
        private static XElement GetDocumentation(MethodInfo method)
        {
            LoadDocumentation(method.DeclaringType.Assembly);
            return GetDocumentation($"M:{GetMethodKey(method)}");
        }

        /// <summary>
        /// Gets the XML documentation for the specified constructor method.
        /// </summary>
        private static XElement GetDocumentation(ConstructorInfo constructor)
        {
            LoadDocumentation(constructor.DeclaringType.Assembly);
            return GetDocumentation($"M:{GetConstructorKey(constructor)}");
        }

        /// <summary>
        /// Gets the XML documentation for the specified property.
        /// </summary>
        private static XElement GetDocumentation(PropertyInfo property)
        {
            LoadDocumentation(property.DeclaringType.Assembly);
            return GetDocumentation($"P:{GetPropertyKey(property)}");
        }

        /// <summary>
        /// Gets the XML documentation for the specified field.
        /// </summary>
        private static XElement GetDocumentation(FieldInfo field)
        {
            LoadDocumentation(field.DeclaringType.Assembly);
            return GetDocumentation($"F:{GetPropertyKey(field)}");
        }

        /// <summary>
        /// Gets the XML documentation for the specified event.
        /// </summary>
        private static XElement GetDocumentation(EventInfo @event)
        {
            LoadDocumentation(@event.DeclaringType.Assembly);
            return GetDocumentation($"E:{GetPropertyKey(@event)}");
        }

        /// <summary>
        /// Gets the XML documentation for the specified parameter.
        /// </summary>
        public static XElement GetDocumentation(ParameterInfo parameter)
        {
            var member = GetDocumentation(parameter.Member);

            if (member != null)
            {
                return member.Descendants("param")
                             .Where(e => e.Attribute("name").Value == parameter.Name)
                             .FirstOrDefault();
            }

            // No known documentation for parent member
            return null;
        }

        #endregion

        #region Reflection Type to XML Key

        public static string GetTypeKey(Type type)
        {
            return GetTypeKey(type, false);
        }

        public static string GetConstructorKey(ConstructorInfo constructor)
        {
            return GetMethodBaseKey(constructor);
        }

        public static string GetMethodKey(MethodInfo method)
        {
            return GetMethodBaseKey(method);
        }

        public static string GetPropertyKey(FieldInfo field)
        {
            return GetMemberKey(field);
        }

        public static string GetPropertyKey(PropertyInfo property)
        {
            return GetMemberKey(property);
        }

        public static string GetPropertyKey(EventInfo @event)
        {
            return GetMemberKey(@event);
        }

        private static string GetMemberKey(MemberInfo member)
        {
            return $"{GetTypeKey(member.DeclaringType)}.{member.Name}";
        }

        private static string GetMethodBaseKey(MethodBase method)
        {
            var generic = "";

            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments();
                generic += "``" + genericArguments.Length;
            }

            // 
            var parameters = string.Join(',', method.GetParameters().Select(p => GetTypeKey(p.ParameterType, true)));
            if (!string.IsNullOrWhiteSpace(parameters)) { parameters = $"({parameters})"; }

            // 
            var name = method.Name;
            if (method is ConstructorInfo) { name = "#ctor"; }

            // 
            return $"{GetTypeKey(method.DeclaringType)}.{name}{generic}{parameters}";
        }

        private static string GetTypeKey(Type type, bool isParameterType)
        {
            // array, pointer or reference
            if (type.HasElementType)
            {
                // 
                if (type.IsByRef)
                {
                    // 
                    type = type.GetElementType();
                    return $"{GetTypeKey(type, isParameterType)}@";
                }
                else
                {
                    // 
                    type = type.GetElementType();
                    return GetTypeKey(type, isParameterType);
                }
            }
            else
            if (type.IsGenericType)
            {
                // Store by direct type string
                var key = type.ToString();

                var ixGrave = key.IndexOf('`');
                if (isParameterType)
                {
                    // Trim generic meta data
                    key = key.Substring(0, ixGrave);
                    key += "{" + string.Join(',', type.GenericTypeArguments.Select(t => GetTypeKey(t))) + "}";
                    return key;
                }
                else
                {
                    // Trim generic meta name data
                    var ixBrace = key.IndexOf('[', ixGrave);
                    return key.Substring(0, ixBrace);
                }
            }
            else
            if (type.IsGenericMethodParameter)
            {
                return $"``{type.GenericParameterPosition}";
            }
            else
            if (type.IsGenericTypeParameter)
            {
                return $"`{type.GenericParameterPosition}";
            }
            else
            {
                // Simplest Case...?
                return type.FullName;
            }
        }

        #endregion

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
                var xml = XDocument.Parse(File.ReadAllText(xmlPath));

                // Get child of the members node
                foreach (var member in xml.Descendants("member"))
                {
                    var name = member.Attribute("name").Value;
                    _documentation[name] = member;
                }
            }

            // Mark assembly as visited
            _assemblies.Add(assembly);
        }
    }
}
