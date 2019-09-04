using System;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.Backends.OpenGL
{
    internal sealed class GLShader : IDisposable
    {
        private bool _isDisposed = false;

        #region Constructors

        public GLShader(ShaderType type, string source)
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

        ~GLShader()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public uint Handle { get; }

        #endregion

        #region Dispose

        void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // TODO: dispose managed objects.
                }

                // TODO: free unmanaged resources
                // Schedule on *some* context for deletion?
                Console.WriteLine("WARN: Disposing Shader! OpenGL Resource Not Deleted.");

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
