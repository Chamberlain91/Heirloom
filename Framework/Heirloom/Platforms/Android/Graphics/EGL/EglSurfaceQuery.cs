#if ANDROID
namespace Heirloom.Android.EGL
{
    internal enum EglSurfaceQuery : uint
    {
        Height = 0x3056,
        Width = 0x3057,
    }
}
#endif