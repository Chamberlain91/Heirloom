using System;

using Heirloom.Math;
using Heirloom.Runtime;

namespace OneBitHero
{
    public class MainScene : Scene
    {
        protected override void Enter()
        {
            Console.WriteLine($"Scene Enter");

            //
            foreach (var identifier in AssetManifest.Identifiers)
            {
                Console.WriteLine(identifier);
            }

            // Configure camera
            Camera.Color = Colors.Background;
            Camera.Scale = 1 / 2F; // 2x zoom

            // Construct player entity
            AddEntity(new Player());

            var tileset = Assets.Get<TileCollection>("tilemap/tileset_colored.tsx");

            // Create a dummy entity that renders the map data
            var map = AddEntity(new Entity());
            var mapRenderer = map.AddComponent<TileGridRenderer>();
            mapRenderer.Grid = TileGrid.CreateGrid(tileset, _mapData, 48);

            // Position map about center to make it visible until collisions are
            // implemented suitably for a tile map for the player to run about on.
            map.Transform.Position = new Vector(mapRenderer.Grid.PixelWidth, mapRenderer.Grid.PixelHeight) * -0.5F;

            // 
            var tileBoxShape = new Polygon(new[] {
                new Vector(-8, -8),
                new Vector(+8, -8),
                new Vector(+8, +8),
                new Vector(-8, +8),
            });

            var tileBoxThinShape = new Polygon(new[] {
                new Vector(-8, -8),
                new Vector(+8, -8),
                new Vector(+8, +0),
                new Vector(-8, +0),
            });

            var tileSlopeTRShape = new Polygon(new[] {
                new Vector(-8, -8),
                new Vector(+8, +8),
                new Vector(-8, +8),
            });

            var tileSlopeTLShape = new Polygon(new[] {
                new Vector(+8, -8),
                new Vector(+8, +8),
                new Vector(-8, +8),
            });

            for (var y = 0; y < mapRenderer.Grid.Height; y++)
            {
                for (var x = 0; x < mapRenderer.Grid.Width; x++)
                {
                    var tile = mapRenderer.Grid[x, y];

                    if (tile != null && IsSolidTile(tile, out var type))
                    {
                        var tileEntity = AddEntity(new Entity());
                        tileEntity.Transform.Position = new Vector(8 + x * 16, 8 + y * 16) + map.Transform.Position;

                        var tileCollider = tileEntity.AddComponent<Collider>();
                        switch (type)
                        {
                            case 0:
                                tileCollider.Shape = tileBoxShape;
                                break;

                            case 1:
                                tileCollider.Shape = tileBoxThinShape;
                                break;

                            case 2:
                                tileCollider.Shape = tileSlopeTRShape;
                                break;

                            case 3:
                                tileCollider.Shape = tileSlopeTLShape;
                                break;
                        }
                    }
                }
            }

            bool IsSolidTile(Tile tile, out int type)
            {
                type = 0;

                // Box Collider
                if (tile == tileset.Get(18)) { return true; } // top-left wall
                if (tile == tileset.Get(19)) { return true; } // top wall
                if (tile == tileset.Get(20)) { return true; } // top-right wall
                if (tile == tileset.Get(50)) { return true; } // left wall
                if (tile == tileset.Get(52)) { return true; } // right wall

                // if (tile == tileset.Get(182)) { return true; } // spring
                if (tile == tileset.Get(87)) { return true; }  // box

                type = 1;

                // Thin Box Collider
                if (tile == tileset.Get(213)) { return true; } // floating platform left
                if (tile == tileset.Get(214)) { return true; } // floating platform middle
                if (tile == tileset.Get(215)) { return true; } // floating platform right
                if (tile == tileset.Get(178)) { return true; } // fall trap platform

                type = 2;

                if (tile == tileset.Get(117)) { return true; } // top-right slope

                type = 3;

                if (tile == tileset.Get(116)) { return true; } // top-left slope

                type = -1;

                return false;
            }
        }

        protected override void Exit()
        {
            Console.WriteLine($"Scene Exit");
        }

        private static readonly int[] _mapData = new int[] {
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,0,0,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,0,0,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,0,0,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,0,0,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,86,0,0,54,0,0,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,0,0,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,0,151,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,0,87,0,0,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,54,0,0,54,213,214,214,215,0,0,0,0,0,0,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,150,54,0,0,54,0,0,0,0,0,0,0,0,0,0,0,86,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,87,54,0,0,54,0,0,0,0,0,0,0,245,0,305,0,54,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,18,19,19,19,117,0,54,0,0,0,0,0,0,0,213,214,215,0,54,0,0,0,0,150,150,85,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,52,0,86,0,0,0,279,0,85,182,0,0,0,0,86,0,150,0,18,19,19,19,19,20,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,51,0,0,52,0,54,0,0,0,18,19,19,20,0,0,0,0,54,0,150,0,50,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,52,0,86,150,0,0,50,0,0,52,0,0,0,307,87,85,0,85,50,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,52,0,54,85,0,116,147,0,0,52,178,178,178,18,19,19,19,19,147,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,51,146,19,19,19,19,147,0,0,0,52,0,0,0,50,0,0,0,0,0,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,85,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,52,0,0,0,50,0,0,0,0,0,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,18,19,19,19,147,0,0,0,0,0,0,0,51,0,0,0,0,52,22,22,22,50,0,0,0,0,0,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,146,19,19,19,147,0,0,0,0,0,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,51,0,0,0,0,0,0,0,0,0,0,0,0,51,0,0,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,18,19,19,19,147,0,0,0,0,0,0,0,0,0,0,0,0,0,51,0,0,0,0,0,0,0,0,51,0,0,51,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,52,0,0,0,0,
            0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,52,0,0,0,0 };
    }
}
