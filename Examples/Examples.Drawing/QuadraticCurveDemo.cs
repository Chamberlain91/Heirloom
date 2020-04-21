using Heirloom;
using Heirloom.Drawing;

namespace Examples.Drawing
{
    public sealed class QuadraticCurveDemo : Demo
    {
        public QuadraticCurveDemo()
            : base("Quadratic Curve")
        { }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            var a = contentBounds.Min + new Vector(0 / 2F, (Calc.Sin(Time * 1) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var b = contentBounds.Min + new Vector(1 / 2F, (Calc.Sin(Time * 2) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var c = contentBounds.Min + new Vector(2 / 2F, (Calc.Sin(Time * 3) + 1F) * 0.5F) * (Vector) contentBounds.Size;

            // Draw the guide lines
            ctx.Color = Color.Gray;
            ctx.DrawLine(a, b);
            ctx.DrawLine(b, c);

            // Draw the main curve
            ctx.Color = Color.White;
            ctx.DrawCurve(a, b, c, 4);
        }
    }
}
