using System;
using System.IO;

namespace Heirloom.Runtime
{
    public abstract class AssetLoader
    {
        internal AssetLoader() { }

        internal abstract Type AssetType { get; }
    }

    public abstract class AssetLoader<TAsset> : AssetLoader
    {
        internal override Type AssetType => typeof(TAsset);

        internal bool Load(string identifier, out TAsset asset)
        {
            using (var stream = AssetManifest.Open(identifier))
            {
                return Load(identifier, stream, out asset);
            }
        }

        protected abstract bool Load(string identifier, Stream stream, out TAsset asset);
    }
}
