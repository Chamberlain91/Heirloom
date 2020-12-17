using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using Meadows.IO;

using Meadows.Text;

namespace Meadows
{
    /// <summary>
    /// Provides extension methods various types within Heirloom.
    /// </summary>
    public static partial class Extensions
    {
        private static readonly Regex _regexNumberedLabel = new Regex(@"[A-Z]+\d+", RegexOptions.Compiled);

        /// <summary>
        /// Determines of this string matches the glob-like pattern.
        /// </summary>
        /// <param name="str">Some string to check against the pattern.</param>
        /// <param name="pattern">A glob-like pattern.</param>
        /// <returns>True, if the string matches the pattern.</returns>
        /// <remarks>
        /// This implementation only supports basic wildcards '*' and '?'.
        /// It does not current support '**' or 'one of many' groups.
        /// </remarks>
        public static bool IsLikePattern(this string str, string pattern)
        {
            if (str == null) { throw new ArgumentException($"'{nameof(str)}' cannot be null.", nameof(str)); }
            if (string.IsNullOrEmpty(pattern)) { throw new ArgumentException($"'{nameof(pattern)}' cannot be null or empty.", nameof(pattern)); }

            var globPattern = "^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
            return new Regex(globPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(str);
        }

        /// <summary>
        /// Shortens a string by removing the center portion and replacing with "..." dependant on the given max length.
        /// This ensures the shortened string has maxLength or less characters.
        /// </summary>
        /// <param name="this">Some string to shorten.</param>
        /// <param name="maxLength">The maximum string length desired.</param>
        /// <returns></returns>
        public static string Shorten(this string @this, int maxLength = 15)
        {
            var segLength = (maxLength - 3) / 2;
            if (@this.Length >= maxLength) { return @this.Substring(0, segLength) + "..." + @this.Substring(@this.Length - segLength, segLength); }
            else { return @this; }
        }

        /// <summary>
        /// Gets the ith unicode character of this string.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCharacter GetCharacter(this string text, int i)
        {
            return (UnicodeCharacter) char.ConvertToUtf32(text, i);
        }

        /// <summary>
        /// Transforms a variable name like string into sname case (ie, "myExampleString" into "my_example_string").
        /// </summary>
        public static string ToSnakeCase(this string @this)
        {
            return @this.ToSmartDisplayName()
                        .ToLowerInvariant()
                        .Replace(' ', '_');
        }

        /// <summary>
        /// Transforms a variable name like string into sname case (ie, "myExampleString" into "MY_EXAMPLE_STRING").
        /// </summary>
        public static string ToShoutingCase(this string @this)
        {
            return @this.ToSmartDisplayName()
                        .ToUpperInvariant()
                        .Replace(' ', '_');
        }

        /// <summary>
        /// Converts this string into a standardized string identifier.
        /// </summary>
        /// <remarks>
        /// Converts all slashes to dots, spaces and non-alphanumeric to single underscores and transforms to lowercase.
        /// </remarks>
        public static string ToIdentifier(this string path)
        {
            path = path.Replace('/', '.');
            path = path.Replace('\\', '.');

            // Should I be doing this?
            path = Regex.Replace(path, @"\s+", "_");
            path = Regex.Replace(path, @"[^\w\d\.]+", "_");
            path = Regex.Replace(path, "_+", "_");

            return path.ToLowerInvariant().Trim('_');
        }

        /// <summary>
        /// Transform a variable name like string to an improved display string (akin to Unity's NicifyVariableName). <para/>
        /// Ie, "myExampleString" becomes "My Example String"
        /// </summary>
        public static string ToSmartDisplayName(this string @this)
        {
            // Strip 
            @this = @this.Trim();

            // todo: trim 'm_' prefix
            // todo: handle spaces/underscores?

            // Nothing to process
            if (@this.Length == 0)
            {
                return string.Empty;
            }

            // ie, Will match "F3" "AREA51" but not "Area51"
            if (_regexNumberedLabel.IsMatch(@this))
            {
                return @this.ToUpper();
            }

            var letter = char.IsLetter(@this[0]);
            var name = $"{char.ToUpper(@this[0])}";

            for (var i = 1; i < @this.Length; i++)
            {
                var c = @this[i];

                // Change of case
                if (char.IsUpper(c) || char.IsDigit(c) && letter)
                {
                    letter = false;

                    name += ' ';
                    name += char.ToUpper(c);
                }
                else
                {
                    name += char.ToLower(c);
                }

                // 
                if (char.IsLetter(c)) { letter = true; }
            }

            return name;
        }
    }
}
