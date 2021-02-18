using System;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents an off-screen render texture (also known as a framebuffer).
    /// </summary>
    public sealed class Surface : Texture
    {
        #region Constructors

        internal Surface(MultisampleQuality multisample, SurfaceFormat format, IScreen screen)
        {
            Screen = screen;
            Format = format;

            // Keep minimum allowable value
            Multisample = Calc.Min(GraphicsBackend.Current.Capabilities.MaxSupportedMultisample, multisample);
        }

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="size">The dimensions of this surface.</param>
        /// <param name="multisample">The multisample quality.</param>
        /// <param name="format">The surface pixel format.</param>
        public Surface(IntSize size, MultisampleQuality multisample = MultisampleQuality.None, SurfaceFormat format = SurfaceFormat.UnsignedByte)
            : this(multisample, format, null)
        {
            if (IsInvalidSize(size)) { throw new ArgumentException("Unable to create surface, invalid size.", nameof(size)); }

            Size = size;
        }

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="width">The width of this surface.</param>
        /// <param name="height">The height of this surface.</param>
        /// <param name="multisample">The multisample quality.</param>
        /// <param name="format">The surface pixel format.</param>
        public Surface(int width, int height, MultisampleQuality multisample = MultisampleQuality.None, SurfaceFormat format = SurfaceFormat.UnsignedByte)
            : this(new IntSize(width, height), multisample, format)
        { }

        private static bool IsInvalidSize(IntSize size)
        {
            if (size.Width <= 0 || size.Height <= 0) { return true; } // Too small
            if (size.Width > Image.MaxImageDimension || size.Height >= Image.MaxImageDimension) { return true; } // Too large
            return false; // The surface will be a suitable size
        }

        #endregion

        /// <summary>
        /// Gets the multisample level of the surface.
        /// </summary>
        public MultisampleQuality Multisample { get; internal set; }

        /// <summary>
        /// Gets the surface format of the surface.
        /// </summary>
        public SurfaceFormat Format { get; }

        internal IScreen Screen { get; }

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
