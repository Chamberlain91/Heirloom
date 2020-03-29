using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Heirloom.Game.Desktop
{
    public abstract class DesktopGameContext : GameContext
    {
        /// <summary>
        /// Gets the game window.
        /// </summary>
        public Window Window { get; }

        protected DesktopGameContext(string title)
            : this(new Window(title))
        { }

        protected DesktopGameContext(string title, MultisampleQuality multisample)
            : this(new Window(title, multisample))
        { }

        protected DesktopGameContext(string title, WindowCreationSettings settings)
            : this(new Window(title, settings))
        { }

        protected DesktopGameContext(Window window)
            : base(window.Graphics)
        {
            Window = window;

            // Listen for input from this window
            Input.AddInputSource(new StandardDesktopInput(Window));
        }
    }
}
