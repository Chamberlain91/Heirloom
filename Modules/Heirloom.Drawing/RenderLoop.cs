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
        public delegate void UpdateFunction(RenderContext ctx, float dt);

        private Thread _thread;

        #region Constructor

        public RenderLoop(RenderContext context)
        {
            Context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated render context.
        /// </summary>
        public RenderContext Context { get; }

        /// <summary>
        /// Is the render thread active?
        /// </summary>
        public bool IsRunning { get; private set; }

        #endregion

        protected abstract void Update(RenderContext renderContext, float delta);

        /// <summary>
        /// Start the render thread.
        /// </summary>
        public void Start()
        {
            if (IsRunning) { throw new InvalidOperationException($"{nameof(RenderLoop)} has already started."); }

            // Create and start thread
            IsRunning = true;
            _thread = new Thread(ThreadBody) { IsBackground = true, Name = "Render Thread" };
            _thread.Start();
        }

        /// <summary>
        /// Stop the render thread.
        /// </summary>
        public void Stop()
        {
            if (!IsRunning) { throw new InvalidOperationException($"{nameof(RenderLoop)} has already stopped."); }

            IsRunning = false;
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
                lock (Context)
                {
                    // Render context was disposed of already (we can't render anymore)
                    // so we will shutdown and exit this thread.
                    if (Context.IsDisposed)
                    {
                        Stop();
                        break;
                    }

                    // Draw Application
                    Context.ResetState();
                    Update(Context, delta);

                    // Push pixels to screen
                    Context.RefreshScreen();
                }
            }
        }

        /// <summary>
        /// Creates a render loop instance from the given context and method reference.
        /// </summary>
        /// <param name="ctx">The relevant render context.</param>
        /// <param name="update">The relevant update function.</param>
        /// <returns></returns>
        public static RenderLoop Create(RenderContext ctx, UpdateFunction update)
        {
            if (ctx is null) { throw new ArgumentNullException(nameof(ctx)); }
            if (update is null) { throw new ArgumentNullException(nameof(update)); }

            return new DefaultRenderLoop(ctx, update);
        }

        private sealed class DefaultRenderLoop : RenderLoop
        {
            private readonly UpdateFunction _update;

            public DefaultRenderLoop(RenderContext context, UpdateFunction update)
                : base(context)
            {
                _update = update ?? throw new ArgumentNullException(nameof(update));
            }

            protected override void Update(RenderContext ctx, float delta)
            {
                _update(ctx, delta);
            }
        }
    }
}
