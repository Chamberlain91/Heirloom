using System;
using System.Collections.Generic;

using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    internal static class TextEditor
    {
        private const float BlinkDuration = 0.5F;

        private static Rectangle _bounds;
        private static string _text = string.Empty;
        private static int _cursor;

        private static float _blinkTimer;
        private static bool _blink;

        private static readonly List<CharacterCell> _characters = new();

        public static bool ShowCursor => _blink;

        public static string Text => _text;

        private static int CursorIndex
        {
            get => _cursor;

            set
            {
                _cursor = value;
                _cursor = Calc.Clamp(_cursor, 0, _text.Length);
            }
        }

        public static Vector CursorPosition { get; internal set; }

        private struct CharacterCell
        {
            public Rectangle Bounds;
            public char Character;
            public int Index;
        }

        public static void Initialize(string text, Rectangle bounds)
        {
            _bounds = bounds;

            text ??= string.Empty;

            if (_text != text)
            {
                CursorIndex = text.Length;
                _text = text;
            }

            // 
            _characters.Clear();
        }

        private static void ResetBlinkTimer()
        {
            _blinkTimer = 0F;
            _blink = true;
        }

        public static void Update(float dt)
        {
            // 
            _blinkTimer += dt;
            while (_blinkTimer > BlinkDuration)
            {
                _blinkTimer -= BlinkDuration;
                _blink = !_blink;
            }
        }

        public static void CharacterCallback(string text, int i, ref TextRendererState state)
        {
            _characters.Add(new CharacterCell
            {
                Bounds = state.Bounds,
                Character = text[i],
                Index = i
            });

            if (i == CursorIndex)
            {
                CursorPosition = state.Bounds.TopLeft;
            }
        }

        internal static void ProcessInput(bool allowNewline)
        {
            // todo: ButtonState.Repeat

            // Special case handling, end of text cursor
            if (CursorIndex == Text.Length && _characters.Count > 0)
            {
                CursorPosition = _characters[^1].Bounds.TopRight;
            }

            // No text, set cursor to top-left of bounds
            if (_characters.Count == 0) { CursorPosition = _bounds.Position; }

            // 
            if (Input.IsMouseDown(MouseButton.Left))
            {
                ResetBlinkTimer();
                CursorIndex = JumpCursorToMouse();
            }

            if (Input.IsKeyPressed(Key.Backspace, repeat: true))
            {
                ResetBlinkTimer();
                DeleteBackwards();
            }
            else if (Input.IsKeyPressed(Key.Delete, repeat: true))
            {
                ResetBlinkTimer();
                DeleteForwards();
            }
            else if (allowNewline && Input.IsKeyPressed(Key.Enter, repeat: true))
            {
                ResetBlinkTimer();
                Insert("\n");
            }
            else if (Input.IsKeyPressed(Key.Left, repeat: true))
            {
                ResetBlinkTimer();
                CursorIndex--;
            }
            else if (Input.IsKeyPressed(Key.Right, repeat: true))
            {
                ResetBlinkTimer();
                CursorIndex++;
            }
            else if (Input.IsKeyPressed(Key.Up, repeat: true))
            {
                JumpCursorToPreviousLine();
                ResetBlinkTimer();
            }
            else if (Input.IsKeyPressed(Key.Down, repeat: true))
            {
                JumpCursorToNextLine();
                ResetBlinkTimer();
            }
            else if (Input.IsKeyPressed(Key.End, repeat: true))
            {
                CursorIndex = FindEndOfLine(CursorIndex);
                ResetBlinkTimer();
            }
            else if (Input.IsKeyPressed(Key.Home, repeat: true))
            {
                CursorIndex = FindStartOfLine(CursorIndex);
                ResetBlinkTimer();
            }
            else if (Input.TextInput.Length > 0)
            {
                Insert(Input.TextInput);
                ResetBlinkTimer();
            }
        }

        private static int JumpCursorToMouse()
        {
            var bestDist = float.MaxValue;
            var best = -1;

            // Check each character cell to find nearest to mouse
            foreach (var cell in _characters)
            {
                var xCenter = cell.Bounds.X;
                var yCenter = cell.Bounds.Y + (cell.Bounds.Height / 2F);
                CheckCell(cell.Index, xCenter, yCenter);
            }

            // Special case handling after last character
            if (_characters.Count > 0)
            {
                var lastCharacter = _characters[^1];
                var xCenter = lastCharacter.Bounds.Right;
                var yCenter = lastCharacter.Bounds.Y + lastCharacter.Bounds.Height / 2F;
                CheckCell(_text.Length, xCenter, yCenter);
            }

            // Unable to detect cursor position, default to end of string.
            if (best == -1) { best = _text.Length; }

            // Return best found cursor position
            return best;

            void CheckCell(int index, float xCenter, float yCenter)
            {
                var xDist = Calc.Abs(xCenter - Input.MousePosition.X);
                var yDist = Calc.Abs(yCenter - Input.MousePosition.Y);
                var dist = xDist + yDist * 2;

                if (dist <= bestDist)
                {
                    bestDist = dist;
                    best = index;
                }
            }
        }

        private static void JumpCursorToNextLine()
        {
            if (_characters.Count > 0)
            {
                // Find the end of the current and following line
                var endA = FindEndOfLine(CursorIndex);
                var endB = FindEndOfLine(endA + 1);

                Console.WriteLine(endA + " " + endB);

                if (endA != endB)
                {
                    var curr = _characters[CursorIndex];
                    var bestDist = float.MaxValue;
                    var best = CursorIndex;

                    for (var i = endA + 1; i < endB; i++)
                    {
                        var next = _characters[i];
                        var dist = Calc.Abs(next.Bounds.X - curr.Bounds.X);
                        if (dist < bestDist)
                        {
                            bestDist = dist;
                            best = i;
                        }
                    }

                    // Set cursor to new position
                    CursorIndex = best;
                }
            }
        }

        private static void JumpCursorToPreviousLine()
        {
            if (_characters.Count > 0)
            {
                // Find the end of the current and following line
                var startA = FindStartOfLine(CursorIndex);
                var startB = FindStartOfLine(startA - 1);

                // Special case handling, cursor at the end of line
                if (CursorIndex == _characters.Count) { CursorIndex = startA - 1; }
                else
                {
                    if (startA != startB)
                    {
                        var curr = _characters[CursorIndex];
                        var bestDist = float.MaxValue;
                        var best = CursorIndex;

                        for (var i = startB; i < startA - 1; i++)
                        {
                            var next = _characters[i];
                            var dist = Calc.Abs(next.Bounds.X - curr.Bounds.X);
                            if (dist < bestDist)
                            {
                                bestDist = dist;
                                best = i;
                            }
                        }

                        // Set cursor to new position
                        CursorIndex = best;
                    }
                }
            }
        }

        private static int FindStartOfLine(int index)
        {
            if (index > 0) // othwerwise its already at start
            {
                index = Calc.Min(_text.Length - 1, index);
                var curr = _characters[index];
                for (var i = index - 1; i >= 0; i--)
                {
                    var next = _characters[i];
                    if (next.Bounds.Position.Y < curr.Bounds.Position.Y)
                    {
                        // We moved down a line
                        return i + 1;
                    }
                }

            }

            // Could not find a visual line break, just go to start of text.
            return 0;
        }

        private static int FindEndOfLine(int index)
        {
            if (index < _text.Length) // othwerwise its already at end
            {
                var curr = _characters[index];
                for (var i = index + 1; i < _text.Length; i++)
                {
                    var next = _characters[i];
                    if (next.Bounds.Position.Y > curr.Bounds.Position.Y)
                    {
                        // We moved down a line
                        return i - 1;
                    }
                }

            }

            // Could not find a visual line break, just go to end of text.
            return _text.Length;
        }

        internal static void Insert(string str)
        {
            _text = _text.Insert(CursorIndex, str);
            CursorIndex += str.Length;
        }

        internal static void DeleteBackwards()
        {
            if (_text.Length > 0 && CursorIndex > 0)
            {
                _text = _text.Remove(CursorIndex - 1, 1);
                CursorIndex--;
            }
        }

        internal static void DeleteForwards()
        {
            if (CursorIndex < _text.Length)
            {
                _text = _text.Remove(CursorIndex, 1);
            }
        }
    }
}
