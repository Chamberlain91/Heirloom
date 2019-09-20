//using Heirloom.OpenGLES;

//namespace Heirloom.Drawing.OpenGLES
//{
//    internal sealed class GLFramebufferSurface : GLSurface
//    {
//        internal GLFramebufferSurface(int width, int height)
//            : base(width, height)
//        {
//            Texture = new Texture(Size);
//        }

//        public Texture Texture { get; }

//        protected internal override void Prepare(OpenGLRenderContext context)
//        {
//            var framebuffer = ResourceManager.GetFramebuffer(context, Texture);
//            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, framebuffer.Handle);
//        }

//        protected override void Dispose(bool managed)
//        {
//            Texture.Dispose();
//        }
//    }
//}
