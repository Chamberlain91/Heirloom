using Heirloom.Game;

namespace Examples.Gridcannon
{
    public sealed class GameManager : Entity
    {
        protected override void Update(float dt)
        {
            if (Input.GetButton("mouse.left") == ButtonState.Pressed)
            {
                // todo: check cards for clicks
            }
        }
    }
}
