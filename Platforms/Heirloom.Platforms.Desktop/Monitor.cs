using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Platforms.Desktop
{
    /// <summary>
    /// Represents a single monitor on the computer.
    /// </summary>
    public unsafe class Monitor
    {
        #region Constructors

        private Monitor(Glfw.Monitor* monitor)
        {
            Native = monitor;

            // Get monitor name
            Name = Marshal.PtrToStringAnsi((IntPtr) Glfw.GetMonitorName(monitor));

            // Get monitor position
            Glfw.GetMonitorPosition(monitor, out var x, out var y);
            Position = new IntVector(x, y);

            // Get monitor work area
            Glfw.GetMonitorWorkarea(monitor, out var wx, out var wy, out var ww, out var wh);
            WorkArea = new IntRectangle(wx, wy, ww, wh);

            // Get video modes
            var modes = Glfw.GetVideoModes(monitor, out var modeCount);
            Modes = Enumerable.Range(0, modeCount)
                              .Where(i => modes[i].RedBits == 8 && modes[i].BlueBits == 8 && modes[i].GreenBits == 8)
                              .Select(i => new VideoMode(this, modes[i]))
                              .ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The supported video modes of this monitor.
        /// </summary>
        public VideoMode[] Modes { get; }

        /// <summary>
        /// Gets the current video mode
        /// </summary>
        public VideoMode CurrentVideoMode => ContextManager.Invoke(() =>
        {
            // Get the current mode known to GLFW
            var mode = Glfw.GetVideoMode(Native);

            // Find exact match in our list of modes
            return Array.Find(Modes, m =>
            {
                return m.Width == mode->Width
                    && m.Height == mode->Height
                    && m.RefreshRate == mode->RefreshRate;
            });
        });

        internal Glfw.Monitor* Native { get; }

        /// <summary>
        /// Is the primary screen/monitor on the system?
        /// </summary>
        public bool IsPrimary { get; }

        /// <summary>
        /// The name of this screen/monitor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Position of this screen
        /// </summary>
        public IntVector Position { get; }

        /// <summary>
        /// Area not occupied by task or menu bars.
        /// </summary>
        public IntRectangle WorkArea { get; }

        #endregion

        #region Static

        private static readonly List<Monitor> _monitorList = new List<Monitor>();
        private static Monitor[] _monitors;

        internal static void AddMonitor(Glfw.Monitor* monitor)
        {
            _monitorList.Add(new Monitor(monitor));
            _monitors = null;
        }

        internal static void RemoveMonitor(Glfw.Monitor* monitor)
        {
            _monitorList.RemoveAll(s => s.Native == monitor);
            _monitors = null;
        }

        /// <summary>
        /// Gets a list of every screen
        /// </summary>
        public static IReadOnlyList<Monitor> GetMonitors()
        {
            // If we don't have a cached array of the monitors, create one
            // this should help prevent concurrent modification exception,
            // in the rare case a monitor is plugged in while the application
            // is running AND trying to access the screen list.
            if (_monitors == null)
            {
                lock (_monitorList)
                {
                    _monitors = _monitorList.ToArray();
                }
            }

            // Return screen array
            return _monitors;
        }

        /// <summary>
        /// Gets the primary monitor.
        /// </summary>
        public static Monitor Default => ContextManager.Invoke(() => _monitorList.Find(s => s.Native == Glfw.GetPrimaryMonitor()));

        #endregion
    }
}
