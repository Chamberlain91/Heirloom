using System;
using System.Runtime.InteropServices;

namespace Heirloom.GLFW3
{
    internal static unsafe partial class Glfw
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct MonitorHandle : IEquatable<MonitorHandle>
        {
            public static readonly MonitorHandle None = new MonitorHandle(IntPtr.Zero);

            public IntPtr Ptr;

            internal MonitorHandle(IntPtr ptr)
            {
                Ptr = ptr;
            }

            public override bool Equals(object obj)
            {
                return obj is MonitorHandle hdl ? Equals(hdl) : false;
            }

            public bool Equals(MonitorHandle obj)
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

            public static bool operator ==(MonitorHandle a, MonitorHandle b)
            {
                return a.Equals(b);
            }

            public static bool operator !=(MonitorHandle a, MonitorHandle b)
            {
                return !a.Equals(b);
            }

            public static implicit operator bool(MonitorHandle obj)
            {
                return obj.Ptr != IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowHandle : IEquatable<WindowHandle>
        {
            public static readonly WindowHandle None = new WindowHandle(IntPtr.Zero);

            public IntPtr Ptr;

            internal WindowHandle(IntPtr ptr)
            {
                Ptr = ptr;
            }

            public override bool Equals(object obj)
            {
                return obj is WindowHandle hdl ? Equals(hdl) : false;
            }

            public bool Equals(WindowHandle obj)
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

            public static bool operator ==(WindowHandle a, WindowHandle b)
            {
                return a.Equals(b);
            }

            public static bool operator !=(WindowHandle a, WindowHandle b)
            {
                return !a.Equals(b);
            }

            public static implicit operator bool(WindowHandle obj)
            {
                return obj.Ptr != IntPtr.Zero;
            }
        }
    }
}
