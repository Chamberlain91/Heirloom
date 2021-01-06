using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Meadows.IO
{
    /// <summary>
    /// A utility to unify access of embedded files, virtual files and files on disk. <para/>
    /// </summary>
    /// <remarks>Standardizes directory separators to UNIX-like.</remarks>
    public static class Files
    {
        private static readonly Regex _slashRegex = new Regex(@"[\\/]+", RegexOptions.Compiled);
        private static readonly HashSet<FileSystem> _fileSystems = new HashSet<FileSystem>();

        private static readonly PhysicalFileSystem _physicalFileSystem;

        static Files()
        {
            _physicalFileSystem = new PhysicalFileSystem(".");

            AddVirtualFileSystem(_physicalFileSystem);
            AddVirtualFileSystem(new EmbeddedFileSystem());
        }

        #region Virtual File System

        internal static void AddVirtualFileSystem(FileSystem fileSystem)
        {
            _fileSystems.Add(fileSystem);
            // todo: validate path conflicts?
        }

        internal static void ChangePhysicalFileRoot(string path)
        {
            _physicalFileSystem.ChangeFileRoot(path);
            // todo: validate path conflicts?
        }

        #endregion

        #region Path Manipulation Functions

        /// <summary>
        /// Splits the specified path at directory separators.
        /// </summary>
        public static string[] Split(string path)
        {
            if (path is null) { throw new ArgumentNullException(nameof(path)); }

            path = _slashRegex.Replace(path, "/");
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries /* | StringSplitOptions.TrimEntries */);
        }

        /// <summary>
        /// Joins two paths into one.
        /// </summary>
        public static string Join(string first, string second)
        {
            if (first is null) { throw new ArgumentNullException(nameof(first)); }
            if (second is null) { throw new ArgumentNullException(nameof(second)); }

            return NormalizePath($"{first}/{second}");
        }

        /// <summary>
        /// Joins multiple paths into one.
        /// </summary>
        public static string Join(string first, params string[] remaning)
        {
            if (first is null) { throw new ArgumentNullException(nameof(first)); }
            if (remaning is null) { throw new ArgumentNullException(nameof(remaning)); }

            var path = string.Join('/', remaning);
            return NormalizePath(path);
        }

        /// <summary>
        /// Joins multiple paths into one path.
        /// </summary>
        public static string Join(IEnumerable<string> parts)
        {
            if (parts is null) { throw new ArgumentNullException(nameof(parts)); }

            var path = string.Join('/', parts);
            return NormalizePath(path);
        }

        /// <summary>
        /// Normalizes a path (forward slades, removing doubles, etc)
        /// </summary>
        public static string NormalizePath(string path)
        {
            if (path is null) { throw new ArgumentNullException(nameof(path)); }

            // Normalize slashes
            path = _slashRegex.Replace(path, "/");
            var isRootPath = path.StartsWith('/');

            // Collapse special path parts
            var resolved = new List<string>();
            foreach (var part in Split(path))
            {
                // If encountering special 'current path' characters.
                if (part == ".") { continue; }
                // If encountering special 'parent path', try to remove the prior element.
                else if (part == ".." && resolved.Count > 0 && resolved[^1] != "..")
                {
                    resolved.RemoveAt(resolved.Count - 1);
                }
                // Otherwise, just append to list
                else
                {
                    resolved.Add(part);
                }
            }

            // Reconstruct path
            if (resolved.Count == 0) { path = "."; }
            else { path = string.Join('/', resolved); }

            // Prepend root
            if (isRootPath && !path.StartsWith(".."))
            {
                path = $"/{path}";
            }

            return path;
        }

        #endregion

        #region List Files and Open Stream

        /// <summary>
        /// Enumerates all files that match the given glob-like pattern (if specified).
        /// </summary>
        /// <param name="pattern">Some glob-like pattern or null for all files.</param>
        /// <seealso cref="Extensions.IsLikePattern(string, string)"/>
        public static IEnumerable<string> EnumerateFiles(string pattern = null)
        {
            foreach (var system in _fileSystems)
            {
                foreach (var file in system.EnumerateFiles(pattern))
                {
                    yield return file;
                }
            }
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
            else
            {
                path = NormalizePath(path);

                // Check each file system for this path
                foreach (var system in _fileSystems)
                {
                    if (system.Exists(path))
                    {
                        return system.OpenStream(path);
                    }
                }

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
            else
            {
                path = NormalizePath(path);

                // Check each file system for this path
                foreach (var system in _fileSystems)
                {
                    if (system.Exists(path))
                    {
                        return true;
                    }
                }

                // No known file by this path
                return false;
            }
        }

        #endregion
         
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
