using System;

namespace Heirloom.Drawing
{
    public abstract class GraphicsAdapter : IDisposable
    {
        private bool _isDisposed = false;

        protected GraphicsAdapter()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Unable to create a second instance of GraphicsAdapter. Dispose the first.");
            }

            Instance = this;

            // Query capabilities
            Capabilities = QueryCapabilities();
        }

        protected internal static GraphicsAdapter Instance { get; protected set; }

        protected internal GraphicsCapabilities Capabilities { get; }

        protected abstract GraphicsCapabilities QueryCapabilities();

        protected internal abstract object CompileShader(string vert, string frag);

        protected virtual void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    Instance = null;
                }

                // native

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
