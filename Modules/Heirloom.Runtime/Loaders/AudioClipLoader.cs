using System.IO;

using Heirloom.Sound;

namespace Heirloom.Runtime.Loaders
{
    public sealed class AudioClipLoader : AssetLoader<AudioClip>
    {
        protected override bool Load(string identifier, Stream stream, out AudioClip asset)
        {
            // Decodes entire audio file now
            asset = new AudioClip(stream);
            return true;
        }
    }
}
