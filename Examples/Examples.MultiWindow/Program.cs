using System;

using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Examples.MultiWindow
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var colors = new Color[] { Color.Red, Color.Green, Color.Blue };

                for (var i = 0; i < colors.Length; i++)
                {
                    var example = new WindowExample(i, colors[i]);
                    example.Window.Position = (100 + i * 600, 100);
                    example.Start();
                }
            });
        }

        private class WindowExample : RenderLoop
        {
            public WindowExample(int index, Color color)
                : this(new Window($"Window {index}", new WindowCreationSettings { Size = (512, 240), VSync = false }))
            {
                Color = color;
            }

            private WindowExample(Window window)
                : base(window.RenderContext)
            {
                Window = window ?? throw new ArgumentNullException(nameof(window));
                RenderContext.ShowFPSOverlay = true;
            }

            public Window Window { get; }

            public Color Color { get; }

            protected override void Update(RenderContext ctx, float dt)
            {
                ctx.Clear(Color.Gray * Color);
                ctx.DrawText($"I am positioned at {Window.Position}\nI am also {Window.Size} pixels in size.", (16, 16), Font.Default, 32);
            }
        }
    }
}
