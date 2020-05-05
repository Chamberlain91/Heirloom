using System;

namespace Heirloom
{
    /// <summary>
    /// A special stretchable rectangle of an image.
    /// </summary>
    public class NineSlice
    {
        /// <summary>
        /// The image used by this nine slice.
        /// </summary>
        public Image Image;

        /// <summary>
        /// The center rectangle of the nine slice.
        /// This implicitly defines all other slice bounds.
        /// </summary>
        public IntRectangle Center;

        /// <summary>
        /// Constructs a new nine slice.
        /// </summary>
        public NineSlice(Image frame, IntRectangle center)
        {
            Image = frame ?? throw new ArgumentNullException(nameof(frame));
            Center = center;
        }
    }
}
