using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents a surface drawing onto.
    /// </summary>
    public sealed class Surface2 : IImageSource
    {
        private InterpolationMode _interpolationMode;
        private uint _version;

        #region Constructors

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="size">Size of the surface in pixels.</param>
        /// <param name="multisample">MSAA to use on the surface</param>
        public Surface2(IntSize size, MultisampleQuality multisample = MultisampleQuality.None)
            : this(size.Width, size.Height, multisample)
        { }

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="width">Width of the surface in pixels.</param>
        /// <param name="height">Height of the surface in pixels.</param>
        /// <param name="multisample">MSAA to use on the surface</param>
        public Surface2(int width, int height, MultisampleQuality multisample = MultisampleQuality.None)
        {
            if (width <= 0 || height <= 0) { throw new ArgumentException("Surface dimensions must be greater than zero."); }

            Size = new IntSize(width, height);
            Multisample = multisample;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the multisampling quality set on this surface.
        /// </summary>
        public MultisampleQuality Multisample { get; }

        public InterpolationMode InterpolationMode
        {
            get => _interpolationMode;

            set
            {
                _interpolationMode = value;

                // We increment to cause the implementing system to update the representation of this image.
                // There might be a better way to notify this change without causing the upload of pixels.
                IncrementVersion();
            }
        }

        /// <summary>
        /// Gets size of the surface in pixels.
        /// </summary>
        public IntSize Size { get; internal set; }

        /// <summary>
        /// The width of the image (in pixels).
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// The height of the image (in pixels).
        /// </summary>
        public int Height => Size.Height;

        public Vector Origin { get; set; }

        uint IImageSource.Version
        {
            get => _version;
            set => _version = value;
        }

        #endregion 

        private void IncrementVersion()
        {
            _version++;

            if (_version == uint.MaxValue)
            {
                _version = 0;
            }
        }
    }
}
