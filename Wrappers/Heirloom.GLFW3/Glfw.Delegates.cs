using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.GLFW3
{
    public static unsafe partial class Glfw
    {
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal unsafe delegate void MonitorCallback([MarshalAs(UnmanagedType.Struct)] MonitorHandle monitor, ConnectState state);
    }
}
