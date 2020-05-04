using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom;
using Heirloom.Collections;
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
                var image = Assets.GetImage(Maze.Tiles[co]);
                gfx.DrawImage(image, co * 8);
            }

            // Draw Graph
            foreach (var source in Maze.Graph.Vertices)
            {
                foreach (var target in Maze.Graph.GetSuccessors(source))
                {
                    var a = (12, 12) + source * 16F;
                    var b = (12, 12) + target * 16F;
                    gfx.Color = Color.FromHSV(0F, 1F, Maze.Graph.GetWeight(source, target));
                    gfx.DrawLine(a, b);
                }
            }
        }
    }

    public class Maze
    {
        public Maze()
        {
            Tiles = new Grid<int>(31, 31);
            Tiles.Clear(12);

            // Generate graph
            Graph = Generate();

            // Render floors into tileset
            foreach (var source in Graph.Vertices)
            {
                // For each place visible
                foreach (var target in Graph.GetSuccessors(source))
                {
                    var aco = (1, 1) + (source * 2);
                    var bco = (1, 1) + (target * 2);

                    // Draw edges into tile map
                    foreach (var m in Rasterizer.Line(aco, bco)) { Tiles[m] = 11; }
                }
            }

            //// Render walls
            //foreach (var co in GetCoordinates(31, 31))
            //{
            //    if (Tiles[co] == 11) { continue; } // already a floor
            //    else
            //    {
            //        var south = co + (0, +1);
            //        var west = co + (-1, 0);
            //        var east = co + (+1, 0);

            //        var face = Face.None;
            //        if (Tiles.IsValidCoordinate(south) && Tiles[south] == 11) { face |= Face.South; }
            //        if (Tiles.IsValidCoordinate(east) && Tiles[east] == 11) { face |= Face.East; }
            //        if (Tiles.IsValidCoordinate(west) && Tiles[west] == 11) { face |= Face.West; }
            //        if (co.Y == 30) { face |= Face.South; }

            //        Tiles[co] = 27;

            //        if (face == Face.None) { Tiles[co] = 60; }
            //        else
            //        {
            //            if (face == (Face.South | Face.East)) { Tiles[co] = 83; }
            //            else if (face == (Face.South | Face.West)) { Tiles[co] = 30; }
            //            else if (face == Face.South) { Tiles[co] = 1; }
            //            else { Tiles[co] = 13; }
            //        }
            //    }
            //}
        }

        public Grid<int> Tiles { get; }

        public UndirectedGraph<IntVector> Graph { get; }

        private UndirectedGraph<IntVector> Generate()
        {
            var graph = new UndirectedGraph<IntVector>();

            var coords = GetCoordinates(15, 15).ToArray();
            coords.Randomize();

            // Insert each grid position
            foreach (var co in coords)
            {
                graph.Add(co);
            }

            // Connect edges to each grid position
            foreach (var co in graph.Vertices)
            {
                // Connect to neighbors with random weight
                foreach (var nco in Tiles.GetNeighborCoordinates(co, GridNeighborType.Axis))
                {
                    if (graph.Contains(nco) && !graph.IsConnected(co, nco))
                    {
                        // Add connection with random path weight
                        graph.Connect(co, nco, Calc.Random.NextFloat());
                    }
                }
            }

            return graph.FindMinimumSpanningTree();
        }

        public static IEnumerable<IntVector> GetCoordinates(int w, int h)
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    yield return new IntVector(x, y);
                }
            }
        }

        [Flags]
        private enum Face
        {
            None = 0,

            South = 1 << 1,
            East = 1 << 2,
            West = 1 << 3
        }
    }
}
