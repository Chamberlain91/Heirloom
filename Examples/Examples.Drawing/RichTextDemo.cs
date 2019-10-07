using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class RichTextDemo : Demo
    {
        private readonly RichText _richText;

        public RichTextDemo()
            : base("Standard Rich Text")
        {
            var parser = new ExampleParser();
            _richText = parser.Parse(Files.ReadText("files/example.txt"));
        }

        internal override void Draw(RenderContext ctx, Rectangle contentBounds)
        {
            ctx.DrawText(_richText, contentBounds, Font.Default, 32);
        }

        private sealed class ExampleParser : StandardRichTextParser
        {
            public ExampleParser()
            {
                AddKeyword("i", ItalicsCallback);
            }

            private void ItalicsCallback(string text, int index, ref CharacterDrawState state)
            {
                // Fake italics by shearing glyph
                state.Transform = Matrix.CreateShear(-0.4F, 0);
            }
        }
    }
}
