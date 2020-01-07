using Heirloom.Drawing;

namespace Heirloom.Desktop
{
    internal abstract class WindowGraphicsAdapter : GraphicsAdapter
    {
        /// <summary>
        /// Creates a graphics context associated with the specified window.
        /// </summary>
        internal abstract Graphics CreateGraphics(Window window); 
    }
}
