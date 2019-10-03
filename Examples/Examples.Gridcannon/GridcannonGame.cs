using System.Collections;

using Heirloom.Desktop;
using Heirloom.Game;
using Heirloom.Game.Desktop;
using Heirloom.Math;

using static Examples.Gridcannon.Assets;

namespace Examples.Gridcannon
{
    internal class GridcannonGame : DesktopGameContext
    // rules: https://www.pentadact.com/2019-08-20-gridcannon-a-single-player-game-with-regular-playing-cards/
    {
        public static int Padding = 12;

        // 
        public readonly Scene Scene;

        public GridcannonGame()
            : base("Gridcannon")
        {
            RenderContext.ShowFPSOverlay = true;

            // == Load Assets

            LoadAssets();

            // == Size Window

            Window.Size = new IntSize(Padding + (GfxCardBack.Width + Padding) * 5, (GfxCardBack.Height + Padding) * 6);
            Window.IsResizable = false;
            Window.MoveToCenter();

            // == Initialize Game State

            SceneManager.Add(Scene = new Scene());
            Scene.Add(new GameManager());
        }

        private IEnumerator CoAnimateToPosition(Card card, Vector target)
        {
            var timer = Timer.StartNew(0.8F);
            var start = card.Transform.Position;

            while (timer.Remaining > 0)
            {
                var t = timer.Elapsed / timer.Duration;
                t = Calc.SmootherStep(0, 1, t);

                card.Transform.Position = Vector.Lerp(start, target, t);
                yield return Coroutine.WaitNextFrame();
            }

            // Snap to final position
            card.Transform.Position = target;
        }

        private static void Main(string[] _)
        {
            Application.Run(() =>
            {
                // Begin game
                var game = new GridcannonGame();
                game.Start();
            });
        }
    }
}
