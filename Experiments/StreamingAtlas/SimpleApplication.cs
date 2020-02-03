using Heirloom.Desktop;
using Heirloom.Drawing;

namespace StreamingAtlas
{
    public abstract class SimpleApplication
    {
        /// <summary>
        /// Gets the main window.
        /// </summary>
        public Window Window { get; }

        /// <summary>
        /// Gets the main (render thread) loop.
        /// </summary>
        public RenderLoop Loop { get; }

        protected SimpleApplication(string title, bool vsync = true)
        {
            Window = new Window(title, vsync);
            Window.KeyRelease += (w, e) => OnKeyEvent(e);
            Window.KeyRepeat += (w, e) => OnKeyEvent(e);
            Window.KeyPress += (w, e) => OnKeyEvent(e);
            Window.MouseRelease += (w, e) => OnMouseButtonEvent(e);
            Window.MousePress += (w, e) => OnMouseButtonEvent(e);

            Loop = RenderLoop.Create(Window.Graphics, OnFrameUpdate);
        }

        protected abstract void OnMouseButtonEvent(MouseButtonEvent e);

        protected abstract void OnKeyEvent(KeyEvent e);

        protected abstract void OnFrameUpdate(Graphics gfx, float dt);

        public static void Start<TGame>() where TGame : SimpleApplication, new()
        {
            Application.Run(() =>
            {
                var game = new TGame();
                game.Loop.Start();
            });
        }
    }
}
