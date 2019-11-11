using System;
using System.Collections.Generic;
using System.Text;

namespace Heirloom.Math
{
    internal static class PolygonCollision
    {
        internal static bool Overlaps(in Circle circle, IReadOnlyList<Vector> polygon)
        {
            for (var i = 0; i < polygon.Count; i++)
            {
                // Get ith edge of the polygon
                var edgeA = PolygonTools.GetVertex(polygon, i + 0);
                var edgeB = PolygonTools.GetVertex(polygon, i + 1);
                var edge = new LineSegment(edgeA, edgeB);

                // Get the nearest point
                var nearest = edge.GetClosestPoint(circle.Position);

                // Compute the distance to the edge
                var distance = Vector.DistanceSquared(nearest, circle.Position);
            }
        }

        internal static bool Overlaps(IReadOnlyList<Vector> polygonA, IReadOnlyList<Vector> polygonB)
        {
            throw new NotImplementedException();
        }
    }
}
