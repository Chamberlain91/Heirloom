using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// Implementation of 2D collisions/overlap using separating axis theorem.
    /// </summary>
    internal static class SeparatingAxis
    {
        /// <summary>
        /// Determines if a (convex) polygon and a circle are overlapping.
        /// </summary>
        internal static bool Overlaps(IReadOnlyList<Vector> polygon, in Circle circle) // Ω(2n), O(3n)
        {
            // If the circle center is contained
            if (PolygonTools.ContainsPoint(polygon, in circle.Position)) // O(n)
            {
                return true;
            }

            // Get the nearest point on the polygon to the circle
            var nearPoint = PolygonTools.GetClosestPointOutline(polygon, in circle.Position); // Ω(n), O(2n)

            // Compute axis (normalize displacement vector from nearest point to circle)
            var axis = Vector.Normalize(circle.Position - nearPoint);

            // Compute circle projection
            var r1 = circle.Project(in axis); // O(1)

            // Compute polygon projection
            var r2 = PolygonTools.Project(polygon, in axis); // Ω(n), O(n)

            // Do the projections overlap?
            return r1.Overlaps(r2);
        }

        /// <summary>
        /// Determines if a (convex) polygon overlaps another (convex) polygon.
        /// </summary>
        internal static bool Overlaps(IReadOnlyList<Vector> polygonA, IReadOnlyList<Vector> polygonB)
        {
            for (var pass = 0; pass < 2; pass++) // two pass to test each axes set
            {
                // For each edge in polygon A
                for (var i = 0; i < polygonA.Count; i++)
                {
                    var axis = PolygonTools.GetScaledNormal(polygonA, i);

                    var r1 = PolygonTools.Project(polygonA, in axis);
                    var r2 = PolygonTools.Project(polygonB, in axis);

                    if (r1.Overlaps(in r2) == false)
                    {
                        // Found a separating axis
                        return false;
                    }
                }

                // Swap polygons to swap axes set
                Calc.Swap(ref polygonA, ref polygonB);
            }

            // No separation detected, shapes must be touching
            return true;
        }
    }
}
