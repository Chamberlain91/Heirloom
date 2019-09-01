using System.IO;

namespace Heirloom.IO
{
    /// <summary>
    /// A utility to unify access of embedded files or files on disk. <para/>
    /// Mimicks operations such as <see cref="File.OpenRead(string)"/> as <see cref="OpenStream(string)"/>,
    /// but also considers paths discoverable by <see cref="EmbeddedFile"/>.
    /// </summary>
    public static class Files
    {
        /// <summary>
        /// Opens a read-only stream to an file, first by disk then by <see cref="EmbeddedFile"/>.
        /// </summary>
        public static Stream OpenStream(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new System.ArgumentException("Unable to open stream, null or blank path.", nameof(path));
            }

            // First try files on disk
            if (File.Exists(path))
            {
                // Open file from disk for reading
                return new FileStream(path, FileMode.Open);
            }
            // Not on disk, try embedded files
            else if (EmbeddedFile.Exists(path))
            {
                // Open embedded file for reading
                return EmbeddedFile.OpenStream(path);
            }
            else
            {
                // Unable to find file!
                throw new FileNotFoundException($"Unable to find file.", path);
            }
        }

        #region Convenience (ReadText, etc)

        public static string ReadText(string path)
        {
            using (var stream = OpenStream(path))
            {
                return stream.ReadAllText();
            }
        }

        public static byte[] ReadBytes(string path)
        {
            using (var stream = OpenStream(path))
            {
                return stream.ReadAllBytes();
            }
        }

        #endregion
    }
}
