using System;
using System.Threading;

using Heirloom.GLFW3;

using Monitor = Heirloom.GLFW3.Monitor;

namespace Examples.SimpleDrawing
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            Application.Run(() =>
            {
                var window = new Window(800, 600, "Simple Drawing");
                window.Show();
            });

            var monitor = Monitor.Default;
            Console.WriteLine($"{monitor.Name}");

            // 
            var mode = monitor.GetCurrentVideoMode();
            Console.WriteLine($"\t{mode}");

            // 
            var modes = monitor.GetVideoModes();
            foreach (var _mode in modes)
            {
                Console.WriteLine($"\t{_mode}");
            }

            // Wait
            Console.ReadKey();
        }
    }
}
