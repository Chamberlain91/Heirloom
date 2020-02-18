using System;

namespace Heirloom.Drawing
{
    public readonly struct GraphicsCapabilities : IEquatable<GraphicsCapabilities>
    {
        /// <summary>
        /// Gets a value that determines if this application has been detected to be running on a mobile platform.
        /// </summary>
        public readonly bool IsMobilePlatform;

        public readonly int MaxSupportedFragmentImages;

        public readonly int MaxSupportedVertexImages;

        public readonly int MaxImageSize;

        public readonly string AdapterVendor;

        public readonly string AdapterName;

        public GraphicsCapabilities(string adapterName, string adapterVendor, bool isMobilePlatform,
                                    int maxSupportedFragmentImages, int maxSupportedVertexImages,
                                    int maxImageSize)
        {
            AdapterName = adapterName;
            AdapterVendor = adapterVendor;

            IsMobilePlatform = isMobilePlatform;

            MaxSupportedFragmentImages = maxSupportedFragmentImages;
            MaxSupportedVertexImages = maxSupportedVertexImages;
            MaxImageSize = maxImageSize;
        }

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is GraphicsCapabilities capabilities && Equals(capabilities);
        }

        public bool Equals(GraphicsCapabilities other)
        {
            return IsMobilePlatform == other.IsMobilePlatform &&
                   MaxSupportedFragmentImages == other.MaxSupportedFragmentImages &&
                   MaxSupportedVertexImages == other.MaxSupportedVertexImages &&
                   MaxImageSize == other.MaxImageSize &&
                   AdapterVendor == other.AdapterVendor &&
                   AdapterName == other.AdapterName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsMobilePlatform, MaxSupportedFragmentImages, MaxSupportedVertexImages,
                                    MaxImageSize, AdapterName, AdapterVendor);
        }

        public static bool operator ==(GraphicsCapabilities left, GraphicsCapabilities right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GraphicsCapabilities left, GraphicsCapabilities right)
        {
            return !(left == right);
        }

        #endregion
    }
}
