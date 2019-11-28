using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Examples.Drawing
{
    internal class Program
    {
        private int _demoIndex = 0;
        private readonly Demo[] _demos;

        public Demo CurrentDemo => _demos[_demoIndex];

        public RenderLoop Loop;
        public Window Window;

        public Program()
        {
            // 
            _demos = new Demo[]
            {
                new ImageDemo(),
                new TextDemo(),
                new TextCallbackDemo(),
                new RichTextDemo(),
                new LineThicknessDemo(),
                new QuadraticCurveDemo(),
                new CubicCurveDemo(),
                new PrimitivesDemo(),
                new PolygonDemo(),
                new MeshDemo(),
                new SurfaceDemo()
            };

            // Create window
            Window = new Window("Heirloom - Drawing Examples", new WindowCreationSettings
            {
                Multisample = MultisampleQuality.High,
                IsResizable = false
            });

            // 
            Window.Graphics.EnableFPSOverlay = true;

            // Create render loop
            Loop = RenderLoop.Create(Window.Graphics, Update);
            Loop.Start();

            // Register key events
            Window.KeyPress += Window_KeyPress;
        }

        private void Update(Graphics gfx, float dt)
        {
            gfx.Clear(Color.DarkGray);

            CurrentDemo.Update(dt);

            // 
            var contentBounds = new Rectangle(256, 32, gfx.Surface.Width - 288, gfx.Surface.Height - 160);
            gfx.Color = Color.Lerp(Color.DarkGray, Color.Black, 0.1F);
            gfx.DrawRect(contentBounds);

            gfx.ResetState();
            CurrentDemo.Draw(gfx, Rectangle.Inflate(contentBounds, -16));

            //
            gfx.ResetState();

            var bottomText = new Vector(gfx.Surface.Width / 2F, gfx.Surface.Height - 96);
            gfx.DrawText($"Use arrow keys to change demo\nDemo {_demoIndex + 1} of {_demos.Length}", bottomText, Font.Default, 32, TextAlign.Center);

            for (var i = 0; i < _demos.Length; i++)
            {
                var menuPosition = new Vector(32, 32 + i * 32);

                if (i == _demoIndex) { gfx.Color = Color.Pink; }
                else { gfx.Color = Color.White; }

                gfx.DrawText(_demos[i].Name, menuPosition, Font.Default, 32, TextAlign.Left);
            }
        }

        private void Window_KeyPress(Window win, KeyboardEvent ev)
        {
            if (ev.Action == ButtonAction.Press)
            {
                if (ev.Key == Key.Up)
                {
                    _demoIndex--;

                    if (_demoIndex < 0)
                    {
                        _demoIndex += _demos.Length;
                    }
                }

                if (ev.Key == Key.Down)
                {
                    _demoIndex++;

                    if (_demoIndex >= _demos.Length)
                    {
                        _demoIndex -= _demos.Length;
                    }
                }
            }
        }

        private static void Main(string[] args)
        {
            Application.Run(() => new Program());
        }
    }
}
