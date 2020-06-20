using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using static Heirloom.Calc;

namespace Heirloom
{
    /// <summary>
    /// Color encoded in CIE-L*ab format.
    /// </summary>
    public struct LabColor : IEquatable<LabColor>
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

        /// <summary>
        /// Constructs a new instance of CIE-L*ab color.
        /// </summary>
        public LabColor(float l, float a, float b)
        {
            L = l;
            A = a;
            B = b;
        }

        /// <summary>
        /// Linear interpolation of two colors.
        /// </summary>
        /// <param name="a">The first color.</param>
        /// <param name="b">The second color.</param>
        /// <param name="t">Interpolation factor.</param>
        public static LabColor Lerp(LabColor a, LabColor b, float t)
        {
            return new LabColor
            {
                L = Calc.Lerp(a.L, b.L, t),
                A = Calc.Lerp(a.A, b.A, t),
                B = Calc.Lerp(a.B, b.B, t)
            };
        }

        private static float DistanceSquared1994(in LabColor a, in LabColor b)
        {
            var xDL = b.L - a.L;
            var xDA = a.A - b.A;
            var xDB = a.B - b.B;

            var xC1 = sqrt(pow2(a.A) + pow2(a.B));
            var xC2 = sqrt(pow2(b.A) + pow2(b.B));
            var xDC = xC2 - xC1;

            var xDE = pow2(xDL) + pow2(xDA) + pow2(xDB);

            var xDH = xDE - pow2(xDL) - pow2(xDC);
            xDH = xDH > 0 ? sqrt(xDH) : 0;

            var xSC = 1 + (0.045F * xC1);
            var xSH = 1 + (0.015F * xC1);

            xDC /= xSC;
            xDH /= xSH;

            return pow2(xDL) + pow2(xDC) + pow2(xDH);

            static float sqrt(float x) => Sqrt(x);
            static float pow2(float x) => x * x;
        }

        /// <summary>
        /// Computes the perceived distance between two colors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in LabColor a, in LabColor b)
        {
            return Sqrt(DistanceSquared(in a, in b));
        }

        /// <summary>
        /// Computes the perceived squared distance between two colors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(in LabColor a, in LabColor b)
        {
            return DistanceSquared1994(in a, in b);
        }

        #region Conversion Operators

        public static explicit operator Color(LabColor lab)
        {
            return ColorSpace.LABtoRGB(lab);
        }

        public static explicit operator ColorBytes(LabColor lab)
        {
            return (Color) lab;
        }

        public static explicit operator LabColor(Color rgb)
        {
            return ColorSpace.RGBtoLAB(rgb);
        }

        public static explicit operator LabColor(ColorBytes rgb)
        {
            return (LabColor) (Color) rgb;
        }

        #endregion

        #region Arithmetic Operators

        public static LabColor operator +(LabColor a, LabColor b)
        {
            return new LabColor
            {
                L = a.L + b.L,
                A = a.A + b.A,
                B = a.B + b.B
            };
        }

        public static LabColor operator -(LabColor a, LabColor b)
        {
            return new LabColor
            {
                L = a.L - b.L,
                A = a.A - b.A,
                B = a.B - b.B
            };
        }

        public static LabColor operator *(LabColor a, float x)
        {
            return new LabColor
            {
                L = a.L * x,
                A = a.A * x,
                B = a.B * x
            };
        }

        public static LabColor operator /(LabColor a, float x)
        {
            return new LabColor
            {
                L = a.L / x,
                A = a.A / x,
                B = a.B / x
            };
        }

        #endregion

        #region Equality

        public static bool operator ==(LabColor left, LabColor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LabColor left, LabColor right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(L, A, B);
        }

        public bool Equals([AllowNull] LabColor other)
        {
            return NearEquals(L, other.L)
                && NearEquals(A, other.A)
                && NearEquals(B, other.B);
        }

        public override bool Equals(object other)
        {
            return other is LabColor lab
                && Equals(lab);
        }

        #endregion

        public override string ToString()
        {
            return $"LAB({L:0.00}, {A:0.00}, {B:0.00})";
        }
    }
}
