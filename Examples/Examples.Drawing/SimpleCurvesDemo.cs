using Heirloom;

namespace Examples.Drawing
{
    public sealed class SimpleCurvesDemo : Demo
    {
        public SimpleCurvesDemo()
            : base("Simple Curves")
        { }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            var a = contentBounds.Min + new Vector(0 / 3F, (Calc.Sin(Time * 1 / 2) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var b = contentBounds.Min + new Vector(1 / 3F, (Calc.Sin(Time * 2 / 2) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var c = contentBounds.Min + new Vector(2 / 3F, (Calc.Sin(Time * 3 / 2) + 1F) * 0.5F) * (Vector) contentBounds.Size;
            var d = contentBounds.Min + new Vector(3 / 3F, (Calc.Sin(Time * 4 / 2) + 1F) * 0.5F) * (Vector) contentBounds.Size;

            // Draw the guide lines
            ctx.Color = Color.Gray;
            ctx.DrawLine(a, b);
            ctx.DrawLine(b, c);
            ctx.DrawLine(c, d);

            // Draw two quadratic curves
            ctx.Color = Color.Cyan;
            ctx.DrawCurve(a, b, c);
            ctx.Color = Color.Orange;
            ctx.DrawCurve(b, c, d);

            // Draw cubic curve
            ctx.Color = Color.White;
            ctx.DrawCurve(a, b, c, d);
        }
    }
}
