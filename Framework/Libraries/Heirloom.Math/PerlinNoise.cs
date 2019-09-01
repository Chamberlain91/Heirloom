using System;
using System.Runtime.CompilerServices;

using static Heirloom.Math.Calc;

namespace Heirloom.Math
{
    public class PerlinNoise : INoise1D, INoise2D, INoise3D
    {
        private readonly int[] _p;

        const int SIZE = byte.MaxValue + 1; // must be power 2
        const int MASK = SIZE - 1;

        #region Constructors

        public PerlinNoise()
            : this(new Random())
        { }

        public PerlinNoise(int seed)
            : this(new Random(seed))
        { }

        public PerlinNoise(Random random)
        {
            _p = CreateArray(random);
        }

        #endregion

        #region Sample

        /// <inheritdoc/>
        public float Sample(float x)
        {
            x += 0.5F;

            var x_f = x - (int) x;
            var x_i = (int) x & MASK;

            var u = Fade(x_f);

            var a = _p[x_i];
            var b = _p[x_i + 1];

            var g1 = Grad(a, x_f);
            var g2 = Grad(b, x_f - 1);

            return Lerp(g1, g2, u);
        }

        /// <inheritdoc/>
        public float Sample(float x, float y)
        {
            x += 0.5F;
            y += 0.5F;

            var x_f = x - (int) x;
            var y_f = y - (int) y;

            var x_i = (int) x & MASK;
            var y_i = (int) y & MASK;

            var u = Fade(x_f);
            var v = Fade(y_f);

            var aa = _p[_p[x_i + 0] + y_i + 0];
            var ab = _p[_p[x_i + 0] + y_i + 1];
            var ba = _p[_p[x_i + 1] + y_i + 0];
            var bb = _p[_p[x_i + 1] + y_i + 1];

            var x1 = Lerp(Grad(aa, x_f + 0, y_f + 0), Grad(ba, x_f - 1, y_f + 0), u);
            var x2 = Lerp(Grad(ab, x_f + 0, y_f - 1), Grad(bb, x_f - 1, y_f - 1), u);

            return Lerp(x1, x2, v);
        }

        /// <inheritdoc/>
        public float Sample(float x, float y, float z)
        {
            x += 0.5F;
            y += 0.5F;
            z += 0.5F;

            var x_f = x - (int) x;
            var y_f = y - (int) y;
            var z_f = z - (int) z;

            var x_i = (int) x & MASK;
            var y_i = (int) y & MASK;
            var z_i = (int) z & MASK;

            var u = Fade(x_f);
            var v = Fade(y_f);
            var w = Fade(z_f);

            var aaa = _p[_p[_p[x_i + 0] + y_i + 0] + z_i + 0];
            var aba = _p[_p[_p[x_i + 0] + y_i + 1] + z_i + 0];
            var baa = _p[_p[_p[x_i + 1] + y_i + 0] + z_i + 0];
            var bba = _p[_p[_p[x_i + 1] + y_i + 1] + z_i + 0];

            var aab = _p[_p[_p[x_i + 0] + y_i + 0] + z_i + 1];
            var abb = _p[_p[_p[x_i + 0] + y_i + 1] + z_i + 1];
            var bab = _p[_p[_p[x_i + 1] + y_i + 0] + z_i + 1];
            var bbb = _p[_p[_p[x_i + 1] + y_i + 1] + z_i + 1];

            float x1, x2;
            x1 = Lerp(Grad(aaa, x_f + 0, y_f + 0, z_f + 0), Grad(baa, x_f - 1, y_f + 0, z_f + 0), u);
            x2 = Lerp(Grad(aba, x_f + 0, y_f - 1, z_f + 0), Grad(bba, x_f - 1, y_f - 1, z_f + 0), u);
            var y1 = Lerp(x1, x2, v);

            x1 = Lerp(Grad(aab, x_f + 0, y_f + 0, z_f - 1), Grad(bab, x_f - 1, y_f + 0, z_f - 1), u);
            x2 = Lerp(Grad(abb, x_f + 0, y_f - 1, z_f - 1), Grad(bbb, x_f - 1, y_f - 1, z_f - 1), u);
            var y2 = Lerp(x1, x2, v);

            return Lerp(y1, y2, w);
        }

        #endregion

        #region Gradient Functions

        // Source: http://riven8192.blogspot.com/2010/08/calculate-perlinnoise-twice-as-fast.html
        private static float Grad(int hash, float x, float y, float z)
        {
            switch (hash & 0xF)
            {
                case 0x0: return x + y;
                case 0x1: return -x + y;
                case 0x2: return x - y;
                case 0x3: return -x - y;
                case 0x4: return x + z;
                case 0x5: return -x + z;
                case 0x6: return x - z;
                case 0x7: return -x - z;
                case 0x8: return y + z;
                case 0x9: return -y + z;
                case 0xA: return y - z;
                case 0xB: return -y - z;

                case 0xC: return y + x;
                case 0xD: return -y + z;
                case 0xE: return y - x;
                case 0xF: return -y - z;

                default: return 0; // never happens
            }
        }

        private static float Grad(int hash, float x, float y)
        {
            switch (hash & 3)
            {
                case 0x0: return x + y;
                case 0x1: return -x + y;
                case 0x2: return x - y;
                case 0x3: return -x - y;

                default: return 0; // never happens
            }
        }

        private static float Grad(int hash, float x)
        {
            switch (hash & 1)
            {
                case 0x0: return x;
                case 0x1: return -x;

                default: return 0; // never happens
            }
        }

        #endregion

        // 6t^5 - 15t^4 + 10t^3
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Fade(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static int[] CreateArray(Random random)
        {
            var data = new int[SIZE * 2];

            // Populate
            for (var i = 0; i < SIZE; i++)
            {
                data[i] = i;
            }

            // Randomize array
            for (var i = 0; i < SIZE; i++)
            {
                var x = random.Next(0, SIZE);
                Swap(ref data[i], ref data[x]);
            }

            // Copy to second half
            for (var i = 0; i < SIZE; i++)
            {
                data[i + SIZE] = data[i];
            }

            return data;
        }
    }
}
