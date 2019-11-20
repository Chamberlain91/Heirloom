using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class LoadScreen
    {
        protected internal static LoadScreenProgress Progress = new LoadScreenProgress();

        protected internal abstract void Draw(Graphics ctx, float dt);
    }
}
