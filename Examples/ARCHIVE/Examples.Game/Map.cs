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

        private readonly List<Collider> _colliders;

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
            _colliders = new List<Collider>();

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

        //public IEnumerable<Rectangle> GetCollisionBounds(int x, int y)
        //{
        //    if (IsSolid(x, y)) { yield return ComputeCollisionBounds(x, y); }

        //    // Floor
        //    if (IsSolid(x, y + 1)) { yield return ComputeCollisionBounds(x, y + 1); }
        //    if (IsSolid(x - 1, y + 1)) { yield return ComputeCollisionBounds(x - 1, y + 1); }
        //    if (IsSolid(x + 1, y + 1)) { yield return ComputeCollisionBounds(x + 1, y + 1); }

        //    // Walls
        //    if (IsSolid(x - 1, y)) { yield return ComputeCollisionBounds(x - 1, y); }
        //    if (IsSolid(x + 1, y)) { yield return ComputeCollisionBounds(x + 1, y); }

        //    // Ceiling
        //    if (IsSolid(x, y - 1)) { yield return ComputeCollisionBounds(x, y - 1); }
        //    if (IsSolid(x + 1, y - 1)) { yield return ComputeCollisionBounds(x + 1, y - 1); }
        //    if (IsSolid(x - 1, y - 1)) { yield return ComputeCollisionBounds(x - 1, y - 1); }
        //}

        private Rectangle ComputeCollisionBounds(int x, int y)
        {
            return new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);
        }

        public void GenerateColliders()
        {
            // Remove any previous
            foreach (var collider in _colliders)
            {
                RemoveComponent(collider);
            }

            // 
            _colliders.Clear();

            //
            foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
            {
                var tile = GetTile(co.X, co.Y);

                if (tile.IsSolid)
                {
                    var bounds = ComputeCollisionBounds(co.X, co.Y);

                    var collider = AddComponent(new Collider(bounds));
                    _colliders.Add(collider);
                }
            }
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsNearClimable(Vector worldPosition)
        {
            var co = (IntVector) (worldPosition / TileSize);

            if (IsValid(co.X, co.Y))
            {
                var tile = GetTile(co.X, co.Y);
                return tile.IsClimbable;
            }

            return false;
        }

        public class Tile
        {
            public List<Image> Images { get; } = new List<Image>();

            public bool IsClimbable { get; set; }

            public bool IsSolid { get; set; }
        }

        private class TileMapRenderer : DrawableComponent
        {
            public readonly Map Map;

            public TileMapRenderer(Map tileMap)
            {
                Map = tileMap;
            }

            protected override void Draw(Graphics ctx)
            {
                for (var i = 0; i < Map.Tiles.Count; i++)
                {
                    var tile = Map.Tiles[i];

                    var x = i % Map.Width;
                    var y = i / Map.Width;

                    if (tile.Images.Count > 0)
                    {
                        // Compute transform
                        var trx = Matrix.CreateTranslation(x * Map.TileSize, y * Map.TileSize);
                        trx = Transform.Matrix * trx;

                        // Draw each image
                        foreach (var image in tile.Images)
                        {
                            ctx.DrawImage(image, trx);
                        }
                    }
                }
            }
        }
    }
}
