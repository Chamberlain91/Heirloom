using System;
using System.Runtime.CompilerServices;

namespace Heirloom
{
    public abstract partial class GraphicsContext
    {
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

        #region Draw SubImage

        /// <summary>
        /// Draws a sub-region of an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="region">Some subregion of the image.</param>
        /// <param name="rectangle">The bounds of the drawn image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawSubImage(ImageSource image, in IntRectangle region, in Rectangle rectangle)
        {
            var transform = Matrix.CreateTransform(rectangle.Position, 0, (Vector) rectangle.Size / (Vector) region.Size);
            DrawSubImage(image, in region, in transform);
        }

        /// <summary>
        /// Draws a sub-region of an image to the current surface ignoring the image origin offset.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="region">Some subregion of the image.</param>
        /// <param name="position">The position of the image.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawSubImage(ImageSource image, in IntRectangle region, in Vector position)
        {
            var transform = Matrix.CreateTranslation(position);
            DrawSubImage(image, in region, in transform);
        }

        /// <summary>
        /// Draws a sub-region of an image to the current surface ignoring the image origin offset.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="region">Some subregion of the image.</param>
        /// <param name="transform">Some transform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawSubImage(ImageSource image, in IntRectangle region, in Matrix transform)
        {
            // Subregion exceeds bounds
            if (region.X < 0 || region.Y < 0 || region.Right > image.Size.Width || region.Bottom > image.Size.Height)
            {
                throw new ArgumentException("Subregion exceeds image bounds.");
            }

            var a = new Vector(0, 0);
            var b = new Vector(region.Width, 0);
            var c = new Vector(region.Width, region.Height);
            var d = new Vector(0, region.Height);

            var size = (Vector) image.Size;
            var ua = region.TopLeft / size;
            var ub = region.TopRight / size;
            var uc = region.BottomRight / size;
            var ud = region.BottomLeft / size;

            _temporaryMesh.Clear();

            // Append vertices
            _temporaryMesh.AddVertex(new Vertex(a, ua));
            _temporaryMesh.AddVertex(new Vertex(b, ub));
            _temporaryMesh.AddVertex(new Vertex(c, uc));
            _temporaryMesh.AddVertex(new Vertex(d, ud));

            // Append indices
            _temporaryMesh.AddTriangle(0, 1, 2);
            _temporaryMesh.AddTriangle(0, 2, 3);

            DrawMesh(image, _temporaryMesh, in transform);
        }

        #endregion

        #region Draw Nine Slice

        /// <summary>
        /// Draws a nine-slice image onto the current surface.
        /// </summary>
        /// <param name="slice">The nine-slice image.</param>
        /// <param name="rectangle">The target rectangle the nine-slice is fitted to.</param>
        public void DrawNineSlice(NineSlice slice, Rectangle rectangle)
        {
            var TH = slice.Center.Top;
            var BH = slice.Image.Height - slice.Center.Bottom;
            var LW = slice.Center.Left;
            var RW = slice.Image.Width - slice.Center.Right;

            // Corners
            var TL = new IntRectangle(0, 0, LW, TH);
            var TR = new IntRectangle(slice.Center.Right, 0, RW, TH);
            var BR = new IntRectangle(slice.Center.Right, slice.Center.Bottom, RW, BH);
            var BL = new IntRectangle(0, slice.Center.Bottom, LW, BH);

            var x2 = rectangle.Width - RW;
            var y2 = rectangle.Height - BH;

            DrawSubImage(slice.Image, TL, rectangle.Position);
            DrawSubImage(slice.Image, TR, rectangle.Position + new Vector(x2, 0));
            DrawSubImage(slice.Image, BR, rectangle.Position + new Vector(x2, y2));
            DrawSubImage(slice.Image, BL, rectangle.Position + new Vector(0, y2));

            // Edges
            var TM = new IntRectangle(slice.Center.Left, 0, slice.Center.Width, TH);
            var BM = new IntRectangle(slice.Center.Left, slice.Center.Bottom, slice.Center.Width, BH);
            var LM = new IntRectangle(0, slice.Center.Top, LW, slice.Center.Height);
            var RM = new IntRectangle(slice.Center.Right, slice.Center.Top, RW, slice.Center.Height);

            var cw = x2 - LW;
            var ch = y2 - TH;

            DrawSubImage(slice.Image, TM, new Rectangle(rectangle.Position + new Vector(LW, 0), new Size(cw, TH)));
            DrawSubImage(slice.Image, BM, new Rectangle(rectangle.Position + new Vector(LW, y2), new Size(cw, BH)));
            DrawSubImage(slice.Image, LM, new Rectangle(rectangle.Position + new Vector(0, TH), new Size(LW, ch)));
            DrawSubImage(slice.Image, RM, new Rectangle(rectangle.Position + new Vector(x2, TH), new Size(RW, ch)));

            // Center
            DrawSubImage(slice.Image, slice.Center, new Rectangle(rectangle.Position + new Vector(LW, TH), new Size(cw, ch)));
        }

        #endregion

        /// <summary>
        /// Applies the specified surface effect to the current surface.
        /// </summary>
        public void Apply(SurfaceEffect effect)
        {
            if (Surface.IsScreenBound)
            {
                throw new ArgumentException("Unable to apply surface effect to a screen bound surface.");
            }

            var surface = Surface;

            PushState(true);
            effect.Apply(this, surface);
            PopState();
        }

        /// <summary>
        /// Overwrites an image to target surface.
        /// </summary>
        public void Blit(ImageSource source, Surface target)
        {
            PushState(true);
            {
                Surface = target;
                Blending = Blending.Opaque;
                DrawImage(source, (Vector.Zero, target.Size));
            }
            PopState();
        }
    }
}
