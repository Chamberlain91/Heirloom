using System;

namespace Heirloom
{
    /// <summary>
    /// Flags that represent the modifier keys pressed or toggled when an associated input event occured.
    /// </summary>
    /// <category>User Input</category>
    [Flags]
    public enum KeyModifiers
    {
        /// <summary>
        /// A shift key was pressed.
        /// </summary>
        Shift = 1 << 0,

        /// <summary>
        /// A control key was pressed.
        /// </summary>
        Control = 1 << 2,

        /// <summary>
        /// An alt key was pressed.
        /// </summary>
        Alt = 1 << 3,

        /// <summary>
        /// The OS 'super' key was pressed.
        /// </summary>
        Super = 1 << 4,

        /// <summary>
        /// The caps lock key was toggled on.
        /// </summary>
        CapsLock = 1 << 5,

        /// <summary>
        /// The number lock key was toggled on.
        /// </summary>
        NumLock = 1 << 6
    }
}
