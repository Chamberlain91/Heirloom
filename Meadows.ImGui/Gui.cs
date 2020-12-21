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

        private static IntRectangle _layoutBox;
        private static int _y;

        private static Vector MousePosition => Input.MousePosition;

        public static GraphicsContext Graphics { get; set; }

        public static GuiTheme Theme { get; set; } = GuiTheme.Default;

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

        public static void PrependText(string text, ref IntRectangle bounds)
        {
            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds?
            Graphics.Color = Theme.TextColor;
            var measure = Graphics.DrawText(text, bounds, Theme.Font, Theme.FontSize, TextAlign.Left | TextAlign.Middle);
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

            Graphics.Color = Theme.BaseColor;
            Graphics.DrawRect(backgroundBounds);
            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRectOutline(backgroundBounds);

            PrependText(text, ref bounds);
        }

        public static bool Button(string text, Texture icon = null)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);

            var isMouseHover = bounds.Contains(MousePosition);
            var isMousePress = isMouseHover && Input.CheckButton(MouseButton.Left, ButtonState.Released);
            var isMouseDown = isMouseHover && Input.CheckButton(MouseButton.Left, ButtonState.Down);
            // todo: somehow track button click down and up to prevent "drag clicks"

            // Draw button body
            Graphics.Color = isMouseDown ? Theme.ActiveColor : (isMouseHover ? Theme.HoverColor : Theme.BaseColor);
            Graphics.DrawRect(bounds);
            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRectOutline(bounds);
            Shrink(ref bounds, 1);
            bounds.Height -= 1; // todo: fix this fudge factor

            // Collapse bounds to give border around button
            Shrink(ref bounds, ButtonPaddingX, ButtonPaddingY);

            if (icon != null)
            {
                // Prepend icon
                PrependIcon(icon, ref bounds);
            }

            // Draw button text
            PrependText(text, ref bounds);

            // 
            return isMousePress;
        }

        public static bool Slider(string text, ref float value, float min = 0F, float max = 100F)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);

            // Adjusts so label text starts in the same place as button
            ShrinkLeft(ref bounds, TextPaddingX);
            PrependText(text, ref bounds);

            var isMouseHover = bounds.Contains(MousePosition);
            var isMouseDown = isMouseHover && Input.CheckButton(MouseButton.Left, ButtonState.Down);
            // todo: somehow track button click down and up to prevent "drag clicks"

            // Draw slider frame
            Graphics.Color = isMouseHover ? Theme.HoverColor : Theme.BaseColor;
            Graphics.DrawRect(bounds);
            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRectOutline(bounds);
            Shrink(ref bounds, 1);
            bounds.Height -= 1; // todo: fix this fudge factor

            // Copy the bounds for drawin the slider box
            var sliderBox = bounds;

            // Shrink the bounds to prevent the handle from escaping the slider box
            const int SLIDER_HANDLE_SIZE = 3;
            ShrinkHorizontal(ref bounds, SLIDER_HANDLE_SIZE);

            // Compute where in the slider the current value is
            var valueBetween = Calc.Clamp(Calc.Between(value, min, max), 0F, 1F);
            var sliderPosition = (int) Calc.Lerp(bounds.Left, bounds.Right, valueBetween);

            // Compute the width of the slider value
            sliderBox.Width = sliderPosition - bounds.Left;

            // Draw handle
            Graphics.Color = Theme.ActiveColor;
            Graphics.DrawRect(sliderBox);

            // Compute the handle box
            var handleBox = bounds;
            handleBox.X = sliderPosition - SLIDER_HANDLE_SIZE;
            handleBox.Width = (SLIDER_HANDLE_SIZE * 2) + 1;

            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRect(handleBox);

            var isInsideHandle = handleBox.Contains(MousePosition);

            if (isMouseDown || isInsideHandle)
            {
                var isModified = false;

                if (isMouseDown)
                {
                    // Slider value has changed
                    valueBetween = Calc.Clamp(Calc.Between(MousePosition.X, bounds.Left, bounds.Right), 0F, 1F);
                    value = Calc.Lerp(min, max, valueBetween);
                    isModified = true;
                }

                Tooltip($"{value:0.00}", handleBox.TopLeft);

                return isModified;
            }

            return false;
        }

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
    }
}
