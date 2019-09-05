using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.GLFW3
{
    public static unsafe partial class Glfw
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS0649  // Default value null
#pragma warning disable CS0169  // Unassigned

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool glfwInit();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode glfwGetError(char** description);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorHandle glfwGetPrimaryMonitor();

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorHandle* glfwGetMonitors(out int count);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern MonitorCallback glfwSetMonitorCallback(MonitorCallback callback);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern char* glfwGetMonitorName(MonitorHandle monitor);

        [DllImport(Library), SuppressUnmanagedCodeSecurity]
        private static extern VideoMode* glfwGetVideoModes(MonitorHandle monitor, out int count);

        [DllImport(Library), SuppressUnmanagedCodeSecurity] 
        private static extern IntPtr glfwGetVideoMode(MonitorHandle monitor);

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS0649  // Default value null
#pragma warning restore CS0169  // Unassigned
    }
}
