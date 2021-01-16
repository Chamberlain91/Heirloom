using System;
using System.Diagnostics;

namespace Heirloom.Hardware
{
    /// <summary>
    /// Utility class for extracting metadata of the hardware.
    /// </summary>
    internal static class HardwareDetector
    {
        internal static CpuInfo DetectCpuInfo()
        {
            try
            {
                switch (SystemInformation.OperatingSystem)
                {
                    case OperatingSystem.Windows:
                    {
                        GetWindowsProcessorInfo(out var name, out var clock, out var threads);
                        return new CpuInfo(name, clock, threads);
                    }

                    case OperatingSystem.Linux:
                    {
                        GetLinuxProcessorInfo(out var name, out var clock, out var threads);
                        return new CpuInfo(name, clock, threads);
                    }

                    case OperatingSystem.OSX:
                    {
                        GetMacProcessorInfo(out var name, out var clock, out var threads);
                        return new CpuInfo(name, clock, threads);
                    }
                }
            }
            catch
            {
                // Whoop, something went wrong. We don't care though...
                // We will just emit default "unknown" values below.
            }

            // Unknown CPU
            return CpuInfo.Unknown;
        }

        #region Query CPU Information

        #region Windows

        private static void GetWindowsProcessorInfo(out string name, out int clock, out int threads)
        {
            // 
            var wmicInfo = RunCommandLine("wmic", "cpu get name, maxclockspeed, numberoflogicalprocessors /format:list").Split('\n');

            // Start with unknown values
            clock = CpuInfo.Unknown.ClockSpeed;
            threads = CpuInfo.Unknown.ThreadCount;
            name = CpuInfo.Unknown.Name;

            foreach (var line in wmicInfo)
            {
                var parts = line.Split('=');
                switch (parts[0].ToLower())
                {
                    case "numberoflogicalprocessors":
                        threads = int.Parse(parts[1]);
                        break;

                    case "maxclockspeed":
                        clock = int.Parse(parts[1]);
                        break;

                    case "name":
                        name = parts[1].Trim();
                        break;

                }
            }
        }

        #endregion

        #region Linux

        private static void GetLinuxProcessorInfo(out string name, out int clock, out int threads)
        {
            // 
            var wmicInfo = RunCommandLine("lscpu").Split('\n');

            // Start with unknown values
            clock = CpuInfo.Unknown.ClockSpeed;
            threads = CpuInfo.Unknown.ThreadCount;
            name = CpuInfo.Unknown.Name;

            // 
            foreach (var line in wmicInfo)
            {
                var parts = line.Split(':');

                switch (parts[0].ToLower())
                {
                    case "model name":
                        name = parts[1].Trim();
                        break;

                    case "cpu max mhz":
                        clock = (int) float.Parse(parts[1]);
                        break;

                    case "cpu(s)":
                        threads = int.Parse(parts[1]);
                        break;
                }
            }
        }

        #endregion

        #region macOS

        private static void GetMacProcessorInfo(out string name, out int clock, out int threads)
        {
            // Query brand string for a human readable name
            var brand_string = RunCommandLine("sysctl", "-n machdep.cpu.brand_string");
            // ie, "Intel(R) Core(TM) i7 - 3770S CPU @ 3.10GHz"
            name = brand_string.Trim();

            // Query number of logical processors
            var logical_per_package = RunCommandLine("sysctl", "-n machdep.cpu.logical_per_package");
            threads = int.Parse(logical_per_package);

            //  sysctl -n hw.cpufrequency
            var cpufrequency = RunCommandLine("sysctl", "-n hw.cpufrequency");
            clock = (int) (float.Parse(cpufrequency) / (1000F * 1000F)); // to megahertz
        }

        #endregion

        [Obsolete]
        private static string RunCommandLine(string exe, string args = "")
        // TODO: I should as soon I have a free weekend replace these executable queries with
        // system library pinvokes to defend against side affects.
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

        #endregion
    }
}
