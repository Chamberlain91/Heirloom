namespace Heirloom.Math
{
    public interface INoise1D
    {
        /// <summary>
        /// Sample one-dimensional noise.
        /// </summary>
        float Sample(float x);
    }

    public interface INoise2D
    {
        /// <summary>
        /// Sample two-dimensional noise.
        /// </summary>
        float Sample(float x, float y);
    }

    public interface INoise3D
    {
        /// <summary>
        /// Sample three-dimensional noise.
        /// </summary>
        float Sample(float x, float y, float z);
    }
}
