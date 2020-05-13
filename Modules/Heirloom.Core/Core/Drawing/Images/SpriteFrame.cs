using System;

namespace Heirloom
{
    /// <summary>
    /// Represents a single frame of a <see cref="SpriteAnimation"/>.
    /// </summary>
    /// <category>Drawing</category>
    public sealed class SpriteFrame
    {
        internal SpriteFrame(Image image, float delay)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
            Delay = delay;
        }

        /// <summary>
        /// The image for this sprite frame.
        /// </summary>
        public Image Image { get; }

        /// <summary>
        /// The delay in seconds to be used when animating the sprite.
        /// </summary>
        public float Delay { get; }
    }
}
