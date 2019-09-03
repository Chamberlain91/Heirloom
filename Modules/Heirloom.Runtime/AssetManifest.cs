using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Heirloom.Runtime
{
    public static class AssetManifest
    {
        private const BindingFlags InstanceAll = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private static Dictionary<Type, List<AssetLoader>> _loaders;
        private static HashSet<Assembly> _assemblies;
        private static List<AssetOrigin> _assets;

        internal static void Initialize()
        {
            // 
            _loaders = new Dictionary<Type, List<AssetLoader>>();
            _assemblies = new HashSet<Assembly>();
            _assets = new List<AssetOrigin>();

            // Search assemblies for assets (and loader types)
            DiscoverEmbeddedAssets(Assembly.GetExecutingAssembly());
            DiscoverEmbeddedAssets(Assembly.GetCallingAssembly());
            DiscoverEmbeddedAssets(Assembly.GetEntryAssembly());

            // try lowercase variant first, windows is case insensitive but *nix is not
            DiscoverDiskAssets("assets/", "Assets/");
            DiscoverDiskAssets("files/", "Files/");
            DiscoverDiskAssets("data/", "Data/");

            // try to automatically assocaite loaders with assets
            AssociateLoaderWithAssets();
        }

        private static void AssociateLoaderWithAssets()
        {
            // Sort files, used for quick searching later
            _assets.Sort();
        }

        /// <summary>
        /// Enumerate the identifiers of all known assets.
        /// </summary>
        public static IEnumerable<string> Identifiers => _assets.Select(a => a.Identifier);

        /// <summary>
        /// Scan the assembly for assets embedded in an assembly.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Discover(Assembly assembly)
        {
            if (assembly == null) { throw new ArgumentNullException(nameof(assembly)); }

            // 
            DiscoverEmbeddedAssets(assembly);

            // Sort files, used for quick searching later
            _assets.Sort();
        }

        /// <summary>
        /// Scan the specified path for assets on disk.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Discover(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) { throw new ArgumentException("message", nameof(path)); }

            // 
            DiscoverDiskAssets(path);

            // Sort files, used for quick searching later
            _assets.Sort();
        }

        internal static string GetRelativeAsset(string identifier, string relative)
        {
            // 
            identifier = GetIdentifier(identifier);

            var exti = identifier.LastIndexOf('.');
            var fname = identifier.Substring(0, exti);

            var diri = fname.LastIndexOf('.');
            if (diri >= 0)
            {
                var dir = fname.Substring(0, diri);
                return GetIdentifier(ResolvePath(dir, relative));
            }
            else
            {
                // No directory, just return relative...
                return GetIdentifier(ResolvePath("", relative));
            }
        }

        private static string ResolvePath(string directory, string path)
        {
            var prefix = Path.GetFullPath("/"); // ... could probably be cached?

            path = Path.Combine("/", directory, path);
            path = Path.GetFullPath(path);
            path = path.Substring(prefix.Length);

            // Return with '/' slashes
            return path.Replace('\\', '/');
        }

        /// <summary>
        /// Does an asset exist with the given identifier.
        /// </summary>
        public static bool Contains(string identifier)
        {
            return FindAssetIndex(identifier) >= 0;
        }

        /// <summary>
        /// Reads the contents of an asset as a text file.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReadText(string identifier)
        {
            using (var stream = Open(identifier))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Reads the contents of an asset as bytes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ReadBytes(string identifier)
        {
            using (var stream = Open(identifier))
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Opens a stream to the contents of the asset.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stream Open(string identifier)
        {
            var idx = FindAssetIndex(identifier);

            if (idx >= 0)
            {
                return _assets[idx].OpenStream();
            }
            else
            {
                throw new KeyNotFoundException($"Unable to find asset '{identifier}'.");
            }
        }

        internal static IEnumerable<AssetLoader<T>> GetLoaders<T>()
        {
            if (_loaders.TryGetValue(typeof(T), out var loaders))
            {
                // cast each to loader of type
                return loaders.Cast<AssetLoader<T>>();
            }
            else
            {
                // No known loaders...
                return Array.Empty<AssetLoader<T>>();
            }
        }

        /// <summary>
        /// Convert the given input to a standard identifier. <para/>
        /// todo: cover the transformations this does.
        /// </summary>
        public static string GetIdentifier(string identifier)
        {
            // first convert to lowercase to make consistent
            // todo: maybe split camelCase to camel_case?
            identifier = identifier.ToLowerInvariant();

            // convert slashes to dot
            identifier = identifier.Replace('\\', '/');
            identifier = identifier.Replace('/', '.');

            // collapse dots to single dot
            identifier = Regex.Replace(identifier, "\\.+", ".");

            // dash and underscore to spaces
            identifier = identifier.Replace('-', ' ');
            identifier = identifier.Replace('_', ' ');

            // collapse spaces to single space
            identifier = Regex.Replace(identifier, "\\s+", "_");

            return identifier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FindAssetIndex(string identifier)
        {
            // 
            identifier = GetIdentifier(identifier);

            // 
            var fake = new FakeOrigin(identifier);
            return _assets.BinarySearch(fake);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DiscoverEmbeddedAssets(Assembly assembly)
        {
            if (_assemblies.Add(assembly))
            {
                // == FIND ASSETS

                // Look at all embedded assets, if contains a qualified substring,
                // append information to asset list.
                foreach (var name in assembly.GetManifestResourceNames())
                {
                    // 
                    var identifier = GetIdentifier(name);

                    // Try to determine if the embedded asset qualifies
                    if (TryGetIdentifier(ref identifier))
                    {
                        // Append to asset list
                        _assets.Add(new EmbeddedOrigin(assembly, name, identifier));
                    }
                }

                // == FIND ASSET LOADERS

                // For every type in the assembly
                foreach (var type in assembly.DefinedTypes)
                {
                    // Is a subclass of the loader class, found a candidate.
                    if (type.IsSubclassOf(typeof(AssetLoader)))
                    {
                        // Was abstract, skip
                        if (type.IsAbstract) { continue; }

                        // If missing a parameterless constructor, skip
                        if (type.GetConstructor(InstanceAll, null, Type.EmptyTypes, Array.Empty<ParameterModifier>()) == null)
                        {
                            Console.WriteLine($"WARN: AssetLoader '{type.Name}' does not have a parameterless constructor and is ignored.");
                            continue;
                        }

                        // Instantiate instance
                        var loader = (AssetLoader) Activator.CreateInstance(type, true);

                        // Try to get loader list, if not created, create list
                        if (_loaders.TryGetValue(loader.AssetType, out var loaders))
                        {
                            // Add our loader to the list
                            loaders.Add(loader);
                        }
                        else
                        {
                            // Create loader list (w/ our loader in it)
                            _loaders[loader.AssetType] = new List<AssetLoader>
                            {
                                loader
                            };
                        }
                    }
                }
            }

            bool TryGetIdentifier(ref string identifier)
            {
                if (TryGetIdentifierPrefix(ref identifier, "embedded.")) { return true; }
                if (TryGetIdentifierPrefix(ref identifier, "assets.")) { return true; }
                if (TryGetIdentifierPrefix(ref identifier, "files.")) { return true; }
                if (TryGetIdentifierPrefix(ref identifier, "data.")) { return true; }

                // Did not qualify
                return false;
            }

            bool TryGetIdentifierPrefix(ref string identifier, string prefix)
            {
                var index = identifier.IndexOf(prefix);

                // Found something prefixed
                if (index >= 0)
                {
                    identifier = identifier.Substring(index + prefix.Length);
                    return true;
                }

                // Did not qualify
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DiscoverDiskAssets(params string[] variants)
        {
            // Try each variant, accept the first found
            foreach (var path in variants)
            {
                // If the directory exists...
                if (Directory.Exists(path))
                {
                    // Enumerate each recursive file in the directory
                    foreach (var file in EnumerateDirectoryRecursive(path))
                    {
                        var identifier = GetIdentifier(file.Substring(path.Length));
                        _assets.Add(new FileOrigin(file, identifier));
                    }

                    return; // stop, first variant is accepted
                }
            }

            IEnumerable<string> EnumerateDirectoryRecursive(string p)
            {
                if (Directory.Exists(p))
                {
                    // Enumerate each file in the directory
                    foreach (var file in Directory.EnumerateFiles(p))
                    {
                        yield return file;
                    }

                    // Look at directories recursively
                    foreach (var directory in Directory.EnumerateDirectories(p))
                    {
                        // Enumerate each file in that directory
                        foreach (var file in EnumerateDirectoryRecursive(directory))
                        {
                            yield return file;
                        }
                    }
                }
            }
        }

        private abstract class AssetOrigin : IComparable<AssetOrigin>
        {
            protected AssetOrigin(string path, string identifier)
            {
                Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
                Path = path ?? throw new ArgumentNullException(nameof(path));
            }

            public string Identifier { get; }

            public string Path { get; }

            internal abstract Stream OpenStream();

            public int CompareTo(AssetOrigin other)
            {
                return Identifier.CompareTo(other.Identifier);
            }
        }

        private sealed class FakeOrigin : AssetOrigin
        {
            public FakeOrigin(string identifier)
                : base(string.Empty, identifier)
            { }

            internal override Stream OpenStream()
            {
                throw new NotImplementedException();
            }
        }

        private sealed class EmbeddedOrigin : AssetOrigin
        {
            public Assembly Assembly { get; }

            public EmbeddedOrigin(Assembly assembly, string path, string identifier)
                : base(path, identifier)
            {
                Assembly = assembly;
            }

            internal override Stream OpenStream()
            {
                return Assembly.GetManifestResourceStream(Path);
            }
        }

        private sealed class FileOrigin : AssetOrigin
        {
            public FileOrigin(string path, string identifier)
                : base(path, identifier)
            { }

            internal override Stream OpenStream()
            {
                return new FileStream(Path, FileMode.Open, FileAccess.Read);
            }
        }
    }
}
