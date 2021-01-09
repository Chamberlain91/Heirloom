using Heirloom.Drawing;

namespace Heirloom.Utilities
{
    public abstract class GameWrapper
    {
        protected GameWrapper(GraphicsContext graphics, int frameRate = -1)
        {
            Graphics = graphics;
            Loop = new GameLoop(Update, frameRate);
        }

        protected GraphicsContext Graphics { get; private set; }

        public GameLoop Loop { get; }

        internal protected virtual void Resume() { }

        internal protected virtual void Pause() { }

        protected abstract void Update(float dt);
    }
}
