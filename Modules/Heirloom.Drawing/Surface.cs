using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents a surface a <see cref="Graphics"/> object can draw on.
    /// </summary>
    public sealed class Surface : ImageSource
    {
        internal readonly object Native;

        #region Constructors

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="size">Size of the surface in pixels.</param>
        /// <param name="multisample">MSAA to use on the surface</param>
        public Surface(IntSize size, MultisampleQuality multisample = MultisampleQuality.None)
            : this(size.Width, size.Height, multisample)
        { }

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="width">Width of the surface in pixels.</param>
        /// <param name="height">Height of the surface in pixels.</param>
        /// <param name="multisample">MSAA to use on the surface</param>
        public Surface(int width, int height, MultisampleQuality multisample = MultisampleQuality.None)
            : this(width, height, multisample, true)
        { }

        internal Surface(int width, int height, MultisampleQuality multisample, bool createNative)
        {
            if (width <= 0 || height <= 0) { throw new ArgumentException("Surface dimensions must be greater than zero."); }

            Size = new IntSize(width, height);
            Multisample = multisample;

            if (createNative)
            {
                Native = GraphicsAdapter.SurfaceFactory.Create(Size, multisample);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets size of the surface in pixels.
        /// </summary>
        public override IntSize Size { get; protected set; }

        /// <summary>
        /// Gets the surface width in pixels.
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// Gets the surface height in pixels.
        /// </summary>
        public int Height => Size.Height;

        /// <summary>
        /// Gets the multisampling quality set on this surface.
        /// </summary>
        public MultisampleQuality Multisample { get; }

        #endregion

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
