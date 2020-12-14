using System.Diagnostics;

namespace Meadows.Drawing
{
    public class GraphicsPerformance
    {
        readonly private Stopwatch _stopwatch;
        private int _frameCount;

        internal GraphicsPerformance()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public float FrameRate { get; private set; }

        internal void NotifyFrame()
        {
            var _frameTime = _stopwatch.ElapsedTicks / (float) Stopwatch.Frequency;
            _frameCount++;

            if (_frameTime > 1F)
            {
                FrameRate = _frameCount / _frameTime;

                _stopwatch.Restart();
                _frameCount = 0;
            }
        }
    }
}
