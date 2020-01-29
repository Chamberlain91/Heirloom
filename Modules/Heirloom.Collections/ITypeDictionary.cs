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
        bool Add(T obj);

        /// <summary>
        /// Remove an object from the type dictionary.
        /// </summary>
        /// <param name="obj">Some object.</param>
        /// <returns>True if the object was contained and successfully removed.</returns>
        bool Remove(T obj);
    }
}
