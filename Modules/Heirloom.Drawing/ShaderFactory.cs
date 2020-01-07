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

            // Load default shader code
            ReadSourceCode("embedded/shaders/", "default.frag");
            ReadSourceCode("embedded/shaders/", "default.vert");
        }

        internal static string GetSource(string path)
        {
            return ReadSourceCode(string.Empty, path);
        }

        private static string ReadSourceCode(string root, string fragment, int depth = 0)
        {
            if (depth > 32) { throw new InvalidOperationException("Include directive gone too deep."); }
            if (depth == 0) { _included.Clear(); }

            // Normalize path
            var path = Path.Combine(root, Files.NormalizePath(fragment));

            // Determines kind of shader by extension
            var shaderType = GetShaderType(path);

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

                // If the root...
                if (depth == 0)
                {
                    // Generates the prefered version preprocessor (ie, #version 330)
                    // and prepends generated code to ensure version is on top.
                    code = $"{GenerateVersionHeader()}\n{code.TrimEnd()}";

                    if (shaderType == ShaderType.Fragment)
                    {
                        // Generate automatic code, this is mainly for abusing the number of texture units as
                        // a batching optimization. This may be replaced by a different mechanism later.
                        code = $"{code}\n\n{GenerateBatchImageMethods()}";
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

        internal static ShaderType GetShaderType(string path)
        {
            var extension = Path.GetExtension(path.ToLowerInvariant());

            return extension switch
            {
                ".frag" => ShaderType.Fragment,
                ".vert" => ShaderType.Vertex,

                _ => throw new ArgumentException($"Unknown or unable to compile glsl shader with extension '{extension}'."),
            };
        }

        #region Auto Generated GLSL

        private static string GenerateVersionHeader()
        {
            // Is OpenGL running on Desktop or ES/Mobile?
            var version = GraphicsAdapter.Instance.Capabilities.IsMobilePlatform ?
                "#version 300 es\n" :
                "#version 330\n";

#if DEBUG
            // When a debug build, enable debug shader pragma
            version += "#pragma debug on\n";
#endif

            return version;
        }

        private static string GenerateBatchImageMethods()
        {
            var maxTextureUnits = GraphicsAdapter.Instance.Capabilities.MaxSupportedShaderImages;

            var code = "";

            // Define uniform
            code += $"uniform sampler2D uImageUnits[{maxTextureUnits}];\n\n";

            // Define imageUnit
            // A function to sample the texture specified by index in vertex data
            code += "vec4 imageUnit(int unit, vec2 uv)\n";
            code += "{\n";
            code += "\tswitch (unit)\n";
            code += "\t{\n";
            code += "\t\tdefault: // Magenta, failure to sample\n";
            code += "\t\t\treturn vec4(1.0, 0.0, 0.5, 1.0);\n";

            for (var i = 0; i < maxTextureUnits; i++)
            {
                code += $"\t\tcase {i}: return texture(uImageUnits[{i}], uv);\n";
            }

            code += "\t}\n";
            code += "}\n\n";

            // Define imageDims
            // A function to get the dimensions of an image (top level of detail)
            code += "ivec2 imageUnitSize(int unit)\n";
            code += "{\n";
            code += "\tswitch (unit)\n";
            code += "\t{\n";
            code += "\t\tdefault: // No texture, no size\n";
            code += "\t\t\treturn ivec2(0, 0);\n";

            for (var i = 0; i < maxTextureUnits; i++)
            {
                code += $"\t\tcase {i}: return textureSize(uImageUnits[{i}], 0);\n";
            }

            code += "\t}\n";
            code += "}\n";

            return code;
        }

        #endregion

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
