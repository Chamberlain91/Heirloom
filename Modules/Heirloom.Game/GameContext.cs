using System;

using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class GameContext
    {
        private readonly RenderLoop _loop;
        private bool _hasInitialized;

        protected GameContext(RenderContext ctx)
        {
            _loop = RenderLoop.Create(ctx, Scene.Update);
            _hasInitialized = false;
        }

        #region Properties

        /// <summary>
        /// Gets the currently running game context instance.
        /// </summary>
        /// <seealso cref="Start"/>
        /// <seealso cref="Stop"/>
        public static GameContext Instance { get; private set; }

        /// <summary>
        /// Gets the render context.
        /// </summary>
        public RenderContext RenderContext => _loop.RenderContext;

        /// <summary>
        /// Gets a value determining if the game loop is running.
        /// </summary>
        public bool IsRunning => _loop.IsRunning;

        #endregion

        #region Start / Stop

        /// <summary>
        /// Begins the game loop.
        /// </summary>
        public void Start()
        {
            if (IsRunning) { throw new InvalidOperationException($"An game instance already has been started. Unable to run two instances."); }

            // Store 
            Instance = this;

            if (!_hasInitialized)
            {
                _hasInitialized = true;
                Initialize();
            }

            _loop.Start();
        }

        /// <summary>
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            _loop.Stop();
            Instance = null;
        }

        #endregion

        protected abstract void Initialize();
    }
}
