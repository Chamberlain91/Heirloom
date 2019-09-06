using System;
using System.Runtime.InteropServices;

namespace Heirloom.GLFW3
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ImageData
    {
        internal int Width;

        internal int Height;

        internal IntPtr Pixels;
    }
}
