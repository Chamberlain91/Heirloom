using System;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ShaderStage : IDisposable
    {
        #region Constructors

        public ShaderStage(ShaderType type, string source)
        {
            Handle = GL.CreateShader(type);
            GL.ShaderSource(Handle, source);
            GL.CompileShader(Handle);

            // 
            var info = GL.GetShaderInfoLog(Handle);
            if (!string.IsNullOrWhiteSpace(info)) { Console.WriteLine(info); }

            // Failed to compile
            if (GL.GetShader(Handle, ShaderParameter.CompileStatus) == 0)
            {
                throw new Exception(info);
            }
        }

        ~ShaderStage()
        {
            Dispose(false);
        }

        #endregion

        #region Properties 

        public uint Handle { get; }

        #endregion

        #region Dispose

        private bool _isDisposed = false;

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // ...
                }

                // Schedule for deletion on a GL thread.
                OpenGLGraphicsAdapter.Invoke(() => GL.DeleteShader(Handle));

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
