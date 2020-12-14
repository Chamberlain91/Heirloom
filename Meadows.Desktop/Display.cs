using System.Linq;

using Meadows.Desktop.GLFW;
using Meadows.Mathematics;

namespace Meadows.Desktop
{
    public sealed class Display
    {
        internal readonly MonitorHandle MonitorHandle;

        internal Display(string name, MonitorHandle monitor)
        {
            MonitorHandle = monitor;
            Name = name;
        }

        #region Properties

        /// <summary>
        /// Gets the human-readable name of the display.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the width (in pixels) of the display (in the current video mode).
        /// </summary>
        public int Width => CurrentMode.Width;

        /// <summary>
        /// Gets the width (in pixels) of the display (in the current video mode).
        /// </summary>
        public int Height => CurrentMode.Height;

        /// <summary>
        /// Gets the refresh rate of the display (in the current video mode).
        /// </summary>
        public int RefreshRate => CurrentMode.RefreshRate;

        /// <summary>
        /// Gets the virtual position of the display (in screen units).
        /// </summary>
        public IntVector Position => Application.Invoke(() =>
        {
            Glfw.GetMonitorPosition(MonitorHandle, out var x, out var y);
            return new IntVector(x, y);
        });

        /// <summary>
        /// Gets the work area (in screen units) of the display.
        /// This is the display bounds minus any global task or menu bars.
        /// </summary>
        public IntRectangle Workarea => Application.Invoke(() =>
        {
            var area = default(IntRectangle);
            Glfw.GetMonitorWorkarea(MonitorHandle, out area.X, out area.Y, out area.Width, out area.Height);
            return area;
        });

        /// <summary>
        /// Gets the current video mode on this display.
        /// </summary>
        public VideoMode CurrentMode => Application.Invoke(() => Glfw.GetVideoMode(MonitorHandle));

        #endregion

        /// <summary>
        /// Gest the primary display.
        /// </summary>
        public static Display Primary { get; internal set; }

        /// <summary>
        /// Gets all known video modes on this display.
        /// </summary>
        public VideoMode[] GetVideoModes()
        {
            return Application.Invoke(() => Glfw.GetVideoModes(MonitorHandle));
        }

        /// <summary>
        /// Gets an array of the currently connected displays.
        /// </summary>
        public static Display[] GetDisplays()
        {
            return Application.Monitors.Values.ToArray();
        }
    }
}
