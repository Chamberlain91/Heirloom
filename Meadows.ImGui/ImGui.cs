using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public static class ImGui
    {
        private const int PaddingX = 2;
        private const int PaddingY = 2;

        private static Vector MousePosition => Input.MousePosition;

        public static GraphicsContext Graphics { get; set; }

        public static Font Font { get; set; } = Font.Default;
        public static int FontSize { get; set; } = 16;

        public static Color FillColor { get; set; } = Color.White;
        public static Color FillHoverColor { get; set; } = Color.LightGray;
        public static Color BorderColor { get; set; } = Color.DarkGray;
        public static Color TextColor { get; set; } = Color.DarkGray;

        public static void Label(IntRectangle bounds, string text)
        {
            // todo: clip text / shorten it?

            Graphics.Color = TextColor;
            Graphics.DrawText(text, bounds, Font, FontSize, TextAlign.Left | TextAlign.Middle);
        }

        // image at position
        public static void Image(int x, int y, Texture image)
        {
            Graphics.Color = Color.White;
            Graphics.DrawImage(image, Matrix.CreateTranslation(x, y));
        }

        // image at position
        public static void Image(IntVector position, Texture texture)
        {
            Image(position.X, position.Y, texture);
        }

        // image in center of bounds
        public static void Image(IntRectangle bounds, Texture texture)
        {
            var x = (bounds.Width - texture.Width) / 2;
            var y = (bounds.Height - texture.Height) / 2;
            Image(bounds.X + x, bounds.Y + y, texture);
        }

        // image resized to bounds
        public static void Icon(IntRectangle bounds, Texture texture)
        {
            Graphics.Color = Color.White;
            Graphics.DrawImage(texture, bounds);
        }

        public static bool Button(IntRectangle bounds, string text, Texture icon = null)
        {
            var isInside = bounds.Contains(MousePosition);
            var isMousePress = isInside && Input.CheckButton(MouseButton.Left, ButtonState.Released);
            var isMouseDown = isInside && Input.CheckButton(MouseButton.Left, ButtonState.Down);
            // todo: somehow track button click down and up to prevent "drag clicks"

            Graphics.Color = isMouseDown ? FillColor * Color.Gray : (isInside ? FillHoverColor : FillColor);
            Graphics.DrawRect(bounds);

            Graphics.Color = BorderColor;
            Graphics.DrawRectOutline(bounds, 1F);

            // Compensate for outline
            bounds = IntRectangle.Inflate(bounds, -1);
            bounds.Height -= 1; // fudge, why?

            if (icon != null)
            {
                // Collapse bounds to give border around button
                bounds = IntRectangle.Inflate(bounds, -PaddingX, -PaddingY);

                var iconBox = new IntRectangle(bounds.X, bounds.Y, bounds.Height, bounds.Height);
                Icon(iconBox, icon);

                // Push border left edge
                // todo: PaddingTextX or figure out why this fudge is needed
                bounds.X += iconBox.Width + PaddingX * 2;
                bounds.Width -= iconBox.Width + PaddingX * 2;
            }
            else
            {
                // Collapse bounds to give border around button
                // todo: PaddingTextX or figure out why this fudge is needed
                bounds = IntRectangle.Inflate(bounds, -PaddingX * 2, -PaddingY);
            }

            // Draw button text
            Label(bounds, text);

            // 
            return isMousePress;
        }
    }
}
