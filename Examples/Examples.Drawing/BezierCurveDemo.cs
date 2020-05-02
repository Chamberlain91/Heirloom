using Heirloom;
using Heirloom.Geometry;

namespace Examples.Drawing
{
    public sealed class BezierCurveDemo : Demo
    {
        public int NUMBER_PETALS = 10;

        public Curve Curve;

        public BezierCurveDemo()
            : base("Bezier Curve")
        {
            // Generates a curve like a flower
            Curve = new Curve();
            for (var i = 0; i <= NUMBER_PETALS; i++)
            {
                var a1 = i / (float) NUMBER_PETALS * Calc.TwoPi;
                var a2 = (i + 1) / (float) NUMBER_PETALS * Calc.TwoPi;
                var p1 = Vector.FromAngle(a1);
                var p2 = Vector.FromAngle(a2);
                Curve.Add(p1 * 170F, p1 * 80, p2 * 80);
            }
        }

        internal override void Draw(Graphics gfx, Rectangle contentBounds)
        {
            gfx.Color = Color.Pink;
            gfx.GlobalTransform = Matrix.CreateTranslation(contentBounds.Center);
            gfx.DrawCurve(Curve, 4);
        }
    }
}
