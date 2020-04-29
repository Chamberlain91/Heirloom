using Heirloom;
using Heirloom.IO;

namespace Examples.Drawing
{
    public sealed class RichTextDemo : Demo
    {
        private readonly StyledText _richText;

        public RichTextDemo()
            : base("Standard Rich Text")
        {
            // Construct a parser object
            var parser = new ExampleParser(this);

            // Creates styled text by parsing some example text
            _richText = parser.Parse(Files.ReadText("files/example.txt"));
        }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            ctx.DrawText(_richText, contentBounds, Font.Default, 32);
        }

        private sealed class ExampleParser : StandardStyledTextParser
        {
            private readonly RichTextDemo _demo;

            public ExampleParser(RichTextDemo demo)
            {
                _demo = demo;

                AddKeyword("i", ItalicsCallback);
                AddKeyword("fire", FireCallback);
                AddKeyword("o", OtherCallback);
            }

            private void ItalicsCallback(string text, int index, ref TextDrawState state)
            {
                state.Color = Color.Pink;

                // Fake italics by shearing glyph
                state.Transform = Matrix.CreateShear(-0.3F, 0)
                                * Matrix.CreateScale(1.1F, 1F);
            }

            private void OtherCallback(string text, int index, ref TextDrawState state)
            {
                state.Color = Color.Cyan;
            }

            private void FireCallback(string text, int index, ref TextDrawState state)
            {
                var s = Calc.Sin(_demo.Time * 6F + index) * 0.5F + 0.5F;
                state.Color = Color.Lerp(Color.Orange, Color.Red, s);
                if (Calc.Random.NextFloat() < 0.02F) { state.Color = Color.White; }
            }
        }
    }
}
