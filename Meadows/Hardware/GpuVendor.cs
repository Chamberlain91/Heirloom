namespace Meadows.Hardware
{
    /// <summary>
    /// Represents GPU vendors.
    /// </summary>
    public enum GpuVendor
    {
        /// <summary>
        /// The GPU vendor was unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// The GPU is an Intel model.
        /// </summary>
        Intel,

        /// <summary>
        /// The GPU is a NVIDA model.
        /// </summary>
        Nvidia,

        /// <summary>
        /// The GPU is an AMD model.
        /// </summary>
        AMD,

        // todo: Mali, PowerVR, Adreno, etc - common mobile graphics
    }
}
