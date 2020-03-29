using System;
using System.Diagnostics;
using System.Threading;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides a thread to manage invoking a render/update function continuously.
    /// </summary>
    public abstract class RenderLoop
    {
        public delegate void UpdateFunction(Graphics gfx, float dt);

        private Thread _thread;

        #region Constructor

        protected RenderLoop(Graphics graphics)
        {
            Graphics = graphics;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated render context.
        /// </summary>
        public Graphics Graphics { get; }

        /// <summary>
        /// Is the render thread active?
        /// </summary>
        public bool IsRunning { get; private set; }

        #endregion

        protected abstract void Update(Graphics gfx, float dt);

        /// <summary>
        /// Start the render thread.
        /// This thread will automatically terminate when the associated graphics object is disposed.
        /// </summary>
        public void Start()
        {
            if (IsRunning) { throw new InvalidOperationException($"{nameof(RenderLoop)} has already started."); }

            // Mark thread for life
            IsRunning = true;

            // Create and start thread
            _thread = new Thread(ThreadBody) { IsBackground = true, Name = "Render Thread" };
            _thread.Start();
        }

        /// <summary>
        /// Stop the render thread.
        /// </summary>
        public void Stop()
        {
            if (!IsRunning) { throw new InvalidOperationException($"{nameof(RenderLoop)} has already stopped."); }

            // Mark thread for death
            IsRunning = false;

            // Wait for thread to exit
            _thread.Join();
        }

        private void ThreadBody()
        {
            var stopwatch = Stopwatch.StartNew();

            while (IsRunning)
            {
                // == Compute Delta

                var delta = (float) stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();

                // == Render Phase

                // Lock the render context to have 'exclusive control' and prevent it
                // from being disposed of when it is needed to render.
                lock (Graphics)
                {
                    // Render context was disposed of already (we can't render anymore)
                    // so we will shutdown and exit this thread.
                    if (Graphics.IsDisposed)
                    {
                        Stop();
                        break;
                    }

                    // Draw Application
                    Graphics.ResetState();
                    Update(Graphics, delta);

                    // Push pixels to screen
                    Graphics.RefreshScreen();
                }
            }
        }

        /// <summary>
        /// Creates a render loop instance from the given context and method reference.
        /// </summary>
        /// <param name="gfx">The relevant graphics context.</param>
        /// <param name="update">The relevant update function.</param>
        /// <returns></returns>
        public static RenderLoop Create(Graphics gfx, UpdateFunction update)
        {
            if (gfx is null) { throw new ArgumentNullException(nameof(gfx)); }
            if (update is null) { throw new ArgumentNullException(nameof(update)); }

            return new DefaultRenderLoop(gfx, update);
        }

        private sealed class DefaultRenderLoop : RenderLoop
        {
            private readonly UpdateFunction _update;

            public DefaultRenderLoop(Graphics context, UpdateFunction update)
                : base(context)
            {
                _update = update ?? throw new ArgumentNullException(nameof(update));
            }

            protected override void Update(Graphics gfx, float delta)
            {
                _update(gfx, delta);
            }
        }
    }
}
