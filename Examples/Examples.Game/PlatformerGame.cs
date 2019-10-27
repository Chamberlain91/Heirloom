using System.Linq;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Game.Desktop;
using Heirloom.IO;
using Heirloom.Math;

using static Heirloom.Game.AssetDatabase;

namespace Examples.Game
{
    internal class PlatformerGame : DesktopGameContext
    {
        public PlatformerGame()
            : base("Example Game")
        {
            RenderContext.ShowFPSOverlay = true;
        }

        protected override void GameStart()
        {
            Scene.AddEntity(new Camera());
            var player = Scene.AddEntity(new Player());

            // Position player over the top center of the map

            // Load map from text file
            var mapData = Files.ReadText("data/level.txt");

            // 
            var mapRows = mapData.Split("\n").Select(s => s.Trim()).ToArray();
            var map = Scene.AddEntity(new Map(mapRows.Max(s => s.Length), mapRows.Length, 64));

            var y = 0;
            foreach (var row in mapRows)
            {
                for (var x = 0; x < map.Width; x++)
                {
                    var tile = map.GetTile(x, y);
                    var tileCenterOffset = new Vector(map.TileSize / 2F, map.TileSize / 2F);
                    var tilePosition = new Vector(x * map.TileSize, y * map.TileSize);

                    switch (row[x])
                    {
                        case '!':
                            player.Physics.Position = tileCenterOffset + tilePosition;
                            break;

                        case 'B':
                            var crate = Scene.AddEntity(new Crate());
                            crate.Physics.Position = tilePosition;
                            break;

                        case '#':
                            tile.Images.Add(GetAsset<Image>("tile.dirt-top"));
                            tile.IsSolid = true;
                            break;

                        case '$':
                            tile.Images.Add(GetAsset<Image>("tile.dirt"));
                            tile.IsSolid = true;
                            break;

                        case 'H':
                            tile.Images.Add(GetAsset<Image>("tile.ladder"));
                            tile.IsClimbable = true;
                            break;

                        case '~':
                            tile.Images.Add(GetAsset<Image>("tile.ladder-top"));
                            tile.IsClimbable = true;
                            break;

                        case '&':
                            tile.Images.Add(GetAsset<Image>("tile.brick"));
                            tile.IsSolid = true;
                            break;

                        case '.':
                            tile.Images.Add(GetAsset<Image>("tile.grass"));
                            break;
                    }
                }

                y++;
            }

            // Create colliders for solid tiles
            map.GenerateColliders();

            // Make camera follow player
            var camera = Scene.GetEntity<Camera>();
            camera.BackgroundColor = Color.Parse("3498DB");
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
                { "tile.crate", "data/tiles/platformPack_tile047.png" },
                { "tile.dirt", "data/tiles/platformPack_tile004.png" },
                { "tile.brick", "data/tiles/platformPack_tile040.png" },
                { "tile.dirt-top", "data/tiles/platformPack_tile001.png" },
                { "tile.grass", "data/tiles/platformPack_tile045.png" },
                { "tile.ladder", "data/tiles/platformPack_tile038.png" },
                { "tile.ladder-top", "data/tiles/platformPack_tile037.png" },
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
