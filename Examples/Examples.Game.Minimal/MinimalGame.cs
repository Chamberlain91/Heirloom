using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Game.Desktop;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Game.Minimal
{
    internal class MinimalGame : DesktopGameContext
    {
        public MinimalGame()
            : base("Minimal Game Example", new WindowCreationSettings { Multisample = MultisampleQuality.Medium })
        {
            Graphics.EnableFPSOverlay = true;
            Window.Size = (800, 500);
        }

        protected override void GameLoad(LoadScreenProgress progress)
        {
            // Nothing!
        }

        protected override void GameStart()
        {
            // Create card and camera
            Scene.AddEntity(new QueenOfHearts());
            Scene.AddEntity(new Camera());
        }

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var game = new MinimalGame();
                game.Start();
            });
        }

        private sealed class QueenOfHearts : Entity
        {
            private float _time;

            public QueenOfHearts()
            {
                // Load card image
                var image = new Image(Files.ReadBytes("files/cardHeartsQ.png"));
                image.Origin = image.Bounds.Center;

                AddComponent(new ImageComponent(image));
            }

            protected override void Update(float dt)
            {
                _time += dt;

                // Make our card graphic dance
                Transform.Rotation = Calc.Sin(_time + (Calc.Cos(_time * 4) * (2 / 3F))) * (22.5F * Calc.ToRadians);
            }
        }
    }
}
