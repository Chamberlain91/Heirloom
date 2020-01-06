using System;

namespace Heirloom.Drawing
{
    public sealed class Shader
    {
        private readonly string[] _paths;
        private object _native;

        public Shader(params string[] paths)
        {
            _paths = paths ?? throw new ArgumentNullException(nameof(paths));

            // 
            if (_paths.Length == 0) { throw new ArgumentException("Must specify at least one path."); }
            if (_paths.Length >= 3) { throw new ArgumentException("Must at most two paths."); }

            // 
            foreach (var path in paths)
            {
                var text = ShaderFactory.GetSource(path);
                Console.WriteLine($"{text}");
            }

            // 
            _native = DrawingResources.Instance.CompileShader();
        }

        public void SetUniform<T>(string name, T value) where T : unmanaged
        {
            throw new NotImplementedException();
        }
    }
}
