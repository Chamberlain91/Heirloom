using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Heirloom.Math
{
    public partial class Polygon
    {
        #region Create (Rectangle)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRectangle(Vector center, float width, float height)
        {
            return new Polygon(GetRectanglePoints(center, width, height));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRectangle(float width, float height)
        {
            return CreateRectangle(Vector.Zero, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRectangle(Rectangle rect)
        {
            return CreateRectangle(rect.Center, rect.Width, rect.Height);
        }

        #endregion

        #region Create (Star)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(Vector center, int numPoints, float radius)
        {
            return new Polygon(GetStarPoints(center, numPoints, radius));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateStar(int numPoints, float radius)
        {
            return CreateStar(Vector.Zero, numPoints, radius);
        }

        #endregion

        #region Create (Regular Polygon)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(Vector center, int segments, float radius)
        {
            return new Polygon(GetRegularPolygonPoints(center, segments, radius));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon CreateRegularPolygon(int segments, float radius)
        {
            return CreateRegularPolygon(Vector.Zero, segments, radius);
        }

        #endregion

        #region Enumerate (IEnumerable<Vector>)

        public static IEnumerable<Vector> GetRectanglePoints(Vector center, float width, float height)
        {
            var w = width / 2F;
            var h = height / 2F;

            yield return (-w + center.X, -h + center.Y);
            yield return (+w + center.X, -h + center.Y);
            yield return (+w + center.X, +h + center.Y);
            yield return (-w + center.X, +h + center.Y);
        }

        public static IEnumerable<Vector> GetRegularPolygonPoints(Vector center, int segments, float radius)
        {
            for (var i = 0; i < segments; i++)
            {
                var a = i / (float) segments * Calc.TwoPi;

                // 
                var x = center.X + Calc.Cos(a) * radius;
                var y = center.Y + Calc.Sin(a) * radius;

                yield return new Vector(x, y);
            }
        }

        public static IEnumerable<Vector> GetStarPoints(Vector center, int numPoints, float radius)
        {
            return GetStarPoints(center, numPoints, radius * 0.66F, radius);
        }

        public static IEnumerable<Vector> GetStarPoints(Vector center, int numPoints, float innerRadius, float outerRadius)
        {
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
