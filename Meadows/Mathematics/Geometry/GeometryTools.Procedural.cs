using System;
using System.Collections.Generic;

namespace Meadows.Mathematics
{
    public static partial class GeometryTools
    {
        #region Generate Regular Polygon

        /// <summary>
        /// Generates the points of a regular polygon.
        /// </summary>
        public static IEnumerable<Vector> GenerateRegularPolygon(int segments, float radius)
        {
            return GenerateRegularPolygon(Vector.Zero, segments, radius);
        }

        /// <summary>
        /// Generates the points of a regular polygon.
        /// </summary>
        public static IEnumerable<Vector> GenerateRegularPolygon(Vector center, int segments, float radius)
        {
            if (segments < 3) { throw new ArgumentOutOfRangeException(); }

            for (var i = 0; i < segments; i++)
            {
                var a = i / (float) segments * Calc.TwoPi;

                // 
                var x = center.X + Calc.Cos(a) * radius;
                var y = center.Y + Calc.Sin(a) * radius;

                yield return new Vector(x, y);
            }
        }

        #endregion

        #region Generate Star

        /// <summary>
        /// Generates the points of a common five-pointed star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(float radius)
        {
            return GenerateStar(Vector.Zero, radius);
        }

        /// <summary>
        /// Generates the points of a common five-pointed star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(Vector center, float radius)
        {
            return GenerateStar(center, 5, radius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(int numPoints, float radius)
        {
            return GenerateStar(Vector.Zero, numPoints, radius * 0.6F, radius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(Vector center, int numPoints, float radius)
        {
            return GenerateStar(center, numPoints, radius * 0.6F, radius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(int numPoints, float innerRadius, float outerRadius)
        {
            return GenerateStar(Vector.Zero, numPoints, innerRadius, outerRadius);
        }

        /// <summary>
        /// Generates the points of a star.
        /// </summary>
        public static IEnumerable<Vector> GenerateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
        {
            if (numPoints < 2) { throw new ArgumentOutOfRangeException(); }

            numPoints *= 2; // For each point and valley

            for (var i = 0; i < numPoints; i++)
            {
                var a = i / (float) numPoints * Calc.TwoPi;
                var r = i % 2 == 0 ? innerRadius : outerRadius;

                // 
                var x = center.X + Calc.Cos(a) * r;
                var y = center.Y + Calc.Sin(a) * r;

                yield return new Vector(x, y);
            }
        }

        #endregion
    }
}
