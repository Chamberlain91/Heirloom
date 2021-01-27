using System;

namespace Heirloom.Collections
{
    /// <summary>
    /// An ordered pair.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public readonly struct OrderedPair<T> : IEquatable<OrderedPair<T>>
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
        public OrderedPair(T a, T b)
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

        /// <summary>
        /// Returns the other item of the pair.
        /// </summary>
        /// <param name="x">Some item in this pair.</param>
        /// <returns>The opposing item of the pair.</returns>
        public T GetOther(T x)
        {
            if (Equals(x, A)) { return B; }
            if (Equals(x, B)) { return A; }
            throw new ArgumentException("Unable to get other item, argument not contained by the pair.", nameof(x));
        }

        #region Conversion

        /// <summary>
        /// Implicitly convert an unordered pair into a 2-tuple.
        /// </summary>
        public static implicit operator OrderedPair<T>((T a, T b) tuple)
        {
            return new OrderedPair<T>(tuple.a, tuple.b);
        }

        /// <summary>
        /// Implicitly convert a 2-tuple into an unordered pair.
        /// </summary>
        public static implicit operator (T a, T b)(OrderedPair<T> pair)
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
            return obj is OrderedPair<T> pair
                && Equals(pair);
        }

        /// <summary>
        /// Compares this unordered pair for equality with another unordered pair.
        /// </summary>
        /// <param name="other">Some other pair.</param>
        /// <returns>True, if the pairs are considered equal.</returns>
        public bool Equals(OrderedPair<T> other)
        {
            return A.Equals(other.A) && B.Equals(other.B);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }

        /// <inheritdoc/>
        public static bool operator ==(OrderedPair<T> left, OrderedPair<T> right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(OrderedPair<T> left, OrderedPair<T> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
