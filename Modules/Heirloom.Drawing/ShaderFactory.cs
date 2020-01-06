using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using Heirloom.IO;

namespace Heirloom.Drawing
{
    internal static class ShaderFactory
    {
        private static readonly Dictionary<string, string> _sources = new Dictionary<string, string>();

        // For expanding include directives
        private static readonly HashSet<string> _included = new HashSet<string>();
        private static readonly Regex _includeDirectiveRegex
            = new Regex("^[ ]*#[ ]*include[ ]+[\"<](.*)[\">].*", RegexOptions.Compiled | RegexOptions.Multiline);

        static ShaderFactory()
        {
            // Populate standard includes
            ReadSourceCode("embedded/shaders/", "standard/standard.frag");
            ReadSourceCode("embedded/shaders/", "standard/standard.vert");

            // Load default shaders
            ReadSourceCode("embedded/shaders/", "shader.frag");
            ReadSourceCode("embedded/shaders/", "shader.vert");
        }

        public static string GetSource(string path)
        {
            return ReadSourceCode(string.Empty, path);
        }

        private static string ReadSourceCode(string root, string fragment, int depth = 0)
        {
            if (depth > 32) { throw new InvalidOperationException("Include directive gone too deep."); }
            if (depth == 0) { _included.Clear(); }

            // Normalize path
            var path = Path.Combine(root, Files.NormalizePath(fragment));

            // Have we already processed this source/path?
            if (_sources.TryGetValue(path, out var code))
            {
                // Yes, just return the text.
                return code;
            }
            // Does the path exist?
            else if (Files.Exists(path))
            {
                // Read source text
                code = Files.ReadText(path);

                // Process include directives (baking the into stored source)
                foreach (Match match in _includeDirectiveRegex.Matches(code))
                {
                    // Get match information
                    var capture = match.Captures[0];
                    var includePath = match.Groups[1].Value;

                    // todo: technically a bug I think with root/fragment vs path
                    // exists here with getting the directory

                    // Attempt to assemble a relative path
                    var directory = Path.GetDirectoryName(path);
                    includePath = ResolvePath(directory, includePath);

                    // Attempt to add the file to the include set,
                    // if newly inserted, descend and process the include!
                    // Otherwise it was already included somewhere else
                    if (_included.Add(includePath))
                    {
                        // Truncate root
                        if (includePath.StartsWith(root))
                        {
                            includePath = includePath.Substring(root.Length);
                        }

                        // Descend and insert included text
                        var include = ReadSourceCode(root, includePath, depth + 1);
                        code = code.Remove(capture.Index, capture.Length);
                        code = code.Insert(capture.Index, include);
                    }
                }

                // Store in map and return
                return _sources[fragment] = code;
            }
            else
            {
                throw new FileNotFoundException("Unable to resolve shader path.", path);
            }
        }

        private static string ResolvePath(string directory, string path)
        {
            // Starts with relative prefix
            if (path.StartsWith("./"))
            {
                // Compute path relative to inclusion directory
                path = Path.Combine("/", directory, path);
            }
            else
            {
                // Compute path relative to application "root" directory
                path = Path.Combine("/", path);
            }

            // 
            path = Path.GetFullPath(path);

            // Truncate "C:/"
            // todo: evaulate for files on disk...
            // this is probably super buggy too
            var prefix = Path.GetFullPath("/");
            path = path.Substring(prefix.Length);

            // Return with forward slashes
            return Files.NormalizePath(path);
        }

        private static ShaderKind GetShaderType(string path)
        {
            var extension = Path.GetExtension(path);

            return extension switch
            {
                ".frag" => ShaderKind.Fragment,
                ".vert" => ShaderKind.Vertex,

                _ => throw new ArgumentException($"Unknown or unable to compile glsl shader with extension '{extension}'."),
            };
        }

        private enum ShaderKind
        {
            Vertex,
            Fragment
        }
    }
}
