using System.Collections.Generic;

using Heirloom.Collections;
using Heirloom.Collections.Spatial;
using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.PathFinding
{
    public class PathFinding : GameWindow
    {
        private const int CellSize = 48;

        public IFiniteGrid<bool> Grid { get; }

        public IList<IntVector> Path;

        public PathFinding()
            : base("Path Finding")
        {
            // Create Grid
            Grid = CreateGrid(new char[,] {
                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { ' ', 'X', 'X', 'X', 'X', 'X', 'X', 'X', ' ' },
                { ' ', 'X', ' ', ' ', ' ', ' ', ' ', 'X', ' ' },
                { ' ', 'X', ' ', 'X', 'X', 'X', ' ', 'X', ' ' },
                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
            });

            // Find Path
            if (FindPath((0, 0), (5, 2), out Path))
            {
                // Path Found!
            }

            // Size window to fit example world
            Size = (Grid.Width * CellSize, Grid.Height * CellSize);
            IsResizeable = false;
        }

        public bool FindPath(IntVector start, IntVector goal, out IList<IntVector> path)
        {
            path = Search.HeuristicSearch(start, goal, co => Grid.FindNeighbors(co, x => !x), IntVector.ManhattanDistance);
            return path != null;
        }

        protected override void Update()
        {
            // TODO: Make Interactive
        }

        protected override void Render(RenderContext ctx)
        {
            // == Draw Grid

            for (var y = 0; y < Grid.Height; y++)
            {
                for (var x = 0; x < Grid.Width; x++)
                {
                    var rect = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);

                    if (Grid[x, y])
                    {
                        // Wall Tiles
                        ctx.DrawRect(rect, Colors.FlatUI.Concrete);
                        ctx.DrawRectOutline(rect, Colors.FlatUI.Asbestos);
                    }
                    else
                    {
                        // Floor Tiles
                        ctx.DrawRect(rect, Colors.FlatUI.WetAshphalt);
                        ctx.DrawRectOutline(rect, Colors.FlatUI.MidnightBlue);
                    }
                }
            }

            // == Draw Path

            if (Path != null)
            {
                var offset = new Vector(CellSize / 2, CellSize / 2);

                // Draw Path (Line)
                for (var i = 0; i < Path.Count - 1; i++)
                {
                    var p1 = offset + (Path[i + 0] * CellSize);
                    var p2 = offset + (Path[i + 1] * CellSize);
                    ctx.DrawLine(p1, p2, Colors.FlatUI.BelizeHole);
                }

                // Draw Path (Points)
                var dotSize = new Size(4, 4);
                var dotOffset = (Vector) dotSize * 0.5F;
                for (var i = 0; i < Path.Count; i++)
                {
                    var rect = new Rectangle(offset - dotOffset + (Path[i] * CellSize), dotSize);
                    ctx.DrawRect(rect, Colors.FlatUI.Sunflower);
                }
            }
        }

        private static IFiniteGrid<bool> CreateGrid(char[,] data)
        {
            var width = data.GetLength(1);
            var height = data.GetLength(0);

            var grid = new Grid<bool>(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    grid[x, y] = data[y, x] != ' ';
                }
            }

            return grid;
        }

        private static void Main(string[] _)
        {
            Run(new PathFinding());
        }
    }
}
