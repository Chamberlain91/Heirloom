using System;
using System.Runtime.InteropServices;

namespace Heirloom.Desktop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VideoMode : IEquatable<VideoMode>
    {
        public int Width;

        public int Height;

        public int RedBits;

        public int GreenBits;

        public int BlueBits;

        public int RefreshRate;

        public override bool Equals(object obj)
        {
            return obj is VideoMode mode ? Equals(mode) : false;
        }

        public bool Equals(VideoMode obj)
        {
            return obj.Width == Width
                && obj.Height == Height
                && obj.RedBits == RedBits
                && obj.GreenBits == GreenBits
                && obj.BlueBits == BlueBits
                && obj.RefreshRate == RefreshRate;
        }

        public override string ToString()
        {
            return string.Format("VideoMode(width: {0}, height: {1}, redBits: {2}, greenBits: {3}, blueBits: {4}, refreshRate: {5})",
                Width.ToString(),
                Height.ToString(),
                RedBits.ToString(),
                GreenBits.ToString(),
                BlueBits.ToString(),
                RefreshRate.ToString()
            );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Width.GetHashCode();
                hash = hash * 23 + Height.GetHashCode();
                hash = hash * 23 + RedBits.GetHashCode();
                hash = hash * 23 + GreenBits.GetHashCode();
                hash = hash * 23 + BlueBits.GetHashCode();
                hash = hash * 23 + RefreshRate.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(VideoMode a, VideoMode b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(VideoMode a, VideoMode b)
        {
            return !a.Equals(b);
        }
    }
}
