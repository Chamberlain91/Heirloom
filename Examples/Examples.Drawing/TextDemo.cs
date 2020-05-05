using Heirloom;
using Heirloom.IO;

namespace Examples.Drawing
{
    public sealed class TextDemo : Demo
    {
        private readonly string _text;

        public TextDemo()
            : base("Simple Text")
        {
            _text = Files.ReadText("files/alice.txt");
        }

        internal override void Draw(GraphicsContext ctx, Rectangle contentBounds)
        {
            ctx.Color = Color.White;
            ctx.DrawText(_text, contentBounds, Font.Default, 32);
        }
    }
}
