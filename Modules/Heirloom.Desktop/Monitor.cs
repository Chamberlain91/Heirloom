using System;
using System.Collections.Generic;

using Heirloom.Desktop;
using Heirloom.Math;

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
        /// Gets the human-readable name of the monitor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the current video mode on this monitor.
        /// </summary>
        public VideoMode CurrentVideoMode => Application.Invoke(() => Glfw.GetVideoMode(MonitorHandle));

        /// <summary>
        /// Gets the work area of the monitor (ie, region ignoring taskbar).
        /// </summary>
        public IntRectangle Workarea => Application.Invoke(() =>
        {
            var area = default(IntRectangle);
            Glfw.GetMonitorWorkarea(MonitorHandle, out area.X, out area.Y, out area.Width, out area.Height);
            return area;
        });

        /// <summary>
        /// Gets all known video modes on this monitor.
        /// </summary>
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
        public static IEnumerable<Monitor> Monitors => _monitors.Values;

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
