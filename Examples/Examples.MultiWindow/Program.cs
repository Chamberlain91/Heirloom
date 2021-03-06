using System;

using Heirloom;
using Heirloom.Desktop;

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

        private class WindowExample : GameLoop
        {
            public WindowExample(int index, Color color)
                : this(new Window($"Window {index}", vsync: false))
            {
                Window.Size = (512, 240);
                Color = color;
            }

            private WindowExample(Window window)
                : base(window.Graphics)
            {
                Window = window ?? throw new ArgumentNullException(nameof(window));
                Graphics.Performance.OverlayMode = PerformanceOverlayMode.Simple;
            }

            public Window Window { get; }

            public Color Color { get; }

            protected override void Update(GraphicsContext ctx, float dt)
            {
                ctx.Clear(Color.Gray * Color);
                ctx.DrawText($"I am positioned at {Window.Position}\nI am also {Window.Size} pixels in size.", (16, 16), Font.Default, 32);
            }
        }
    }
}
