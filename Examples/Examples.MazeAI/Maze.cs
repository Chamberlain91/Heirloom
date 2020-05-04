using System;
using System.Collections.Generic;

using Heirloom;
using Heirloom.Collections;

namespace Examples.MazeAI
{
    public class Maze
    {
        public Maze()
        {
            Tiles = new Grid<int>(31, 31);
            Tiles.Clear(-1);

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

                    // Draw floors into tile map
                    foreach (var m in Rasterizer.Line(aco, bco)) { Tiles[m] = 0; }
                }
            }

            // Render walls
            foreach (var co in GetCoordinates(31, 31))
            {
                if (Tiles[co] == 0) { continue; } // already a floor
                else
                {
                    var north = co + (0, -1);
                    var south = co + (0, +1);
                    var west = co + (-1, 0);
                    var east = co + (+1, 0);

                    var face = Face.None;
                    if ((Tiles.IsValidCoordinate(north) && Tiles[north] == 0) || co.Y == 0) { face |= Face.North; }
                    if ((Tiles.IsValidCoordinate(south) && Tiles[south] == 0) || co.Y == 30) { face |= Face.South; }
                    if ((Tiles.IsValidCoordinate(east) && Tiles[east] == 0) || co.X == 30) { face |= Face.East; }
                    if ((Tiles.IsValidCoordinate(west) && Tiles[west] == 0) || co.X == 0) { face |= Face.West; }

                    // Cross Wall
                    if (face == Face.None) { Tiles[co] = 2; }
                    else if (face == Face.North) { Tiles[co] = 2; }
                    // Vertical Wall
                    else if (face.HasFlag(Face.East | Face.West)) { Tiles[co] = face.HasFlag(Face.South) ? 8 : 3; }
                    // Horizontal Wall 
                    else if (face == (Face.South | Face.North | Face.East)) { Tiles[co] = 9; }
                    else if (face == (Face.South | Face.North | Face.West)) { Tiles[co] = 10; }
                    else if (face == (Face.South | Face.North)) { Tiles[co] = 1; }
                    else if (face == Face.South) { Tiles[co] = 1; }
                    // Upper Right Corner
                    else if (face == (Face.East | Face.North)) { Tiles[co] = 4; }
                    else if (face == Face.East) { Tiles[co] = 4; }
                    // Upper Left Corner
                    else if (face == (Face.West | Face.North)) { Tiles[co] = 5; }
                    else if (face == Face.West) { Tiles[co] = 5; }
                    // Bottom Right Corner
                    else if (face == (Face.South | Face.East)) { Tiles[co] = 6; }
                    // Bottom Left Corner
                    else if (face == (Face.South | Face.West)) { Tiles[co] = 7; }
                    // Unable to determine tile to use
                    else { Tiles[co] = -1; }
                }
            }
        }

        public Grid<int> Tiles { get; }

        public UndirectedGraph<IntVector> Graph { get; }

        private UndirectedGraph<IntVector> Generate()
        {
            var graph = new UndirectedGraph<IntVector>();

            var coords = GetCoordinates(15, 15);

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

            North = 1 << 1,
            South = 1 << 2,
            East = 1 << 3,
            West = 1 << 4
        }
    }
}
