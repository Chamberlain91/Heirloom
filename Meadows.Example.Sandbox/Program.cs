using System;

using Meadows.Drawing;
using Meadows.Engine;
using Meadows.Mathematics;
using Meadows.Runtime.Desktop;

namespace Meadows.Example.Sandbox
{
    internal sealed class Program : GameLoop
    {
        public Program()
            : base(new Window("", (1280, 720)))
        { }

        protected override void Update(Surface surface)
        {
            // 
            surface.Clear(Color.DarkGray);
            surface.DrawText("Hello World", (Vector) surface.Size / 2F, TextAlign.Center | TextAlign.Middle, Font.Default, 16);
        }

        private static void Main(string[] args)
        {
            MeadowsApp.Run<Program>();
        }
    }
}
