using System;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Extras;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Examples.Drawing
{
    internal class Program : GameWindow
    {
        private int _demoIndex = 0;
        private readonly Demo[] _demos;

        public Demo CurrentDemo => _demos[_demoIndex];

        public Program()
            : base("Heirloom - Drawing Examples", multisample: MultisampleQuality.High)
        {
            ShowFPSOverlay = true;
            IsResizable = false;

            // 
            _demos = new Demo[]
            {
                new ImageDemo(),
                new TextDemo(),
                new TextCallbackDemo(),
                new LineThicknessDemo(),
                new QuadraticCurveDemo(),
                new CubicCurveDemo(),
                new PrimitivesDemo(),
                new PolygonDemo(),
                new MeshDemo(),
                new SurfaceDemo()
            };
        }

        protected override void Update(float dt)
        {
            CurrentDemo.Update(dt);
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            ctx.Clear(Color.DarkGray);

            // 
            var contentBounds = new Rectangle(256, 32, ctx.Surface.Width - 288, ctx.Surface.Height - 160);
            ctx.Color = Color.Lerp(Color.DarkGray, Color.Black, 0.1F);
            ctx.DrawRect(contentBounds);

            ctx.ResetState();
            CurrentDemo.Draw(ctx, contentBounds.Inflate(-16));

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

        protected override void OnKeyPressed(Key key, int scancode, ButtonAction action, KeyModifiers modifiers)
        {
            if (action == ButtonAction.Press)
            {
                if (key == Key.Up)
                {
                    _demoIndex--;

                    if (_demoIndex < 0)
                    {
                        _demoIndex += _demos.Length;
                    }
                }

                if (key == Key.Down)
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
