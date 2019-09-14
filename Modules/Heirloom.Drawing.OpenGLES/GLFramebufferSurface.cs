using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class GLFramebufferSurface : GLSurface
    {
        internal GLFramebufferSurface(int width, int height)
            : base(width, height)
        {
            Texture = new GLTexture(Size);
        }

        public GLTexture Texture { get; }

        protected internal override void Prepare(OpenGLRenderContext context)
        {
            var framebuffer = context.GetFramebuffer(Texture);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, framebuffer.Handle);
        }

        protected override void Dispose(bool managed)
        {
            Texture.Dispose();
        }
    }
}
