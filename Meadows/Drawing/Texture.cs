using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public abstract class Texture : NativeResource
    {
        internal Texture()
        {
            // Only visible to friends
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
        /// Repeat mode.
        /// </summary>
        public RepeatMode Repeat { get; set; } = RepeatMode.Blank;
    }
}
