using System.Threading;

using Heirloom;
using Heirloom.Desktop;

namespace Cross_Window_Surfaces
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var winA = new Window("Cross Window Surfaces", (200, 100), vsync: false) { IsResizable = false };
                winA.MoveToCenter();

                var winB = new Window("Cross Window Surfaces", winA.Size, vsync: false) { IsResizable = false };
                winB.MoveToCenter();

                // Offset windows
                winA.Position -= (winA.Size.Width / 2 + 10, 0);
                winB.Position += (winA.Size.Width / 2 + 10, 0);

                // Bind closing event
                winA.Closed += WindowClosed;
                winB.Closed += WindowClosed;

                // 
                var surface = new Surface(winA.FramebufferSize);
                var counter = 0;

                RunThread(() =>
                {
                    var c = surface.Width / 2F;

                    while (true)
                    {
                        counter++; // count to ushort max and wrap
                        if (counter > ushort.MaxValue) { counter = 0; }

                        // Draw to surface using window A context
                        winA.Graphics.PushState(true);
                        {
                            winA.Graphics.Surface = surface;
                            winA.Graphics.Clear(Color.DarkGray);

                            // Draw red text
                            winA.Graphics.Color = Color.Red;
                            winA.Graphics.DrawText($"Count: {counter}", (c, 16), Font.Default, 32, TextAlign.Center);
                        }
                        // Draw surface to window A
                        winA.Graphics.PopState();
                        winA.Graphics.DrawImage(surface, Vector.Zero);
                        winA.Graphics.Commit(); // commit drawing to window

                        // Draw to surface using window A context
                        winB.Graphics.PushState(true);
                        {
                            winB.Graphics.Surface = surface;

                            // Draw cyan text
                            winB.Graphics.Color = Color.Cyan;
                            winB.Graphics.DrawText($"Count: {counter}", (c, 48), Font.Default, 32, TextAlign.Center);
                        }
                        // Draw surface to window B
                        winB.Graphics.PopState();
                        winB.Graphics.DrawImage(surface, Vector.Zero);
                        // Note: No need to commit here because RefreshScreen will

                        // Refresh Windows (Swap Buffers)
                        winA.Graphics.RefreshScreen();
                        winB.Graphics.RefreshScreen();
                    }
                });

                void WindowClosed(Window obj)
                {
                    if (!winA.IsClosed) { winA.Close(); }
                    if (!winB.IsClosed) { winB.Close(); }
                }
            });
        }

        private static Thread RunThread(ThreadStart action)
        {
            var thread = new Thread(action) { IsBackground = true };
            thread.Start();
            return thread;
        }
    }
}
