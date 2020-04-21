using System.Collections.Generic;
using System.Diagnostics;

using Heirloom;

namespace Examples.Drawing
{
    public sealed class TextCallbackDemo : Demo
    {
        private readonly RichText _richText;

        public TextCallbackDemo()
            : base("Text Callback")
        {
            _richText = new RichText(Files.ReadText("files/alice.txt"));
        }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            ctx.DrawText(_richText.Text, contentBounds, Font.Default, 32, TextAlign.Left, _richText.CharacterCallback);
        }

        private class RichText
        {
            private readonly Stopwatch _stopwatch;
            private readonly State[] _states;

            private enum State { None, Quote, Parens }

            #region Constructor

            public RichText(string text)
            {
                RawText = text;

                _stopwatch = Stopwatch.StartNew();

                // Process Text
                var states = new List<State>();
                var chars = new List<char>();

                var state = State.None;
                for (var i = 0; i < text.Length; i++)
                {
                    var ch = text[i];

                    // Append current character  
                    chars.Add(ch);

                    var beganState = false;

                    // Is this a special state start?
                    if (state == State.None)
                    {
                        switch (ch)
                        {
                            case '\'':
                                state = State.Quote;
                                break;

                            case '(':
                                state = State.Parens;
                                break;
                        }

                        // 
                        beganState = state != State.None;
                    }

                    // Append character state
                    states.Add(state);

                    // Is this a special state end?
                    if (!beganState && state != State.None)
                    {
                        switch (ch)
                        {
                            case '\'':
                                state = State.None;
                                break;

                            case ')':
                                state = State.None;
                                break;
                        }
                    }
                }

                // 
                Text = new string(chars.ToArray());
                _states = states.ToArray();
            }

            #endregion

            public string RawText { get; }

            public string Text { get; }

            internal void CharacterCallback(string _, int index, ref TextDrawState state)
            {
                switch (_states[index])
                {
                    default:
                        state.Color = Color.White;
                        break;

                    case State.Parens:
                        state.Color = Color.Gray;
                        break;

                    case State.Quote:

                        // Set color
                        state.Color = (Color.Blue + Color.Cyan) / 2F;

                        // Animated up and down wobble
                        var now = (float) _stopwatch.Elapsed.TotalSeconds;
                        state.Position.Y += Calc.Sin(index / 2F + now * 8F) * 2;

                        break;
                }
            }
        }
    }
}
