using Heirloom.Math;

namespace Heirloom.Runtime
{
    public interface ICollisionCallback
    {
        void OnCollision(Collider a, Collider b, in Manifold manifold);
    }
}
