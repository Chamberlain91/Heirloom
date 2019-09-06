using System;
using System.Collections.Generic;
using Heirloom.GLFW3;

namespace Heirloom.Desktop
{
    public class Monitor
    {
        internal readonly MonitorHandle Handle;

        internal Monitor(string name, MonitorHandle handle)
        {
            Handle = handle;
            Name = name;
        }

        public string Name { get; }

        public VideoMode[] GetVideoModes()
        {
            return Glfw.GetVideoModes(Handle);
        }

        public VideoMode GetCurrentVideoMode()
        {
            return Glfw.GetVideoMode(Handle);
        }

        public void SetFullscreen(Window window)
        {
            throw new NotImplementedException();
        }

        public static Monitor Default { get; private set; }

        public static IEnumerable<Monitor> GetMonitors()
        {
            return _monitors.Values;
        }

        static Monitor()
        {
            _monitors = new Dictionary<MonitorHandle, Monitor>();

            // 
            Glfw.SetMonitorCallback(OnMonitorCallback);

            // Scan current monitors
            foreach (var monitor in Glfw.GetMonitors())
            {
                OnMonitorCallback(monitor, ConnectState.Connected);
            }
        }

        private static void OnMonitorCallback(MonitorHandle monitor, ConnectState state)
        {
            var name = Glfw.GetMonitorName(monitor);

            var primary = Glfw.GetPrimaryMonitor();
            var isPrimary = primary == monitor;

            Console.WriteLine($"Monitor: \"{name}\" ({state}, isPrimary: {isPrimary})");

            // Connected Monitor
            if (state == ConnectState.Connected)
            {
                // We can only insert if unknown
                if (!_monitors.ContainsKey(monitor))
                {
                    _monitors[monitor] = new Monitor(name, monitor);
                }
            }
            // Disconnected Monitor
            else
            {
                // We can only remove if known already
                if (_monitors.ContainsKey(monitor))
                {
                    _monitors.Remove(monitor);
                }
            }

            // Set default monitor
            Default = _monitors[primary];
        }

        private static readonly Dictionary<MonitorHandle, Monitor> _monitors;
    }
}
