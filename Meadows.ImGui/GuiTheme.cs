using System;

using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public class GuiTheme
    {
        public Color Background { get; init; }

        public Color TextColor { get; init; }

        public Color BorderColor { get; init; }

        public Color FocusColor { get; init; }

        public Color BaseColor { get; init; }

        public Color ActiveColor { get; init; }

        public Color HoverColor { get; init; }

        public Font Font { get; init; }

        public int FontSize { get; init; }

        private static Color _textLight = Color.Parse("212121");

        private static Color _textDark = Color.Parse("EEEEEE");

        public static Color ErrorColor { get; } = Color.Parse("E57373");

        public static Color SuccessColor { get; } = Color.Parse("A5D6A7");

        public static Color InfoColor { get; } = Color.Parse("BBDEFB");

        public static Color WarnColor { get; } = Color.Parse("FFB74D");

        public static GuiTheme Light { get; } = CreateTheme(Color.Parse("CCC"), _textLight);

        public static GuiTheme Dark { get; } = CreateTheme(Color.Parse("333"));

        public static GuiTheme Default { get; } = Dark;

        /// <summary>
        /// Constructs a theme based on the specified color.
        /// </summary>
        /// <param name="baseColor">The base color.</param>
        /// <param name="focusColor">If unspecified, uses the complement color from the base hue.</param>
        /// <returns></returns>
        public static GuiTheme CreateTheme(Color baseColor, Color? focusColor = null)
        {
            return CreateTheme(Font.SansSerif, 12, baseColor, focusColor);
        }

        /// <summary>
        /// Constructs a theme based on the specified color.
        /// </summary>
        /// <param name="font">The font to use</param>
        /// <param name="baseColor">The base color.</param>
        /// <param name="focusColor">If unspecified, uses the complement color from the base hue.</param>
        /// <returns></returns>
        public static GuiTheme CreateTheme(Font font, int fontSize, Color baseColor, Color? focusColor = null)
        {
            if (font is null) { throw new ArgumentNullException(nameof(font)); }
            if (fontSize <= 0) { throw new ArgumentException("Font size must be greater than zero."); }

            focusColor ??= Color.FromHSV(baseColor.Hue + 180, 0.9F, 0.9F, baseColor.A);

            // Select text color based on brightness of base color
            var textColor = baseColor.Luminosity < 0.5 ? _textDark : _textLight;
            var backColor = baseColor.Luminosity < 0.5 ? MixColor(baseColor, 0.15F, 0.3F) : MixColor(baseColor, 0.15F, 0.7F);

            // 
            var activeColor = baseColor.Luminosity > 0.5 ? MixColor(baseColor, 0.5F, 0.35F) : MixColor(baseColor, 0.5F, 0.65F);
            var borderColor = baseColor.Luminosity > 0.5 ? MixColor(baseColor, 0.5F, 0.45F) : MixColor(baseColor, 0.5F, 0.55F);

            return new GuiTheme
            {
                Background = backColor,
                TextColor = textColor,
                BorderColor = borderColor,
                BaseColor = baseColor,
                ActiveColor = activeColor,
                HoverColor = MixColor(baseColor, 1.0F, 0.5F),

                FocusColor = focusColor.Value,

                Font = font,
                FontSize = fontSize
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
