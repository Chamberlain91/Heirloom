namespace Heirloom.GLFW3
{
    public enum WindowHint
    {
        Visible = 0x00020004,
        Decorated = 0x00020005,
        Focused = 0x00020001,
        AutoIconify = 0x00020006,
        Floating = 0x00020007,
        Maximized = 0x00020008,
        CenterCursor = 0x00020009,
        TransparentFramebuffer = 0x0002000A,
        FocusOnShow = 0x0002000C,
        ScaleToMonitor = 0x0002200C,

        ClientApi = 0x00022001,
        ContextCreationApi = 0x0002200B,
        ContextVersionMajor = 0x00022002,
        ContextVersionMinor = 0x00022003,
        ContextVersionRevision = 0x00022004,
        OpenGLForwardCompatibility = 0x00022006,
        OpenGLDebugContext = 0x00022007,
        OpenGLProfile = 0x00022008,
        ContextRobustness = 0x00022005,

        RedBits = 0x00021007,
        GreenBits = 0x00021008,
        BlueBits = 0x00021009,
        AlphaBits = 0x0002100A,
        DepthBits = 0x00021005,
        StencilBits = 0x00021006,
        Stereo = 0x0002100C,
        DoubleBuffer = 0x00021010,
        Samples = 0x0002100D,
        SRGBCapable = 0x0002100E
    }
}
