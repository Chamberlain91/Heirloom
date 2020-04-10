namespace Heirloom.Desktop
{
    internal enum WindowAttribute
    {
        /// <summary>
        /// Is the window decorated? Can be set.
        /// </summary>
        Decorated = 0x00020005,

        /// <summary>
        /// Is the window resizable? Can be set.
        /// </summary>
        Resizable = 0x00020003,

        /// <summary>
        /// Is the window floating? Can be set.
        /// </summary>
        Floating = 0x00020007,

        /// <summary>
        /// Will the window automatically iconify when lost focus in fullscreen? Can be set.
        /// </summary>
        AutoIconify = 0x00020006,

        /// <summary>
        /// Will the window automatically focus when shown? Can be set.
        /// </summary>
        FocusOnShow = 0x0002000C,

        /// <summary>
        /// Is the window focused?
        /// </summary>
        Focused = 0x00020001,

        /// <summary>
        /// Is the window visible?
        /// </summary>
        Visible = 0x00020004,

        /// <summary>
        /// Is the window maximized?
        /// </summary>
        Maximized = 0x00020008,

        /// <summary>
        /// Is the window maximized?
        /// </summary>
        Iconified = 0x00020002,

        /// <summary>
        /// Does the window support a transparent framebuffer?
        /// </summary>
        TransparentFramebuffer = 0x0002000A,

        /// <summary>
        /// Which <see cref="ClientApi"/> was used during window creation.
        /// </summary>
        ClientApi = 0x00022001,

        /// <summary>
        /// Which <see cref="ContextCreationApi"/> was used during window creation.
        /// </summary>
        ContextCreationApi = 0x0002200B,

        /// <summary>
        /// The context major version set during context creation.
        /// </summary>
        ContextVersionMajor = 0x00022002,

        /// <summary>
        /// The context minor version set during context creation.
        /// </summary>
        ContextVersionMinor = 0x00022003,

        /// <summary>
        /// The context revision number set during context creation.
        /// </summary>
        ContextVersionRevision = 0x00022004,

        /// <summary>
        /// The <see cref="Desktop.ContextRobustness"/> set during context creation.
        /// </summary>
        ContextRobustness = 0x00022005,

        /// <summary>
        /// Is the window's OpenGL context forward compatible?
        /// </summary>
        OpenGLForwardCompatibility = 0x00022006,

        /// <summary>
        /// Is the window's OpenGL context a debug context?
        /// </summary>
        OpenGLDebugContext = 0x00022007,

        /// <summary>
        /// The <see cref="Desktop.OpenGLProfile"/> set during context creation.
        /// </summary>
        OpenGLProfile = 0x00022008
    }
}
