using System;

namespace Heirloom.Collections.Spatial
{
    public readonly struct BroadPhasePair<T> : IEquatable<BroadPhasePair<T>> where T : ISpatialObject
    {
        public readonly T A;

        public readonly T B;

        public BroadPhasePair(T a, T b)
        {
            if (Equals(a, b))
            {
                throw new ArgumentException($"Creating broad phase pair key with the same body.");
            }

            A = a;
            B = b;
        }

        public override bool Equals(object obj)
        {
            return obj is BroadPhasePair<T> key && Equals(key);
        }

        public bool Equals(BroadPhasePair<T> other)
        {
            return (Equals(A, other.A) && Equals(B, other.B))
                || (Equals(A, other.B) && Equals(B, other.A));
        }

        public override int GetHashCode()
        {
            var h1 = A.GetHashCode();
            var h2 = B.GetHashCode();

            return h1 ^ h2;
        }

        public static bool operator ==(BroadPhasePair<T> left, BroadPhasePair<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BroadPhasePair<T> left, BroadPhasePair<T> right)
        {
            return !(left == right);
        }
    }
}
