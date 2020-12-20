using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public static class Gui
    {
        private const int BasicElementHeight = 22;
        private const int BasicSpace = 3;

        private const int PaddingX = 4;
        private const int PaddingY = 2;

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
            _y += height + BasicSpace;

            return box;
        }

        public static void PrependText(string text, ref IntRectangle bounds)
        {
            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds?
            Graphics.Color = Theme.TextColor;
            var measure = Graphics.DrawText(text, bounds, Theme.Font, Theme.FontSize, TextAlign.Left | TextAlign.Middle);
            ShrinkLeft(ref bounds, PaddingX + (int) measure.Width);
        }

        public static void PrependIcon(Texture texture, ref IntRectangle bounds)
        {
            var aspect = texture.Size.Aspect;

            // Fit image into bounds
            var box = bounds;
            box.Width = (int) (bounds.Height * aspect);
            ShrinkLeft(ref bounds, PaddingX + box.Width);

            // Draw label
            Graphics.Color = Color.White;
            Graphics.DrawImage(texture, box);
        }

        public static void Label(string text)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);

            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds.
            Graphics.Color = Theme.TextColor;
            Graphics.DrawText(text, bounds, Theme.Font, Theme.FontSize, TextAlign.Left | TextAlign.Middle);
        }

        public static bool Button(string text, Texture icon = null)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);

            var isMouseHover = bounds.Contains(MousePosition);
            var isMousePress = isMouseHover && Input.CheckButton(MouseButton.Left, ButtonState.Released);
            var isMouseDown = isMouseHover && Input.CheckButton(MouseButton.Left, ButtonState.Down);
            // todo: somehow track button click down and up to prevent "drag clicks"

            Graphics.Color = isMouseDown ? Theme.ActiveColor : (isMouseHover ? Theme.HoverColor : Theme.BaseColor);
            Graphics.DrawRect(bounds);

            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRectOutline(bounds, 1F);

            // Compensate for outline
            bounds = IntRectangle.Inflate(bounds, -1);
            bounds.Height -= 1; // fudge, why?

            // Collapse bounds to give border around button
            Shrink(ref bounds, PaddingX, PaddingY);

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

            // Draw slider label
            PrependText(text, ref bounds);

            var isMouseHover = bounds.Contains(MousePosition);
            var isMouseDown = isMouseHover && Input.CheckButton(MouseButton.Left, ButtonState.Down);
            // todo: somehow track button click down and up to prevent "drag clicks"

            // Draw slider frame
            Graphics.Color = isMouseHover ? Theme.HoverColor : Theme.BaseColor;
            Graphics.DrawRect(bounds);
            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRectOutline(bounds, 1F);

            // Compute how in-between the slider value is
            var between = Calc.Clamp(Calc.Between(value, min, max), 0F, 1F);

            // Compute the handle box
            var handleBox = bounds;
            handleBox.X = ((int) Calc.Lerp(bounds.Left, bounds.Right, between)) - 2;
            handleBox.Width = 5;

            Graphics.Color = Theme.BorderColor;
            Graphics.DrawRect(handleBox);

            if (isMouseDown)
            {
                // Slider value has changed
                between = Calc.Between(MousePosition.X, bounds.Left, bounds.Right);
                value = Calc.Lerp(min, max, between);
                return true;
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
