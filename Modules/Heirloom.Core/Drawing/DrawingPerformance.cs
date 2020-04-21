using Heirloom;

using Range = Heirloom.Range;

namespace Heirloom
{
    /// <summary>
    /// Contains information pertaining to draw performance.
    /// </summary>
    public sealed class DrawingPerformance
    {
        private readonly Timer _timer;

        private readonly StatisticsHelper _triangleCount;
        private readonly StatisticsHelper _batchCount;
        private readonly StatisticsHelper _drawCount;
        private readonly StatisticsHelper _frameRate;

        internal DrawingPerformance()
        {
            // 
            _triangleCount = new StatisticsHelper();
            _batchCount = new StatisticsHelper();
            _drawCount = new StatisticsHelper();
            _frameRate = new StatisticsHelper();

            // 
            _timer = new Timer(1F);
        }

        /// <summary>
        /// Gets or sets a value that will enable or disable drawing the performance overlay.
        /// </summary>
        public PerformanceOverlayMode OverlayMode { get; set; } = PerformanceOverlayMode.Disabled;

        /// <summary>
        /// Statistics of the number of batches.
        /// </summary>
        public Statistics BatchCount { get; private set; }

        /// <summary>
        /// Statistics of the number of 'things' drawn.
        /// </summary>
        public Statistics DrawCount { get; private set; }

        /// <summary>
        /// Statistics of the number of triangles.
        /// </summary>
        public Statistics TriangleCount { get; private set; }

        /// <summary>
        /// Statistics of the frame rate.
        /// </summary>
        public Statistics FrameRate { get; private set; }

        internal void ComputeStatistics(Graphics.DrawCounts info)
        {
            // If enough time has passed, compute average
            if (_timer.Tick(out _))
            {
                // Compute statistics
                TriangleCount = _triangleCount.Compute();
                BatchCount = _batchCount.Compute();
                DrawCount = _drawCount.Compute();
                FrameRate = _frameRate.Compute();

                // Clear samples
                _triangleCount.ClearSamples();
                _batchCount.ClearSamples();
                _drawCount.ClearSamples();
                _frameRate.ClearSamples();
            }

            // Record sample
            _triangleCount.AddSample(info.TriangleCount);
            _batchCount.AddSample(info.BatchCount);
            _drawCount.AddSample(info.DrawCount);
            _frameRate.AddSample(1F / _timer.Delta);
        }

        private sealed class StatisticsHelper
        {
            private float _sum, _squareSum;
            private Range _range;
            private int _count;

            public StatisticsHelper()
            {
                ClearSamples();
            }

            public void AddSample(float value)
            {
                // Accumulate value (sum values)
                _range.Include(value);
                _squareSum += value * value;
                _sum += value;

                // Count sample 
                _count++;
            }

            public void ClearSamples()
            {
                _range = Range.Indeterminate;
                _squareSum = _sum = 0;
                _count = 0;
            }

            public Statistics Compute()
            {
                return Statistics.Compute(_sum, _squareSum, _range, _count);
            }
        }
    }
}
