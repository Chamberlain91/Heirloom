using System;
using System.IO;

using Heirloom.Desktop;
using Heirloom.Mathematics;

namespace Heirloom.Benchmark.Desktop
{
    internal sealed class Program
    {
        private const string RecordsFilePath = "./last_run.txt";

        public Window Window { get; }

        public BenchmarkApp Game;

        public Program()
        {
            // Create window and go fullscreen
            Window = new Window("Heirloom Benchmark", (1280, 720), vsync: false) { IsResizable = false };
            Window.Position = (IntVector) (Display.Primary.Size - Window.Size) / 2; // Center on display

            // Create benchmark app
            Game = new BenchmarkApp(Window.Graphics)
            {
                GetRecordText = GetRecordText,
                SetRecordText = SetRecordText
            };

            // Bind loop close and begin loop
            Window.Closed += w => Game.Loop.Stop();
            Game.Loop.Start();
        }

        private static void Main(string[] args)
        {
            Application.Run<Program>();
        }

        private void SetRecordText(string message)
        {
            File.WriteAllText(RecordsFilePath, message);
        }

        private string GetRecordText()
        {
            if (File.Exists(RecordsFilePath))
            {
                return File.ReadAllText(RecordsFilePath);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
