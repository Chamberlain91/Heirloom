using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Platforms.Desktop
{
    public unsafe class Screen
    {
        #region Constructors

        private Screen(Glfw.Monitor* monitor)
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
            Modes = new VideoMode[modeCount];
            for (var i = 0; i < modeCount; i++)
            {
                Modes[i] = new VideoMode(this, modes[i]);
            }

            // In debug mode, print information about the monitor to console
            PrintScreenDiagnostics();
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
        public VideoMode CurrentVideoMode => GraphicsContext.Invoke(() =>
        {
            // Get the current mode known to GLFW
            var mode = Glfw.GetVideoMode(Native);

            // Find exact match in our list of modes
            return Array.Find(Modes, m =>
            {
                return m.Width == mode->Width
                    && m.Height == mode->Height
                    && m.RefreshRate == mode->RefreshRate
                    && m.RedBits == mode->RedBits
                    && m.GreenBits == mode->GreenBits
                    && m.BlueBits == mode->BlueBits;
            });
        });

        internal Glfw.Monitor* Native { get; }

        public bool IsPrimary { get; }

        public string Name { get; }

        public IntVector Position { get; }

        /// <summary>
        /// Area not occupied by task or menu bars.
        /// </summary>
        public IntRectangle WorkArea { get; }

        #endregion

        [Conditional("DEBUG")]
        private void PrintScreenDiagnostics()
        {
            Console.WriteLine($"Detected Screen: {Name} at {Position} w/ {Modes.Length} modes.");
            foreach (var mode in Modes)
            {
                Console.WriteLine($"    {mode.Width}x{mode.Height} @ {mode.RefreshRate}hz ({mode.RedBits}, {mode.GreenBits}, {mode.BlueBits})");
            }
        }

        #region Static

        private static readonly List<Screen> _screenList = new List<Screen>();
        private static Screen[] _screens;

        internal static void AddScreen(Glfw.Monitor* monitor)
        {
            _screenList.Add(new Screen(monitor));
            _screens = null;
        }

        internal static void RemoveScreen(Glfw.Monitor* monitor)
        {
            _screenList.RemoveAll(s => s.Native == monitor);
            _screens = null;
        }

        /// <summary>
        /// Gets a list of every screen
        /// </summary>
        public static IReadOnlyList<Screen> GetScreens()
        {
            // If we don't have a cached array of the monitors, create one
            // this should help prevent concurrent modification exception,
            // in the rare case a monitor is plugged in while the application
            // is running AND trying to access the screen list.
            if (_screens == null)
            {
                lock (_screenList)
                {
                    _screens = _screenList.ToArray();
                }
            }

            // Return screen array
            return _screens;
        }

        /// <summary>
        /// Gets the primary screen.
        /// </summary>
        public static Screen GetPrimaryScreen()
        {
            return GraphicsContext.Invoke(() =>
                _screenList.Find(s => s.Native == Glfw.GetPrimaryMonitor()));
        }

        #endregion
    }
}
