namespace Heirloom.Desktop.Hardware
{
    /// <summary>
    /// Represents GPU vendors.
    /// </summary>
    public enum GpuVendor
    {
        /// <summary>
        /// The vendor was unknown.
        /// </summary>
        Unknown,
        // -- Desktop
        /// <summary>
        /// The GPU was produced by Intel.
        /// </summary>
        Intel,
        /// <summary>
        /// The GPU was produced by Nvidia.
        /// </summary>
        Nvidia,
        /// <summary>
        /// The GPU was produced by AMD/ATI.
        /// </summary>
        AMD,
        // -- Mobile
        // Mali,
        // PowerVR,
        // Adreno
    }
}
