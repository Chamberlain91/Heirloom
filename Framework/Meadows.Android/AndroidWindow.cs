using System;
using System.Runtime.InteropServices;

using Android.Runtime;
using Android.Views;

namespace Meadows.Android
{
    internal static class AndroidWindow
    {
        // Get native window from surface
        [DllImport("android")]
        private static extern IntPtr ANativeWindow_fromSurface(IntPtr jni, IntPtr surface);

        // Get native window from surface
        [DllImport("android")]
        private static extern void ANativeWindow_release(IntPtr surface);

        internal static IntPtr GetWindowHandle(ISurfaceHolder holder)
        {
            var window = ANativeWindow_fromSurface(JNIEnv.Handle, holder.Surface.Handle);
            // Console.Debug( $"++ Acquiring Window Handle | {surface}" );
            return window;
        }

        internal static void ReleaseWindowHandle(IntPtr window)
        {
            // Console.Debug( $"++ Releasing Window Handle | {surface}" );
            ANativeWindow_release(window);
        }
    }
}
