using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using Heirloom.Drawing;

namespace Heirloom.Desktop.Hardware
{
    /// <summary>
    /// Utility class for extracting metadata of the hardware.
    /// </summary>
    internal static class HardwareDetector
    {
        // Some regex to match some GPU names
        private static readonly Regex _gpuGeforceRegex = new Regex(@"(GeForce [GR]TX[^/]*)/.*", RegexOptions.Compiled);
        private static readonly Regex _gpuRadeonRegex = new Regex(@"AMD Radeon ?\(TM\) (.+) (Series|Graphics)", RegexOptions.Compiled);
        private static readonly Regex _gpuIntelRegex = new Regex(@".*Intel.* (.+ .+ \d+)", RegexOptions.Compiled);

        // Some regex to match some CPU names
        private static readonly Regex _cpuRyzenRegex = new Regex(@"AMD Ryzen (\d \d+\w?) ?.*", RegexOptions.Compiled);
        private static readonly Regex _cpuIntelRegex = new Regex(@"Intel.+Core.+ (i\d-\d+\w?) ?.*", RegexOptions.Compiled);

        internal static GpuInfo DetectGpuInfo()
        {
            // Query GPU info
            var deviceName = GraphicsAdapter.Info.Name;
            var vendor = GetGpuVendor(GraphicsAdapter.Info.Vendor);

            return new GpuInfo(vendor, deviceName);
        }

        internal static CpuInfo DetectCpuInfo()
        {
            // Query CPU info
            GetProcessorInfo(out var name, out var clock, out var threads);
            var vendor = GetCpuVendor(name);

            return new CpuInfo(vendor, name, clock, threads);
        }

        #region Get Cleaner Names

        internal static string GetCleanGpuName(GpuVendor vendor, string name)
        {
            switch (vendor)
            {
                case GpuVendor.Nvidia:
                {
                    // GeForce RTX 2080/pcie/SSE2 -> GeForce RTX 2080
                    var matches = _gpuGeforceRegex.Match(name);
                    if (matches.Success) { return matches.Groups[1].Value; }
                }
                break;

                case GpuVendor.AMD:
                {
                    // AMD Radeon(TM) Vega 8 Graphics -> Radeon Vega 8
                    var matches = _gpuRadeonRegex.Match(name);
                    if (matches.Success) { return $"Radeon {matches.Groups[1].Value}"; }
                }
                break;

                case GpuVendor.Intel:
                {
                    // Intel(R) UHD Graphics 630 -> UHD Graphics 630
                    var matches = _gpuIntelRegex.Match(name);
                    if (matches.Success) { return matches.Groups[1].Value; }
                }
                break;
            }

            // Eh, couldn't improve it
            return name;
        }

        internal static string GetCleanCpuName(CpuVendor vendor, string name)
        {
            switch (vendor)
            {
                case CpuVendor.Intel:
                {
                    // Intel(R) Core(TM) i7-8750H CPU @ 2.20GHz
                    var matches = _cpuIntelRegex.Match(name);
                    if (matches.Success) { return $"Core {matches.Groups[1].Value}"; }
                }
                break;

                case CpuVendor.AMD:
                {
                    // AMD Ryzen 7 2700X Eight-Core Processor -> Ryzen 7 2700X
                    var matches = _cpuRyzenRegex.Match(name);
                    if (matches.Success) { return $"Ryzen {matches.Groups[1].Value}"; }
                }
                break;
            }

            // Eh, couldn't improve it
            return name;
        }

        #endregion

        #region Detect Vendor Enum

        internal static CpuVendor GetCpuVendor(string name)
        {
            var vendorHints = new Dictionary<CpuVendor, string[]>
            {
                [CpuVendor.Intel] = new[] {
                    "INTEL", "I7", "I5", "I3"
                },
                [CpuVendor.AMD] = new[] {
                    "AMD", "RYZEN"
                }
            };

            // Detect CPU Vendor!
            return DetectVendor(null, vendorHints, name);
        }

        internal static GpuVendor GetGpuVendor(string name)
        {
            var vendors = new Dictionary<GpuVendor, string[]>
            {
                [GpuVendor.AMD] = new string[] {
                    "ATI TECHNOLOGIES INC"
                },
                [GpuVendor.Nvidia] = new string[] {
                    "NVIDIA CORPORATION"
                },
                [GpuVendor.Intel] = new string[] {
                    "INTEL OPEN SOURCE TECHNOLOGY CENTER",
                    "INTEL"
                }
            };

            var vendorHints = new Dictionary<GpuVendor, string[]>
            {
                [GpuVendor.AMD] = new string[] { "AMD", "ATI", "RADEON", "VEGA" },
                [GpuVendor.Nvidia] = new string[] { "NVIDIA", "GEFORCE", "GTX", "RTX" },
                [GpuVendor.Intel] = new string[] { "INTEL" }
            };

            // Detect GPU Vendor!
            return DetectVendor(vendors, vendorHints, name);
        }

        internal static TVendor DetectVendor<TVendor>(
            Dictionary<TVendor, string[]> vendorTemplates,
            Dictionary<TVendor, string[]> vendorHints,
            string vendorString)
        {
            // Normalize input by upper case and stripping non-alphanumeric
            // ie, "NVIDIA Corporation" -> "NVIDIA CORPORATION"
            vendorString = vendorString.ToUpper();
            vendorString = Regex.Replace(vendorString, @"[^\w\d ]", "");
            vendorString = Regex.Replace(vendorString, @" +", " ");

            // For each vendor
            foreach (var vendor in (TVendor[]) Enum.GetValues(typeof(TVendor)))
            {
                if (vendorTemplates != null)
                {
                    // Try to get known template strings
                    if (vendorTemplates.TryGetValue(vendor, out var templates))
                    {
                        // For each template string
                        foreach (var template in templates)
                        {
                            if (string.Equals(template, vendorString))
                            {
                                // Exact match!
                                return vendor;
                            }
                        }
                    }
                }

                // Could not determine exact match.
                // We will try to find by hints (brand names, etc).

                // Try to get known template strings
                if (vendorHints.TryGetValue(vendor, out var hints))
                {
                    // For each template string
                    foreach (var hint in hints)
                    {
                        // If the vendor string contains this hint, we
                        // assume it is correct. Hope for the best!
                        if (vendorString.Contains(hint))
                        {
                            // Hint detected, hopefully correct.
                            return vendor;
                        }
                    }
                }
            }

            return default;
        }

        #endregion

        #region Query CPU Information

        private static void GetProcessorInfo(out string name, out int clock, out int threads)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // windows 10, windows 7, etc
                    GetWindowsProcessorInfo(out name, out clock, out threads);
                    return;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // linux, ubuntu, etc
                    GetLinuxProcessorInfo(out name, out clock, out threads);
                    return;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // macOS
                    GetMacProcessorInfo(out name, out clock, out threads);
                    return;
                }
            }
            catch
            {
                // Whoop, something went wrong. We don't care though...
                // We will just emit default values below.
            }

            // Emit default values
            threads = CpuInfo.Unknown.ThreadCount;
            clock = CpuInfo.Unknown.ClockSpeed;
            name = CpuInfo.Unknown.Name;
        }

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

        #endregion

        [Obsolete]
        private static string RunCommandLine(string exe, string args = "")
        // TODO: I should as soon I have a free afternoon replace these executable queries with
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
    }
}
