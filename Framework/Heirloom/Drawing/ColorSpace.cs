using System;

using static Heirloom.Mathematics.Calc;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides methods for converting between color spaces and other utility functions.
    /// </summary>
    public static class ColorSpace
    {
        #region RGB Bits

        /// <summary>
        /// Snaps a color to the nearest position in a color space described by the specified bits.
        /// </summary>
        /// <param name="color">The orignial color.</param>
        /// <param name="redBits">The number of red bits.</param>
        /// <param name="greenBits">The number of green bits.</param>
        /// <param name="blueBits">The number of blue bits.</param>
        /// <returns>A bit-reduced color.</returns>
        public static Color ConvertBits(Color color, int redBits, int greenBits, int blueBits)
        {
            var redMax = (float) ((1 << redBits) - 1);
            var greenMax = (float) ((1 << greenBits) - 1);
            var blueMax = (float) ((1 << blueBits) - 1);

            // 
            color.R = Round(color.R * redMax) / redMax;
            color.G = Round(color.G * greenMax) / greenMax;
            color.B = Round(color.B * blueMax) / blueMax;

            return color;
        }

        /// <summary>
        /// Snaps a color to the nearest position in a color space described by the specified bits using the specified dither pattern.
        /// </summary>
        /// <param name="color">The orignial color.</param>
        /// <param name="redBits">The number of red bits.</param>
        /// <param name="greenBits">The number of green bits.</param>
        /// <param name="blueBits">The number of blue bits.</param>
        /// <param name="pattern">The dithering pattern to use.</param>
        /// <param name="x">The x-coordinate to use for dither.</param>
        /// <param name="y">The y-coordinate to use for dither.</param>
        /// <returns>A dithered, bit-reduced color.</returns>
        public static Color ConvertBits(Color color, int redBits, int greenBits, int blueBits, DitherPattern pattern, int x, int y)
        {
            if (pattern is null) { throw new ArgumentNullException(nameof(pattern)); }

            // Snaps each color channel to either upper or lower bit level
            // based on the dither pattern.
            color.R = QuantizeDither(x, y, color.R, redBits);
            color.G = QuantizeDither(x, y, color.G, greenBits);
            color.B = QuantizeDither(x, y, color.B, blueBits);

            return color;

            float QuantizeDither(int x, int y, float val, int bits)
            {
                var levels = (float) ((1 << bits) - 1); // 31 for 5 bits

                // Upper level
                var lo = Floor(val * levels) / (float) levels;
                var hi = Ceil(val * levels) / (float) levels;

                // 
                return Between(val, lo, hi) > pattern.GetValue(x, y) ? hi : lo;
            }
        }

        #endregion

        #region Conversion (Gamma sRGB to Linear sRGB)

        /// <summary>
        /// Move color from sRGB (w/ Gamma Curve) to Linear sRGB (w/out Gamma Curve)
        /// </summary>
        public static Color ToLinear(Color c)
        {
            return new Color(ToLinear(c.R), ToLinear(c.G), ToLinear(c.B), c.A);
        }

        /// <summary>
        /// Move color from Linear sRGB (w/out Gamma Curve) to sRGB (w/ Gamma Curve)
        /// </summary>
        public static Color ToGamma(Color c)
        {
            return new Color(ToGamma(c.R), ToGamma(c.G), ToGamma(c.B), c.A);
        }

        private static float ToLinear(float x)
        {
            if (x > 0.04045F) { return Pow((x + 0.055F) / 1.055F, 2.4F); }
            else { return x / 12.92F; }
        }

        private static float ToGamma(float x)
        {
            if (x > 0.0031308F) { return (1.055F * Pow(x, 1 / 2.4F)) - 0.055F; }
            else { return 12.92F * x; }
        }

        #endregion

        #region Conversion (RGB and Lab)

        /// <summary>
        /// Convert from linear sRGB to Lab.
        /// </summary>
        public static ColorLab RgbToLab(Color c)
        // https://bottosson.github.io/posts/oklab/
        {
            var l = 0.4122214708f * c.R + 0.5363325363f * c.G + 0.0514459929f * c.B;
            var m = 0.2119034982f * c.R + 0.6806995451f * c.G + 0.1073969566f * c.B;
            var s = 0.0883024619f * c.R + 0.2817188376f * c.G + 0.6299787005f * c.B;

            var l_ = MathF.Cbrt(l);
            var m_ = MathF.Cbrt(m);
            var s_ = MathF.Cbrt(s);

            return new ColorLab(
                0.2104542553f * l_ + 0.7936177850f * m_ - 0.0040720468f * s_,
                1.9779984951f * l_ - 2.4285922050f * m_ + 0.4505937099f * s_,
                0.0259040371f * l_ + 0.7827717662f * m_ - 0.8086757660f * s_
            );
        }

        /// <summary>
        /// Convert from Lab to linear sRGB.
        /// </summary>
        public static Color LabToRgb(ColorLab c)
        // https://bottosson.github.io/posts/oklab/
        {
            var l_ = c.L + 0.3963377774f * c.A + 0.2158037573f * c.B;
            var m_ = c.L - 0.1055613458f * c.A - 0.0638541728f * c.B;
            var s_ = c.L - 0.0894841775f * c.A - 1.2914855480f * c.B;

            var l = l_ * l_ * l_;
            var m = m_ * m_ * m_;
            var s = s_ * s_ * s_;

            return new Color(
                +4.0767416621f * l - 3.3077115913f * m + 0.2309699292f * s,
                -1.2684380046f * l + 2.6097574011f * m - 0.3413193965f * s,
                -0.0041960863f * l - 0.7034186147f * m + 1.7076147010f * s);
        }

        #endregion
    }
}
