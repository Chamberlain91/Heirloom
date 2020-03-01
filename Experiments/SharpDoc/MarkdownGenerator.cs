using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Heirloom.IO;

namespace SharpDoc
{
    public class MarkdownGenerator : DocumentationGenerator
    {
        public MarkdownGenerator()
            : base("md")
        { }

        protected override string GenerateTypeSummary()
        {
            // Emit header
            var markdown = $"{CurrentType.Assembly.GetName().Name} - {GetName(CurrentType)} ({GetTypeAccess(CurrentType)})\n";
            markdown += $"{new string('-', markdown.Length - 2)}\n\n";

            // Emit Summary
            var typeSummary = EscapeCharacters(CurrentType.GetDocumentation());
            if (typeSummary?.Length > 0)
            {
                markdown += $"{typeSummary}\n\n";
            }

            // Emit
            markdown += Small($"**Namespace**: {CurrentType.Namespace}</sub>") + "  \n";
            var inherits = WalkInheritedTypes(CurrentType).Select(t => GetLink(t));
            if (inherits.Any()) { markdown += Small($"**Inherits**: {string.Join(", ", inherits)}") + "  \n"; }
            var interfaces = CurrentType.GetInterfaces().Select(t => GetName(t));
            if (interfaces.Any()) { markdown += Small($"**Interfaces**: {string.Join(", ", interfaces)}") + "  \n"; }

            return markdown;
        }

        protected override string GenerateEnumBody()
        {
            var markdown = "";

            markdown += Header("Values\n", 3);
            foreach (var name in CurrentType.GetEnumNames())
            {
                // Emit Enum Field
                markdown += Header(name, 4);
                markdown += Documentation.GetDocumentation($"F:{CurrentType.FullName}.{name}");
                markdown += "\n\n";
            }

            return markdown;
        }

        protected override string GenerateObjectBody()
        {
            var markdown = "";

            // Emit Fields
            if (Fields.Count > 0)
            {
                markdown += Header("Fields\n", 3);
                foreach (var fieldInfo in Fields)
                {
                    markdown += GenerateSummary(fieldInfo, false);
                }

                foreach (var fieldInfo in StaticFields)
                {
                    markdown += GenerateSummary(fieldInfo, true);
                }
            }

            // Emit Constructors
            if (Constructors.Count > 0)
            {
                markdown += Header("Constructors\n", 3);
                foreach (var constructor in Constructors)
                {
                    markdown += GenerateSummary(constructor);
                }
            }

            // Emit Properties
            if (Properties.Count > 0)
            {
                markdown += $"### Properties\n\n";

                foreach (var prop in InstanceProperties)
                {
                    markdown += GenerateSummary(prop, false);
                }

                foreach (var prop in StaticProperties)
                {
                    markdown += GenerateSummary(prop, true);
                }
            }

            // Emit Events
            if (Events.Count > 0)
            {
                markdown += $"### Events\n\n";
                foreach (var eventInfo in Events)
                {
                    markdown += $"#### {GetName(eventInfo)}\n";

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
                foreach (var methodInfo in InstanceMethods)
                {
                    markdown += GenerateSummary(methodInfo, false);
                }

                foreach (var methodInfo in StaticMethods)
                {
                    markdown += GenerateSummary(methodInfo, true);
                }
            }

            return markdown;
        }

        protected override string GenerateMembersTable()
        {
            var markdown = "";

            //
            if (Fields.Count > 0)
            {
                markdown += "| Fields | Summary |\n";
                markdown += "|-------|---------|\n";
                foreach (var m in Fields) { markdown += $"| {AnchorLink(GetName(m))} | {Shorten(GetSummary(m))} |\n"; }
                markdown += "\n";
            }

            if (Properties.Count > 0)
            {
                markdown += "| Properties | Summary |\n";
                markdown += "|------------|---------|\n";
                foreach (var m in Properties) { markdown += $"| {AnchorLink(GetName(m))} | {Shorten(GetSummary(m))} |\n"; }
                markdown += "\n";
            }

            if (Events.Count > 0)
            {
                markdown += "| Events | Summary |\n";
                markdown += "|--------|---------|\n";
                foreach (var m in Events) { markdown += $"| {AnchorLink(GetName(m))} | {Shorten(GetSummary(m))} |\n"; }
                markdown += "\n";
            }

            if (Methods.Count > 0)
            {
                markdown += "| Methods | Summary |\n";
                markdown += "|---------|---------|\n";
                foreach (var m in Methods) { markdown += $"| {AnchorLink(GetName(m), GetSignature(m))} | {Shorten(GetSummary(m))} |\n"; }
                markdown += "\n";
            }

            return markdown;
        }

        protected override string GenerateSeparator()
        {
            return "\n--------------------------------------------------------------------------------\n\n";
        }

        private string GenerateSummary(ConstructorInfo constructor)
        {
            var markdown = Header($"{GetName(constructor)}{GetParameterSignature(constructor)}", 4);

            // Generate Badges
            markdown += GenerateBadges(constructor);
            markdown += "\n";

            // Generate Summary
            var summary = GetSummary(constructor);
            if (summary?.Length > 0)
            {
                markdown += $"{summary}\n\n";
            }

            return markdown;
        }

        private string GenerateSummary(FieldInfo field, bool isStatic)
        {
            var markdown = Header($"{GetName(field)} : {GetLink(field.FieldType)}", 4);

            // Generate Badges
            markdown += GenerateBadges(field, isStatic);
            markdown += "\n";

            // Generate Summary
            var summary = GetSummary(field);
            if (summary?.Length > 0)
            {
                markdown += $"{summary}\n\n";
            }

            return markdown;
        }

        private string GenerateSummary(PropertyInfo property, bool isStatic)
        {
            // Emit Name
            var markdown = Header($"{Anchor(GetName(property))} : {GetLink(property.PropertyType)}\n", 4);

            // Generate Badges
            markdown += GenerateBadges(property, isStatic);
            markdown += "\n";

            // 
            var summary = GetSummary(property);
            if (summary?.Length > 0)
            {
                markdown += $"{summary}\n\n";
            }

            return markdown + "\n";
        }

        private string GenerateSummary(MethodInfo method, bool isStatic)
        {
            var markdown = Header(Anchor(GetSignature(method)), 4);
            markdown += "\n";

            // Generate Badges
            markdown += GenerateBadges(method, isStatic);
            markdown += "\n";

            // Generate Summary
            var summary = GetSummary(method);
            if (summary?.Length > 0)
            {
                markdown += $"{summary}\n";
            }

            var parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                markdown += "\n";

                foreach (var param in parameters)
                {
                    var paramSummary = param.GetDocumentation();
                    if (paramSummary != null)
                    {
                        var text = $"**{GetName(param)}**: {paramSummary}  \n";
                        markdown += $"{Small(text)}\n";
                    }
                }
            }

            return markdown;
        }

        #region Badges

        private string GenerateBadges(ConstructorInfo constructor)
        {
            var badges = new List<string>();
            badges.AddRange(GetMemberBadges(constructor));
            return GetBadgeText(badges);
        }

        private string GenerateBadges(PropertyInfo property, bool isStatic)
        {
            var badges = new List<string>();

            // Emit Static Badge
            if (isStatic) { badges.Add("Static"); }

            // Emit Get/Set Badges
            var canRead = property.CanRead && (property.GetMethod.IsPublic || property.GetMethod.IsFamily);
            var canWrite = property.CanWrite && (property.SetMethod.IsPublic || property.SetMethod.IsFamily);
            if (!canRead || !canWrite)
            {
                if (canRead) { badges.Add("Read Only"); }
                if (canWrite) { badges.Add("Write Only"); }
            }

            // 
            badges.AddRange(GetMemberBadges(property));
            return GetBadgeText(badges);
        }

        private string GenerateBadges(FieldInfo field, bool isStatic)
        {
            var badges = new List<string>();

            // Emit Static Badge
            if (isStatic) { badges.Add("Static"); }

            // Emit Real Only
            if (field.IsInitOnly) { badges.Add("Read Only"); }

            // 
            badges.AddRange(GetMemberBadges(field));
            return GetBadgeText(badges);
        }

        private string GenerateBadges(MethodInfo method, bool isStatic)
        {
            var badges = new List<string>();

            // Emit Static Badge
            if (isStatic) { badges.Add("Static"); }

            // 
            if (method.IsAbstract) { badges.Add("Abstract"); }
            else if (method.IsVirtual) { badges.Add("Virtual"); }
            if (method.IsFamily) { badges.Add("Protected"); }

            // 
            badges.AddRange(GetMemberBadges(method));
            return GetBadgeText(badges);
        }

        private IEnumerable<string> GetMemberBadges(MemberInfo info)
        {
            return info.GetCustomAttributes(true)
                       .Select(s => GetName(s.GetType()));
        }

        private string GetBadgeText(IEnumerable<string> tokens)
        {
            return tokens.Any()
                ? $"<small>{string.Join(", ", tokens.Select(s => Badge(s))).Trim()}</small>\n"
                : string.Empty;
        }

        #endregion

        #region Anchor/Link

        private static string AnchorLink(string text, string target = null)
        {
            var link = target == null
                ? GetLinkIdentifier(text)
                : GetLinkIdentifier(target);

            return $"[{text}](#{link})";
        }

        private static string Anchor(string text, string target = null)
        {
            var link = target == null
                ? GetLinkIdentifier(text)
                : GetLinkIdentifier(target);

            return $"<a name=\"{link}\"></a>{text}";
        }

        private static string GetLinkIdentifier(string text)
        {
            var hash = GetDeterministicHashCode(text).ToString("X");
            var key = text[0..Math.Min(text.Length, 3)].ToUpper();
            return key + hash;
        }

        private static int GetDeterministicHashCode(string str)
        // https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/
        {
            unchecked
            {
                var hash1 = (5381 << 16) + 5381;
                var hash2 = hash1;

                for (var i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                    {
                        break;
                    }
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        #endregion

        protected override string GenerateLink(Type type)
        {
            return $"[{GetName(type)}]({GetPath(GetSimpleType(type))})";
        }

        protected override string Header(string text, int level)
        {
            if (level < 1) { level = 1; }
            var pre = new string('#', level);
            return $"{pre} {text}\n";
        }

        protected override string CodeBlock(string text)
        {
            return $"```\n{text}\n```";
        }

        protected override string Code(string text)
        {
            return $"`{text}`";
        }

        protected override string Badge(string text)
        {
            return $"`{text}`";
        }

        protected override string Small(string text)
        {
            return $"<small>{text}</small>";
        }

        protected override string EscapeCharacters(string text)
        {
            return text?.Replace("<", "\\<");
        }
    }
}
