using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGL
{
    internal class GLShaderProgram
    {
        private const int LOCATION_NOT_FOUND = -1;
        private const int LOCATION_IN_BLOCK = -2;

        private readonly Dictionary<string, ActiveUniformBlock> _blocks;
        private readonly Dictionary<string, uint> _binding;

        private readonly Dictionary<string, Uniform> _uniforms;
        private readonly Dictionary<string, int> _locations;

        private bool _isDisposed = false;

        #region Constructors

        internal GLShaderProgram(GLShader frag, GLShader vert)
        {
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
                // won't cover *every* case. Something like an the location of 
                // array with non-zero index will still have to be retrieved
                // on-demand, but this should prepopulate the location of most
                // uniforms by a commonly used name.
                GetUniformLocation(uniform.Name);

                // 
                _uniforms[uniform.Name] = new Uniform(uniform);

                // 
                PrintUniformDebug(uniform);
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

                // 
                PrintUniformBlockDebug(block);
            }

            // 
            ConfigureStandardUniforms();
        }

        ~GLShaderProgram()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        internal uint Handle { get; }

        #endregion

        #region Print Debug

        [Conditional("DEBUG")]
        private void PrintUniformBlockDebug(ActiveUniformBlock block)
        {
            Console.WriteLine($"Block {block.Name}");
            Console.WriteLine($"    Index: {block.Index}");
            Console.WriteLine($"    Byte Size: {block.DataSize}");

            foreach (var uniform in block.Uniforms)
            {
                PrintUniformDebug(uniform.Value, "    ");
            }
        }

        [Conditional("DEBUG")]
        private void PrintUniformDebug(ActiveUniform uniform, string indent = "")
        {
            var location = GetUniformLocation(uniform.Name);

            Console.WriteLine($"{indent}Uniform {uniform.Name}");
            Console.WriteLine($"{indent}    Index: {uniform.Index}");
            Console.WriteLine($"{indent}    Location: {location}");
            Console.WriteLine($"{indent}    Offset: {uniform.Offset}");
            Console.WriteLine($"{indent}    Size: {uniform.Size}");
            Console.WriteLine($"{indent}    Type: {uniform.Type}");
        }

        #endregion

        private void ConfigureStandardUniforms()
        {
            // Query how many image units we can use
            var maxTextureImageUnits = GL.GetInteger(GetParameter.MaxTextureImageUnits);

            // Set initial uniforms
            GL.UseProgram(Handle);

            // Associate samplers with image units one-to-one
            var uImage = GetUniformLocation("uImage[0]");
            for (var i = 0; i < maxTextureImageUnits; i++)
            {
                GL.Uniform1(uImage + i, i);
            }

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

        public IEnumerable<ActiveUniformBlock> GetBlocks()
        {
            return _blocks.Values;
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

        private static uint CreateAndLinkShaderProgram(GLShader frag, GLShader vert)
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
