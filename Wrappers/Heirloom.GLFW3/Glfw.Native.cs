using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.GLFW3
{
    internal static unsafe partial class Glfw
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS0649  // Default value null
#pragma warning disable CS0169  // Unassigned

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool glfwInit();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode glfwGetError(char** description);

        #region Monitor

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorHandle glfwGetPrimaryMonitor();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorHandle* glfwGetMonitors(out int count);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern char* glfwGetMonitorName(MonitorHandle monitor);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern VideoMode* glfwGetVideoModes(MonitorHandle monitor, out int count);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr glfwGetVideoMode(MonitorHandle monitor);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorCallback glfwSetMonitorCallback(MonitorCallback callback);

        #endregion

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern WindowHandle glfwCreateWindow(int width, int height, string title, MonitorHandle monitor, WindowHandle share);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwShowWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwHideWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwFocusWindow(WindowHandle window);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern void glfwPollEvents();

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS0649  // Default value null
#pragma warning restore CS0169  // Unassigned
    }
}
