using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class RenderContext : IDisposable
    {
        private const float FpsSampleDuration = 1F;

        // Quad mesh
        private static readonly Mesh _quadMesh = Mesh.CreateQuad(1, 1);

        private readonly Stack<State> _stateStack;

        private readonly Stopwatch _stopwatch;
        private float _fpsTime;
        private int _fpsCount;

        protected RenderContext(MultisampleQuality multisample)
        {
            _stateStack = new Stack<State>();
            _stopwatch = Stopwatch.StartNew();

            DefaultSurface = new Surface(1, 1, multisample);
        }

        /// <summary>
        /// Gets a value determining if this <see cref="RenderContext"/> was disposed.
        /// </summary>
        public abstract bool IsDisposed { get; }

        /// <summary>
        /// Gets how often the default surface is presented to the screen per second.
        /// </summary>
        public float FrameRate { get; private set; }

        /// <summary>
        /// Gets or sets a value that will enable or disable drawing the FPS overlay.
        /// </summary>
        public bool ShowFPSOverlay { get; set; } = false;

        /// <summary>
        /// Gets the default surface (ie, window) of this render context.
        /// </summary>
        public Surface DefaultSurface { get; }

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
        /// Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).
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
        /// Save the context state (push it on the state stack).
        /// </summary>
        public void SaveState()
        {
            var state = new State { Blending = Blending, Color = Color, Surface = Surface, Transform = Transform, Viewport = Viewport };
            _stateStack.Push(state);
        }

        protected void SetDefaultSurfaceSize(IntSize size)
        {
            DefaultSurface.SetSize(size);
        }

        /// <summary>
        /// Restore the context state (pop from the state stack).
        /// </summary>
        public void RestoreState()
        {
            if (_stateStack.Count == 0)
            {
                // todo: Should this throw an exception instead?
                ResetState();
            }
            else
            {
                // 
                var state = _stateStack.Pop();

                // Recover state values
                Surface = state.Surface;
                Viewport = state.Viewport;
                Transform = state.Transform;
                Blending = state.Blending;
                Color = state.Color;
            }
        }

        /// <summary>
        /// Clears the current surface with the specified color.
        /// </summary>
        public abstract void Clear(Color color);

        /// <summary>
        /// Draws a mesh with the given image to the current surface.
        /// </summary>
        /// <param name="mesh">Some mesh.</param>
        /// <param name="image">Some image.</param>
        /// <param name="transform">Some transform.</param>
        public abstract void DrawMesh(ImageSource image, Mesh mesh, Matrix transform);

        /// <summary>
        /// Draws an image to the current surface.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="transform">Some transform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawImage(ImageSource image, Matrix transform)
        {
            if (image.Origin != Vector.Zero)
            {
                // todo: optimize? M2 and M5?
                transform = transform * Matrix.CreateTranslation(-image.Origin);
            }

            // Scale to image dimensions
            transform.M0 *= image.Size.Width;
            transform.M3 *= image.Size.Width;
            transform.M1 *= image.Size.Height;
            transform.M4 *= image.Size.Height;

            DrawMesh(image, _quadMesh, transform);
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
        public void RefreshScreen()
        {
            ComputeFPS();
            DrawFPSOverlay();
            Flush();

            // Low level swap buffers
            SwapBuffers();
        }

        protected abstract void SwapBuffers();

        /// <summary>
        /// Force pending drawing operations to complete, useful for synchronization between contexts. <para/>
        /// Note: Currently untested for said synchronization.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Dispose this render context, freeing any resources occupied by it.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Updates the current surfaces version number.
        /// </summary>
        protected void UpdateSurfaceVersionNumber()
        {
            Surface.UpdateVersionNumber();
        }

        private void DrawFPSOverlay()
        {
            if (ShowFPSOverlay)
            {
                ResetState();

                var text = $"FPS: {FrameRate.ToString("0.00")}";
                var size = Font.Default.MeasureText(text, 16);

                Color = Color.DarkGray;
                this.DrawRect(new Rectangle(Surface.Width - 8 - size.Width - 3, 8, size.Width + 4, size.Height + 1));

                Color = Color.Pink;
                this.DrawText(text, new Vector(Surface.Width - 8, 8), Font.Default, 16, TextAlign.Right);
            }
        }

        private void ComputeFPS()
        {
            // Get elapsed time
            var delta = (float) _stopwatch.Elapsed.TotalSeconds;
            _stopwatch.Restart();

            _fpsTime += delta;
            _fpsCount++;

            if (_fpsTime >= FpsSampleDuration)
            {
                // hz, events/time
                FrameRate = _fpsCount / _fpsTime;

                _fpsCount = 0;
                _fpsTime = 0;
            }
        }

        private struct State
        {
            public Surface Surface;
            public Rectangle Viewport;
            public Matrix Transform;
            public Blending Blending;
            public Color Color;
        }
    }
}
