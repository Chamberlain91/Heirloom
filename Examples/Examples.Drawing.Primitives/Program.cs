using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing.Primitives
{
    internal class Program : GameWindow
    {
        private float _angle = 0;

        public Program()
            : base(512, 512, "Drawing Primitives")
        { }

        protected override void Update(float dt)
        {
            // 
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            ctx.Clear(Color.DarkGray);

            ctx.Transform = Matrix.CreateScale(ctx.Surface.Width, ctx.Surface.Height);

            _angle += dt;

            var ce = new Vector(0.5F, 0.5F);
            var ve = Vector.FromAngle(_angle + 1.57F) * 0.2F;

            var p0 = ce + Vector.FromAngle(_angle) * 0.5F;
            var p1 = ce + Vector.FromAngle(_angle) * 0.2F + ve;
            var p2 = ce - Vector.FromAngle(_angle) * 0.2F - ve;
            var p3 = ce - Vector.FromAngle(_angle) * 0.5F;

            // 
            ctx.Color = Color.Yellow;
            ctx.DrawCurve(p0, p1, p2, p3, 2);
        }

        private static void Main(string[] args)
        {
            Application.Run(() => new Program());
        }
    }
}
