using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Heirloom.IO
{
    /// <summary>
    /// Provides access assembly embedded files in more intuitive way. <para/>
    /// Embedded files, if prefixed by a namespace, are truncated to have the shorter path.
    /// </summary>
    internal static class EmbeddedFiles
    {
        private static readonly HashSet<Assembly> _assemblies;
        private static readonly Dictionary<string, EmbeddedFile> _aliasMap;
        private static readonly HashSet<EmbeddedFile> _files;

        static EmbeddedFiles()
        {
            _assemblies = new HashSet<Assembly>();
            _aliasMap = new Dictionary<string, EmbeddedFile>();
            _files = new HashSet<EmbeddedFile>();

            // Discover entry assembly
            Discover(Assembly.GetEntryAssembly());

            // Try all known assemblies to the current domain
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Discover(assembly);
            }
        }

        public static EmbeddedFile GetFile(string identifier)
        {
            identifier = identifier.ToIdentifier();
            return _aliasMap.ContainsKey(identifier) ? _aliasMap[identifier] : null;
        }

        public static IEnumerable<EmbeddedFile> GetFiles()
        {
            return _files;
        }

        /// <summary>
        /// Discover embedded files in the given assembly.
        /// </summary>
        public static void Discover(Assembly assembly)
        {
            // todo: What and why is this guard here?
            if (assembly == null) { return; }

            // Ignore known assemblies we shouldn't scan (ie. System, etc)
            if (IsIgnoredAssembly(assembly)) { return; }

            // 
            if (_assemblies.Add(assembly))
            {
                try
                {
                    // Find namespace prefixes
                    var namespaces = GetNamespaces(assembly).OrderByDescending(ns => ns.Length);

                    // Look at each file and alias any starting with a namespace
                    foreach (var manifestName in assembly.GetManifestResourceNames())
                    {
                        // Create list default identifier
                        var identifiers = new List<string> { manifestName.ToIdentifier() };

                        // Find a shorter alias
                        foreach (var prefix in namespaces)
                        {
                            // Starts with a namespace prefix?
                            if (manifestName.StartsWith(prefix))
                            {
                                // Chop off the prefix and renormalize
                                var identifier = manifestName[(prefix.Length + 1)..];
                                identifier = identifier.ToIdentifier();

                                // Store the aliased reference to the embedded file
                                identifiers.Add(identifier);
                                break;
                            }
                        }

                        // Create file access object
                        var file = new EmbeddedFile(assembly, manifestName, identifiers);
                        _files.Add(file);

                        // Store file by known identifiers
                        foreach (var identifier in file.Identifiers)
                        {
                            _aliasMap[identifier] = file;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"Unable to discover embedded files in '{assembly.GetName().Name}'.");
                    // Oh no!
                }
            }
        }

        /// <summary>
        /// Gets namespaces of the defined typed in the assembly.
        /// </summary>
        private static IEnumerable<string> GetNamespaces(Assembly assembly)
        {
            return assembly.DefinedTypes.Select(x => x.Namespace)
                                        .Distinct()
                                        .Where(x => !string.IsNullOrEmpty(x));
        }

        private static bool IsIgnoredAssembly(Assembly assembly)
        {
            // Ignore .NET Framework Assemblies
            var productAttribute = assembly.GetCustomAttributes<AssemblyProductAttribute>().FirstOrDefault();
            if (productAttribute?.Product == "Microsoft® .NET Framework") { return true; }
            if (assembly.FullName.StartsWith("System.")) { return true; }

            // Not ignored
            return false;
        }

        /// <summary>
        /// Opens a stream to the embedded file.
        /// </summary>
        public static Stream OpenStream(string identifier)
        {
            Discover(Assembly.GetCallingAssembly());

            // Get the file by identifier
            var embeddedFile = GetFile(identifier);

            // Not a known identifier
            if (embeddedFile == null)
            {
                // Raw real .NET embedded file name
                // todo: try all known assemblies?
                var assembly = Assembly.GetCallingAssembly();
                var stream = assembly.GetManifestResourceStream(identifier);
                return stream ?? throw new FileNotFoundException($"Embedded file: '{identifier}' was not found.");
            }
            else
            {
                // Was a known identifier
                return embeddedFile.OpenStream();
            }
        }

        /// <summary>
        /// Does the given embedded file exist?
        /// </summary>
        public static bool Exists(string identifier)
        {
            return GetFile(identifier) != null;
        }

        /// <summary>
        /// Attempts to convert an embedded identifier into a path.
        /// </summary>
        public static string GuessIdentifierPath(string identifier)
        {
            var parts = identifier.Split('.');
            if (parts.Length > 2)
            {
                var directory = string.Join('/', parts[0..^2]);
                return $"{directory}/{parts[^2]}.{parts[^1]}";
            }
            else
            {
                return identifier;
            }
        }
    }
}
