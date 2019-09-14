using System;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;

namespace Benchmark
{
    internal class Program : GameWindow
    {
        public BenchmarkApp App;

        public Program()
            : base("Heirloom Benchmark", vsync: false)
        {
            Maximize();

            // Display FPS
            ShowFPSOverlay = true;

            foreach (var ef in Files.GetEmbeddedFiles())
            {
                Console.WriteLine(ef.Path);
                foreach (var id in ef.Identifiers)
                {
                    Console.WriteLine("    " + id);
                }
            }

            // 
            App = new BenchmarkApp(60, 8, 20000);
        }

        protected override void Update(float dt)
        {
            App.Update(dt);
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            App.Render(ctx, dt);
        }

        private static void Main(string[] args)
        {
            Application.Run(() => new Program());
        }
    }
}
