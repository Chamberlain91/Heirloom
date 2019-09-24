using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class ImageSource : IDrawingResource
    {
        private object _native;

        internal ImageSource()
        {
            // Only visible in Heirloom.Drawing
        }

        /// <summary>
        /// The version number of the image.
        /// Modifications to the image increment this number.
        /// </summary>
        public uint Version { get; private set; }

        /// <summary>
        /// The size of this image.
        /// </summary>
        public abstract IntSize Size { get; protected set; }

        internal void UpdateVersionNumber()
        {
            Version++;

            // If hit the maximum version number, wrap around.
            if (Version == uint.MaxValue)
            {
                Version = 0;
            }
        }

        object IDrawingResource.NativeObject
        {
            get => _native;
            set => _native = value;
        }

        void IDrawingResource.UpdateVersionNumber()
        {
            UpdateVersionNumber();
        }
    }
}
