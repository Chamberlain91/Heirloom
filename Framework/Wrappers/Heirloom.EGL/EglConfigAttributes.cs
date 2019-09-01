using System;
using System.Text;

namespace Heirloom.OpenGLES.Platform
{
    public struct EglConfigAttributes
    {
        public int RedBits;
        public int BlueBits;
        public int GreenBits;
        public int AlphaBits;
        public int DepthBits;
        public int StencilBits;
        public int Samples;

        internal EglRenderableType RenderableType;
        internal EglAttribSurfaceType SurfaceType;

        public EglConfigAttributes(int redBits, int blueBits, int greenBits, int alphaBits, int depthBits, int stencilBits, int samples = 0)
        {
            RedBits = redBits;
            BlueBits = blueBits;
            GreenBits = greenBits;
            AlphaBits = alphaBits;

            DepthBits = depthBits;

            StencilBits = stencilBits;

            Samples = samples;

            // 
            RenderableType = EglRenderableType.EGL_OPENGL_ES2_BIT;
            SurfaceType = EglAttribSurfaceType.MultisampleResolveBox;
        }

        public EglConfigAttributes(int colorBits, int depthBits, int stencilBits, int samples = 0)
            : this(colorBits, colorBits, colorBits, colorBits, depthBits, stencilBits, samples)
        { }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine($"R: {RedBits} G: {GreenBits} B: {BlueBits} A: {AlphaBits} D: {DepthBits} S: {StencilBits} M: {Samples}");
            return str.ToString();
        }
    }
}
