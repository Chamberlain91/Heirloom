using System;
using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Testing.Performance
{
    internal sealed class PolygonPerformance : PerformanceTest
    {
        private readonly Polygon _star;
        private readonly Polygon _hull;

        public PolygonPerformance()
        {
            _star = new Polygon(GeometryTools.GenerateStar(1));
            _hull = Polygon.CreateConvexHull(GeneratePoints(count: 10));
        }

        private static IEnumerable<Vector> GeneratePoints(int count, int seed = 0)
        {
            var random = new Random(seed);
            for (var i = 0; i < count; i++)
            {
                yield return random.NextVectorDisk();
            }
        }

        [Test]
        public void TriangulateStar()
        {
            foreach (var convex in PolygonTools.TriangulateIndices(_star.Vertices)) { }
        }

        [Test]
        public void TriangulateHull()
        {
            foreach (var convex in PolygonTools.TriangulateIndices(_hull.Vertices)) { }
        }

        [Test]
        public void ConvexStar()
        {
            foreach (var convex in PolygonTools.DecomposeConvexIndices(_star.Vertices)) { }
        }

        [Test]
        public void ConvexHull()
        {
            foreach (var convex in PolygonTools.DecomposeConvexIndices(_hull.Vertices)) { }
        }

        [Test]
        public void HullMetrics()
        {
            PolygonTools.ComputeMetrics(_hull.Vertices, out var area, out var center, out var centroid);
        }

        [Test]
        public void StarMetrics()
        {
            PolygonTools.ComputeMetrics(_star.Vertices, out var area, out var center, out var centroid);
        }
    }
}
