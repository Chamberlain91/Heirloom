using System;
using System.Linq;
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

        protected override object CompileShader(string name, string vert, string frag, out string[] uniforms)
        {
            var program = InvokeOnGLThread(() =>
            {
                var vShader = new ShaderStage(ShaderType.Vertex, vert);
                var fShader = new ShaderStage(ShaderType.Fragment, frag);

                return new ShaderProgram(name, vShader, fShader);
            });

            // Get uniform names
            uniforms = program.Uniforms.Where(u => u.BlockInfo == null)
                                       .Select(s => s.Info.Name)
                                       .ToArray();

            return program;
        }

        protected abstract T InvokeOnGLThread<T>(Func<T> action);
    }
}
