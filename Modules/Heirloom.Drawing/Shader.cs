using System;

namespace Heirloom.Drawing
{
    public sealed class Shader : IDrawingResource
    {
        private readonly string[] _paths;
        private readonly object _native;

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
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            // Load shader sources
            LoadShaderSource(paths, out var vert, out var frag);

            // Compile shader
            _native = GraphicsAdapter.Instance.CompileShader(vert, frag);
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

        public void SetUniform<T>(string name, T value) where T : unmanaged
        {
            throw new NotImplementedException();
        }

        object IDrawingResource.NativeObject
        {
            get => _native;
            set => throw new NotImplementedException();
        }

        uint IDrawingResource.Version => throw new NotImplementedException();

        void IDrawingResource.UpdateVersionNumber()
        {
            throw new NotImplementedException();
        }
    }
}
