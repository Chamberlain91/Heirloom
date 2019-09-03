using System.IO;
using System.Linq;
using System.Xml.Linq;

using Heirloom.Drawing;
using Heirloom.IO;

namespace Heirloom.Runtime.Loaders
{
    public class TiledSetLoader : AssetLoader<TileCollection>
    {
        protected override bool Load(string identifier, Stream stream, out TileCollection asset)
        {
            asset = Read(identifier, stream.ParseXML("tileset"));
            return true;
        }

        internal static TileCollection Read(string identifier, XElement xml)
        {
            var name = xml.GetString("name");

            // 
            var tileWidth = xml.GetInteger("tilewidth");
            var tileHeight = xml.GetInteger("tileheight");
            var spacing = xml.GetInteger("spacing");
            var margin = xml.GetInteger("margin");
            var tileCount = xml.GetInteger("tilecount");
            var columns = xml.GetInteger("columns");

            // 
            var collection = new TileCollection(tileWidth, tileHeight);

            // Load base image (if defined)
            var xImage = xml.Element("image");
            var baseImage = xImage != null ? ReadImage(identifier, xImage) : null;

            // 
            var tileElements = xml.Elements("tile");
            if (tileElements.Any())
            {
                // For Each Tile
                foreach (var xElement in tileElements)
                {
                    // Get the tile id
                    var id = xElement.GetInteger("id");
                    Image image;

                    // Collection of images (each tile is its own image)
                    if (baseImage == null)
                    {
                        // Load image
                        image = ReadImage(identifier, xElement.Element("image"));
                    }
                    // Based on image (each tile is a region of an image)
                    else
                    {
                        // Construct sub-image
                        var x = id % columns * (tileWidth + spacing) + margin;
                        var y = id / columns * (tileHeight + spacing) + margin;

                        image = new Image(baseImage, (x, y, tileWidth, tileHeight));
                    }

                    // TODO: Animation?
                    // TODO: Object Group?

                    // Save tile
                    collection.Add(id, new Tile(image, tileWidth, tileHeight));
                }
            }
            else
            {
                // No tile elements, a plain slice of the grid
                for (var id = 0; id < tileCount; id++)
                {
                    // Construct sub-image
                    var x = id % columns * (tileWidth + spacing) + margin;
                    var y = id / columns * (tileHeight + spacing) + margin;

                    var image = new Image(baseImage, (x, y, tileWidth, tileHeight));

                    // TODO: Animation?
                    // TODO: Object Group?

                    // Save tile
                    collection.Add(id, new Tile(image, tileWidth, tileHeight));
                }
            }

            // If a tileset made of multiple images
            if (baseImage == null)
            {
                // Combine tiles into a single atlas image (for performance considerations)
                // This will duplicate data if two tilesets use the same source image
                Image.CreateAtlas(collection.Tiles.SelectMany(t => t.Sprite.Select(s => s.Image)));
            }

            return collection;
        }

        internal static Image ReadImage(string identifier, XElement xml)
        {
            var relative = xml.GetString("source");
            var other = AssetManifest.GetRelativeAsset(identifier, relative);
            return Assets.Get<Image>(other);
        }
    }
}
