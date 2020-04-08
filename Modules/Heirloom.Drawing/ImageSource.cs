using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class ImageSource
    {
        internal ImageSource()
        {
            // Only visible in Heirloom.Drawing and friends
        }

        /// <summary>
        /// The size of this image.
        /// </summary>
        public abstract IntSize Size { get; protected set; }

        /// <summary>
        /// The width of the image (in pixels).
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// The height of the image (in pixels).
        /// </summary>
        public int Height => Size.Height;

        /// <summary>
        /// The offset used to 'center' the image around a non-zero origin.
        /// </summary>
        public Vector Origin { get; set; }

        /// <summary>
        /// Version number to track changes against the image data.
        /// </summary>
        internal uint Version { get; private set; }

        /// <summary>
        /// Interpolation mode.
        /// </summary>
        public InterpolationMode Interpolation { get; set; } = InterpolationMode.Nearest;

        /// <summary>
        /// Repeat mode.
        /// </summary>
        public RepeatMode Repeat { get; set; } = RepeatMode.Blank;

        internal object Native;

        internal void IncrementVersion()
        {
            Version++;

            // If hit the maximum version number, wrap around.
            if (Version == uint.MaxValue)
            {
                Version = 0;
            }
        }
    }
}
