using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Game.Desktop;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    internal class PlatformerGame : DesktopGameContext
    {
        public PlatformerGame()
            : base("Example Game", MultisampleQuality.Low)
        { }

        protected override void GameStart()
        {
            Scene.AddEntity(new Camera());
            Scene.AddEntity(new Player());
            Scene.AddEntity(new Map(16, 4, 64)); // 64x64 sized tiles

            // 
            var map = Scene.GetEntity<Map>();
            for (var i = 0; i < map.Width; i++)
            {
                map.GetTile(i, map.Height - 1).Image = GetAsset<Image>("tile.dirt");
            }

            for (var i = 0; i < map.Height; i++)
            {
                map.GetTile(0, i).Image = GetAsset<Image>("tile.dirt");
                map.GetTile(map.Width - 1, i).Image = GetAsset<Image>("tile.dirt");
            }

            map.GetTile(2, 1).Image = GetAsset<Image>("tile.dirt");

            // Position player over the top center of the map
            var player = Scene.GetEntity<Player>();
            player.Transform.Position = (map.Width * map.TileSize / 2F, 0);

            // Make camera follow player
            var camera = Scene.GetEntity<Camera>();
            camera.AddComponent(new SmoothFollow(player.Transform));
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
                // Tiles
                { "tile.dirt", "data/tiles/platformPack_tile001.png" }
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

            // Set each frame of the player sprite to center origin
            foreach (var frame in GetAsset<Sprite>("player").Frames)
            {
                frame.Image.Origin = frame.Image.Bounds.Center;
            }
        }

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var game = new PlatformerGame();
                game.Start();
            });
        }
    }
}
