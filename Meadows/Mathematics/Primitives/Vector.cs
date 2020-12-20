using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Meadows.Mathematics
{
    /// <summary>
    /// Represents a vector with two single-precision floating-point values.
    /// </summary>
    /// <category>Mathematics</category>
    [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 8)]
    public unsafe struct Vector : IEquatable<Vector>, IFormattable
    {
        [FieldOffset(0)]
        private fixed float _components[4];

        /// <summary>
        /// The x-component of this vector.
        /// </summary>
        [FieldOffset(0 * 4)]
        public float X;

        /// <summary>
        /// The y-component of this vector.
        /// </summary>
        [FieldOffset(1 * 4)]
        public float Y;

        #region Constants

        /// <summary>
        /// A vector with value (0, 0).
        /// </summary>
        public static readonly Vector Zero = new Vector(0, 0);

        /// <summary>
        /// A vector with value (1, 1).
        /// </summary>
        public static readonly Vector One = new Vector(1, 1);

        /// <summary>
        /// A vector with value (1, 0).
        /// </summary>
        public static readonly Vector Right = new Vector(1, 0);

        /// <summary>
        /// A vector with value (0, -1).
        /// </summary>
        public static readonly Vector Up = new Vector(0, -1);

        /// <summary>
        /// A vector with value (-1, 0).
        /// </summary>
        public static readonly Vector Left = new Vector(-1, 0);

        /// <summary>
        /// A vector with value (0, 1).
        /// </summary>
        public static readonly Vector Down = new Vector(0, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="Vector"/>.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the magnitude of this vector.
        /// </summary>
        public float Length
        {
            get
            {
                var sqr = LengthSquared;
                return Calc.NearZero(sqr) ? 0 : Calc.Sqrt(LengthSquared);
            }
        }

        /// <summary>
        /// Gets the squared magnitude of this vector.
        /// </summary>
        public float LengthSquared => Dot(in this, in this);

        /// <summary>
        /// Gets a normalized copy of this vector.
        /// </summary>
        public Vector Normalized => Normalize(this);

        /// <summary>
        /// Gets a perpendicular (anti-clockwise) copy of this vector.
        /// </summary>
        public Vector Perpendicular => Cross(this, 1.0f);

        /// <summary>
        /// Gets the angle this vector is pointing with reference to <see cref="Right"/>.
        /// </summary>
        public float Angle
        {
            get
            {
                // Degenerate vector
                if (Calc.NearZero(X) && Calc.NearZero(Y))
                {
                    return 0;
                }

                // Gets the angle of the vector
                // todo: do we have to be normalized?
                var angle = Calc.Atan2(Y, X);
                if (angle < 0) { angle += Calc.TwoPi; }
                return angle;
            }
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Accesses the <see cref="X"/> and <see cref="Y"/> components by numerical index in respective order.
        /// </summary>
        public float this[int i]
        {
            get => _components[i];
            set => _components[i] = value;
        }

        #endregion

        /// <summary>
        /// Sets the components of this vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float x, float y)
        {
            X = x;
            Y = y;
        }

        #region Min / Max / Abs

        /// <summary>
        /// Gets the maximal component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetMaxComponent(Vector vec)
        {
            return Calc.Max(vec.X, vec.Y);
        }

        /// <summary>
        /// Gets the minimal component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetMinComponent(Vector vec)
        {
            return Calc.Min(vec.X, vec.Y);
        }

        /// <summary>
        /// Computes a new vector where each component is the minimum component in each respective input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Min(Vector a, Vector b)
        {
            var x = Calc.Min(a.X, b.X);
            var y = Calc.Min(a.Y, b.Y);

            return new Vector(x, y);
        }

        /// <summary>
        /// Computes a new vector where each component is the maximum component in each respective input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Max(Vector a, Vector b)
        {
            var x = Calc.Max(a.X, b.X);
            var y = Calc.Max(a.Y, b.Y);

            return new Vector(x, y);
        }

        /// <summary>
        /// Computes a new vector where each component is the absolute value of each component in the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Abs(Vector vec)
        {
            var x = Calc.Abs(vec.X);
            var y = Calc.Abs(vec.Y);

            return new Vector(x, y);
        }

        #endregion

        #region Distance

        /// <summary>
        /// Computes the euclidean distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in Vector a, in Vector b)
        {
            return Calc.Distance(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Computes the squared euclidean distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(in Vector a, in Vector b)
        {
            return Calc.DistanceSquared(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Computes the manhattan distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ManhattanDistance(in Vector a, in Vector b)
        {
            return Calc.ManhattanDistance(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Computes an approximation to euclidean distance between any two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ApproximateDistance(in Vector a, in Vector b)
        {
            return Calc.ApproximateDistance(a.X, a.Y, b.X, b.Y);
        }

        #endregion

        #region Normalize

        /// <summary>
        /// Normalize this vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            Normalize(ref this);
        }

        /// <summary>
        /// Normalizes the input vector and return it.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Normalize(Vector vec)
        {
            Normalize(ref vec);
            return vec;
        }

        /// <summary>
        /// Normalizes the input vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Vector vec)
        {
            var length = vec.Length;

            // Avoid divide by zero
            if (length == 0F || length < Calc.Epsilon)
            {
                // Improper behaviour, but should be more stable than NaN?
                vec = Zero;
            }
            else
            {
                // Normalize (make unit length)
                vec /= length;
            }
        }

        #endregion

        #region Interpolation

        /// <summary>
        /// Interpolate two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Lerp(Vector from, Vector to, float t)
        {
            var x = Calc.Lerp(from.X, to.X, t);
            var y = Calc.Lerp(from.Y, to.Y, t);

            return new Vector(x, y);
        }

        #endregion

        #region Create / Trig / Linear

        /// <summary>
        /// Creates a unit length vector with the given angle from the x-axis.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector FromAngle(float angle)
        {
            var x = Calc.Cos(angle);
            var y = Calc.Sin(angle);

            return new Vector(x, y);
        }

        /// <summary>
        /// Computes the angle (in radians) between two vectors (using dot product).
        /// </summary>
        public static float AngleBetween(Vector a, Vector b)
        {
            // Normalize to remove magnitude in the dot product
            Normalize(a);
            Normalize(b);

            // Invert cosine to get the angle
            var dot = (a.X * b.X) + (a.Y * b.Y);
            if (float.IsNaN(dot)) { return 0; }
            else { return Calc.Acos(dot); }
        }

        /// <summary>
        /// Rotates a vector by the specified angle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Rotate(Vector v, float angle)
        {
            // todo: rotation type?
            var cos = Calc.Cos(angle);
            var sin = Calc.Sin(angle);

            // R * v
            return new Vector
            {
                X = cos * v.X - sin * v.Y,
                Y = sin * v.X + cos * v.Y
            };
        }

        #endregion

        #region Dot (Scalar) Product 

        /// <summary>
        /// Computes the dot-product of two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(in Vector a, in Vector b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        #endregion

        #region Cross Product 

        /// <summary>
        /// Computes the cross-product of two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cross(in Vector a, in Vector b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        /// <summary>
        /// Computes the cross-product of a vector and a magnitude.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Cross(in Vector a, float s)
        {
            return new Vector(s * a.Y, -s * a.X);
        }

        /// <summary>
        /// Computes the cross-product of a vector and a magnitude.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Cross(float s, in Vector a)
        {
            return new Vector(-s * a.Y, s * a.X);
        }

        #endregion

        #region Projection

        /// <summary>
        /// Projects the first vector onto the second.
        /// </summary>
        /// <param name="u"> The first vector. </param>
        /// <param name="v"> The second vector. </param>
        /// <returns>The 'progress' along <paramref name="v"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Project(in Vector u, in Vector v)
        {
            return Dot(in u, in v) / Dot(in v, in v);
        }

        /// <summary>
        /// Projects a point onto a line segment.
        /// </summary>
        /// <param name="start">Starting point of the line segment.</param>
        /// <param name="end">Ending point of the line segment.</param>
        /// <param name="point">Point to project.</param>
        /// <param name="clamp">Should we clamp to the ends of the line segment?</param>
        /// <returns>The 'progress' along the line segment.</returns>
        public static float Project(in Vector start, in Vector end, in Vector point, bool clamp = true)
        {
            var v = point - start;
            var e = end - start;

            // time along edge
            var t = Project(v, e);

            if (clamp)
            {
                t = Calc.Clamp(t, 0, 1);
            }

            // form point and return
            return t;
        }

        #endregion

        #region Reflection

        /// <summary>
        /// Computes the reflection of the input vector about the specified axis.
        /// </summary>
        /// <param name="vec">The vector to reflect.</param>
        /// <param name="axis">The axis of reflection, normalized.</param>
        public static Vector Reflect(in Vector vec, in Vector axis)
        {
            return vec - (2 * Dot(vec, axis) * axis);
        }

        #endregion

        #region Rounding

        /// <summary>
        /// Computes a new vector with the floor of each component of the input vector.
        /// </summary>
        /// <see cref="Calc.Floor(float)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Floor(Vector v)
        {
            v.X = Calc.Floor(v.X);
            v.Y = Calc.Floor(v.Y);
            return v;
        }

        /// <summary>
        /// Computes a new vector with the ceiling of each component of the input vector.
        /// </summary>
        /// <see cref="Calc.Ceil(float)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Ceil(Vector v)
        {
            v.X = Calc.Ceil(v.X);
            v.Y = Calc.Ceil(v.Y);
            return v;
        }

        /// <summary>
        /// Computes a new vector with the rounded value of each component of the input vector.
        /// </summary>
        /// <see cref="Calc.Round(float)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Round(Vector v)
        {
            v.X = Calc.Round(v.X);
            v.Y = Calc.Round(v.Y);
            return v;
        }

        /// <summary>
        /// Computes a new vector with the fractional portion of each component of the input vector.
        /// </summary>
        /// <see cref="Calc.Fraction(float)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Fraction(Vector v)
        {
            v.X = Calc.Fraction(v.X);
            v.Y = Calc.Fraction(v.Y);
            return v;
        }

        #endregion

        #region Deconstruct

        /// <summary>
        /// Deconstructs this <see cref="Vector"/> into constituent components.
        /// </summary>
        /// <param name="x">Outputs the x component.</param>
        /// <param name="y">Outputs the y component.</param>
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Converts a <see cref="Vector"/> into <see cref="Size"/>.
        /// </summary>
        public static explicit operator Size(Vector vec)
        {
            var width = vec.X;
            var height = vec.Y;

            return new Size(width, height);
        }

        /// <summary>
        /// Converts a <see cref="Vector"/> into <see cref="IntSize"/> by truncating each component to integer.
        /// </summary>
        public static explicit operator IntSize(Vector vec)
        {
            var width = (int) vec.X;
            var height = (int) vec.Y;

            return new IntSize(width, height);
        }

        /// <summary>
        /// Converts a <see cref="Vector"/> into <see cref="IntVector"/> by truncating each component to integer.
        /// </summary>
        public static explicit operator IntVector(Vector vec)
        {
            var x = (int) vec.X;
            var y = (int) vec.Y;

            return new IntVector(x, y);
        }

        /// <summary>
        /// Converts the x, y formatted tuple into <see cref="Vector"/>.
        /// </summary>
        public static implicit operator Vector((float x, float y) vec)
        {
            return new Vector(vec.x, vec.y);
        }

        /// <summary>
        /// Converts the <see cref="Vector"/> into a x, y formatted tuple.
        /// </summary>
        public static implicit operator (float x, float y)(Vector vec)
        {
            return (vec.X, vec.Y);
        }

        #endregion

        #region Arithmetic Operators

        /// <summary>
        /// Negates a <see cref="Vector"/>.
        /// </summary>
        public static Vector operator -(Vector v)
        {
            var x = -v.X;
            var y = -v.Y;

            return new Vector(x, y);
        }

        /// <summary>
        /// Returns the same vector.
        /// </summary>
        public static Vector operator +(Vector v)
        {
            return v;
        }

        #region Addition

        /// <summary>
        /// Performs the addition of two vectors.
        /// </summary>
        public static Vector operator +(Vector a, Vector b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;

            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the addition of two vectors.
        /// </summary>
        public static Vector operator +(Vector a, IntVector b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;

            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the addition of two vectors.
        /// </summary>
        public static Vector operator +(IntVector a, Vector b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;

            return new Vector(x, y);
        }

        #endregion

        #region Subtraction

        /// <summary>
        /// Performs the subtraction of two vectors.
        /// </summary>
        public static Vector operator -(Vector a, Vector b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the subtraction of two vectors.
        /// </summary>
        public static Vector operator -(Vector a, IntVector b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the subtraction of two vectors.
        /// </summary>
        public static Vector operator -(IntVector a, Vector b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return new Vector(x, y);
        }

        #endregion

        #region Multiply

        /// <summary>
        /// Performs the component-wise multiplication of two vectors.
        /// </summary>
        public static Vector operator *(Vector a, Vector b)
        {
            var x = a.X * b.X;
            var y = a.Y * b.Y;

            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the scaling of a vector.
        /// </summary>
        public static Vector operator *(Vector a, float b)
        {
            var x = a.X * b;
            var y = a.Y * b;

            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the scaling of a vector.
        /// </summary>
        public static Vector operator *(float a, Vector b)
        {
            var x = a * b.X;
            var y = a * b.Y;

            return new Vector(x, y);
        }

        #endregion

        #region Divide

        /// <summary>
        /// Performs the component-wise division of two vectors.
        /// </summary>
        public static Vector operator /(Vector a, Vector b)
        {
            var x = a.X / b.X;
            var y = a.Y / b.Y;

            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the scaling of a vector via per-component division.
        /// </summary>
        public static Vector operator /(float a, Vector b)
        {
            var x = a / b.X;
            var y = a / b.Y;

            return new Vector(x, y);
        }

        /// <summary>
        /// Performs the scaling of a vector via per-component division.
        /// </summary>
        public static Vector operator /(Vector a, float b)
        {
            var x = a.X / b;
            var y = a.Y / b;

            return new Vector(x, y);
        }

        #endregion

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two vectors for equality.
        /// </summary>
        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Compares two vectors for inequality.
        /// </summary>
        public static bool operator !=(Vector v1, Vector v2)
        {
            return v1.Equals(v2) == false;
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this vector for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Vector vec
                && Equals(vec);
        }

        /// <summary>
        /// Compares this vector for equality with another vector.
        /// </summary>
        public bool Equals(Vector other)
        {
            return Calc.NearEquals(X, other.X)
                && Calc.NearEquals(Y, other.Y);
        }

        /// <summary>
        /// Returns the hash code for this vector.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        #endregion

        /// <summary>
        /// Converts this <see cref="Vector"/> into string representation.
        /// </summary>
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        /// <summary>
        /// Converts this <see cref="Vector"/> into string representation.
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var _X = X.ToString(format, formatProvider);
            var _Y = Y.ToString(format, formatProvider);
            return $"({_X}, {_Y})";
        }
    }
}
