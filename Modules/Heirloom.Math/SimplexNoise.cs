using System;

using static Heirloom.Math.Calc;

namespace Heirloom.Math
{
    /// <summary>
    /// Implements methods for sampling 2D and 3D simplex noise.
    /// </summary>
    public class SimplexNoise : INoise2D, INoise3D
    {
        private readonly float _h0 = 0.1030F;
        private readonly float _h1 = 0.1031F;
        private readonly float _h2 = 0.0973F;
        private readonly float _h3 = 19.191F;

        #region Constructors

        public SimplexNoise()
            : this(new Random())
        { }

        public SimplexNoise(int seed)
            : this(new Random(seed))
        { }

        public SimplexNoise(Random random)
        {
            // Seed hash function values
            _h0 = 0.1F + random.NextFloat(-0.05F, +0.05F);
            _h1 = 0.1F + random.NextFloat(-0.05F, +0.05F);
            _h2 = 0.1F + random.NextFloat(-0.05F, +0.05F);
            _h3 = 20F + random.NextFloat(-2F, +2F);
        }

        #endregion

        #region 2D Noise

        public float Sample(float x, float y)
        // ref: (2D) https://www.shadertoy.com/view/Msf3WH
        {
            const float K1 = 0.366025404F;  // (sqrt(3)-1)/2;

            const float K21 = 0.211324865F; // (3-sqrt(3))/6;
            const float K22 = K21 * 2.0F;

            var a = (x + y) * K1;

            var s_x = Floor(x + a);
            var s_y = Floor(y + a);

            var b = (s_x + s_y) * K21;

            var x0_x = x - s_x + b;
            var x0_y = y - s_y + b;

            var e_x = x0_x < x0_y ? 0F : 1F;
            var e_y = 1F - e_x;

            Random2(s_x, s_y, out var ra_x, out var ra_y);
            Random2(s_x + e_x, s_y + e_y, out var rb_x, out var rb_y);
            Random2(s_x + 1F, s_y + 1F, out var rc_x, out var rc_y);

            var x1_x = x0_x - e_x + K21;
            var x1_y = x0_y - e_y + K21;

            var x2_x = x0_x - 1.0F + K22;
            var x2_y = x0_y - 1.0F + K22;

            var w_x = Max(0.5F - ((x0_x * x0_x) + (x0_y * x0_y)), 0.0F);
            var w_y = Max(0.5F - ((x1_x * x1_x) + (x1_y * x1_y)), 0.0F);
            var w_z = Max(0.5F - ((x2_x * x2_x) + (x2_y * x2_y)), 0.0F);

            w_x = w_x * w_x * w_x * w_x;
            w_y = w_y * w_y * w_y * w_y;
            w_z = w_z * w_z * w_z * w_z;

            var d_x = w_x * ((x0_x * ra_x) + (x0_y * ra_y));
            var d_y = w_y * ((x1_x * rb_x) + (x1_y * rb_y));
            var d_z = w_z * ((x2_x * rc_x) + (x2_y * rc_y));

            return (d_x + d_y + d_z) * 66F;
        }

        // -1.0 to +1.0
        private void Random2(float x, float y, out float rx, out float ry)
        // ref: (hash22) https://www.shadertoy.com/view/4djSRW
        {
            var p_x = Fraction(x * _h0);
            var p_y = Fraction(y * _h1);
            var p_z = Fraction(x * _h2);

            var d = (p_x * (p_y + _h3)) + (p_y * (p_z + _h3)) + (p_z * (p_x + _h3));

            p_x += d;
            p_y += d;
            p_z += d;

            rx = (Fraction((p_x + p_y) * p_z) * 2.0F) - 1.0F;
            ry = (Fraction((p_x + p_z) * p_y) * 2.0F) - 1.0F;
        }

        #endregion

        #region 3D Noise

        public float Sample(float x, float y, float z)
        // ref: (3D) https://www.shadertoy.com/view/XsX3zB
        {
            const float F3 = 0.3333333F;  // 1/3

            const float G31 = 0.1666667F; // 1/6
            const float G32 = 2.0F * G31;
            const float G33 = 3.0F * G31;

            var a = (x + y + z) * F3;

            var s_x = Floor(x + a);
            var s_y = Floor(y + a);
            var s_z = Floor(z + a);

            var b = (s_x + s_y + s_z) * G31;

            var x0_x = x - s_x + b;
            var x0_y = y - s_y + b;
            var x0_z = z - s_z + b;

            var e_x = x0_x < x0_y ? 0F : 1F;
            var e_y = x0_y < x0_z ? 0F : 1F;
            var e_z = x0_z < x0_x ? 0F : 1F;

            var v_x = 1.0F - e_x;
            var v_y = 1.0F - e_y;
            var v_z = 1.0F - e_z;

            var i1_x = e_x * v_z;
            var i1_y = e_y * v_x;
            var i1_z = e_z * v_y;

            var i2_x = 1.0F - (e_z * v_x);
            var i2_y = 1.0F - (e_x * v_y);
            var i2_z = 1.0F - (e_y * v_z);

            Random3(s_x + 0.0F, s_y + 0.0F, s_z + 0.0F, out var r1_x, out var r1_y, out var r1_z);
            Random3(s_x + i1_x, s_y + i1_y, s_z + i1_z, out var r2_x, out var r2_y, out var r2_z);
            Random3(s_x + i2_x, s_y + i2_y, s_z + i2_z, out var r3_x, out var r3_y, out var r3_z);
            Random3(s_x + 1.0F, s_y + 1.0F, s_z + 1.0F, out var r4_x, out var r4_y, out var r4_z);

            var x1_x = x0_x - i1_x + G31;
            var x1_y = x0_y - i1_y + G31;
            var x1_z = x0_z - i1_z + G31;

            var x2_x = x0_x - i2_x + G32;
            var x2_y = x0_y - i2_y + G32;
            var x2_z = x0_z - i2_z + G32;

            var x3_x = x0_x - 1.0F + G33;
            var x3_y = x0_y - 1.0F + G33;
            var x3_z = x0_z - 1.0F + G33;

            var w_x = Max(0.5F - ((x0_x * x0_x) + (x0_y * x0_y) + (x0_z * x0_z)), 0.0F);
            var w_y = Max(0.5F - ((x1_x * x1_x) + (x1_y * x1_y) + (x1_z * x1_z)), 0.0F);
            var w_z = Max(0.5F - ((x2_x * x2_x) + (x2_y * x2_y) + (x2_z * x2_z)), 0.0F);
            var w_w = Max(0.5F - ((x3_x * x3_x) + (x3_y * x3_y) + (x3_z * x3_z)), 0.0F);

            w_x = w_x * w_x * w_x * w_x;
            w_y = w_y * w_y * w_y * w_y;
            w_z = w_z * w_z * w_z * w_z;
            w_w = w_w * w_w * w_w * w_w;

            var d_x = w_x * ((r1_x * x0_x) + (r1_y * x0_y) + (r1_z * x0_z));
            var d_y = w_y * ((r2_x * x1_x) + (r2_y * x1_y) + (r2_z * x1_z));
            var d_z = w_z * ((r3_x * x2_x) + (r3_y * x2_y) + (r3_z * x2_z));
            var d_w = w_w * ((r4_x * x3_x) + (r4_y * x3_y) + (r4_z * x3_z));

            return (d_x + d_y + d_z + d_w) * 66F;
        }

        // -1.0 to +1.0
        private void Random3(float x, float y, float z, out float rx, out float ry, out float rz)
        // ref: (hash33) https://www.shadertoy.com/view/4djSRW
        {
            var p_x = Fraction(x * _h0);
            var p_y = Fraction(y * _h1);
            var p_z = Fraction(z * _h2);

            var d = (p_x * (p_y + _h3)) + (p_y * (p_x + _h3)) + (p_z * (p_z + _h3));

            p_x += d;
            p_y += d;
            p_z += d;

            rx = (Fraction((p_x + p_y) * p_z) * 2.0F) - 1.0F;
            ry = (Fraction((p_x + p_x) * p_y) * 2.0F) - 1.0F;
            rz = (Fraction((p_y + p_x) * p_x) * 2.0F) - 1.0F;
        }

        #endregion
    }
}
