using Heirloom.Drawing;

namespace Heirloom.Game
{
    /// <summary>
    /// Provides rendering an image to the attached entity.
    /// </summary>
    public sealed class ImageComponent : DrawableComponent
    {
        private Image _image;

        public ImageComponent(Image image)
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

        protected override void Draw(RenderContext ctx)
        {
            ctx.DrawImage(Image, Transform.Matrix);
        }
    }
}
