using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public interface IRenderContext
    {
        Surface CreateSurface(int width, int height);
        Surface CreateSurface(IntSize size);

        // == Surface ==

        Surface DefaultSurface { get; } // window, framebuffer 0 
        Surface Surface { get; set; }   // active framebuffer

        // == State ==

        Rectangle Viewport { get; set; }
        Matrix Transform { get; set; }
        BlendMode BlendMode { get; set; }
        Color BlendColor { get; set; }

        // Reset state to defaults
        void ResetState();

        // 
        // void SetShader(Shader shader);
        // void GetShader();
        // 
        // void SetShaderVariable<P>(string name, P parameter) where P : struct;
        // 

        // == Write Operations ==

        void Clear(Color color);

        void Draw(ImageSource image, Matrix transform);
        void Draw(ImageSource image, Matrix transform, Color color);

        void Draw(ImageSource image, Mesh mesh, Matrix transform);
        void Draw(ImageSource image, Mesh mesh, Matrix transform, Color color);

        // == Read Operations ==

        Image Capture(IntRectangle region);
        Image Capture();

        // == Swap Buffers

        void SwapBuffers();

        // == Dispose ==

        bool IsDisposed { get; }
        void Dispose();
    }

    public abstract class RenderContext : IDisposable, IRenderContext
    {
        private readonly struct DisposePush : IDisposable
        {
            private readonly RenderContext _context;

            public DisposePush(RenderContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public void Dispose()
            {
                _context.PopState();
            }
        }

        private struct State
        {
            public Surface Surface;
            public Rectangle Viewport;
            public Matrix Transform;
            public Color BlendColor;
            public BlendMode BlendMode;

            public State(Surface surface, Rectangle viewport, Matrix transform, Color blendColor, BlendMode blendMode)
            {
                Surface = surface ?? throw new ArgumentNullException(nameof(surface));
                Viewport = viewport;
                Transform = transform;
                BlendColor = blendColor;
                BlendMode = blendMode;
            }
        }

        private readonly Stack<State> _states;

        protected RenderContext()
        {
            _states = new Stack<State>();
        }

        public abstract Surface DefaultSurface { get; }

        public abstract Surface CreateSurface(IntSize size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Surface CreateSurface(int width, int height)
        {
            return CreateSurface(new IntSize(width, height));
        }

        public abstract Surface Surface { get; set; }

        public abstract Rectangle Viewport { get; set; }

        public abstract Matrix Transform { get; set; }

        public abstract Matrix InverseTransform { get; }

        /// <summary>
        /// Approximates the scaling factor that one pixel consumes in world units.
        /// </summary>
        public abstract float ApproximatePixelScale { get; }

        public abstract Color BlendColor { get; set; }

        public abstract BlendMode BlendMode { get; set; }

        public IDisposable PushState()
        {
            var state = new State(Surface, Viewport, Transform, BlendColor, BlendMode);
            _states.Push(state);

            return new DisposePush(this);
        }

        public void PopState()
        {
            var state = _states.Pop();

            Surface = state.Surface;
            Viewport = state.Viewport;
            Transform = state.Transform;
            BlendColor = state.BlendColor;
            BlendMode = state.BlendMode;
        }

        public virtual void ResetState()
        {
            Surface = DefaultSurface; // also adjusts viewport
            Viewport = (0, 0, 1, 1);

            Transform = Matrix.Identity;
            BlendMode = BlendMode.Alpha;
            BlendColor = Color.White;
        }

        // 'Write Operations'
        public abstract void Clear(Color color);
        public abstract void Draw(ImageSource image, Matrix transform, Color color);
        public abstract void Draw(ImageSource image, Mesh mesh, Matrix transform, Color color);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(ImageSource image, Matrix transform)
        {
            Draw(image, transform, Color.White);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(ImageSource image, Mesh mesh, Matrix transform)
        {
            Draw(image, mesh, transform, Color.White);
        }

        // 'Read Operations'
        public abstract Image Capture(IntRectangle region);

        public Image Capture()
        {
            return Capture((0, 0, Surface.Width, Surface.Height));
        }

        // 
        public abstract void SwapBuffers();

        // 
        public abstract bool IsDisposed { get; }
        public abstract void Dispose();
    }
}
