namespace Heirloom.Math
{
    public interface INoise1D
    {
        /// <summary>
        /// Samples noise in 1D.
        /// </summary>
        float Sample(float x);
    }

    public interface INoise2D
    {
        /// <summary>
        /// Samples noise in 2D.
        /// </summary>
        float Sample(float x, float y);
    }

    public interface INoise3D
    {
        /// <summary>
        /// Samples noise in 3D.
        /// </summary>
        float Sample(float x, float y, float z);
    }
}
