using System;
using System.Runtime.InteropServices;
using Heirloom.GLFW3;
using Heirloom.OpenGLES;

namespace Examples.SimpleDrawing
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            // Initialize
            if (!Glfw.Init())
            {
                Console.WriteLine("Unable to initialize GLFW");
                Environment.Exit(-1);
            }

            // Set to use OpenGL 3.2 core (forward compatible)
            Glfw.SetWindowHint(WindowHint.ContextVersionMajor, 3);
            Glfw.SetWindowHint(WindowHint.ContextVersionMinor, 2);
            Glfw.SetWindowHint(WindowHint.OpenGLForwardCompatibility, 1);
            Glfw.SetWindowHint(WindowHint.OpenGLProfile, (int) OpenGLProfile.Core);

            // Create window
            var window = Glfw.CreateWindow(800, 600, "GLFW Example");

            // 
            Glfw.SetKeyCallback(window, (w, k, c, a, m) =>
            {
                Console.WriteLine($"Key '{k}' => {a}");
            });

            // 
            Glfw.SetDropCallback(window, (w, c, p) =>
            {
                Console.WriteLine($"Dropped: {c} files");

                for (var i = 0; i < c; i++)
                {
                    Console.WriteLine($"    {i}: {Glfw.ReadString(p, i)}");
                }
            });

            // Prepare OpenGL
            Glfw.MakeContextCurrent(window);
            GL.LoadFunctions(Glfw.GetProcAddress);

            // Main Loop
            while (!Glfw.GetWindowShouldClose(window))
            {
                Glfw.WaitEvents();

                // 
                Glfw.GetFramebufferSize(window, out var w, out var h);
                GL.SetViewport(0, 0, w, h);

                // 
                GL.SetClearColor(0xFF123456);
                GL.Clear(ClearMask.Color);

                // 
                Glfw.SwapBuffers(window);

                // Hit the GC hard
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);
            }

            // Terminate
            Glfw.DestroyWindow(window);
            Glfw.Terminate();
        }
    }
}
