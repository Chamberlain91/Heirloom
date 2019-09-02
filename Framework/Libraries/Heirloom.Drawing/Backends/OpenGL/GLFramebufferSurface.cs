using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.Backends.OpenGL
{
    internal sealed class GLFramebufferSurface : GLSurface
    {
        internal GLFramebufferSurface(IntSize size)
            : base(size)
        {
            Texture = new GLTexture(size);
        }

        public GLTexture Texture { get; }

        protected internal override void Prepare(OpenGLRenderContext context)
        {
            var framebuffer = context.GetFramebuffer(Texture);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, framebuffer.Handle);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, framebuffer.Handle);
        }

        protected override void Dispose(bool managed)
        {
            Texture.Dispose();
        }
    }
}
