using System;
using System.Diagnostics.CodeAnalysis;

namespace Heirloom.Desktop.Hardware
{
    /// <summary>
    /// Represents information about the GPU on some device.
    /// </summary>
    public readonly struct GpuInfo : IEquatable<GpuInfo>
    {
        /// <summary>
        /// Gets the vendor of the GPU.
        /// </summary>
        public readonly GpuVendor Vendor { get; }

        /// <summary>
        /// Gets the name of the GPU.
        /// </summary>
        public readonly string Name { get; }

        #region Constructors

        internal GpuInfo(string vendor, string name)
            : this(HardwareDetector.GetGpuVendor(vendor), name)
        { }

        internal GpuInfo(GpuVendor vendor, string name)
        {
            Vendor = vendor;
            Name = HardwareDetector.GetCleanGpuName(vendor, name).Trim();
        }

        #endregion

        /// <summary>
        /// Gets default information when properties of GPU are unknown.
        /// </summary>
        internal static GpuInfo Unknown { get; } = new GpuInfo(GpuVendor.Unknown, "Unknown");

        #region Equality

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is CpuInfo info
                && Equals(info);
        }

        /// <summary>
        /// Compares the <see cref="GpuInfo"/> against each other for equality.
        /// </summary>
        public bool Equals([AllowNull] GpuInfo other)
        {
            return Vendor == other.Vendor &&
                   Name == other.Name;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Vendor, Name);
        }

        /// <summary>
        /// Compares two <see cref="GpuInfo"/> for equality.
        /// </summary>
        public static bool operator ==(GpuInfo left, GpuInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="GpuInfo"/> for inequality.
        /// </summary>
        public static bool operator !=(GpuInfo left, GpuInfo right)
        {
            return !(left == right);
        }

        #endregion

        /// <summary>
        /// Returns a string representation of the <see cref="GpuInfo"/>.
        /// </summary>
        public override string ToString()
        {
            return $"{Vendor} - {Name}";
        }
    }
}
