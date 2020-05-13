namespace Heirloom
{
    /// <summary>
    /// Contains the data of an event when a mouse button has been pressed or released on some input source.
    /// </summary>
    /// <category>User Input</category>
    public readonly struct MouseButtonEvent
    {
        /// <summary>
        /// The mouse button associated with the event.
        /// </summary>
        public readonly MouseButton Button;

        /// <summary>
        /// The modifier keys pressed when the event was generated.
        /// </summary>
        public readonly KeyModifiers Modifiers;

        /// <summary>
        /// The state of the mouse button when the event was generated.
        /// </summary>
        public readonly ButtonState State;

        /// <summary>
        /// The position of the mouse when the event was generated.
        /// </summary>
        public readonly Vector Position;

        /// <summary>
        /// Constructs a new instance of <see cref="MouseButtonEvent"/>.
        /// </summary>
        /// <param name="button">The mouse button associated with this event.</param>
        /// <param name="modifiers">The associated key modifiers.</param>
        /// <param name="state">The state of the mouse button.</param>
        /// <param name="position">The position of the mouse.</param>
        public MouseButtonEvent(MouseButton button, KeyModifiers modifiers, ButtonState state, Vector position)
        {
            Button = button;
            Modifiers = modifiers;
            State = state;
            Position = position;
        }
    }
}
