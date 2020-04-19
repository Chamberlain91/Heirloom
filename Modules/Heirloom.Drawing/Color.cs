using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Color encoded as 4 component floats.
    /// </summary>
    /// <seealso cref="ColorBytes"/>
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

        public static Color Red { get; } = Parse("FF0000");

        public static Color Green { get; } = Parse("00FF00");

        public static Color Blue { get; } = Parse("0000FF");

        public static Color Yellow { get; } = Parse("FFFF00");

        public static Color Cyan { get; } = Parse("00FFFF");

        public static Color Magenta { get; } = Parse("FF00FF");

        public static Color White { get; } = Parse("FFFFFF");

        public static Color Black { get; } = Parse("000000");

        public static Color Gray { get; } = Parse("999999");

        public static Color DarkGray { get; } = Parse("333333");

        public static Color LightGray { get; } = Parse("CCCCCC");

        public static Color Orange { get; } = Parse("FF8811");

        public static Color Indigo { get; } = Parse("FF4B0082");

        public static Color Violet { get; } = Parse("FF8A2BE2");

        public static Color Pink { get; } = Parse("DD55AA");

        public static Color Transparent { get; } = Parse("00000000");

        public static IReadOnlyList<Color> Rainbow { get; } = new[] { Red, Orange, Yellow, Green, Blue, Indigo, Violet };

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

        /// <summary>
        /// Sets the components of this color.
        /// </summary>
        public void Set(float r, float g, float b, float a = 1F)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        #region Parse

        /// <summary>
        /// Parses a hex-string representation of a color. 
        /// May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.
        /// </summary>
        /// <param name="color">The hex-encoded string.</param>
        public static Color Parse(string color)
        {
            if (TryParse(color, out var @out)) { return @out; }
            else { throw new FormatException($"Unable to parse color, invalid format."); }
        }

        /// <summary>
        /// Parses a hex-string representation of a color. 
        /// May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.
        /// </summary>
        /// <param name="color">The hex-encoded string.</param>
        /// <param name="outColor">Outputs the parsed color.</param>
        /// <returns>True if color was successfully parsed, otherwise false.</returns>
        public static bool TryParse(string color, out Color outColor)
        {
            try
            {
                // Strip hex prefix or octothorpe character ( if given )
                if (color.StartsWith("0x")) { color = color.Substring(2); }
                color = color.TrimStart('#');

                int r, g, b;
                var a = 0xFF;

                // RGB style
                if (color.Length == 3)
                {
                    r = ParseHexInt(color.Substring(0, 1)) * 0x11;
                    g = ParseHexInt(color.Substring(1, 1)) * 0x11;
                    b = ParseHexInt(color.Substring(2, 1)) * 0x11;
                }

                // RGBA style
                else if (color.Length == 4)
                {
                    a = ParseHexInt(color.Substring(0, 1)) * 0x11;
                    r = ParseHexInt(color.Substring(1, 1)) * 0x11;
                    g = ParseHexInt(color.Substring(2, 1)) * 0x11;
                    b = ParseHexInt(color.Substring(3, 1)) * 0x11;
                }

                // RRGGBB style
                else if (color.Length == 6)
                {
                    r = ParseHexInt(color.Substring(0, 2));
                    g = ParseHexInt(color.Substring(2, 2));
                    b = ParseHexInt(color.Substring(4, 2));
                }

                // RRGGBBAA style
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
                    outColor = default;
                    return false;
                }

                // 
                outColor = new Color
                {
                    R = r / 255F,
                    G = g / 255F,
                    B = b / 255F,
                    A = a / 255F
                };

                return true;
            }
            catch (FormatException)
            {
                outColor = default;
                return false;
            }

            static int ParseHexInt(string str)
            {
                return Convert.ToInt32(str, 16);
            }
        }

        #endregion

        #region HSV Conversion

        /// <summary>
        /// Converts HSV values into a RGBA color.
        /// </summary>
        /// <param name="hue">The hue (0 to 360).</param>
        /// <param name="saturation">The saturation (0.0 to 1.0).</param>
        /// <param name="value">The value (0.0 to 1.0).</param>
        /// <param name="alpha">The opacity (0.0 to 1.0).</param>
        /// <returns></returns>
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

        /// <summary>
        /// Extracts the HSV values from this color.
        /// </summary>
        /// <param name="hue">The hue (0 to 360).</param>
        /// <param name="saturation">The saturation (0.0 to 1.0).</param>
        /// <param name="value">The value (0.0 to 1.0).</param>
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

        public static implicit operator ColorBytes(Color c)
        {
            var r = (byte) (c.R * 255);
            var g = (byte) (c.G * 255);
            var b = (byte) (c.B * 255);
            var a = (byte) (c.A * 255);

            return new ColorBytes(r, g, b, a);
        }

        public static explicit operator uint(Color c)
        {
            return (uint) (ColorBytes) c;
        }

        public static explicit operator int(Color c)
        {
            return (int) (ColorBytes) c;
        }

        public static explicit operator Color(uint c)
        {
            return (ColorBytes) c;
        }

        public static explicit operator Color(int c)
        {
            return (ColorBytes) c;
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(Color color1, Color color2)
        {
            return color1.Equals(color2);
        }

        public static bool operator !=(Color color1, Color color2)
        {
            return !(color1 == color2);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Color color
                && Equals(color);
        }

        public bool Equals(Color other)
        {
            return Calc.NearEquals(R, other.R)
                && Calc.NearEquals(G, other.G)
                && Calc.NearEquals(B, other.B)
                && Calc.NearEquals(A, other.A);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        #endregion

        public override string ToString()
        {
            return $"({R}, {G}, {B}, {A})";
        }
    }
}
