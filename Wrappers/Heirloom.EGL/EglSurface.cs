using System;

namespace Heirloom.OpenGLES.Platform
{
    public class EglSurface : IDisposable
    {
        internal IntPtr Address { get; set; }

        /// <summary>
        /// The window handle used to create the surface.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// The display device used to create the surface.
        /// </summary>
        public EglDisplay Display { get; private set; }

        /// <summary>
        /// Determines if this object was disposed ( unmanaged resources cleaned up ).
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// The width of the surface.
        /// </summary>
        public int Width
        {
            get
            {
                if (Egl.QuerySurface(Display, this, EglSurfaceQuery.Width, out var width))
                {
                    return width;
                }

                return 0;
            }
        }

        /// <summary>
        /// The height of the surface.
        /// </summary>
        public int Height
        {
            get
            {
                if (Egl.QuerySurface(Display, this, EglSurfaceQuery.Height, out var height))
                {
                    return height;
                }
                else
                {
                    return 0;
                }
            }
        }

        internal EglSurface(EglDisplay display, IntPtr address, IntPtr handle)
        {
            Handle = handle;

            Display = display;
            Address = address;

            // Use EGL_MULTISAMPLE_RESOLVE_BOX
            // EGL.SetSurfaceAttribute( display, this, 0x3099, 0x309B );
        }

        public bool SwapBuffers()
        {
            return Egl.SwapBuffers(Display, this);
        }

        public override string ToString()
        {
            return $"EGL SURFACE {Address}";
        }

        public void Dispose()
        {
            if (IsDisposed == false)
            {
                if (!Egl.DestroySurface(Display, this))
                {
                    throw new EglException("Unable to destroy surface");
                }

                IsDisposed = true;
            }
        }
    }
}
