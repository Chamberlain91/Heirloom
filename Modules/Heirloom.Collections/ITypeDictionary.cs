using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Manages objects by their type hierarchy up to the base type, allowing access by enumeration of objects by type.
    /// </summary>
    /// <typeparam name="T">Some base type</typeparam>
    public interface ITypeDictionary<T> : IReadOnlyTypeDictionary<T>
    {
        /// <summary>
        /// Add a new object to the type dictionary.
        /// </summary>
        /// <param name="obj">Some object.</param>
        void Add(T obj);

        /// <summary>
        /// Remove an object from the type dictionary.
        /// </summary>
        /// <param name="obj">Some object.</param>
        /// <returns>True if the object was contained and successfully removed.</returns>
        bool Remove(T obj);
    }

    /// <summary>
    /// A read-only view of <see cref="ITypeDictionary{T}"/>.
    /// </summary>
    /// <typeparam name="T">Some base type</typeparam>
    public interface IReadOnlyTypeDictionary<T>
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
        IEnumerable<X> Enumerate<X>() where X : T;
    }
}
