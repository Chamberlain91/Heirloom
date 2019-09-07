using System;
using System.Collections.Generic;

using Heirloom.GLFW;

namespace Heirloom.Desktop
{
    public class Monitor
    {
        internal readonly MonitorHandle MonitorHandle;

        internal Monitor(string name, MonitorHandle monitor)
        {
            MonitorHandle = monitor;
            Name = name;
        }

        /// <summary>
        /// The human-readable name of the monitor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The current video mode on this monitor.
        /// </summary>
        public VideoMode CurrentVideoMode => Application.Invoke(() => Glfw.GetVideoMode(MonitorHandle));

        /// <summary>
        /// Get known video modes on this monitor.
        /// </summary>
        /// <returns></returns>
        public VideoMode[] GetVideoModes()
        {
            return Application.Invoke(() => Glfw.GetVideoModes(MonitorHandle));
        }

        #region Static

        private static readonly Dictionary<MonitorHandle, Monitor> _monitors;

        static Monitor()
        {
            _monitors = new Dictionary<MonitorHandle, Monitor>();

            // Register monitor callback, invoked when the monitor configuration changes.
            Glfw.SetMonitorCallback(OnMonitorCallback);

            // Scan currently connected monitors
            foreach (var monitor in Glfw.GetMonitors())
            {
                OnMonitorCallback(monitor, ConnectState.Connected);
            }
        }

        /// <summary>
        /// The default (primary) monitor.
        /// </summary>
        public static Monitor Default { get; private set; }

        /// <summary>
        /// Get all currently connected monitors.
        /// </summary>
        public static IEnumerable<Monitor> GetMonitors()
        {
            return _monitors.Values;
        }

        private static void OnMonitorCallback(MonitorHandle monitor, ConnectState state)
        {
            var name = Glfw.GetMonitorName(monitor);

            var primary = Glfw.GetPrimaryMonitor();
            var isPrimary = primary == monitor;

            Console.WriteLine($"Found Monitor: \"{name}\" ({state}, isPrimary: {isPrimary})");

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

        #endregion
    }
}
