using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using Heirloom.Drawing.OpenGL.Utilities;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGL
{
    internal sealed class GLShaderFactory
    {
        private readonly Dictionary<string, GLShader> _shaders;
        private readonly Dictionary<string, string> _sources;

        private readonly Dictionary<(string vert, string frag), GLShaderProgram> _programs;

        private readonly Regex _includeDirectiveRegex;
        private readonly HashSet<string> _included;

        private readonly OpenGLRenderContext _renderContext;

        public GLShaderFactory(OpenGLRenderContext renderContext)
        {
            _renderContext = renderContext;

            // 
            _programs = new Dictionary<(string vert, string frag), GLShaderProgram>();
            _shaders = new Dictionary<string, GLShader>();
            _sources = new Dictionary<string, string>();

            // Compile include directive regex
            _includeDirectiveRegex = new Regex("^[ ]*#[ ]*include[ ]+[\"<](.*)[\">].*", RegexOptions.Compiled | RegexOptions.Multiline);
            _included = new HashSet<string>();

            // Generate GLSL function `imageUnit()`
            // todo: rename? ie, `sampleImage()` ??
            var maxTextureImageUnits = GL.GetInteger(GetParameter.MaxTextureImageUnits);

            // 
            SetShaderSource("generated/image", GenerateImageUnitCode(maxTextureImageUnits));
        }

        internal GLShaderProgram GetShaderProgram(string vsFilePath, string fsFilePath)
        {
            // 
            var key = (vsFilePath, fsFilePath);

            // 
            if (_programs.TryGetValue(key, out var program) == false)
            {
                // Construct new shader program
                var vs = GetShader(vsFilePath);
                var fs = GetShader(fsFilePath);
                program = new GLShaderProgram(vs, fs);

                // Store shader for next time
                _programs[key] = program;
            }

            return program;
        }

        #region GetShader, SetShaderSource

        internal void SetShaderSource(string path, string source)
        {
            // normalize to foward slash
            path = path.Replace('\\', '/');

            // Duplicate?
            if (_sources.ContainsKey(path))
            {
                throw new ArgumentException($"Unable to set shader source, '{path}' already defined.");
            }

            // 
            _sources[path] = source;
        }

        private GLShader GetShader(string filePath)
        {
            if (_shaders.TryGetValue(filePath, out var shader))
            {
                // Return already compiled program
                return shader;
            }
            else
            {
                var type = GetShaderType();
                var code = GetSourceCode(filePath, 0);

                // Is OpenGL ES (Mobile)
                if (_renderContext.Info.IsEmbedded)
                {
                    // 
                    var prefix = "#version 300 es\n";
                    if (type == ShaderType.Fragment) { prefix += "precision highp sampler2DArray;\n"; }
                    prefix += "precision highp float;\n";

                    // Append Version Prefix
                    code = prefix + code;
                }
                // Is OpenGL (Desktop)
                else
                {
                    // 
                    var prefix = "#version 330\n";

                    // Append Version Prefix
                    code = prefix + code;
                }

                // Compile and return
                shader = new GLShader(type, code);
                return _shaders[filePath] = shader;
            }

            ShaderType GetShaderType()
            {
                var extension = Path.GetExtension(filePath);

                switch (extension)
                {
                    case ".frag":
                        return ShaderType.Fragment;

                    case ".vert":
                        return ShaderType.Vertex;

                    default:
                        throw new ArgumentException($"Unknown or unable to compile glsl shader with extension '{extension}'.");
                }
            }
        }

        #endregion

        private string GetSourceCode(string filePath, int depth)
        {
            if (depth > 32) { throw new InvalidOperationException("Include directive gone too deep."); }
            if (depth == 0) { _included.Clear(); }

            // Try to get existing shader source...
            if (_sources.TryGetValue(filePath, out var source))
            {
                // Return already known source code
                return source;
            }
            else
            {
                // Shader source was not knowm, load from embedded files
                // and recursively process includes.
                var code = EmbeddedFiles.ReadText(filePath);

                // Process include directives (bakes them into stored source)
                foreach (Match match in _includeDirectiveRegex.Matches(code))
                {
                    // Get match information
                    var capture = match.Captures[0];
                    var includePath = match.Groups[1].Value;

                    // Attempt to assemble a relative path
                    var directory = Path.GetDirectoryName(filePath);
                    includePath = ResolvePath(directory, includePath);

                    // Console.WriteLine($"include '{includePath}'");

                    // Attempt to add the file to the include set,
                    // if newly inserted, descend and process the include!
                    // Otherwise it was already included somewhere else
                    if (_included.Add(includePath))
                    {
                        // Descend and insert included text
                        var include = GetSourceCode(includePath, depth + 1);
                        code = code.Remove(capture.Index, capture.Length);
                        code = code.Insert(capture.Index, include);
                    }
                }

                // Store complete code
                SetShaderSource(filePath, code);
                return code;
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

        static private string GenerateImageUnitCode(int maxTextureImageUnits)
        {
            var code = "";

            // Define effect offset
            code += $"#define EFFECT_UNIT_START {maxTextureImageUnits - 4}\n\n";

            // Define uniform
            code += $"uniform sampler2D uImage[{maxTextureImageUnits}];\n\n";

            // Define imageUnit
            // A function to sample the texture specified by index in vertex data
            code += "vec4 imageUnit(int unit, vec2 uv)\n";
            code += "{\n";
            code += "\tswitch (unit)\n";
            code += "\t{\n";
            code += "\t\tdefault: // Magenta, failure to sample\n";
            code += "\t\t\treturn vec4(1.0, 0.0, 0.5, 1.0);\n";

            for (var i = 0; i < maxTextureImageUnits; i++)
            {
                code += $"\t\tcase {i}: return texture(uImage[{i}], uv);\n";
            }

            code += "\t}\n";
            code += "}\n";

            // Define imageDims
            // A function to get the dimensions of an image (top level of detail)
            code += "ivec2 imageDims(int unit)\n";
            code += "{\n";
            code += "\tswitch (unit)\n";
            code += "\t{\n";
            code += "\t\tdefault: // No texture, no size\n";
            code += "\t\t\treturn ivec2(0, 0);\n";

            for (var i = 0; i < maxTextureImageUnits; i++)
            {
                code += $"\t\tcase {i}: return textureSize(uImage[{i}], 0);\n";
            }

            code += "\t}\n";
            code += "}\n";

            return code;
        }
    }
}
