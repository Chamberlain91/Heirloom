using System;

using Heirloom.GLFW3;

namespace Examples.SimpleDrawing
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            if (!Glfw.Initialize())
            {
                Console.WriteLine("Unable to initialize GL FW");
                Environment.Exit(-1);
            }

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
