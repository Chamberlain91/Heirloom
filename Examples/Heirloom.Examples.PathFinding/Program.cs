using System.Collections.Generic;

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

            Graphics.Performance.ShowOverlay = true;
        }

        protected override void Update(float dt)
        {
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);

            Graphics.Color = Color.Gray;
            foreach (var co in Rasterizer.Rectangle(Maze.Grid.Size))
            {
                if (Maze.Grid[co])
                {
                    var box = new Rectangle(co * 16, IntSize.One * 16);
                    Graphics.DrawRect(box);
                }
            }

            var pathOffset = new Vector(8, 8);

            //Graphics.Color = Color.Cyan;
            //foreach (var (a, b) in Maze.Graph.Edges)
            //{
            //    Graphics.DrawLine((a * 16) + pathOffset, (b * 16) + pathOffset);
            //}

            Graphics.Color = Color.White;
            Graphics.DrawRect((_pathStart * 16, (16, 16)));

            if (_path != null)
            {
                Graphics.Color = Color.White;
                Graphics.DrawRect((_pathTarget * 16, (16, 16)));

                Graphics.Color = Color.Orange;
                for (var i = 1; i < _path.Count; i++)
                {
                    var prev = _path[i - 1];
                    var curr = _path[i + 0];

                    Graphics.DrawLine((prev * 16) + pathOffset, (curr * 16) + pathOffset, 2);
                }
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
                    _path = Maze.Graph.FindPath(_pathStart, _pathTarget, (a, b) => 1, co => IntVector.ManhattanDistance(co, _pathTarget));
                }
            }

            Graphics.Screen.Refresh();
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Path Finding", (512, 512), MultisampleQuality.High);
            window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;

            return window.Graphics;
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }

    public sealed class Maze
    {
        public Maze(int width, int height)
        {
            Graph = new Graph<IntVector, float>(directed: false, 1F);
            Grid = new Grid<bool>(width, height);

            // Generate stage
            foreach (var co in Rasterizer.Rectangle(Grid.Size))
            {
                Grid[co] = Calc.Simplex.Sample((Vector) co / 4F) < 0.2F;
                Graph.AddVertex(co);
            }

            // Generate graph
            foreach (var co in Rasterizer.Rectangle(Grid.Size))
            {
                if (Grid[co])
                {
                    Connect(co, (+1, 0));
                    Connect(co, (-1, 0));
                    Connect(co, (0, +1));
                    Connect(co, (0, -1));
                }
            }

            // Keep only largest component
            Graph = Graph.GetComponents().FindMaximal(graph => graph.Vertices.Count);

            void Connect(IntVector src, IntVector offset)
            {
                var dst = src + offset;
                if (Grid.IsValidCoordinate(dst) && Grid[dst])
                {
                    if (!Graph.ContainsEdge(src, dst))
                    {
                        Graph.AddEdge(src, dst);
                    }
                }
            }
        }

        public Graph<IntVector, float> Graph { get; }

        public Grid<bool> Grid { get; }
    }
}
