using System;

using Meadows.Hardware;

namespace Meadows.Drawing
{
    public abstract class GraphicsBackend : IDisposable
    {
        protected GraphicsBackend()
        {
            if (Current != null)
            {
                throw new InvalidOperationException("Unable to initialize graphics backend, a previous backend instance already exists!");
            }

            Current = this;
        }

        #region Shaders

        internal abstract bool SupportsCustomShaders { get; }

        internal abstract Uniform[] CompileShader(Shader shader);

        public GraphicsCapabilities Capabilities { get; private set; }

        internal GpuInfo GpuInfo { get; private set; }

        #endregion

        protected abstract GraphicsCapabilities GetGraphicsCapabilities();

        protected abstract GpuInfo GetGpuInfo();

        internal static GraphicsBackend Current { get; private set; }

        internal static bool IsInitialized { get; private set; }

        internal static void InitializeBackend()
        {
            if (IsInitialized == false)
            {
                Log.Debug($"Initializing Graphics Backend: {Current.GetType().Name}");

                // Detect GPU information
                Current.Capabilities = Current.GetGraphicsCapabilities();
                Current.GpuInfo = Current.GetGpuInfo();

                // Log capabilities
                Log.Debug(Current.Capabilities);

                // Initialize shader backend
                Shader.InitializeDefaults();

                // Mark backend as properly initialized.
                IsInitialized = true;
            }
        }

        public virtual void Dispose()
        {
            Log.Debug($"Initializing Graphics Backend: {GetType().Name}");

            GC.SuppressFinalize(this);
            IsInitialized = false;
            Current = null;
        }

        #region Native Backend Objects

        protected internal abstract object GenerateNativeObject(GraphicsResource resource);

        protected internal T GetNativeObject<T>(GraphicsResource resource) where T : class, IDisposable
        {
            if (!(resource.GetBackendNativeObject() is T obj))
            {
                // No backend object is known for this resource, we must now create one.
                obj = GenerateNativeObject(resource) as T;
                if (obj == null) { throw new InvalidOperationException("Generated a global native object that does not match the requested type!"); }

                // Store the backend object for next time
                SetBackendNativeObject(resource, obj);
            }

            return obj;
        }

        protected internal void SetBackendNativeObject<T>(GraphicsResource resource, T obj) where T : class, IDisposable
        {
            resource.SetBackendNativeObject(obj);
        }

        #endregion
    }
}
