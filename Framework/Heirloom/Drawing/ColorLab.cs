using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Heirloom.Mathematics;

using static Heirloom.Mathematics.Calc;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Color in CIE-L*ab format encoded as 3 floats.
    /// </summary>
    /// <remarks> Note: This color representation does not contain transparency (alpha). </remarks>
    /// <seealso cref="Color"/>
    /// <seealso cref="ColorBytes"/>
    /// <category>Drawing</category>
    public struct ColorLab : IEquatable<ColorLab>
    {
        /// <summary>
        /// The color's lightness. Ranges from 0 (black) to 100 (white).
        /// </summary>
        public float L;

        /// <summary>
        /// This component ranges from green (-100) to red (+100).
        /// </summary>
        public float A;

        /// <summary>
        /// This component ranges from blue (-100) to yellow (+100).
        /// </summary>
        public float B;

        #region Constants

        /// <summary>
        /// The color red (#FF0000).
        /// </summary>
        public static ColorLab Red { get; } = (ColorLab) Color.Red;

        /// <summary>
        /// The color green (#00FF00).
        /// </summary>
        public static ColorLab Green { get; } = (ColorLab) Color.Green;

        /// <summary>
        /// The color blue (#0000FF).
        /// </summary>
        public static ColorLab Blue { get; } = (ColorLab) Color.Blue;

        /// <summary>
        /// The color yellow (#FFFF00).
        /// </summary>
        public static ColorLab Yellow { get; } = (ColorLab) Color.Yellow;

        /// <summary>
        /// The color cyan (#00FFFF).
        /// </summary>
        public static ColorLab Cyan { get; } = (ColorLab) Color.Cyan;

        /// <summary>
        /// The color magenta (#FF00FF).
        /// </summary>
        public static ColorLab Magenta { get; } = (ColorLab) Color.Magenta;

        /// <summary>
        /// The color white (#FFFFFF).
        /// </summary>
        public static ColorLab White { get; } = (ColorLab) Color.White;

        /// <summary>
        /// The color black (#000000).
        /// </summary>
        public static ColorLab Black { get; } = (ColorLab) Color.Black;

        /// <summary>
        /// The color gray (#999999).
        /// </summary>
        public static ColorLab Gray { get; } = (ColorLab) Color.Gray;

        /// <summary>
        /// The color dark gray (#333333).
        /// </summary>
        public static ColorLab DarkGray { get; } = (ColorLab) Color.DarkGray;

        /// <summary>
        /// The color light gray (#CCCCCC).
        /// </summary>
        public static ColorLab LightGray { get; } = (ColorLab) Color.LightGray;

        /// <summary>
        /// The color orange (#FF8811).
        /// </summary>
        public static ColorLab Orange { get; } = (ColorLab) Color.Orange;

        /// <summary>
        /// The color indigo (#4B0082).
        /// </summary>
        public static ColorLab Indigo { get; } = (ColorLab) Color.Indigo;

        /// <summary>
        /// The color violet (#8A2BE2).
        /// </summary>
        public static ColorLab Violet { get; } = (ColorLab) Color.Violet;

        /// <summary>
        /// The color pink (#DD55AA).
        /// </summary>
        public static ColorLab Pink { get; } = (ColorLab) Color.Pink;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of CIE-L*ab color.
        /// </summary>
        public ColorLab(float l, float a, float b)
        {
            L = l;
            A = a;
            B = b;
        }

        #endregion 

        /// <summary>
        /// Linear interpolation of two colors.
        /// </summary>
        /// <param name="a">The first color.</param>
        /// <param name="b">The second color.</param>
        /// <param name="t">Interpolation factor.</param>
        public static ColorLab Lerp(ColorLab a, ColorLab b, float t)
        {
            return new ColorLab
            {
                L = Calc.Lerp(a.L, b.L, t),
                A = Calc.Lerp(a.A, b.A, t),
                B = Calc.Lerp(a.B, b.B, t)
            };
        }

        #region Distance

        /// <summary>
        /// Computes the perceived distance between two colors (using the 1994 model, for performance reasons).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(ColorLab a, ColorLab b)
        {
            return Sqrt(DistanceSquared(a, b));
        }

        /// <summary>
        /// Computes the perceived squared distance between two colors (using the 1994 model, for performance reasons).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(ColorLab a, ColorLab b)
        {
            var xDL = b.L - a.L;
            var xDA = a.A - b.A;
            var xDB = a.B - b.B;

            var xC1 = Sqrt(Pow2(a.A) + Pow2(a.B));
            var xC2 = Sqrt(Pow2(b.A) + Pow2(b.B));
            var xDC = xC2 - xC1;

            var xDE = Pow2(xDL) + Pow2(xDA) + Pow2(xDB);

            var xDH = xDE - Pow2(xDL) - Pow2(xDC);
            xDH = xDH > 0 ? Sqrt(xDH) : 0;

            var xSC = 1 + (0.045F * xC1);
            var xSH = 1 + (0.015F * xC1);

            xDC /= xSC;
            xDH /= xSH;

            return Pow2(xDL) + Pow2(xDC) + Pow2(xDH);

            static float Pow2(float x) => x * x;
        }

        #endregion

        #region Arithmetic Operators

        /// <summary>
        /// Performs a component-wise sum of two instances of <see cref="ColorLab"/>.
        /// </summary>
        public static ColorLab operator +(ColorLab c1, ColorLab c2)
        {
            return new ColorLab
            {
                L = c1.L + c2.L,
                A = c1.A + c2.A,
                B = c1.B + c2.B
            };
        }

        /// <summary>
        /// Performs a component-wise difference of two instances of <see cref="ColorLab"/>.
        /// </summary>
        public static ColorLab operator -(ColorLab c1, ColorLab c2)
        {
            return new ColorLab
            {
                L = c1.L - c2.L,
                A = c1.A - c2.A,
                B = c1.B - c2.B
            };
        }

        /// <summary>
        /// Performs a component-wise scale of a <see cref="ColorLab"/>.
        /// </summary>
        public static ColorLab operator *(ColorLab c, float x)
        {
            return new ColorLab
            {
                L = c.L * x,
                A = c.A * x,
                B = c.B * x
            };
        }

        /// <summary>
        /// Performs a component-wise scale of a <see cref="ColorLab"/>.
        /// </summary>
        public static ColorLab operator *(float x, ColorLab c)
        {
            return new ColorLab
            {
                L = c.L * x,
                A = c.A * x,
                B = c.B * x
            };
        }

        /// <summary>
        /// Performs a component-wise scale of a <see cref="ColorLab"/>.
        /// </summary>
        public static ColorLab operator /(ColorLab c, float x)
        {
            return new ColorLab
            {
                L = c.L / x,
                A = c.A / x,
                B = c.B / x
            };
        }

        /// <summary>
        /// Performs a component-wise scale of a <see cref="ColorLab"/>.
        /// </summary>
        public static ColorLab operator /(float x, ColorLab c)
        {
            return new ColorLab
            {
                L = x / c.L,
                A = x / c.A,
                B = x / c.B
            };
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts a <see cref="ColorLab"/> into <see cref="Color"/>.
        /// </summary>
        public static explicit operator Color(ColorLab lab)
        {
            return ColorSpace.LABtoRGB(lab);
        }

        /// <summary>
        /// Converts a <see cref="Color"/> into <see cref="ColorLab"/>.
        /// </summary>
        public static explicit operator ColorLab(Color rgb)
        {
            return ColorSpace.RGBtoLAB(rgb);
        }

        /// <summary>
        /// Converts a <see cref="ColorLab"/> into <see cref="ColorBytes"/>.
        /// </summary>
        public static explicit operator ColorBytes(ColorLab lab)
        {
            return (Color) lab;
        }

        /// <summary>
        /// Converts a <see cref="ColorBytes"/> into <see cref="ColorLab"/>.
        /// </summary>
        public static explicit operator ColorLab(ColorBytes rgb)
        {
            return (ColorLab) (Color) rgb;
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two instances of <see cref="ColorLab"/> for equality.
        /// </summary>
        public static bool operator ==(ColorLab left, ColorLab right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="ColorLab"/> for inequality.
        /// </summary>
        public static bool operator !=(ColorLab left, ColorLab right)
        {
            return !(left == right);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="ColorLab"/> for equality with another <see cref="ColorLab"/>.
        /// </summary>
        public bool Equals([AllowNull] ColorLab other)
        {
            return NearEquals(L, other.L)
                && NearEquals(A, other.A)
                && NearEquals(B, other.B);
        }

        /// <summary>
        /// Compares this <see cref="ColorLab"/> for equality with another object.
        /// </summary>
        public override bool Equals(object other)
        {
            return other is ColorLab lab
                && Equals(lab);
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="ColorLab"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(L, A, B);
        }

        #endregion

        /// <summary>
        /// Converts this <see cref="ColorLab"/> into string representation.
        /// </summary>
        public override string ToString()
        {
            return $"LAB({L:0.00}, {A:0.00}, {B:0.00})";
        }
    }
}
