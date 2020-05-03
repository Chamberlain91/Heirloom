using System;

namespace Heirloom
{
    /// <summary>
    /// Represents the state of a button.
    /// </summary>
    [Flags]
    public enum ButtonState
    {
        /// <summary>
        /// Button is released.
        /// </summary>
        Up = 1 << 0,

        /// <summary>
        /// Button is held.
        /// </summary>
        Down = 1 << 1,

        /// <summary>
        /// Button state changed this frame.
        /// </summary>
        Now = 1 << 2,

        /// <summary>
        /// Button was pressed this frame.
        /// </summary>
        Pressed = Down | Now,

        /// <summary>
        /// Button was pressed this frame.
        /// </summary>
        Released = Up | Now
    }
}
