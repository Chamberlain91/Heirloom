using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Hardware;
using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class ESGraphicsBackend : GraphicsBackend
    {
        internal static new ESGraphicsBackend Current => GraphicsBackend.Current as ESGraphicsBackend;

        protected internal bool IsMobilePlatform { get; private set; }

        internal readonly Dictionary<string, ESUniformBuffer> UniformBuffers;

        protected ESGraphicsBackend()
        {
            UniformBuffers = new Dictionary<string, ESUniformBuffer>();
        }

        internal override bool SupportsCustomShaders => true;

        internal override Uniform[] CompileShader(Shader shader)
        {
            var shaderName = shader.GetType().Name;

            // Compile shader program
            var program = Invoke(() =>
            {
                var fShader = new ESShaderStage(ShaderType.Fragment, shader.FragSource);
                var vShader = new ESShaderStage(ShaderType.Vertex, shader.VertSource);
                return new ESShaderProgram(shaderName, fShader, vShader);
            });

            // Store native object with shader
            SetBackendNativeObject(shader, program);

            // Get uniform names
            var uniforms = program.Uniforms.Where(u => u.BlockInfo == null)
                                           .Select(s => CreateUniformInfo(s.Info, s.Location))
                                           .ToArray();

            // Return shader uniform information
            return uniforms;

            #region Create Uniform Info

            static Uniform CreateUniformInfo(ActiveUniform uniform, int location)
            {
                var type = GetUniformType(uniform);
                var size = GetUniformSize(uniform);

                return new Uniform(location, uniform.Name, type, size, uniform.Size);
            }

            static UniformType GetUniformType(ActiveUniform uniform)
            {
                switch (uniform.Type)
                {
                    case ActiveUniformType.Bool:
                    case ActiveUniformType.BoolVec2:
                    case ActiveUniformType.BoolVec3:
                    case ActiveUniformType.BoolVec4:
                        return UniformType.Bool;

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
                        return UniformType.Float;

                    case ActiveUniformType.Integer:
                    case ActiveUniformType.IntVec2:
                    case ActiveUniformType.IntVec3:
                    case ActiveUniformType.IntVec4:
                        return UniformType.Integer;

                    case ActiveUniformType.UnsignedInteger:
                    case ActiveUniformType.UnsignedIntVec2:
                    case ActiveUniformType.UnsignedIntVec3:
                    case ActiveUniformType.UnsignedIntVec4:
                        return UniformType.UnsignedInteger;

                    case ActiveUniformType.Sampler2D:
                        return UniformType.Sampler2D;

                    default:
                        throw new NotSupportedException($"Unable to extract type, uniform type '{uniform.Type}' is not supported.");
                }
            }

            static IntSize GetUniformSize(ActiveUniform uniform)
            {
                switch (uniform.Type)
                {
                    case ActiveUniformType.Bool:
                    case ActiveUniformType.Float:
                    case ActiveUniformType.Integer:
                    case ActiveUniformType.Sampler2D:
                    case ActiveUniformType.UnsignedInteger:
                        return (1, 1);

                    case ActiveUniformType.BoolVec2:
                    case ActiveUniformType.FloatVec2:
                    case ActiveUniformType.IntVec2:
                    case ActiveUniformType.UnsignedIntVec2:
                        return (1, 2);

                    case ActiveUniformType.BoolVec3:
                    case ActiveUniformType.FloatVec3:
                    case ActiveUniformType.IntVec3:
                    case ActiveUniformType.UnsignedIntVec3:
                        return (1, 3);

                    case ActiveUniformType.BoolVec4:
                    case ActiveUniformType.FloatVec4:
                    case ActiveUniformType.IntVec4:
                    case ActiveUniformType.UnsignedIntVec4:
                        return (1, 4);

                    case ActiveUniformType.Matrix2:
                        return (2, 2);

                    case ActiveUniformType.Matrix2x3:
                        return (2, 3);

                    case ActiveUniformType.Matrix2x4:
                        return (2, 4);

                    case ActiveUniformType.Matrix3x2:
                        return (3, 2);

                    case ActiveUniformType.Matrix3:
                        return (3, 3);

                    case ActiveUniformType.Matrix3x4:
                        return (3, 4);

                    case ActiveUniformType.Matrix4x2:
                        return (4, 2);

                    case ActiveUniformType.Matrix4x3:
                        return (4, 3);

                    case ActiveUniformType.Matrix4:
                        return (4, 4);

                    default:
                        throw new NotSupportedException($"Unable to extract size, uniform type '{uniform.Type}' is not supported.");
                }
            }

            #endregion
        }

        protected internal override object GenerateNativeObject(GraphicsResource resource)
        {
            return Invoke<object>(() => resource switch
            {
                Surface surface => new ESSurfaceStorage(surface),
                Image image => new ESTexture(image.Size),

                _ => throw new InvalidOperationException($"Unable to generate native reresentation of {resource}"),
            });
        }

        protected internal abstract void Invoke(Action action);

        protected internal T Invoke<T>(Func<T> action)
        {
            var val = default(T);
            Invoke(() =>
            {
                val = action();
                return;
            });
            return val;
        }

        protected override GraphicsCapabilities GetGraphicsCapabilities()
        {
            // Textures will be intentionally limited to at most 64 megabytes (4096^2 * 4)

            return new GraphicsCapabilities
            {
                MaxTextureSize = Calc.Min(4096, GLES.GetInteger(GetParameter.MaxTextureSize)),
                MaxSupportedMultisample = (MultisampleQuality) GLES.GetInteger(GetParameter.MaxSamples)
            };
        }

        protected override GpuInfo GetGpuInfo()
        {
            // Detect if running in GL or ES
            IsMobilePlatform = DetectEmbeddedOpenGL();

            // Detect GPU Info
            var renderer = GLES.GetString(StringParameter.Renderer);
            var vendor = GLES.GetString(StringParameter.Vendor);
            return new GpuInfo(vendor, renderer);
        }

        private static bool DetectEmbeddedOpenGL()
        {
            // Query GL Version
            var version = GLES.GetString(StringParameter.Version);

            // Known ES prefixes
            var prefixes = new[]
            {
                "OpenGL ES ",
                "OpenGL ES-CM ",
                "OpenGL ES-CL "
            };

            // Does the version string match any known prefix
            return prefixes.Where(prefix => version.StartsWith(prefix)).Any();
        }
    }
}
