using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public abstract class Texture : GraphicsResource
    {
        /// <summary>
        /// A small (1x1) solid white image.
        /// </summary>
        public static readonly Texture Default = Image.CreateColor(1, 1, Color.White);

        internal Texture()
        {
            // Only visible to friends
        }

        /// <summary>
        /// The size of this image.
        /// </summary>
        public IntSize Size { get; protected set; }

        /// <summary>
        /// The width of the image (in pixels).
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// The height of the image (in pixels).
        /// </summary>
        public int Height => Size.Height;

        /// <summary>
        /// Interpolation Mode.
        /// </summary>
        public InterpolationMode Interpolation { get; set; } = InterpolationMode.Nearest;

        /// <summary>
        /// Repeat mode.
        /// </summary>
        public RepeatMode Repeat { get; set; } = RepeatMode.Blank;
    }
}
