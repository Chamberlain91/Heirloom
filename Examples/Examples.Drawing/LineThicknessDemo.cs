using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class LineThicknessDemo : Demo
    {
        public LineThicknessDemo()
            : base("Line Thickness")
        { }

        internal override void Draw(RenderContext ctx)
        {
            var min = (Vector) ctx.Surface.Size * 0.25F;
            var max = (Vector) ctx.Surface.Size * 0.75F;

            for (var i = 0; i < 6; i++)
            {
                var t = i / 6F;

                var x = Calc.Lerp(min.X, max.X, t);
                var y = min.Y;

                var w = (max.X - min.X) / 9F;
                var h = max.Y - min.Y;

                var rect = new Rectangle(x, y, w, h);

                // 
                ctx.Color = Color.Yellow;
                ctx.DrawRectOutline(rect, 1 + i);
            }
        }
    }
}
