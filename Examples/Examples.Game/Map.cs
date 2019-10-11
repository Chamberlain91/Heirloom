﻿using System;
using System.Collections.Generic;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Math;

namespace Examples.Game
{
    public partial class Map : Entity
    {
        private readonly TileMapRenderer _mapRenderer;
        private readonly Tile[] _tiles;

        public Map(int width, int height, int tileSize)
        {
            Width = width;
            Height = height;
            TileSize = tileSize;

            // 
            _tiles = new Tile[width * height];
            for (var i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = new Tile();
            }

            // 
            AddComponent(_mapRenderer = new TileMapRenderer(this));
            _mapRenderer.Depth = -1; // Push behind most things
        }

        public int Width { get; }

        public int Height { get; }

        public int TileSize { get; }

        public IReadOnlyList<Tile> Tiles => _tiles;

        public Tile GetTile(int x, int y)
        {
            var i = (y * Width) + x;
            return Tiles[i];
        }

        public IEnumerable<Rectangle> GetCollisionBounds(int x, int y)
        {
            if (IsSolid(x, y)) { yield return GetCollider(x, y); }

            // Floor
            if (IsSolid(x, y + 1)) { yield return GetCollider(x, y + 1); }
            if (IsSolid(x - 1, y + 1)) { yield return GetCollider(x - 1, y + 1); }
            if (IsSolid(x + 1, y + 1)) { yield return GetCollider(x + 1, y + 1); }

            // Walls
            if (IsSolid(x - 1, y)) { yield return GetCollider(x - 1, y); }
            if (IsSolid(x + 1, y)) { yield return GetCollider(x + 1, y); }

            // Ceiling
            if (IsSolid(x, y - 1)) { yield return GetCollider(x, y - 1); }
            if (IsSolid(x + 1, y - 1)) { yield return GetCollider(x + 1, y - 1); }
            if (IsSolid(x - 1, y - 1)) { yield return GetCollider(x - 1, y - 1); }
        }

        private Rectangle GetCollider(int x, int y)
        {
            return new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);
        }

        private bool IsSolid(int x, int y)
        {
            if (IsValid(x, y))
            {
                var tile = GetTile(x, y);
                return tile.Image != null;
            }
            else
            {
                // Not on the map, not solid
                return false;
            }
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public class Tile
        {
            public Image Image { get; set; }
        }

        private class TileMapRenderer : DrawableComponent
        {
            public readonly Map Map;

            public TileMapRenderer(Map tileMap)
            {
                Map = tileMap;
            }

            protected override void Draw(RenderContext ctx)
            {
                for (var i = 0; i < Map.Tiles.Count; i++)
                {
                    var tile = Map.Tiles[i];

                    var x = i % Map.Width;
                    var y = i / Map.Width;

                    if (tile.Image != null)
                    {
                        // 
                        var trx = Matrix.CreateTranslation(x * Map.TileSize, y * Map.TileSize);
                        ctx.DrawImage(tile.Image, Transform.Matrix * trx);
                    }
                }

                // Visualize player bounds
                var player = Scene.GetEntity<Player>();
                var playerCollider = player.GetCollisionBounds();

                ctx.Color = Color.Orange;
                ctx.DrawRectOutline(playerCollider.Inflate(8), 2);

                // Draw map tiles
                var mapCoord = (IntVector) (player.Transform.Position / Map.TileSize);
                foreach (var tileCollider in Map.GetCollisionBounds(mapCoord.X, mapCoord.Y))
                {
                    ctx.Color = Color.Green;
                    ctx.DrawRectOutline(tileCollider, 2);
                }

                ctx.Color = Color.White;
            }
        }
    }
}
