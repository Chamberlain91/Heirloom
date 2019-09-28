using System.Diagnostics;

using Heirloom.Math;

namespace Heirloom.Desktop.Game
{
    public class Timer
    {
        private readonly Stopwatch _stopwatch;

        public Timer(float duration)
        {
            _stopwatch = new Stopwatch();
            Duration = duration;
        }

        public float Duration { get; set; }

        public float Elapsed => Calc.Min(Duration, (float) _stopwatch.Elapsed.TotalSeconds);

        public float Remaining => Duration - Elapsed;

        public static Timer StartNew(float duration)
        {
            var timer = new Timer(duration);
            timer.Start();
            return timer;
        }

        public void Restart()
        {
            _stopwatch.Restart();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }
    }
}
