#if ANDROID
namespace Heirloom.Android.EGL
{
    /// <summary>
    /// An enum used to select a surface based on if its bound to read or draw.
    /// </summary>
    internal enum EglSurfaceType : uint
    {
        /// <summary> EGL_READ </summary>
        Read = 0x305A,
        /// <summary> EGL_DRAW </summary>
        Draw = 0x3059
    }
}
#endif
