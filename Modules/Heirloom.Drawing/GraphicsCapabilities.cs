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

        public readonly string AdapterVendor;

        public readonly string AdapterName;

        public GraphicsCapabilities(string adapterName, string adapterVendor, bool isMobilePlatform, int maxSupportedFragmentImages, int maxSupportedVertexImages)
        {
            AdapterName = adapterName;
            AdapterVendor = adapterVendor;

            IsMobilePlatform = isMobilePlatform;

            MaxSupportedFragmentImages = maxSupportedFragmentImages;
            MaxSupportedVertexImages = maxSupportedVertexImages;
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
                   AdapterVendor == other.AdapterVendor &&
                   AdapterName == other.AdapterName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsMobilePlatform, MaxSupportedFragmentImages, MaxSupportedVertexImages, AdapterName, AdapterVendor);
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
