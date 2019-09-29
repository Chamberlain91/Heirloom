using Heirloom.Drawing;

namespace Heirloom.Game
{
    public sealed class ImageRenderer : Renderer
    {
        private Image _image;

        public ImageRenderer()
        {
            // 
        }

        public ImageRenderer(Image image)
        {
            Image = image;
        }

        public Image Image
        {
            get => _image;
            set => _image = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        protected internal override void Update(float dt)
        {
            // Nothing to do
        }

        protected internal override void Draw(RenderContext ctx)
        {
            ctx.Blending = Blending;
            ctx.Color = Color;

            ctx.DrawImage(Image, Transform.Matrix);
        }
    }
}
