using System;

namespace Heirloom
{
    internal abstract class GraphicsAdapter : IDisposable
    {
        #region Constructor

        internal void Initialize()
        {
            if (Adapter != null)
            {
                throw new InvalidOperationException($"Unable to initialize a second instance of {nameof(GraphicsAdapter)}. Dispose the first instance.");
            }

            IsInitialized = false;
            Adapter = this;

            // Get adapter info
            Info = GetAdapterInfo();

            // Construct resource managers
            SurfaceFactory = CreateSurfaceFactory();
            ShaderFactory = CreateShaderFactory();

            // Initialize drawing resources
            Shader.Initialize();

            IsInitialized = true;
        }

        #endregion

        public bool IsInitialized { get; private set; }

        public bool IsDisposed => _isDisposed;

        #region Singleton

        protected static GraphicsAdapter Adapter { get; private set; }

        /// <summary>
        /// Gets the capabilities of the graphics adapter associated with this application.
        /// </summary>
        public static AdapterInfo Info { get; private set; }

        /// <summary>
        /// Implementation of shader resources.
        /// </summary>
        protected internal static IShaderFactory ShaderFactory { get; private set; }

        /// <summary>
        /// Implementation of shader resources.
        /// </summary>
        protected internal static ISurfaceFactory SurfaceFactory { get; private set; }

        #endregion

        protected abstract AdapterInfo GetAdapterInfo();

        protected abstract ISurfaceFactory CreateSurfaceFactory();

        protected abstract IShaderFactory CreateShaderFactory();

        #region Dispose

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    Info = default;
                    Adapter = null;
                }

                // todo: dispose native?

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        protected internal interface IShaderFactory
        {
            object Compile(string name, string vert, string frag, out UniformInfo[] uniforms);

            void Dispose(object native);
        }

        protected internal interface ISurfaceFactory
        {
            MultisampleQuality MaxSupportedMultisampleQuality { get; }

            object Create(Surface surface);

            void Dispose(object native);
        }

        /// <summary>
        /// Contains basic information about the graphics adapter.
        /// </summary>
        public readonly struct AdapterInfo
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

            internal AdapterInfo(bool isMobilePlatform, string vendor, string name)
            {
                IsMobilePlatform = isMobilePlatform;
                Vendor = vendor ?? throw new ArgumentNullException(nameof(vendor));
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }
        }
    }
}
