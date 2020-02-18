using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract partial class Graphics
    {
        // used to center the line within the 1x1 pixel image to anchor at left-center
        private static readonly Matrix _lineOffsetMatrix = Matrix.CreateTranslation(0, -1 / 2F);
        private static readonly Mesh _temporaryMesh = new Mesh();

        #region Draw Image

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary> 
        /// <param name="image">Some image.</param>
        /// <param name="position">The position of the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, in Vector position)
        {
            DrawImage(image, Matrix.CreateTranslation(in position));
        }

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary> 
        /// <param name="image">Some image.</param>
        /// <param name="position">The position of the image.</param>
        /// <param name="rotation">The rotation applied to the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, in Vector position, float rotation)
        {
            DrawImage(image, Matrix.CreateTransform(in position, rotation, Vector.One));
        }

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary> 
        /// <param name="image">Some image.</param>
        /// <param name="position">The position of the image.</param>
        /// <param name="rotation">The rotation applied to the image.</param>
        /// <param name="scale">The scale applied to the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, in Vector position, float rotation, in Vector scale)
        {
            DrawImage(image, Matrix.CreateTransform(in position, rotation, in scale));
        }

        /// <summary>
        /// Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.
        /// </summary> 
        /// <param name="image">Some image.</param>
        /// <param name="rectangle">The bounds of the drawn image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, in Rectangle rectangle)
        {
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) rectangle.Size);
            DrawMesh(image, _quadMesh, in transform);
        }

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="transform">Some transform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, in Matrix transform)
        {
            var mat = transform;

            if (image.Origin != Vector.Zero)
            {
                // todo: optimize? M2 and M5?
                mat = transform * Matrix.CreateTranslation(-image.Origin);
            }

            // Scale to image dimensions
            mat.M0 *= image.Size.Width;
            mat.M3 *= image.Size.Width;
            mat.M1 *= image.Size.Height;
            mat.M4 *= image.Size.Height;

            DrawMesh(image, _quadMesh, in mat);
        }

        #endregion

        #region Draw Sprite

        /// <summary>
        /// Draw a sprite to the current surface.
        /// </summary> 
        /// <param name="sprite">Some sprite.</param>
        /// <param name="index">Some valid frame number within the sprite.</param>
        /// <param name="transform">Some transform to draw the sprite.</param>
        public void DrawSprite(Sprite sprite, int index, in Matrix transform)
        {
            var frame = sprite.Frames[index];
            DrawImage(frame.Image, in transform);
        }

        #endregion 
    }
}
