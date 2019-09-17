using System;
using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Color encoded as 4 component bytes.
    /// </summary>
    /// <seealso cref="Color"/>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe struct Pixel : IEquatable<Pixel>
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

        public static readonly Pixel Red = Parse("FF0000");

        public static readonly Pixel Green = Parse("00FF00");

        public static readonly Pixel Blue = Parse("0000FF");

        public static readonly Pixel Yellow = Parse("FFFF00");

        public static readonly Pixel Cyan = Parse("00FFFF");

        public static readonly Pixel Magenta = Parse("FF00FF");

        public static readonly Pixel White = Parse("FFFFFF");

        public static readonly Pixel Black = Parse("000000");

        public static readonly Pixel Gray = Parse("999999");

        public static readonly Pixel DarkGray = Parse("333333");

        public static readonly Pixel LightGray = Parse("CCCCCC");

        public static readonly Pixel Transparent = Parse("00000000");

        #endregion

        #region Constructors

        public Pixel(byte r, byte g, byte b, byte a = 255)
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
        public Pixel Inverted => new Pixel((byte) (0xFF - R), (byte) (0xFF - G), (byte) (0xFF - B), A);

        #endregion

        #region Parse

        public static Pixel Parse(string color)
        {
            if (TryParse(color, out var @out)) { return @out; }
            else { throw new FormatException($"Unable to parse color, invalid format."); }
        }

        public static bool TryParse(string color, out Pixel @out)
        {
            // Strip octothorpe character ( if given )
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
            @out = new Pixel
            {
                R = (byte) r,
                G = (byte) g,
                B = (byte) b,
                A = (byte) a
            };

            return true;
        }

        private static int ParseHexInt(string str)
        {
            return Convert.ToInt32(str, 16);
        }

        #endregion

        /// <summary>
        /// Interpolate two colors together.
        /// </summary>
        /// <param name="source">Source color</param>
        /// <param name="target">Target color.</param>
        /// <param name="factor">Blending factor (0.0 to 1.0)</param>
        /// <returns>The interpolated color.</returns>
        public static Pixel Lerp(Pixel source, Pixel target, float factor)
        {
            var r = Calc.Lerp(source.R, target.R, factor);
            var g = Calc.Lerp(source.G, target.G, factor);
            var b = Calc.Lerp(source.B, target.B, factor);
            var a = Calc.Lerp(source.A, target.A, factor);

            return new Pixel(r, g, b, a);
        }

        public static void Multiply(in Pixel c1, in Pixel c2, ref Pixel target)
        {
            target.R = (byte) (c1.R * c2.R / 255);
            target.G = (byte) (c1.G * c2.G / 255);
            target.B = (byte) (c1.B * c2.B / 255);
            target.A = (byte) (c1.A * c2.A / 255);
        }

        #region Arithmetic Operators

        public static Pixel operator +(Pixel c1, Pixel c2)
        {
            var r = c1.R + c2.R;
            var g = c1.G + c2.G;
            var b = c1.B + c2.B;
            var a = c1.A + c2.A;

            return new Pixel((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static Pixel operator -(Pixel c1, Pixel c2)
        {
            var r = c1.R - c2.R;
            var g = c1.G - c2.G;
            var b = c1.B - c2.B;
            var a = c1.A - c2.A;

            return new Pixel((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static Pixel operator *(Pixel c1, Pixel c2)
        {
            Multiply(in c1, in c2, ref c1);
            return c1;
        }

        public static Pixel operator /(Pixel c1, Pixel c2)
        {
            var r = c1.R * 255 / c2.R;
            var g = c1.G * 255 / c2.G;
            var b = c1.B * 255 / c2.B;
            var a = c1.A * 255 / c2.A;

            return new Pixel((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static Pixel operator *(float x, Pixel c2)
        {
            var r = x * c2.R / 255;
            var g = x * c2.G / 255;
            var b = x * c2.B / 255;
            var a = x * c2.A / 255;

            return new Pixel((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static Pixel operator *(Pixel c1, int x)
        {
            var r = c1.R * x / 255;
            var g = c1.G * x / 255;
            var b = c1.B * x / 255;
            var a = c1.A * x / 255;

            return new Pixel((byte) r, (byte) g, (byte) b, (byte) a);
        }

        public static Pixel operator /(Pixel c1, float x)
        {
            var r = c1.R * 255 / x;
            var g = c1.G * 255 / x;
            var b = c1.B * 255 / x;
            var a = c1.A * 255 / x;

            return new Pixel((byte) r, (byte) g, (byte) b, (byte) a);
        }

        #endregion

        #region Conversion Operators

        public static explicit operator Color(Pixel p)
        {
            var r = p.R / 255F;
            var g = p.G / 255F;
            var b = p.B / 255F;
            var a = p.A / 255F;

            return new Color(r, g, b, a);
        }

        public static explicit operator uint(Pixel p)
        {
            return *(uint*) &p;
        }

        public static explicit operator int(Pixel p)
        {
            return *(int*) &p;
        }

        public static explicit operator Pixel(uint p)
        {
            return *(Pixel*) &p;
        }

        public static explicit operator Pixel(int p)
        {
            return *(Pixel*) &p;
        }

        #endregion

        #region Comparison Operators

        public static bool operator ==(Pixel color1, Pixel color2)
        {
            return color1.Equals(color2);
        }

        public static bool operator !=(Pixel color1, Pixel color2)
        {
            return !(color1 == color2);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Pixel && Equals((Pixel) obj);
        }

        public bool Equals(Pixel other)
        {
            return R == other.R &&
                   G == other.G &&
                   B == other.B &&
                   A == other.A;
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
