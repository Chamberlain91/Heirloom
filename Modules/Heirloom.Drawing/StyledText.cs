namespace Heirloom.Drawing
{
    /// <summary>
    /// Styled text compiled by a <see cref="StyledTextParser"/>.
    /// </summary>
    public abstract class StyledText
    {
        /// <summary>
        /// Gets the plain text component of this <see cref="StyledText"/>.
        /// </summary>
        public abstract string Text { get; }

        /// <summary>
        /// Gets the text styling callback.
        /// </summary>
        protected internal abstract DrawTextCallback Callback { get; }
    }
}
