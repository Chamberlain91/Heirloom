using System.IO;

using Heirloom.Drawing;

namespace Heirloom.Runtime.Loaders
{
    public sealed class ImageLoader : AssetLoader<Image>
    {
        protected override bool Load(string identifier, Stream stream, out Image asset)
        {
            asset = new Image(stream);
            return true;
        }
    }
}
