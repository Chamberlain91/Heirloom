using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Meadows.IO
{
    /// <summary>
    /// Represents an embedded file.
    /// </summary>
    public sealed class EmbeddedFile
    {
        /// <summary>
        /// Which assembly did this embedded file originate?
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// The name of this file in the assembly manifest.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The known transformed identifiers.
        /// </summary>
        public IReadOnlyList<string> Identifiers { get; }

        internal EmbeddedFile(Assembly assembly, string manifestName, IEnumerable<string> identifiers)
        {
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            Path = manifestName ?? throw new ArgumentNullException(nameof(manifestName));
            Identifiers = identifiers.ToArray();
        }

        /// <summary>
        /// Opens a stream to the embedded file.
        /// </summary>
        public Stream OpenStream()
        {
            return Assembly.GetManifestResourceStream(Path);
        }
    }
}
