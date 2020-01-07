using System;

namespace Heirloom.Drawing
{
    public readonly struct GraphicsCapabilities : IEquatable<GraphicsCapabilities>
    {
        public readonly bool IsMobilePlatform;

        public readonly int MaxSupportedShaderImages;

        public GraphicsCapabilities(bool isMobilePlatform, int maxSupportedTextures)
        {
            IsMobilePlatform = isMobilePlatform;

            MaxSupportedShaderImages = maxSupportedTextures;
        }

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is GraphicsCapabilities capabilities && Equals(capabilities);
        }

        public bool Equals(GraphicsCapabilities other)
        {
            return IsMobilePlatform == other.IsMobilePlatform &&
                   MaxSupportedShaderImages == other.MaxSupportedShaderImages;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsMobilePlatform, MaxSupportedShaderImages);
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
