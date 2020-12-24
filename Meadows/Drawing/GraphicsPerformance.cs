using System.Diagnostics;

namespace Meadows.Drawing
{
    public class GraphicsPerformance
    {
        readonly private Stopwatch _stopwatch;

        private int _batchCount;
        private int _frameCount;

        internal GraphicsPerformance()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public float FPS { get; private set; }

        public float Batches { get; private set; }

        public bool ShowOverlay { get; set; }

        internal void NotifyFrame()
        {
            var elapsedTime = _stopwatch.ElapsedTicks / (float) Stopwatch.Frequency;

            // Count frame
            _frameCount++;

            // When a second has elapsed
            if (elapsedTime > 1F)
            {
                // Compute average frames per-second
                FPS = _frameCount / elapsedTime;

                // Compute average batches per second -> per frame
                Batches = _batchCount / elapsedTime;
                Batches /= FPS;

                _stopwatch.Restart();

                _batchCount = 0;
                _frameCount = 0;
            }
        }

        internal void NotifyBatch()
        {
            _batchCount++;
        }
    }
}
