using System.Runtime.InteropServices;

namespace Heirloom.Hardware
{
    public static class SystemInformation
    {
        public static OperatingSystem OperatingSystem { get; } = GetOperatingSystem();

        public static CpuInfo Cpu { get; } = HardwareDetector.DetectCpuInfo();

        public static GpuInfo Gpu { get; private set; }

        private static OperatingSystem GetOperatingSystem()
        {
#if ANDROID
            // Running MonoAndroid so... Android.
            return OperatingSystem.Android;
#else
            // Ask .NET for which OS we are on
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) { return OperatingSystem.OSX; }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return OperatingSystem.Windows; }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) { return OperatingSystem.Linux; }
            else
            {
                // Just couldn't figure it out
                return OperatingSystem.Unknown;
            }
#endif
        }

        internal static void UpdateGPUInfo()
        {
            Gpu = HardwareDetector.DetectGpuInfo();
        }
    }
}
