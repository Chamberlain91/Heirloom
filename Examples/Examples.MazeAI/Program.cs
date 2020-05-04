
using Heirloom;
using Heirloom.Desktop;

namespace Examples.MazeAI
{
    internal class Program : GameLoop
    {
        public Maze Maze;

        public Program()
            : base(new Window("Maze AI", (496, 496)) { IsResizable = false })
        {
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

        protected override void Update(Graphics gfx, float dt)
        {
            gfx.GlobalTransform = Matrix.CreateScale(2F);
            gfx.Clear(Color.DarkGray);

            // Draw Tile Grid
            foreach (var co in Maze.GetCoordinates(31, 31))
            {
                var tileIndex = Maze.Tiles[co];
                if (tileIndex >= 0)
                {
                    var image = Assets.GetImage(tileIndex);
                    gfx.DrawImage(image, co * 8);
                }
                else
                {
                    // Bad!
                    gfx.PushState();
                    gfx.Color = Color.Orange;
                    gfx.DrawRect((co * 8, (8, 8)));
                    gfx.PopState();
                }
            }

            //// Draw Graph
            //foreach (var source in Maze.Graph.Vertices)
            //{
            //    foreach (var target in Maze.Graph.GetSuccessors(source))
            //    {
            //        var a = (12, 12) + source * 16F;
            //        var b = (12, 12) + target * 16F;
            //        gfx.Color = Color.FromHSV(0F, 1F, Maze.Graph.GetWeight(source, target));
            //        gfx.DrawLine(a, b);
            //    }
            //}
        }
    }
}
