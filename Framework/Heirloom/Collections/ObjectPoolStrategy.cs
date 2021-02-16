namespace Heirloom.Collections
{
    /// <summary>
    /// Discovered by reflection, implementations of this class allow instances <see cref="ObjectPool{T}"/>
    /// to allocate and clear objects of type <typeparamref name="T"/>.
    /// </summary>
    public abstract class ObjectPoolStrategy<T>
    {
        /// <summary>
        /// Clear an object for reuse in the pool.
        /// </summary>
        protected internal abstract void Clear(T obj);

        /// <summary>
        /// Creates a new instance for use in the pool
        /// </summary>
        protected internal abstract T Create();
    }
}
