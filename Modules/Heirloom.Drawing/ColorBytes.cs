using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Color encoded as 4 component bytes.
    /// </summary>
    /// <seealso cref="Color"/>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe struct ColorBytes : IEquatable<ColorBytes>
    {
        /// <summary>
        /// The red component.
        /// </summary>
        public byte R;

        /// <summary>
        /// The green component.
        /// </summary>
        public byte G;

        /// <summary>
        /// The blue component.
        /// </summary>
        public byte B;

        /// <summary>
        /// The alpha/transparency component.
        /// </summary>
        public byte A;

        #region Constants

        public static ColorBytes Red { get; } = Parse("FF0000");

        public static ColorBytes Green { get; } = Parse("00FF00");

        public static ColorBytes Blue { get; } = Parse("0000FF");

        public static ColorBytes Yellow { get; } = Parse("FFFF00");

        public static ColorBytes Cyan { get; } = Parse("00FFFF");

        public static ColorBytes Magenta { get; } = Parse("FF00FF");

        public static ColorBytes White { get; } = Parse("FFFFFF");

        public static ColorBytes Black { get; } = Parse("000000");

        public static ColorBytes Gray { get; } = Parse("999999");

        public static ColorBytes DarkGray { get; } = Parse("333333");

        public static ColorBytes LightGray { get; } = Parse("CCCCCC");

        public static ColorBytes Orange { get; } = Parse("FF8811");

        public static ColorBytes Indigo { get; } = Parse("FF4B0082");

        public static ColorBytes Violet { get; } = Parse("FF8A2BE2");

        public static ColorBytes Pink { get; } = Parse("DD55AA");

        public static ColorBytes Transparent { get; } = Parse("00000000");

        public static IReadOnlyList<ColorBytes> Rainbow { get; } = new[] { Red, Orange, Yellow, Green, Blue, Indigo, Violet };

        public static ColorBytes Random
            => new ColorBytes((byte) (Calc.Random.Next() & 0xFF), (byte) (Calc.Random.Next() & 0xFF), (byte) (Calc.Random.Next() & 0xFF));

        #endregion

        #region Constructors

        public ColorBytes(byte r, byte g, byte b, byte a = 255)
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
        public byte Luminosity
            => (byte) ((R * 299 + G * 587 + B * 114) * A / (1000 * 255));

        /// <summary>
        /// The inversion of this color.
        /// </summary>
        public ColorBytes Inverted => new ColorBytes((byte) (0xFF - R), (byte) (0xFF - G), (byte) (0xFF - B), A);

        #endregion

        /// <summary>
        /// Sets the components of this color.
        /// </summary>
        public void Set(byte r, byte g, byte b, byte a = 0xFF)
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
        public static ColorBytes Parse(string color)
        {
            if (TryParse(color, out var outColor)) { return outColor; }
            else { throw new FormatException($"Unable to parse color, invalid format."); }
        }

        /// <summary>
        /// Parses a hex-string representation of a color. 
        /// May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.
        /// </summary>
        /// <param name="color">The hex-encoded string.</param>
        /// <param name="outColor">Outputs the parsed color.</param>
        /// <returns>True if color was successfully parsed, otherwise false.</returns>
        public static bool TryParse(string color, out ColorBytes outColor)
        {
            if (string.IsNullOrWhiteSpace(color)) { throw new ArgumentException("Color string must not be blank or null.", nameof(color)); }

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
                outColor = new ColorBytes
                {
                    R = (byte) r,
                    G = (byte) g,
                    B = (byte) b,
                    A = (byte) a
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

        /// <summary>
        /// Interpolate two colors together.
        /// </summary>
        /// <param name="source">Source color</param>
        /// <param name="target">Target color.</param>
        /// <param name="factor">Blending factor (0.0 to 1.0)</param>
        /// <returns>The interpolated color.</returns>
        public static ColorBytes Lerp(ColorBytes source, ColorBytes target, float factor)
        {
            var r = Calc.Lerp(source.R, target.R, factor);
            var g = Calc.Lerp(source.G, target.G, factor);
            var b = Calc.Lerp(source.B, target.B, factor);
            var a = Calc.Lerp(source.A, target.A, factor);

            return new ColorBytes(r, g, b, a);
        }

        /// <summary>
        /// Multiplies two <see cref="ColorBytes"/> together.
        /// Behaves the same as <see cref="Color"/>.
        /// </summary>
        public static void Multiply(in ColorBytes c1, in ColorBytes c2, ref ColorBytes target)
        {
            target.R = (byte) (c1.R * c2.R / 255);
            target.G = (byte) (c1.G * c2.G / 255);
            target.B = (byte) (c1.B * c2.B / 255);
            target.A = (byte) (c1.A * c2.A / 255);
        }

        #region Arithmetic Operators

        public static ColorBytes operator +(ColorBytes c1, ColorBytes c2)
        {
            var r = c1.R + c2.R;
            var g = c1.G + c2.G;
            var b = c1.B + c2.B;
            var a = c1.A + c2.A;

            return new ColorBytes((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static ColorBytes operator -(ColorBytes c1, ColorBytes c2)
        {
            var r = c1.R - c2.R;
            var g = c1.G - c2.G;
            var b = c1.B - c2.B;
            var a = c1.A - c2.A;

            return new ColorBytes((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static ColorBytes operator *(ColorBytes c1, ColorBytes c2)
        {
            Multiply(in c1, in c2, ref c1);
            return c1;
        }

        public static ColorBytes operator /(ColorBytes c1, ColorBytes c2)
        {
            var r = c1.R * 255 / c2.R;
            var g = c1.G * 255 / c2.G;
            var b = c1.B * 255 / c2.B;
            var a = c1.A * 255 / c2.A;

            return new ColorBytes((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static ColorBytes operator *(float x, ColorBytes c2)
        {
            var r = x * c2.R / 255;
            var g = x * c2.G / 255;
            var b = x * c2.B / 255;
            var a = x * c2.A / 255;

            return new ColorBytes((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static ColorBytes operator *(ColorBytes c1, int x)
        {
            var r = c1.R * x / 255;
            var g = c1.G * x / 255;
            var b = c1.B * x / 255;
            var a = c1.A * x / 255;

            return new ColorBytes((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static ColorBytes operator /(ColorBytes c1, float x)
        {
            var r = c1.R * 255 / x;
            var g = c1.G * 255 / x;
            var b = c1.B * 255 / x;
            var a = c1.A * 255 / x;

            return new ColorBytes((byte) r, (byte) g, (byte) b, (byte) a);
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Color(ColorBytes p)
        {
            var r = p.R / 255F;
            var g = p.G / 255F;
            var b = p.B / 255F;
            var a = p.A / 255F;

            return new Color(r, g, b, a);
        }

        public static explicit operator uint(ColorBytes p)
        {
            return *(uint*) &p;
        }

        public static explicit operator int(ColorBytes p)
        {
            return *(int*) &p;
        }

        public static explicit operator ColorBytes(uint p)
        {
            return *(ColorBytes*) &p;
        }

        public static explicit operator ColorBytes(int p)
        {
            return *(ColorBytes*) &p;
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(ColorBytes color1, ColorBytes color2)
        {
            return color1.Equals(color2);
        }

        public static bool operator !=(ColorBytes color1, ColorBytes color2)
        {
            return !(color1 == color2);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is ColorBytes bytes
                && Equals(bytes);
        }

        public bool Equals(ColorBytes other)
        {
            return R == other.R
                && G == other.G
                && B == other.B
                && A == other.A;
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

        #endregion

        public override string ToString()
        {
            return $"({R}, {G}, {B}, {A})";
        }
    }
}
