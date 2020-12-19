using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public static class ImGui
    {
        private static Vector MousePosition => Input.MousePosition;

        public static GraphicsContext Graphics { get; set; }

        public static Font Font { get; set; } = Font.Default;

        public static int FontSize { get; set; } = 16;

        public static Color FillColor { get; set; } = Color.White;

        public static Color BorderColor { get; set; } = Color.DarkGray;

        public static Color TextColor { get; set; } = Color.DarkGray;

        public static bool Button(IntRectangle bounds, string text, Image icon = null)
        {
            var isInside = bounds.Contains(MousePosition);

            Graphics.Color = FillColor;
            Graphics.DrawRect(bounds);

            Graphics.Color = BorderColor;
            Graphics.DrawRectOutline(bounds, 1F);

            // todo: clip text / shorten it?
            Graphics.Color = TextColor;
            Graphics.DrawText(text, bounds, Font, FontSize, TextAlign.Center | TextAlign.Middle);

            // 
            return isInside && Input.CheckButton(MouseButton.Left, ButtonState.Pressed);
        }
    }
}
