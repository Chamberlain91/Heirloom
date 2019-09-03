using System.IO;

using Heirloom.Drawing;

namespace Heirloom.Runtime.Loaders
{
    public sealed class FontLoader : AssetLoader<Font>
    {
        protected override bool Load(string identifier, Stream stream, out Font asset)
        {
            asset = new Font(stream);
            return true;
        }
    }
}
