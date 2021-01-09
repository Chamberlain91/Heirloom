using System;
using System.Collections.Generic;
using System.IO;

namespace Heirloom.IO
{
    public abstract class FileSystem
    {
        public abstract IEnumerable<string> EnumerateFiles(string pattern = null);

        public abstract Stream OpenStream(string path);

        public abstract bool Exists(string path);

        protected static string CheckAndResolvePath(string directory)
        {
            // Resolve path and ensure it doesn't go beyond root
            directory = Files.NormalizePath(directory);
            if (directory.StartsWith("..")) { throw new InvalidOperationException("Unable to resolve path higher than file system root."); }
            return directory;
        }
    }
}
