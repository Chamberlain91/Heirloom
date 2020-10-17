namespace Meadows.Drawing
{
    public abstract class Shader : NativeResource
    {
        public static readonly Shader Default = new DefaultShader();

        internal string VertexSource { get; }

        internal string FragmentSource { get; }

        protected Shader()
        {
            GraphicsContext.CompileShader(this, out var uniforms);
        }

        private sealed class DefaultShader : Shader
        {
            // todo: load default shader source code
        }
    }
}
