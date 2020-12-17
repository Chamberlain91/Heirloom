using System;
using System.Collections.Generic;
using System.IO;

namespace Meadows.IO
{
    internal sealed class PhysicalFileSystem : FileSystem
    {
        public string Root { get; private set; }

        public PhysicalFileSystem(string directory)
        {
            ChangeFileRoot(directory);
        }

        internal void ChangeFileRoot(string directory)
        {
            Root = Files.NormalizePath(directory);
            if (!Directory.Exists(Root))
            {
                throw new ArgumentException($"Unable to set file system root to '{directory}'.", nameof(directory));
            }
        }

        public override IEnumerable<string> EnumerateFiles(string pattern = null)
        {
            foreach (var file in Directory.EnumerateFiles(Root, "*.*", SearchOption.AllDirectories))
            {
                var path = Files.NormalizePath(file);

                if (pattern == null || path.IsLikePattern(pattern))
                {
                    yield return path;
                }
            }
        }

        public override Stream OpenStream(string path)
        {
            path = CheckAndResolvePath(path);
            path = Path.Combine(Root, path);

            return new FileStream(path, FileMode.Open);
        }

        public override bool Exists(string path)
        {
            path = CheckAndResolvePath(path);
            path = Path.Combine(Root, path);

            return Directory.Exists(path) || File.Exists(path);
        }
    }
}
