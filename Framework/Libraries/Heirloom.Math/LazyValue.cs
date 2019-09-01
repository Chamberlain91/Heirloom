namespace Heirloom.Math
{
    /// <summary>
    /// A utility structire to store a value and track the version number to lazily update the value on demand.
    /// </summary>
    public struct LazyValue<T> where T : struct
    {
        public uint Version { get; private set; }

        public T Value { get; private set; }

        public void Set(T value, uint version)
        {
            Version = version;
            Value = value;
        }

        public static implicit operator T(LazyValue<T> lazy)
        {
            return lazy.Value;
        }
    }
}
