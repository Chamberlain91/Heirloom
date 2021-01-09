using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Heirloom.Drawing.OpenGLES;
using Heirloom.IO;
using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public sealed class Shader : GraphicsResource
    {
        private const string DefaultFragmentPath = "embedded/shaders/default.frag";
        private const string DefaultVertexPath = "embedded/shaders/default.vert";

        internal readonly bool IsDefaultShader;

        internal readonly string VertSource;

        internal readonly string FragSource;

        private readonly Dictionary<string, Uniform> _uniforms;
        private readonly Dictionary<string, uint> _units;

        #region Constructors

        public Shader(string path, params Define[] defines)
            : this(GetVertexPath(path), GetFragmentPath(path), defines, isDefaultShader: false)
        { }

        public Shader(string vertPath, string fragPath, params Define[] defines)
            : this(vertPath, fragPath, defines, isDefaultShader: false)
        { }

        internal Shader(string vertPath, string fragPath, Define[] defines, bool isDefaultShader)
        {
            IsDefaultShader = isDefaultShader;

            // Ensure graphics backend has been initialized and supports custom shaders
            if (GraphicsBackend.Current == null) { throw new InvalidOperationException($"Graphics backend must be initialized before creating shaders."); }
            if (!isDefaultShader && GraphicsBackend.Current.SupportsCustomShaders)
            {
                throw new InvalidOperationException($"Graphics backend does not support custom shaders.");
            }

            // Validate paths
            ValidateExtension(vertPath, ".vert", "Vertex shader must have a '.vert' extension.");
            ValidateExtension(fragPath, ".frag", "Fragment shader must have a '.frag' extension.");

            // Load shader source (handles preprocessor, etc)
            VertSource = ShaderFactory.LoadSource(vertPath, defines, true);
            FragSource = ShaderFactory.LoadSource(fragPath, defines, true);

            // If custom shaders are supported, compile this shader. If custom shaders
            // are not supported, this must the 'default shader' and we can just skip compilation.
            Uniforms = Array.Empty<Uniform>();
            if (GraphicsBackend.Current.SupportsCustomShaders)
            {
                // Compile shader immedately (blocking call).
                // This returns the uniforms (ie, shader variables) discovered during compilation.
                Uniforms = GraphicsBackend.Current?.CompileShader(this);
            }

            // Store uniforms by name
            _uniforms = new Dictionary<string, Uniform>();
            _units = new Dictionary<string, uint>();
            foreach (var uniform in Uniforms)
            {
                // Store by name
                _uniforms[uniform.Name] = uniform;

                // Map samplers to some unit
                if (uniform.Type == UniformType.Image)
                {
                    _units[uniform.Name] = (uint) _units.Count;
                }
            }
        }

        private static string GetVertexPath(string path)
        {
            return (Path.GetExtension(path).ToLower()) switch
            {
                ".vert" => path,
                ".frag" => DefaultVertexPath,
                _ => throw new ArgumentException("Shaders must have a '.vert' or '.frag' extension.")
            };
        }

        private static string GetFragmentPath(string path)
        {
            return (Path.GetExtension(path).ToLower()) switch
            {
                ".vert" => DefaultFragmentPath,
                ".frag" => path,
                _ => throw new ArgumentException("Shaders must have a '.vert' or '.frag' extension.")
            };
        }

        private static void ValidateExtension(string path, string ext, string message)
        {
            if (!string.Equals(Path.GetExtension(path), ext, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException(message);
            }
        }

        #endregion

        #region Uniforms

        internal IReadOnlyList<Uniform> Uniforms { get; private set; }

        internal Uniform GetUniform(string name)
        {
            if (_uniforms.TryGetValue(name, out var uniform))
            {
                return uniform;
            }
            else
            {
                throw new KeyNotFoundException($"Unable to find uniform named '{name}'.");
            }
        }

        internal bool HasUniform(string name)
        {
            return _uniforms.ContainsKey(name);
        }

        internal uint GetTextureUnit(string name)
        {
            if (_units.TryGetValue(name, out var unit))
            {
                return unit;
            }
            else
            {
                throw new KeyNotFoundException($"Unable to find sampler2D uniform named '{name}'.");
            }
        }

        #endregion

        /// <summary>
        /// Holds a key-value pair for generating #define directives in the shader.
        /// </summary>
        public readonly struct Define
        {
            /// <summary>
            /// The name of the directive.
            /// </summary>
            public readonly string Name;

            /// <summary>
            /// The replacement value of the directive (may be null).
            /// </summary>
            public readonly string Value;

            /// <summary>
            /// Creates a new define directive.
            /// </summary>
            public Define(string name, string value)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Shader symbol name must not be null or empty.", nameof(name));
                }

                Name = name;
                Value = value;
            }

            #region Tuple to Define

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, string val) tuple)
            {
                return new Define(tuple.name, tuple.val);
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, float val) tuple)
            {
                return new Define(tuple.name, $"{tuple.val}");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, float[] val) tuple)
            {
                return new Define(tuple.name, $"float[]({string.Join(", ", tuple.val)}");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, int val) tuple)
            {
                return new Define(tuple.name, $"{tuple.val}");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, int[] val) tuple)
            {
                return new Define(tuple.name, $"int[]({string.Join(", ", tuple.val)}");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, bool val) tuple)
            {
                return new Define(tuple.name, $"{tuple.val}");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, Vector val) tuple)
            {
                var vec = tuple.val;
                return new Define(tuple.name, $"vec2({vec.X}, {vec.Y})");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, Vector[] val) tuple)
            {
                var vecs = tuple.val.Select(vec => $"vec2({vec.X}, {vec.Y})");
                return new Define(tuple.name, $"vec2[]({string.Join(", ", vecs)})");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, Size val) tuple)
            {
                var siz = tuple.val;
                return new Define(tuple.name, $"vec2({siz.Width}, {siz.Height})");
            }

            /// <summary>
            /// Converts the formatted tuple into a <see cref="Define"/> structure.
            /// </summary>
            public static implicit operator Define((string name, Color val) tuple)
            {
                var col = tuple.val;
                return new Define(tuple.name, $"vec4({col.R}, {col.G}, {col.B}, {col.A})");
            }

            #endregion
        }

        internal enum ShaderType
        {
            Vertex,
            Fragment,
            // Compute
        }

        public static Shader Default { get; private set; }

        internal static void InitializeDefaults()
        {
            // Load standard fragment shader library
            var standardFragSource = ShaderFactory.LoadSource("embedded/shaders/standard/standard.frag", new Define[] { ("FRAGMENT_SHADER", 1) }, false);
            ShaderFactory.StoreSource("standard/standard.frag", standardFragSource);

            // Load standard vertex shader library
            var standardVertSource = ShaderFactory.LoadSource("embedded/shaders/standard/standard.vert", new Define[] { ("VERTEX_SHADER", 1) }, false);
            ShaderFactory.StoreSource("standard/standard.vert", standardVertSource);

            // Create default shader
            Default = new Shader("embedded/shaders/default.vert", "embedded/shaders/default.frag", Array.Empty<Define>(), isDefaultShader: true);
        }

        private static class ShaderFactory
        {
            private static readonly Dictionary<string, string> _storage = new Dictionary<string, string>();

            private static readonly Regex _includeRegex
                = new Regex(@"^\s*#\s*include\s+""(.*)"".*", RegexOptions.Compiled | RegexOptions.Multiline);

            private static string GenerateVersionHeader()
            {
                // Is OpenGL running on Desktop or ES/Mobile?
                var version = ESGraphicsBackend.Current?.IsMobilePlatform ?? false ?
                              "#version 300 es\n" :
                              "#version 330\n";

#if DEBUG
                // When a debug build, enable debug shader pragma
                version += "#pragma debug(on)\n";
#endif

                return $"{version}\n";
            }

            private static string GenerateDefines(IEnumerable<Define> defines)
            {
                var output = "";
                foreach (var define in defines)
                {
                    output += $"#define {define.Name}";
                    if (define.Value != null)
                    {
                        output += $" {define.Value}";
                    }
                    output += "\n";
                }
                return output;
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
            public static string LoadSource(string path, IEnumerable<Define> defines, bool prependVersion)
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
                    else
                    if (Files.Exists(path))
                    {
                        // Recursion limits
                        if (depth > 32) { throw new InvalidOperationException("Include directive gone too deep."); }
                        if (depth == 0) { included.Clear(); }

                        // Read source text
                        code = Files.ReadText(path);

                        // Process include directives (baking the into stored source)
                        foreach (Match match in _includeRegex.Matches(code))
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
                        if (depth == 0)
                        {
                            // Generates and prepends the preprocessor definitions
                            code = code.Insert(0, GenerateDefines(defines));

                            if (prependVersion)
                            {
                                // Generates and prepends the prefered version preprocessor (ie, 330 or 300 es)
                                code = code.Insert(0, GenerateVersionHeader());
                            }
                        }

                        // Store processed source
                        // NOTE: Removed because when introducing defines this caused two "different" shaders to load the same
                        //       source with incorrect defines on the other.
                        // TODO: Produce some sort of path/defines key to map with instead to reenable optimized loading of shaders.
                        // StoreSource(path, code);

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
                path = path[prefix.Length..];

                // Return with forward slashes
                return Files.NormalizePath(path);
            }
        }
    }
}
