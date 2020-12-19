using System;

using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows
{
    /// <summary>
    /// A screen represents a window or panel to view graphical content.
    /// </summary>
    /// <remarks>
    /// On certain platforms, the screen size and surface size may not be equal.
    /// </remarks>
    public abstract class Screen
    {
        protected Screen(MultisampleQuality multisample)
        {
            Surface = new Surface(multisample, SurfaceFormat.UnsignedByte, this);
        }

        /// <summary>
        /// Gets or serts the size of this screen.
        /// </summary>
        public abstract IntSize Size { get; set; }

        #region Graphics

        /// <summary>
        /// Gets the graphics context associated with this screen.
        /// </summary>
        public abstract GraphicsContext Graphics { get; }

        /// <summary>
        /// Gets the surface associated with this screen (aka, the backbuffer).
        /// </summary>
        public Surface Surface { get; }

        #endregion

        #region Events

        /// <summary>
        /// Event called when the screen is resized.
        /// </summary>
        /// <seealso cref="ContentScaleChanged"/>
        /// <seealso cref="Surface"/>
        public event Action<Screen, IntSize> Resized;

        protected void OnResized(IntSize size)
        {
            Resized?.Invoke(this, size);
        }

        /// <summary>
        /// Event called when the surface associated with the screen is resized.
        /// </summary>
        /// <seealso cref="ContentScaleChanged"/>
        /// <seealso cref="Surface"/>
        public event Action<Screen, IntSize> SurfaceResized;

        protected void OnSurfaceResized(IntSize size)
        {
            SurfaceResized?.Invoke(this, size);
        }

        #endregion

        public virtual void Refresh()
        {
            Graphics.CompleteFrame();
        }
    }
}
