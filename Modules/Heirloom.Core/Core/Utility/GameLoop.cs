using System;
using System.Diagnostics;
using System.Threading;

namespace Heirloom
{
    /// <summary>
    /// Update function called every iteration of the game loop.
    /// </summary>
    /// <category>Utility</category>
    public delegate void UpdateFunction(GraphicsContext gfx, float dt);

    /// <summary>
    /// Provides a thread to manage invoking a render/update function continuously.
    /// </summary>
    /// <category>Utility</category>
    public abstract class GameLoop
    {
        private Thread _thread;

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="GameLoop"/>.
        /// </summary>
        protected GameLoop(Screen screen, int frameRate = -1)
            : this(screen.Graphics, frameRate)
        { }

        /// <summary>
        /// Constructs a new instance of <see cref="GameLoop"/>.
        /// </summary>
        protected GameLoop(GraphicsContext graphics, int frameRate = -1)
        {
            if (frameRate <= 0 && frameRate != -1) { throw new ArgumentException("Must be greater than zero or equal to -1.", nameof(frameRate)); }

            Graphics = graphics;
            FixedFrameRate = frameRate;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated render context.
        /// </summary>
        public GraphicsContext Graphics { get; }

        /// <summary>
        /// Is the render thread active?
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets or sets the fixed frame rate.
        /// </summary>
        /// <remarks>
        /// Set to -1 to 'unlock' the framerate.
        /// </remarks>
        public int FixedFrameRate { get; set; }

        #endregion

        /// <summary>
        /// Called every iteration of the game loop to update and/or render the application "every frame".
        /// </summary>
        protected abstract void Update(GraphicsContext gfx, float dt);

        /// <summary>
        /// Start the render thread.
        /// This thread will automatically terminate when the associated graphics object is disposed.
        /// </summary>
        public void Start()
        {
            if (IsRunning) { throw new InvalidOperationException($"{nameof(GameLoop)} has already started."); }

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
            if (!IsRunning) { throw new InvalidOperationException($"{nameof(GameLoop)} has already stopped."); }

            // Mark thread for death
            IsRunning = false;

            // Wait for thread to exit
            _thread.Join();
        }

        private void ThreadBody()
        {
            var stopwatch = Stopwatch.StartNew();

            var errorTime = 0F;

            while (IsRunning)
            {
                // == Compute Delta

                var delta = (float) stopwatch.Elapsed.TotalSeconds + errorTime;

                // Fixed frame rate
                var fixedDelta = 1F / FixedFrameRate;

                // If enough time has elapsed or infinite frame rate
                if (delta >= fixedDelta || FixedFrameRate == -1)
                {
                    // Begin measuring time from zero
                    stopwatch.Restart();

                    // Get the amount of time exceeded for next iteration
                    if (FixedFrameRate > 0)
                    {
                        errorTime = delta - fixedDelta;
                        delta = fixedDelta;
                    }
                    else
                    {
                        errorTime = 0F;
                    }

                    // == Render Phase

                    // Lock the render context to have 'exclusive control' and prevent it
                    // from being disposed of when it is needed to render.
                    lock (Graphics)
                    {
                        // Render context was disposed of already (we can't render anymore)
                        // so we will shutdown and exit this thread.
                        if (Graphics.IsDisposed || !Graphics.IsInitialized)
                        {
                            Stop();
                            break;
                        }

                        // Draw Application
                        Graphics.ResetState();
                        Update(Graphics, delta);

                        // Push pixels to screen
                        Graphics.Screen.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Creates a render loop instance from the given context and method reference.
        /// </summary>
        /// <param name="gfx">The relevant graphics context.</param>
        /// <param name="update">The relevant update function.</param>
        /// <param name="frameRate">The desired fixed frame rate or -1 for unlimited.</param>
        public static GameLoop Create(GraphicsContext gfx, UpdateFunction update, int frameRate = -1)
        {
            if (gfx is null) { throw new ArgumentNullException(nameof(gfx)); }
            if (update is null) { throw new ArgumentNullException(nameof(update)); }

            return new DefaultRenderLoop(gfx, update, frameRate);
        }

        private sealed class DefaultRenderLoop : GameLoop
        {
            private readonly UpdateFunction _update;

            public DefaultRenderLoop(GraphicsContext context, UpdateFunction update, int frameRate)
                : base(context, frameRate)
            {
                _update = update ?? throw new ArgumentNullException(nameof(update));
            }

            protected override void Update(GraphicsContext gfx, float delta)
            {
                _update(gfx, delta);
            }
        }
    }
}
