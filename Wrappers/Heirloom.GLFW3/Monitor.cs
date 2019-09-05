using System;
using System.Collections.Generic;

namespace Heirloom.GLFW3
{
    public unsafe class Monitor
    {
        internal readonly Glfw.MonitorHandle Handle;

        internal Monitor(string name, Glfw.MonitorHandle handle)
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
            _monitors = new Dictionary<Glfw.MonitorHandle, Monitor>();

            // 
            Glfw.SetMonitorCallback(OnMonitorCallback);

            // Scan current monitors
            foreach (var monitor in Glfw.GetMonitors())
            {
                OnMonitorCallback(monitor, Glfw.ConnectState.Connected);
            }
        }

        private static void OnMonitorCallback(Glfw.MonitorHandle monitor, Glfw.ConnectState state)
        {
            var name = Glfw.GetMonitorName(monitor);

            var primary = Glfw.GetPrimaryMonitor();
            var isPrimary = primary == monitor;

            Console.WriteLine($"Monitor: \"{name}\" ({state}, isPrimary: {isPrimary})");

            // Connected Monitor
            if (state == Glfw.ConnectState.Connected)
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

        private static readonly Dictionary<Glfw.MonitorHandle, Monitor> _monitors;
    }
}
