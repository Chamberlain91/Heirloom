using Heirloom.Drawing;

namespace KenneyUI
{
    public class UITheme
    {
        public Font TitleFont = Font.PixelFont;
        public int TitleFontSize = 32;

        public Font Font = Font.PixelFont;
        public int FontSize = 16;

        public NineSlice WindowFrame;
        public float WindowPadding = 4;

        public NineSlice ContentFrame;
        public float ContentPadding = 8;

        public NineSlice ButtonFrame;
        public NineSlice ButtonDownFrame;
    }
}
