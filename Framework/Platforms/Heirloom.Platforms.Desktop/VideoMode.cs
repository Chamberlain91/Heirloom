﻿using Heirloom.GLFW;

namespace Heirloom.Platforms.Desktop
{
    /// <summary>
    /// Represents a video mode of a specific screen.
    /// </summary>
    public class VideoMode
    {
        private Glfw.VideoMode _mode;

        internal VideoMode(Screen screen, Glfw.VideoMode mode)
        {
            Screen = screen;
            _mode = mode;
        }

        /// <summary>
        /// The screen this video mode represents.
        /// </summary>
        public Screen Screen { get; }

        /// <summary>
        /// Gets the width of the screen when this mode is active.
        /// </summary>
        public int Width => _mode.Width;

        /// <summary>
        /// Gets the height of the screen when this mode is active.
        /// </summary>
        public int Height => _mode.Height;

        /// <summary>
        /// Gets the refresh rate of the screen when this mode is active.
        /// </summary>
        public int RefreshRate => _mode.RefreshRate;

        // ...?

        public int RedBits => _mode.RedBits;

        public int BlueBits => _mode.BlueBits;

        public int GreenBits => _mode.GreenBits;
    }
}
