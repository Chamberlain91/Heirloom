using Heirloom.Runtime;

namespace OneBitHero
{
    public class Actor : Entity
    {
        public SpriteRenderer SpriteRenderer { get; }

        public Collider Collider { get; }

        public Actor()
        {
            SpriteRenderer = AddComponent<SpriteRenderer>();
            Collider = AddComponent<Collider>();
            Depth = 10;
        }
    }
}
