using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Examples.CustomText
{
    internal class Program : GameWindow
    {
        private readonly CustomTextExample _app;

        public Program()
            : base("Custom Text Callback")
        {
            ShowFPSOverlay = true;
            _app = new CustomTextExample();
        }

        private static void Main(string[] args)
        {
            Application.Run(() => new Program());
        }

        protected override void Update(float dt)
        {
            _app.Update(dt);
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            _app.Draw(ctx, dt);
        }
    }
}
