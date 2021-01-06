namespace Meadows.Hardware
{
    /// <summary>
    /// Represents CPU vendors.
    /// </summary>
    public enum CpuVendor
    {
        /// <summary>
        /// The vendor was unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// The CPU is an Intel model.
        /// </summary>
        Intel,

        /// <summary>
        /// The CPU is an AMD model.
        /// </summary>
        AMD,

        // todo: ARM, Qualcomm, Exynosm, etc - common mobile processors
    }
}
