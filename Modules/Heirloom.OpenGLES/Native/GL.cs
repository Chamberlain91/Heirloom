using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Heirloom.OpenGLES
{
    internal static unsafe partial class GL
    {
        public delegate IntPtr GetProcAddress(string name);

        public static bool HasLoadedFunctions { get; private set; }

        public static void LoadFunctions(GetProcAddress getProcAddress)
        {
            var getDelegateGeneric = typeof(Marshal).GetMethod("GetDelegateForFunctionPointer", new[] { typeof(IntPtr) });

            Console.ForegroundColor = ConsoleColor.Red;

            // Note: Automated use of getProcAddress, hopefully fairly bullet proof?
            // Using reflection, invoke getProcAddress for each field prefixed with 'gl'
            foreach (var field in typeof(GL).GetFields(BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (field.Name.StartsWith("gl"))
                {
                    // Create method to get delegate from getProcAddress
                    var getDelegate = getDelegateGeneric.MakeGenericMethod(field.FieldType);
                    if (getDelegate == null) { throw new InvalidOperationException("Unable to load GL function, function pointer was null!"); }
                    else
                    {
                        // Get address of GL function
                        var procAddr = getProcAddress(field.Name);
                        if (procAddr == IntPtr.Zero)
                        {
                            // Unable to load that function...
                            Console.WriteLine($"Unable to load OpenGL function '{field.Name}', address returned null.");
                        }
                        else
                        {
                            // Convert function pointer to delegate
                            var glFunction = getDelegate.Invoke(null, new object[] { procAddr });

                            // Store delegate in field
                            field.SetValue(null, glFunction);
                        }
                    }
                }
            }

            Console.ResetColor();

            // Claim functions are loaded
            HasLoadedFunctions = true;
        }

        #region Shader

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CreateShader(ShaderType type)
        {
            var name = glCreateShader(type);
            CheckError(nameof(CreateShader));
            return name;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteShader(uint shader)
        {
            glDeleteShader(shader);
            CheckError(nameof(DeleteShader));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CompileShader(uint shader)
        {
            glCompileShader(shader);
            CheckError(nameof(CompileShader));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ShaderSource(uint shader, string source)
        {
            glShaderSource(shader, 1, new[] { source }, new int[] { source.Length });
            CheckError(nameof(ShaderSource));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetShader(uint shader, ShaderParameter name)
        {
            var x = new int[1];
            glGetShaderiv(shader, name, x);
            CheckError(nameof(GetShader));
            return x[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetShaderInfoLog(uint shader)
        {
            var builder = new StringBuilder(16384);
            glGetShaderInfoLog(shader, builder.Capacity, out var messageLength, builder);
            CheckError(nameof(GetShaderInfoLog));
            return builder.ToString(0, messageLength).Trim();
        }

        #endregion

        #region Shader Program

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CreateProgram()
        {
            var name = glCreateProgram();
            CheckError(nameof(CreateProgram));
            return name;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteProgram(uint program)
        {
            glDeleteProgram(program);
            CheckError(nameof(DeleteProgram));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteProgram(ref uint program)
        {
            DeleteProgram(program);
            program = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsProgram(uint program)
        {
            var x = glIsProgram(program);
            CheckError(nameof(IsProgram));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LinkProgram(uint program)
        {
            glLinkProgram(program);
            CheckError(nameof(LinkProgram));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValidateProgram(uint program)
        {
            glValidateProgram(program);
            CheckError(nameof(ValidateProgram));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AttachShader(uint program, uint shader)
        {
            glAttachShader(program, shader);
            CheckError(nameof(AttachShader));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DetachShader(uint program, uint shader)
        {
            glDetachShader(program, shader);
            CheckError(nameof(DetachShader));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UseProgram(uint program)
        {
            glUseProgram(program);
            CheckError(nameof(UseProgram));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindAttribLocation(uint program, uint index, string name)
        {
            glBindAttribLocation(program, index, name);
            CheckError(nameof(BindAttribLocation));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetFragDataLocation(uint program, string name)
        {
            var x = glGetFragDataLocation(program, name);
            CheckError(nameof(GetFragDataLocation));
            return x;
        }

        /// <summary>
        /// Get a value about the given program from the GL implementation.
        /// </summary>
        /// <param name="program"> A valid program name </param>
        /// <param name="property"> The type of property to request </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetProgram(uint program, ProgramParameter property)
        {
            var x = new int[1];
            glGetProgramiv(program, property, x);
            CheckError(nameof(GetProgram));
            return x[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetProgramInfoLog(uint program)
        {
            var builder = new StringBuilder(16384);
            glGetProgramInfoLog(program, builder.Capacity, out var messageLength, builder);
            CheckError(nameof(GetProgramInfoLog));
            return builder.ToString(0, messageLength).Trim();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActiveAttribute GetActiveAttribute(uint program, uint index)
        {
            var builder = new StringBuilder(16384);
            glGetActiveAttrib(program, index, builder.Capacity, out var messageLength, out var size, out var type, builder);
            var name = builder.ToString(0, messageLength);

            CheckError(nameof(GetActiveAttribute));

            return new ActiveAttribute(name, size, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActiveAttribute[] GetActiveAttributes(uint program)
        {
            // Get the number of active attributes
            var count = GetProgram(program, ProgramParameter.ActiveAttributes);

            // Create and populate attribute array
            var attributes = new ActiveAttribute[count];
            for (var i = 0u; i < count; i++)
            {
                attributes[i] = GetActiveAttribute(program, i);
            }

            return attributes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActiveUniform GetActiveUniform(uint program, uint index)
        {
            var builder = new StringBuilder(16384);
            glGetActiveUniform(program, index, builder.Capacity, out var messageLength, out var size, out var type, builder);
            var name = builder.ToString(0, messageLength);
            CheckError(nameof(GetActiveUniform));

            var offset = new int[1];
            glGetActiveUniformsiv(program, 1, new[] { index }, UniformParameter.UnifomOffset, offset);
            CheckError(nameof(GetActiveUniform));

            return new ActiveUniform(name, index, size, type, offset[0]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActiveUniform[] GetActiveUniforms(uint program)
        {
            // Get the number of active uniforms
            var count = GetProgram(program, ProgramParameter.ActiveUniforms);

            // Create and populate attribute array
            var uniforms = new ActiveUniform[count];
            for (var i = 0u; i < count; i++)
            {
                uniforms[i] = GetActiveUniform(program, i);
            }

            return uniforms;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetAttribLocation(uint program, string name)
        {
            var x = glGetAttribLocation(program, name);
            CheckError(nameof(GetAttribLocation));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetUniformLocation(uint program, string name)
        {
            var x = glGetUniformLocation(program, name);
            CheckError(nameof(GetAttribLocation));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetActiveUniformBlockName(uint program, uint blockIndex)
        {
            var builer = new StringBuilder(16384);
            glGetActiveUniformBlockName(program, blockIndex, builer.Capacity, out var nameLength, builer);
            CheckError(nameof(GetActiveUniformBlockName));

            // Fails, return null
            return nameLength == -1 ? null : builer.ToString(0, nameLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetActiveUniformBlockIndex(uint program, string name)
        {
            var idx = glGetUniformBlockIndex(program, name);
            CheckError(nameof(GetActiveUniformBlockIndex));
            return idx;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetActiveUniform(uint program, uint[] uniformIndices, UniformParameter pname, int[] result)
        // TODO: switch( pname ) { ... return arr; } ... because each parameter has known size return?
        {
            glGetActiveUniformsiv(program, uniformIndices.Length, uniformIndices, pname, result);
            CheckError(nameof(GetActiveUniform));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActiveUniformBlock GetActiveUniformBlock(uint program, uint index)
        {
            // Get block name and size
            var name = GetActiveUniformBlockName(program, index);
            GetActiveUniformBlock(program, index, UniformBlockParameter.BlockDataSize, out var size);

            // ??
            GetActiveUniformBlock(program, index, UniformBlockParameter.BlockBinding, out var binding);
            GetActiveUniformBlock(program, index, UniformBlockParameter.BlockReferencedByFragmentShader, out var refByFragment);
            GetActiveUniformBlock(program, index, UniformBlockParameter.BlockReferencedByVertexShader, out var refByVertex);

            // Get active uniforms
            GetActiveUniformBlock(program, index, UniformBlockParameter.BlockActiveUniforms, out var nUniforms);
            var uniformIndices = new int[nUniforms];
            GetActiveUniformBlock(program, index, UniformBlockParameter.BlockActiveUniformIndices, uniformIndices);
            var uniforms = uniformIndices.Select(i => GetActiveUniform(program, (uint) i)).ToArray();

            // Return block
            return new ActiveUniformBlock(name, index, size, uniforms);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActiveUniformBlock[] GetActiveUniformBlocks(uint program)
        {
            // Get the number of active uniform blocks
            var count = GetProgram(program, ProgramParameter.ActiveUniformBlocks);

            // Create and populate attribute array
            var blocks = new ActiveUniformBlock[count];
            for (var i = 0u; i < count; i++)
            {
                blocks[i] = GetActiveUniformBlock(program, i);
            }

            return blocks;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetActiveUniformBlock(uint program, uint blockIndex, UniformBlockParameter pname, int[] result)
        // TODO: switch( pname ) { ... return arr; } ... because each parameter has known size return?
        {
            glGetActiveUniformBlockiv(program, blockIndex, pname, result);
            CheckError(nameof(GetActiveUniformBlock));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetActiveUniformBlock(uint program, uint blockIndex, UniformBlockParameter pname, out int value)
        // TODO: switch( pname ) { ... return arr; } ... because each parameter has known size return?
        {
            var result = new int[1];
            GetActiveUniformBlock(program, blockIndex, pname, result);
            value = result[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformBlockBinding(uint program, uint index, uint binding)
        {
            glUniformBlockBinding(program, index, binding);
            CheckError(nameof(UniformBlockBinding));
        }

        #endregion

        #region Singular Uniforms

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, float v)
        {
            glUniform1f(location, v);
            CheckError(nameof(Uniform1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, float v1, float v2)
        {
            glUniform2f(location, v1, v2);
            CheckError(nameof(Uniform2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, float v1, float v2, float v3)
        {
            glUniform3f(location, v1, v2, v3);
            CheckError(nameof(Uniform3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, float v1, float v2, float v3, float v4)
        {
            glUniform4f(location, v1, v2, v3, v4);
            CheckError(nameof(Uniform4));
        }

        #endregion

        #region Integer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, int v)
        {
            glUniform1i(location, v);
            CheckError(nameof(Uniform1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, int v1, int v2)
        {
            glUniform2i(location, v1, v2);
            CheckError(nameof(Uniform2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, int v1, int v2, int v3)
        {
            glUniform3i(location, v1, v2, v3);
            CheckError(nameof(Uniform3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, int v1, int v2, int v3, int v4)
        {
            glUniform4i(location, v1, v2, v3, v4);
            CheckError(nameof(Uniform4));
        }

        #endregion

        #region Unsigned Integer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, uint v)
        {
            glUniform1ui(location, v);
            CheckError(nameof(Uniform1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, uint v1, uint v2)
        {
            glUniform2ui(location, v1, v2);
            CheckError(nameof(Uniform2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, uint v1, uint v2, uint v3)
        {
            glUniform3ui(location, v1, v2, v3);
            CheckError(nameof(Uniform3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, uint v1, uint v2, uint v3, uint v4)
        {
            glUniform4ui(location, v1, v2, v3, v4);
            CheckError(nameof(Uniform4));
        }

        #endregion

        #endregion

        #region Array Uniforms

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, int count, float* arr)
        {
            glUniform1fv(location, count, arr);
            CheckError(nameof(Uniform1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, int count, float* arr)
        {
            glUniform2fv(location, count, arr);
            CheckError(nameof(Uniform2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, int count, float* arr)
        {
            glUniform3fv(location, count, arr);
            CheckError(nameof(Uniform3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, int count, float* arr)
        {
            glUniform4fv(location, count, arr);
            CheckError(nameof(Uniform4));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, float[] arr)
        {
            fixed (float* ptr = arr)
            {
                glUniform1fv(location, arr.Length, ptr);
                CheckError(nameof(Uniform1));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, float[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 2) != 0) { throw new ArgumentException("Array length is not a multiple of 2."); }

            fixed (float* ptr = arr)
            {
                glUniform2fv(location, arr.Length / 2, ptr);
                CheckError(nameof(Uniform2));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, float[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 3) != 0) { throw new ArgumentException("Array length is not a multiple of 3."); }

            fixed (float* ptr = arr)
            {
                glUniform3fv(location, arr.Length / 3, ptr);
                CheckError(nameof(Uniform3));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, float[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 4) != 0) { throw new ArgumentException("Array length is not a multiple of 4."); }

            fixed (float* ptr = arr)
            {
                glUniform4fv(location, arr.Length / 4, ptr);
                CheckError(nameof(Uniform4));
            }
        }

        #endregion

        #region Integer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, int count, int* arr)
        {
            glUniform1iv(location, count, arr);
            CheckError(nameof(Uniform1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, int count, int* arr)
        {
            glUniform2iv(location, count, arr);
            CheckError(nameof(Uniform2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, int count, int* arr)
        {
            glUniform3iv(location, count, arr);
            CheckError(nameof(Uniform3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, int count, int* arr)
        {
            glUniform4iv(location, count, arr);
            CheckError(nameof(Uniform4));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, int[] arr)
        {
            fixed (int* ptr = arr)
            {
                glUniform1iv(location, arr.Length, ptr);
                CheckError(nameof(Uniform1));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, int[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 2) != 0) { throw new ArgumentException("Array length is not a multiple of 2."); }

            fixed (int* ptr = arr)
            {
                glUniform2iv(location, arr.Length / 2, ptr);
                CheckError(nameof(Uniform2));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, int[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 3) != 0) { throw new ArgumentException("Array length is not a multiple of 3."); }

            fixed (int* ptr = arr)
            {
                glUniform3iv(location, arr.Length / 3, ptr);
                CheckError(nameof(Uniform3));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, int[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 4) != 0) { throw new ArgumentException("Array length is not a multiple of 4."); }

            fixed (int* ptr = arr)
            {
                glUniform4iv(location, arr.Length / 4, ptr);
                CheckError(nameof(Uniform4));
            }
        }

        #endregion

        #region Unsigned Integer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, int count, uint* arr)
        {
            glUniform1uiv(location, count, arr);
            CheckError(nameof(Uniform1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, int count, uint* arr)
        {
            glUniform2uiv(location, count, arr);
            CheckError(nameof(Uniform2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, int count, uint* arr)
        {
            glUniform3uiv(location, count, arr);
            CheckError(nameof(Uniform3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, int count, uint* arr)
        {
            glUniform4uiv(location, count, arr);
            CheckError(nameof(Uniform4));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform1(int location, uint[] arr)
        {
            fixed (uint* ptr = arr)
            {
                glUniform1uiv(location, arr.Length, ptr);
                CheckError(nameof(Uniform1));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform2(int location, uint[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 2) != 0) { throw new ArgumentException("Array length is not a multiple of 2."); }

            fixed (uint* ptr = arr)
            {
                glUniform2uiv(location, arr.Length / 2, ptr);
                CheckError(nameof(Uniform2));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform3(int location, uint[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 3) != 0) { throw new ArgumentException("Array length is not a multiple of 3."); }

            fixed (uint* ptr = arr)
            {
                glUniform3uiv(location, arr.Length / 3, ptr);
                CheckError(nameof(Uniform3));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Uniform4(int location, uint[] arr)
        {
            // Ensure the array is appropriate length
            if ((arr.Length % 4) != 0) { throw new ArgumentException("Array length is not a multiple of 4."); }

            fixed (uint* ptr = arr)
            {
                glUniform4uiv(location, arr.Length / 4, ptr);
                CheckError(nameof(Uniform4));
            }
        }

        #endregion

        #endregion

        #region Matrix Uniforms

        #region Array Based

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix2x2(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix2x2(location, values.Length / 4, ptr);
            }

            CheckError(nameof(UniformMatrix2x2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix2x3(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix2x3(location, values.Length / 6, ptr);
            }

            CheckError(nameof(UniformMatrix2x3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix2x4(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix2x4(location, values.Length / 8, ptr);
            }

            CheckError(nameof(UniformMatrix2x4));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix3x2(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix3x2(location, values.Length / 6, ptr);
            }

            CheckError(nameof(UniformMatrix3x2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix3x3(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix3x3(location, values.Length / 9, ptr);
            }

            CheckError(nameof(UniformMatrix3x3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix3x4(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix3x2(location, values.Length / 12, ptr);
            }

            CheckError(nameof(UniformMatrix3x2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix4x2(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix4x2(location, values.Length / 8, ptr);
            }

            CheckError(nameof(UniformMatrix4x2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix4x3(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix4x3(location, values.Length / 12, ptr);
            }

            CheckError(nameof(UniformMatrix4x3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix4x4(int location, float[] values)
        {
            fixed (float* ptr = values)
            {
                UniformMatrix4x4(location, values.Length / 16, ptr);
            }

            CheckError(nameof(UniformMatrix4x4));
        }

        #endregion

        #region Pointer Based

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix2x2(int location, int count, float* ptr)
        {
            glUniformMatrix2fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix2x2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix2x3(int location, int count, float* ptr)
        {
            glUniformMatrix2x3fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix2x3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix2x4(int location, int count, float* ptr)
        {
            glUniformMatrix2x4fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix2x4));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix3x2(int location, int count, float* ptr)
        {
            glUniformMatrix3x2fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix3x2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix3x3(int location, int count, float* ptr)
        {
            glUniformMatrix3fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix3x3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix3x4(int location, int count, float* ptr)
        {
            glUniformMatrix3x4fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix3x4));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix4x2(int location, int count, float* ptr)
        {
            glUniformMatrix4x2fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix4x2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix4x3(int location, int count, float* ptr)
        {
            glUniformMatrix4x3fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix4x3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UniformMatrix4x4(int location, int count, float* ptr)
        {
            glUniformMatrix4fv(location, count, false, ptr);
            CheckError(nameof(UniformMatrix4x4));
        }

        #endregion

        #endregion

        #region Get Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(StringParameter name)
        {
            var x = new string(glGetString(name));
            CheckError(nameof(GetString));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBoolean(GetParameter name)
        {
            bool x;
            glGetBooleanv(name, &x);
            CheckError(nameof(GetBoolean));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetBoolean(GetParameter name, bool[] values)
        {
            fixed (bool* ptr = values)
            {
                glGetBooleanv(name, ptr);
            }

            CheckError(nameof(GetBoolean));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetInteger(GetParameter name)
        {
            int x;
            glGetIntegerv(name, &x);
            CheckError(nameof(GetInteger));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] GetInternalformat(RenderbufferFormat renderBufferFormat, InternalFormatParameter pname, int bufferSize = 16)
        {
            var buffer = new int[bufferSize];
            glGetInternalformativ(InternalFormatTarget.GL_RENDERBUFFER, (int) renderBufferFormat, pname, buffer.Length, buffer);
            CheckError(nameof(GetInteger));
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetIntegers(GetParameter name, int[] values)
        {
            fixed (int* ptr = values)
            {
                glGetIntegerv(name, ptr);
            }

            CheckError(nameof(GetIntegers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] GetIntegers(GetParameter name)
        {
            int[] arr;
            switch (name)
            {
                case GetParameter.Viewport:
                case GetParameter.ScissorBox:
                case GetParameter.ColorWriteMask:
                    arr = new int[4];
                    break;

                default:
                    throw new NotImplementedException();
            }

            GetIntegers(name, arr);
            return arr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetFloat(GetParameter name)
        {
            float x;
            glGetFloatv(name, &x);
            CheckError(nameof(GetFloat));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetFloat(GetParameter name, float[] values)
        {
            fixed (float* ptr = values)
            {
                glGetFloatv(name, ptr);
            }

            CheckError(nameof(GetFloat));
        }

        #endregion

        #region Draw Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawArrays(DrawMode mode, int count)
        {
            glDrawArrays(mode, 0, count);
            CheckError(nameof(DrawArrays));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawArrays(DrawMode mode, int first, int count)
        {
            glDrawArrays(mode, first, count);
            CheckError(nameof(DrawArrays));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawElements(DrawMode mode, int count, DrawElementType type, int offset = 0)
        {
            glDrawElements(mode, count, type, (void*) offset);
            CheckError(nameof(DrawElements));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRangeElements(DrawMode mode, int start, int end, int count, DrawElementType type, int offset = 0)
        {
            glDrawRangeElements(mode, start, end, count, type, (void*) offset);
            CheckError(nameof(DrawRangeElements));
        }

        #endregion

        #region Instance Draw Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawArraysInstanced(DrawMode mode, int count, int primCount)
        {
            glDrawArraysInstanced(mode, 0, count, primCount);
            CheckError(nameof(DrawArraysInstanced));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawArraysInstanced(DrawMode mode, int first, int count, int primCount)
        {
            glDrawArraysInstanced(mode, first, count, primCount);
            CheckError(nameof(DrawArraysInstanced));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawElementsInstanced(DrawMode mode, int count, DrawElementType type, int primCount, int offset = 0)
        {
            glDrawElementsInstanced(mode, count, type, (void*) offset, primCount);
            CheckError(nameof(DrawElementsInstanced));
        }

        #endregion

        #region Vertex Attribute Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnableVertexAttribArray(uint index)
        {
            glEnableVertexAttribArray(index);
            CheckError(nameof(EnableVertexAttribArray));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisableVertexAttribArray(uint index)
        {
            glDisableVertexAttribArray(index);
            CheckError(nameof(DisableVertexAttribArray));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVertexAttribPointer(uint index, int size, VertexAttributeType type, bool normalized, int stride, IntPtr pointer)
        {
            glVertexAttribPointer(index, size, type, normalized, stride, pointer);
            CheckError(nameof(SetVertexAttribPointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVertexAttribPointer(uint index, int size, VertexAttributeType type, bool normalized, int stride, uint bufferOffset)
        {
            glVertexAttribPointer(index, size, type, normalized, stride, (IntPtr) bufferOffset);
            CheckError(nameof(SetVertexAttribPointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVertexAttribDivisor(uint index, int divisor)
        {
            glVertexAttribDivisor(index, divisor);
            CheckError(nameof(SetVertexAttribDivisor));
        }

        #endregion

        #region Buffers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GenBuffer()
        {
            return GenBuffers(1)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GenBuffers(int n)
        {
            var x = new uint[n];
            glGenBuffers(n, x);
            CheckError(nameof(GenBuffers));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteBuffers(int n, uint[] buffers)
        {
            glDeleteBuffers(n, buffers);
            CheckError(nameof(DeleteBuffers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteBuffer(uint buffer)
        {
            DeleteBuffers(1, new[] { buffer });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteBuffer(ref uint buffer)
        {
            DeleteBuffer(buffer);
            buffer = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBuffer(uint buffer)
        {
            var x = glIsBuffer(buffer);
            CheckError(nameof(IsBuffer));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindBuffer(BufferTarget target, uint buffer)
        {
            glBindBuffer(target, buffer);
            CheckError(nameof(BindBuffer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindBufferBase(BufferTarget target, uint index, uint buffer)
        {
            glBindBufferBase(target, index, buffer);
            CheckError(nameof(BindBufferBase));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetBufferParameters(BufferTarget index, BufferParameter parameter, int[] result)
        {
            glGetBufferParameteriv(index, parameter, result);
            CheckError(nameof(GetBufferParameters));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BufferData(BufferTarget target, uint size, IntPtr data, BufferUsage usage = BufferUsage.Static)
        {
            glBufferData(target, size, data, usage);
            CheckError(nameof(BufferData));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BufferSubData(BufferTarget target, uint offset, uint size, IntPtr data)
        {
            glBufferSubData(target, offset, size, data);
            CheckError(nameof(BufferSubData));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BufferData<T>(BufferTarget target, T[] data, BufferUsage usage = BufferUsage.Static) where T : struct
        {
            var size = Marshal.SizeOf<T>() * data.Length;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            glBufferData(target, (uint) size, handle.AddrOfPinnedObject(), usage);
            handle.Free();

            CheckError(nameof(BufferData));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BufferSubData<T>(BufferTarget target, uint offset, T[] data) where T : struct
        {
            var size = Marshal.SizeOf<T>() * data.Length;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            glBufferSubData(target, offset, (uint) size, handle.AddrOfPinnedObject());
            handle.Free();

            CheckError(nameof(BufferSubData));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* MapBufferRange(BufferTarget target, int offset, int length, MapBufferAccess access)
        {
            var ptr = glMapBufferRange(target, offset, length, access);
            CheckError(nameof(MapBufferRange));
            return ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnmapBuffer(BufferTarget target)
        {
            var success = glUnmapBuffer(target);
            CheckError(nameof(UnmapBuffer));
            return success;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FlushMappedBufferRange(BufferTarget target, int offset, int size)
        {
            glFlushMappedBufferRange(target, offset, size);
            CheckError(nameof(FlushMappedBufferRange));
        }

        #endregion

        #region Vertex Array

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GenVertexArray()
        {
            return GenVertexArrays(1)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GenVertexArrays(int n)
        {
            var x = new uint[n];
            glGenVertexArrays(n, x);
            CheckError(nameof(GenVertexArrays));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteVertexArrays(int n, uint[] vaos)
        {
            glDeleteVertexArrays(1, vaos);
            CheckError(nameof(DeleteVertexArrays));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteVertexArray(uint vao)
        {
            DeleteVertexArrays(1, new[] { vao });
            CheckError(nameof(DeleteVertexArray));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteVertexArray(ref uint vao)
        {
            DeleteVertexArray(vao);
            vao = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsVertexArray(uint vao)
        {
            glIsVertexArray(vao);
            CheckError(nameof(IsVertexArray));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindVertexArray(uint vao)
        {
            glBindVertexArray(vao);
            CheckError(nameof(BindVertexArray));
        }

        #endregion

        #region Framebuffers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GenFramebuffer()
        {
            return GenFramebuffers(1)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GenFramebuffers(int n)
        {
            var x = new uint[n];
            glGenFramebuffers(n, x);
            CheckError(nameof(GenFramebuffers));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFramebuffers(int n, uint[] buffers)
        {
            glDeleteFramebuffers(1, buffers);
            CheckError(nameof(DeleteFramebuffers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFramebuffer(uint buffer)
        {
            DeleteFramebuffers(1, new[] { buffer });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFramebuffer(ref uint buffer)
        {
            DeleteFramebuffer(buffer);
            buffer = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFramebuffer(uint buffer)
        {
            var x = glIsFramebuffer(buffer);
            CheckError(nameof(IsFramebuffer));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindFramebuffer(FramebufferTarget target, uint buffer)
        {
            glBindFramebuffer(target, buffer);
            CheckError(nameof(BindFramebuffer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetFramebufferAttachmentParameters(FramebufferTarget target, FramebufferAttachment attachment, FramebufferAttachmentParameter parameter, int[] result)
        {
            glGetFramebufferAttachmentParameteriv(target, attachment, parameter, result);
            CheckError(nameof(GetFramebufferAttachmentParameters));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FramebufferStatus CheckFramebufferStatus(FramebufferTarget target)
        {
            var x = glCheckFramebufferStatus(target);
            CheckError(nameof(CheckFramebufferStatus));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, TextureImageTarget texTarget, uint tex, int mip)
        {
            glFramebufferTexture2D(target, attachment, texTarget, tex, mip);
            CheckError(nameof(FramebufferTexture2D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FramebufferTextureLayer(FramebufferTarget target, FramebufferAttachment attachment, uint tex, int mip, int layer)
        {
            glFramebufferTextureLayer(target, attachment, tex, mip, layer);
            CheckError(nameof(FramebufferTextureLayer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment, uint renderbuffer)
        {
            glFramebufferRenderbuffer(target, attachment, RenderbufferTarget.Renderbuffer, renderbuffer);
            CheckError(nameof(FramebufferRenderbuffer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, FramebufferBlitMask mask, FramebufferBlitFilter filter)
        {
            glBlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
            CheckError(nameof(BlitFramebuffer));
        }

        //public static void BlitFramebuffer( Rectangle src, Rectangle dst, FramebufferBlitMask mask, FramebufferBlitFilter filter )
        //{
        //    glBlitFramebuffer( src.X, src.Y, src.Right, src.Bottom, dst.X, dst.Y, dst.Right, dst.Bottom, mask, filter );
        //    CheckError( "GetActiveUniform" );
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvalidateFramebuffer(FramebufferAttachment[] attachments)
        {
            glInvalidateFramebuffer(FramebufferTarget.Framebuffer, attachments.Length, attachments);
            CheckError(nameof(InvalidateFramebuffer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvalidateSubFramebuffer(FramebufferAttachment[] attachments, int x, int y, int width, int height)
        {
            glInvalidateSubFramebuffer(FramebufferTarget.Framebuffer, attachments.Length, attachments, x, y, width, height);
            CheckError(nameof(InvalidateSubFramebuffer));
        }

        #endregion

        #region Renderbuffers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GenRenderbuffer()
        {
            return GenRenderbuffers(1)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GenRenderbuffers(int n)
        {
            var x = new uint[n];
            glGenRenderbuffers(n, x);
            CheckError(nameof(GenRenderbuffers));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteRenderbuffer(uint buffer)
        {
            DeleteRenderbuffers(1, new[] { buffer });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteRenderbuffer(ref uint buffer)
        {
            DeleteRenderbuffer(buffer);
            buffer = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteRenderbuffers(int n, uint[] buffers)
        {
            glDeleteRenderbuffers(1, buffers);
            CheckError(nameof(DeleteRenderbuffers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRenderbuffer(uint buffer)
        {
            var x = glIsRenderbuffer(buffer);
            CheckError(nameof(IsRenderbuffer));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RenderbufferStorage(RenderbufferFormat format, int width, int height)
        {
            glRenderbufferStorage(RenderbufferTarget.Renderbuffer, format, width, height);
            CheckError(nameof(RenderbufferStorage));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RenderbufferStorage(RenderbufferFormat format, int width, int height, int samples = 0)
        {
            glRenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, samples, format, width, height);
            CheckError(nameof(RenderbufferStorage));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetRenderbufferParameter(RenderbufferValue parameter, int[] result)
        {
            glGetRenderbufferParameteriv(RenderbufferTarget.Renderbuffer, parameter, result);
            CheckError(nameof(GetRenderbufferParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindRenderbuffer(uint renderbuffer)
        {
            glBindRenderbuffer(RenderbufferTarget.Renderbuffer, renderbuffer);
            CheckError(nameof(BindRenderbuffer));
        }

        #endregion

        #region Textures

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GenTexture()
        {
            return GenTextures(1)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GenTextures(int n)
        {
            var x = new uint[n];
            glGenTextures(n, x);
            CheckError(nameof(GenTextures));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteTextures(int n, uint[] textures)
        {
            glDeleteTextures(1, textures);
            CheckError(nameof(DeleteTextures));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteTexture(uint texture)
        {
            DeleteTextures(1, new[] { texture });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteTexture(ref uint texture)
        {
            DeleteTexture(texture);
            texture = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTexture(uint texture)
        {
            var x = glIsTexture(texture);
            CheckError(nameof(IsTexture));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindTexture(TextureTarget target, uint texture)
        {
            glBindTexture(target, texture);
            CheckError(nameof(BindTexture));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ActiveTexture(uint texture)
        {
            glActiveTexture( /*GL_TEXTURE0*/0x84C0 + texture);
            CheckError(nameof(ActiveTexture));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetTextureParameters(TextureTarget index, TextureParameter parameter, float[] result)
        {
            glGetTexParameterfv(index, parameter, result);
            CheckError(nameof(GetTextureParameters));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetTextureParameters(TextureTarget index, TextureParameter parameter, int[] result)
        {
            glGetTexParameteriv(index, parameter, result);
            CheckError(nameof(GetTextureParameters));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTextureParameter(TextureTarget index, TextureParameter parameter, float value)
        {
            glTexParameterf(index, parameter, value);
            CheckError(nameof(SetTextureParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTextureParameter(TextureTarget index, TextureParameter parameter, int value)
        {
            glTexParameteri(index, parameter, value);
            CheckError(nameof(SetTextureParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTextureParameter(TextureTarget index, TextureParameter parameter, float[] value)
        {
            glTexParameterfv(index, parameter, value);
            CheckError(nameof(SetTextureParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTextureParameter(TextureTarget index, TextureParameter parameter, int[] value)
        {
            glTexParameteriv(index, parameter, value);
            CheckError(nameof(SetTextureParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexStorage2D(TextureImageTarget target, int levels, TextureSizedFormat format, int width, int height)
        {
            glTexStorage2D(target, levels, format, width, height);
            CheckError(nameof(TexStorage2D));
        }

        // ES 3.1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexStorage2DMultisample(TextureImageTarget target, int samples, TextureSizedFormat format, int width, int height, bool fixedSampleLocations = false)
        {
            glTexStorage2DMultisample(target, samples, format, width, height, fixedSampleLocations);
            CheckError(nameof(TexStorage2DMultisample));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexStorage3D(TextureImageTarget target, int levels, TextureSizedFormat format, int width, int height, int depth)
        {
            glTexStorage3D(target, levels, format, width, height, depth);
            CheckError(nameof(TexStorage3D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexImage2D(TextureImageTarget target, int level, TextureSizedFormat internalFormat, int width, int height, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data)
        {
            glTexImage2D(target, level, internalFormat, width, height, 0, format, pixelFormat, data);
            CheckError(nameof(TexImage2D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexImage3D(TextureImageTarget target, int level, TextureSizedFormat internalFormat, int width, int height, int depth, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data)
        {
            glTexImage3D(target, level, internalFormat, width, height, depth, 0, format, pixelFormat, data);
            CheckError(nameof(TexImage3D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexSubImage2D(TextureImageTarget target, int level, int xoffset, int yoffset, int width, int height, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data)
        {
            glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, pixelFormat, data);
            CheckError(nameof(TexSubImage2D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexSubImage3D(TextureImageTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data)
        {
            glTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, pixelFormat, data);
            CheckError(nameof(TexSubImage3D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CompressedTexImage2D(TextureImageTarget target, int level, int width, int height, TextureCompressedFormat format, int size, IntPtr data)
        {
            glCompressedTexImage2D(target, level, format, width, height, 0, size, data);
            CheckError(nameof(CompressedTexImage2D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CompressedTexImage3D(TextureImageTarget target, int level, int width, int height, int depth, TextureCompressedFormat format, int size, IntPtr data)
        {
            glCompressedTexImage3D(target, level, format, width, height, depth, 0, size, data);
            CheckError(nameof(CompressedTexImage3D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CompressedTexSubImage2D(TextureImageTarget target, int level, int xoffset, int yoffset, int width, int height, int size, IntPtr data)
        {
            glCompressedTexSubImage2D(target, level, xoffset, yoffset, width, height, size, data);
            CheckError(nameof(CompressedTexSubImage2D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CompressedTexSubImage3D(TextureImageTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int size, IntPtr data)
        {
            glCompressedTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, size, data);
            CheckError(nameof(CompressedTexSubImage3D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexImage2D<T>(TextureImageTarget target, int level, TextureSizedFormat internalFormat, int width, int height, TexturePixelFormat format, TexturePixelType pixelFormat, T[] data)
            where T : struct
        {
            var size = Marshal.SizeOf<T>() * data.Length;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            glTexImage2D(target, level, internalFormat, width, height, 0, format, pixelFormat, handle.AddrOfPinnedObject());
            handle.Free();

            CheckError(nameof(TexImage2D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexImage3D<T>(TextureImageTarget target, int level, TextureSizedFormat internalFormat, int width, int height, int depth, TexturePixelFormat format, TexturePixelType pixelFormat, T[] data)
            where T : struct
        {
            var size = Marshal.SizeOf<T>() * data.Length;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            glTexImage3D(target, level, internalFormat, width, height, depth, 0, format, pixelFormat, handle.AddrOfPinnedObject());
            handle.Free();

            CheckError(nameof(TexImage3D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexSubImage2D<T>(TextureImageTarget target, int level, int xoffset, int yoffset, int width, int height, TexturePixelFormat format, TexturePixelType pixelFormat, T[] data)
            where T : struct
        {
            var size = Marshal.SizeOf<T>() * data.Length;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, pixelFormat, handle.AddrOfPinnedObject());
            handle.Free();

            CheckError(nameof(TexSubImage2D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TexSubImage3D<T>(TextureImageTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, TexturePixelFormat format, TexturePixelType pixelFormat, T[] data)
            where T : struct
        {
            var size = Marshal.SizeOf<T>() * data.Length;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            glTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, pixelFormat, handle.AddrOfPinnedObject());
            handle.Free();

            CheckError(nameof(TexSubImage3D));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GenerateMipmap(TextureTarget target)
        {
            glGenerateMipmap(target);
            CheckError(nameof(GenerateMipmap));
        }

        #endregion

        #region Samplers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GenSampler()
        {
            return GenSamplers(1)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GenSamplers(int n)
        {
            var x = new uint[n];
            glGenSamplers(n, x);
            CheckError(nameof(GenSamplers));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteSamplers(int n, uint[] sampler)
        {
            glDeleteSamplers(1, sampler);
            CheckError(nameof(DeleteSamplers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteSampler(uint sampler)
        {
            DeleteSamplers(1, new[] { sampler });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteSampler(ref uint sampler)
        {
            DeleteTexture(sampler);
            sampler = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSampler(uint sampler)
        {
            var x = glIsSampler(sampler);
            CheckError(nameof(IsSampler));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSampler(uint unit, uint sampler)
        {
            glBindSampler(unit, sampler);
            CheckError(nameof(BindSampler));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetSamplerParameters(uint sampler, TextureParameter parameter, float[] result)
        {
            glGetSamplerParameterfv(sampler, parameter, result);
            CheckError(nameof(GetSamplerParameters));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetSamplerParameters(uint sampler, TextureParameter parameter, int[] result)
        {
            glGetSamplerParameteriv(sampler, parameter, result);
            CheckError(nameof(GetSamplerParameters));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SamplerParameter(uint sampler, TextureParameter parameter, float value)
        {
            glSamplerParameterf(sampler, parameter, value);
            CheckError(nameof(SamplerParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SamplerParameter(uint sampler, TextureParameter parameter, int value)
        {
            glSamplerParameteri(sampler, parameter, value);
            CheckError(nameof(SamplerParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SamplerParameter(uint sampler, TextureParameter parameter, float[] value)
        {
            glSamplerParameterfv(sampler, parameter, value);
            CheckError(nameof(SamplerParameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SamplerParameter(uint sampler, TextureParameter parameter, int[] value)
        {
            glSamplerParameteriv(sampler, parameter, value);
            CheckError(nameof(SamplerParameter));
        }

        #endregion

        #region Render State

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Enable(EnableCap enable)
        {
            glEnable(enable);
            CheckError(nameof(Enable));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Disable(EnableCap enable)
        {
            glDisable(enable);
            CheckError(nameof(Disable));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEnabled(EnableCap enable)
        {
            var x = glIsEnabled(enable);
            CheckError(nameof(IsEnabled));
            return x;
        }

        /// <summary>
        /// Clears the buffers of the currently bound framebuffer specified by the mask.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(ClearMask mask)
        {
            glClear(mask);
            CheckError(nameof(Clear));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetViewport(int x, int y, int width, int height)
        {
            glViewport(x, y, width, height);
            CheckError(nameof(SetViewport));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScissor(int x, int y, int width, int height)
        {
            glScissor(x, y, width, height);
            CheckError(nameof(SetScissor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FrontFace(FrontFaceMode mode)
        {
            glFrontFace(mode);
            CheckError(nameof(FrontFace));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCullFace(Face face)
        {
            glCullFace(face);
            CheckError(nameof(SetCullFace));
        }

        #region Color State

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetColorMask(bool r, bool g, bool b, bool a)
        {
            glColorMask(r, g, b, a);
            CheckError(nameof(SetColorMask));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClearColor(float r, float g, float b, float a)
        {
            glClearColor(r, g, b, a);
            CheckError(nameof(SetClearColor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClearColor(uint c)
        {
            // Extract from ARGB integer
            var a = ((c >> 24) & 255) / 255F;
            var r = ((c >> 16) & 255) / 255F;
            var g = ((c >> 8) & 255) / 255F;
            var b = (c & 255) / 255F;

            glClearColor(r, g, b, a);
            CheckError(nameof(SetClearColor));
        }

        //public static void SetClearColor( Color color )
        //{
        //    glClearColor( color.R, color.G, color.B, color.A );
        //    CheckError( "SetClearColor" );
        //}

        #endregion

        #region Depth State

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDepthMask(bool depth)
        {
            glDepthMask(depth);
            CheckError(nameof(SetDepthMask));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDepthFunction(DepthFunction func)
        {
            glDepthFunc(func);
            CheckError(nameof(SetDepthFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDepthRange(float near, float far)
        {
            glDepthRangef(near, far);
            CheckError(nameof(SetDepthRange));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClearDepth(float depth)
        {
            glClearDepthf(depth);
            CheckError(nameof(SetClearDepth));
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClearStencil(int stencil)
        {
            glClearStencil(stencil);
            CheckError(nameof(SetClearStencil));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLineWidth(float width)
        {
            glLineWidth(width);
            CheckError(nameof(SetLineWidth));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHint(Hint hint, HintMode mode)
        {
            glHint(hint, mode);
            CheckError(nameof(SetHint));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSampleCoverage(float val, bool invert)
        {
            glSampleCoverage(val, invert);
            CheckError(nameof(SetSampleCoverage));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPolygonOffset(float factor, float units)
        {
            glPolygonOffset(factor, units);
            CheckError(nameof(SetPolygonOffset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPixelStore(PixelStoreParameter pname, int value)
        {
            glPixelStorei(pname, value);
            CheckError(nameof(SetPixelStore));
        }

        /// <summary>
        /// Changes the read buffer ( where reading operations occur )
        /// </summary>
        /// <param name="mode"> The read mode/target </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetReadBuffer(FramebufferBuffer mode)
        {
            glReadBuffer(mode);
        }

        /// <summary>
        /// Changes the read buffer ( where reading operations occur )
        /// </summary>
        /// <param name="mode"> The read mode/target </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDrawBuffers(FramebufferBuffer[] mode)
        {
            glDrawBuffers(mode.Length, mode);
            CheckError(nameof(SetDrawBuffers));
        }

        /// <summary>
        /// Reads a block of pixels from the frame buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadPixels(int x, int y, int width, int height, ReadPixelsFormat format, ReadPixelsType type, void* data)
        {
            glReadPixels(x, y, width, height, format, type, data);
            CheckError(nameof(ReadPixels));
        }

        /// <summary>
        /// Reads a block of pixels from the frame buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadPixels(int x, int y, int width, int height, ReadPixelsFormat format, ReadPixelsType type, byte[] data)
        {
            fixed (void* ptr = data)
            {
                glReadPixels(x, y, width, height, format, type, ptr);
            }

            CheckError(nameof(ReadPixels));
        }

        /// <summary>
        /// Reads a block of pixels from the frame buffer. ( ReadPixelsFormat.RGBA and ReadPixelsType.UnsignedByte )
        /// </summary> 
        /// <seealso cref="ReadPixels(int, int, int, int, ReadPixelsFormat, ReadPixelsType, void*)"/>
        /// <seealso cref="ReadPixels(int, int, int, int, ReadPixelsFormat, ReadPixelsType, byte[])"/> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] ReadPixels(int x, int y, int width, int height)
        {
            var pixels = new uint[width * height];
            fixed (void* ptr = pixels)
            {
                glReadPixels(x, y, width, height, ReadPixelsFormat.RGBA, ReadPixelsType.UnsignedByte, ptr);
            }

            CheckError(nameof(ReadPixels));
            return pixels;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Finish()
        {
            glFinish();
            CheckError(nameof(Finish));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Flush()
        {
            glFlush();
            CheckError(nameof(Flush));
        }

        #endregion

        #region Blending

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBlendColor(float r, float g, float b, float a)
        {
            glBlendColor(r, g, b, a);
            CheckError(nameof(SetBlendColor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBlendEquation(BlendEquation eq)
        {
            glBlendEquation(eq);
            CheckError(nameof(SetBlendEquation));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBlendEquation(BlendEquation eqColor, BlendEquation eqAlpha)
        {
            glBlendEquationSeparate(eqColor, eqAlpha);
            CheckError(nameof(SetBlendEquation));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBlendFunction(BlendFunction source, BlendFunction destination)
        {
            glBlendFunc(source, destination);
            CheckError(nameof(SetBlendFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBlendFunction(BlendFunction sourceColor, BlendFunction destinationColor, BlendFunction sourceAlpha, BlendFunction destinationAlpha)
        {
            glBlendFuncSeparate(sourceColor, destinationColor, sourceAlpha, destinationAlpha);
            CheckError(nameof(SetBlendFunction));
        }

        #endregion

        #region Stencil

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StencilFunction(StencilFunction func, int refVal, uint mask)
        {
            glStencilFunc(func, refVal, mask);
            CheckError(nameof(StencilFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StencilFunction(Face face, StencilFunction func, int refVal, uint mask)
        {
            glStencilFuncSeparate(face, func, refVal, mask);
            CheckError(nameof(StencilFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetStencilMask(uint mask)
        {
            glStencilMask(mask);
            CheckError(nameof(SetStencilMask));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetStencilMask(Face face, uint mask)
        {
            glStencilMaskSeparate(face, mask);
            CheckError(nameof(SetStencilMask));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StencilOperation(StencilOperation stencilFail, StencilOperation depthFail, StencilOperation depthPass)
        {
            glStencilOp(stencilFail, depthFail, depthPass);
            CheckError(nameof(StencilOperation));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StencilOperation(Face face, StencilOperation stencilFail, StencilOperation depthFail, StencilOperation depthPass)
        {
            glStencilOpSeparate(face, stencilFail, depthFail, depthPass);
            CheckError(nameof(StencilOperation));
        }

        #endregion

        #region Query

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GenQueries(int n)
        {
            var x = new uint[n];
            glGenQueries(n, x);
            CheckError(nameof(GenQueries));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GenQuery()
        {
            return GenQueries(1)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteQueries(uint[] queries)
        {
            glDeleteQueries(queries.Length, queries);
            CheckError(nameof(DeleteQueries));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteQuery(uint query)
        {
            DeleteQueries(new[] { query });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteQuery(ref uint query)
        {
            DeleteQuery(query);
            query = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BeginQuery(QueryTarget target, uint query)
        {
            glBeginQuery(target, query);
            CheckError(nameof(BeginQuery));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EndQuery(QueryTarget target)
        {
            glEndQuery(target);
            CheckError(nameof(EndQuery));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetQuery(QueryTarget target, QueryParameter pname, int[] values)
        {
            glGetQueryiv(target, pname, values);
            CheckError(nameof(GetQuery));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetQueryObject(uint query, QueryObjectParameter pname, out uint value)
        {
            var result = new uint[1];
            glGetQueryObjectuiv(query, pname, result);
            CheckError(nameof(GetQueryObject));
            value = result[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsQuery(uint query)
        {
            var x = glIsQuery(query);
            CheckError(nameof(IsQuery));
            return x;
        }

        #endregion

        #region Sync

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong FenceSync(SyncFenceCondition condition, SyncFenceFlags flags)
        {
            var x = glFenceSync(condition, flags);
            CheckError(nameof(FenceSync));
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitSync(ulong sync, WaitSyncFlags flags)
        {
            glWaitSync(sync, flags, 0xFFFFFFFFFFFFFFFF);
            CheckError(nameof(WaitSync));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteSync(ulong sync)
        {
            glDeleteSync(sync);
            CheckError(nameof(DeleteSync));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSync(ulong sync)
        {
            var x = glIsSync(sync);
            CheckError(nameof(IsSync));
            return x;
        }

        #endregion

        /// <summary>
        /// An elaborate error check, calls <see cref="GetError"/> until no error is returned and concatinates a string response and throws exception.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckError(string command)
        {
            var foundError = false;
            ErrorCode err;

            var infloop = 0;
            while ((err = glGetError()) != ErrorCode.NoError && (++infloop < 10))
            {
                Console.WriteLine($"Error: {err} in {command}");
                foundError = true;
            }

            // 
            if (foundError)
            {
                throw new OpenGLException($"GL error detected in a call to {command}");
            }
        }
    }
}
