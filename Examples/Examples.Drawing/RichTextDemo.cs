using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class RichTextDemo : Demo
    {
        private readonly StyledText _richText;

        public RichTextDemo()
            : base("Standard Rich Text")
        {
            var parser = new ExampleParser();
            _richText = parser.Parse(Files.ReadText("files/example.txt"));
        }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            ctx.DrawText(_richText, contentBounds, Font.Default, 32);
        }

        private sealed class ExampleParser : StandardStyledTextParser
        {
            public ExampleParser()
            {
                AddKeyword("i", ItalicsCallback);
            }

            private void ItalicsCallback(string text, int index, ref CharacterDrawState state)
            {
                state.Color = Color.Pink;

                // Fake italics by shearing glyph
                state.Transform = Matrix.CreateShear(-0.3F, 0)
                                * Matrix.CreateScale(1.1F, 1F);
            }
        }
    }
}
