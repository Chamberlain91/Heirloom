namespace Meadows.Drawing
{
    public abstract class Shader : NativeResource
    {
        public static Shader Default { get; }

        protected Shader()
        {
        }
    }
}
