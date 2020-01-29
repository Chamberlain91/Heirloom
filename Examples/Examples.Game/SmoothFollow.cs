using Heirloom.Game;
using Heirloom.Math;

namespace Examples.Game
{
    internal class SmoothFollow : Component
    {
        public SmoothFollow(Transform target)
        {
            Target = target;
        }

        public Transform Target { get; }

        protected override void Update(float dt)
        {
            var x = Calc.Lerp(Transform.Position.X, Target.Position.X, 3 * dt);
            var y = Calc.Lerp(Transform.Position.Y, Target.Position.Y, 6 * dt);
            Transform.Position = (x, y);
        }
    }
}
