using System;

namespace Meadows.Collections
{
    /// <summary>
    /// An unordered pair.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public readonly struct UnorderedPair<T> : IEquatable<UnorderedPair<T>>
    {
        /// <summary>
        /// The 'first' element in the pair.
        /// </summary>
        public readonly T A;

        /// <summary>
        /// The 'second' element in the pair.
        /// </summary>
        public readonly T B;

        /// <summary>
        /// Constructs a new unordered pair.
        /// </summary>
        /// <param name="a">The 'first' element.</param>
        /// <param name="b">The 'second' element.</param>
        public UnorderedPair(T a, T b)
        {
            A = a;
            B = b;
        }

        /// <inheritdoc/>
        public void Deconstruct(out T outA, out T outB)
        {
            outA = A;
            outB = B;
        }

        #region Conversion

        /// <summary>
        /// Implicitly convert an unordered pair into a 2-tuple.
        /// </summary>
        public static implicit operator UnorderedPair<T>((T a, T b) tuple)
        {
            return new UnorderedPair<T>(tuple.a, tuple.b);
        }

        /// <summary>
        /// Implicitly convert a 2-tuple into an unordered pair.
        /// </summary>
        public static implicit operator (T a, T b)(UnorderedPair<T> pair)
        {
            return (pair.A, pair.B);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this object for equality with another.
        /// </summary>
        /// <param name="obj">Some other object.</param>
        /// <returns>True, if the objects are considered equal.</returns>
        public override bool Equals(object obj)
        {
            return obj is UnorderedPair<T> pair
                && Equals(pair);
        }

        /// <summary>
        /// Compares this unordered pair for equality with another unordered pair.
        /// </summary>
        /// <param name="other">Some other pair.</param>
        /// <returns>True, if the pairs are considered equal.</returns>
        public bool Equals(UnorderedPair<T> other)
        {
            return (A.Equals(other.A) && B.Equals(other.B))
                || (A.Equals(other.B) && B.Equals(other.A));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            // Makes the hashcode symmetric, might not be the best for collisions however.
            return HashCode.Combine(A, B)
                 + HashCode.Combine(B, A);
        }

        #endregion
    }
}
