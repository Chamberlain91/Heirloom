using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.IO;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides GLSL shader support for custom image effects and other visual processing.
    /// </summary>
    public sealed class Shader
    {
        private readonly string[] _paths;

        // 
        internal readonly Dictionary<string, UniformStorage> UniformStorageMap;
        internal bool IsAnyUniformDirty;

        // 
        internal readonly object Native;

        #region Static

        /// <summary>
        /// Gets the default (ie, "no effect") shader.
        /// </summary>
        public static Shader Default { get; }

        static Shader()
        {
            // Loadn and compile the default shader
            Default = new Shader("embedded/shaders/default.vert", "embedded/shaders/default.frag");
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new shader from either a vertex shader (.vert), a fragment shader (.frag) or both.
        /// </summary>
        /// <param name="paths">Must specify at least one path to an asset, resolved using <see cref="Files.OpenStream(string)"/>.</param>
        public Shader(params string[] paths)
        {
            _paths = paths ?? throw new ArgumentNullException(nameof(paths));

            // 
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            // Compile shader program from vertex and source fragments
            Native = ShaderFactory.LoadAndCompile(paths, out var name, out var uniforms);

            // Store the shader name
            Name = name;

            // == Create Uniform Storage
            UniformStorageMap = new Dictionary<string, UniformStorage>();

            // For each detected uniform, create a storage object.
            foreach (var uniform in uniforms)
            {
                // todo: Smart alias "myUnform[0]" as "myUnform"?
                UniformStorageMap[uniform.Name] = new UniformStorage(uniform);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The name of the shader (composed from names of input files) for debugging purposes.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Enumerates the uniforms defined in this shader.
        /// </summary>
        public IEnumerable<UniformInfo> Uniforms => UniformStorageMap.Values.Select(x => x.Info);

        #endregion

        #region Set Uniform

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUniform(string name, float[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUniform(string name, int[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUniform(string name, uint[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUniform(string name, bool[] arr)
        {
            SetUniformValue(name, arr);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        /// <param name="value">The value to assign to the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUniform<T>(string name, T value) where T : unmanaged
        {
            SetUniformValue(name, value);
        }

        /// <summary>
        /// Updates one of the shader uniforms by name.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        /// <param name="image">An image to assign to the uniform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUniform(string name, ImageSource image)
        {
            SetUniformValue(name, image);
        }

        private void SetUniformValue(string name, object value)
        {
            // Attempt to get the uniform storage
            if (UniformStorageMap.TryGetValue(name, out var uniform))
            {
                // Validate the given value is acceptable by the uniform
                if (!uniform.Info.IsAcceptable(value))
                {
                    throw new ArgumentException($"Uniform '{name}' does not have an acceptable type '{Name}'.", nameof(name));
                }

                // Update uniform value
                uniform.IsDirty = true;
                uniform.Value = value;

                // Mark that there was a change at all
                IsAnyUniformDirty = true;
            }
            else
            {
                throw new ArgumentException($"Unknown uniform '{name}' in shader '{Name}'.", nameof(name));
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

        public override string ToString()
        {
            return Name;
        }
    }
}
