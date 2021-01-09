using System;
using System.Linq;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents a (square) dither pattern.
    /// </summary>
    public sealed class DitherPattern
    {
        private readonly float[,] _values;

        /// <summary>
        /// A standard 64x64 ordered bayer dither.
        /// </summary>
        public static readonly DitherPattern Standard = CreateBayerDither(64);

        /// <summary>
        /// A 64x64 blue noise dither.
        /// </summary>
        public static readonly DitherPattern BlueNoise = CreateBlueNoise();

        /// <summary>
        /// Constructs a new dither pattern with the specified size and value callback.
        /// </summary>
        /// <param name="size">The side length of pattern square.</param>
        /// <param name="create">The callback for populating values of the pattern.</param>
        public DitherPattern(int size, Func<IntVector, float> create)
        {
            if (create is null) { throw new ArgumentNullException(nameof(create)); }
            if (size <= 1) { throw new ArgumentException($"Parameter '{nameof(size)}' must be greater than one.", nameof(size)); }

            Size = size;
            _values = new float[Size, Size];

            // Compute dither matrix
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    _values[y, x] = create((x, y));
                }
            }
        }

        /// <summary>
        /// The size of the pattern.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the value at the specified coordinate. These coordinates will wrap.
        /// </summary>
        /// <param name="x">Some x-coordinate.</param>
        /// <param name="y">Some y-coordinate.</param>
        /// <returns>The value of the dither pattern.</returns>
        public float GetValue(int x, int y)
        {
            return _values[Calc.Wrap(y, Size), Calc.Wrap(x, Size)];
        }

        /// <summary>
        /// Gets the value at the specified coordinate. These coordinates will wrap.
        /// </summary>
        /// <param name="co">Some coordinate.</param>
        /// <returns>The value of the dither pattern.</returns>
        public float GetValue(IntVector co)
        {
            return GetValue(co.X, co.Y);
        }

        /// <summary>
        /// Constructs an ordered Bayer dither pattern.
        /// </summary>
        /// <param name="size">The size of the pattern (must be a power of two).</param>
        /// <returns>A ordered bayer dither pattern.</returns>
        public static DitherPattern CreateBayerDither(int size)
        {
            if (!Calc.IsPowerOfTwo(size)) { throw new ArgumentException("Bayer dither size must be a power of two."); }
            var n = (int) Calc.Log(size, 2);

            return new DitherPattern(size, co =>
            {
                var value = BitReverse(BitInterleave(co.X ^ co.Y, co.X, n), n * 2);
                return value / (float) (size * size);
            });

            static long BitReverse(long a, int n)
            {
                var binary = Convert.ToString(a, 2).PadLeft(n, '0');
                binary = new string(binary.Reverse().ToArray()); // Reverse string
                return Convert.ToInt64(binary, 2);
            }

            static long BitInterleave(int a, int b, int n)
            {
                var s1 = Convert.ToString(a, 2).PadLeft(n, '0');
                var s2 = Convert.ToString(b, 2).PadLeft(n, '0');
                var binary = string.Join("", s1.Zip(s2, (x, y) => $"{x}{y}"));
                return Convert.ToInt64(binary, 2);
            }
        }

        private static DitherPattern CreateBlueNoise()
        {
            var image = new Image("Heirloom/Embedded/LDR_RGB1_0.png");
            return new DitherPattern(image.Width, co =>
            {
                return image.GetPixel(co).R / 256F;
            });
        }
    }
}
