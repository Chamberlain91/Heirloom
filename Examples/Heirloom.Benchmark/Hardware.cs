using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Heirloom.Benchmark
{
    public static class Hardware
    {
        public static ProcessorInfo GetProcessorInfo()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // windows 10, windows 7, etc
                return GetWindowsProcessorInfo();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // linux, ubuntu, etc
                return GetLinuxProcessorInfo();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS
                return GetMacProcessorInfo();
            }
            else
            {
                // No clue what platform we are on
                return ProcessorInfo.Unknown;
            }
        }

        private static ProcessorInfo GetWindowsProcessorInfo()
        {
            // 
            var wmicInfo = QueryProcess("wmic", "cpu get name, maxclockspeed, numberoflogicalprocessors /format:list").Split('\n');

            var clockSpeed = ProcessorInfo.Unknown.ClockSpeed;
            var coreCount = ProcessorInfo.Unknown.ProcessorCount;
            var name = ProcessorInfo.Unknown.Name;

            foreach (var line in wmicInfo)
            {
                var parts = line.Split('=');
                switch (parts[0].ToLower())
                {
                    case "numberoflogicalprocessors":
                        coreCount = int.Parse(parts[1]);
                        break;

                    case "maxclockspeed":
                        clockSpeed = int.Parse(parts[1]);
                        break;

                    case "name":
                        name = parts[1];
                        break;

                }
            }

            // Return processor information
            return new ProcessorInfo(name, clockSpeed, coreCount);
        }

        private static ProcessorInfo GetLinuxProcessorInfo()
        {
            return ProcessorInfo.Unknown;
        }

        private static ProcessorInfo GetMacProcessorInfo()
        {
            // shell -> sysctl - n machdep.cpu.brand_string
            // "Intel(R) Core(TM) i7 - 3770S CPU @ 3.10GHz"
            return ProcessorInfo.Unknown;
        }

        private static string QueryProcess(string exe, string args)
        {
            var p = new Process();

            // Configure process start info
            p.StartInfo.FileName = exe;   // ie, "ls"
            p.StartInfo.Arguments = args; // ie, "-a"
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;

            // Launch process
            p.Start();

            // Wait for all output
            var output = p.StandardOutput.ReadToEnd().Trim();
            p.WaitForExit();
            return output;
        }
    }
}
