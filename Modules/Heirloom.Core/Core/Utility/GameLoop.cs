using System;
using System.Collections.Generic;
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

        private readonly List<(UpdateFunction Update, int SortIndex)> _updateCallbacks = new List<(UpdateFunction, int)>();

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

            Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            FixedFrameRate = frameRate;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the graphics context associated with this loop.
        /// </summary>
        public GraphicsContext Graphics { get; }

        /// <summary>
        /// Gets the screen associated with <see cref="Graphics"/>.
        /// </summary>
        public Screen Screen => Graphics.Screen;

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
        protected virtual void Update(float dt)
        {
            // Process update functions
            foreach (var (update, _) in _updateCallbacks)
            {
                update(Graphics, dt);
            }
        }

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

        /// <summary>
        /// Adds a function to run during <see cref="Update(float)"/>.
        /// </summary>
        /// <remarks>
        /// The <paramref name="priority"/> number controls the execution order of the callbacks.
        /// </remarks>
        /// <param name="callback">Some update callback.</param>
        /// <param name="priority">A priority number to ensure callbacks happen in expected order.</param>
        /// <exception cref="ArgumentException">Thrown when the callback function already exists.</exception>
        public void AddUpdateCallback(UpdateFunction callback, int priority = 5000)
        {
            // Validate arguments
            if (callback is null) { throw new ArgumentNullException(nameof(callback)); }
            if (_updateCallbacks.FindIndex(c => c.Update == callback) >= 0)
            {
                throw new InvalidOperationException("Callback already registered.");
            }

            // Append the callback
            _updateCallbacks.Add((callback, priority));
            _updateCallbacks.Sort((a, b) => a.SortIndex.CompareTo(b.SortIndex));
        }

        /// <summary>
        /// Removes an update callback previously added with <see cref="AddUpdateCallback(UpdateFunction, int)"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the callback function already exists.</exception>
        public void RemoveUpdateCallback(UpdateFunction callback)
        {
            // Remove the callback
            _updateCallbacks.RemoveAll(c => c.Update == callback);
            _updateCallbacks.Sort((a, b) => a.SortIndex.CompareTo(b.SortIndex));
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
                        Update(delta);

                        // Push pixels to screen
                        Graphics.Screen.Refresh();
                    }
                }
            }
        }
    }
}
