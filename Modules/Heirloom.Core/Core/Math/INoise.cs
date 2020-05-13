namespace Heirloom
{
    /// <summary>
    /// Provides an interface for sampling one-dimensional noise.
    /// </summary>
    /// <category>Mathematics</category>
    public interface INoise1D
    {
        /// <summary>
        /// Sample one-dimensional noise.
        /// </summary>
        float Sample(float x);
    }

    /// <summary>
    /// Provides an interface for sampling two-dimensional noise.
    /// </summary>
    /// <category>Mathematics</category>
    public interface INoise2D
    {
        /// <summary>
        /// Sample two-dimensional noise.
        /// </summary>
        float Sample(float x, float y);
    }

    /// <summary>
    /// Provides an interface for sampling three-dimensional noise.
    /// </summary>
    /// <category>Mathematics</category>
    public interface INoise3D
    {
        /// <summary>
        /// Sample three-dimensional noise.
        /// </summary>
        float Sample(float x, float y, float z);
    }
}
