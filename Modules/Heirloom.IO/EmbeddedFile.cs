using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Heirloom.IO
{
    /// <summary>
    /// Access .NET embedded files in more intuitive way.
    /// </summary>
    public static class EmbeddedFile
    {
        private static readonly Dictionary<string, ManifestFile> _files;
        private static readonly HashSet<Assembly> _assemblies;

        static EmbeddedFile()
        {
            _assemblies = new HashSet<Assembly>();
            _files = new Dictionary<string, ManifestFile>();

            // Discover entry assembly
            Discover(Assembly.GetEntryAssembly());

            // Try all known assemblies to the current domain
            foreach (var assembly in GetKnownAssemblies())
            {
                Discover(assembly);
            }
        }

        private static Assembly[] GetKnownAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        /// Gets a copy of all known embedded file identifiers.
        /// </summary>
        public static string[] GetKnownIdentifiers()
        {
            return _files.Keys.ToArray();
        }

        /// <summary>
        /// Discover embedded files in the given assembly.
        /// </summary>
        public static void Discover(Assembly assembly)
        {
            // todo: warning?
            if (assembly == null) { return; }

            // 
            if (_assemblies.Add(assembly))
            {
                // Find prefixes
                var prefixes = assembly.DefinedTypes
                                .Select(x => x.Namespace)
                                .Distinct()
                                .Where(x => !string.IsNullOrEmpty(x));

                // Look at each file and alias any starting with a namespace
                foreach (var name in assembly.GetManifestResourceNames())
                {
                    var mfile = new ManifestFile(assembly, name);
                    var alias = NormalizeManifestPath(name);
                    var foundAlias = false;

                    // Find a shorter alias
                    foreach (var prefix in prefixes)
                    {
                        // Starts with a namespace prefix?
                        if (name.StartsWith(prefix))
                        {
                            // Chop off the prefix
                            alias = name.Substring(prefix.Length + 1);

                            // Normalize the path
                            alias = NormalizeManifestPath(alias);

                            // Store the aliased reference to the embedded file
                            _files[alias] = mfile;
                            foundAlias = true;

                            break;
                        }
                    }

                    if (!foundAlias)
                    {
                        // Store the reference to the embedded file
                        _files[alias] = mfile;
                    }
                }
            }
        }

        /// <summary>
        /// Opens a stream to the embedded file.
        /// </summary>
        public static Stream OpenStream(string identifier)
        {
            Discover(Assembly.GetCallingAssembly());
            
            if (Exists(identifier))
            {
                // Existing identifier
                var file = GetManifest(identifier);
                return file.OpenStream();
            }
            else
            {
                // Raw real .NET embedded file name
                // todo: try all known assemblies?
                var assembly = Assembly.GetCallingAssembly();
                var stream = assembly.GetManifestResourceStream(identifier);
                return stream ?? throw new FileNotFoundException($"Embedded file: '{identifier}' was not found.");
            }
        }

        /// <summary>
        /// Does the given embedded file exist?
        /// </summary>
        public static bool Exists(string identifier)
        {
            return GetManifest(identifier) != null;
        }

        private static ManifestFile GetManifest(string identifier)
        {
            identifier = NormalizeManifestPath(identifier);

            if (_files.ContainsKey(identifier))
            {
                return _files[identifier];
            }
            else
            {
                return null;
            }
        }

        private static string NormalizeManifestPath(string name)
        {
            name = name.Replace('/', '.');
            name = name.Replace('\\', '.');

            // Should I be doing this?
            name = name.Replace(' ', '_');
            name = name.Replace('-', '_');
            name = name.Replace("(", string.Empty);
            name = name.Replace(")", string.Empty);

            return name.ToLowerInvariant();
        }

        private class ManifestFile
        {
            public string Name { get; }

            public Assembly Assembly { get; }

            public ManifestFile(Assembly assembly, string name)
            {
                Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }

            public Stream OpenStream()
            {
                return Assembly.GetManifestResourceStream(Name);
            }
        }
    }
}
