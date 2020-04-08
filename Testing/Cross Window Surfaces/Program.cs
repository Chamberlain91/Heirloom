using System;
using System.Threading;
using System.Threading.Tasks;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Cross_Window_Surfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.IsTerminating)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine($"{e.ExceptionObject}");
                Console.ResetColor();
            };

            Application.Run(() =>
            {
                var winA = new Window("Cross Window Surfaces") { Size = (256, 256) };
                winA.MoveToCenter();

                var winB = new Window("Cross Window Surfaces") { Size = (256, 256) };
                winB.MoveToCenter();

                // Offset windows
                winA.Position -= (128 + 10, 0);
                winB.Position += (128 + 10, 0);

                // 
                var surface = new Surface(256, 256);
                var counter = 0;

                Task.Run(() =>
                {
                    while (true)
                    {
                        counter++;

                        // Draw to surface using window A context
                        winA.Graphics.ResetState();
                        winA.Graphics.Surface = surface;
                        winA.Graphics.Clear(Color.DarkGray);
                        winA.Graphics.Color = Color.Red;
                        winA.Graphics.DrawText($"Count: {counter}", (16, 16), Font.Default, 64);
                        winA.Graphics.Flush(); // commits drawing to surface

                        // Draw surface to window b
                        winB.Graphics.ResetState();
                        winB.Graphics.DrawImage(surface, Vector.Zero);
                        winB.Graphics.RefreshScreen();

                        // Draw surface to window A
                        winA.Graphics.ResetState();
                        winA.Graphics.DrawImage(surface, Vector.Zero);
                        winA.Graphics.RefreshScreen();

                        Thread.Sleep(500);
                    }
                });
            });
        }
    }
}
