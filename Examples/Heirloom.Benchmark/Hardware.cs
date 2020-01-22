using System.Runtime.InteropServices;

namespace Heirloom.Benchmark
{
    public static class Hardware
    {
        public static ProcessorInfo GetProcessorInfo()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsProcessorName();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return GetLinuxProcessorName();
            }
            else
            {
                return ProcessorInfo.Unknown;
            }
        }

        private static ProcessorInfo GetWindowsProcessorName()
        {
            return ProcessorInfo.Unknown;
        }

        private static ProcessorInfo GetLinuxProcessorName()
        {
            return ProcessorInfo.Unknown;
        }
    }
}
