using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Runtime
{
    public class ImageRenderer : Renderer
    {
        public Image Image { get; set; }

        internal protected override void Update()
        {
            // 
        }

        internal protected override void Render(RenderContext ctx)
        {
            if (Image == null) { return; }
            else
            {
                // Draw image
                ctx.Draw(Image, Transform.Matrix, Color);
            }
        }
    }
}
