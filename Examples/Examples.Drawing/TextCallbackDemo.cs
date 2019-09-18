using System.Collections.Generic;
using System.Diagnostics;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class TextCallbackDemo : Demo
    {
        private RichText _richText = new RichText("You have found the [Blade of Harmony], you lucky adventurer!");

        public TextCallbackDemo()
            : base("Text Callback")
        { }

        internal override void Draw(RenderContext ctx)
        {
            // todo: Implement feature, should be able to vertical align text without this step.
            var align = new Vector(0, Font.Default.MeasureText(_richText.Text, 32).Height / 2F);
            _richText.Draw(ctx, -align + ((Vector) ctx.Surface.Size) * 0.5F, Font.Default, 32, TextAlign.Center);
        }

        private class RichText
        {
            private readonly Stopwatch _stopwatch;
            private readonly bool[] _states;

            public RichText(string text)
            {
                RawText = text;

                _stopwatch = Stopwatch.StartNew();

                // Process Text
                var states = new List<bool>();
                var chars = new List<char>();

                var isSpecial = false;
                for (var i = 0; i < text.Length; i++)
                {
                    var ch = text[i];

                    // Found special bound
                    if (ch == '[') { isSpecial = true; continue; }
                    if (ch == ']') { isSpecial = false; continue; }

                    // Append current character and state
                    states.Add(isSpecial);
                    chars.Add(ch);
                }

                // 
                Text = new string(chars.ToArray());
                _states = states.ToArray();
            }

            public string RawText { get; }

            public string Text { get; }

            internal void CallbackProcessor(string text, int index, ref CharacterRenderState state)
            {
                ref var isSpecial = ref _states[index];
                state.Color = isSpecial ? Color.Pink : Color.Yellow;

                // 
                if (isSpecial)
                {
                    var now = (float) _stopwatch.Elapsed.TotalSeconds;
                    state.Position.Y += Calc.Sin(index / 2F + now * 8F) * 2;
                }
            }

            public void Draw(RenderContext ctx, Vector position, Font font, int size, TextAlign align)
            {
                ctx.DrawText(Text, position, font, size, align, CallbackProcessor);
            }
        }
    }
}
