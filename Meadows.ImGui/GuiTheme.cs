
using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public class GuiTheme
    {
        public Color TextColor { get; init; }

        public Color BorderColor { get; init; }

        public Color BaseColor { get; init; }

        public Color ShadeColor { get; init; }

        public Color ActiveColor { get; init; }

        public Color HoverColor { get; init; }

        public Font Font { get; init; }

        public int FontSize { get; init; }

        public static GuiTheme Default { get; } = CreateDefaultTheme();

        private static GuiTheme CreateDefaultTheme()
        {
            return new GuiTheme
            {
                TextColor = MixColor(Color.Orange, 0.5F, 0.25F),
                BorderColor = MixColor(Color.Orange, 0.5F, 0.25F),
                BaseColor = MixColor(Color.Orange, 0.5F, 0.5F),
                ShadeColor = MixColor(Color.Orange, 0.25F, 0.25F),
                ActiveColor = MixColor(Color.Orange, 0.75F, 0.5F),
                HoverColor = MixColor(Color.Orange, 0.25F, 0.75F),

                Font = Font.Default,
                FontSize = 16
            };
        }

        private static Color MixColor(Color baseColor, float saturation, float brightness)
        {
            if (brightness < 0.5F)
            {
                var value = Calc.Between(brightness, 0.0F, 0.5F);
                return Color.FromHSV(baseColor.Hue, saturation, value);
            }
            else
            {
                var value = Calc.Between(brightness, 0.5F, 1.0F);
                var color = Color.FromHSV(baseColor.Hue, saturation, 1F);
                return Color.Lerp(color, Color.White, value);
            }
        }
    }
}
