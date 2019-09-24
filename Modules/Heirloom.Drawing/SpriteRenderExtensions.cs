using Heirloom.Math;

namespace Heirloom.Drawing
{
    public static class SpriteRenderExtensions
    {
        /// <summary>
        /// Draw a sprite to the current surface.
        /// </summary>
        /// <param name="ctx">Some render context.</param>
        /// <param name="sprite">Some sprite.</param>
        /// <param name="index">Some valid frame number within the sprite.</param>
        /// <param name="transform">Some transform to draw the sprite.</param>
        public static void DrawSprite(this RenderContext ctx, Sprite sprite, int index, Matrix transform)
        {
            var frame = sprite.Frames[index];
            transform = transform * Matrix.CreateTranslation(-frame.Origin);
            ctx.DrawImage(frame.Image, transform);
        }
    }
}
