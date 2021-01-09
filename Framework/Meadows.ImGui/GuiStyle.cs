using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public sealed class GuiStyle
    {
        public int BasicElementSpace { get; init; }

        public int BasicElementHeight => (Padding.Y * 2) + (int) Font.GetMetrics(FontSize).Height;

        public IntVector ContainerPadding { get; init; }

        public IntVector TextPadding { get; init; }

        public IntVector Padding { get; init; }

        public int HandleSize { get; init; }

        public Font FontBold { get; init; }

        public Font Font { get; init; }

        public int FontSize { get; init; }

        public Color Background { get; init; }

        public Color TextColor { get; init; }

        public Color BorderColor { get; init; }

        public Color FocusColor { get; init; }

        public Color BaseColor { get; init; }

        public Color ActiveColor { get; init; }

        public Color HoverColor { get; init; }

        public Color ErrorColor { get; } = Color.Parse("E57373");

        public Color SuccessColor { get; } = Color.Parse("A5D6A7");

        public Color InfoColor { get; } = Color.Parse("BBDEFB");

        public Color WarnColor { get; } = Color.Parse("FFB74D");

        public static GuiStyle Light { get; } = CreateSimpleTheme(Color.Parse("CCC"), Color.Parse("29B6F6"));

        public static GuiStyle Dark { get; } = CreateSimpleTheme(Color.Parse("333"), Color.Parse("29B6F6"));

        public GuiStyle()
        {
            // Default font
            FontSize = 13;
            FontBold = Font.SansSerifBold;
            Font = Font.SansSerif;

            // 
            BasicElementSpace = 3;
            ContainerPadding = new(10, 10);
            TextPadding = new(10, 5);
            Padding = new(5, 5);
            HandleSize = 5;

            //// todo: actually, remove these... styling should be simpler
            //Slider = new SliderElementStyle { Padding = (0, 4), HandleSize = 3, HandlePadding = 1 };
            //Button = new BasicElementStyle { Padding = (8, 8) };
            //Panel = new BasicElementStyle { Padding = (12, 12) };
            //Text = new BasicElementStyle { Padding = (8, 4) };
        }

        /// <summary>
        /// Constructs a theme based on the specified color.
        /// </summary>
        /// <param name="baseColor">The base color.</param>
        /// <param name="focusColor">If unspecified, uses the complement color from the base hue.</param>
        /// <returns></returns>
        public static GuiStyle CreateSimpleTheme(Color baseColor, Color? focusColor = null)
        {
            focusColor ??= Color.FromHSV(baseColor.Hue + 180, 0.8F, 0.8F, baseColor.A);

            // Text colors light and dark backgrounds
            var textLight = Color.Parse("212121");
            var textDark = Color.Parse("EEEEEE");

            // Select text color based on brightness of base color
            var textColor = baseColor.Luminosity < 0.5 ? textDark : textLight;
            var backColor = baseColor.Luminosity < 0.5 ? MixColor(baseColor, 0.15F, 0.3F) : MixColor(baseColor, 0.15F, 0.7F);

            // 
            var activeColor = baseColor.Luminosity > 0.5 ? MixColor(baseColor, 0.5F, 0.40F) : MixColor(baseColor, 0.5F, 0.60F);
            var borderColor = baseColor.Luminosity > 0.5 ? MixColor(baseColor, 0.5F, 0.45F) : MixColor(baseColor, 0.5F, 0.55F);

            return new GuiStyle
            {
                Background = backColor,

                TextColor = textColor,
                BorderColor = borderColor,

                BaseColor = baseColor,
                HoverColor = MixColor(baseColor, 0.95F, 0.55F),

                ActiveColor = activeColor,
                FocusColor = focusColor.Value
            };
        }

        private static Color MixColor(Color baseColor, float saturation, float brightness)
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
