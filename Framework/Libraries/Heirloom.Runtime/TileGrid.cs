using System;

namespace Heirloom.Runtime
{
    public class TileGrid
    {
        private readonly Tile[,] _tiles;

        public TileGrid(int width, int height, int tileWidth, int tileHeight)
        {
            Width = width;
            Height = height;

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            _tiles = new Tile[Height, Width];
        }

        public int Width { get; }

        public int Height { get; }

        public int TileWidth { get; }

        public int TileHeight { get; }

        public int PixelWidth => Width * TileWidth;

        public int PixelHeight => Height * TileHeight;

        public Tile this[int x, int y]
        {
            get => _tiles[y, x];

            set
            {
                if (!IsValid(value)) { throw new ArgumentException($"Tile must be a {TileWidth}x{TileHeight} sized tile."); }
                _tiles[y, x] = value;
            }
        }

        private bool IsValid(Tile tile)
        {
            return tile.Width == TileWidth && tile.Height == TileHeight;
        }

        public static TileGrid CreateGrid(TileCollection collection, int[] data, int width)
        {
            if (data.Length % width != 0) { throw new ArgumentException($"Input data must be a multiple of the grid width.", nameof(data.Length)); }

            var height = data.Length / width;

            var grid = new TileGrid(width, height, collection.TileWidth, collection.TileHeight);

            // 
            for (int y = 0, i = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++, i++)
                {
                    var index = data[i];

                    if (index == 0) { continue; }

                    // Assign tile from collection to grid.
                    grid[x, y] = collection[index];
                }
            }

            return grid;
        }

        public static TileGrid CreateGrid(TileCollection collection, int[,] data)
        {
            var width = data.GetLength(0);
            var height = data.GetLength(1);

            var grid = new TileGrid(width, height, collection.TileWidth, collection.TileHeight);

            // 
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var index = data[y, x];

                    if (index == 0) { continue; }

                    // Assign tile from collection to grid.
                    grid[x, y] = collection[index];
                }
            }

            return grid;
        }
    }
}
