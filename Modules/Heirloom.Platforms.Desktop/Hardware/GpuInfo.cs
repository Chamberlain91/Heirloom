using System;
using System.Diagnostics.CodeAnalysis;

namespace Heirloom.Desktop.Hardware
{
    public readonly struct GpuInfo : IEquatable<GpuInfo>
    {
        public readonly GpuVendor Vendor { get; }

        public readonly string Name { get; }

        internal GpuInfo(string vendor, string name)
            : this(HardwareDetector.GetGpuVendor(vendor), name)
        { }

        internal GpuInfo(GpuVendor vendor, string name)
        {
            Vendor = vendor;
            Name = HardwareDetector.GetCleanGpuName(vendor, name).Trim();
        }

        /// <summary>
        /// Gets default information when properties of GPU are unknown.
        /// </summary>
        internal static GpuInfo Unknown { get; } = new GpuInfo(GpuVendor.Unknown, "Unknown");

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is CpuInfo info && Equals(info);
        }

        public bool Equals([AllowNull] GpuInfo other)
        {
            return Vendor == other.Vendor &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vendor, Name);
        }

        public static bool operator ==(GpuInfo left, GpuInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GpuInfo left, GpuInfo right)
        {
            return !(left == right);
        }

        #endregion

        public override string ToString()
        {
            return $"{Vendor} - {Name}";
        }
    }
}
