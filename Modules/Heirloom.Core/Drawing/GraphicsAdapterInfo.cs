using System;

namespace Heirloom.Drawing
{
    public readonly struct GraphicsAdapterInfo
    {
        /// <summary>
        /// Gets a value that determines if this application has been detected to be running on a mobile platform.
        /// </summary>
        public readonly bool IsMobilePlatform;

        /// <summary>
        /// The adapter vedor (ie, NVIDIA or AMD).
        /// </summary>
        public readonly string Vendor;

        /// <summary>
        /// The adapter name (ie, GTX 1080).
        /// </summary>
        public readonly string Name;

        internal GraphicsAdapterInfo(bool isMobilePlatform, string vendor, string name)
        {
            IsMobilePlatform = isMobilePlatform;
            Vendor = vendor ?? throw new ArgumentNullException(nameof(vendor));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
