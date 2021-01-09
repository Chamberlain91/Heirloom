namespace Meadows.Desktop.GLFW
{
    internal enum WindowCreationHint
    {
        /// <summary>
        /// Should the cursor be automatically centered on newly created fullscreen windows? (default: true)
        /// </summary>
        CenterCursor = 0x00020009,

        /// <summary>
        /// Should the window content scale automatically adjust for the monitor it is on? (default: false)
        /// </summary>
        ScaleToMonitor = 0x0002200C,

        /// <summary>
        /// The number of red bits in the window framebuffer format. (default: 8)
        /// </summary>
        RedBits = 0x00021007,

        /// <summary>
        /// The number of green bits in the window framebuffer format. (default: 8)
        /// </summary>
        GreenBits = 0x00021008,

        /// <summary>
        /// The number of blue bits in the window framebuffer format. (default: 8)
        /// </summary>
        BlueBits = 0x00021009,

        /// <summary>
        /// The number of alpha bits in the window framebuffer format. (default: 8)
        /// </summary>
        AlphaBits = 0x0002100A,

        /// <summary>
        /// The number of depth bits in the window framebuffer format. (default: 24)
        /// </summary>
        DepthBits = 0x00021005,

        /// <summary>
        /// The number of stencil bits in the window framebuffer format. (default: 8)
        /// </summary>
        StencilBits = 0x00021006,

        /// <summary>
        /// The number of samples (msaa) in the window framebuffer format. (default: 0)
        /// </summary>
        Samples = 0x0002100D,

        /// <summary>
        /// Should the window be configured for stereoscopic rendering? (default: false)
        /// </summary>
        Stereo = 0x0002100C,

        /// <summary>
        /// Should the window support a double buffered framebuffer? (default: true)
        /// </summary>
        DoubleBuffer = 0x00021010,

        /// <summary>
        /// Should the window framebuffer support SRGB colorspace. (default: false)
        /// </summary>
        SRGBCapable = 0x0002100E
    }
}
