using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    public abstract class OpenGLGraphicsAdapter : GraphicsAdapter
    {
        protected override GraphicsCapabilities QueryCapabilities()
        {
            return new GraphicsCapabilities(
                maxSupportedTextures: GL.GetInteger(GetParameter.MaxTextureImageUnits),
                isMobilePlatform: DetectOpenGLES());
        }

        private static bool DetectOpenGLES()
        {
            var version = GL.GetString(StringParameter.Version);
            var embedded = false;

            var embeddedPrefixes = new[]
            {
                "OpenGL ES ",
                "OpenGL ES-CM ",
                "OpenGL ES-CL "
            };

            // Try to detect OpenGL ES
            foreach (var prefix in embeddedPrefixes)
            {
                if (version.StartsWith(prefix))
                {
                    // Strip prefix
                    version = version.Substring(prefix.Length);
                    embedded = true;
                    break;
                }
            }

            return embedded;
        }

        protected override object CompileShader(string vert, string frag)
        {
            return Invoke(() =>
            {
                var vShader = new ShaderStage("vert", ShaderType.Vertex, vert);
                var fShader = new ShaderStage("frag", ShaderType.Fragment, frag);

                return new ShaderProgram(vShader, fShader);
            });
        }
    }
}
