using System;
using System.Diagnostics;
using System.Threading;

namespace Meadows.Utilities
{
    /// <summary>
    /// Update function called every iteration of the game loop.
    /// </summary>
    /// <category>Utility</category>
    public delegate void UpdateFunction(float dt);

    /// <summary>
    /// Provides a thread to manage invoking a render/update function continuously.
    /// </summary>
    /// <category>Utility</category>
    public sealed class GameLoop
    {
        private Thread _thread;

        private readonly UpdateFunction _updateFunction;

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="GameLoop"/>.
        /// </summary>
        public GameLoop(UpdateFunction updateFunction, int frameRate = -1)
        {
            if (frameRate <= 0 && frameRate != -1) { throw new ArgumentException("Must be greater than zero or equal to -1.", nameof(frameRate)); }

            _updateFunction = updateFunction;
            FixedFrameRate = frameRate;
        }

        #endregion

        #region Properties

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

                    // Invoke update callback
                    _updateFunction(delta);
                }
            }
        }

        public static GameLoop StartNew(UpdateFunction updateFunction, int frameRate = -1)
        {
            var loop = new GameLoop(updateFunction, frameRate);
            loop.Start();
            return loop;
        }
    }
}
