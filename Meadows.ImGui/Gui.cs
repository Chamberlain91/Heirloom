using System;
using System.Collections.Generic;

using Meadows.Drawing;
using Meadows.Mathematics;

using Microsoft.VisualBasic;

namespace Meadows.UI
{
    public static class Gui
    {
        private const int BasicElementHeight = 23;
        private const int BasicElementSpace = 3;

        private const int BasicGroupSpace = BasicElementSpace * 1;

        private const int TextPaddingX = BasicElementSpace * 2;
        private const int TextPaddingY = BasicElementSpace;

        private const int ButtonPaddingX = BasicElementSpace;
        private const int ButtonPaddingY = BasicElementSpace;

        private const int PanelPaddingX = BasicGroupSpace * 4;
        private const int PanelPaddingY = BasicGroupSpace * 4;

        private const int SliderHandleSize = 3; // 7 wide

        private static IntRectangle _layoutBox;
        private static int _y;

        public static GraphicsContext Graphics { get; private set; }

        public static GuiTheme Theme { get; set; } = GuiTheme.Default;

        public static void BeginFrame(GraphicsContext graphics, float dt)
        {
            Graphics = graphics;

            // Update mouse state
            Mouse.Position = Input.MousePosition;
            Mouse.Button = Input.GetButton(MouseButton.Left);

            // Assign new active element 
            Element.ActiveElement = Element.NextActiveElement;

            // If we click, the active element will now change. This might change
            // into the same element but thats still effectively like changing elements.
            if (Mouse.IsPressed) { Element.NextActiveElement = 0; }

            // 
            TextEditor.Update(dt);
        }

        public static void SetLayoutBox(IntRectangle layoutBox)
        {
            // Render panel
            PrependPanel(ref layoutBox);

            // 
            _layoutBox = layoutBox;
            _y = layoutBox.Top;
        }

        private static IntRectangle GetNextLayoutBox(int height = BasicElementHeight)
        {
            var box = new IntRectangle(_layoutBox.X, _y, _layoutBox.Width, height);
            _y += height + BasicElementSpace;

            return box;
        }

        private static void BeginElement(string title)
        {
            Element.CurrentElement = HashCode.Combine(title);
        }

        #region Element Construction

        public static void PrependText(string text, ref IntRectangle bounds, bool centerAlign = false)
        {
            var textAlign = TextAlign.Left;
            if (centerAlign) { textAlign = TextAlign.Center; }

            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds?
            Graphics.Color = Theme.TextColor;
            var measure = Graphics.DrawText(text, bounds, Theme.Font, Theme.FontSize, textAlign | TextAlign.Middle);
            ShrinkLeft(ref bounds, TextPaddingX + (int) measure.Width);
        }

        public static void PrependIcon(Texture texture, ref IntRectangle bounds)
        {
            var aspect = texture.Size.Aspect;

            // Fit image into bounds
            var box = bounds;
            box.Width = (int) (bounds.Height * aspect);
            ShrinkLeft(ref bounds, TextPaddingX + box.Width);

            // Draw label
            Graphics.Color = Color.White;
            Graphics.DrawImage(texture, box);
        }

        public static void PrependFrame(Color baseColor, Color borderColor, ref IntRectangle bounds)
        {
            Graphics.Color = baseColor;
            Graphics.DrawRect(bounds);

            Graphics.Color = borderColor;
            Graphics.DrawRectOutline(bounds);

            Shrink(ref bounds, 1);
            bounds.Height -= 1; // todo: fix this fudge factor
        }

        public static void PrependPanel(ref IntRectangle bounds)
        {
            Graphics.Color = Theme.Background;
            Graphics.DrawRect(bounds);

            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRectOutline(bounds);

            Shrink(ref bounds, PanelPaddingX, PanelPaddingY);
        }

        #endregion

        #region Non-Interactive Elements

        public static void Space()
        {
            _y += BasicGroupSpace;
        }

        public static void Label(string text)
        {
            var bounds = GetNextLayoutBox();

            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds.
            Graphics.Color = Theme.TextColor;
            Graphics.DrawText(text, bounds, Theme.Font, Theme.FontSize, TextAlign.Left | TextAlign.Middle);
        }

        public static void Tooltip(string text, IntVector position)
        {
            var bounds = (IntRectangle) TextLayout.Measure(text, Theme.Font, Theme.FontSize);
            bounds.Position = position - (IntVector) bounds.Size;

            var backgroundBounds = IntRectangle.Inflate(bounds, TextPaddingX, TextPaddingY);
            PrependFrame(Theme.BaseColor, Theme.BorderColor, ref backgroundBounds);
            PrependText(text, ref bounds, true);
        }

        #endregion

        #region Interactive Elements

        public static bool Button(string title, Texture icon = null)
        {
            BeginElement(title);

            var bounds = GetNextLayoutBox();

            var isMouseOver = bounds.Contains(Mouse.Position);
            if (isMouseOver && Mouse.IsPressed)
            {
                Element.SetElementActive();
            }

            var isActiveMouseOver = isMouseOver && Element.IsActiveElement;

            // Draw button body
            var buttonColor = (isActiveMouseOver && Mouse.IsDown) ? Theme.ActiveColor : (isMouseOver ? Theme.HoverColor : Theme.BaseColor);
            var borderColor = Element.IsActiveElement ? Theme.FocusColor : Theme.BorderColor;
            PrependFrame(buttonColor, borderColor, ref bounds);
            Graphics.DrawLine(bounds.BottomLeft, bounds.BottomRight);

            // Collapse bounds to give padding to the button text
            Shrink(ref bounds, ButtonPaddingX, ButtonPaddingY);

            if (icon != null)
            {
                // Prepend icon
                PrependIcon(icon, ref bounds);
            }

            // Draw button text
            PrependText(title, ref bounds);

            return isActiveMouseOver && Mouse.IsReleased;
        }

        public static bool Slider(string title, ref float value, float min = 0F, float max = 100F, float step = 1F)
        {
            BeginElement(title);

            var bounds = GetNextLayoutBox();

            // Draws text label
            PrependText(title, ref bounds);

            // Determine if the mouse is hovering this element.
            // If clicked on, store id and determine if selected.
            var isHoverSliderFrame = bounds.Contains(Mouse.Position);
            if (isHoverSliderFrame && Mouse.IsPressed) { Element.SetElementActive(); }

            // Draw slider frame
            var sliderColor = isHoverSliderFrame ? Theme.HoverColor : Theme.BaseColor;
            var borderColor = Element.IsActiveElement ? Theme.FocusColor : Theme.BorderColor;
            PrependFrame(sliderColor, borderColor, ref bounds);

            // Copy the bounds for drawing the active slider value
            var valueRect = bounds;

            // Shrink the bounds to prevent the handle from escaping the slider frame
            ShrinkHorizontal(ref bounds, SliderHandleSize);

            // Compute where in the slider the current value is
            var valueBetween = Calc.Clamp(Calc.Between(value, min, max), 0F, 1F);
            var sliderPosition = (int) Calc.Lerp(bounds.Left, bounds.Right, valueBetween);

            // Compute the width of the slider value
            valueRect.Width = sliderPosition - bounds.Left;

            // Draw the active value representation
            Graphics.Color = Theme.ActiveColor;
            Graphics.DrawRect(valueRect);

            // Compute the handle box
            var handleRect = bounds;
            handleRect.X = sliderPosition - SliderHandleSize;
            handleRect.Width = (SliderHandleSize * 2) + 1;

            // Draw the slider handle
            Graphics.Color = Theme.TextColor;
            Graphics.DrawRect(handleRect);

            // 
            var isHoverHandle = handleRect.Contains(Mouse.Position);
            var isInteracting = Element.IsActiveElement && Mouse.IsDown && isHoverSliderFrame;

            var isModified = false;

            if (isInteracting || isHoverHandle)
            {
                if (isInteracting)
                {
                    // Slider value has changed
                    valueBetween = Calc.Clamp(Calc.Between(Mouse.Position.X, bounds.Left, bounds.Right), 0F, 1F);

                    var newValue = Calc.Lerp(min, max, valueBetween);
                    newValue = Calc.Round(newValue / step) * step;

                    isModified = !Calc.NearEquals(newValue, value);
                    value = newValue;
                }

                // Display tooltip
                Tooltip($"{value}", handleRect.TopLeft);
            }

            return isModified;
        }

        public static bool StringInput(string title, ref string text)
        {
            BeginElement(title);

            // Returns the next layout slice to layout element within
            var bounds = GetNextLayoutBox();

            // Prepend title text
            PrependText(title, ref bounds, false);

            // If mouse has clicked on this box
            var isHovering = bounds.Contains(Mouse.Position);
            if (isHovering && Mouse.IsPressed)
            {
                Element.SetElementActive();
            }

            // Draw text box frame
            // todo: make visually distinct from buttons?
            var baseColor = Element.IsActiveElement ? Theme.ActiveColor : Theme.BaseColor;
            var borderColor = Element.IsActiveElement ? Theme.FocusColor : Theme.BorderColor;
            PrependFrame(baseColor, borderColor, ref bounds);
            Graphics.DrawLine(bounds.TopLeft, bounds.TopRight);

            Shrink(ref bounds, TextPaddingX, TextPaddingY);

            if (Element.IsActiveElement)
            {
                // Initialize the text editor
                TextEditor.Initialize(text);

                // If the element is active, use the callback to track glyphs rendered.
                // todo: scroll text into view?
                Graphics.Color = Theme.TextColor;
                Graphics.DrawText(text, bounds, Theme.Font, Theme.FontSize, TextAlign.Left | TextAlign.Middle, TextEditor.CharacterCallback);

                // Render cursor
                if (TextEditor.ShowCursor) // causes the cursor to blink
                {
                    var lineHeight = Theme.Font.GetMetrics(Theme.FontSize).Height;

                    // Draw the cursor
                    Graphics.Color = Theme.TextColor;
                    Graphics.DrawLine(TextEditor.CursorPosition, TextEditor.CursorPosition + (Vector.Down * lineHeight), 1);
                }

                // Process text editor input
                TextEditor.ProcessInput();

                // If the text isn't equivalent, it has been modified. 
                if (text != TextEditor.Text)
                {
                    text = TextEditor.Text;
                    return true;
                }
            }
            else
            {
                // Simply draw the text
                // todo: scroll text into view?
                Graphics.Color = Theme.TextColor;
                Graphics.DrawText(text, bounds, Theme.Font, Theme.FontSize, TextAlign.Left | TextAlign.Middle);
            }

            // Text is not modified.
            return false;
        }

        #endregion

        #region Shrink Box

        public static void ShrinkRight(ref IntRectangle bounds, int size)
        {
            bounds.Width -= size;
        }

        public static void ShrinkLeft(ref IntRectangle bounds, int size)
        {
            bounds.Width -= size;
            bounds.X += size;
        }

        public static void ShrinkHorizontal(ref IntRectangle bounds, int size)
        {
            bounds.Width -= size * 2;
            bounds.X += size;
        }

        public static void ShrinkTop(ref IntRectangle bounds, int size)
        {
            bounds.Height -= size;
            bounds.Y += size;
        }

        public static void ShrinkBottom(ref IntRectangle bounds, int size)
        {
            bounds.Height -= size;
        }

        public static void ShrinkVertical(ref IntRectangle bounds, int size)
        {
            bounds.Height -= size * 2;
            bounds.Y += size;
        }

        public static void Shrink(ref IntRectangle bounds, int sizeX, int sizeY)
        {
            ShrinkHorizontal(ref bounds, sizeX);
            ShrinkVertical(ref bounds, sizeY);
        }

        public static void Shrink(ref IntRectangle bounds, int size)
        {
            Shrink(ref bounds, size, size);
        }

        #endregion

        internal static class Mouse
        {
            internal static ButtonState Button;

            internal static Vector Position;

            internal static bool IsDown => Button.HasFlag(ButtonState.Down);

            internal static bool IsPressed => Button.HasFlag(ButtonState.Pressed);

            internal static bool IsReleased => Button.HasFlag(ButtonState.Released);

            internal static bool IsUp => Button.HasFlag(ButtonState.Up);

            internal static bool HasMouseInput => IsDown || IsPressed || IsReleased;
        }

        internal static class Element
        {
            internal static int ActiveElement;

            internal static int NextActiveElement;

            internal static int CurrentElement;

            internal static bool IsActiveElement => ActiveElement == CurrentElement;

            internal static void SetElementActive()
            {
                NextActiveElement = CurrentElement;
            }
        }

        internal class TextEditor
        {
            private const float BlinkDuration = 0.5F;

            private static string _text = string.Empty;
            private static int _cursor;

            private static float _blinkTimer;
            private static bool _blink;

            internal static bool ShowCursor => _blink;

            public static string Text => _text;

            private static int Cursor
            {
                get => _cursor;

                set
                {
                    _cursor = value;
                    _cursor = Calc.Clamp(_cursor, 0, _text.Length);
                }
            }

            public static Vector CursorPosition { get; internal set; }

            private static readonly List<CharacterCell> _characters = new();

            private struct CharacterCell
            {
                public Rectangle Bounds;
                public char Character;
                public int Index;
            }

            public static void Initialize(string text)
            {
                text ??= string.Empty;

                if (_text != text)
                {
                    Cursor = text.Length;
                    _text = text;
                }

                // 
                _characters.Clear();
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
            }

            internal static void ProcessInput()
            {
                // todo: ButtonState.Repeat

                if (Input.CheckKey(Key.Backspace, ButtonState.Pressed))
                {
                    DeleteBackwards();
                }
                else if (Input.CheckKey(Key.Delete, ButtonState.Pressed))
                {
                    DeleteForwards();
                }
                else if (Input.CheckKey(Key.Left, ButtonState.Pressed))
                {
                    Cursor--;
                }
                else if (Input.CheckKey(Key.Right, ButtonState.Pressed))
                {
                    Cursor++;
                }
                else if (Input.CheckKey(Key.Up, ButtonState.Pressed))
                {
                    // todo: somehow, find "equal" position in line above (if a line above)
                }
                else if (Input.CheckKey(Key.Down, ButtonState.Pressed))
                {
                    // todo: somehow, find "equal" position in line below (if a line below)
                }
                else if (Input.CheckKey(Key.End, ButtonState.Pressed))
                {
                    if (Cursor <= _text.Length) // othwerwise its already at end
                    {
                        // todo: somehow work on the visual text breaks...?
                        var next = _text.IndexOf('\n', Cursor);
                        if (next == -1) { next = _text.Length; }
                        Cursor = next;
                    }
                }
                else if (Input.CheckKey(Key.Home, ButtonState.Pressed))
                {
                    if (Cursor > 0) // otherwise, its already at home
                    {
                        // todo: somehow work on the visual text breaks...?
                        var next = _text.PreviousIndexOf('\n', Cursor - 1);
                        if (next == -1) { next = 0; }
                        Cursor = next;
                    }
                }
                else if (Input.TextInput.Length > 0)
                {
                    Insert(Input.TextInput);
                }
            }

            internal static void Insert(string str)
            {
                _text = _text.Insert(Cursor, str);
                Cursor += str.Length;
            }

            internal static void DeleteBackwards()
            {
                if (_text.Length > 0 && Cursor > 0)
                {
                    _text = _text.Remove(Cursor - 1, 1);
                    Cursor--;
                }
            }

            internal static void DeleteForwards()
            {
                if (Cursor < _text.Length)
                {
                    _text = _text.Remove(Cursor, 1);
                }
            }
        }
    }
}
