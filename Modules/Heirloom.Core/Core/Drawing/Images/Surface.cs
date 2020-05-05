using System;

namespace Heirloom
{
    /// <summary>
    /// Represents a surface a <see cref="GraphicsContext"/> object can draw on.
    /// </summary>
    public sealed class Surface : ImageSource
    {
        #region Constructors

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="width">Width of the surface in pixels.</param>
        /// <param name="height">Height of the surface in pixels.</param>
        /// <param name="multisample">MSAA to use on the surface</param>
        /// <param name="surfaceType">The storage format of the surface.</param>
        public Surface(int width, int height, MultisampleQuality multisample = MultisampleQuality.None, SurfaceType surfaceType = SurfaceType.UnsignedByte)
            : this(new IntSize(width, height), multisample, surfaceType)
        { }

        /// <summary>
        /// Creates a new surface.
        /// </summary>
        /// <param name="size">Size of the surface in pixels.</param>
        /// <param name="multisample">MSAA to use on the surface</param>
        /// <param name="surfaceType">The storage format of the surface.</param>
        public Surface(IntSize size, MultisampleQuality multisample = MultisampleQuality.None, SurfaceType surfaceType = SurfaceType.UnsignedByte)
            : this(size, multisample, surfaceType, false)
        { }

        internal Surface(IntSize size, MultisampleQuality multisample, SurfaceType surfaceType, bool isScreenBound)
        {
            if (size.Width <= 0 || size.Height <= 0) { throw new ArgumentException("Surface dimensions must be greater than zero."); }

            Size = size;
            IsScreenBound = isScreenBound;
            SurfaceType = surfaceType;

            // Keep highest supported MSAA level.
            Multisample = Calc.Min(multisample, GraphicsAdapter.SurfaceFactory.MaxSupportedMultisampleQuality);
            if (Multisample != multisample) { Log.Warning($"Requested MSAA level '{multisample}' was not supported on this device."); }

            if (!IsScreenBound)
            {
                // Surface is an offscreen render target
                Native = GraphicsAdapter.SurfaceFactory.Create(this);
                Multisample = multisample;
            }
            else
            {
                if (SurfaceType != SurfaceType.UnsignedByte)
                {
                    throw new ArgumentException($"Screen bound surfaces must be have type '{nameof(SurfaceType.UnsignedByte)}'.");
                }
            }
        }

        /// <inheritdoc/>
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

        /// <summary>
        /// Gets the type of this surface.
        /// </summary>
        public SurfaceType SurfaceType { get; }

        /// <summary>
        /// Determines if this surface is attached to a screen (ie, a window).
        /// </summary>
        public bool IsScreenBound { get; }

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
