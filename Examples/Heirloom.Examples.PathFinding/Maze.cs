
using Heirloom.Collections;
using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Examples.PathFinding
{
    public sealed class Maze
    {
        public Maze(int width, int height)
        {
            Graph = new Graph<IntVector, float>(directed: false, 1F);
            Grid = new Grid<bool>(width, height);

            // Generate stage
            foreach (var co in Rasterizer.Rectangle(Grid.Size))
            {
                Grid[co] = Calc.Simplex.Sample((Vector) co / 5F) < 0.1F;
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
            // Graph = Graph.GetComponents().FindMaximal(graph => graph.Vertices.Count);

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
