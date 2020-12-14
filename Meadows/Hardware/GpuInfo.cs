using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Meadows.Hardware
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
            : this(GetGpuVendor(vendor), name)
        { }

        internal GpuInfo(GpuVendor vendor, string name)
        {
            Vendor = vendor;
            Name = GetCleanGpuName(vendor, name).Trim();
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

        #region Clean Name and Detect Vendor

        // Some regex to match some GPU names
        private static readonly Regex _gpuGeforceRegex = new Regex(@"(GeForce [GR]TX[^/]*)/.*", RegexOptions.Compiled);
        private static readonly Regex _gpuRadeonRegex = new Regex(@"AMD Radeon ?\(TM\) (.+) (Series|Graphics)", RegexOptions.Compiled);
        private static readonly Regex _gpuIntelRegex = new Regex(@".*Intel.* (.+ .+ \d+)", RegexOptions.Compiled);

        internal static string GetCleanGpuName(GpuVendor vendor, string name)
        {
            switch (vendor)
            {
                case GpuVendor.Nvidia:
                {
                    // GeForce RTX 2080/pcie/SSE2 -> GeForce RTX 2080
                    var matches = _gpuGeforceRegex.Match(name);
                    if (matches.Success) { return matches.Groups[1].Value; }
                }
                break;

                case GpuVendor.AMD:
                {
                    // AMD Radeon(TM) Vega 8 Graphics -> Radeon Vega 8
                    var matches = _gpuRadeonRegex.Match(name);
                    if (matches.Success) { return $"Radeon {matches.Groups[1].Value}"; }
                }
                break;

                case GpuVendor.Intel:
                {
                    // Intel(R) UHD Graphics 630 -> UHD Graphics 630
                    var matches = _gpuIntelRegex.Match(name);
                    if (matches.Success) { return matches.Groups[1].Value; }
                }
                break;
            }

            // Eh, couldn't improve it
            return name;
        }

        internal static GpuVendor GetGpuVendor(string name)
        {
            var vendors = new Dictionary<GpuVendor, string[]>
            {
                [GpuVendor.AMD] = new string[] {
                    "ATI TECHNOLOGIES INC"
                },
                [GpuVendor.Nvidia] = new string[] {
                    "NVIDIA CORPORATION"
                },
                [GpuVendor.Intel] = new string[] {
                    "INTEL OPEN SOURCE TECHNOLOGY CENTER",
                    "INTEL"
                }
            };

            var vendorHints = new Dictionary<GpuVendor, string[]>
            {
                [GpuVendor.AMD] = new string[] { "AMD", "ATI", "RADEON", "VEGA" },
                [GpuVendor.Nvidia] = new string[] { "NVIDIA", "GEFORCE", "GTX", "RTX" },
                [GpuVendor.Intel] = new string[] { "INTEL" }
            };

            // Determine GPU Vendor!
            return VendorUtility.DetectVendor(vendors, vendorHints, name);
        }

        #endregion
    }
}
