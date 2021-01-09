using System;
using System.Runtime.InteropServices;

namespace Heirloom.Android.EGL
{
    internal static unsafe partial class Egl
    {
#pragma warning disable IDE1006 // Naming Styles

        public const string LibraryEGL = "libEGL";

        internal static readonly int EGL_NONE = 0x3038;

        internal static readonly int EGL_CONTEXT_CLIENT_VERSION = 0x3098;
        internal static readonly int EGL_HEIGHT = 0x3056;
        internal static readonly int EGL_WIDTH = 0x3057;

        internal static readonly IntPtr NO_CONTEXT = (IntPtr) 0;
        internal static readonly IntPtr NO_DISPLAY = (IntPtr) 0;
        internal static readonly IntPtr NO_SURFACE = (IntPtr) 0;

        internal static readonly IntPtr DefaultNativeDisplay = (IntPtr) 0;

        #region Api

        [DllImport(LibraryEGL)]
        private static extern EglBindApi eglQueryAPI();

        [DllImport(LibraryEGL)]
        private static extern void eglBindAPI(EglBindApi api);

        #endregion

        #region Query

        [DllImport(LibraryEGL)]
        private static extern sbyte* eglQueryString(IntPtr display, EglStringQuery name);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglQuerySurface(IntPtr display, IntPtr surface, EglSurfaceQuery name, out int result);

        #endregion

        #region Initialize / Terminate

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglInitialize(IntPtr display, int* major, int* minor);

        private static bool eglInitialize(IntPtr display)
        {
            return eglInitialize(display, (int*) 0, (int*) 0);
        }

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglTerminate(IntPtr display);

        #endregion

        #region Context

        [DllImport(LibraryEGL)]
        private static extern IntPtr eglCreateContext(IntPtr display, IntPtr config, IntPtr share_context, int[] attrib_list);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglDestroyContext(IntPtr display, IntPtr context);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglMakeCurrent(IntPtr display, IntPtr drawSurface, IntPtr readSurface, IntPtr context);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglReleaseThread();

        [DllImport(LibraryEGL)]
        public static extern IntPtr eglGetCurrentContext();

        #endregion

        #region Display

        [DllImport(LibraryEGL)]
        private static extern IntPtr eglGetDisplay(IntPtr native_display);

        [DllImport(LibraryEGL)]
        private static extern IntPtr eglGetPlatformDisplayEXT(int platform, IntPtr native_display, int[] attribs);

        [DllImport(LibraryEGL)]
        private static extern IntPtr eglGetCurrentDisplay();

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglSwapInterval(IntPtr display, int interval);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglSwapBuffers(IntPtr display, IntPtr surface);

        #endregion

        #region Surface

        [DllImport(LibraryEGL, SetLastError = true)]
        private static extern IntPtr eglCreateWindowSurface(IntPtr display, IntPtr config, IntPtr native_window, int[] attrib_list);

        [DllImport(LibraryEGL, SetLastError = true)]
        private static extern IntPtr eglCreatePbufferSurface(IntPtr display, IntPtr config, int* attrib_list);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglSurfaceAttrib(IntPtr display, IntPtr surface, int attrib, int value);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglDestroySurface(IntPtr display, IntPtr surface);

        [DllImport(LibraryEGL)]
        private static extern IntPtr eglGetCurrentSurface(EglSurfaceType readdraw);

        #endregion

        #region Config

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglGetConfigs(IntPtr display, IntPtr[] configs, int config_size, out int num_config);

        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglChooseConfig(IntPtr display, int[] attrib_list, IntPtr[] configs, int config_size, out int num_config);

        // ??????
        [DllImport(LibraryEGL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool eglGetConfigAttrib(IntPtr display, IntPtr config, EglConfigAttribute attribute, out int value);

        #endregion

        #region Error

        [DllImport(LibraryEGL)]
        private static extern EglErrorCode eglGetError();

        #endregion

        [DllImport(LibraryEGL)]
        private static extern void* eglGetProcAddress(string procname);

#pragma warning restore IDE1006 // Naming Styles
    }
}
