using System;
using System.Runtime.InteropServices;

namespace Heirloom.Desktop
{
    /// <summary>
    /// Represents a video mode a <see cref="Monitor"/> can be in.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct VideoMode : IEquatable<VideoMode>
    {
        /// <summary>
        /// The width in pixels.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// The height in pixels.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// The number of red bits in the color space supported by the mode.
        /// </summary>
        public readonly int RedBits;

        /// <summary>
        /// The number of green bits in the color space supported by the mode.
        /// </summary>
        public readonly int GreenBits;

        /// <summary>
        /// The number of blue bits in the color space supported by the mode.
        /// </summary>
        public readonly int BlueBits;

        /// <summary>
        /// The refresh rate (in hertz) of the mode.
        /// </summary>
        public readonly int RefreshRate;

        #region Constructors

        /// <summary>
        /// Constructs a new <see cref="VideoMode"/>.
        /// </summary>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="redBits">The number of red bits.</param>
        /// <param name="greenBits">The number of green bits.</param>
        /// <param name="blueBits">The number of blue bits.</param>
        /// <param name="refreshRate">The refresh rate (in hertz).</param>
        public VideoMode(int width, int height, int redBits, int greenBits, int blueBits, int refreshRate)
        {
            Width = width;
            Height = height;
            RedBits = redBits;
            GreenBits = greenBits;
            BlueBits = blueBits;
            RefreshRate = refreshRate;
        }

        /// <summary>
        /// Constructs a new <see cref="VideoMode"/> with default bit depth.
        /// </summary>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="refreshRate">The refresh rate (in hertz).</param>
        public VideoMode(int width, int height, int refreshRate)
            : this(width, height, 0, 0, 0, refreshRate)
        { }

        /// <summary>
        /// Constructs a new <see cref="VideoMode"/> with the default refresh rate and bit depth.
        /// </summary>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        public VideoMode(int width, int height)
            : this(width, height, 0)
        { }

        #endregion

        #region Equality

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is VideoMode mode
                && Equals(mode);
        }

        /// <summary>
        /// Compares the <see cref="VideoMode"/> again another for equality.
        /// </summary>
        /// <param name="other">Some other <see cref="VideoMode"/>.</param>
        /// <returns>True, if all fields are equal.</returns>
        public bool Equals(VideoMode other)
        {
            return other.Width == Width
                && other.Height == Height
                && other.RedBits == RedBits
                && other.GreenBits == GreenBits
                && other.BlueBits == BlueBits
                && other.RefreshRate == RefreshRate;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
                Width, Height, RefreshRate,
                RedBits, GreenBits, BlueBits);
        }

        /// <summary>
        /// Compares two <see cref="VideoMode"/> for equality.
        /// </summary>
        public static bool operator ==(VideoMode a, VideoMode b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares two <see cref="VideoMode"/> for inequality.
        /// </summary>
        public static bool operator !=(VideoMode a, VideoMode b)
        {
            return !a.Equals(b);
        }

        #endregion

        /// <summary>
        /// Converts this video mode into a string representation.
        /// </summary>
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
    }
}
