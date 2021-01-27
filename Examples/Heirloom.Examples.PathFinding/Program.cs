using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.PathFinding
{
    public class Program : GameWrapper
    {
        public Maze Maze { get; }

        private IntVector _pathStart, _pathTarget;
        private IReadOnlyList<IntVector> _path;
        private ClickPhase _phase;

        enum ClickPhase { First, Second }

        public Program()
            : base(CreateWindowGraphics())
        {
            Maze = new Maze(32, 32);
        }

        protected override void Update(float dt)
        {
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);

            foreach (var co in Rasterizer.Rectangle(Maze.Grid.Size))
            {
                if (Maze.Grid[co])
                {
                    var box = new Rectangle(co * 16, IntSize.One * 16);
                    if (((co.X & 1) ^ (co.Y & 1)) == 0) { Graphics.Color = Color.LightGray * Color.LightGray; }
                    else { Graphics.Color = Color.Gray; }
                    Graphics.DrawRect(box);
                }
            }

            var pathOffset = new Vector(8, 8);

            // Draw nav mesh
            Graphics.Color = Fade(Color.DarkGray, 0.1F);
            foreach (var (a, b) in Maze.Graph.Edges)
            {
                var p0 = (a * 16) + pathOffset;
                var p1 = (b * 16) + pathOffset;

                Graphics.DrawLine(p0, p1, 1);
            }

            // Draw start point
            Graphics.Color = Color.White;
            Graphics.DrawRect((_pathStart * 16, (16, 16)));

            if (_path != null)
            {
                // Draw end point
                Graphics.Color = Color.White;
                Graphics.DrawRect((_pathTarget * 16, (16, 16)));

                // Draw path betwen start and end points
                Graphics.Color = Color.Cyan;
                Graphics.DrawPolyLine(_path.Select(x => (Vector) x * 16 + pathOffset), 2);
            }

            if (Input.IsMousePressed(MouseButton.Left))
            {
                var co = (IntVector) Vector.Floor(Input.MousePosition / 16F);

                if (_phase == ClickPhase.First)
                {
                    _pathStart = co;
                    _phase = ClickPhase.Second;

                    // Clear path
                    _path = null;
                }
                else
                if (_phase == ClickPhase.Second)
                {
                    _pathTarget = co;
                    _phase = ClickPhase.First;

                    // Find path
                    _path = Maze.Graph.FindPath(_pathStart, _pathTarget, (a, b) => 1, co => IntVector.Distance(co, _pathTarget));
                }
            }

            Graphics.Screen.Refresh();
        }

        private static Color Fade(Color color, float alpha)
        {
            color.A = alpha;
            return color;
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Path Finding", (512, 512), MultisampleQuality.High) { IsResizable = false };
            window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;

            return window.Graphics;
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
