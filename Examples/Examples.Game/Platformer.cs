using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Game.Desktop;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    internal class Platformer : DesktopGameContext
    {
        public Platformer()
            : base("Example Game", MultisampleQuality.Low)
        { }

        protected override void GameStart()
        {
            Scene.AddEntity(new Player());
        }

        protected override void GameLoad(LoadScreenProgress progress)
        {
            var manifest = new AssetLoadManifest
            {
                // Player
                { "player.idle", "data/characters/platformChar_idle.png" },
                { "player.jump", "data/characters/platformChar_jump.png" },
                { "player.happy", "data/characters/platformChar_happy.png" },
                { "player.duck", "data/characters/platformChar_duck.png" },
                { "player.walk1", "data/characters/platformChar_walk1.png" },
                { "player.walk2", "data/characters/platformChar_walk2.png" },
                { "player.climb1", "data/characters/platformChar_climb1.png" },
                { "player.climb2", "data/characters/platformChar_climb2.png" },
            };

            // Load!
            manifest.LoadAssets(progress);

            // Build player sprite
            var builder = new SpriteBuilder
            {
                { "idle", GetAsset<Image>("player.idle") },
                { "jump", GetAsset<Image>("player.jump") },
                { "happy", GetAsset<Image>("player.happy") },
                { "duck", GetAsset<Image>("player.duck") },
                { "walk", 0.1F, GetAssets<Image>("player.walk1", "player.walk2") },
                { "climb", 0.2F, GetAssets<Image>("player.climb1", "player.climb2") }
            };

            // Store player sprite
            AddAsset("player", builder.CreateSprite());
        }

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var game = new Platformer();
                game.Start();
            });
        }
    }
}
