using System;

namespace Heirloom.Drawing
{
    public sealed class Shader
    {
        private readonly string[] _paths;
        private readonly object _native;

        public Shader(params string[] paths)
        {
            _paths = paths ?? throw new ArgumentNullException(nameof(paths));

            // 
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            var frag = default(string);
            var vert = default(string);

            // 
            foreach (var path in paths)
            {
                var type = ShaderFactory.GetShaderType(path);
                var text = ShaderFactory.GetSource(path);

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

            // 
            _native = GraphicsAdapter.Instance.CompileShader(vert, frag);
        }

        public void SetUniform<T>(string name, T value) where T : unmanaged
        {
            throw new NotImplementedException();
        }
    }
}
