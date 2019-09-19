using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class CubicCurveDemo : Demo
    {
        public CubicCurveDemo()
            : base("Cubic Curve")
        { }

        internal override void Draw(RenderContext ctx)
        {
            var s = (Vector) ctx.Surface.Size;

            var a = new Vector(1 / 5F, 0.2F + (Calc.Sin(Time * 1) + 1F) * 0.3F) * s;
            var b = new Vector(2 / 5F, 0.2F + (Calc.Sin(Time * 2) + 1F) * 0.3F) * s;
            var c = new Vector(3 / 5F, 0.2F + (Calc.Sin(Time * 3) + 1F) * 0.3F) * s;
            var d = new Vector(4 / 5F, 0.2F + (Calc.Sin(Time * 4) + 1F) * 0.3F) * s;

            // 
            ctx.Color = Color.Yellow;
            ctx.DrawCurve(a, b, c, d, 2);
        }
    }
}
