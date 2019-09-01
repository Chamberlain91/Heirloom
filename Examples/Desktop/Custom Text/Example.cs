using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.CustomText
{
    public class Example : GameWindow
    {
        private const string ExampleText = "This is an [example] of rendering text with {custom processing}.\nYou can even <change characters>.";

        private enum WordMode { Normal, Keyword, Special, Galatic }

        private WordMode _wordMode = WordMode.Normal;

        public Example()
            : base("Custom Text")
        {
            // Measure the size of the text laid out to fit in a 512, 512 box.
            var size = (IntSize) Font.Default.MeasureText(ExampleText, (320, 600), 32);

            // Set window to fit that size with padding
            Size = (IntSize) ((size + (32, 32)) / ContentScale.X);
        }

        protected override void Update()
        {
            // Do Nothing
        }

        protected override void Render(RenderContext context)
        {
            var surface = context.Surface;

            // Compute the text layout box.
            var rect = new Rectangle(16, 16, surface.Width - 32, surface.Height - 32);

            // Clear the surface.
            context.Clear(Color.DarkGray);

            // Draw a backing rectangle around the text layout box.
            context.DrawRect(rect.Inflate(8), Color.White);

            // Draw the text with callback to customize the gyphs drawn.
            context.DrawText(ExampleText, rect, TextAlign.Left, Font.Default, 32, Color.Black, CustomTextProcessor);
        }

        private void CustomTextProcessor(string text, int index, ref CharacterRenderState state)
        {
            // First character, Reset parse machine
            if (index == 0) { _wordMode = WordMode.Normal; }

            // Fairly simplistic example of using a text callback to modify text.
            // If you wanted to process a markup language to color text, it should be 
            // done before to strip markup from the rendering string and have the callback
            // read a datastructure to know what modifications to make.
            if (state.Character == '[') { _wordMode = WordMode.Keyword; return; }
            if (state.Character == '{') { _wordMode = WordMode.Special; return; }
            if (state.Character == '<') { _wordMode = WordMode.Galatic; return; }

            // Retun to normal
            if (state.Character == ']' || state.Character == '}' || state.Character == '>')
            {
                _wordMode = WordMode.Normal;
                return;
            }

            // Apply modifications
            switch (_wordMode)
            {
                default:
                    state.Color = Color.Black;
                    break;

                case WordMode.Keyword:
                    state.Color = Colors.FlatUI.Nephritis;
                    break;

                case WordMode.Special:
                    state.Position.Y += Calc.Sin(index / 2F + Time * 8F) * 3;
                    state.Color = Colors.FlatUI.All[(int) (index + 10 * Time) % Colors.FlatUI.All.Length];
                    break;

                case WordMode.Galatic:
                    // TODO: Provide a common subset of the char type static functions, ie UnicodeCharacter.IsWhiteSpace
                    // so a cast isn't needed
                    if (!char.IsWhiteSpace((char) state.Character))
                    {
                        state.Position.Y += Calc.Random.NextFloat(-2, +2);
                        state.Position.X += Calc.Random.NextFloat(-1, +1);
                        state.Character = (char) ('A' + Calc.Random.Next('Z' - 'A'));
                        state.Color = Colors.FlatUI.Alizarin;
                    }
                    break;
            }
        }

        private static void Main(string[] args)
        {
            Run(new Example());
        }
    }
}
