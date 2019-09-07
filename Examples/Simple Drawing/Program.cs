using System.Diagnostics;
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

            Application.Run(() =>
            {
                var window = new Window(512, 512, "Example Window");

                Task.Run(() =>
                {
                    var ctx = window.RenderContext;

                    // 
                    while (!window.IsDisposed)
                    {
                        var time = (float) sw.Elapsed.TotalSeconds;

                        var v = Calc.Sin(time) * 0.5F + 0.5F;

                        ctx.ResetState(); // todo: have to call to resize viewport, fix this.
                        ctx.Clear(new Color(v * v * v, v * v, v, 1F));
                        ctx.SwapBuffers();
                    }
                });
            });
        }
    }
}
