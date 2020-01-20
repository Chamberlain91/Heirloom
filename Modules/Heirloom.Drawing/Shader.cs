using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using Heirloom.IO;
using Heirloom.Math;

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
            UniformStorageMap = new Dictionary<string, UniformStorage>();

            // 
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            // Resolve vertex and fragment shader paths
            var (vertShaderPath, fragShaderPath) = ShaderFactory.GetShaderPaths(paths);

            // Creates a name to help identify this shader
            Name = GetShaderName(vertShaderPath, fragShaderPath);

            // Compile shader program from vertex and source fragments
            Native = ShaderFactory.LoadAndCompile(Name, vertShaderPath, fragShaderPath, out var uniforms);

            // Create storage objects for each uniform
            foreach (var uniform in uniforms)
            {
                // todo: Smart alias "myUnform[0]" as "myUnform"?
                UniformStorageMap[uniform.Name] = new UniformStorage();
            }
        }

        private static string GetShaderName(string vertShaderPath, string fragShaderPath)
        {
            var vname = Path.GetFileNameWithoutExtension(vertShaderPath).ToLowerInvariant();
            var fname = Path.GetFileNameWithoutExtension(fragShaderPath).ToLowerInvariant();
            return Regex.Replace($"{vname}-{fname}", "\\s+", "_");
        }

        #endregion

        /// <summary>
        /// The name of the shader (composed from names of input files) for debugging purposes.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Enumerates the uniforms defined in this shader.
        /// </summary>
        public IEnumerable<UniformInfo> Uniforms => UniformStorageMap.Values.Select(x => x.Info);

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

        private void SetUniformValue(string name, object value)
        {
            // TODO: Validate uniform type is an acceptable type?
            //       It does this inside the GL implementation, but this deferred 
            //       until just before the batch is submitted.

            // Attempt to get the uniform storage
            if (UniformStorageMap.TryGetValue(name, out var uniform))
            {
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
            public UniformInfo Info;

            public object Value;
            public bool IsDirty;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class UniformInfo
    {
        public UniformInfo(string name, UniformType type, IntSize dimensions, int arraySize)
        {
            Name = name;
            Type = type;
            Dimensions = dimensions;
            ArraySize = arraySize;
        }

        // ie, uStrength
        public string Name { get; }

        // ie, float, image, etc
        public UniformType Type { get; }

        // ie, float 1x3 => vec3
        public IntSize Dimensions { get; }

        // ie, 2 => float[2]
        public int ArraySize { get; }

        /// <summary>
        /// Is this uniform a vector?
        /// </summary>
        public bool IsVector => Dimensions.Width == 1 && Dimensions.Height > 1;

        /// <summary>
        /// Is this uniform a matrix?
        /// </summary>
        public bool IsMatrix => Dimensions.Width > 1 && Dimensions.Height > 1;

        /// <summary>
        /// Is this uniform an array?
        /// </summary>
        public bool IsArray => ArraySize > 1;
    }

    public enum UniformType
    {
        Float,
        Integer,
        UnsignedInteger,
        Bool,

        Image
    }
}
