using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Heirloom.GenDoc
{
    public class MarkdownGenerator : DocumentationGenerator
    {
        public MarkdownGenerator()
            : base("md")
        { }

        protected override string GenerateTypeSummary()
        {
            // Emit header
            var markdown = Header($"{GetName(CurrentType)} ({GetTypeAccess(CurrentType)})", 2);

            // Emit
            markdown += Small($"**Namespace**: {CurrentType.Namespace}</sub>") + "  \n";
            var inherits = WalkTypeInheritance(CurrentType).Select(t => Link(t));
            if (inherits.Any()) { markdown += Small($"**Inherits**: {string.Join(", ", inherits)}") + "  \n"; }
            var interfaces = CurrentType.GetInterfaces().Select(t => Link(t));
            if (interfaces.Any()) { markdown += Small($"**Interfaces**: {string.Join(", ", interfaces)}") + "  \n"; }

            // Emit Summary
            var summary = GetSummary(CurrentType);
            if (summary.Length > 0)
            {
                markdown += $"\n{summary}\n";
            }

            // Generate Remarks
            var remarks = GetRemarks(CurrentType);
            if (remarks?.Length > 0)
            {
                markdown += $"\n{remarks}\n";
            }

            markdown += "\n";
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
                foreach (var @event in Events)
                {
                    markdown += $"#### {GetName(@event)}\n";

                    // Generate Summary
                    var summary = GetSummary(@event);
                    if (summary?.Length > 0)
                    {
                        markdown += $"\n{summary}\n";
                    }

                    // Generate Remarks
                    var remarks = GetRemarks(@event);
                    if (remarks?.Length > 0)
                    {
                        markdown += $"\n{remarks}\n";
                    }
                }
            }

            // Emit Methods
            if (Methods.Count > 0)
            {
                markdown += $"### Methods\n\n";
                foreach (var method in InstanceMethods)
                {
                    markdown += GenerateSummary(method, false);
                }

                foreach (var methodInfo in StaticMethods)
                {
                    markdown += GenerateSummary(methodInfo, true);
                }
            }

            return markdown;
        }

        private static string GenerateTable(string headerLeft, string headerRight, IEnumerable<(string left, string right)> rows)
        {
            if (rows.Any())
            {
                int lSize = headerLeft.Length, rSize = headerRight.Length;

                // Measure Rows
                foreach (var (left, right) in rows)
                {
                    lSize = Math.Max(lSize, left.Length);
                    rSize = Math.Max(rSize, right.Length);
                }

                var markdown = "";
                markdown += $"| {Str(headerLeft, lSize)} | {Str(headerRight, rSize)} |\n";
                markdown += $"|{Rep('-', lSize + 2)}|{Rep('-', rSize + 2)}|\n";

                foreach (var (left, right) in rows)
                {
                    markdown += $"| {Str(left, lSize)} | {Str(right, rSize)} |\n";
                }

                markdown += "\n";
                return markdown;
            }
            else
            {
                return string.Empty;
            }

            static string Rep(char chr, int num)
            {
                return new string(chr, num);
            }

            static string Str(string txt, int num)
            {
                return txt + Rep(' ', num - txt.Length);
            }
        }

        protected override string GenerateMembersTable()
        {
            var markdown = "";

            //
            if (Fields.Count > 0)
            {
                var rows = Fields.Select(m => (AnchorLink(GetName(m)), NormalizeSpaces(GetSummary(m))));
                markdown += GenerateTable("Fields", "Summary", rows);
            }

            //
            if (Properties.Count > 0)
            {
                var rows = Properties.Select(m => (AnchorLink(GetName(m)), NormalizeSpaces(GetSummary(m))));
                markdown += GenerateTable("Properties", "Summary", rows);
            }

            //
            if (Events.Count > 0)
            {
                var rows = Events.Select(m => (AnchorLink(GetName(m)), NormalizeSpaces(GetSummary(m))));
                markdown += GenerateTable("Events", "Summary", rows);
            }

            //
            if (Methods.Count > 0)
            {
                var rows = Methods.Select(m => (AnchorLink(GetName(m)), NormalizeSpaces(GetSummary(m))));
                markdown += GenerateTable("Methods", "Summary", rows);
            }

            return markdown;
        }

        protected override string GenerateSeparator()
        {
            return "--------------------------------------------------------------------------------\n\n";
        }

        private string GenerateSummary(ConstructorInfo constructor)
        {
            var markdown = Header($"{GetName(constructor)}{GetParameterSignature(constructor)}", 4);

            // Generate Badges
            markdown += GenerateBadges(constructor);

            // Generate Summary
            var summary = GetSummary(constructor);
            if (summary?.Length > 0)
            {
                markdown += $"\n{summary}\n";
            }

            // Generate Remarks
            var remarks = GetRemarks(constructor);
            if (remarks?.Length > 0)
            {
                markdown += $"\n{remarks}\n";
            }

            markdown += "\n";
            return markdown;
        }

        private string GenerateSummary(FieldInfo field, bool isStatic)
        {
            var markdown = Header($"{Anchor(GetName(field))} : {Link(field.FieldType)}", 4);

            // Generate Badges
            markdown += GenerateBadges(field, isStatic);

            // Generate Summary
            var summary = GetSummary(field);
            if (summary?.Length > 0)
            {
                markdown += $"\n{summary}\n";
            }

            // Generate Remarks
            var remarks = GetRemarks(field);
            if (remarks?.Length > 0)
            {
                markdown += $"\n{remarks}\n";
            }

            markdown += "\n";
            return markdown;
        }

        private string GenerateSummary(PropertyInfo property, bool isStatic)
        {
            // Emit Name
            var markdown = Header($"{Anchor(GetName(property))} : {Link(property.PropertyType)}\n", 4);

            // Generate Badges
            markdown += GenerateBadges(property, isStatic);

            // Generate Summary
            var summary = GetSummary(property);
            if (summary?.Length > 0)
            {
                markdown += $"\n{summary}\n";
            }

            // Generate Remarks
            var remarks = GetRemarks(property);
            if (remarks?.Length > 0)
            {
                markdown += $"\n{remarks}\n";
            }

            markdown += "\n";
            return markdown;
        }

        private string GenerateSummary(MethodInfo method, bool isStatic)
        {
            var markdown = Header(Anchor(GetSignature(method)), 4);

            // Generate Badges
            markdown += GenerateBadges(method, isStatic);

            // Generate Summary
            var summary = GetSummary(method);
            if (summary?.Length > 0)
            {
                markdown += $"\n{summary}\n";
            }

            var parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                markdown += "\n";

                foreach (var param in parameters)
                {
                    var paramSummary = Documentation.GetDocumentation(param);
                    if (paramSummary != null)
                    {
                        var text = $"{Bold(GetName(param))}: {paramSummary}";
                        markdown += $"{Small(text)}  \n";
                    }
                }
            }

            // Generate Remarks
            var remarks = GetRemarks(method);
            if (remarks?.Length > 0)
            {
                markdown += $"\n{remarks}\n";
            }

            markdown += "\n";
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

            return $"<a name=\"{link}\"></a> {text}";
        }

        private static string GetLinkIdentifier(string text)
        {
            var hash = GetDeterministicHashCode(text).ToString("X").Substring(0, 4);
            var key = text[0..Math.Min(text.Length, 4)].ToUpper();
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

        protected override string Link(string text, string target)
        {
            return $"[{text}]({target})";
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

        protected override string Bold(string text)
        {
            return $"**{text}**";
        }

        protected override string Small(string text)
        {
            return $"<small>{text}</small>";
        }

        protected override string EscapeCharacters(string text)
        {
            text = text?.Replace("\\<", "<");
            return text?.Replace("<", "\\<");
        }
    }
}
