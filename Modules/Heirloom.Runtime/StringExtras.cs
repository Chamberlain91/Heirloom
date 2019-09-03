namespace Heirloom.Runtime
{
    public static class StringExtras
    {
        /// <summary>
        /// Shortens a string by removing the center portyion and replacing with "..." dependant on the given max length.
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
        /// Gets a human readable format for the time specified in seconds.
        /// </summary>
        public static string GetHumanTime(float time)
        {
            if (time > 1F)
            {
                return $"{time.ToString("0.00")} seconds";
            }
            else if (time > 1e-3F)
            {
                return $"{(time * 1000).ToString("0.00")} miliseconds";
            }
            else if (time > 1e-6F)
            {
                return $"{(time * 1000).ToString("0.00")} microseconds";
            }
            else if (time > 1e-9F)
            {
                return $"{time.ToString("0.00")} nanoseconds";
            }
            else
            {
                return $"0.00";
            }
        }

        /// <summary>
        /// Like Unity's NicifyVariableName.
        /// </summary>
        public static string GetSmartDisplayName(string key)
        {
            // Strip 
            key = key.Trim();

            // Nothing to process
            if (key.Length == 0)
            {
                return string.Empty;
            }

            var letter = char.IsLetter(key[0]);
            var name = $"{char.ToUpper(key[0])}";

            for (var i = 1; i < key.Length; i++)
            {
                var c = key[i];

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
