using Heirloom.Math;

namespace Heirloom.Desktop
{
    /// <summary>
    /// Represents a physical display on the current device.
    /// </summary>
    public class Monitor
    {
        internal readonly MonitorHandle MonitorHandle;

        internal Monitor(string name, MonitorHandle monitor)
        {
            MonitorHandle = monitor;
            Name = name;
        }

        #region Properties

        /// <summary>
        /// Gets the human-readable name of the monitor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the width (in pixels) of the monitor (in the current video mode).
        /// </summary>
        public int Width => CurrentMode.Width;

        /// <summary>
        /// Gets the width (in pixels) of the monitor (in the current video mode).
        /// </summary>
        public int Height => CurrentMode.Height;

        /// <summary>
        /// Gets the refresh rate of the monitor (in the current video mode).
        /// </summary>
        public int RefreshRate => CurrentMode.RefreshRate;

        /// <summary>
        /// Gets the virtual position of the monitor (in screen units).
        /// </summary>
        public IntVector Position => Application.Invoke(() =>
        {
            Glfw.GetMonitorPosition(MonitorHandle, out var x, out var y);
            return new IntVector(x, y);
        });

        /// <summary>
        /// Gets the work area (in screen units) of the monitor.
        /// This is the monitor bounds minus any global task or menu bars.
        /// </summary>
        public IntRectangle Workarea => Application.Invoke(() =>
        {
            var area = default(IntRectangle);
            Glfw.GetMonitorWorkarea(MonitorHandle, out area.X, out area.Y, out area.Width, out area.Height);
            return area;
        });

        /// <summary>
        /// Gets the current video mode on this monitor.
        /// </summary>
        public VideoMode CurrentMode => Application.Invoke(() => Glfw.GetVideoMode(MonitorHandle));

        #endregion

        /// <summary>
        /// Gets all known video modes on this monitor.
        /// </summary>
        public VideoMode[] GetVideoModes()
        {
            return Application.Invoke(() => Glfw.GetVideoModes(MonitorHandle));
        }
    }
}
