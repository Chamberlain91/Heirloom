using System;
using System.Linq;
using Heirloom.Math;
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

        protected override object CompileShader(string name, string vert, string frag, out UniformInfo[] uniforms)
        {
            var program = InvokeOnGLThread(() =>
            {
                var vShader = new ShaderStage(ShaderType.Vertex, vert);
                var fShader = new ShaderStage(ShaderType.Fragment, frag);

                return new ShaderProgram(name, vShader, fShader);
            });

            // Get uniform names
            uniforms = program.Uniforms.Where(u => u.BlockInfo == null)
                                       .Select(s => CreateUniformInfo(s.Info))
                                       .ToArray();

            return program;
        }

        protected abstract T InvokeOnGLThread<T>(Func<T> action);

        private UniformInfo CreateUniformInfo(ActiveUniform uniform)
        {
            var type = GetUniformType();
            var size = GetUniformSize();

            return new UniformInfo(uniform.Name, type, size, uniform.Size);

            IntSize GetUniformSize()
            {
                IntSize size;
                switch (uniform.Type)
                {
                    case ActiveUniformType.Bool:
                    case ActiveUniformType.Float:
                    case ActiveUniformType.Integer:
                    case ActiveUniformType.Sampler2D:
                    case ActiveUniformType.UnsignedInteger:
                        size = (1, 1);
                        break;

                    case ActiveUniformType.BoolVec2:
                    case ActiveUniformType.FloatVec2:
                    case ActiveUniformType.IntVec2:
                    case ActiveUniformType.UnsignedIntVec2:
                        size = (1, 2);
                        break;

                    case ActiveUniformType.BoolVec3:
                    case ActiveUniformType.FloatVec3:
                    case ActiveUniformType.IntVec3:
                    case ActiveUniformType.UnsignedIntVec3:
                        size = (1, 3);
                        break;

                    case ActiveUniformType.BoolVec4:
                    case ActiveUniformType.FloatVec4:
                    case ActiveUniformType.IntVec4:
                    case ActiveUniformType.UnsignedIntVec4:
                        size = (1, 4);
                        break;

                    case ActiveUniformType.Matrix2:
                        size = (2, 2);
                        break;

                    case ActiveUniformType.Matrix2x3:
                        size = (2, 3);
                        break;

                    case ActiveUniformType.Matrix2x4:
                        size = (2, 4);
                        break;

                    case ActiveUniformType.Matrix3x2:
                        size = (3, 2);
                        break;

                    case ActiveUniformType.Matrix3:
                        size = (3, 3);
                        break;

                    case ActiveUniformType.Matrix3x4:
                        size = (3, 4);
                        break;

                    case ActiveUniformType.Matrix4x2:
                        size = (4, 2);
                        break;

                    case ActiveUniformType.Matrix4x3:
                        size = (4, 3);
                        break;

                    case ActiveUniformType.Matrix4:
                        size = (4, 4);
                        break;

                    default:
                        throw new NotSupportedException($"Unable to extract size, uniform type '{uniform.Type}' is not supported.");
                }

                return size;
            }

            UniformType GetUniformType()
            {
                UniformType type;
                switch (uniform.Type)
                {
                    case ActiveUniformType.Bool:
                    case ActiveUniformType.BoolVec2:
                    case ActiveUniformType.BoolVec3:
                    case ActiveUniformType.BoolVec4:
                        type = UniformType.Bool;
                        break;

                    case ActiveUniformType.Matrix2:
                    case ActiveUniformType.Matrix2x3:
                    case ActiveUniformType.Matrix2x4:
                    case ActiveUniformType.Matrix3x2:
                    case ActiveUniformType.Matrix3:
                    case ActiveUniformType.Matrix3x4:
                    case ActiveUniformType.Matrix4x2:
                    case ActiveUniformType.Matrix4x3:
                    case ActiveUniformType.Matrix4:
                    case ActiveUniformType.Float:
                    case ActiveUniformType.FloatVec2:
                    case ActiveUniformType.FloatVec3:
                    case ActiveUniformType.FloatVec4:
                        type = UniformType.Float;
                        break;

                    case ActiveUniformType.Integer:
                    case ActiveUniformType.IntVec2:
                    case ActiveUniformType.IntVec3:
                    case ActiveUniformType.IntVec4:
                        type = UniformType.Integer;
                        break;

                    case ActiveUniformType.UnsignedInteger:
                    case ActiveUniformType.UnsignedIntVec2:
                    case ActiveUniformType.UnsignedIntVec3:
                    case ActiveUniformType.UnsignedIntVec4:
                        type = UniformType.UnsignedInteger;
                        break;

                    case ActiveUniformType.Sampler2D:
                        type = UniformType.Image;
                        break;

                    default:
                        throw new NotSupportedException($"Unable to extract type, uniform type '{uniform.Type}' is not supported.");
                }

                return type;
            }
        }
    }
}
