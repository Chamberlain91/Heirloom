using System;
using System.Diagnostics.CodeAnalysis;

namespace Heirloom.Benchmark
{
    public readonly struct ProcessorInfo : IEquatable<ProcessorInfo>
    {
        /// <summary>
        /// Gets the name of the processor on this system.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Gets the clock speed (in megahertz) on this system.
        /// </summary>
        public readonly int ClockSpeed;

        /// <summary>
        /// The number of logical processors on this system.
        /// </summary>
        public readonly int ProcessorCount;

        /// <summary>
        /// Gets the default information when properties of CPU are unknown.
        /// </summary>
        public static ProcessorInfo Unknown { get; } = new ProcessorInfo("Unknown", 0, 0);

        #region Constructors

        public ProcessorInfo(string name, int clockSpeed, int processorCount)
        {
            Name = name;
            ClockSpeed = clockSpeed;
            ProcessorCount = processorCount;
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is ProcessorInfo info && Equals(info);
        }

        public bool Equals([AllowNull] ProcessorInfo other)
        {
            return Name == other.Name &&
                   ClockSpeed == other.ClockSpeed &&
                   ProcessorCount == other.ProcessorCount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ClockSpeed, ProcessorCount);
        }

        public static bool operator ==(ProcessorInfo left, ProcessorInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ProcessorInfo left, ProcessorInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
