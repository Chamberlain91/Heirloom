
using Heirloom.Drawing;

namespace Heirloom.Runtime
{
    public abstract class LoadingScreen
    {
        protected LoadingScreen()
        {
            Message = "Default";
            Progress = 0.33F;
        }

        public string Message { get; internal set; }

        public float Progress { get; internal set; }

        protected internal abstract void Render(RenderContext ctx);
    }
}
