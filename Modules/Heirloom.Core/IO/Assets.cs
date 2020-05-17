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
        /// Loads an asset with inference by specified type and file extension.
        /// </summary>
        /// <typeparam name="T">Some asset type.</typeparam>
        /// <param name="path">Some path to a resource.</param>
        /// <returns>The loaded asset</returns>
        /// <exception cref="InvalidCastException">Thrown when the asset has been loaded, but the specified type is incorrect.</exception>
        /// <exception cref="NotImplementedException">Thrown when all asset loaders fail to load the resource.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the file at the specified path does not exist.</exception>
        public static T Load<T>(string path)
        {
            // Try to get the asset
            if (_assets.TryGetValue(path, out var reference))
            {
                if (reference.IsAlive)
                {
                    if (reference.Target is T asset)
                    {
                        // Asset was loaded and the correct type.
                        return asset;
                    }
                    else
                    {
                        // Asset was loaded, but we are requesting the wrong type.
                        throw new InvalidCastException($"Unable to return asset '{path}', was not of type {typeof(T).Name}.");
                    }
                }
                else
                {
                    // Reference was GC'd
                }
            }
            else
            {
                // Was never loaded
            }

            if (Files.Exists(path))
            {
                // Have to load the resource
                foreach (var loader in GetLoaders<T>())
                {
                    // ...try to load the resource
                    if (loader.TryLoad(path, out var asset))
                    {
                        Log.Debug($"Loaded: {path}");

                        // Asset loaded succesfully
                        _assets[path] = new WeakReference(asset);
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
