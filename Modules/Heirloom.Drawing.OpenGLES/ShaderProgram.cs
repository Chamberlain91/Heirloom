using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal class ShaderProgram
    {
        private const int LOCATION_NOT_FOUND = -1;
        private const int LOCATION_IN_BLOCK = -2;

        private readonly Dictionary<string, ActiveUniformBlock> _blocks;
        private readonly Dictionary<string, uint> _binding;

        private readonly Dictionary<string, Uniform> _uniforms;
        private readonly Dictionary<string, int> _locations;

        private bool _isDisposed = false;

        #region Constructors

        internal ShaderProgram(string name, ShaderStage frag, ShaderStage vert)
        {
            Name = name;

            FragmentShader = frag;
            VertexShader = vert;

            // 
            Handle = CreateAndLinkShaderProgram(frag, vert);

            // 
            _blocks = new Dictionary<string, ActiveUniformBlock>();
            _binding = new Dictionary<string, uint>();

            _uniforms = new Dictionary<string, Uniform>();
            _locations = new Dictionary<string, int>();

            // Cache uniform locations
            foreach (var uniform in GL.GetActiveUniforms(Handle))
            {
                // Causes the dictionary to populate with the locations this
                // won't cover *every* case. Something like the location of an
                // array with non-zero index will still have to be retrieved
                // on-demand, but this should prepopulate the location of most
                // uniforms by a commonly used name.
                GetUniformLocation(uniform.Name);

                // 
                _uniforms[uniform.Name] = new Uniform(uniform);
            }

            //
            foreach (var block in GL.GetActiveUniformBlocks(Handle))
            {
                // 
                _blocks[block.Name] = block;

                // 
                GL.UniformBlockBinding(Handle, block.Index, block.Index);
                _binding[block.Name] = block.Index;

                // 
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

        ~ShaderProgram()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        internal ShaderStage FragmentShader { get; }

        internal ShaderStage VertexShader { get; }

        internal string Name { get; }

        internal uint Handle { get; }

        public IEnumerable<ActiveUniformBlock> Blocks => _blocks.Values;

        public IEnumerable<Uniform> Uniforms => _uniforms.Values;

        #endregion

        #region Print Debug

        [Conditional("DEBUG")]
        private void DebugPrintUniformStructure()
        {
            Console.WriteLine($"Shader Structure ({Name})");

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

            Console.WriteLine();

            static void PrintBlock(ActiveUniformBlock block, string indent)
            {
                Console.WriteLine($"{indent}B {block.Index} \"{block.Name}\" ({block.DataSize} bytes)");

                foreach (var uniform in block.Uniforms)
                {
                    PrintUniform(uniform.Value, indent + "  ");
                }
            }

            static void PrintUniform(ActiveUniform uniform, string indent)
            {
                var message = $"{indent}U {uniform.Index} \"{uniform.Name}\" ({uniform.Size} x {uniform.Type})";
                if (uniform.Offset != -1) { message += $" @ {uniform.Offset} bytes"; }
                Console.WriteLine(message);
            }
        }

        #endregion

        private void ConfigureStandardUniforms()
        {
            // Query how many image units we can use
            var maxTextureImageUnits = GL.GetInteger(GetParameter.MaxTextureImageUnits);

            // Set initial uniforms
            GL.UseProgram(Handle);

            // Associate samplers with image units one-to-one
            var uImage = GetUniformLocation("uImageUnits[0]");
            for (var i = 0; i < maxTextureImageUnits; i++)
            {
                GL.Uniform1(uImage + i, i);
            }

            // todo: Automatically enumerate image/texture units, tracking how 
            //       many 'batch units' remain after processing.

            GL.UseProgram(0);
        }

        public uint GetBindPoint(string blockName)
        {
            return _binding[blockName];
        }

        public ActiveUniformBlock GetBlock(string name)
        {
            if (_blocks.TryGetValue(name, out var block))
            {
                return block;
            }

            throw new ArgumentException($"Unknown uniform block '{name}'.", nameof(name));
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
                location = GL.GetUniformLocation(Handle, name);
                _locations[name] = location;
            }

            return location;
        }

        private static uint CreateAndLinkShaderProgram(ShaderStage frag, ShaderStage vert)
        {
            var handle = GL.CreateProgram();

            // Attach shaders to program
            GL.AttachShader(handle, vert.Handle);
            GL.AttachShader(handle, frag.Handle);

            // Bind relevant attribute locations
            foreach (var attr in VertexAttribute.GetAttributes())
            {
                GL.BindAttribLocation(handle, VertexAttribute.GetAttributeIndex(attr), $"a{attr}");
            }

            // Link Program
            GL.LinkProgram(handle);

            // Get program info, printing info if non blank
            var info = GL.GetProgramInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(info)) { Console.WriteLine(info); }

            // Failed to link
            if (GL.GetProgram(handle, ProgramParameter.LinkStatus) == 0)
            {
                throw new Exception(info);
            }

            return handle;
        }

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // TODO: dispose managed objects.
                }

                // TODO: free unmanaged resources
                // Schedule on *some* context for deletion?
                Console.WriteLine("WARN: Disposing Shader Program! OpenGL Resource Not Deleted.");

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
