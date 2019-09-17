using System.Collections.Generic;
using System.Diagnostics;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.CustomText
{
    internal class Program : GameWindow
    {
        public RichText Text = new RichText("You have found the [Blade of Harmony]!");

        public Program()
            : base("Custom Text Callback")
        {
            ShowFPSOverlay = true;
        }

        private static void Main(string[] args)
        {
            Application.Run(() => new Program());
        }

        protected override void Update(float dt)
        {
            // Nothing To Do
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            ctx.Clear(Color.DarkGray);

            // 
            var position = new Vector(ctx.Surface.Width / 2F, ctx.Surface.Height - 128);
            Text.Draw(ctx, position, Font.Default, 64, TextAlign.Center);
        }

        public class RichText
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
                state.Color = isSpecial ? Color.Violet : Color.White;

                // 
                if (isSpecial)
                {
                    var now = (float) _stopwatch.Elapsed.TotalSeconds;
                    state.Position.Y += Calc.Sin(index / 4F + now * 4F) * 3;
                }
            }

            public void Draw(RenderContext ctx, Vector position, Font font, int size, TextAlign align)
            {
                ctx.DrawText(Text, position, font, size, align, CallbackProcessor);
            }
        }
    }
}
