﻿using System;

using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Examples.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Create Window
                var window = new Window("Window Information and Events");
                window.KeyRelease += Window_Key;
                window.KeyPress += Window_Key;

                // Loads the inverted color shader
                var shader = new Shader("files/invert.frag");

                // 
                var loop = RenderLoop.Create(window.Graphics, Update);
                loop.Start();
            });
        }

        private static void Window_Key(Window win, KeyEvent ev)
        {
            if (ev.Action == ButtonAction.Press)
            {
                var key = ev.Key.ToString().ToSmartDisplayName().Replace(' ', '_').ToLower();

                var nameA = Keyboard.GetPrintableKeyName(ev.Key);
                var nameB = Keyboard.GetPrintableKeyName(ev.ScanCode);
                Console.WriteLine($"{key}: '{nameA}' . '{nameB}'");
            }
            else
            {

            }
        }

        private static void Update(IGraphics gfx, float dt)
        {
            gfx.Clear(Color.Magenta);
        }
    }
}
