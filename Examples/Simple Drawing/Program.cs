using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.SimpleDrawing
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            var sw = Stopwatch.StartNew();
            var count = 0;
            var fps = 0F;

            Application.Run(() =>
            {
                var windows = new Window[Colors.FlatUI.All.Length];
                for (var i = 0; i < windows.Length; i++)
                {
                    windows[i] = new Window(200, 200, $"Window {i}", vsync: true);
                    windows[i].Position = (16 + (i % 4) * 200, 16 + (i / 4) * 200);
                    windows[i].Resizable = false;
                }

                Task.Run(() =>
                {
                    // 
                    while (windows.Any(w => !w.IsClosed))
                    {
                        var time = (float) sw.Elapsed.TotalSeconds;

                        // 
                        if (time > 1F)
                        {
                            // 
                            fps = count / time;
                            count = 0;

                            //
                            sw.Restart();
                        }

                        for (var i = 0; i < windows.Length; i++)
                        {
                            var win = windows[i];

                            if (!win.IsClosed)
                            {
                                var ctx = win.RenderContext;
                                var col = Colors.FlatUI.All[i % Colors.FlatUI.All.Length];

                                // todo: we needlessly have to call reset resize viewport, fix this.
                                ctx.ResetState();

                                // 
                                ctx.Clear(Color.Lerp(col, Color.White, time));
                                ctx.DrawText($"FPS: {fps}", (10, 10, win.FramebufferSize.Width - 20, win.FramebufferSize.Height - 20), TextAlign.Left, Font.Default, 16, Color.White);
                                ctx.SwapBuffers();
                            }
                        }

                        // 
                        count++;
                    }

                    Console.WriteLine($"Task {System.Threading.Thread.CurrentThread.ManagedThreadId} Exit.");
                });
            });
        }
    }
}
