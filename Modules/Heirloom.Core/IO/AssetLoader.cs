namespace Heirloom.IO
{
    /// <summary>
    /// Abstract class for implementing additional loading via <see cref="Assets"/>.
    /// </summary>
    public abstract class AssetLoader<T> : Assets.IAssetLoader
    {
        /// <summary>
        /// This method tries to load the specified asset.
        /// It should return false when unable to load.
        /// </summary>
        /// <param name="path">The path to asset.</param>
        /// <param name="item">Ouputs the asset, if loaded successfully.</param>
        /// <returns>True if successfully loaded asset.</returns>
        protected internal abstract bool TryLoad(string path, out T item);

        bool Assets.IAssetLoader.TryLoad(string path, out object asset)
        {
            var success = TryLoad(path, out var typedAsset);
            asset = typedAsset;
            return success;
        }
    }
}
