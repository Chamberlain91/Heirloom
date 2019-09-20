using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class ImageSource
    {
        internal ImageSource()
        {
            // Only visible in Heirloom.Drawing
        }

        /// <summary>
        /// The version number of the image.
        /// Modifications to the image increment this number.
        /// </summary>
        public uint Version { get; private set; } = 0;

        /// <summary>
        /// The size of this image.
        /// </summary>
        public abstract IntSize Size { get; protected set; }

        /// <summary>
        /// Increments the version number.
        /// </summary>
        internal void UpdateVersionNumber()
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
