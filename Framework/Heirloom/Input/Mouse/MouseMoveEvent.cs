using Heirloom.Mathematics;

namespace Heirloom
{
    /// <summary>
    /// Contains the data of an event when the mouse has been moved on some input source.
    /// </summary>
    /// <category>User Input</category>
    public readonly struct MouseMoveEvent
    {
        /// <summary>
        /// The position of the mouse when the event was generated.
        /// </summary>
        public readonly Vector Position;

        /// <summary>
        /// The difference in position of the mouse when the event was generated.
        /// </summary>
        public readonly Vector Delta;

        /// <summary>
        /// Constructs a new instance of <see cref="MouseMoveEvent"/>.
        /// </summary>
        /// <param name="position">The position of the mouse.</param>
        /// <param name="delta">The delta position of the mouse.</param>
        public MouseMoveEvent(Vector position, Vector delta)
        {
            Position = position;
            Delta = delta;
        }
    }
}
