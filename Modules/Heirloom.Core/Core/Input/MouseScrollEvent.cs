namespace Heirloom
{
    /// <summary>
    /// Contains the data of an event when the mouse wheel has been scrolled on some input source.
    /// </summary>
    public readonly struct MouseScrollEvent
    {
        /// <summary>
        /// The amount the mouse wheel was scrolled when the event was generated.
        /// </summary>
        public readonly Vector Scroll;

        /// <summary>
        /// Constructs a new instanec of <see cref="MouseScrollEvent"/>.
        /// </summary>
        /// <param name="x">The amount the mouse was scrolled horizontally.</param>
        /// <param name="y">The amount the mouse was scrolled vertically.</param>
        public MouseScrollEvent(float x, float y)
        {
            Scroll = new Vector(x, y);
        }
    }
}
