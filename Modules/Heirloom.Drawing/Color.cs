using System;
using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Color encoded as 4 component floats.
    /// </summary>
    /// <seealso cref="Pixel"/>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Color : IEquatable<Color>
    {
        /// <summary>
        /// The red component.
        /// </summary>
        public float R;

        /// <summary>
        /// The green component.
        /// </summary>
        public float G;

        /// <summary>
        /// The blue component.
        /// </summary>
        public float B;

        /// <summary>
        /// The alpha/transparency component.
        /// </summary>
        public float A;

        #region Constants

        public static readonly Color Red = Parse("FF0000");

        public static readonly Color Green = Parse("00FF00");

        public static readonly Color Blue = Parse("0000FF");

        public static readonly Color Yellow = Parse("FFFF00");

        public static readonly Color Cyan = Parse("00FFFF");

        public static readonly Color Magenta = Parse("FF00FF");

        public static readonly Color White = Parse("FFFFFF");

        public static readonly Color Black = Parse("000000");

        public static readonly Color Gray = Parse("999999");

        public static readonly Color DarkGray = Parse("333333");

        public static readonly Color LightGray = Parse("CCCCCC");

        public static readonly Color Orange = Parse("FF8811");

        public static readonly Color Indigo = Parse("FF4B0082");

        public static readonly Color Violet = Parse("FF8A2BE2");

        public static readonly Color Pink = Parse("DD55AA");

        public static readonly Color Transparent = Parse("00000000");

        public static readonly Color[] Rainbow = { Red, Orange, Yellow, Green, Blue, Indigo, Violet };

        public static Color Random
            => new Color(Calc.Random.NextFloat(), Calc.Random.NextFloat(), Calc.Random.NextFloat());

        #endregion

        #region Constructors

        public Color(float r, float g, float b, float a = 1F)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Computes a luminosity component (grayscale).
        /// </summary>
        public float Luminosity
            => ((R * 0.299F) + (G * 0.587F) + (B * 0.114F)) * A;

        /// <summary>
        /// The inversion of this color.
        /// </summary>
        public Color Inverted => new Color(1F - R, 1F - G, 1F - B, A);

        /// <summary>
        /// Gets or sets the (HSV) hue of this color.
        /// </summary>
        public float Hue
        {
            get
            {
                var min = Calc.Min(R, G, B);
                var max = Calc.Max(R, G, B);
                var delta = max - min;

                // Undefined hue cases
                if (max <= 0F || Calc.NearZero(delta)) { return 0; }

                float hue;

                // Compute Hue
                if (Calc.NearEquals(R, max))
                {
                    hue = (G - B) / delta;
                }
                else
                if (Calc.NearEquals(G, max))
                {
                    hue = 2F + (B - R) / delta;
                }
                else
                {
                    hue = 4F + (R - G) / delta;
                }

                hue *= 60; // quadrant to degrees

                // Return hue in positive range
                return hue < 0 ? hue + 360 : hue;
            }

            set
            {
                // todo: optimize, can probably skip some of the work
                ToHSV(out _, out var s, out var v);
                this = FromHSV(value, s, v);
            }
        }

        /// <summary>
        /// Gets or sets the (HSV) brightness value of this color.
        /// </summary>
        public float Brightness
        {
            get => Calc.Max(R, G, B);

            set
            {
                // todo: optimize, can probably skip some of the work
                ToHSV(out var h, out var s, out _);
                this = FromHSV(h, s, value);
            }
        }

        /// <summary>
        /// Gets or sets the (HSV) saturation of this color.
        /// </summary>
        public float Saturation
        {
            get
            {
                var min = Calc.Min(R, G, B);
                var max = Calc.Max(R, G, B);
                var delta = max - min;

                // Invalid saturation cases
                if (max <= 0F || Calc.NearZero(delta))
                {
                    return 0;
                }

                // Compute Saturation
                return delta / max;
            }

            set
            {
                // todo: optimize, can probably skip some of the work
                ToHSV(out var h, out var _, out var v);
                this = FromHSV(h, value, v);
            }
        }

        #endregion

        #region Parse

        public static Color Parse(string color)
        {
            if (TryParse(color, out var @out)) { return @out; }
            else { throw new FormatException($"Unable to parse color, invalid format."); }
        }

        public static bool TryParse(string color, out Color @out)
        {
            try
            {
                // Strip hex prefix or octothorpe character ( if given )
                if (color.StartsWith("0x")) { color = color.Substring(2); }
                color = color.TrimStart('#');

                int r, g, b;
                var a = 0xFF;

                // ABC style
                if (color.Length == 3)
                {
                    r = ParseHexInt(color.Substring(0, 1)) * 0x11;
                    g = ParseHexInt(color.Substring(1, 1)) * 0x11;
                    b = ParseHexInt(color.Substring(2, 1)) * 0x11;
                }

                // ABCD style
                else if (color.Length == 4)
                {
                    a = ParseHexInt(color.Substring(0, 1)) * 0x11;
                    r = ParseHexInt(color.Substring(1, 1)) * 0x11;
                    g = ParseHexInt(color.Substring(2, 1)) * 0x11;
                    b = ParseHexInt(color.Substring(3, 1)) * 0x11;
                }

                // AABBCC style
                else if (color.Length == 6)
                {
                    r = ParseHexInt(color.Substring(0, 2));
                    g = ParseHexInt(color.Substring(2, 2));
                    b = ParseHexInt(color.Substring(4, 2));
                }

                // AABBCCDD style
                else if (color.Length == 8)
                {
                    a = ParseHexInt(color.Substring(0, 2));
                    r = ParseHexInt(color.Substring(2, 2));
                    g = ParseHexInt(color.Substring(4, 2));
                    b = ParseHexInt(color.Substring(6, 2));
                }
                // No known style
                else
                {
                    @out = default;
                    return false;
                }

                // 
                @out = new Color
                {
                    R = r / 255F,
                    G = g / 255F,
                    B = b / 255F,
                    A = a / 255F
                };

                return true;
            }
            catch (Exception)
            {
                @out = default;
                return false;
            }
        }

        private static int ParseHexInt(string str)
        {
            return Convert.ToInt32(str, 16);
        }

        #endregion

        #region HSV Conversion

        public static Color FromHSV(float hue, float saturation, float value, float alpha = 1F)
        // ref: https://github.com/Inseckto/HSV-to-RGB
        {
            var h = hue / 360F;
            var s = saturation;
            var v = value;

            var i = Calc.Floor(h * 6);
            var f = h * 6 - i;
            var p = v * (1 - s);
            var q = v * (1 - f * s);
            var t = v * (1 - (1 - f) * s);

            float r, g, b;

            switch (i % 6)
            {
                default:
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                case 5: r = v; g = p; b = q; break;
            }

            return new Color(r, g, b, alpha);
        }

        public void ToHSV(out float hue, out float saturation, out float value)
        // ref: https://stackoverflow.com/questions/3018313
        {
            var min = Calc.Min(R, G, B);
            var max = Calc.Max(R, G, B);
            var delta = max - min;

            // Compute Value
            value = max;

            if (Calc.NearZero(delta))
            {
                // Pure gray
                saturation = 0;
                hue = 0; // undefined
                return;
            }

            if (max > 0F)
            {
                // Compute Saturation
                saturation = delta / max;
            }
            else
            {
                // Pure black
                saturation = 0;
                hue = 0;
                return;
            }

            // Compute Hue
            if (Calc.NearEquals(R, max))
            {
                hue = (G - B) / delta;
            }
            else
            if (Calc.NearEquals(G, max))
            {
                hue = 2F + (B - R) / delta;
            }
            else
            {
                hue = 4F + (R - G) / delta;
            }

            hue *= 60; // quadrant to degrees

            // Wrap hue to positive range
            if (hue < 0)
            {
                hue += 360;
            }
        }

        #endregion

        /// <summary>
        /// Interpolate two colors together.
        /// </summary>
        /// <param name="source">Source color</param>
        /// <param name="target">Target color.</param>
        /// <param name="factor">Blending factor (0.0 to 1.0)</param>
        /// <returns>The interpolated color.</returns>
        public static Color Lerp(Color source, Color target, float t)
        {
            var r = Calc.Lerp(source.R, target.R, t);
            var g = Calc.Lerp(source.G, target.G, t);
            var b = Calc.Lerp(source.B, target.B, t);
            var a = Calc.Lerp(source.A, target.A, t);

            return new Color(r, g, b, a);
        }

        #region Arithmetic Operators

        public static Color operator +(Color c1, Color c2)
        {
            var r = c1.R + c2.R;
            var g = c1.G + c2.G;
            var b = c1.B + c2.B;
            var a = c1.A + c2.A;

            return new Color(r, g, b, a);
        }

        public static Color operator -(Color c1, Color c2)
        {
            var r = c1.R - c2.R;
            var g = c1.G - c2.G;
            var b = c1.B - c2.B;
            var a = c1.A - c2.A;

            return new Color(r, g, b, a);
        }

        public static Color operator *(Color c1, Color c2)
        {
            var r = c1.R * c2.R;
            var g = c1.G * c2.G;
            var b = c1.B * c2.B;
            var a = c1.A * c2.A;

            return new Color(r, g, b, a);
        }

        public static Color operator /(Color c1, Color c2)
        {
            var r = c1.R / c2.R;
            var g = c1.G / c2.G;
            var b = c1.B / c2.B;
            var a = c1.A / c2.A;

            return new Color(r, g, b, a);
        }

        public static Color operator *(float x, Color c2)
        {
            var r = x * c2.R;
            var g = x * c2.G;
            var b = x * c2.B;
            var a = x * c2.A;

            return new Color(r, g, b, a);
        }

        public static Color operator *(Color c1, float x)
        {
            var r = c1.R * x;
            var g = c1.G * x;
            var b = c1.B * x;
            var a = c1.A * x;

            return new Color(r, g, b, a);
        }

        public static Color operator /(Color c1, float x)
        {
            var r = c1.R / x;
            var g = c1.G / x;
            var b = c1.B / x;
            var a = c1.A / x;

            return new Color(r, g, b, a);
        }

        #endregion

        #region Conversion Operators

        public static explicit operator Pixel(Color c)
        {
            var r = (byte) (c.R * 255);
            var g = (byte) (c.G * 255);
            var b = (byte) (c.B * 255);
            var a = (byte) (c.A * 255);

            return new Pixel(r, g, b, a);
        }

        public static explicit operator uint(Color c)
        {
            return (uint) (Pixel) c;
        }

        public static explicit operator int(Color c)
        {
            return (int) (Pixel) c;
        }

        public static explicit operator Color(uint c)
        {
            return (Color) (Pixel) c;
        }

        public static explicit operator Color(int c)
        {
            return (Color) (Pixel) c;
        }

        #endregion

        #region ToString / Equals / GetHashCode

        public override string ToString()
        {
            return $"({R}, {G}, {B}, {A})";
        }

        public override bool Equals(object obj)
        {
            return obj is Color && Equals((Color) obj);
        }

        public bool Equals(Color other)
        {
            return Calc.NearEquals(R, other.R) &&
                   Calc.NearEquals(G, other.G) &&
                   Calc.NearEquals(B, other.B) &&
                   Calc.NearEquals(A, other.A);
        }

        public override int GetHashCode()
        {
            var hashCode = 1960784236;
            hashCode = hashCode * -1521134295 + R.GetHashCode();
            hashCode = hashCode * -1521134295 + G.GetHashCode();
            hashCode = hashCode * -1521134295 + B.GetHashCode();
            hashCode = hashCode * -1521134295 + A.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Color color1, Color color2)
        {
            return color1.Equals(color2);
        }

        public static bool operator !=(Color color1, Color color2)
        {
            return !(color1 == color2);
        }

        #endregion
    }
}
