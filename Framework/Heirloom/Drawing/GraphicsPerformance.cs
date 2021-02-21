using System.Diagnostics;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides performance metrics for an instance of <see cref="GraphicsContext"/>.
    /// </summary>
    public sealed class GraphicsPerformance
    {
        readonly private Stopwatch _stopwatch;

        private int _batchCount;
        private int _evictCount;
        private int _frameCount;

        internal GraphicsPerformance()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Enables or disables drawing the performance overlay.
        /// </summary>
        public bool ShowOverlay { get; set; }

        /// <summary>
        /// Gets the average frame rate (in hertz).
        /// </summary>
        public float AverageFrameRate { get; private set; }

        /// <summary>
        /// Gets the average frame time (in seconds).
        /// </summary>
        public float AverageFrameTime { get; private set; }

        /// <summary>
        /// Gets the average number of geometry batches per frame.
        /// </summary>
        public float Batches { get; private set; }

        /// <summary>
        /// Gets the average number of atlas evictions per frame.
        /// </summary>
        public float AtlasEvictions { get; private set; }

        internal void NotifyFrame()
        {
            var elapsedTime = _stopwatch.ElapsedTicks / (float) Stopwatch.Frequency;

            // Count frame
            _frameCount++;

            // When a second has elapsed
            if (elapsedTime > 1F)
            {
                // Compute average frames per-second
                AverageFrameRate = _frameCount / elapsedTime;
                AverageFrameTime = 1F / AverageFrameRate;

                // Compute average batches per frame
                Batches = _batchCount / elapsedTime;
                Batches /= AverageFrameRate;

                // Compute average evictions per frame
                AtlasEvictions = _evictCount / elapsedTime;
                AtlasEvictions /= AverageFrameRate;

                _stopwatch.Restart();

                _batchCount = 0;
                _evictCount = 0;
                _frameCount = 0;
            }
        }

        internal void NotifyBatch()
        {
            _batchCount++;
        }

        internal void NotifyEviction()
        {
            _batchCount++;
        }
    }
}
