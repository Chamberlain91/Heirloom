namespace Heirloom.Extras
{
    public static class StringExtensions
    {
        /// <summary>
        /// Shortens a string by removing the center portion and replacing with "..." dependant on the given max length.
        /// This ensures the shortened string has maxLength or less characters.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Shorten(this string @this, int maxLength = 15)
        {
            var segLength = (maxLength - 3) / 2;
            if (@this.Length >= maxLength) { return @this.Substring(0, segLength) + "..." + @this.Substring(@this.Length - segLength, segLength); }
            else { return @this; }
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
