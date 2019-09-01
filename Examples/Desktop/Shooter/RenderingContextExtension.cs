using Heirloom.Drawing;
using Heirloom.Math;

namespace Shooter
{
    public static class RenderingContextExtension
    {
        /// <summary>
        /// Draws an image within a rectangle tiled, incomplete. Currently isn't clipped to the rectangle.
        /// </summary>
        public static void DrawImageTiled(this RenderContext ctx, in Image image, in Rectangle rectangle)
        {
            DrawImageTiled(ctx, image, rectangle, (0, 0));
        }

        /// <summary>
        /// Draws an image within a rectangle tiled, incomplete. Currently isn't clipped to the rectangle.
        /// </summary>
        public static void DrawImageTiled(this RenderContext ctx, in Image image, in Rectangle rectangle, in Vector offset)
        {
            var x = offset.X - rectangle.X;
            var y = offset.Y - rectangle.Y;

            // 
            var offsetX = x % image.Width;
            var offsetY = y % image.Height;

            // 
            for (var _x = offsetX - image.Width; _x < rectangle.Width + image.Width; _x += image.Width)
            {
                for (var _y = offsetY - image.Height; _y < rectangle.Height + image.Height; _y += image.Height)
                {
                    ctx.Draw(image, Matrix.CreateTranslation(_x + rectangle.X, _y + rectangle.Y));
                }
            }
        }
    }
}
