using Heirloom.Desktop;

namespace Heirloom.Game.Desktop
{
    public abstract class DesktopGameContext : GameContext
    {
        /// <summary>
        /// Gets the game window.
        /// </summary>
        public Window Window { get; }

        protected DesktopGameContext(string title)
            : this(title, WindowCreationSettings.Default)
        { }

        protected DesktopGameContext(string title, WindowCreationSettings settings)
            : this(new Window(title, settings))
        { }

        protected DesktopGameContext(Window window)
            : base(window.RenderContext)
        {
            Window = window;

            // Listen for input from this window
            Input.AddInputSource(new StandardDesktopInput(Window));
        }
    }
}
