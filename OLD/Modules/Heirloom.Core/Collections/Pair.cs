using System;

namespace Heirloom.Collections
{
    /// <summary>
    /// A pair.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public readonly struct Pair<T> : IEquatable<Pair<T>>
    {
        /// <summary>
        /// The first element in the pair.
        /// </summary>
        public readonly T A;

        /// <summary>
        /// The second element in the pair.
        /// </summary>
        public readonly T B;

        /// <summary>
        /// Constructs a new pair.
        /// </summary>
        /// <param name="a">The 'first' element.</param>
        /// <param name="b">The 'second' element.</param>
        public Pair(T a, T b)
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
        /// Implicitly convert an pair into a 2-tuple.
        /// </summary>
        public static implicit operator Pair<T>((T a, T b) tuple)
        {
            return new Pair<T>(tuple.a, tuple.b);
        }

        /// <summary>
        /// Implicitly convert a 2-tuple into an pair.
        /// </summary>
        public static implicit operator (T a, T b)(Pair<T> pair)
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
            return obj is Pair<T> pair
                && Equals(pair);
        }

        /// <summary>
        /// Compares this pair for equality with another pair.
        /// </summary>
        /// <param name="other">Some other pair.</param>
        /// <returns>True, if the pairs are considered equal.</returns>
        public bool Equals(Pair<T> other)
        {
            return A.Equals(other.A) && B.Equals(other.B);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }

        #endregion
    }
}
