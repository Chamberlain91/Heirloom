using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Heirloom.Benchmark
{
    public static class Hardware
    {
        private static ProcessorInfo? _info;

        /// <summary>
        /// Gets the (best attempt) CPU information for the current computer.
        /// </summary>
        public static ProcessorInfo ProcessorInfo
        {
            get
            {
                if (!_info.HasValue)
                {
                    try
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            // windows 10, windows 7, etc
                            _info = GetWindowsProcessorInfo();
                        }
                        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        {
                            // linux, ubuntu, etc
                            _info = GetLinuxProcessorInfo();
                        }
                        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        {
                            // macOS
                            _info = GetMacProcessorInfo();
                        }
                        else
                        {
                            // No clue what platform we are on
                            _info = ProcessorInfo.Unknown;
                        }
                    }
                    catch
                    {
                        // Something went wrong when determining information
                        // todo: log warning?
                        _info = ProcessorInfo.Unknown;
                    }
                }

                return _info.Value;
            }
        }

        #region Windows

        private static ProcessorInfo GetWindowsProcessorInfo()
        {
            // 
            var wmicInfo = RunCommandLine("wmic", "cpu get name, maxclockspeed, numberoflogicalprocessors /format:list").Split('\n');

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
                        name = parts[1].Trim();
                        break;

                }
            }

            // Return processor information
            return new ProcessorInfo(name, clockSpeed, coreCount);
        }

        #endregion

        #region Linux

        private static ProcessorInfo GetLinuxProcessorInfo()
        {
            // 
            var wmicInfo = RunCommandLine("lscpu").Split('\n');

            var clockSpeed = ProcessorInfo.Unknown.ClockSpeed;
            var coreCount = ProcessorInfo.Unknown.ProcessorCount;
            var name = ProcessorInfo.Unknown.Name;

            foreach (var line in wmicInfo)
            {
                var parts = line.Split(':');
                var key = parts[0].ToLower();
                switch (key)
                {
                    case "model name":
                        name = parts[1].Trim();
                        break;

                    case "cpu max mhz":
                        clockSpeed = (int) float.Parse(parts[1]);
                        break;

                    case "cpu(s)":
                        coreCount = int.Parse(parts[1]);
                        break;
                }
            }

            // Return processor information
            return new ProcessorInfo(name, clockSpeed, coreCount);
        }

        #endregion

        #region macOS

        private static ProcessorInfo GetMacProcessorInfo()
        {
            // Query brand string for a human readable name
            var brand_string = RunCommandLine("sysctl", "-n machdep.cpu.brand_string");
            // ie, "Intel(R) Core(TM) i7 - 3770S CPU @ 3.10GHz"
            var name = brand_string.Trim();

            // Query number of logical processors
            var logical_per_package = RunCommandLine("sysctl", "-n machdep.cpu.logical_per_package");
            var logical_processors = int.Parse(logical_per_package);

            //  sysctl -n hw.cpufrequency
            var cpufrequency = RunCommandLine("sysctl", "-n hw.cpufrequency");
            var clock = float.Parse(cpufrequency) / (1000F * 1000F); // to megahertz

            return new ProcessorInfo(name, (int) clock, logical_processors);
        }

        #endregion

        private static string RunCommandLine(string exe, string args = "")
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
