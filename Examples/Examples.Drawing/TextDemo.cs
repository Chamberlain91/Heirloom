using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class TextDemo : Demo
    {
        private string _text;

        public TextDemo()
            : base("Simple Text")
        {
            _text = Files.ReadText("files/alice.txt");
        }

        internal override void Draw(RenderContext ctx, Rectangle contentBounds)
        {
            ctx.DrawText(_text, contentBounds, Font.Default, 32, TextAlign.Left);
        }
    }
}
