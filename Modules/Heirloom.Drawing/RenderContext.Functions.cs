using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract partial class RenderContext
    {
        // used to center the line within the 1x1 pixel image to anchor at left-center
        private static readonly Matrix _lineOffsetMatrix = Matrix.CreateTranslation(0, -1 / 2F);
        private static readonly Mesh _temporaryMesh = new Mesh();

        #region Draw Image

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="image">Some image.</param>
        /// <param name="position">The position of the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, Vector position)
        {
            DrawImage(image, Matrix.CreateTranslation(position));
        }

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="image">Some image.</param>
        /// <param name="position">The position of the image.</param>
        /// <param name="rotation">The rotation applied to the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, Vector position, float rotation)
        {
            DrawImage(image, Matrix.CreateTransform(position, rotation, Vector.One));
        }

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="image">Some image.</param>
        /// <param name="position">The position of the image.</param>
        /// <param name="rotation">The rotation applied to the image.</param>
        /// <param name="scale">The scale applied to the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, Vector position, float rotation, Vector scale)
        {
            DrawImage(image, Matrix.CreateTransform(position, rotation, scale));
        }

        /// <summary>
        /// Draws an image stretched to fill a rectangular region to the current surface.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="image">Some image.</param>
        /// <param name="rectangle">The bounds of the drawn image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, Rectangle rectangle)
        {
            var scale = rectangle.Size / image.Size;
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) scale);
            DrawImage(image, transform);
        }

        #endregion

        #region Draw Sprite

        /// <summary>
        /// Draw a sprite to the current surface.
        /// </summary>
        /// <param name="ctx">Some render context.</param>
        /// <param name="sprite">Some sprite.</param>
        /// <param name="index">Some valid frame number within the sprite.</param>
        /// <param name="transform">Some transform to draw the sprite.</param>
        public void DrawSprite(Sprite sprite, int index, Matrix transform)
        {
            var frame = sprite.Frames[index];
            DrawImage(frame.Image, transform);
        }

        #endregion

        #region Draw Nine Slice

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawNineSlice(NineSlice nine, Rectangle rectangle)
        {
            // Compute stretch factors
            var w = (rectangle.Width - nine.TopLeftImage.Width - nine.TopRightImage.Width) / nine.MiddleImage.Width;
            var h = (rectangle.Height - nine.TopLeftImage.Height - nine.BottomLeftImage.Height) / nine.MiddleImage.Height;

            var x0 = rectangle.Left + 0;
            var x1 = x0 + nine.TopLeftImage.Width;
            var x2 = x1 + nine.MiddleImage.Width * w;

            var y0 = rectangle.Top + 0;
            var y1 = y0 + nine.TopLeftImage.Height;
            var y2 = y1 + nine.MiddleImage.Height * h;

            // Corners
            DrawImage(nine.TopLeftImage, Matrix.CreateTranslation(x0, y0));
            DrawImage(nine.TopRightImage, Matrix.CreateTranslation(x2, y0));
            DrawImage(nine.BottomLeftImage, Matrix.CreateTranslation(x0, y2));
            DrawImage(nine.BottomRightImage, Matrix.CreateTranslation(x2, y2));

            if (w > 0)
            {
                // Horizontal
                DrawImage(nine.TopMiddleImage, Matrix.CreateTransform(x1, y0, 0, w, 1));
                DrawImage(nine.BottomMiddleImage, Matrix.CreateTransform(x1, y2, 0, w, 1));
            }

            if (h > 0)
            {
                // Vertical
                DrawImage(nine.MiddleLeftImage, Matrix.CreateTransform(x0, y1, 0, 1, h));
                DrawImage(nine.MiddleRightImage, Matrix.CreateTransform(x2, y1, 0, 1, h));
            }

            if (w > 0 && h > 0)
            {
                // Middle
                DrawImage(nine.MiddleImage, Matrix.CreateTransform(x1, y1, 0, w, h));
            }
        }

        #endregion

        /// <summary>
        /// Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.
        /// </summary>
        /// <param name="ctx">The drawing context.</param>
        /// <param name="center">The position of the cross.</param>
        /// <param name="size">Size in screen pixels (not world space).</param>
        /// <param name="width">Width of the lines screen pixels (not world space).</param>
        public void DrawCross(Vector center, float size = 2, float width = 1F)
        {
            // Scale input size by pixel scaling
            size *= ApproximatePixelScale;
            
            // Draw axis
            DrawLine(center + (Vector.Left * size), center + (Vector.Right * size), width);
            DrawLine(center + (Vector.Up * size), center + (Vector.Down * size), width);
        }
    }
}
