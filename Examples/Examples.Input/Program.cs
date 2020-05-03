using Heirloom;
using Heirloom.Desktop;

namespace Examples.Input
{
    using Input = Heirloom.Input;

    internal class Program
    {
        public static bool IsDragImage;
        public static Vector ImagePosition;
        public static Image Image;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var window = new Window("Game Input Example", vsync: false) { IsResizable = false, Size = (800, 600) };

                // 
                Image = Image.CreateGridPattern(98, 98, Color.Violet, 16, 2);

                // Begin Render Loop
                var loop = GameLoop.Create(window.Graphics, OnDraw);
                loop.Start();
            });
        }

        private static void OnDraw(Graphics gfx, float dt)
        {
            // Are we holding the left mouse button down?
            if (Input.CheckMouse(MouseButton.Left, ButtonState.Down))
            {
                // Is mouse over image bounds?
                var bounds = new Rectangle(ImagePosition, Image.Size);
                if (bounds.Contains(Input.MousePosition))
                {
                    IsDragImage = true;
                }
            }
            else
            {
                IsDragImage = false;
            }

            // Has the space key just been pressed?
            if (Input.CheckButton(Key.Space, ButtonState.Pressed))
            {
                var surface = gfx.Screen.Surface;
                ImagePosition = (Vector) (surface.Size - Image.Size) / 2F;
                IsDragImage = false; // cause the drag state to stop
            }

            if (IsDragImage)
            {
                ImagePosition += Input.MouseDelta;
            }

            gfx.Clear(Color.Black);
            gfx.DrawImage(Image, ImagePosition);

            gfx.DrawText("Press <space> to center image. Click and drag to move image.", (10, 10), Font.Default, 16);
        }
    }
}
