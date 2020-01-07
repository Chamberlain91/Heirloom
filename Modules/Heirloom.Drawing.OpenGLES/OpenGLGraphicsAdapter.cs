using System;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    public abstract class OpenGLGraphicsAdapter : GraphicsAdapter
    {
        protected override GraphicsCapabilities QueryCapabilities()
        {
            return new GraphicsCapabilities(
                maxSupportedTextures: GL.GetInteger(GetParameter.MaxTextureImageUnits),
                isMobilePlatform: false);
        }

        protected override object CompileShader(string vert, string frag)
        {
            return Invoke(() =>
            {
                var vshader = new Shader("vert", ShaderType.Vertex, vert);
                var fshader = new Shader("frag", ShaderType.Fragment, frag);
                return new ShaderProgram(vshader, fshader);
            });
        }
    }
}
