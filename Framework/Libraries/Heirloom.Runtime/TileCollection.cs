using System;
using System.Collections;
using System.Collections.Generic;
using Heirloom.Math;

namespace Heirloom.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Returned from a TSX/Tiled loader</remarks>
    public class TileCollection : IReadOnlyDictionary<int, Tile>
    {
        private readonly Dictionary<int, Tile> _tiles;

        public TileCollection(int tileWidth, int tileHeight)
        {
            _tiles = new Dictionary<int, Tile>();

            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public Tile this[int identifier] => _tiles[identifier];

        public int Count => _tiles.Count;

        public int TileWidth { get; }

        public int TileHeight { get; }

        public IEnumerable<int> Identifiers => _tiles.Keys;

        public IEnumerable<Tile> Tiles => _tiles.Values;

        public void Add(int identifier, Tile tile)
        {
            if (!IsValid(tile)) { throw new ArgumentException($"Tile must be a {TileWidth}x{TileHeight} format tile."); }
            if (Contains(identifier)) { throw new ArgumentException($"Duplicate key, sprite '{identifier}' already exists the {nameof(TileCollection)}."); }

            // Add tile to map
            _tiles[identifier] = tile;
        }

        private bool IsValid(Tile tile)
        {
            return tile.Width == TileWidth && tile.Height == TileHeight;
        }

        public Tile Get(int identifier)
        {
            if (!TryGet(identifier, out var tile))
            {
                throw new KeyNotFoundException($"Unable to find sprite named '{identifier}' in the {nameof(TileCollection)}");
            }

            return tile;
        }

        public bool TryGet(int identifier, out Tile tile)
        {
            return _tiles.TryGetValue(identifier, out tile);
        }

        public bool Remove(int identifier)
        {
            return _tiles.Remove(identifier);
        }

        public bool Contains(int identifier)
        {
            return _tiles.ContainsKey(identifier);
        }

        public Sprite CreateSprite(IEnumerable<int> frames, Vector origin = default)
        {
            var sprite = new Sprite();

            // 
            foreach (var tile in frames)
            {
                var image = Get(tile).Sprite.Frames[0].Image;
                sprite.Add(image);
            }

            // 
            sprite.Origin = origin;

            return sprite;
        }

        #region IReadOnlyDictionary

        bool IReadOnlyDictionary<int, Tile>.ContainsKey(int key)
        {
            return _tiles.ContainsKey(key);
        }

        bool IReadOnlyDictionary<int, Tile>.TryGetValue(int key, out Tile value)
        {
            return _tiles.TryGetValue(key, out value);
        }

        IEnumerable<int> IReadOnlyDictionary<int, Tile>.Keys => _tiles.Keys;

        IEnumerable<Tile> IReadOnlyDictionary<int, Tile>.Values => _tiles.Values;

        IEnumerator<KeyValuePair<int, Tile>> IEnumerable<KeyValuePair<int, Tile>>.GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }

        #endregion
    }
}
