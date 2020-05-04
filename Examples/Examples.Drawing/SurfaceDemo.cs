using Heirloom;

namespace Examples.Drawing
{
    public sealed class SurfaceDemo : Demo
    {
        public Surface LowResSurface;

        public SurfaceDemo()
            : base("Surface")
        {
            LowResSurface = new Surface(350, 200);
        }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            // == Draw curve to low res surface

            ctx.Surface = LowResSurface;
            ctx.Clear(Color.Transparent);

            var a = new Vector(0 / 3F, (Calc.Sin(Time * 1) + 1F) * 0.5F) * (Vector) LowResSurface.Size;
            var b = new Vector(1 / 3F, (Calc.Sin(Time * 2) + 1F) * 0.5F) * (Vector) LowResSurface.Size;
            var c = new Vector(2 / 3F, (Calc.Sin(Time * 3) + 1F) * 0.5F) * (Vector) LowResSurface.Size;
            var d = new Vector(3 / 3F, (Calc.Sin(Time * 4) + 1F) * 0.5F) * (Vector) LowResSurface.Size;

            // Draw the guide lines
            ctx.Color = Color.Gray;
            ctx.DrawLine(a, b);
            ctx.DrawLine(b, c);
            ctx.DrawLine(c, d);

            // Draw the main curve
            ctx.Color = Color.White;
            ctx.DrawCurve(a, b, c, d, 4);

            // == Draw surface centered within content bounds

            var newHeight = contentBounds.Height;
            var newWidth = ctx.Surface.Width * (newHeight / ctx.Surface.Height);
            var offset = (contentBounds.Width - newWidth) / 2F;

            var rect = new Rectangle(contentBounds.X + offset, contentBounds.Y, newWidth, newHeight);

            ctx.Surface = ctx.Screen.Surface;
            ctx.DrawImage(LowResSurface, rect);
        }
    }
}
