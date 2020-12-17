using System.Collections.Generic;

namespace Meadows.Collections
{
    /// <summary>
    /// A read-only view of <see cref="ITypeDictionary{T}"/>.
    /// </summary>
    /// <typeparam name="T">Some base type</typeparam>
    public interface IReadOnlyTypeDictionary<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Does this type dictionary contain this object?
        /// </summary>
        /// <param name="obj">Some object.</param>
        /// <returns>True if the object is contained.</returns>
        bool Contains(T obj);

        /// <summary>
        /// Does the dictionary contain any object that inherits from the specified type.
        /// </summary>
        /// <typeparam name="X">Some inherited type.</typeparam>
        /// <returns>True if any object inherit from this type.</returns>
        bool ContainsType<X>() where X : T;

        /// <summary>
        /// Enumerates any object that inherits from the specified type.
        /// </summary>
        /// <typeparam name="X">Some inherited type.</typeparam>
        /// <returns>Enumeration of objects by the inherited type.</returns>
        IEnumerable<X> GetItemsByType<X>() where X : T;
    }
}
