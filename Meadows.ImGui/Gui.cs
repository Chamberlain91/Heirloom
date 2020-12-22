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

            // 
            _input.LastActiveElement = _input.ActiveElement;

            // No input and the active element did not change
            var hasInput = _input.IsDown || _input.IsPressed || _input.IsReleased;
            if (!hasInput && _input.LastActiveElement == _input.ActiveElement)
            {
                _input.ActiveElement = 0;
            }
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

        public static void PrependFrame(Color color, ref IntRectangle bounds)
        {
            Graphics.Color = color;
            Graphics.DrawRect(bounds);

            Graphics.Color = Theme.BorderColor;
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
            PrependFrame(Theme.BaseColor, ref backgroundBounds);
            PrependText(text, ref bounds, true);
        }

        #endregion

        #region Interactive Elements

        public static bool Button(string text, Texture icon = null)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);
            var id = HashCode.Combine(text, icon);

            // Determine if the mouse is hovering this element.
            // If clicked on, store id and determine if selected.
            var isHovering = bounds.Contains(_input.Position);
            if (isHovering && _input.IsPressed) { _input.ActiveElement = id; }
            var isSelected = isHovering && _input.ActiveElement == id;

            // Draw button body
            var buttonColor = (isSelected && _input.IsDown) ? Theme.ActiveColor : (isHovering ? Theme.HoverColor : Theme.BaseColor);
            PrependFrame(buttonColor, ref bounds);

            // Collapse bounds to give padding to the button text
            Shrink(ref bounds, ButtonPaddingX, ButtonPaddingY);

            if (icon != null)
            {
                // Prepend icon
                PrependIcon(icon, ref bounds);
            }

            // Draw button text
            PrependText(text, ref bounds);

            // 
            return _input.IsReleased && isSelected;
        }

        public static bool Slider(string text, ref float value, float min = 0F, float max = 100F)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);
            var id = HashCode.Combine(text);

            // Draws text label
            ShrinkLeft(ref bounds, TextPaddingX);
            PrependText(text, ref bounds);

            // Determine if the mouse is hovering this element.
            // If clicked on, store id and determine if selected.
            var isHoverSlider = bounds.Contains(_input.Position);
            if (isHoverSlider && _input.IsPressed) { _input.ActiveElement = id; }
            var isSelected = isHoverSlider && _input.ActiveElement == id;

            // Draw slider frame
            var sliderColor = isHoverSlider ? Theme.HoverColor : Theme.BaseColor;
            PrependFrame(sliderColor, ref bounds);

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
            var isInteracting = isSelected && _input.IsDown;

            if (isInteracting || isHoverHandle)
            {
                var isModified = false;

                if (isInteracting)
                {
                    // Slider value has changed
                    valueBetween = Calc.Clamp(Calc.Between(_input.Position.X, bounds.Left, bounds.Right), 0F, 1F);
                    value = Calc.Lerp(min, max, valueBetween);
                    isModified = true;
                }

                // Display tooltip
                Tooltip($"{value:0.00}", handleRect.TopLeft);

                return isModified;
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

            public int ActiveElement;

            public int LastActiveElement;

            public bool IsDown => Button.HasFlag(ButtonState.Down);

            public bool IsPressed => Button.HasFlag(ButtonState.Pressed);

            public bool IsReleased => Button.HasFlag(ButtonState.Released);

            public bool IsUp => Button.HasFlag(ButtonState.Up);
        }
    }
}
