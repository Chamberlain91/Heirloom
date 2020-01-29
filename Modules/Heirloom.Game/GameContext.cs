using System;
using System.IO;
using System.Threading.Tasks;
using Heirloom.Drawing;
using Heirloom.IO;

namespace Heirloom.Game
{
    public abstract class GameContext
    {
        private readonly RenderLoop _loop;
        private bool _isLoaded;

        protected GameContext(Graphics ctx)
        {
            _loop = RenderLoop.Create(ctx, Scene.Update);
            _isLoaded = false;
        }

        protected abstract void GameLoad(LoadScreenProgress progress);

        protected abstract void GameStart();

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
        public Graphics Graphics => _loop.Graphics;

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
            _loop.Start();

            if (!_isLoaded)
            {
                _isLoaded = true;

                // Launch on secondary thread to prevent locking up the window
                Task.Run(() =>
                {
                    Scene.ShowLoadScreen();
                    GameLoad(LoadScreen.Progress);
                    Scene.HideLoadScreen();
                    GameStart();
                })
                .ContinueWith((t) =>
                {
                    if (t.IsFaulted)
                    {
                        Console.WriteLine(t.Exception);
                        throw t.Exception;
                    }
                });
            }
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

        static GameContext()
        {
            AssetDatabase.RegisterAssetLoader<Image, ImageLoader>();
            AssetDatabase.RegisterAssetLoader<Sprite, SpriteLoader>();
        }

        private sealed class ImageLoader : AssetLoader<Image>
        {
            protected override Image LoadAsset(Stream stream)
            {
                return new Image(stream);
            }
        }

        private sealed class SpriteLoader : AssetLoader<Sprite>
        {
            protected override Sprite LoadAsset(Stream stream)
            {
                return new Sprite(stream);
            }
        }
    }
}
