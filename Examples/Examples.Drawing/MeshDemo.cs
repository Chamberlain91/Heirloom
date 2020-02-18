using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Drawing
{
    public sealed class MeshDemo : Demo
    {
        public Polygon Star;
        public Mesh StarMesh;

        public Image Image;

        public static Color Shadow = new Color(0, 0, 0, 0.2F);

        public MeshDemo()
            : base("Mesh")
        {
            var noise = new SimplexNoise();
            Image = Image.CreateNoise(256, 256, noise, 450);

            // Create a classic 5 point star
            Star = Polygon.CreateStar(5, 250);

            // 
            StarMesh = Mesh.CreateFromPolygon(Star);
        }

        internal override void Draw(Graphics ctx, Rectangle contentBounds)
        {
            DrawStar(ctx, contentBounds.Center, 1F);
            DrawStar(ctx, contentBounds.Min + new Vector(100, 100), 0.2F);
            DrawStar(ctx, contentBounds.Min + new Vector(64, 180), 0.075F);
            DrawStar(ctx, contentBounds.Max - new Vector(110, 110), 0.3F);
        }

        private void DrawStar(Graphics ctx, Vector position, float scale)
        {
            var transform = Matrix.CreateTransform(position, Calc.Sin(position.X + Time * 3) * 0.2F, (scale, scale));

            // Draw mesh
            ctx.Color = Shadow;
            ctx.DrawMesh(Image, StarMesh, Matrix.CreateTranslation(10, 10) * transform);

            ctx.Color = Color.White;
            ctx.DrawMesh(Image, StarMesh, transform);

            // Draw polygon outline
            ctx.Color = (Color.Blue + Color.Cyan) / 2F;
            ctx.DrawPolygonOutline(Star, transform, 1);
        }
    }
}
