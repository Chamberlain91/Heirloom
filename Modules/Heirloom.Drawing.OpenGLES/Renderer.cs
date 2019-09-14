using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class Renderer
    {
        public abstract bool IsDirty { get; }

        public abstract void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color);

        public abstract void Flush();
    }
}
