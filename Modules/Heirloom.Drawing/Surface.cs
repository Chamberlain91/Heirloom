using System;

using Heirloom.IO;
using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents a surface a <see cref="Graphics"/> object can draw on.
    /// </summary>
    public sealed class Surface : ImageSource
    {
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
                Native = GraphicsAdapter.SurfaceFactory.Create(Size, ref multisample);
                Multisample = multisample;
            }
        }

        ~Surface()
        {
            Log.Debug($"[Dispose] Surface");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets size of the surface in pixels.
        /// </summary>
        public override IntSize Size { get; protected set; }

        /// <summary>
        /// Gets the multisampling quality set on this surface.
        /// </summary>
        /// <remarks>
        /// This wll be set to the value actually availble used to create the surface.
        /// Some platforms might not support all multisample levels.
        /// </remarks>
        public MultisampleQuality Multisample { get; }

        #endregion

        /// <summary>
        /// Gets the max multisample quality supported on this system.
        /// </summary>
        public static MultisampleQuality MaxSupportedMultisampleQuality => GraphicsAdapter.SurfaceFactory.MaxSupportedMultisampleQuality;

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
