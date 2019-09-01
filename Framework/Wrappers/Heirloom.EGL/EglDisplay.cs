using System;
using System.Text;

namespace Heirloom.OpenGLES.Platform
{
    public class EglDisplay : IDisposable
    {
        internal IntPtr Address { get; set; }

        /// <summary>
        /// Gets the major version of the EGL implementation.
        /// </summary>
        public int MajorVersion { get; private set; }

        /// <summary>
        /// Gets the minor version of the EGL implementation.
        /// </summary>
        public int MinorVersion { get; private set; }

        /// <summary>
        /// Gets the name of the vendor of the EGL implementation.
        /// </summary>
        public string Vendor { get; private set; }

        /// <summary>
        /// <para> Gets the version string of the vendor of the EGL implementation. </para>
        /// This follows the format of "major.minor vendorattrib"
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// A string representing the client API available.
        /// </summary>
        public string ClientApi { get; private set; }

        /// <summary>
        /// All extension strings found available to this display.
        /// </summary>
        public string[] Extensions { get; private set; }

        /// <summary>
        /// All possible configurations to create contexts with.
        /// </summary>
        public EglConfig[] Configurations { get; private set; }

        /// <summary>
        /// Determines if this object was disposed ( unmanaged resources cleaned up ).
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Creates a display reference from the given window handle.
        /// </summary>
        /// <param name="address"></param>
        internal unsafe EglDisplay(IntPtr address)
        {
            Address = address;

            // Initialize display connection
            if (Egl.Initialize(this, out var major, out var minor))
            {
                // Store version
                MajorVersion = major;
                MinorVersion = minor;

                // Query strings
                Vendor = Egl.QueryString(this, EglStringQuery.Vendor);
                Version = Egl.QueryString(this, EglStringQuery.Version);
                ClientApi = Egl.QueryString(this, EglStringQuery.ClientApi);
                Extensions = Egl.QueryString(this, EglStringQuery.Extensions).Trim().Split(' ');

                // Extract configurations
                Configurations = Egl.GetConfigs(this);
            }
            else
            {
                throw new EglException("Unable to initialize EGL display");
            }
        }

        public override string ToString()
        {
            var Console = new StringBuilder();

            Console.AppendLine($"Vendor: {Vendor}");
            Console.AppendLine($"Version: {Version} ( {MajorVersion} {MinorVersion} )");
            Console.AppendLine($"Api: {ClientApi}");
            Console.AppendLine();
            Console.AppendLine($"Extensions:");
            foreach (var ext in Extensions)
            {
                Console.AppendLine($"\t{ext}");
            }

            return Console.ToString();
        }

        public void SetSwapInterval(int interval)
        {
            if (!Egl.SwapInterval(this, interval))
            {
                throw new EglException("Unable to set swap interval");
            }
        }

        public void Dispose()
        {
            if (IsDisposed == false)
            {
                // Terminate display connection
                if (!Egl.Terminate(this))
                {
                    throw new EglException("Unable to terminate EGL display");
                }

                IsDisposed = true;
            }
        }
    }
}
