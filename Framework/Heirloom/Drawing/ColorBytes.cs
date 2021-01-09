using System;
using System.Runtime.InteropServices;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Color in RGBA format encoded as 4 component bytes.
    /// </summary>
    /// <seealso cref="Color"/>
    /// <seealso cref="ColorLab"/>
    /// <category>Drawing</category>
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

        /// <summary>
        /// The color red (#FF0000).
        /// </summary>
        public static ColorBytes Red { get; } = Parse("FF0000");

        /// <summary>
        /// The color green (#00FF00).
        /// </summary>
        public static ColorBytes Green { get; } = Parse("00FF00");

        /// <summary>
        /// The color blue (#0000FF).
        /// </summary>
        public static ColorBytes Blue { get; } = Parse("0000FF");

        /// <summary>
        /// The color yellow (#FFFF00).
        /// </summary>
        public static ColorBytes Yellow { get; } = Parse("FFFF00");

        /// <summary>
        /// The color cyan (#00FFFF).
        /// </summary>
        public static ColorBytes Cyan { get; } = Parse("00FFFF");

        /// <summary>
        /// The color magenta (#FF00FF).
        /// </summary>
        public static ColorBytes Magenta { get; } = Parse("FF00FF");

        /// <summary>
        /// The color white (#FFFFFF).
        /// </summary>
        public static ColorBytes White { get; } = Parse("FFFFFF");

        /// <summary>
        /// The color black (#000000).
        /// </summary>
        public static ColorBytes Black { get; } = Parse("000000");

        /// <summary>
        /// The color gray (#999999).
        /// </summary>
        public static ColorBytes Gray { get; } = Parse("999999");

        /// <summary>
        /// The color dark gray (#333333).
        /// </summary>
        public static ColorBytes DarkGray { get; } = Parse("333333");

        /// <summary>
        /// The color light gray (#CCCCCC).
        /// </summary>
        public static ColorBytes LightGray { get; } = Parse("CCCCCC");

        /// <summary>
        /// The color orange (#FF8811).
        /// </summary>
        public static ColorBytes Orange { get; } = Parse("FF8811");

        /// <summary>
        /// The color indigo (#4B0082).
        /// </summary>
        public static ColorBytes Indigo { get; } = Parse("4B0082");

        /// <summary>
        /// The color violet (#8A2BE2).
        /// </summary>
        public static ColorBytes Violet { get; } = Parse("8A2BE2");

        /// <summary>
        /// The color pink (#DD55AA).
        /// </summary>
        public static ColorBytes Pink { get; } = Parse("DD55AA");

        /// <summary>
        /// The color transparent black (#00000000).
        /// </summary>
        public static ColorBytes Transparent { get; } = Parse("00000000");

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="ColorBytes"/>.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
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
            => (byte) (((R * 2126) + (G * 7152) + (B * 722)) / 10000);

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
            var r = (byte) Calc.Lerp(source.R, target.R, factor);
            var g = (byte) Calc.Lerp(source.G, target.G, factor);
            var b = (byte) Calc.Lerp(source.B, target.B, factor);
            var a = (byte) Calc.Lerp(source.A, target.A, factor);

            return new ColorBytes(r, g, b, a);
        }

        #region Arithmetic Operators

        /// <summary>
        /// Performs a component-wise sum of two instances of <see cref="ColorBytes"/>.
        /// </summary>
        public static ColorBytes operator +(ColorBytes c1, ColorBytes c2)
        {
            var r = (byte) (c1.R + c2.R);
            var g = (byte) (c1.G + c2.G);
            var b = (byte) (c1.B + c2.B);
            var a = (byte) (c1.A + c2.A);

            return new ColorBytes(r, g, b, a);
        }

        /// <summary>
        /// Performs a component-wise difference of two instances of <see cref="ColorBytes"/>.
        /// </summary>
        public static ColorBytes operator -(ColorBytes c1, ColorBytes c2)
        {
            var r = (byte) (c1.R - c2.R);
            var g = (byte) (c1.G - c2.G);
            var b = (byte) (c1.B - c2.B);
            var a = (byte) (c1.A - c2.A);

            return new ColorBytes(r, g, b, a);
        }

        /// <summary>
        /// Performs a component-wise multiplication of two instances of <see cref="ColorBytes"/>, normalizing back into byte range.
        /// </summary>
        public static ColorBytes operator *(ColorBytes c1, ColorBytes c2)
        {
            var r = (byte) (c1.R * c2.R / 255);
            var g = (byte) (c1.G * c2.G / 255);
            var b = (byte) (c1.B * c2.B / 255);
            var a = (byte) (c1.A * c2.A / 255);

            return new ColorBytes(r, g, b, a);
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts a <see cref="ColorBytes"/> into <see cref="Color"/>.
        /// </summary>
        public static implicit operator Color(ColorBytes c)
        {
            var r = c.R / 255F;
            var g = c.G / 255F;
            var b = c.B / 255F;
            var a = c.A / 255F;

            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Converts a <see cref="ColorBytes"/> structure into 32 bit integer representation.
        /// </summary>
        public static explicit operator uint(ColorBytes p)
        {
            return *(uint*) &p;
        }

        /// <summary>
        /// Converts a <see cref="ColorBytes"/> structure into 32 bit integer representation.
        /// </summary>
        public static explicit operator int(ColorBytes p)
        {
            return *(int*) &p;
        }

        /// <summary>
        /// Converts the integer representation of a 32 bit color into a <see cref="ColorBytes"/> structure.
        /// </summary>
        public static explicit operator ColorBytes(uint p)
        {
            return *(ColorBytes*) &p;
        }

        /// <summary>
        /// Converts the integer representation of a 32 bit color into a <see cref="ColorBytes"/> structure.
        /// </summary>
        public static explicit operator ColorBytes(int p)
        {
            return *(ColorBytes*) &p;
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two instances of <see cref="ColorBytes"/> for equality.
        /// </summary>
        public static bool operator ==(ColorBytes color1, ColorBytes color2)
        {
            return color1.Equals(color2);
        }

        /// <summary>
        /// Compares two instances of <see cref="ColorBytes"/> for inequality.
        /// </summary>
        public static bool operator !=(ColorBytes color1, ColorBytes color2)
        {
            return !(color1 == color2);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="ColorBytes"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is ColorBytes bytes
                && Equals(bytes);
        }

        /// <summary>
        /// Compares this <see cref="ColorBytes"/> for equality with another <see cref="ColorBytes"/>.
        /// </summary>
        public bool Equals(ColorBytes other)
        {
            return R == other.R
                && G == other.G
                && B == other.B
                && A == other.A;
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="ColorBytes"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        #endregion

        /// <summary>
        /// Converts this <see cref="ColorBytes"/> into string representation.
        /// </summary>
        public override string ToString()
        {
            return $"RGBA({R}, {G}, {B}, {A})";
        }
    }
}
