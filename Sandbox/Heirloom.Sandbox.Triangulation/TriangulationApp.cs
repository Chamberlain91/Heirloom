using System;
using System.Collections.Generic;
using System.Diagnostics;
using Heirloom.Collections;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Sandbox.Triangulation
{
    internal class TriangulationApp : GameWrapper
    {
        public (string, Polygon)[] Polygons;

        private readonly Stopwatch _stopwatch;

        private float _runningAverage = 0;
        private float _runningCount = 0;

        private TriangulationMode _mode = TriangulationMode.EarClip;

        private enum TriangulationMode
        {
            Delaunay,
            EarClip
        }

        public TriangulationApp(GraphicsContext graphics)
            : base(graphics)
        {
            _stopwatch = new Stopwatch();

            // Create Polygons
            Polygons = new[]{
                ("5-star", new Polygon(GeometryTools.GenerateStar((150, 150), 5, 125))),
                ("10-star", new Polygon(GeometryTools.GenerateStar((150, 150), 10, 125))),
                ("c-shape", new Polygon(new[] {
                    new Vector(100, 100),
                    new Vector(200, 100),
                    new Vector(200, 120),
                    new Vector(150, 120),
                    new Vector(150, 180),
                    new Vector(200, 180),
                    new Vector(200, 200),
                    new Vector(100, 200)
                }))
            };
        }

        protected override void Update(float dt)
        {
            Graphics.Clear(Color.DarkGray);

            var (name, polygon) = Polygons[2];

            // 
            var rotate = Matrix.CreateTranslation(150, 150) * Matrix.CreateRotation(dt) * Matrix.CreateTranslation(-150, -150);
            for (var i = 0; i < polygon.Vertices.Count; i++)
            {
                polygon.Vertices[i] = rotate * polygon.Vertices[i];
            }

            // Draw polygon outline
            Graphics.Color = Color.Black;
            Graphics.DrawPolygonOutline(polygon, 15);

            _stopwatch.Restart();

            switch (_mode)
            {
                case TriangulationMode.Delaunay:
                {
                    foreach (var triangle in polygon.Vertices.TriangulatePoints())
                    {
                        Graphics.Color = Color.Cyan;
                        Graphics.DrawTriangleOutline(triangle, 2);
                    }

                    break;
                }

                case TriangulationMode.EarClip:
                {
                    foreach (var triangle in polygon.Triangulate())
                    {
                        Graphics.Color = Color.Yellow;
                        Graphics.DrawTriangleOutline(triangle, 2);
                    }

                    break;
                }
            }

            _stopwatch.Stop();

            // Compute running average
            var elapsed = (float) _stopwatch.Elapsed.TotalSeconds;
            _runningAverage = (_runningAverage * 0.99F) + elapsed;
            _runningCount = (_runningCount * 0.99F) + 1F;

            // Report frame times
            Graphics.Color = Color.LightGray;
            Graphics.DrawText(Time.GetEnglishTime(_runningAverage / _runningCount), (10, 10), Font.Default, 16);

            Graphics.Screen.Refresh();
        }

        private static class Triangulator
        {
            public static IEnumerable<Triangle> Triangulate(Polygon polygon, GraphicsContext gfx)
            {
                // Create min-heap (priority queue)
                var priority = new Heap<Vertex>((a, b) => polygon.Vertices[a.Self].Y.CompareTo(polygon.Vertices[b.Self].Y));
                for (var curr = 0; curr < polygon.Vertices.Count; curr++)
                {
                    var v = polygon.Vertices[curr];

                    // Compute previous and next index
                    var prev = Calc.Wrap(curr - 1, polygon.Vertices.Count);
                    var next = Calc.Wrap(curr + 1, polygon.Vertices.Count);

                    // Insert vertex into priority queue
                    var vertex = new Vertex(prev, curr, next);
                    priority.Add(vertex);
                }

                // Process vertices (top to bottom)
                while (priority.Count > 0)
                {
                    var vertex = priority.Remove();
                    Console.WriteLine(polygon.Vertices[vertex.Self]);
                }

                yield break;
            }

            private enum VertexType
            {
                Normal,
                Start,
                End,
                Split,
                Merge
            }

            private struct Vertex
            {
                public VertexType Type;

                public readonly int Above;

                public readonly int Below;

                public readonly int Self;

                public Vertex(int above, int below, int self) : this()
                {
                    Above = above;
                    Below = below;
                    Self = self;
                }
            }
        }
    }
}
