using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.IO;
using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides GLSL shader support for custom image effects and other visual processing.
    /// </summary>
    public abstract partial class Shader : IDisposable
    {
        private readonly string[] _paths;

        // 
        internal readonly Dictionary<string, UniformStorage> UniformStorageMap;
        internal bool IsDirty;

        // 
        internal readonly object Native;

        #region Static

        /// <summary>
        /// Gets the default (ie, "no effect") shader.
        /// </summary>
        public static Shader Default { get; private set; }

        internal static void Initialize()
        {
            // todo: Grab info like max uniforms or max block size.
            // Load and compile the default shader
            Default = new DefaultShader();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new shader from either a vertex shader (.vert) or a fragment shader (.frag).
        /// </summary>
        /// <param name="path">Must specify at the path to a shader asset, resolved using <see cref="Files.OpenStream(string)"/>.</param>
        /// <param name="defines">Zero or more define directives to add to the loaded shader.</param>
        protected Shader(string path, params Define[] defines)
            : this(new string[] { path }, defines)
        { }

        /// <summary>
        /// Constructs a new shader from both a vertex shader (.vert) and a fragment shader (.frag).
        /// </summary>
        /// <param name="path1">Must specify at the path to a shader asset (either .frag or .vert), resolved using <see cref="Files.OpenStream(string)"/>.</param>
        /// <param name="path2">Must specify at the path to the other shader asset, resolved using <see cref="Files.OpenStream(string)"/>.</param>
        /// <param name="defines">Zero or more define directives to add to the loaded shader.</param>
        protected Shader(string path1, string path2, params Define[] defines)
            : this(new string[] { path1, path2 }, defines)
        { }

        /// <summary>
        /// Constructs a new shader from either a vertex shader (.vert), a fragment shader (.frag) or both.
        /// </summary>
        /// <param name="paths">Must specify at least one path to a shader asset, resolved using <see cref="Files.OpenStream(string)"/>.</param>
        /// <param name="defines">Zero or more define directives to add to the loaded shader.</param>
        protected Shader(string[] paths, params Define[] defines)
        {
            _paths = paths ?? throw new ArgumentNullException(nameof(paths));

            // 
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            // Compile shader program from vertex and source fragments
            Native = Factory.LoadAndCompile(GetType().Name, paths, defines, out var uniforms);

            // == Create Uniform Storage
            UniformStorageMap = new Dictionary<string, UniformStorage>();

            // For each detected uniform, create a storage object.
            foreach (var uniform in uniforms)
            {
                // todo: Smart alias "myUnform[0]" as "myUnform"?
                UniformStorageMap[uniform.Name] = new UniformStorage(uniform);
            }
        }

        ~Shader()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The paths used to create this shader object.
        /// </summary>
        public IReadOnlyList<string> Paths => _paths;

        /// <summary>
        /// Enumerates the uniforms defined in this shader.
        /// </summary>
        protected IEnumerable<UniformInfo> Uniforms => UniformStorageMap.Values.Select(x => x.Info);

        #endregion

        #region Set Uniform

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SetUniform(string name, float[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SetUniform(string name, int[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SetUniform(string name, uint[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SetUniform(string name, bool[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        /// <param name="value">The value to assign to the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SetUniform<T>(string name, T value) where T : unmanaged
        {
            SetUniformValue(name, value);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        /// <param name="image">An image to assign to the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SetUniform(string name, ImageSource image)
        {
            SetUniformValue(name, image);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetUniformValue(string name, object value)
        {
            // Attempt to get the uniform storage
            if (UniformStorageMap.TryGetValue(name, out var uniform))
            {
                // Update uniform value
                uniform.IsDirty = true;
                uniform.Value = value;

                // Mark that there was a change at all
                IsDirty = true;
            }
            else
            {
                throw new ArgumentException($"Unknown uniform '{name}' in shader '{GetType()}'.", nameof(name));
            }
        }

        #endregion

        internal class UniformStorage
        {
            public readonly UniformInfo Info;

            public object Value;
            public bool IsDirty;

            public UniformStorage(UniformInfo info)
            {
                Info = info ?? throw new ArgumentNullException(nameof(info));
            }
        }

        #region Dispose

        private bool _isDispsoed = false;

        private void Dispose(bool disposing)
        {
            if (!_isDispsoed)
            {
                if (disposing)
                {
                    // todo: Somehow invalidate shader source code in storage to
                    //       release the memory. I doubt this is a significant 
                    //       amount of memory, but it feels responsible to do so.
                }

                // Alert the implementation that this resource has been disposed
                GraphicsAdapter.ShaderFactory.Dispose(Native);

                _isDispsoed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Define Struct

        /// <summary>
        /// Holds a key-value pair for generating #define directives in the shader.
        /// </summary>
        protected readonly struct Define
        {
            /// <summary>
            /// The name of the directive.
            /// </summary>
            public readonly string Name;

            /// <summary>
            /// The replacement value of the directive (may be null).
            /// </summary>
            public readonly string Value;

            /// <summary>
            /// Creates a new define directive.
            /// </summary>
            public Define(string name, string value)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Shader symbol name must not be null or empty.", nameof(name));
                }

                Name = name;
                Value = value;
            }

            public static implicit operator Define((string name, string val) tuple)
            {
                return new Define(tuple.name, tuple.val);
            }

            public static implicit operator Define((string name, float val) tuple)
            {
                return new Define(tuple.name, $"{tuple.val}");
            }

            public static implicit operator Define((string name, float[] val) tuple)
            {
                return new Define(tuple.name, $"float[]({string.Join(", ", tuple.val)}");
            }

            public static implicit operator Define((string name, int val) tuple)
            {
                return new Define(tuple.name, $"{tuple.val}");
            }

            public static implicit operator Define((string name, int[] val) tuple)
            {
                return new Define(tuple.name, $"int[]({string.Join(", ", tuple.val)}");
            }

            public static implicit operator Define((string name, bool val) tuple)
            {
                return new Define(tuple.name, $"{tuple.val}");
            }

            public static implicit operator Define((string name, Vector val) tuple)
            {
                var vec = tuple.val;
                return new Define(tuple.name, $"vec2({vec.X}, {vec.Y})");
            }

            public static implicit operator Define((string name, Vector[] val) tuple)
            {
                var vecs = tuple.val.Select(vec => $"vec2({vec.X}, {vec.Y})");
                return new Define(tuple.name, $"vec2[]({string.Join(", ", vecs)}");
            }

            public static implicit operator Define((string name, Size val) tuple)
            {
                var siz = tuple.val;
                return new Define(tuple.name, $"vec2({siz.Width}, {siz.Height})");
            }

            public static implicit operator Define((string name, Color val) tuple)
            {
                var col = tuple.val;
                return new Define(tuple.name, $"vec4({col.R}, {col.G}, {col.B}, {col.A})");
            }
        }

        #endregion

        private sealed class DefaultShader : Shader
        {
            public DefaultShader()
                : base("embedded/shaders/default.vert", "embedded/shaders/default.frag")
            { }
        }
    }
}
