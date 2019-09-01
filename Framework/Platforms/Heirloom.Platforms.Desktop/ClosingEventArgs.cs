using System;

namespace Heirloom.Platforms.Desktop
{
    public class ClosingEventArgs : EventArgs
    {
        /// <summary>
        /// Should we cancel closing the window.
        /// </summary>
        public bool PreventClose;
    }
}
