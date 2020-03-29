using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.UserInput
{
    internal class Program
    {
        public static bool IsDragImage;
        public static Vector ImagePosition;
        public static Image Image;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var window = new Window("User Input", vsync: false) { IsResizable = false, Size = (800, 600) };
                window.MouseRelease += OnMouseClick;
                window.MousePress += OnMouseClick;
                window.MouseMove += OnMouseMove;
                window.KeyPress += OnKeyPress;

                // 
                Image = Image.CreateGridPattern(98, 98, Color.Violet, 16, 2);

                // Begin Render Loop
                var loop = RenderLoop.Create(window.Graphics, OnDraw);
                loop.Start();
            });
        }

        private static void OnDraw(Graphics gfx, float dt)
        {
            gfx.Clear(Color.Black);
            gfx.DrawImage(Image, ImagePosition);

            gfx.DrawText("Press <space> to center image. Click and drag to move image.", (10, 10), Font.Default, 16);
        }

        private static void OnMouseMove(Window w, MouseMoveEvent e)
        {
            if (IsDragImage)
            {
                ImagePosition += e.Delta;
            }
        }

        private static void OnMouseClick(Window w, MouseButtonEvent e)
        {
            if (e.Action == ButtonAction.Press)
            {
                // Is mouse over image bounds?
                var bounds = new Rectangle(ImagePosition, Image.Size);
                if (bounds.Contains(e.Position))
                {
                    IsDragImage = true;
                }
            }
            else
            {
                IsDragImage = false;
            }
        }

        private static void OnKeyPress(Window w, KeyEvent e)
        {
            if (e.Key == Key.Space)
            {
                ImagePosition = w.Bounds.Center - w.Position - ((Vector) Image.Size / 2F);
                IsDragImage = false; // cause the drag state to stop
            }
        }
    }
}
