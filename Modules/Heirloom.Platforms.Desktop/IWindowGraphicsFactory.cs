using Heirloom.Drawing;

namespace Heirloom.Desktop
{
    internal interface IWindowGraphicsFactory
    {
        /// <summary>
        /// Creates a graphics context associated with the specified window.
        /// </summary>
        Graphics CreateGraphics(Window window, Surface surface, bool vsync);
    }
}
