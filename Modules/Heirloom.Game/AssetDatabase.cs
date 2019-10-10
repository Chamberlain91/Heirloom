using System;
using System.Collections.Generic;

using Heirloom.IO;

namespace Heirloom.Game
{
    public static class AssetDatabase
    {
        private static readonly Dictionary<Type, AssetLoader> _loaders = new Dictionary<Type, AssetLoader>();
        private static readonly Dictionary<Type, Dictionary<string, object>> _assetMaps = new Dictionary<Type, Dictionary<string, object>>();

        public static void RegisterAssetLoader<TAsset>(AssetLoader<TAsset> loader)
        {
            var type = typeof(TAsset);
            _loaders[type] = loader;
        }

        public static void RegisterAssetLoader<TAsset, TLoader>() where TLoader : AssetLoader<TAsset>, new()
        {
            RegisterAssetLoader(new TLoader());
        }

        public static void LoadAsset(Type type, string identifier, string path)
        {
            using (var stream = Files.OpenStream(path))
            {
                // Do we have a loader for this type?
                if (_loaders.TryGetValue(type, out var loader))
                {
                    // Get the type generic implementation of the loader and load the asset!
                    var asset = loader.Load(stream);

                    // Store loaded asset
                    var storage = GetAssetStorage(type);
                    storage.Add(identifier, asset);
                }
                else
                {
                    throw new InvalidOperationException($"Unable to load an asset, no known loader for type '{type.Name}'.");
                }
            }
        }

        public static TAsset LoadAsset<TAsset>(string identifier, string path)
        {
            LoadAsset(typeof(TAsset), identifier, path);
            return GetAsset<TAsset>(identifier);
        }

        public static void AddAsset<TAsset>(string identifier, TAsset asset)
        {
            var storage = GetAssetStorage(typeof(TAsset));
            storage.Add(identifier, asset);
        }

        public static bool HasLoadedAsset<TAsset>(string identifier, TAsset asset)
        {
            var storage = GetAssetStorage(typeof(TAsset));
            return storage.ContainsKey(identifier);
        }

        public static bool TryGetAsset<TAsset>(string identifier, out TAsset asset)
        {
            var storage = GetAssetStorage(typeof(TAsset));

            // Try to get a previously loaded asset
            if (storage.TryGetValue(identifier, out var obj))
            {
                // Found asset, cast and return success
                asset = (TAsset) obj;
                return true;
            }

            // Could not find asset, return failure
            asset = default;
            return false;
        }

        public static TAsset GetAsset<TAsset>(string identifier)
        {
            var storage = GetAssetStorage(typeof(TAsset));
            return (TAsset) storage[identifier];
        }

        public static IEnumerable<TAsset> GetAssets<TAsset>(params string[] identifiers)
        {
            foreach (var identifier in identifiers)
            {
                yield return GetAsset<TAsset>(identifier);
            }
        }

        public static void UnloadAsset<TAsset>(string identifier)
        {
            var storage = GetAssetStorage(typeof(TAsset));
            storage.Remove(identifier);
        }

        private static Dictionary<string, object> GetAssetStorage(Type type)
        {
            if (_assetMaps.TryGetValue(type, out var lookup) == false)
            {
                _assetMaps[type] = lookup = new Dictionary<string, object>();
            }

            return lookup;
        }
    }
}
