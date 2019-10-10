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
        }

        protected override void Update(float dt)
        {
            // Check input buttons
            var goLeft = Input.GetButton("a") == ButtonState.Down;
            var goRight = Input.GetButton("d") == ButtonState.Down;

            // 
            if (goLeft || goRight)
            {
                if (SpriteRenderer.Animation.Name != "walk")
                {
                    SpriteRenderer.Play("walk");
                }

                // 
                if (goLeft) { Transform.Scale = (-1, 1); }
                else { Transform.Scale = (1, 1); }

                //
                if (goLeft) { Transform.Position -= (240 * dt, 0); }
                else { Transform.Position += (240 * dt, 0); }
            }
            else
            {
                if (SpriteRenderer.Animation.Name != "idle")
                {
                    SpriteRenderer.Play("idle");
                }
            }
        }
    }
}
