using System;
using System.Collections.Generic;

namespace Heirloom.Drawing
{
    public struct FrameStatistics<T> : IEquatable<FrameStatistics<T>> where T : struct
    {
        /// <summary>
        /// The number of batches drawn this frame.
        /// </summary>
        public T BatchCount { get; internal set; }

        /// <summary>
        /// The number of items drawn frame.
        /// </summary>
        public T DrawCount { get; internal set; }

        /// <summary>
        /// The number of triangles drawn this frame. <para/>
        /// <b>Note:</b> Two triangles per image drawn, custom polygons will have more.
        /// </summary>
        public T TriCount { get; internal set; }

        internal void Reset()
        {
            BatchCount = default;
            DrawCount = default;
            TriCount = default;
        }

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is FrameStatistics<T> statistics && Equals(statistics);
        }

        public bool Equals(FrameStatistics<T> other)
        {
            return EqualityComparer<T>.Default.Equals(BatchCount, other.BatchCount) &&
                   EqualityComparer<T>.Default.Equals(DrawCount, other.DrawCount) &&
                   EqualityComparer<T>.Default.Equals(TriCount, other.TriCount);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BatchCount, DrawCount, TriCount);
        }

        public static bool operator ==(FrameStatistics<T> left, FrameStatistics<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FrameStatistics<T> left, FrameStatistics<T> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
