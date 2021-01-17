using Heirloom.Mathematics;

namespace Heirloom.Testing.Performance
{
    internal sealed class LineSegmentPerformance : PerformanceTest
    {
        private readonly LineSegment _segmentA;
        private readonly LineSegment _segmentB;

        public LineSegmentPerformance()
        {
            _segmentA = new LineSegment((0, 0), (10, 10));
            _segmentB = new LineSegment((0, 10), (10, 0));
        }

        [Test]
        public void IntersectionPerformance()
        {
            _segmentA.Intersects(_segmentB);
        }
    }
}
