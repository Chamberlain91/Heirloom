using System;
using System.Runtime.InteropServices;

namespace Heirloom.Platforms.Desktop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CursorHandle : IEquatable<CursorHandle>
    {
        public static readonly CursorHandle None = new CursorHandle(IntPtr.Zero);

        public IntPtr Ptr;

        internal CursorHandle(IntPtr ptr)
        {
            Ptr = ptr;
        }

        public override bool Equals(object obj)
        {
            return obj is CursorHandle cur ? Equals(cur) : false;
        }

        public bool Equals(CursorHandle obj)
        {
            return Ptr == obj.Ptr;
        }

        public override string ToString()
        {
            return Ptr.ToString();
        }

        public override int GetHashCode()
        {
            return Ptr.GetHashCode();
        }

        public static bool operator ==(CursorHandle a, CursorHandle b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(CursorHandle a, CursorHandle b)
        {
            return !a.Equals(b);
        }
    }
}
