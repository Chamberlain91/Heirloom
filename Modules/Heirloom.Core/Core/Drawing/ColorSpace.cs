using System;

using static Heirloom.Calc;

namespace Heirloom
{
    /// <summary>
    /// Provides methods for converting between color spaces and other utility functions.
    /// </summary>
    public static class ColorSpace
    // http://www.easyrgb.com/en/math.php
    {
        // D65/2 illuminant
        private const float D65X = 95.047F;
        private const float D65Y = 100.0F;
        private const float D65Z = 108.883F;

        #region Convert RGB & LAB

        #region RGB & XYZ

        private static XYZColor RGBtoXYZ(in Color rgb)
        {
            // sR, sG and sB (Standard RGB) input range = 0 ÷ 255
            // X, Y and Z output refer to a D65/2° standard illuminant.

            var r = rgb.R;
            var g = rgb.G;
            var b = rgb.B;

            if (r > 0.04045F) { r = Pow((r + 0.055F) / 1.055F, 2.4F); }
            else { r /= 12.92F; }

            if (g > 0.04045F) { g = Pow((g + 0.055F) / 1.055F, 2.4F); }
            else { g /= 12.92F; }

            if (b > 0.04045F) { b = Pow((b + 0.055F) / 1.055F, 2.4F); }
            else { b /= 12.92F; }

            r *= 100;
            g *= 100;
            b *= 100;

            var X = (r * 0.4124F) + (g * 0.3576F) + (b * 0.1805F);
            var Y = (r * 0.2126F) + (g * 0.7152F) + (b * 0.0722F);
            var Z = (r * 0.0193F) + (g * 0.1192F) + (b * 0.9505F);

            return new XYZColor(X, Y, Z); // 0 - 100
        }

        private static Color XYZtoRGB(in XYZColor color)
        {
            //X, Y and Z input refer to a D65/2° standard illuminant.
            //sR, sG and sB (standard RGB) output range = 0 ÷ 255

            var x = color.X / 100F;
            var y = color.Y / 100F;
            var z = color.Z / 100F;

            var r = (x * 3.2406F) + (y * -1.5372F) + (z * -0.4986F);
            var g = (x * -0.9689F) + (y * 1.8758F) + (z * 0.0415F);
            var b = (x * 0.0557F) + (y * -0.2040F) + (z * 1.0570F);

            if (r > 0.0031308F) { r = (1.055F * Pow(r, 1 / 2.4F)) - 0.055F; }
            else { r = 12.92F * r; }

            if (g > 0.0031308F) { g = (1.055F * Pow(g, 1 / 2.4F)) - 0.055F; }
            else { g = 12.92F * g; }

            if (b > 0.0031308F) { b = (1.055F * Pow(b, 1 / 2.4F)) - 0.055F; }
            else { b = 12.92F * b; }

            // 
            return new Color(r, g, b);
        }

        #endregion

        #region XYZ & LAB

        private static LabColor XYZtoLAB(XYZColor xyz)
        {
            var x = xyz.X / D65X;
            var y = xyz.Y / D65Y;
            var z = xyz.Z / D65Z;

            if (x > 0.008856F) { x = Pow(x, 1 / 3F); }
            else { x = (7.787F * x) + (16 / 116F); }

            if (y > 0.008856F) { y = Pow(y, 1 / 3F); }
            else { y = (7.787F * y) + (16 / 116F); }

            if (z > 0.008856F) { z = Pow(z, 1 / 3F); }
            else { z = (7.787F * z) + (16 / 116F); }

            var L = (116 * y) - 16;
            var A = 500 * (x - y);
            var B = 200 * (y - z);

            return new LabColor(L, A, B);
        }

        private static XYZColor LABtoXYZ(LabColor lab)
        {
            var y = (lab.L + 16) / 116F;
            var x = (lab.A / 500F) + y;
            var z = y - (lab.B / 200F);

            var y3 = y * y * y;
            if (y3 > 0.008856) { y = y3; }
            else { y = (y - (16 / 116F)) / 7.787F; }

            var x3 = x * x * x;
            if (x3 > 0.008856) { x = x3; }
            else { x = (x - (16 / 116F)) / 7.787F; }

            var z3 = z * z * z;
            if (z3 > 0.008856) { z = z3; }
            else { z = (z - (16 / 116F)) / 7.787F; }

            x *= D65X;
            y *= D65Y;
            z *= D65Z;

            return new XYZColor(x, y, z);
        }

        #endregion

        private struct XYZColor
        {
            public float X;
            public float Y;
            public float Z;

            public XYZColor(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        /// <summary>
        /// Converts a RGB color to CIE-LAB.
        /// </summary>
        /// <remarks>Note, the alpha component is discarded.</remarks>
        public static LabColor RGBtoLAB(Color rgb)
        {
            return XYZtoLAB(RGBtoXYZ(rgb));
        }

        /// <summary>
        /// Converts a CIE-LAB Color to RGB
        /// </summary>
        public static Color LABtoRGB(LabColor lab)
        {
            return XYZtoRGB(LABtoXYZ(lab));
        }

        #endregion

        #region RGB Bits

        /// <summary>
        /// Snaps a color to the nearest position in a color space described by the specified bits.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="redBits"></param>
        /// <param name="greenBits"></param>
        /// <param name="blueBits"></param>
        /// <returns></returns>
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
        /// <param name="color"></param>
        /// <param name="redBits"></param>
        /// <param name="greenBits"></param>
        /// <param name="blueBits"></param>
        /// <returns></returns>
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
    }
}
