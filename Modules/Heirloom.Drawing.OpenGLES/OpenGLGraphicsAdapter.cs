using System;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    public abstract class OpenGLGraphicsAdapter : GraphicsAdapter
    {
        #region Query Capabilities

        protected override GraphicsCapabilities QueryCapabilities()
        {
            var renderer = GL.GetString(StringParameter.Renderer);
            var vendor = GL.GetString(StringParameter.Vendor);

            return new GraphicsCapabilities(
                adapterName: renderer,
                adapterVendor: vendor,
                maxSupportedVertexImages: GL.GetInteger(GetParameter.MaxVertexTextureImageUnits),
                maxSupportedFragmentImages: GL.GetInteger(GetParameter.MaxTextureImageUnits),
                isMobilePlatform: DetectEmbeddedOpenGL());
        }

        private static bool DetectEmbeddedOpenGL()
        {
            // Query GL Version
            var version = GL.GetString(StringParameter.Version);

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

        #endregion

        protected override IShaderManager CreateShaderManager()
        {
            return new ShaderManager(this);
        }

        #region Resource Managers

        private abstract class ResourceManager
        {
            protected ResourceManager(OpenGLGraphicsAdapter adapter)
            {
                Adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            }

            public OpenGLGraphicsAdapter Adapter { get; }
        }

        private sealed class ShaderManager : ResourceManager, IShaderManager
        {
            public ShaderManager(OpenGLGraphicsAdapter adapter)
                : base(adapter)
            { }

            public object Compile(string name, string vert, string frag, out UniformInfo[] uniforms)
            {
                var program = Adapter.Invoke(() =>
                {
                    var vShader = new ShaderStage(Adapter, ShaderType.Vertex, vert);
                    var fShader = new ShaderStage(Adapter, ShaderType.Fragment, frag);

                    return new ShaderProgram(Adapter, name, vShader, fShader);
                });

                // Get uniform names
                uniforms = program.Uniforms.Where(u => u.BlockInfo == null)
                                           .Select(s => CreateUniformInfo(s.Info))
                                           .ToArray();

                return program;
            }

            public void Dispose(object native)
            {
                // Dispose shader program
                (native as ShaderProgram)?.Dispose();
            }

            #region Create Uniform Info

            private static UniformInfo CreateUniformInfo(ActiveUniform uniform)
            {
                var type = GetUniformType(uniform);
                var size = GetUniformSize(uniform);

                return new UniformInfo(uniform.Name, type, size, uniform.Size);
            }

            private static UniformType GetUniformType(ActiveUniform uniform)
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

            private static IntSize GetUniformSize(ActiveUniform uniform)
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

            #endregion
        }

        #endregion

        #region Invoke

        protected internal abstract T Invoke<T>(Func<T> action);

        protected internal abstract void Invoke(Action action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Schedule(Action action)
        {
            var adapter = Instance as OpenGLGraphicsAdapter;
            adapter.Invoke(action); // go!
        }

        #endregion
    }
}
