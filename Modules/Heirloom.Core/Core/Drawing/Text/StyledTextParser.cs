namespace Heirloom
{
    /// <summary>
    /// Provides an ability to parse text with some sort of markup into <see cref="StyledText"/>.
    /// </summary>
    /// <category>Text</category>
    public abstract class StyledTextParser
    {
        /// <summary>
        /// Parse the input text.
        /// </summary>
        public abstract StyledText Parse(string input);
    }
}
