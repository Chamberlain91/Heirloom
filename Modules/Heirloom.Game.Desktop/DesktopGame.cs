using System;

using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Heirloom.Game.Desktop
{
    public abstract class DesktopGame : AbstractGame
    {
        protected DesktopGame(string title)
            : base(title)
        {
            Window = new DesktopGameWindow(this);
        }

        public GameWindow Window { get; }

        public static void Run<TGame>() where TGame : DesktopGame, new()
        {
            Application.Run(() =>
            {
                var game = new TGame();
                Input.AddInputSource(new StandardDesktopInput(game.Window));
                game.Window.Run();
            });
        }

        internal new void Update(RenderContext ctx, float dt)
        {
            base.Update(ctx, dt);
        }

        private class DesktopGameWindow : GameWindow
        {
            public DesktopGameWindow(DesktopGame game)
                : base(game.Title)
            {
                Game = game ?? throw new ArgumentNullException(nameof(game));
            }

            public DesktopGame Game { get; }

            protected override void Update(RenderContext ctx, float dt)
            {
                Game.Update(ctx, dt);
            }
        }
    }
}
