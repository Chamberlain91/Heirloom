using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Math
{
    /// <summary>
    /// A 2x3 transformation matrix.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 4 * 6, Pack = 4)]
    public struct Matrix : IEquatable<Matrix>
    {
        // row 0
        public float M0; // sx *  c
        public float M1; // sy * -s
        public float M2; // tx

        // row 1
        public float M3; // sx *  s
        public float M4; // sy *  c
        public float M5; // ty 

        // 3x3
        // note, for glsl uniform buffers the missing float in each row
        // 0 1 2 x
        // 3 4 5 x
        // 6 7 8 x

        #region Constants

        /// <summary>
        /// The identity matrix.
        /// </summary>
        public static readonly Matrix Identity = new Matrix(1f, 0f, 0f, 0f, 1f, 0f);

        #endregion

        #region Indexer

        public float this[int r, int c]
        {
            get
            {
                switch (c)
                {
                    case 0:
                        if (r == 0) { return M0; }
                        if (r == 1) { return M3; }
                        break;

                    case 1:
                        if (r == 0) { return M1; }
                        if (r == 1) { return M4; }
                        break;

                    case 2:
                        if (r == 0) { return M2; }
                        if (r == 1) { return M5; }
                        break;
                }

                throw new InvalidOperationException();
            }

            set
            {
                if (r < 0 || r > 2) { throw new InvalidOperationException(); }
                if (c < 0 || c > 2) { throw new InvalidOperationException(); }

                switch (c)
                {
                    case 0:
                        if (r == 0) { M0 = value; }
                        if (r == 1) { M3 = value; }
                        break;

                    case 1:
                        if (r == 0) { M1 = value; }
                        if (r == 1) { M4 = value; }
                        break;

                    case 2:
                        if (r == 0) { M2 = value; }
                        if (r == 1) { M5 = value; }
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return M0;
                    case 1: return M1;
                    case 2: return M2;
                    case 3: return M3;
                    case 4: return M4;
                    case 5: return M5;

                    default:
                        throw new InvalidOperationException();
                }
            }

            set
            {
                switch (i)
                {
                    case 0: M0 = value; break;
                    case 1: M1 = value; break;
                    case 2: M2 = value; break;
                    case 3: M3 = value; break;
                    case 4: M4 = value; break;
                    case 5: M5 = value; break;

                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        #endregion

        #region Constructors

        public Matrix(float m0, float m1, float m2, float m3, float m4, float m5)
        {
            M0 = m0;
            M1 = m1;
            M2 = m2;
            M3 = m3;
            M4 = m4;
            M5 = m5;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the inverse of this matrix.
        /// </summary>
        public Matrix Inverted => Inverse(in this);

        #endregion

        #region Inverse

        /// <summary>
        /// Computes the inverse of this matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix Inverse(in Matrix a)
        {
            var inv = default(Matrix);
            Inverse(in a, ref inv);
            return inv;
        }

        /// <summary>
        /// Computes the inverse of the matrix and stores the resulting matrix into <paramref name="dest"/>.
        /// </summary>
        public static void Inverse(in Matrix a, ref Matrix dest)
        {
            dest = Identity;

            var invdet = 1F / (a.M0 * a.M4 - a.M3 * a.M1);

            // Computes into temporary locals to prevent error when 
            // input and dest matrices are the same reference.

            var m0 = a.M4 * invdet;
            var m1 = -a.M1 * invdet;
            var m2 = (a.M1 * a.M5 - a.M2 * a.M4) * invdet;
            var m3 = -a.M3 * invdet;
            var m4 = a.M0 * invdet;
            var m5 = -(a.M0 * a.M5 - a.M2 * a.M3) * invdet;

            dest.M0 = m0;
            dest.M1 = m1;
            dest.M2 = m2;
            dest.M3 = m3;
            dest.M4 = m4;
            dest.M5 = m5;
        }

        #endregion

        #region Multiply (M * M)

        /// <summary>
        /// Multiply two matrices together and store the result in <paramref name="dest"/>.
        /// </summary>
        public static void Multiply(in Matrix a, in Matrix b, ref Matrix dest)
        {
            // Computes into temporary locals to prevent error when 
            // input and dest matrices are the same reference.

            // row 0
            var m0 = (a.M0 * b.M0) + (a.M1 * b.M3);
            var m1 = (a.M0 * b.M1) + (a.M1 * b.M4);
            var m2 = (a.M0 * b.M2) + (a.M1 * b.M5) + a.M2;

            // row 1
            var m3 = (a.M3 * b.M0) + (a.M4 * b.M3);
            var m4 = (a.M3 * b.M1) + (a.M4 * b.M4);
            var m5 = (a.M3 * b.M2) + (a.M4 * b.M5) + a.M5;

            dest.M0 = m0;
            dest.M1 = m1;
            dest.M2 = m2;
            dest.M3 = m3;
            dest.M4 = m4;
            dest.M5 = m5;
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix Multiply(in Matrix a, in Matrix b)
        {
            var c = default(Matrix);
            Multiply(in a, in b, ref c);
            return c;
        }

        #endregion

        #region Multiply (M * V)

        /// <summary>
        /// Multiplies a vector against this matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector Multiply(in Vector v)
        {
            var c = default(Vector);
            Multiply(in this, in v, ref c);
            return c;
        }

        /// <summary>
        /// Multiplies a vector against this matrix ignoring the translational components.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector MultiplyVector(in Vector v)
        {
            var c = default(Vector);
            MultiplyVector(in this, in v, ref c);
            return c;
        }

        /// <summary>
        /// Multiplies a vector and matrix together and stores the resulting vector into <paramref name="dest"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(in Matrix a, in Vector v, ref Vector dest)
        {
            // Computes into temporary locals to prevent error when 
            // input and dest vectors are the same reference.

            var x = (a.M0 * v.X) + (a.M1 * v.Y) + a.M2;
            var y = (a.M3 * v.X) + (a.M4 * v.Y) + a.M5;

            dest.X = x;
            dest.Y = y;
        }

        /// <summary>
        /// Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into <paramref name="dest"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MultiplyVector(in Matrix a, in Vector v, ref Vector r)
        {
            var x = (a.M0 * v.X) + (a.M1 * v.Y);
            var y = (a.M3 * v.X) + (a.M4 * v.Y);

            r.X = x;
            r.Y = y;
        }

        /// <summary>
        /// Multiplies a vector and matrix together.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Multiply(in Matrix a, in Vector v)
        {
            var c = default(Vector);
            Multiply(in a, in v, ref c);
            return c;
        }

        /// <summary>
        /// Multiplies a vector and matrix together ignoring the translational components.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector MultiplyVector(in Matrix a, in Vector v)
        {
            var c = default(Vector);
            MultiplyVector(in a, in v, ref c);
            return c;
        }

        #endregion

        #region Set Matrix

        /// <summary>
        /// Configures this matrix as a scaling matrix.
        /// </summary>
        public void SetScale(float sx, float sy)
        {
            M0 = sx;
            M1 = 0F;
            M2 = 0F;

            M3 = 0F;
            M4 = sy;
            M5 = 0F;
        }

        /// <summary>
        /// Configures this matrix as a shearing matrix.
        /// </summary>
        public void SetShear(float sx, float sy)
        {
            M0 = 1F;
            M1 = sx;
            M2 = 0F;

            M3 = sy;
            M4 = 1F;
            M5 = 0F;
        }

        /// <summary>
        /// Configures this matrix as a rotation matrix.
        /// </summary>
        public void SetRotation(float angle)
        {
            var c = Calc.Cos(angle);
            var s = Calc.Sin(angle);

            M0 = +c;
            M1 = -s;
            M2 = 0F;

            M3 = +s;
            M4 = +c;
            M5 = 0F;
        }

        /// <summary>
        /// Configures this matrix as a translation matrix.
        /// </summary>
        public void SetTranslation(float x, float y)
        {
            M0 = 1F;
            M1 = 0F;
            M2 = +x;

            M3 = 0F;
            M4 = 1F;
            M5 = +y;
        }

        #endregion

        #region Create Rotation

        /// <summary>
        /// Constructs a new rotation matrix.
        /// </summary>
        public static Matrix CreateRotation(float angle)
        {
            var m = Identity;
            m.SetRotation(angle);
            return m;
        }

        #endregion

        #region Create Scale

        /// <summary>
        /// Constructs a new scaling matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateScale(float sx, float sy)
        {
            var m = Identity;
            m.SetScale(sx, sy);
            return m;
        }

        /// <summary>
        /// Constructs a new scaling matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateScale(in Size scale)
        {
            return CreateScale(scale.Width, scale.Height);
        }

        /// <summary>
        /// Constructs a new scaling matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateScale(in Vector scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        /// <summary>
        /// Constructs a new uniform scaling matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateScale(float scale)
        {
            return CreateScale(scale, scale);
        }

        #endregion

        #region Create Shear

        /// <summary>
        /// Constructs a new shearing matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateShear(in Vector shear)
        {
            return CreateShear(shear.X, shear.Y);
        }

        /// <summary>
        /// Constructs a new shearing matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateShear(float sx, float sy)
        {
            var m = Identity;
            m.SetShear(sx, sy);
            return m;
        }

        #endregion

        #region Create Translation

        /// <summary>
        /// Constructs a new translation matrix.
        /// </summary>
        public static Matrix CreateTranslation(float x, float y)
        {
            var m = Identity;
            m.SetTranslation(x, y);
            return m;
        }

        /// <summary>
        /// Constructs a new translation matrix.
        /// </summary>
        public static Matrix CreateTranslation(in Vector vec)
        {
            return CreateTranslation(vec.X, vec.Y);
        }

        #endregion

        #region Create Transform (TRS)

        /// <summary>
        /// Creates a transform matrix with postion, rotation and scale.
        /// </summary>
        public static Matrix CreateTransform(in float tx, in float ty, in float angle, in float sx, in float sy)
        {
            var c = Calc.Cos(angle);
            var s = Calc.Sin(angle);

            var m = Identity;

            m.M0 = sx * c;
            m.M1 = sy * -s;
            m.M2 = tx;

            m.M3 = sx * s;
            m.M4 = sy * c;
            m.M5 = ty;

            return m;

            //return
            //    CreateTranslation(tx, ty) *
            //    CreateRotation(angle) *
            //    CreateScale(sx, sy);
        }

        /// <summary>
        /// Creates a transform matrix with postion, rotation and scale.
        /// </summary>
        public static Matrix CreateTransform(in Vector position, float angle, in Vector scale)
        {
            return CreateTransform(position.X, position.Y, angle, scale.X, scale.Y);
        }

        /// <summary>
        /// Creates a transform matrix with postion, rotation and scale.
        /// </summary>
        public static Matrix CreateTransform(in Vector position, float angle, in float scale)
        {
            return CreateTransform(position.X, position.Y, angle, scale, scale);
        }

        #endregion

        #region Create Rectangle Projection

        /// <summary>
        /// Constructs a matrix that transforms a rectangular region to normalized screen coordinates.
        /// </summary>
        public static Matrix RectangleProjection(Rectangle rectangle)
        {
            return RectangleProjection(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        /// <summary>
        /// Constructs a matrix that transforms a rectangular region to normalized screen coordinates.
        /// </summary>
        public static Matrix RectangleProjection(float left, float top, float right, float bottom)
        {
            var sx = 2F / (right - left);
            var sy = 2F / (top - bottom);
            var tx = -(right + left) / (right - left);
            var ty = -(top + bottom) / (top - bottom);

            return new Matrix(sx, 0F, tx, 0F, sy, ty);
        }

        #endregion

        #region Deconstruct / Extrat Affine Components

        public void Deconstruct(out Vector position, out float rotation, out Vector scale)
        // ref: https://stackoverflow.com/questions/4361242/extract-rotation-scale-values-from-2d-transformation-matrix
        {
            position = GetAffineTranslation();
            rotation = GetAffineRotation();
            scale = GetAffineScale();
        }

        /// <summary>
        /// Extracts affine scaling components from this matrix.
        /// </summary>
        public Vector GetAffineScale()
        {
            // todo: Get scale with negatives?
            //       This may not be possible given the nature of reflection and rotations

            // Extract scale
            var sx = Calc.Sqrt((M0 * M0) + (M3 * M3)); // sx = sqrt(a² + c²)
            var sy = Calc.Sqrt((M1 * M1) + (M4 * M4)); // sy = sqrt(b² + d²)

            return new Vector(sx, sy);
        }

        /// <summary>
        /// Extracts affine translational components from this matrix.
        /// </summary>
        public Vector GetAffineTranslation()
        {
            // Extract translation
            return new Vector(M2, M5);
        }

        /// <summary>
        /// Extracts affine rotational component (the angle) from this matrix.
        /// </summary>
        public float GetAffineRotation()
        {
            // Extract rotation
            var sin = M3; // _m3 / sx
            var cos = M4; // _m4 / sy

            return Calc.Atan(sin / cos);
        }

        #endregion

        #region Arithmetic Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Multiply(in a, in b, ref b);
            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator *(Matrix m, Vector v)
        {
            Multiply(in m, in v, ref v);
            return v;
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Matrix matrix && Equals(matrix);
        }

        public bool Equals(Matrix other)
        {
            return Calc.NearEquals(M0, other.M0) &&
                   Calc.NearEquals(M1, other.M1) &&
                   Calc.NearEquals(M2, other.M2) &&
                   Calc.NearEquals(M3, other.M3) &&
                   Calc.NearEquals(M4, other.M4) &&
                   Calc.NearEquals(M5, other.M5);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(M0, M1, M2, M3, M4, M5);
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }

        #endregion
    }
}
