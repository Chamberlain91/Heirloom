using System.Threading;

using Heirloom.Drawing;

namespace Heirloom.Desktop
{
    public abstract class GameWindow : Window
    {
        protected GameWindow(string title, bool vsync = true, bool transparentFramebuffer = false)
            : base(800, 600, title, vsync, transparentFramebuffer)
        {
            Maximize();
        }

        private static void RunBackgroundThread(ThreadStart threadStart)
        {
            var thread = new Thread(threadStart) { Name = "Game Thread", IsBackground = true };
            thread.Start();
        }

        public void Run()
        {
            RunBackgroundThread(() =>
            {
                // 
                while (!IsClosed)
                {
                    // == Update Phase

                    Update();

                    // == Render Phase

                    RenderContext.ResetState();
                    Draw(RenderContext);
                    RenderContext.SwapBuffers();
                }
            });
        }

        protected abstract void Update();

        protected abstract void Draw(RenderContext renderContext);
    }
}
