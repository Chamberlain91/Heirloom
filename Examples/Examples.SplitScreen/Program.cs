using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.SplitScreen
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var window = new Window("Split Screen");
                window.Maximize();

                var loop = RenderLoop.Create(window.Graphics, OnUpdate);
                loop.Start();
            });
        }

        private static void OnUpdate(Graphics gfx, float dt)
        {
            // 
            gfx.Viewport = new Rectangle(0.0F, 0.0F, 0.5F, 1.0F);
            gfx.Clear(Color.DarkGray * Color.Red);
            DrawWorld(gfx, Color.Red);

            // 
            gfx.Viewport = new Rectangle(0.5F, 0.0F, 0.5F, 1.0F);
            gfx.Clear(Color.DarkGray * Color.Green);
            DrawWorld(gfx, Color.Green);
        }

        private static void DrawWorld(Graphics gfx, Color color)
        {
            gfx.Color = color;
            gfx.DrawText("Hi", (16, 16), Font.Default, 32);
        }
    }
}
