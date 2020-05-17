namespace Heirloom
{
    public static partial class Extensions
    {
        /// <summary>
        /// Provides extension methods various types within Heirloom.
        /// </summary>
        /// <returns>A noise value from -1.0 to +1.0.</returns>
        public static float Sample(this INoise1D noise, float x, int octaves, float persistence = 0.5F)
        {
            var total = 0F;
            var frequency = 1F;
            var amplitude = 1F;
            var maxValue = 0F;

            for (var i = 0; i < octaves; i++)
            {
                total += noise.Sample(x * frequency) * amplitude;

                maxValue += amplitude;

                amplitude *= persistence;
                frequency *= 2;
            }

            return total / maxValue;
        }

        /// <summary>
        /// Sample two-dimensional noise.
        /// </summary>
        /// <returns>A noise value from -1.0 to +1.0.</returns>
        public static float Sample(this INoise2D noise, in Vector position)
        {
            return noise.Sample(position.X, position.Y);
        }

        /// <summary>
        /// Sample two-dimensional octave noise.
        /// </summary>
        /// <returns>A noise value from -1.0 to +1.0.</returns>
        public static float Sample(this INoise2D noise, in Vector position, int octaves, float persistence = 0.5F)
        {
            return Sample(noise, position.X, position.Y, octaves, persistence);
        }

        /// <summary>
        /// Sample two-dimensional octave noise.
        /// </summary>
        /// <returns>A noise value from -1.0 to +1.0.</returns>
        public static float Sample(this INoise2D noise, float x, float y, int octaves, float persistence = 0.5F)
        {
            var total = 0F;
            var frequency = 1F;
            var amplitude = 1F;
            var maxValue = 0F;

            for (var i = 0; i < octaves; i++)
            {
                total += noise.Sample(x * frequency, y * frequency) * amplitude;

                maxValue += amplitude;

                amplitude *= persistence;
                frequency *= 2;
            }

            return total / maxValue;
        }

        /// <summary>
        /// Sample three-dimensional octave noise.
        /// </summary>
        /// <returns>A noise value from -1.0 to +1.0.</returns>
        public static float Sample(this INoise3D noise, float x, float y, float z, int octaves, float persistence = 0.5F)
        {
            var total = 0F;
            var frequency = 1F;
            var amplitude = 1F;
            var maxValue = 0F;

            for (var i = 0; i < octaves; i++)
            {
                total += noise.Sample(x * frequency, y * frequency, z * frequency) * amplitude;

                maxValue += amplitude;

                amplitude *= persistence;
                frequency *= 2;
            }

            return total / maxValue;
        }
    }
}
