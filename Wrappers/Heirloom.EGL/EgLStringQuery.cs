namespace Heirloom.EGL
{
    /// <summary>
    /// An enum used to select the information returned by <see cref="EGLDisplay.Query(EglStringQuery)"/> 
    /// </summary>
    internal enum EglStringQuery : uint
    {
        /// <summary> EGL_CLIENT_APIS </summary>
        ClientApi = 0x308D,
        /// <summary> EGL_VENDOR </summary>
        Vendor = 0x3053,
        /// <summary> EGL_VERSION </summary>
        Version = 0x3054,
        /// <summary> EGL_EXTENSIONS </summary>
        Extensions = 0x3055
    }
}
