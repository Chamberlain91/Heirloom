using System;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class Sampler : IDisposable
    {
        private bool _isDisposed = false;

        public readonly uint Handle;

        #region Constructors

        public Sampler(InterpolationMode interpolation)
            : this(GetInterpolationMagFilter(interpolation), GetInterpolationMinFilter(interpolation))
        { }

        public Sampler(TextureMagFilter magFilter, TextureMinFilter minFilter)
        {
            Handle = GL.GenSampler();
            GL.SamplerParameter(Handle, TextureParameter.MagFilter, (int) magFilter);
            GL.SamplerParameter(Handle, TextureParameter.MinFilter, (int) minFilter);
            GL.SamplerParameter(Handle, TextureParameter.WrapS, (int) TextureWrap.Clamp);
            GL.SamplerParameter(Handle, TextureParameter.WrapT, (int) TextureWrap.Clamp);
        }

        ~Sampler()
        {
            Dispose(false);
        }

        #endregion

        public void Bind(uint unit)
        {
            GL.BindSampler(unit, Handle);
        }

        #region Get Filters by Interpolation

        private static TextureMinFilter GetInterpolationMinFilter(InterpolationMode interpolation)
        {
            return interpolation == InterpolationMode.Linear ? TextureMinFilter.NearestMipLinear : TextureMinFilter.NearestMipNearest;
        }

        private static TextureMagFilter GetInterpolationMagFilter(InterpolationMode interpolation)
        {
            return interpolation == InterpolationMode.Linear ? TextureMagFilter.Linear : TextureMagFilter.Nearest;
        }

        #endregion

        #region Dispose

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // nothing
                }

                OpenGLGraphicsAdapter.Schedule(() => GL.DeleteSampler(Handle));

                _isDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
