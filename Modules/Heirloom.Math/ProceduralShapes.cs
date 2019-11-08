using System.Collections.Generic;

namespace Heirloom.Math
{
    public static class ProceduralShapes
    {
        public static IEnumerable<Vector> GenerateRegularPolygon(Vector center, int segments, float radius)
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

        public static IEnumerable<Vector> GenerateStar(Vector center, int numPoints, float innerRadius, float outerRadius)
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

        public static IEnumerable<Vector> GenerateRectangle(Vector center, float width, float height)
        {
            var w = width / 2F;
            var h = height / 2F;

            yield return (-w + center.X, -h + center.Y);
            yield return (+w + center.X, -h + center.Y);
            yield return (+w + center.X, +h + center.Y);
            yield return (-w + center.X, +h + center.Y);
        }
    }
}
