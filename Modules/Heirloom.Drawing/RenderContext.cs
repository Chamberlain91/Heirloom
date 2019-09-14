using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class RenderContext : IDisposable
    {
        protected RenderContext()
        {
            // Nothing to do
        }

        public abstract Surface CreateSurface(int width, int height);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Surface CreateSurface(IntSize size)
        {
            return CreateSurface(size.Width, size.Height);
        }

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
        public abstract Color BlendColor { get; set; }

        /// <summary>
        /// Gets or sets the current blending mode.
        /// </summary>
        public abstract BlendMode BlendMode { get; set; }

        /// <summary>
        /// Reset context state to defaults.
        /// </summary>
        public void ResetState()
        {
            Surface = DefaultSurface; // also adjusts viewport?
            Viewport = (0, 0, 1, 1);

            Transform = Matrix.Identity;
            BlendMode = BlendMode.Alpha;
            BlendColor = Color.White;
        }

        /// <summary>
        /// Clears the current surface with the specified color.
        /// </summary>
        public abstract void Clear(Color color);

        /// <summary>
        /// Draws an image with the given blending color.
        /// </summary>
        public abstract void Draw(ImageSource image, Matrix transform, Color color);

        /// <summary>
        /// Draws an image with the given mesh and blending color.
        /// </summary>
        public abstract void Draw(ImageSource image, Mesh mesh, Matrix transform, Color color);

        /// <summary>
        /// Draws an image.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(ImageSource image, Matrix transform)
        {
            Draw(image, transform, Color.White);
        }

        /// <summary>
        /// Draws an image with the given mesh.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(ImageSource image, Mesh mesh, Matrix transform)
        {
            Draw(image, mesh, transform, Color.White);
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
