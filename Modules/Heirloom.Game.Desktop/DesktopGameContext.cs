using Heirloom.Desktop;

namespace Heirloom.Game.Desktop
{
    public abstract class DesktopGameContext : GameContext
    {
        public Window Window { get; }

        public DesktopGameContext(string title)
            : this(new Window(title))
        { }

        public DesktopGameContext(string title, WindowCreationSettings settings)
            : this(new Window(title, settings))
        { }

        public DesktopGameContext(Window window)
            : base(window.RenderContext)
        {
            Window = window;

            // Listen for input from this window
            Input.AddInputSource(new StandardDesktopInput(Window));
        }
    }
}
