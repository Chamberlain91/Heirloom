using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class RenderContext : IDisposable
    {
        // Quad mesh
        private static readonly Mesh _quadMesh = Mesh.CreateQuad();

        #region Create Surface

        public abstract Surface CreateSurface(int width, int height);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Surface CreateSurface(IntSize size)
        {
            return CreateSurface(size.Width, size.Height);
        }

        #endregion

        /// <summary>
        /// Gets the default surface (ie, window) of this render context.
        /// </summary>
        public abstract Surface DefaultSurface { get; }

        /// <summary>
        /// Gets or sets the current surface.
        /// </summary>
        public abstract Surface Surface { get; set; }

        /// <summary>
        /// Gets or sets the viewport in normalized coordinates.
        /// </summary>
        public abstract Rectangle Viewport { get; set; }

        /// <summary>
        /// Get or sets the global transform.
        /// </summary>
        public abstract Matrix Transform { get; set; }

        /// <summary>
        /// Gets the inverse of the current global transform.
        /// </summary>
        public abstract Matrix InverseTransform { get; }

        /// <summary>
        /// Gets the approximate scaling factor that one pixel consumes in world units.
        /// </summary>
        public abstract float ApproximatePixelScale { get; }

        /// <summary>
        /// Gets or sets the current blending color.
        /// </summary>
        public abstract Color Color { get; set; }

        /// <summary>
        /// Gets or sets the current blending mode.
        /// </summary>
        public abstract Blending Blending { get; set; }

        /// <summary>
        /// Reset context state to default (default surface, full viewport, no transform, alpha and white).
        /// </summary>
        public void ResetState()
        {
            Surface = DefaultSurface; // also adjusts viewport?
            Viewport = (0, 0, 1, 1);

            Transform = Matrix.Identity;
            Blending = Blending.Alpha;
            Color = Color.White;
        }

        /// <summary>
        /// Clears the current surface with the specified color.
        /// </summary>
        public abstract void Clear(Color color);

        /// <summary>
        /// Draw a mesh with the given image to the current surface.
        /// </summary>
        /// <param name="mesh">Some mesh.</param>
        /// <param name="image">Some image.</param>
        /// <param name="transform">Some transform.</param>
        public abstract void Draw(ImageSource image, Mesh mesh, Matrix transform);

        /// <summary>
        /// Draw an image to the current surface.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="transform">Some transform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(ImageSource image, Matrix transform)
        {
            // Scale to image dimensions
            transform.M0 *= image.Size.Width;
            transform.M3 *= image.Size.Width;
            transform.M1 *= image.Size.Height;
            transform.M4 *= image.Size.Height;

            Draw(image, _quadMesh, transform);
        }

        /// <summary>
        /// Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)
        /// </summary>
        /// <param name="region">A region within the currently set surface.</param>
        /// <returns>An image with a copy of the pixels on the surface.</returns>
        public abstract Image GrabPixels(IntRectangle region);

        /// <summary>
        /// Grab the pixels from the current surface and return that image. (ie, a screenshot)
        /// </summary>
        /// <returns>An image with a copy of the pixels on the surface.</returns>
        public Image GrabPixels()
        {
            return GrabPixels((0, 0, Surface.Width, Surface.Height));
        }

        /// <summary>
        /// Present the drawing operations to the screen.
        /// </summary>
        public abstract void SwapBuffers();

        /// <summary>
        /// Force pending drawing operations to complete, useful for synchronization between contexts. <para/>
        /// Note: Currently untested for said synchronization.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Dispose this render context, freeing any resources occupied by it.
        /// </summary>
        public abstract void Dispose();
    }
}
