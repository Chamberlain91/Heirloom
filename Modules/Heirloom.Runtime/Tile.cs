using System;

using Heirloom.Drawing;

namespace Heirloom.Runtime
{
    public class Tile
    {
        public Tile(Image image, int width, int height)
            : this(new Sprite { image }, width, height)
        { }

        public Tile(Sprite sprite, int width, int height)
        {
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));

            Width = width;
            Height = height;
        }

        public Sprite Sprite { get; }

        public int Width { get; }

        public int Height { get; }

        // todo: colliders, etc?
    }
}
