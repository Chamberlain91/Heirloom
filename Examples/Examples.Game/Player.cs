using Heirloom.Drawing;
using Heirloom.Game;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    public class Player : Entity
    {
        public readonly SpriteComponent SpriteRenderer;

        public Player()
        {
            SpriteRenderer = AddComponent(new SpriteComponent(GetAsset<Sprite>("player")));
            SpriteRenderer.Play("walk");

            Transform.Position = (100, 100);
        }

        protected override void Update(float dt)
        {
            // 
        }
    }
}
