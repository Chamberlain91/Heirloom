using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class AbstractGame
    {
        protected AbstractGame(string title)
        {
            SceneManager = new SceneManager();
            Title = title;
        }

        public string Title { get; }

        public RenderContext RenderContext { get; }

        public SceneManager SceneManager { get; }

        protected void Update(RenderContext ctx, float dt)
        {
            // Process input
            Input.Update();

            // Update scenes
            SceneManager.Update(ctx, dt);
        }
    }
}
