using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using Heirloom.IO;

namespace Heirloom.Drawing
{
    public sealed partial class Shader
    {
        #region Helpers

        /// <summary>
        /// Determines shader type by extension.
        /// </summary>
        internal static ShaderType GetShaderType(string path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            // Get the shader file extension (to lowercase)
            var extension = Path.GetExtension(path).ToLowerInvariant();

            return extension switch
            {
                ".frag" => ShaderType.Fragment,
                ".vert" => ShaderType.Vertex,

                _ => throw new ArgumentException($"Unable to determine shader type with extension '{extension}'."),
            };
        }

        /// <summary>
        /// Creates a shader name from the vertex and fragment shader paths.
        /// </summary>
        private static string ComposeName(string vertShaderPath, string fragShaderPath)
        {
            var vname = Path.GetFileNameWithoutExtension(vertShaderPath).ToLowerInvariant();
            var fname = Path.GetFileNameWithoutExtension(fragShaderPath).ToLowerInvariant();

            return Regex.Replace($"{vname}-{fname}", "\\s+", "_");
        }

        /// <summary>
        /// Given a set of input paths, determine the vertex and fragment paths.
        /// </summary>
        private static void GetShaderPaths(string[] paths, out string vert, out string frag)
        {
            vert = default;
            frag = default;

            // Load by paths and assign to frag or vert strings. 
            foreach (var path in paths)
            {
                var type = GetShaderType(path);

                switch (type)
                {
                    case ShaderType.Vertex:
                        vert = path;
                        break;

                    case ShaderType.Fragment:
                        frag = path;
                        break;
                }
            }

            // Populate defaults for null strings (which should be common for fragment only shaders)
            if (frag == null) { frag = "embedded/shaders/default.frag"; }
            if (vert == null) { vert = "embedded/shaders/default.vert"; }
        }

        #endregion

        private static class Factory
        {
            private static readonly Dictionary<string, string> _storage = new Dictionary<string, string>();

            private static readonly Regex _regex
                = new Regex("^\\s*#\\s*include\\s+\"(.*)\".*", RegexOptions.Compiled | RegexOptions.Multiline);

            static Factory()
            {
                // Populate standard includes
                StoreSource("standard/standard.frag", LoadSource("embedded/shaders/standard/standard.frag", false));
                StoreSource("standard/standard.vert", LoadSource("embedded/shaders/standard/standard.vert", false));
            }

            internal static object LoadAndCompile(string[] paths, out string name, out UniformInfo[] uniforms)
            {
                // Resolve vertex and fragment shader paths
                GetShaderPaths(paths, out var vertPath, out var fragPath);

                // Compose overall shader name from shader file names
                name = ComposeName(vertPath, fragPath);

                // ...
                var vert = LoadSource(vertPath, true);
                var frag = LoadSource(fragPath, true);

                // Compile shader
                return GraphicsAdapter.ShaderResources.Compile(name, vert, frag, out uniforms);
            }

            private static string GenerateVersionHeader()
            {
                // Is OpenGL running on Desktop or ES/Mobile?
                var version = GraphicsAdapter.Capabilities.IsMobilePlatform ?
                    "#version 300 es\n" :
                    "#version 330\n";

#if DEBUG
                // When a debug build, enable debug shader pragma
                version += "#pragma debug(on)\n";
#endif

                return $"{version}\n";
            }

            /// <summary>
            /// Store GLSL source code.
            /// </summary>
            public static void StoreSource(string path, string code)
            {
                if (_storage.ContainsKey(path))
                {
                    throw new InvalidOperationException($"Unable to store shader source code, '{path}' already exists in map.");
                }

                _storage[path] = code;
            }

            /// <summary>
            /// Load GLSL source code (does not do metadata processing).
            /// </summary>
            public static string LoadSource(string path, bool prependVersion)
            {
                // Set to prevent cyclic inclusion
                var included = new HashSet<string>();

                // Recursively load source files.
                // Descends on each #include and expand into the parent source
                // baking the inclusion into the stored source.
                return RecursiveLoadSource(path, 0);

                // 
                string RecursiveLoadSource(string path, int depth)
                {
                    // Normalize path
                    // todo: resolve path (ie, collapse '..')
                    path = Files.NormalizePath(path);

                    // Have we already loaded this source?
                    if (_storage.TryGetValue(path, out var code))
                    {
                        // Yes, just return the text.
                        return code;
                    }
                    // Does the path exist?
                    else if (Files.Exists(path))
                    {
                        // Recursion limits
                        if (depth > 32) { throw new InvalidOperationException("Include directive gone too deep."); }
                        if (depth == 0) { included.Clear(); }

                        // Read source text
                        code = Files.ReadText(path);

                        // Process include directives (baking the into stored source)
                        foreach (Match match in _regex.Matches(code))
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
                            if (included.Add(includePath))
                            {
                                // Remove #include directive
                                code = code.Remove(capture.Index, capture.Length);

                                // Load requested include and insert into source
                                var include = RecursiveLoadSource(includePath, depth + 1);
                                code = code.Insert(capture.Index, include);
                            }
                        }

                        // 
                        if (depth == 0 && prependVersion)
                        {
                            // Generates and prepends the prefered version preprocessor (ie, 330 or 300 es)
                            code = code.Insert(0, GenerateVersionHeader());
                        }

                        // Store in map and return
                        StoreSource(path, code);

                        // Return include expanded source
                        return code;
                    }
                    else
                    {
                        throw new FileNotFoundException("Unable to resolve shader path.", path);
                    }
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
        }
    }
}
