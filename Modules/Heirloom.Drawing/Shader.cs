using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides GLSL shader support for custom image effects and other visual processing.
    /// </summary>
    public sealed class Shader
    {
        private readonly string[] _paths;

        internal readonly Dictionary<string, Uniform> Uniforms;
        internal bool IsAnyUniformDirty;

        internal readonly object Native;

        /// <summary>
        /// The name of the shader (composed from input files) for debugging purposes.
        /// </summary>
        public string Name { get; }

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
        /// <param name="paths">Must specify at least one path to an asset, resolved using <see cref="IO.Files"/>.</param>
        public Shader(params string[] paths)
        {
            _paths = paths ?? throw new ArgumentNullException(nameof(paths));

            // 
            Uniforms = new Dictionary<string, Uniform>();

            // 
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            // Resolve vertex and fragment shader paths
            GetShaderPaths(paths, out var vertPath, out var fragPath);

            // Creates a name to help identify this shader
            Name = ComposeShaderName(vertPath, fragPath);

            // Load shader sources
            var vert = ShaderFactory.GetSourceCode(vertPath);
            var frag = ShaderFactory.GetSourceCode(fragPath);

            // Compile shader
            Native = GraphicsAdapter.Instance.CompileShader(Name, vert, frag, out var uniformNames);

            // Create storage for each uniform
            foreach (var uniform in uniformNames)
            {
                Uniforms[uniform] = new Uniform();
            }

            // todo: Smart alias "myUnform[0]" as "myUnform"?
        }

        private string ComposeShaderName(string vertPath, string fragPath)
        {
            if (vertPath is null) { throw new ArgumentNullException(nameof(vertPath)); }
            if (fragPath is null) { throw new ArgumentNullException(nameof(fragPath)); }

            var vname = Path.GetFileNameWithoutExtension(vertPath).ToLowerInvariant();
            var fname = Path.GetFileNameWithoutExtension(fragPath).ToLowerInvariant();
            return Regex.Replace($"{vname}-{fname}", "\\s+", "_");
        }

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

        private void SetUniformValue(string name, object value)
        {
            // TODO: Validate uniform type is an acceptable type?
            //       It does this inside the GL implementation, but this deferred 
            //       until just before the batch is submitted.

            // Attempt to get the uniform storage
            if (Uniforms.TryGetValue(name, out var uniform))
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

        private static void GetShaderPaths(string[] paths, out string vert, out string frag)
        {
            vert = default;
            frag = default;

            // Load by paths and assign to frag or vert strings. 
            foreach (var path in paths)
            {
                var type = ShaderFactory.GetShaderType(path);

                switch (type)
                {
                    case ShaderType.Vertex:
                        vert = path;
                        break;

                    case ShaderType.Fragment:
                        frag = path;
                        break;
                }
            }

            // Populate defaults for null strings (which should be common for fragment only shaders)
            if (frag == null) { frag = "embedded/shaders/default.frag"; }
            if (vert == null) { vert = "embedded/shaders/default.vert"; }
        }

        internal class Uniform
        {
            public object Value;
            public bool IsDirty;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
