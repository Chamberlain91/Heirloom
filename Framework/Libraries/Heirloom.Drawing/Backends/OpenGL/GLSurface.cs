using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.Backends.OpenGL
{
    internal abstract class GLSurface : Surface
    {
        protected GLSurface(IntSize size)
            : base(size)
        { }

        // expose function with new visibility
        new internal void UpdateVersionNumber() { base.UpdateVersionNumber(); }

        protected internal abstract void Prepare(OpenGLRenderContext context);

        protected static void BindDefaultFramebuffer()
        {
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
        }
    }
}
