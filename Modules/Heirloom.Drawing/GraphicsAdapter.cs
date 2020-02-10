using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class GraphicsAdapter : IDisposable
    {
        #region Constructor

        protected GraphicsAdapter()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Unable to create a second instance of GraphicsAdapter. Dispose the first instance.");
            }

            Instance = this;

            // Query capabilities
            Capabilities = QueryCapabilities();

            // Construct resource managers
            SurfaceFactory = CreateSurfaceFactory();
            ShaderFactory = CreateShaderFactory();
        }

        #endregion

        #region Singleton

        protected static GraphicsAdapter Instance { get; private set; }

        /// <summary>
        /// Gets the capabilities of the graphics adapter associated with this application.
        /// </summary>
        public static GraphicsCapabilities Capabilities { get; private set; }

        /// <summary>
        /// Implementation of shader resources.
        /// </summary>
        protected internal static IShaderFactory ShaderFactory { get; private set; }

        /// <summary>
        /// Implementation of shader resources.
        /// </summary>
        protected internal static ISurfaceFactory SurfaceFactory { get; private set; }

        #endregion

        protected abstract GraphicsCapabilities QueryCapabilities();

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
                    Capabilities = default;
                    Instance = null;
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
            object Create(IntSize size, MultisampleQuality multisample);

            void Dispose(object native);
        }
    }
}
