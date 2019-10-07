namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides an ability to parse text with some sort of markup into <see cref="RichText"/>.
    /// </summary>
    public abstract class RichTextParser
    {
        /// <summary>
        /// Parse the input text.
        /// </summary>
        public abstract RichText Parse(string input);
    }
}
