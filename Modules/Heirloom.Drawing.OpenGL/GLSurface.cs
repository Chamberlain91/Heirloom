using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGL
{
    internal abstract class GLSurface : Surface
    {
        protected GLSurface(int width, int height)
            : base(width, height)
        { }

        // expose function with new visibility
        new internal void UpdateVersionNumber() { base.UpdateVersionNumber(); }

        protected internal abstract void Prepare(OpenGLRenderContext context);

        protected static void BindDefaultFramebuffer()
        {
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
        }
    }
}
