using System;

using Meadows.Hardware;

namespace Meadows.Drawing
{
    public abstract class GraphicsBackend : IDisposable
    {
        #region Shaders

        internal abstract bool SupportsCustomShaders { get; }

        internal abstract Uniform[] CompileShader(Shader shader);

        #endregion

        internal abstract GpuInfo GetGpuInfo();

        internal static GraphicsBackend Current { get; private set; }

        protected static void InitializeRenderingSystems(GraphicsBackend backend)
        {
            if (Current != null)
            {
                throw new InvalidOperationException("Unable to initialize graphics backend, a previous backend instance already exists!");
            }

            Current = backend;

            // Initialize shader backend
            Shader.InitializeDefaults();
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            Current = null;
        }

        #region Native Backend Objects

        protected internal abstract object GenerateNativeObject(GraphicsResource resource);

        protected internal T GetNativeObject<T>(GraphicsResource resource) where T : class, IDisposable
        {
            if (resource.GetBackendNativeObject() is not T obj)
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
