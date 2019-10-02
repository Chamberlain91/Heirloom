using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class GameContext
    {
        private readonly RenderLoop _gameLoop;

        protected GameContext(RenderContext ctx)
        {
            _gameLoop = RenderLoop.Create(ctx, Update);
            SceneManager = new SceneManager();
        }

        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        public SceneManager SceneManager { get; }

        /// <summary>
        /// Gets the render context.
        /// </summary>
        public RenderContext RenderContext => _gameLoop.RenderContext;

        /// <summary>
        /// Gets a value determining if the game loop is running.
        /// </summary>
        public bool IsRunning => _gameLoop.IsRunning;

        /// <summary>
        /// Begins the game loop.
        /// </summary>
        public void Start()
        {
            _gameLoop.Start();
        }

        /// <summary>
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            _gameLoop.Stop();
        }

        protected void Update(RenderContext ctx, float dt)
        {
            // Process input
            Input.Update();

            // Update scenes
            SceneManager.Update(ctx, dt);
        }
    }
}
