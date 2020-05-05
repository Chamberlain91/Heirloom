namespace Heirloom.Desktop
{
    internal interface IWindowGraphicsFactory
    {
        /// <summary>
        /// Creates a graphics context associated with the specified window.
        /// </summary>
        GraphicsContext CreateGraphics(Window window, bool vsync);
    }
}
