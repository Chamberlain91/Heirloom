using System;
using System.Collections.Generic;
using System.Linq;

namespace Meadows.Drawing.OpenGLES
{
    internal class ESShaderProgram : IDisposable
    {
        private const int LOCATION_NOT_FOUND = -1;
        private const int LOCATION_IN_BLOCK = -2;

        private readonly Dictionary<string, ActiveUniformBlock> _blocks;
        private readonly Dictionary<string, uint> _blockBindings;

        private readonly Dictionary<string, Uniform> _uniforms;
        private readonly Dictionary<string, uint> _textureUnits;
        private readonly Dictionary<string, int> _locations;

        internal readonly uint Handle;

        internal readonly string Name;

        internal readonly ESShaderStage FragmentShader;

        internal readonly ESShaderStage VertexShader;

        #region Constructors

        internal ESShaderProgram(string name, ESShaderStage frag, ESShaderStage vert)
        {
            Name = name;

            FragmentShader = frag;
            VertexShader = vert;

            // 
            Handle = CreateAndLinkShaderProgram(frag, vert);

            // 
            _blocks = new Dictionary<string, ActiveUniformBlock>();
            _blockBindings = new Dictionary<string, uint>();

            _uniforms = new Dictionary<string, Uniform>();
            _textureUnits = new Dictionary<string, uint>();
            _locations = new Dictionary<string, int>();

            // Create uniforms (global)
            foreach (var uniform in GLES.GetActiveUniforms(Handle))
            {
                _uniforms[uniform.Name] = new Uniform(uniform);
            }

            // For each block
            foreach (var block in GLES.GetActiveUniformBlocks(Handle))
            {
                // 
                _blocks[block.Name] = block;

                // 
                GLES.UniformBlockBinding(Handle, block.Index, block.Index);
                _blockBindings[block.Name] = block.Index;

                // Create uniforms (in-block)
                foreach (var uniform in block.Uniforms)
                {
                    // Store block information with uniform
                    _uniforms[uniform.Key].BlockInfo = block;

                    // Mark as part of a block 
                    _locations[uniform.Key] = LOCATION_IN_BLOCK;
                }
            }

            DebugPrintUniformStructure();

            // 
            ConfigureStandardUniforms();
        }

        ~ESShaderProgram()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public IEnumerable<ActiveUniformBlock> Blocks => _blocks.Values;

        public IEnumerable<Uniform> Uniforms => _uniforms.Values;

        #endregion

        #region Print Shader Structure (Debug)

        private void DebugPrintUniformStructure()
        {
            Log.Debug($"Shader Structure ({Name})");

            // Print raw uniforms
            foreach (var uniform in _uniforms.Values.Where(u => u.BlockInfo == null).Select(u => u.Info))
            {
                PrintUniform(uniform, "  ");
            }

            // Print uniform blocks
            foreach (var block in _blocks.Values)
            {
                PrintBlock(block, "  ");
            }

            Log.Debug("");

            static void PrintBlock(ActiveUniformBlock block, string indent)
            {
                Log.Debug($"{indent}B {block.Index} \"{block.Name}\" ({block.DataSize} bytes)");

                foreach (var uniform in block.Uniforms)
                {
                    PrintUniform(uniform.Value, indent + "  ");
                }
            }

            static void PrintUniform(ActiveUniform uniform, string indent)
            {
                var message = $"{indent}U {uniform.Index} \"{uniform.Name}\" ({uniform.Size} x {uniform.Type})";
                if (uniform.Offset != -1) { message += $" @ {uniform.Offset} bytes"; }
                Log.Debug(message);
            }
        }

        #endregion

        private void ConfigureStandardUniforms()
        {
            GLES.UseProgram(Handle);

            // Associate texture units with each sampler
            var textureUnit = 0u;
            foreach (var uniform in Uniforms.Where(u => u.BlockInfo == null).Select(u => u.Info)
                                            .Where(uniform => uniform.Type == ActiveUniformType.Sampler2D))
            {
                // Store texture unit by name
                _textureUnits[uniform.Name] = textureUnit;

                // Associate uniform location to unit
                GLES.Uniform1(GetUniformLocation(uniform.Name), (int) textureUnit);

                // Increment unit
                textureUnit++;
            }

            GLES.UseProgram(0);
        }

        public uint GetBindPoint(string blockName)
        {
            return _blockBindings[blockName];
        }

        public ActiveUniformBlock GetBlock(string name)
        {
            if (_blocks.TryGetValue(name, out var block))
            {
                return block;
            }

            throw new ArgumentException($"Unknown uniform block '{name}'.", nameof(name));
        }

        public bool HasUniform(string name)
        {
            return _uniforms.ContainsKey(name);
        }

        public Uniform GetUniform(string name)
        {
            if (_uniforms.TryGetValue(name, out var uniform))
            {
                return uniform;
            }

            throw new ArgumentException($"Unknown uniform '{name}'.", nameof(name));
        }

        public int GetUniformLocation(string name)
        {
            if (!_locations.TryGetValue(name, out var location))
            {
                location = GLES.GetUniformLocation(Handle, name);
                _locations[name] = location;
            }

            return location;
        }

        public uint GetTextureUnit(string name)
        {
            if (!_textureUnits.TryGetValue(name, out var unit))
            {
                throw new InvalidOperationException($"Unable to get texture unit for '{name}'.");
            }

            return unit;
        }

        private static uint CreateAndLinkShaderProgram(ESShaderStage frag, ESShaderStage vert)
        {
            var handle = GLES.CreateProgram();

            // Attach shaders to program
            GLES.AttachShader(handle, vert.Handle);
            GLES.AttachShader(handle, frag.Handle);

            // Bind relevant attribute locations
            foreach (var attr in ESVertexAttribute.GetAttributes())
            {
                GLES.BindAttribLocation(handle, ESVertexAttribute.GetAttributeIndex(attr), $"a{attr}");
            }

            // Link Program
            GLES.LinkProgram(handle);

            // Get program info, printing info if non blank
            var info = GLES.GetProgramInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(info)) { Console.WriteLine(info); }

            // Failed to link
            if (GLES.GetProgram(handle, ProgramParameter.LinkStatus) == 0)
            {
                throw new Exception(info);
            }

            return handle;
        }

        #region Dispose

        private bool _isDisposed;

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // ...
                }

                // Schedule for deletion on a GL thread.
                ESGraphicsContext.InvokeOnSomeThread(() =>
                {
                    Log.Debug($"[Dispose] Shader ({Handle})");
                    GLES.DeleteProgram(Handle);
                });

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public class Uniform
        {
            public ActiveUniform Info;

            public ActiveUniformBlock BlockInfo;

            public Uniform(ActiveUniform info)
            {
                Info = info ?? throw new ArgumentNullException(nameof(info));
            }
        }
    }
}
