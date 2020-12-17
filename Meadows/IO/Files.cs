using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Meadows.IO
{
    /// <summary>
    /// A utility to unify access of embedded files and files on disk. <para/>
    /// </summary>
    public static class Files
    {
        private static readonly Regex _slashRegex = new(@"[\\/]+", RegexOptions.Compiled);

        /// <summary>
        /// Normalizes a path (forward slades, removing doubles, etc)
        /// </summary>
        public static string NormalizePath(string path)
        {
            // Normalize to foward slash, removing doubles.
            path = _slashRegex.Replace(path, "/");

            // Collapse special path parts
            var resolved = new List<string>();
            foreach (var part in path.Split('/'))
            {
                // If encountering special 'current path' characters.
                if (part == "." && resolved.Count > 0) { continue; }
                // If encountering special 'parent path', try to remove the prior element.
                else if (part == ".." && resolved.Count > 0) { resolved.RemoveAt(resolved.Count - 1); }
                // Otherwise, just append to list
                else { resolved.Add(part); }
            }

            return string.Join('/', resolved);
        }

        /// <summary>
        /// Opens a read-only stream to a file, first found by disk, then by embedded files.
        /// </summary>
        public static Stream OpenStream(string path)
        {
            // No file, empty string
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Unable to open stream, null or blank path.", nameof(path));
            }
            // Check disk path first
            else if (File.Exists(path))
            {
                // Open file from disk for reading
                return new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            // Check embedded path next
            else if (EmbeddedFiles.Exists(path))
            {
                // Open embedded file for reading
                return EmbeddedFiles.OpenStream(path);
            }
            // No known file by this path
            else
            {
                throw new FileNotFoundException($"Unable to find file.", path);
            }
        }

        /// <summary>
        /// Checks if a file exists, first by disk, then by embedded files.
        /// </summary>
        public static bool Exists(string path)
        {
            // No file, empty string
            if (string.IsNullOrWhiteSpace(path)) { return false; }
            // Check disk path first
            else if (File.Exists(path)) { return true; }
            // Check embedded path next
            else if (EmbeddedFiles.Exists(path)) { return true; }
            // No known file by this path
            else { return false; }
        }

        /// <summary>
        /// Gets all known embedded files.
        /// </summary>
        public static EmbeddedFile[] GetEmbeddedFiles()
        {
            return EmbeddedFiles.GetFiles().ToArray();
        }

        /// <summary>
        /// Gets information about the embedded file.
        /// </summary>
        public static EmbeddedFile GetEmbeddedInfo(string path)
        {
            return EmbeddedFiles.GetFile(path);
        }

        #region Convenience (ReadText, etc)

        /// <summary>
        /// Reads all text in a given file.
        /// </summary>
        /// <param name="path">A path to a file or embedded identifier.</param>
        /// <returns>String contents of a file.</returns>
        /// <seealso cref="OpenStream(string)"/>
        public static string ReadText(string path)
        {
            using var stream = OpenStream(path);
            return stream.ReadAllText();
        }

        /// <summary>
        /// Reads all bytes in a given file.
        /// </summary>
        /// <param name="path">A path to a file or embedded identifier.</param>
        /// <returns>Raw byte contents of a file.</returns>
        /// <seealso cref="OpenStream(string)"/>
        public static byte[] ReadBytes(string path)
        {
            using var stream = OpenStream(path);
            return stream.ReadAllBytes();
        }

        #endregion
    }
}
