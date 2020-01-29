using System.IO;

namespace Heirloom.Game
{
    public abstract class AssetLoader
    {
        internal AssetLoader() { }

        internal abstract object Load(Stream stream);
    }

    public abstract class AssetLoader<TAsset> : AssetLoader
    {
        protected abstract TAsset LoadAsset(Stream stream);

        internal override object Load(Stream stream)
        {
            return LoadAsset(stream);
        }
    }
}
