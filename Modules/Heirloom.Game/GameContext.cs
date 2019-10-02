using System;
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

        public SceneManager SceneManager { get; }

        public RenderContext RenderContext => _gameLoop.RenderContext;

        public bool IsRunning => _gameLoop.IsRunning;

        public event Action GameStart;

        public event Action GameStop;

        /// <summary>
        /// Begins the game thread.
        /// </summary>
        public void Start()
        {
            _gameLoop.Start();
            OnStart();
        }

        /// <summary>
        /// Stops the game thread.
        /// </summary>
        public void Stop()
        {
            _gameLoop.Stop();
            OnStop();
        }

        protected virtual void OnStart()
        {
            GameStart?.Invoke();
        }

        protected virtual void OnStop()
        {
            GameStop?.Invoke();
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
