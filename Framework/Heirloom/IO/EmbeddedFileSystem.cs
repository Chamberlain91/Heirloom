using System.Collections.Generic;
using System.IO;

namespace Heirloom.IO
{
    internal sealed class EmbeddedFileSystem : FileSystem
    {
        public override IEnumerable<string> EnumerateFiles(string pattern)
        {
            if (pattern == null)
            {
                // Emit all files w/ shortest identifier conversion
                foreach (var file in EmbeddedFiles.GetFiles())
                {
                    var shortestIdentifier = file.Identifiers.FindMinimal(i => i.Length);
                    yield return EmbeddedFiles.GuessIdentifierPath(shortestIdentifier);
                }
            }
            else
            {
                // Emit all files that match the pattern with any identifier
                foreach (var file in EmbeddedFiles.GetFiles())
                {
                    foreach (var identifier in file.Identifiers)
                    {
                        var path = EmbeddedFiles.GuessIdentifierPath(identifier);
                        if (path.IsLikePattern(pattern))
                        {
                            yield return path;
                        }
                    }
                }
            }
        }

        public override Stream OpenStream(string identifier)
        {
            return EmbeddedFiles.OpenStream(identifier);
        }

        public override bool Exists(string identifier)
        {
            return EmbeddedFiles.Exists(identifier);
        }
    }
}
