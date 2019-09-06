using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGL
{
    internal class GLBuffer : IDisposable
    {
        private bool _isDisposed = false;

        #region Constructors

        public GLBuffer(BufferTarget target, uint capacity)
        {
            Capacity = capacity;
            Target = target;

            // Create buffer
            Handle = GL.GenBuffer();
            GL.BindBuffer(Target, Handle);
            GL.BufferData(Target, Capacity, IntPtr.Zero, BufferUsage.Stream);
            GL.BindBuffer(Target, 0);
        }

        ~GLBuffer()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        internal uint Handle { get; }

        internal BufferTarget Target { get; }

        internal uint Capacity { get; }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Bind()
        {
            GL.BindBuffer(Target, Handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Update(void* data, int offset, int size)
        {
            // 
            Bind();

            // Update data
            if (offset == 0 && size == Capacity) { GL.BufferData(Target, (uint) size, (IntPtr) data, BufferUsage.Stream); }
            else { GL.BufferSubData(Target, (uint) offset, (uint) size, (IntPtr) data); }

            //var ptr = GL.MapBufferRange(Target, offset, size, MapBufferAccess.Write | MapBufferAccess.InvalidateRange);
            //Buffer.MemoryCopy(data, ptr, size, size);
            //GL.UnmapBuffer(Target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Update<T>(T[] data, int count, int offset) where T : struct
        {
            var size = Marshal.SizeOf<T>() * count;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Update((void*) handle.AddrOfPinnedObject(), offset, size);
            handle.Free();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Update<T>(T[] data, int offset) where T : struct
        {
            Update(data, data.Length, offset);
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
                Console.WriteLine("WARN: Disposing Buffer! OpenGL Resource Not Deleted.");

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
