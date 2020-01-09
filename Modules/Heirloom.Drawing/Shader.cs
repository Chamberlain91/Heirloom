using System;
using System.Collections.Generic;

namespace Heirloom.Drawing
{
    public sealed class Shader
    {
        private readonly string[] _paths;

        internal readonly Dictionary<string, Uniform> Uniforms;
        internal readonly object Native;

        public static Shader Default { get; }

        static Shader()
        {
            // Loadn and compile the default shader
            Default = new Shader("embedded/shaders/default.vert", "embedded/shaders/default.frag");
        }

        public Shader(params string[] paths)
        {
            _paths = paths ?? throw new ArgumentNullException(nameof(paths));

            // 
            Uniforms = new Dictionary<string, Uniform>();

            // 
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            // Load shader sources
            LoadShaderSource(paths, out var vert, out var frag);

            // Compile shader
            Native = GraphicsAdapter.Instance.CompileShader(vert, frag);
        }

        public void Scream()
        {
            Console.WriteLine($"UNIFORM MAP: {Uniforms.Count}");
            foreach (var (name, uniform) in Uniforms)
            {
                Console.WriteLine($"UNIFORM '{name}' IS '{uniform.Value}' ({uniform.IsDirty})");
            }
        }

        private static void LoadShaderSource(string[] paths, out string vert, out string frag)
        {
            vert = default;
            frag = default;

            // Load by paths and assign to frag or vert strings. 
            foreach (var path in paths)
            {
                var type = ShaderFactory.GetShaderType(path);
                var text = ShaderFactory.GetSourceCode(path);

                switch (type)
                {
                    case ShaderType.Vertex:
                        vert = text;
                        break;

                    case ShaderType.Fragment:
                        frag = text;
                        break;
                }
            }

            // Populate defaults for possibly null strings
            if (frag == null) { frag = ShaderFactory.GetSourceCode("embedded/shaders/default.frag"); }
            if (vert == null) { vert = ShaderFactory.GetSourceCode("embedded/shaders/default.vert"); }
        }

        public void SetUniform(string name, float[] arr)
        {
            SetUniformValue(name, arr);
        }

        public void SetUniform(string name, int[] arr)
        {
            SetUniformValue(name, arr);
        }

        public void SetUniform(string name, uint[] arr)
        {
            SetUniformValue(name, arr);
        }

        public void SetUniform(string name, bool[] arr)
        {
            SetUniformValue(name, arr);
        }

        public void SetUniform<T>(string name, T value) where T : unmanaged
        {
            SetUniformValue(name, value);
        }

        private void SetUniformValue(string name, object value)
        {
            // TODO: Validate uniform exists?
            // TODO: Validate uniform type is an acceptable type.

            // Attempt to get (or create) the uniform storage
            if (!Uniforms.TryGetValue(name, out var uniform))
            {
                Uniforms[name] = uniform = new Uniform();
            }

            // Update uniform value
            uniform.IsDirty = true;
            uniform.Value = value;
        }

        internal class Uniform
        {
            public object Value;
            public bool IsDirty;
        }
    }
}
