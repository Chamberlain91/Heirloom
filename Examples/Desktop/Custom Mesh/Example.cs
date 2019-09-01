using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.CustomMesh
{
    internal class Example : GameWindow
    {
        public Mesh Mesh;

        public Example()
            : base("Mesh Example")
        {
            Mesh = new Mesh();

            const int N = 5;

            // 
            for (var i = 0; i < N; i++)
            {
                var a = i * Calc.TwoPi / N;
                var c = Calc.Cos(a) * 0.5F;
                var s = Calc.Sin(a) * 0.5F;
                var u = c + 0.5F;
                var v = s + 0.5F;

                // 
                Mesh.Vertices.Add(new Vertex
                {
                    Position = new Vector(c * 512, s * 512) * 0.8F,
                    UV = (u, v)
                });
            }

            // 
            for (var i = 0; i < N - 1; i++)
            {
                Mesh.Indices.Add(0);
                Mesh.Indices.Add((ushort) ((i + 1) % N));
                Mesh.Indices.Add((ushort) ((i + 2) % N));
            }

            // Prevent resize
            IsResizeable = false;
        }

        protected override void Update()
        {
            // Do Nothing
        }

        protected override void Render(RenderContext context)
        {
            context.Clear(Color.DarkGray);

            for (var i = 0; i < Color.Rainbow.Length; i++)
            {
                var color = Color.Rainbow[i];
                var a = Calc.Sin(Calc.Cos(Time + i) * (i + 1));
                var s = 2F / (i + 1);

                var transform = Matrix.CreateTransform((context.Surface.Width / 2F, context.Surface.Height / 2F), a, (s, s));
                context.Draw(Image.White, Mesh, transform, color);
            }
        }

        private static void Main(string[] args)
        {
            Run(new Example());
        }
    }
}
