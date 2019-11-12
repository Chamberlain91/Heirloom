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
            Window.RenderContext.ShowFPSOverlay = true;

            // Create render loop
            Loop = RenderLoop.Create(Window.RenderContext, Update);
            Loop.Start();

            // Register key events
            Window.KeyPress += Window_KeyPress;
        }

        private void Update(RenderContext ctx, float dt)
        {
            ctx.Clear(Color.DarkGray);

            CurrentDemo.Update(dt);

            // 
            var contentBounds = new Rectangle(256, 32, ctx.Surface.Width - 288, ctx.Surface.Height - 160);
            ctx.Color = Color.Lerp(Color.DarkGray, Color.Black, 0.1F);
            ctx.DrawRect(contentBounds);

            ctx.ResetState();
            CurrentDemo.Draw(ctx, Rectangle.Inflate(contentBounds, -16));

            //
            ctx.ResetState();

            var bottomText = new Vector(ctx.Surface.Width / 2F, ctx.Surface.Height - 96);
            ctx.DrawText($"Use arrow keys to change demo\nDemo {_demoIndex + 1} of {_demos.Length}", bottomText, Font.Default, 32, TextAlign.Center);

            for (var i = 0; i < _demos.Length; i++)
            {
                var menuPosition = new Vector(32, 32 + i * 32);

                if (i == _demoIndex) { ctx.Color = Color.Pink; }
                else { ctx.Color = Color.White; }

                ctx.DrawText(_demos[i].Name, menuPosition, Font.Default, 32, TextAlign.Left);
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
