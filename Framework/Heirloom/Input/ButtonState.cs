using System;

namespace Heirloom
{
    /// <summary>
    /// Represents the state of a button.
    /// </summary>
    /// <category>User Input</category>
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
        Recent = 1 << 2,

        /// <summary>
        /// Button state is now repeating, this is dependant on the OS.
        /// </summary>
        Repeat = 1 << 3,

        /// <summary>
        /// Button was pressed this frame.
        /// </summary>
        Pressed = Down | Recent,

        /// <summary>
        /// Button was pressed this frame.
        /// </summary>
        Released = Up | Recent
    }
}
