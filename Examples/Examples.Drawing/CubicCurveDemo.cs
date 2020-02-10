using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class CubicCurveDemo : Demo
    {
        public CubicCurveDemo()
            : base("Cubic Curve")
        { }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            var a = contentBounds.Min + new Vector(0 / 3F, (Calc.Sin(Time * 1) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var b = contentBounds.Min + new Vector(1 / 3F, (Calc.Sin(Time * 2) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var c = contentBounds.Min + new Vector(2 / 3F, (Calc.Sin(Time * 3) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var d = contentBounds.Min + new Vector(3 / 3F, (Calc.Sin(Time * 4) + 1F) * 0.5F) * (Vector) contentBounds.Size;

            // Draw the guide lines
            ctx.Color = Color.Gray;
            ctx.DrawLine(a, b);
            ctx.DrawLine(b, c);
            ctx.DrawLine(c, d);

            // Draw the main curve
            ctx.Color = Color.White;
            ctx.DrawCurve(a, b, c, d, 4);
        }
    }
}
