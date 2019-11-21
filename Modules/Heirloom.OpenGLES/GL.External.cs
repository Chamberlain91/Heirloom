using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Heirloom.OpenGLES
{
    public static unsafe partial class GL
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS0649  // Default value null
#pragma warning disable CS0169  // Unassigned

        #region Get

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ErrorCode _glGetError();
        private static _glGetError glGetError;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate sbyte* _glGetString(StringParameter name);
        private static _glGetString glGetString;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate sbyte* _glGetStringi(StringParameter name, int index);
        private static _glGetStringi glGetStringi;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetBooleanv(GetParameter enable, bool* values);
        private static _glGetBooleanv glGetBooleanv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetFloatv(GetParameter name, float* values);
        private static _glGetFloatv glGetFloatv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetIntegerv(GetParameter name, int* values);
        private static _glGetIntegerv glGetIntegerv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetInteger64v(GetParameter name, long* values);
        private static _glGetInteger64v glGetInteger64v;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetIntegeri_v(GetParameter name, int index, int* values);
        private static _glGetIntegeri_v glGetIntegeri_v;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetInteger64i_v(GetParameter name, int index, long* values);
        private static _glGetInteger64i_v glGetInteger64i_v;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetInternalformativ(InternalFormatTarget target, int internalFormat, InternalFormatParameter pname, int bufSize, int[] values);
        private static _glGetInternalformativ glGetInternalformativ;

        #endregion

        #region Draw

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDrawArrays(DrawMode mode, int first, int count);
        private static _glDrawArrays glDrawArrays;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDrawArraysInstanced(DrawMode mode, int first, int count, int primCount);
        private static _glDrawArraysInstanced glDrawArraysInstanced;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDrawRangeElements(DrawMode mode, int start, int end, int count, DrawElementType type, void* indices);
        private static _glDrawRangeElements glDrawRangeElements;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDrawElements(DrawMode mode, int count, DrawElementType type, void* indices);
        private static _glDrawElements glDrawElements;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDrawElementsInstanced(DrawMode mode, int count, DrawElementType type, void* indices, int primCount);
        private static _glDrawElementsInstanced glDrawElementsInstanced;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glEnableVertexAttribArray(uint index);
        private static _glEnableVertexAttribArray glEnableVertexAttribArray;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDisableVertexAttribArray(uint index);
        private static _glDisableVertexAttribArray glDisableVertexAttribArray;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetVertexAttribfv(uint index, VertexAttributeParameter pname, float* values);
        private static _glGetVertexAttribfv glGetVertexAttribfv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetVertexAttribiv(uint index, VertexAttributeParameter pname, int* values);
        private static _glGetVertexAttribiv glGetVertexAttribiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetVertexAttribPointerv(uint index, VertexAttributePointerParameter pname, IntPtr* values);
        private static _glGetVertexAttribPointerv glGetVertexAttribPointerv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glVertexAttribPointer(uint index, int size, VertexAttributeType type, bool normalized, int stride, IntPtr pointer);
        private static _glVertexAttribPointer glVertexAttribPointer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glVertexAttribDivisor(uint index, int divisor);
        private static _glVertexAttribDivisor glVertexAttribDivisor;

        #endregion

        #region Shaders

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint _glCreateShader(ShaderType type);
        private static _glCreateShader glCreateShader;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteShader(uint shader);
        private static _glDeleteShader glDeleteShader;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsShader(uint shader);
        private static _glIsShader glIsShader;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glShaderSource(uint shader, int count, string[] sources, int[] lengths);
        private static _glShaderSource glShaderSource;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetShaderSource(uint shader, int maxSourceLength, out int sourceLength, StringBuilder builder);
        private static _glGetShaderSource glGetShaderSource;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCompileShader(uint shader);
        private static _glCompileShader glCompileShader;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int _glShaderBinary(int shaderCount, uint[] shaders, uint format, byte[] binary, int binarySize);
        private static _glShaderBinary glShaderBinary;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetShaderiv(uint shader, ShaderParameter name, int[] result);
        private static _glGetShaderiv glGetShaderiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetShaderInfoLog(uint shader, int maxLength, out int outLength, StringBuilder outMessage);
        private static _glGetShaderInfoLog glGetShaderInfoLog;

        #endregion

        #region Shader Programs

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint _glCreateProgram();
        private static _glCreateProgram glCreateProgram;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteProgram(uint program);
        private static _glDeleteProgram glDeleteProgram;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsProgram(uint shader);
        private static _glIsProgram glIsProgram;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glAttachShader(uint program, uint shader);
        private static _glAttachShader glAttachShader;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDetachShader(uint program, uint shader);
        private static _glDetachShader glDetachShader;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glLinkProgram(uint program);
        private static _glLinkProgram glLinkProgram;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glValidateProgram(uint program);
        private static _glValidateProgram glValidateProgram;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint _glBindAttribLocation(uint program, uint index, string name);
        private static _glBindAttribLocation glBindAttribLocation;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetAttachedShaders(uint program, int maxCount, out int count, uint[] shaders);
        private static _glGetAttachedShaders glGetAttachedShaders;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetProgramiv(uint program, ProgramParameter name, int[] result);
        private static _glGetProgramiv glGetProgramiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetProgramInfoLog(uint program, int maxLength, out int outLength, StringBuilder outMessage);
        private static _glGetProgramInfoLog glGetProgramInfoLog;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUseProgram(uint program);
        private static _glUseProgram glUseProgram;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int _glGetProgramBinary(uint program, int bufSize, out int length, out uint format, byte[] binary);
        private static _glGetProgramBinary glGetProgramBinary;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int _glProgramBinary(uint program, uint format, byte[] binary, int binarySize);
        private static _glProgramBinary glProgramBinary;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int _glGetAttribLocation(uint program, string name);
        private static _glGetAttribLocation glGetAttribLocation;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int _glGetFragDataLocation(uint program, string name);
        private static _glGetFragDataLocation glGetFragDataLocation;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int _glGetUniformLocation(uint program, string name);
        private static _glGetUniformLocation glGetUniformLocation;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint _glGetUniformBlockIndex(uint program, string name);
        private static _glGetUniformBlockIndex glGetUniformBlockIndex;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetUniformIndices(uint program, int uniformCount, string[] uniformNames, uint[] uniformIndices);
        private static _glGetUniformIndices glGetUniformIndices;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetUniformiv(uint program, int location, int[] result);
        private static _glGetUniformiv glGetUniformiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetUniformuiv(uint program, int location, uint[] result);
        private static _glGetUniformuiv glGetUniformuiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetUniformfv(uint program, int location, float[] result);
        private static _glGetUniformfv glGetUniformfv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetActiveUniformsiv(uint program, int uniformCount, uint[] uniformIndices, UniformParameter pname, int[] result);
        private static _glGetActiveUniformsiv glGetActiveUniformsiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetActiveAttrib(uint program, uint index, int maxNameLength, out int nameLength, out int size, out ActiveAttribType type, StringBuilder name);
        private static _glGetActiveAttrib glGetActiveAttrib;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetActiveUniform(uint program, uint index, int maxNameLength, out int nameLength, out int size, out ActiveUniformType type, StringBuilder name);
        private static _glGetActiveUniform glGetActiveUniform;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetActiveUniformBlockName(uint program, uint blockIndex, int maxNameLength, out int nameLength, StringBuilder name);
        private static _glGetActiveUniformBlockName glGetActiveUniformBlockName;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetActiveUniformBlockiv(uint program, uint blockIndex, UniformBlockParameter pname, int[] result);
        private static _glGetActiveUniformBlockiv glGetActiveUniformBlockiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformBlockBinding(uint program, uint blockIndex, uint blockBinding);
        private static _glUniformBlockBinding glUniformBlockBinding;

        #region Singular Uniforms

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform1f(int location, float v);
        private static _glUniform1f glUniform1f;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform2f(int location, float v1, float v2);
        private static _glUniform2f glUniform2f;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform3f(int location, float v1, float v2, float v3);
        private static _glUniform3f glUniform3f;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform4f(int location, float v1, float v2, float v3, float v4);
        private static _glUniform4f glUniform4f;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform1i(int location, int v);
        private static _glUniform1i glUniform1i;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform2i(int location, int v1, int v2);
        private static _glUniform2i glUniform2i;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform3i(int location, int v1, int v2, int v3);
        private static _glUniform3i glUniform3i;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform4i(int location, int v1, int v2, int v3, int v4);
        private static _glUniform4i glUniform4i;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform1ui(int location, uint v);
        private static _glUniform1ui glUniform1ui;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform2ui(int location, uint v1, uint v2);
        private static _glUniform2ui glUniform2ui;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform3ui(int location, uint v1, uint v2, uint v3);
        private static _glUniform3ui glUniform3ui;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform4ui(int location, uint v1, uint v2, uint v3, uint v4);
        private static _glUniform4ui glUniform4ui;

        #endregion

        #region Array Uniforms

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform1fv(int location, int count, float* ptr);
        private static _glUniform1fv glUniform1fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform2fv(int location, int count, float* ptr);
        private static _glUniform2fv glUniform2fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform3fv(int location, int count, float* ptr);
        private static _glUniform3fv glUniform3fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform4fv(int location, int count, float* ptr);
        private static _glUniform4fv glUniform4fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform1iv(int location, int count, int* ptr);
        private static _glUniform1iv glUniform1iv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform2iv(int location, int count, int* ptr);
        private static _glUniform2iv glUniform2iv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform3iv(int location, int count, int* ptr);
        private static _glUniform3iv glUniform3iv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform4iv(int location, int count, int* ptr);
        private static _glUniform4iv glUniform4iv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform1uiv(int location, int count, uint* ptr);
        private static _glUniform1uiv glUniform1uiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform2uiv(int location, int count, uint* ptr);
        private static _glUniform2uiv glUniform2uiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform3uiv(int location, int count, uint* ptr);
        private static _glUniform3uiv glUniform3uiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniform4uiv(int location, int count, uint* ptr);
        private static _glUniform4uiv glUniform4uiv;

        #endregion

        #region Matrix Uniforms

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix2fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix2fv glUniformMatrix2fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix2x3fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix2x3fv glUniformMatrix2x3fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix2x4fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix2x4fv glUniformMatrix2x4fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix3fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix3fv glUniformMatrix3fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix3x2fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix3x2fv glUniformMatrix3x2fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix3x4fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix3x4fv glUniformMatrix3x4fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix4fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix4fv glUniformMatrix4fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix4x2fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix4x2fv glUniformMatrix4x2fv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glUniformMatrix4x3fv(int location, int count, bool transpose, float* ptr);
        private static _glUniformMatrix4x3fv glUniformMatrix4x3fv;

        #endregion

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glReleaseShaderCompiler();
        private static _glReleaseShaderCompiler glReleaseShaderCompiler;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int _glGetShaderPrecisionFormat(ShaderType shaderType, ShaderPrecision precisionType, int[] range, int[] precision);
        private static _glGetShaderPrecisionFormat glGetShaderPrecisionFormat;

        #endregion

        #region Buffers

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenBuffers(int n, uint[] buffers);
        private static _glGenBuffers glGenBuffers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteBuffers(int n, uint[] buffers);
        private static _glDeleteBuffers glDeleteBuffers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsBuffer(uint buffer);
        private static _glIsBuffer glIsBuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetBufferParameteriv(BufferTarget target, BufferParameter parameter, int[] result);
        private static _glGetBufferParameteriv glGetBufferParameteriv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBindBuffer(BufferTarget target, uint buffer);
        private static _glBindBuffer glBindBuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBindBufferBase(BufferTarget target, uint index, uint buffer);
        private static _glBindBufferBase glBindBufferBase;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBufferData(BufferTarget target, uint size, IntPtr data, BufferUsage usage);
        private static _glBufferData glBufferData;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBufferSubData(BufferTarget target, uint offset, uint size, IntPtr data);
        private static _glBufferSubData glBufferSubData;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void* _glMapBufferRange(BufferTarget target, int offset, int size, MapBufferAccess access);
        private static _glMapBufferRange glMapBufferRange;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glUnmapBuffer(BufferTarget targets);
        private static _glUnmapBuffer glUnmapBuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glFlushMappedBufferRange(BufferTarget target, int offset, int size);
        private static _glFlushMappedBufferRange glFlushMappedBufferRange;

        #endregion

        #region Vertex Array

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenVertexArrays(int n, uint[] arrays);
        private static _glGenVertexArrays glGenVertexArrays;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteVertexArrays(int n, uint[] arrays);
        private static _glDeleteVertexArrays glDeleteVertexArrays;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsVertexArray(uint array);
        private static _glIsVertexArray glIsVertexArray;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBindVertexArray(uint array);
        private static _glBindVertexArray glBindVertexArray;

        #endregion

        #region Framebuffers

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenFramebuffers(int n, uint[] framebuffers);
        private static _glGenFramebuffers glGenFramebuffers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteFramebuffers(int n, uint[] framebuffers);
        private static _glDeleteFramebuffers glDeleteFramebuffers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsFramebuffer(uint framebuffer);
        private static _glIsFramebuffer glIsFramebuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetFramebufferAttachmentParameteriv(FramebufferTarget target, FramebufferAttachment attachment, FramebufferAttachmentParameter name, int[] result);
        private static _glGetFramebufferAttachmentParameteriv glGetFramebufferAttachmentParameteriv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBindFramebuffer(FramebufferTarget target, uint framebuffer);
        private static _glBindFramebuffer glBindFramebuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FramebufferStatus _glCheckFramebufferStatus(FramebufferTarget target);
        private static _glCheckFramebufferStatus glCheckFramebufferStatus;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glFramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, TextureImageTarget textarget, uint texture, int level);
        private static _glFramebufferTexture2D glFramebufferTexture2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glFramebufferTextureLayer(FramebufferTarget target, FramebufferAttachment attachment, uint texture, int level, int layer);
        private static _glFramebufferTextureLayer glFramebufferTextureLayer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glFramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment, RenderbufferTarget textarget, uint renderbuffer);
        private static _glFramebufferRenderbuffer glFramebufferRenderbuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, FramebufferBlitMask mask, FramebufferBlitFilter filter);
        private static _glBlitFramebuffer glBlitFramebuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glInvalidateFramebuffer(FramebufferTarget target, int numAttachments, FramebufferAttachment[] attachments);
        private static _glInvalidateFramebuffer glInvalidateFramebuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glInvalidateSubFramebuffer(FramebufferTarget target, int numAttachments, FramebufferAttachment[] attachments, int x, int y, int width, int height);
        private static _glInvalidateSubFramebuffer glInvalidateSubFramebuffer;

        #endregion

        #region Renderbuffers

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenRenderbuffers(int n, uint[] renderbuffers);
        private static _glGenRenderbuffers glGenRenderbuffers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteRenderbuffers(int n, uint[] renderbuffers);
        private static _glDeleteRenderbuffers glDeleteRenderbuffers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsRenderbuffer(uint renderbuffer);
        private static _glIsRenderbuffer glIsRenderbuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBindRenderbuffer(RenderbufferTarget target, uint renderbuffer);
        private static _glBindRenderbuffer glBindRenderbuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetRenderbufferParameteriv(RenderbufferTarget target, RenderbufferValue name, int[] result);
        private static _glGetRenderbufferParameteriv glGetRenderbufferParameteriv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glRenderbufferStorage(RenderbufferTarget target, RenderbufferFormat format, int width, int height);
        private static _glRenderbufferStorage glRenderbufferStorage;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glRenderbufferStorageMultisample(RenderbufferTarget target, int samples, RenderbufferFormat format, int width, int height);
        private static _glRenderbufferStorageMultisample glRenderbufferStorageMultisample;

        #endregion

        #region Textures

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenTextures(int n, uint[] textures);
        private static _glGenTextures glGenTextures;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteTextures(int n, uint[] textures);
        private static _glDeleteTextures glDeleteTextures;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsTexture(uint texture);
        private static _glIsTexture glIsTexture;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetTexParameterfv(TextureTarget target, TextureParameter parameter, float[] result);
        private static _glGetTexParameterfv glGetTexParameterfv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetTexParameteriv(TextureTarget target, TextureParameter parameter, int[] result);
        private static _glGetTexParameteriv glGetTexParameteriv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexParameterf(TextureTarget target, TextureParameter parameter, float value);
        private static _glTexParameterf glTexParameterf;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexParameteri(TextureTarget target, TextureParameter parameter, int value);
        private static _glTexParameteri glTexParameteri;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexParameterfv(TextureTarget target, TextureParameter parameter, float[] value);
        private static _glTexParameterfv glTexParameterfv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexParameteriv(TextureTarget target, TextureParameter parameter, int[] value);
        private static _glTexParameteriv glTexParameteriv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBindTexture(TextureTarget target, uint texture);
        private static _glBindTexture glBindTexture;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glActiveTexture(uint texture);
        private static _glActiveTexture glActiveTexture;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexImage2D(TextureImageTarget target, int level, TextureSizedFormat internalFormat, int width, int height, int border, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data);
        private static _glTexImage2D glTexImage2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexImage3D(TextureImageTarget target, int level, TextureSizedFormat internalFormat, int width, int height, int depth, int border, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data);
        private static _glTexImage3D glTexImage3D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexStorage2D(TextureImageTarget target, int levels, TextureSizedFormat internalFormat, int width, int height);
        private static _glTexStorage2D glTexStorage2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexStorage3D(TextureImageTarget target, int levels, TextureSizedFormat internalFormat, int width, int height, int depth);
        private static _glTexStorage3D glTexStorage3D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexSubImage2D(TextureImageTarget target, int level, int xoffset, int yoffset, int width, int height, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data);
        private static _glTexSubImage2D glTexSubImage2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glTexSubImage3D(TextureImageTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, TexturePixelFormat format, TexturePixelType pixelFormat, IntPtr data);
        private static _glTexSubImage3D glTexSubImage3D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCompressedTexImage2D(TextureImageTarget target, int level, TextureCompressedFormat internalFormat, int width, int height, int border, int imageSize, IntPtr data);
        private static _glCompressedTexImage2D glCompressedTexImage2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCompressedTexImage3D(TextureImageTarget target, int level, TextureCompressedFormat internalFormat, int width, int height, int depth, int border, int imageSize, IntPtr data);
        private static _glCompressedTexImage3D glCompressedTexImage3D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCompressedTexSubImage2D(TextureImageTarget target, int level, int xoffset, int yoffset, int width, int height, int imageSize, IntPtr data);
        private static _glCompressedTexSubImage2D glCompressedTexSubImage2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCompressedTexSubImage3D(TextureImageTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int imageSize, IntPtr data);
        private static _glCompressedTexSubImage3D glCompressedTexSubImage3D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCopyTexImage2D(TextureImageTarget target, int level, TexturePixelFormat internalFormat, int x, int y, int width, int height, int border);
        private static _glCopyTexImage2D glCopyTexImage2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCopyTexSubImage2D(TextureImageTarget target, int level, int xoffset, int yoffset, int x, int y, int width, int height);
        private static _glCopyTexSubImage2D glCopyTexSubImage2D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCopyTexSubImage3D(TextureImageTarget target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height, int depth);
        private static _glCopyTexSubImage3D glCopyTexSubImage3D;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenerateMipmap(TextureTarget target);
        private static _glGenerateMipmap glGenerateMipmap;

        #endregion

        #region Samplers

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenSamplers(int n, uint[] samplers);
        private static _glGenSamplers glGenSamplers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteSamplers(int n, uint[] samplers);
        private static _glDeleteSamplers glDeleteSamplers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBindSampler(uint unit, uint sampler);
        private static _glBindSampler glBindSampler;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsSampler(uint sampler);
        private static _glIsSampler glIsSampler;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glSamplerParameterf(uint sampler, TextureParameter parameter, float value);
        private static _glSamplerParameterf glSamplerParameterf;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glSamplerParameteri(uint sampler, TextureParameter parameter, int value);
        private static _glSamplerParameteri glSamplerParameteri;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glSamplerParameterfv(uint sampler, TextureParameter parameter, float[] value);
        private static _glSamplerParameterfv glSamplerParameterfv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glSamplerParameteriv(uint sampler, TextureParameter parameter, int[] value);
        private static _glSamplerParameteriv glSamplerParameteriv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetSamplerParameterfv(uint sampler, TextureParameter parameter, float[] result);
        private static _glGetSamplerParameterfv glGetSamplerParameterfv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetSamplerParameteriv(uint sampler, TextureParameter parameter, int[] result);
        private static _glGetSamplerParameteriv glGetSamplerParameteriv;

        #endregion

        #region Blending

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBlendColor(float r, float g, float b, float a);
        private static _glBlendColor glBlendColor;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBlendEquation(BlendEquation eq);
        private static _glBlendEquation glBlendEquation;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBlendEquationSeparate(BlendEquation eqColor, BlendEquation eqAlpha);
        private static _glBlendEquationSeparate glBlendEquationSeparate;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBlendFunc(BlendFunction source, BlendFunction destination);
        private static _glBlendFunc glBlendFunc;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBlendFuncSeparate(BlendFunction sourceColor, BlendFunction destinationColor, BlendFunction sourceAlpha, BlendFunction destinationAlpha);
        private static _glBlendFuncSeparate glBlendFuncSeparate;

        #endregion

        #region Stencil

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glStencilFunc(StencilFunction func, int _ref, uint mask);
        private static _glStencilFunc glStencilFunc;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glStencilFuncSeparate(Face face, StencilFunction func, int _ref, uint mask);
        private static _glStencilFuncSeparate glStencilFuncSeparate;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glStencilMask(uint mask);
        private static _glStencilMask glStencilMask;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glStencilMaskSeparate(Face face, uint mask);
        private static _glStencilMaskSeparate glStencilMaskSeparate;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glStencilOp(StencilOperation sfail, StencilOperation dfail, StencilOperation dpass);
        private static _glStencilOp glStencilOp;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glStencilOpSeparate(Face face, StencilOperation sfail, StencilOperation dfail, StencilOperation dpass);
        private static _glStencilOpSeparate glStencilOpSeparate;

        #endregion

        #region Rendering

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClear(ClearMask mask);
        private static _glClear glClear;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClearBufferiv(uint buffer, int drawBuffer, int[] value);
        private static _glClearBufferiv glClearBufferiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClearBufferuiv(uint buffer, int drawBuffer, uint[] value);
        private static _glClearBufferuiv glClearBufferuiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClearBufferfv(uint buffer, int drawBuffer, float[] value);
        private static _glClearBufferfv glClearBufferfv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClearBufferfi(uint buffer, int drawBuffer, float depth, int stencil);
        private static _glClearBufferfi glClearBufferfi;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClearColor(float r, float g, float b, float a);
        private static _glClearColor glClearColor;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClearDepthf(float d);
        private static _glClearDepthf glClearDepthf;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glClearStencil(int stencil);
        private static _glClearStencil glClearStencil;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glReadBuffer(FramebufferBuffer mode);
        private static _glReadBuffer glReadBuffer;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDrawBuffers(int n, FramebufferBuffer[] bufs);
        private static _glDrawBuffers glDrawBuffers;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glReadPixels(int x, int y, int width, int height, ReadPixelsFormat format, ReadPixelsType type, void* data);
        private static _glReadPixels glReadPixels;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glFinish();
        private static _glFinish glFinish;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glFlush();
        private static _glFlush glFlush;

        #endregion

        #region Query

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glBeginQuery(QueryTarget target, uint id);
        private static _glBeginQuery glBeginQuery;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glEndQuery(QueryTarget target);
        private static _glEndQuery glEndQuery;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGenQueries(int n, uint[] queries);
        private static _glGenQueries glGenQueries;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteQueries(int n, uint[] queries);
        private static _glDeleteQueries glDeleteQueries;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetQueryObjectuiv(uint id, QueryObjectParameter pname, uint[] values);
        private static _glGetQueryObjectuiv glGetQueryObjectuiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetQueryiv(QueryTarget target, QueryParameter pname, int[] values);
        private static _glGetQueryiv glGetQueryiv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsQuery(uint query);
        private static _glIsQuery glIsQuery;

        #endregion

        #region Sync

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ClientWaitSyncResult _glClientWaitSync(ulong sync, ClientWaitSyncFlags flags, ulong timeout);
        private static _glClientWaitSync glClientWaitSync;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong _glFenceSync(SyncFenceCondition condition, SyncFenceFlags flags);
        private static _glFenceSync glFenceSync;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDeleteSync(ulong sync);
        private static _glDeleteSync glDeleteSync;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glGetSynciv(ulong sync, SyncParameter pname, int bufSize, out int length, int[] values);
        private static _glGetSynciv glGetSynciv;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glWaitSync(ulong sync, WaitSyncFlags flags, ulong timeout);
        private static _glWaitSync glWaitSync;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsSync(ulong sync);
        private static _glIsSync glIsSync;

        #endregion

        #region State Management

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glColorMask(bool r, bool g, bool b, bool a);
        private static _glColorMask glColorMask;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDepthMask(bool depth);
        private static _glDepthMask glDepthMask;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDepthFunc(DepthFunction func);
        private static _glDepthFunc glDepthFunc;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDepthRangef(float near, float far);
        private static _glDepthRangef glDepthRangef;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glViewport(int x, int y, int width, int height);
        private static _glViewport glViewport;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glScissor(int x, int y, int width, int height);
        private static _glScissor glScissor;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glFrontFace(FrontFaceMode face);
        private static _glFrontFace glFrontFace;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glCullFace(Face face);
        private static _glCullFace glCullFace;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glEnable(EnableCap enable);
        private static _glEnable glEnable;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glDisable(EnableCap enable);
        private static _glDisable glDisable;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool _glIsEnabled(EnableCap enable);
        private static _glIsEnabled glIsEnabled;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glHint(Hint hint, HintMode mode);
        private static _glHint glHint;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glLineWidth(float width);
        private static _glLineWidth glLineWidth;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glPixelStorei(PixelStoreParameter pname, int param);
        private static _glPixelStorei glPixelStorei;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glPolygonOffset(float factor, float units);
        private static _glPolygonOffset glPolygonOffset;

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void _glSampleCoverage(float value, bool invert);
        private static _glSampleCoverage glSampleCoverage;

        #endregion

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS0649  // Default value null
#pragma warning restore CS0169  // Unassigned
    }
}
