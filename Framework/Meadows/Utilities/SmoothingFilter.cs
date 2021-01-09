namespace Meadows.Utilities
{
    public sealed class SmoothingFilter : ISignalFilter
    {
        private float _x;

        public SmoothingFilter(float alpha)
        {
            Alpha = alpha;
        }

        public float Alpha { get; }

        public float ProcessSample(float x)
        {
            _x += Alpha * (x - _x);
            return _x;
        }
    }
}
