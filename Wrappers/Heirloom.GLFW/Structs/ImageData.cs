using System;
using System.Runtime.InteropServices;

namespace Heirloom.GLFW
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ImageData
    {
        internal int Width;

        internal int Height;

        internal IntPtr Pixels;
    }
}
