namespace Heirloom.Drawing
{
    public abstract class DrawingResources
    {
        protected internal static DrawingResources Instance { get; protected set; }

        protected internal abstract object CompileShader();

        protected internal class ShaderInfo
        {

        }
    }
}
