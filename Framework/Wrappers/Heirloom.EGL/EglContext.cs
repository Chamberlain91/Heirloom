using System;

namespace Heirloom.OpenGLES.Platform
{
    public class EglContext : IDisposable
    {
        internal IntPtr Address { get; set; }

        /// <summary>
        /// EGL Configuration used when creating this context.
        /// </summary>
        public EglConfig Config { get; private set; }

        /// <summary>
        /// EGL Display used when creating this context.
        /// </summary>
        public EglDisplay Display => Config.Display;

        /// <summary>
        /// Determines if this object was disposed ( unmanaged resources cleaned up ).
        /// </summary>
        public bool IsDisposed { get; private set; }

        internal EglContext(EglConfig config, IntPtr address)
        {
            Config = config;
            Address = address;
        }

        public override string ToString()
        {
            return $"EGL CONTEXT {Address}";
        }

        public void Dispose()
        {
            if (IsDisposed == false)
            {
                if (!Egl.DestroyContext(Config.Display, this))
                {
                    throw new EglException("Unable to destroy context");
                }

                IsDisposed = true;
            }
        }
    }
}
