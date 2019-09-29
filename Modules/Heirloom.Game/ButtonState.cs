using System;

namespace Heirloom.Game
{
    [Flags]
    public enum ButtonState
    {
        /// <summary>
        /// Button is considered to be not held.
        /// </summary>
        Up = 0,

        /// <summary>
        /// Button is considered to be held.
        /// </summary>
        Down = 1 << 0,

        /// <summary>
        /// Button state was changed this frame.
        /// </summary>
        Now = 1 << 1,

        /// <summary>
        /// Button state was changed to <see cref="Up"/> state this frame.
        /// </summary>
        Released = Up | Now,

        /// <summary>
        /// Button state was changed to <see cref="Down"/> state this frame.
        /// </summary>
        Pressed = Down | Now
    }
}
