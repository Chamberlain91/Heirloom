using System;
using System.Collections.Generic;

using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    internal static class TextEditor
    {
        private const float BlinkDuration = 0.5F;

        private static bool _needsInitialize;

        private static Rectangle _bounds;

        private static string _text = string.Empty;
        private static int _cursor, _select;

        private static float _blinkTimer;
        private static bool _blink;

        private static readonly List<Rectangle> _selectionRectangles = new();
        private static readonly List<Rectangle> _cells = new();

        public static string Text => _text;

        /// <summary>
        /// A value that determines if some text has been selected.
        /// </summary>
        public static bool HasSelection => _select != _cursor;

        /// <summary>
        /// A collection of rectangle regions to draw text selection.
        /// </summary>
        public static IEnumerable<Rectangle> SelectionRectangles = _selectionRectangles;

        private static int MinSelection => HasSelection ? Calc.Min(_select, _cursor) : _cursor;

        private static int MaxSelection => HasSelection ? Calc.Max(_select, _cursor) : _cursor;

        /// <summary>
        /// The top-left corner of the text cursor.
        /// </summary>
        public static Vector CursorPosition { get; internal set; }

        /// <summary>
        /// Gets a boolean value that determines if the cursor should be drawn.
        /// This is useful for animating a blinking cursor.
        /// </summary>
        public static bool ShowCursor => _blink;

        private static void ResetBlinkTimer()
        {
            _blinkTimer = 0F;
            _blink = true;
        }

        public static void Initialize(string text, Rectangle bounds)
        {
            // Ensure text is never null
            text ??= string.Empty;

            // If the text is different...
            if (_text != text)
            {
                // We will need to initialize
                _needsInitialize = true;

                // Assign new text
                _text = text;
            }

            //
            _bounds = bounds;
            _cells.Clear();
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
            _cells.Add(state.Bounds);
        }

        private static Rectangle GetCell(int i)
        {
            if (_cells.Count == 0)
            {
                // Special zero length text
                return new(_bounds.Position, Size.Zero);
            }
            else
            if (i >= _cells.Count)
            {
                // Special after text cell
                var priorCell = _cells[^1];
                return new(priorCell.TopRight, new Size(0, priorCell.Height));
            }
            else
            {
                // Regular character cell
                return _cells[i];
            }
        }

        public static void ProcessInput(bool allowNewline)
        {
            if (_needsInitialize)
            {
                // We are starting fresh (editor is newly focused) 
                // Clear selection information
                _selectionRectangles.Clear();
                _cursor = _text.Length;
                _select = _cursor;
            }

            // 
            CursorPosition = GetCell(_cursor).Position;

            // Begin selection / place cursor at mouse
            if (Input.IsMousePressed(MouseButton.Left) || _needsInitialize)
            {
                SetCursor(FindMouseCharacterIndex(Input.MousePosition));
                ClearSelection();
            }

            // Mouse is dragging, place cursor at mouse (causing selection boxes)
            if (Input.IsMouseDown(MouseButton.Left))
            {
                SetCursor(FindMouseCharacterIndex(Input.MousePosition));
            }

            if (Input.IsKeyPressed(Key.Backspace, repeat: true))
            {
                DeleteBackwards();
            }
            else if (Input.IsKeyPressed(Key.Delete, repeat: true))
            {
                DeleteForwards();
            }
            else if (allowNewline && Input.IsKeyPressed(Key.Enter, repeat: true))
            {
                Insert("\n");
            }
            else if (Input.IsKeyPressed(Key.Left, repeat: true))
            {
                SetCursor(_cursor - 1);
                ClearSelection();
            }
            else if (Input.IsKeyPressed(Key.Right, repeat: true))
            {
                SetCursor(_cursor + 1);
                ClearSelection();
            }
            else if (Input.IsKeyPressed(Key.Up, repeat: true))
            {
                JumpCursorToPreviousLine();
                ClearSelection();
            }
            else if (Input.IsKeyPressed(Key.Down, repeat: true))
            {
                JumpCursorToNextLine();
                ClearSelection();
            }
            else if (Input.IsKeyPressed(Key.End, repeat: true))
            {
                SetCursor(FindEndOfLine(_cursor));
                ClearSelection();
            }
            else if (Input.IsKeyPressed(Key.Home, repeat: true))
            {
                SetCursor(FindStartOfLine(_cursor));
                ClearSelection();
            }
            else if (Input.IsKeyDown(Key.LeftControl) && Input.IsKeyPressed(Key.A))
            {
                SelectAll();
            }
            else if (Input.TextInput.Length > 0)
            {
                Insert(Input.TextInput);
            }

            // Update selection rects
            UpdateSelectionRects();

            // Mark as no longer needing initialization
            _needsInitialize = false;
        }

        public static void SelectText(int min, int max)
        {
            // Zero sized selection, which means no selection.
            if (min == max) { ClearSelection(); }
            else
            {
                // Assign selection range
                SetCursor(Calc.Max(min, max));
                _select = Calc.Min(min, max);
            }
        }

        private static void SetCursor(int index)
        {
            _cursor = Calc.Clamp(index, 0, _text.Length);
            ResetBlinkTimer();
        }

        private static void SelectAll()
        {
            SelectText(0, _text.Length);
        }

        private static void ClearSelection()
        {
            _select = _cursor;
        }

        private static void UpdateSelectionRects()
        {
            _selectionRectangles.Clear();

            if (HasSelection)
            {
                // Get selection limits
                var minIndex = MinSelection;
                var maxIndex = Calc.Min(MaxSelection, _cells.Count);

                while (true)
                {
                    // Find next end of line (or end of selection)
                    var endIndex = Calc.Min(maxIndex, FindEndOfLine(minIndex));

                    // Append selection rect for line
                    var rectMin = GetCell(minIndex).Position;
                    var rectMax = GetCell(endIndex).BottomLeft;

                    _selectionRectangles.Add(new Rectangle(rectMin, rectMax));

                    // If we have reached the max index, everything is highlighted
                    if (endIndex == maxIndex) { break; }

                    // We have more to select, move min index to new end of line
                    minIndex = endIndex + 1;
                }
            }
        }

        private static void DeleteSelection()
        {
            // 
            var minIndex = Calc.Min(_cursor, _select);
            var maxIndex = Calc.Max(_cursor, _select);

            // Remove selection
            _text = _text.Remove(minIndex, maxIndex - minIndex);

            // Update cursor and selection index
            SetCursor(minIndex);
            _select = _cursor;
        }

        internal static void Insert(string str)
        {
            if (HasSelection) { DeleteSelection(); }

            _text = _text.Insert(_cursor, str);
            SetCursor(_cursor + str.Length);
            _select = _cursor;
        }

        internal static void DeleteBackwards()
        {
            if (HasSelection) { DeleteSelection(); }
            else
            {
                // Remove prior character
                if (_text.Length > 0 && _cursor > 0)
                {
                    _text = _text.Remove(_cursor - 1, 1);
                    SetCursor(_cursor - 1);
                }

                ClearSelection();
            }
        }

        internal static void DeleteForwards()
        {
            if (HasSelection) { DeleteSelection(); }
            else
            {
                // Remove next character
                if (_cursor < _text.Length)
                {
                    _text = _text.Remove(_cursor, 1);
                    ClearSelection();
                }
            }
        }

        private static int FindMouseCharacterIndex(Vector query)
        {
            var bestDist = float.MaxValue;
            var best = -1;

            // Check each character cell to find nearest to mouse
            for (var index = 0; index <= _cells.Count; index++)
            {
                var cell = GetCell(index);
                var xCenter = cell.X;
                var yCenter = cell.Y + (cell.Height / 2F);
                CheckCell(index, xCenter, yCenter);
            }

            //// Special case handling after last character
            //if (_cells.Count > 0)
            //{
            //    var lastCharacter = _cells[^1];
            //    var xCenter = lastCharacter.Right;
            //    var yCenter = lastCharacter.Y + lastCharacter.Height / 2F;
            //    CheckCell(_text.Length, xCenter, yCenter);
            //}

            // Unable to detect cursor position, default to end of string.
            if (best == -1) { best = _text.Length; }

            // Return best found cursor position
            return best;

            void CheckCell(int index, float xCenter, float yCenter)
            {
                var xDist = Calc.Abs(xCenter - query.X);
                var yDist = Calc.Abs(yCenter - query.Y);
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
            if (_cells.Count > 0)
            {
                // Find the end of the current and following line
                var endA = FindEndOfLine(_cursor);
                var endB = FindEndOfLine(endA + 1);

                if (endA != endB)
                {
                    var curr = GetCell(_cursor);
                    var bestDist = float.MaxValue;
                    var best = _cursor;

                    for (var i = endA + 1; i < endB; i++)
                    {
                        var next = GetCell(i);
                        var dist = Calc.Abs(next.X - curr.X);
                        if (dist < bestDist)
                        {
                            bestDist = dist;
                            best = i;
                        }
                    }

                    // Set cursor to new position
                    SetCursor(best);
                }
            }
        }

        private static void JumpCursorToPreviousLine()
        {
            if (_cells.Count > 0)
            {
                // Find the end of the current and following line
                var startA = FindStartOfLine(_cursor);
                var startB = FindStartOfLine(startA - 1);

                // Special case handling, cursor at the end of line
                if (_cursor == _cells.Count) { SetCursor(startA - 1); }
                else
                {
                    if (startA != startB)
                    {
                        var curr = GetCell(_cursor);
                        var bestDist = float.MaxValue;
                        var best = _cursor;

                        for (var i = startB; i < startA - 1; i++)
                        {
                            var next = GetCell(i);
                            var dist = Calc.Abs(next.X - curr.X);
                            if (dist < bestDist)
                            {
                                bestDist = dist;
                                best = i;
                            }
                        }

                        // Set cursor to new position
                        SetCursor(best);
                    }
                }
            }
        }

        private static int FindStartOfLine(int index)
        {
            if (index > 0) // othwerwise its already at start
            {
                index = Calc.Min(_cells.Count - 1, index);
                var curr = GetCell(index);
                for (var i = index - 1; i >= 0; i--)
                {
                    var next = GetCell(i);
                    if (next.Position.Y < curr.Position.Y)
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
            if (index < _cells.Count) // othwerwise its already at end
            {
                var curr = GetCell(index);
                for (var i = index + 1; i < _cells.Count; i++)
                {
                    var next = GetCell(i);
                    if (next.Position.Y > curr.Position.Y)
                    {
                        // We moved down a line
                        return i - 1;
                    }
                }

            }

            // Could not find a visual line break, just go to end of text.
            return _cells.Count;
        }
    }
}
