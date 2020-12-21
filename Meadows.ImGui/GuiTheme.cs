
using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public class GuiTheme
    {
        public Color Background { get; init; }

        public Color TextColor { get; init; }

        public Color BorderColor { get; init; }

        public Color BaseColor { get; init; }

        public Color ActiveColor { get; init; }

        public Color HoverColor { get; init; }

        public Font Font { get; init; }

        public int FontSize { get; init; }

        public static GuiTheme Default { get; } = CreateDefaultTheme();

        private static GuiTheme CreateDefaultTheme()
        {
            var color = Color.Parse("B0BEC5"); // material blue-grey 200

            return new GuiTheme
            {
                Background = MixColor(color, 0.1F, 0.6F),
                TextColor = MixColor(color, 1.0F, 0.2F),
                BorderColor = MixColor(color, 1.0F, 0.4F),
                BaseColor = MixColor(color, 0.75F, 0.5F),
                ActiveColor = MixColor(color, 0.3F, 0.7F),
                HoverColor = MixColor(color, 1.0F, 0.5F),

                Font = Font.Default,
                FontSize = 16
            };

            static Color MixColor(Color baseColor, float saturation, float brightness)
            {
                saturation = baseColor.Saturation * saturation;

                if (brightness < 0.5F)
                {
                    var value = Calc.Between(brightness, 0.5F, 0.0F);
                    var color = Color.FromHSV(baseColor.Hue, saturation, baseColor.Value);
                    return Color.Lerp(color, Color.Black, value);
                }
                else
                {
                    var value = Calc.Between(brightness, 0.5F, 1.0F);
                    var color = Color.FromHSV(baseColor.Hue, saturation, baseColor.Value);
                    return Color.Lerp(color, Color.White, value);
                }
            }
        }
    }
}
