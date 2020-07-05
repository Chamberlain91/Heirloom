using System;
using System.Collections.Generic;
using System.IO;

using Heirloom.Sound;

namespace Heirloom.IO
{
    /// <summary>
    /// Implements an asset load and cache system using <see cref="WeakReference"/>.
    /// </summary>
    public static class Assets
    {
        private static readonly Dictionary<string, WeakReference> _assets = new Dictionary<string, WeakReference>();
        private static readonly Dictionary<Type, Dictionary<Type, IAssetLoader>> _loaders = new Dictionary<Type, Dictionary<Type, IAssetLoader>>();

        static Assets()
        {
            RegisterLoader(new ImageLoader());
            RegisterLoader(new AudioClipLoader());
        }

        #region Loaders

        /// <summary>
        /// Regsiter a new asset loader.
        /// </summary>
        /// <typeparam name="T">Some asset type.</typeparam>
        /// <param name="loader">The loader.</param>
        public static void RegisterLoader<T>(AssetLoader<T> loader)
        {
            if (loader is null) { throw new ArgumentNullException(nameof(loader)); }

            // Get loader set for asset
            var assetType = typeof(T);
            if (_loaders.TryGetValue(assetType, out var loaders) == false)
            {
                _loaders[assetType] =
                            loaders = new Dictionary<Type, IAssetLoader>();
            }

            // Determine if a loader for this type already exists
            var loaderType = loader.GetType();
            if (loaders.ContainsKey(loaderType))
            {
                throw new InvalidOperationException("An instance of this loader type already exists.");
            }
            else
            {
                // Store loader in loader set
                loaders[loaderType] = loader;
            }
        }

        private static IEnumerable<AssetLoader<T>> GetLoaders<T>()
        {
            // Try to get known loaders for asset type
            if (_loaders.TryGetValue(typeof(T), out var loaders))
            {
                foreach (var loader in loaders.Values)
                {
                    yield return loader as AssetLoader<T>;
                }
            }
            else
            {
                throw new InvalidOperationException("No known loader for this asset type.");
            }
        }

        #endregion

        /// <summary>
        /// Checks if an asset with the specified identifier has been loaded.
        /// </summary>
        public static bool IsLoaded(string identifier)
        {
            // Try to get the asset
            if (_assets.TryGetValue(identifier, out var reference))
            {
                // Asset was loaded, now return if it is still alive
                return reference.IsAlive;
            }

            // Asset was never loaded or has been collected
            return false;
        }

        /// <summary>
        /// Attempts to retreive an asset with the specified identifier.
        /// </summary>
        /// <typeparam name="T">Some asset type.</typeparam>
        /// <param name="identifier">Some identifier representing an asset.</param>
        /// <param name="asset">Outputs the asset, if loaded.</param>
        /// <returns>True if the asset was retrieved successfully, otherwise false.</returns>
        /// <exception cref="InvalidCastException">Generic type mismatch, asset was not assignable to a value of <typeparamref name="T"/>.</exception>
        public static bool TryGet<T>(string identifier, out T asset)
        {
            // Try to get the asset
            if (_assets.TryGetValue(identifier, out var reference))
            {
                // Is the reference to the asset still valid?
                if (reference.IsAlive)
                {
                    // Is the reference to the asset still valid?
                    if (reference.Target is T _asset)
                    {
                        asset = _asset;
                        return true;
                    }
                    else
                    {
                        // The asset was loaded, but we are requesting the wrong type.
                        throw new InvalidCastException($"Unable to get asset '{identifier}', was not of type {typeof(T).Name}.");
                    }
                }
            }

            // Asset was never loaded or was collected
            asset = default;
            return false;
        }

        /// <summary>
        /// Retreives a loaded asset with the specified identifier.
        /// </summary>
        /// <typeparam name="T">Some asset type.</typeparam>
        /// <param name="identifier">Some identifier representing an asset.</param>
        /// <returns>Returns the associated asset.</returns>
        /// <exception cref="InvalidCastException">Generic type mismatch, asset was not assignable to a value of <typeparamref name="T"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the asset requested has not been loaded into memory.</exception>
        public static T Get<T>(string identifier)
        {
            if (TryGet<T>(identifier, out var value))
            {
                return value;
            }
            else
            {
                // Can't get an unloaded asset
                throw new InvalidOperationException($"Unable to get asset. Asset was not loaded.");
            }
        }

        /// <summary>
        /// Stores an asset with the specified identifier.
        /// </summary>
        /// <typeparam name="T">Some asset type.</typeparam>
        /// <param name="identifier">Some identifier representing an asset.</param>
        /// <exception cref="InvalidOperationException">Thrown when the identifier already exists has not been loaded into memory.</exception>
        public static void Set<T>(string identifier, T asset)
        {
            if (_assets.ContainsKey(identifier))
            {
                throw new InvalidOperationException($"Unable to store asset. An asset with the specified identifier already exists.");
            }
            else
            {
                _assets[identifier] = new WeakReference(asset);
            }
        }

        /// <summary>
        /// Loads an asset with inference by specified type and file extension. If the asset is already loaded, this simply returns it.
        /// </summary>
        /// <typeparam name="T">Some asset type.</typeparam>
        /// <param name="identifier">Some identifier to represent the resource.</param>
        /// <param name="path">Some path to a resource.</param>
        /// <returns>The loaded asset</returns>
        /// <exception cref="InvalidCastException">Thrown when the asset has been loaded, but the specified type is incorrect.</exception>
        /// <exception cref="NotImplementedException">Thrown when all asset loaders fail to load the resource.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the file at the specified path does not exist.</exception>
        public static T Load<T>(string identifier, string path)
        {
            // Try to get the asset
            if (TryGet<T>(identifier, out var asset))
            {
                // Was loaded already, so just return it!
                return asset;
            }
            else
            {
                // Was not loaded, so we must attempt to locate and load this asset.
                // Check if the file exists (this check will check for embedded and on disk).
                if (Files.Exists(path))
                {
                    // Try each known loader for the specified asset type.
                    foreach (var loader in GetLoaders<T>())
                    {
                        // Try to load the asset
                        if (loader.TryLoad(path, out asset))
                        {
                            Log.Debug($"Loaded: {path}");

                            // Asset loaded succesfully. We will store the asset
                            // with by the specified identifier.
                            Set(identifier, asset);
                            return asset;
                        }
                    }

                    // All loaders failed to load resource
                    throw new NotImplementedException($"Unable to load asset, no successful loader.");
                }
                else
                {
                    // Resource could not be found
                    throw new FileNotFoundException($"Unable to load asset, file not found.");
                }
            }
        }

        internal interface IAssetLoader
        {
            internal abstract bool TryLoad(string path, out object item);
        }

        private class ImageLoader : AssetLoader<Image>
        {
            protected internal override bool TryLoad(string path, out Image item)
            {
                if (path.EndsWith(".png") || path.EndsWith(".jpg") || path.EndsWith(".bmp"))
                {
                    // Can load this path
                    item = new Image(path);
                    return true;
                }
                else
                {
                    // Unable to load
                    item = default;
                    return false;
                }
            }
        }

        private class AudioClipLoader : AssetLoader<AudioClip>
        {
            protected internal override bool TryLoad(string path, out AudioClip item)
            {
                if (path.EndsWith(".wav") || path.EndsWith(".ogg") || path.EndsWith(".mp3"))
                {
                    // Can load this path
                    item = new AudioClip(path);
                    return true;
                }
                else
                {
                    // Unable to load
                    item = default;
                    return false;
                }
            }
        }
    }
}
