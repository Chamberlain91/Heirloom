using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class QuadraticCurveDemo : Demo
    {
        public QuadraticCurveDemo()
            : base("Quadratic Curve")
        { }

        internal override void Draw(RenderContext ctx)
        {
            var s = (Vector) ctx.Surface.Size;

            var a = new Vector(1 / 4F, 0.2F + (Calc.Sin(Time * 1) + 1F) * 0.3F) * s;
            var b = new Vector(2 / 4F, 0.2F + (Calc.Sin(Time * 2) + 1F) * 0.3F) * s;
            var c = new Vector(3 / 4F, 0.2F + (Calc.Sin(Time * 3) + 1F) * 0.3F) * s;

            // 
            ctx.Color = Color.Yellow;
            ctx.DrawCurve(a, b, c, 2);
        }
    }
}
