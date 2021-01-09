using System;
using System.Diagnostics;

using Heirloom;
using Heirloom.Desktop;

namespace Examples.MazeAI
{
    internal class Program : GameLoop
    {
        public Maze Maze;
        public Player Player;

        public static Color Sunflower = new ColorBytes(241, 196, 15);
        public static Color WetAsphalt = new ColorBytes(52, 73, 94);

        public Program()
            : base(new Window("Maze AI", (496, 496)) { IsResizable = false })
        {
            Player = new Player();
            Maze = new Maze();
        }

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                Assets.LoadAssets();

                var game = new Program();
                game.Start();
            });
        }

        protected override void Update(float dt)
        {
            // Process Player
            Player.Update(Maze, dt);
            Graphics.GlobalTransform = Matrix.CreateScale(2F);
            Graphics.Clear(Color.DarkGray);

            // Draw Tile Grid
            foreach (var co in Maze.GetCoordinates(31, 31))
            {
                var tileIndex = Maze.Tiles[co];
                if (tileIndex >= 0)
                {
                    var image = Assets.GetImage(tileIndex);
                    Graphics.DrawImage(image, co * 8);
                }
                else
                {
                    Graphics.PushState();
                    Graphics.Color = Color.Orange;
                    Graphics.DrawRect((co * 8, (8, 8)));
                    Graphics.PopState();
                }
            }
            Graphics.PushState();
            Graphics.DrawImage(Assets.GetImage(11), (8, 8) + Player.Position * 16);
            if (Player.TargetPosition != Player.Goal)
            {
                Graphics.Color = WetAsphalt;
                foreach (var co in Player.MoveQueue)
                {
                    Graphics.DrawCross((12, 12) + co * 16, 1);
                }
                Graphics.Color = Sunflower;
                Graphics.DrawCross((12, 12) + Player.Goal * 16, 1);
            }
            Graphics.PopState();

            // Draw graph debug
            DrawGraphDebug(Graphics);
        }

        [Conditional("DEBUG")]
        private void DrawGraphDebug(GraphicsContext gfx)
        {
            // Draw Graph
            foreach (var source in Maze.Graph.Vertices)
            {
                foreach (var target in Maze.Graph.GetNeighbors(source))
                {
                    var a = (12, 12) + source * 16F;
                    var b = (12, 12) + target * 16F;
                    gfx.Color = Color.Black;
                    gfx.DrawLine(a, b);
                }
            }
        }
    }
}
