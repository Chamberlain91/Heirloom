using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Meadows.Hardware
{
    /// <summary>
    /// Contains information related to the CPU.
    /// </summary>
    public readonly struct CpuInfo : IEquatable<CpuInfo>
    {
        /// <summary>
        /// Gets the CPU Vendor.
        /// </summary>
        public readonly CpuVendor Vendor { get; }

        /// <summary>
        /// Gets the name of the CPU.
        /// </summary>
        public readonly string Name { get; }

        /// <summary>
        /// Gets how many threads (logical cores) this CPU supports.
        /// </summary>
        public readonly int ThreadCount { get; }

        /// <summary>
        /// Gets the clockspeed of this CPU in MHz.
        /// </summary>
        public readonly int ClockSpeed { get; }

        #region Constructors

        internal CpuInfo(string name, int clockSpeed, int threadCount)
            : this(GetCpuVendor(name), name, clockSpeed, threadCount)
        { }

        internal CpuInfo(CpuVendor vendor, string name, int clockSpeed, int threadCount)
        {
            Vendor = vendor;
            Name = GetCleanCpuName(vendor, name);

            ThreadCount = threadCount;
            ClockSpeed = clockSpeed;
        }

        #endregion

        /// <summary>
        /// Gets default information when properties of CPU are unknown.
        /// </summary>
        internal static CpuInfo Unknown { get; } = new CpuInfo(CpuVendor.Unknown, "Unknown", 0, 0);

        #region Equality

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is CpuInfo info
                && Equals(info);
        }

        /// <summary>
        /// Compares the <see cref="CpuInfo"/> against each other for equality.
        /// </summary>
        public bool Equals([AllowNull] CpuInfo other)
        {
            return Vendor == other.Vendor &&
                   Name == other.Name &&
                   ClockSpeed == other.ClockSpeed &&
                   ThreadCount == other.ThreadCount;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Vendor, Name, ClockSpeed, ThreadCount);
        }

        /// <summary>
        /// Compares two <see cref="CpuInfo"/> for equality.
        /// </summary>
        public static bool operator ==(CpuInfo left, CpuInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="CpuInfo"/> for inequality.
        /// </summary>
        public static bool operator !=(CpuInfo left, CpuInfo right)
        {
            return !(left == right);
        }

        #endregion

        /// <summary>
        /// Returns a string representation of the <see cref="CpuInfo"/>.
        /// </summary>
        public override string ToString()
        {
            return $"{Vendor} - {Name} ({ThreadCount} threads at {ClockSpeed / 1000F:0.#}GHz)";
        }

        #region Clean Name and Detect Vendor

        // Some regex to match some CPU names
        private static readonly Regex _cpuRyzenRegex = new Regex(@"AMD Ryzen (\d \d+\w?) ?.*", RegexOptions.Compiled);
        private static readonly Regex _cpuIntelRegex = new Regex(@"Intel.+Core.+ (i\d-\d+\w?) ?.*", RegexOptions.Compiled);

        internal static string GetCleanCpuName(CpuVendor vendor, string name)
        {
            switch (vendor)
            {
                case CpuVendor.Intel:
                {
                    // Intel(R) Core(TM) i7-8750H CPU @ 2.20GHz
                    var matches = _cpuIntelRegex.Match(name);
                    if (matches.Success) { return $"Core {matches.Groups[1].Value}"; }
                }
                break;

                case CpuVendor.AMD:
                {
                    // AMD Ryzen 7 2700X Eight-Core Processor -> Ryzen 7 2700X
                    var matches = _cpuRyzenRegex.Match(name);
                    if (matches.Success) { return $"Ryzen {matches.Groups[1].Value}"; }
                }
                break;
            }

            // Eh, couldn't improve it
            return name;
        }

        internal static CpuVendor GetCpuVendor(string name)
        {
            var vendorHints = new Dictionary<CpuVendor, string[]>
            {
                [CpuVendor.Intel] = new[] {
                    "INTEL", "I7", "I5", "I3"
                },
                [CpuVendor.AMD] = new[] {
                    "AMD", "RYZEN"
                }
            };

            // Determine CPU Vendor!
            return VendorUtility.DetectVendor(null, vendorHints, name);
        }

        #endregion
    }
}
