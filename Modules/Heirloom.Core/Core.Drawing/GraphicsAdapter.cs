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
        public static GraphicsAdapterInfo Info { get; private set; }

        /// <summary>
        /// Implementation of shader resources.
        /// </summary>
        protected internal static IShaderFactory ShaderFactory { get; private set; }

        /// <summary>
        /// Implementation of shader resources.
        /// </summary>
        protected internal static ISurfaceFactory SurfaceFactory { get; private set; }

        #endregion

        protected abstract GraphicsAdapterInfo GetAdapterInfo();

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

            object Create(IntSize size, ref MultisampleQuality multisample);

            void Dispose(object native);
        }
    }
}
