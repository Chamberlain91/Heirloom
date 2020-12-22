using System;

using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public static class Gui
    {
        private const int BasicElementHeight = 23;
        private const int BasicElementSpace = 2;

        private const int BasicGroupSpace = BasicElementSpace * 3;

        private const int TextPaddingX = 4;
        private const int TextPaddingY = 2;

        private const int ButtonPaddingX = 2;
        private const int ButtonPaddingY = 2;

        private const int SliderHandleSize = 3; // 7 wide

        private static IntRectangle _layoutBox;
        private static int _y;

        private static InputState _input;

        public static GraphicsContext Graphics { get; private set; }

        public static GuiTheme Theme { get; set; } = GuiTheme.Default;

        public static void BeginFrame(GraphicsContext graphics)
        {
            Graphics = graphics;

            // Update mouse state
            _input.Position = Input.MousePosition;
            _input.Button = Input.GetButton(MouseButton.Left);

            // Assign new active element 
            _input.ActiveElement = _input.NextActiveElement;

            // If we click, the active element will now change. This might change
            // into the same element but thats still effectively like changing elements.
            if (_input.IsPressed) { _input.NextActiveElement = 0; }
        }

        public static void SetLayoutBox(IntRectangle layoutBox)
        {
            _layoutBox = layoutBox;
            _y = layoutBox.Top;
        }

        private static IntRectangle GetNextLayoutBox(int height)
        {
            var box = new IntRectangle(_layoutBox.X, _y, _layoutBox.Width, height);
            _y += height + BasicElementSpace;

            return box;
        }

        private static void BeginElement(string title)
        {
            _input.CurrentElement = HashCode.Combine(title);
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

        #endregion

        #region Non-Interactive Elements

        public static void Space()
        {
            _y += BasicGroupSpace;
        }

        public static void Label(string text)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);

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

            var bounds = GetNextLayoutBox(BasicElementHeight);

            var isMouseOver = bounds.Contains(_input.Position);
            if (isMouseOver && _input.IsPressed)
            {
                _input.MakeElementActive();
            }

            var isActiveMouseOver = isMouseOver && _input.IsActiveElement;

            // Draw button body
            var buttonColor = (isActiveMouseOver && _input.IsDown) ? Theme.ActiveColor : (isMouseOver ? Theme.HoverColor : Theme.BaseColor);
            var borderColor = _input.IsActiveElement ? Theme.FocusColor : Theme.BorderColor;
            PrependFrame(buttonColor, borderColor, ref bounds);

            // Collapse bounds to give padding to the button text
            Shrink(ref bounds, ButtonPaddingX, ButtonPaddingY);

            if (icon != null)
            {
                // Prepend icon
                PrependIcon(icon, ref bounds);
            }

            // Draw button text
            PrependText(title, ref bounds);

            return isActiveMouseOver && _input.IsReleased;
        }

        public static bool Slider(string title, ref float value, float min = 0F, float max = 100F, float step = 1F)
        {
            BeginElement(title);

            var bounds = GetNextLayoutBox(BasicElementHeight);

            // Draws text label
            PrependText(title, ref bounds);

            // Determine if the mouse is hovering this element.
            // If clicked on, store id and determine if selected.
            var isHoverSliderFrame = bounds.Contains(_input.Position);
            if (isHoverSliderFrame && _input.IsPressed) { _input.MakeElementActive(); }

            // Draw slider frame
            var sliderColor = isHoverSliderFrame ? Theme.HoverColor : Theme.BaseColor;
            var borderColor = _input.IsActiveElement ? Theme.FocusColor : Theme.BorderColor;
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
            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRect(handleRect);

            // 
            var isHoverHandle = handleRect.Contains(_input.Position);
            var isInteracting = _input.IsActiveElement && _input.IsDown && isHoverSliderFrame;

            var isModified = false;

            if (isInteracting || isHoverHandle)
            {
                if (isInteracting)
                {
                    // Slider value has changed
                    valueBetween = Calc.Clamp(Calc.Between(_input.Position.X, bounds.Left, bounds.Right), 0F, 1F);

                    value = Calc.Lerp(min, max, valueBetween);
                    value = Calc.Round(value / step) * step;

                    isModified = true;
                }

                // Display tooltip
                Tooltip($"{value}", handleRect.TopLeft);
            }

            return isModified;
        }

        public static bool TextBox(string title, ref string text)
        {
            BeginElement(title);

            var bounds = GetNextLayoutBox(BasicElementHeight);

            // Prepend title
            PrependText(title, ref bounds, false);

            // Determine if the mouse is hovering this element.
            // If clicked on, store id and determine if selected.
            var isHovering = bounds.Contains(_input.Position);
            if (isHovering && _input.IsPressed) { _input.MakeElementActive(); }

            //
            var bodyColor = _input.IsActiveElement ? Theme.ActiveColor : Theme.BaseColor;
            var borderColor = _input.IsActiveElement ? Theme.FocusColor : Theme.BorderColor;
            PrependFrame(bodyColor, borderColor, ref bounds);
            Shrink(ref bounds, TextPaddingX, TextPaddingY);

            //
            var displayText = text;
            if (_input.IsActiveElement) { displayText = $"{displayText}_"; }
            PrependText(displayText, ref bounds, false);

            // 
            if (_input.IsActiveElement)
            {
                if (Input.CheckKey(Key.Backspace, ButtonState.Pressed))
                // todo: ButtonState.Repeat?
                {
                    // Backspace
                    if (text.Length > 0)
                    {
                        text = text[..^1];
                    }
                }
                else if (Input.TextInput.Length > 0)
                {
                    text += Input.TextInput;
                }
            }

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

        private struct InputState
        {
            public ButtonState Button;

            public Vector Position;

            public bool IsDown => Button.HasFlag(ButtonState.Down);

            public bool IsPressed => Button.HasFlag(ButtonState.Pressed);

            public bool IsReleased => Button.HasFlag(ButtonState.Released);

            public bool IsUp => Button.HasFlag(ButtonState.Up);

            public bool HasMouseInput => IsDown || IsPressed || IsReleased;

            public int ActiveElement;

            public int NextActiveElement;

            public int CurrentElement;

            public bool IsActiveElement => ActiveElement == CurrentElement;

            internal void MakeElementActive()
            {
                _input.NextActiveElement = _input.CurrentElement;
            }
        }
    }
}
