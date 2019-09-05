using System;
using System.Runtime.InteropServices;

namespace Heirloom.GLFW3
{
    public static unsafe partial class Glfw
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct MonitorHandle : IEquatable<MonitorHandle>
        {
            public static readonly MonitorHandle None = new MonitorHandle(IntPtr.Zero);

            /// <summary>
            /// Pointer to an internal GLFWmonitor.
            /// </summary>
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
    }
}
