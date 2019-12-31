using System;
using System.Runtime.InteropServices;

namespace Heirloom.Desktop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ImageData
    {
        internal int Width;

        internal int Height;

        internal IntPtr Pixels;
    }
}
