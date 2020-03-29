using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class LineThicknessDemo : Demo
    {
        public LineThicknessDemo()
            : base("Line Thickness")
        { }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            var min = contentBounds.Min;
            var max = contentBounds.Max;

            for (var i = 0; i < 8; i++)
            {
                var t = i / 8F;

                var x = Calc.Lerp(min.X, max.X + 16, t);
                var y = min.Y;

                var w = (max.X - min.X) / 8F - 16;
                var h = max.Y - min.Y;

                var rect = new Rectangle(x, y, w, h);

                // 
                ctx.Color = Color.White;
                ctx.DrawRectOutline(rect, 1 + t * 7);
            }
        }
    }
}
