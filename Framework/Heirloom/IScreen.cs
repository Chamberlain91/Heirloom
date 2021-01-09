using System;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom
{
    /// <summary>
    /// A screen represents a window or panel to view graphical content.
    /// </summary>
    /// <remarks>
    /// On certain platforms, the screen size and surface size may not be equal.
    /// </remarks>
    public interface IScreen
    {
        /// <summary>
        /// Gets or serts the size of this screen.
        /// </summary>
        public IntSize Size { get; set; }

        /// <summary>
        /// Gets the graphics context associated with this screen.
        /// </summary>
        public GraphicsContext Graphics { get; }

        /// <summary>
        /// Gets the surface associated with this screen (aka, the backbuffer).
        /// </summary>
        public Surface Surface { get; }

        /// <summary>
        /// Event called when the screen is resized.
        /// </summary>
        /// <seealso cref="ContentScaleChanged"/>
        /// <seealso cref="Surface"/>
        public event Action<IScreen, IntSize> Resized;

        /// <summary>
        /// Event called when the surface associated with the screen is resized.
        /// </summary>
        /// <seealso cref="ContentScaleChanged"/>
        /// <seealso cref="Surface"/>
        public event Action<IScreen, IntSize> SurfaceResized;

        /// <summary>
        /// Cause the screen to submit its contents to the display (ie, swap buffers, repaint).
        /// </summary>
        void Refresh();
    }
}
