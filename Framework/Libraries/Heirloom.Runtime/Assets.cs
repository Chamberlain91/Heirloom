using System;
using System.Collections.Generic;

namespace Heirloom.Runtime
{
    public static class Assets
    {
        private static Dictionary<string, object> _assets;

        internal static void Initialize()
        {
            _assets = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets the asset by identifier, will load on demand if not already in memory.
        /// </summary>
        public static T Get<T>(string identifier)
        {
            // Try to get asset
            if (_assets.TryGetValue(identifier, out var asset))
            {
                // Already loaded, return
                return (T) asset;
            }
            else
            {
                // Load the asset
                Load<T>(identifier);

                // Return from asset map
                return (T) _assets[identifier];
            }
        }

        /// <summary>
        /// Loads the asset into memory (if not already).
        /// </summary>
        public static void Load<T>(string identifier)
        {
            // Not loaded, we must load now
            if (!_assets.ContainsKey(identifier))
            {
                // Do we know about this asset?
                if (AssetManifest.Contains(identifier))
                {
                    // Look at each loader and attempt to load with that loader
                    foreach (var loader in AssetManifest.GetLoaders<T>())
                    {
                        if (loader.Load(identifier, out var asset))
                        {
                            // Store asset and exit function
                            _assets[identifier] = asset;
                            return;
                        }
                    }

                    // Failed to load, no loader succeeded
                    throw new InvalidOperationException($"Unable to load '{identifier}', all loaders failed or skipped loading asset.");
                }
                else
                {
                    // No known asset, unable to load
                    throw new InvalidOperationException($"Unable to load '{identifier}', asset not found.");
                }
            }
            else
            {
                // Already loaded, nothing to do!
            }
        }

        /// <summary>
        /// Unloads the asset from memory (releasing from reference in the database).
        /// </summary>
        public static void Unload(string identifier)
        {
            // todo: do we want to be more pendantic about dispose, memory, etc?
            // Remove asset from dictionary, so it is no longer referenced by the asset dictionary.
            // As long as it is not referenced by the game somewhere else, it will eventually be collected
            _assets.Remove(identifier);
        }
    }
}
