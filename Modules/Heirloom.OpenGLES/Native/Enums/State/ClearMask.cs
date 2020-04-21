using System;

namespace Heirloom.OpenGLES
{
    [Flags]
    internal enum ClearMask : uint
    {
        /// <summary>
        /// Mask to flag clearing the depth buffer.
        /// </summary>
        Depth = 0x00000100,

        /// <summary>
        /// Mask to flag clearing the stencil buffer.
        /// </summary>
        Stencil = 0x00000400,

        /// <summary>
        /// Mask to flag clearing the color buffer.
        /// </summary>
        Color = 0x00004000,
    }
}
