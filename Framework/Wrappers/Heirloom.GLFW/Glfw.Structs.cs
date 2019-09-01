using System;
using System.Collections.Generic;

namespace Heirloom.GLFW
{
    public static unsafe partial class Glfw
    {
        public struct Window : IEquatable<Window>
        {
            public static readonly Window None = new Window(IntPtr.Zero);

            public IntPtr Address;

            internal Window(IntPtr pointer)
            {
                Address = pointer;
            }

            public override bool Equals(object obj)
            {
                if (obj is Window win) { return Equals(this, win); }
                else { return false; }
            }

            public bool Equals(Window other)
            {
                return other.Address == Address;
            }

            public override int GetHashCode()
            {
                return -638417062 + EqualityComparer<IntPtr>.Default.GetHashCode(Address);
            }

            public static bool operator ==(Window left, Window right) { return left.Equals(right); }
            public static bool operator !=(Window left, Window right) { return !(left == right); }
        }

        public struct Monitor : IEquatable<Monitor>
        {
            public static readonly Monitor None = new Monitor(IntPtr.Zero);

            public IntPtr Pointer;

            internal Monitor(IntPtr pointer)
            {
                Pointer = pointer;
            }

            public override bool Equals(object obj)
            {
                if (obj is Monitor win) { return Equals(this, win); }
                else { return false; }
            }

            public bool Equals(Monitor other)
            {
                return other.Pointer == Pointer;
            }

            public override int GetHashCode()
            {
                return -638417062 + EqualityComparer<IntPtr>.Default.GetHashCode(Pointer);
            }

            public static bool operator ==(Monitor left, Monitor right) { return left.Equals(right); }
            public static bool operator !=(Monitor left, Monitor right) { return !(left == right); }
        }

        public struct Cursor { }

        public struct Image { }

        public unsafe struct VideoMode
        {
            public int Width;
            public int Height;

            public int RedBits;
            public int GreenBits;
            public int BlueBits;

            public int RefreshRate;
        }

        public unsafe struct GammaRamp
        {
            /*! An array of value describing the response of the red channel. */
            public ushort* Red;

            /*! An array of value describing the response of the green channel. */
            public ushort* Green;

            /*! An array of value describing the response of the blue channel.*/
            public ushort* Blue;

            /*! The number of elements in each array.*/
            public uint Size;
        }

        public unsafe struct GamepadState
        {
            /*! The states of each [gamepad button](@ref gamepad_buttons), `PRESS`
             *  or `RELEASE`.
             */
            public fixed byte Buttons[15];

            /*! The states of each [gamepad axis](@ref gamepad_axes), in the range -1.0
             *  to 1.0 inclusive.
             */
            public fixed float Axes[6];
        }

        public struct VkInstance { }

        public struct VkPhysicalDevice { }

        public struct VkAllocationCallbacks { }

        public struct VkSurfaceKHR { }

        public struct VkResult { }
    }
}
