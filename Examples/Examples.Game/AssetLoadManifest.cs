using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Heirloom.Drawing;
using Heirloom.Game;

namespace Examples.Game
{
    internal class AssetLoadManifest : IEnumerable
    {
        private readonly List<AssetEntry> _entries;

        public AssetLoadManifest()
        {
            _entries = new List<AssetEntry>();
        }

        public void Add(string identifier, string path)
        {
            var type = GetAutomaticType(path);
            // todo: prevent duplicates, exception?
            _entries.Add(new AssetEntry(type, identifier, path));
        }

        public void LoadAssets(LoadScreenProgress progress)
        {
            var i = 0;
            foreach (var entry in _entries)
            {
                // Update progress
                progress.Message = $"Loading {i} of {_entries.Count}\n{entry.Identifier}";

                progress.Percent = i / (float) _entries.Count;
                i++;

                // Load asset
                AssetDatabase.LoadAsset(entry.Type, entry.Identifier, entry.Path);
            }

            // Update final progress
            progress.Message = $"Loading Complete";
            progress.Percent = 1F;
        }

        private static Type GetAutomaticType(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".png":
                case ".jpg":
                    return typeof(Image);

                case ".ase":
                case ".aseprite":
                    return typeof(Sprite);

                default:
                    throw new InvalidOperationException("Unable to determine type from extension.");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        private struct AssetEntry
        {
            public Type Type;
            public string Identifier;
            public string Path;

            public AssetEntry(Type type, string identifier, string path)
            {
                Type = type;
                Identifier = identifier;
                Path = path;
            }
        }
    }
}
