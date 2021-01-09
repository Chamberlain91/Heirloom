using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class ESBuffer : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// The buffer target.
        /// </summary>
        public readonly BufferTarget Target;

        /// <summary>
        /// The buffer handle (or name).
        /// </summary>
        public readonly uint Handle;

        /// <summary>
        /// The size of the buffer in bytes.
        /// </summary>
        public readonly uint Size;

        #region Constructors

        protected ESBuffer(BufferTarget target, uint sizeInBytes)
        {
            Target = target;
            Size = sizeInBytes;

            // Create buffer
            Handle = GLES.GenBuffer();
            GLES.BindBuffer(Target, Handle);
            GLES.BufferData(Target, sizeInBytes, IntPtr.Zero, BufferUsage.Stream);
            GLES.BindBuffer(Target, 0);
        }

        ~ESBuffer()
        {
            Dispose(false);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Bind()
        {
            GLES.BindBuffer(Target, Handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe void Update(void* data, int offset, int size)
        {
            // Bind the buffer...?
            GLES.BindBuffer(Target, Handle);

            // Submit new data...
            if (offset == 0 && size == Size) { GLES.BufferData(Target, (uint) size, (IntPtr) data, BufferUsage.Stream); }
            else { GLES.BufferSubData(Target, (uint) offset, (uint) size, (IntPtr) data); }

            // var ptr = GL.MapBufferRange(Target, offset, size, MapBufferAccess.Write | MapBufferAccess.InvalidateRange);
            // Buffer.MemoryCopy(data, ptr, size, size);
            // GL.UnmapBuffer(Target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe void Update<TStruct>(TStruct[] data, int count, int offset = 0)
            where TStruct : struct
        {
            var size = Marshal.SizeOf<TStruct>() * count;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Update((void*) handle.AddrOfPinnedObject(), offset, size);
            handle.Free();
        }

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // 
                }

                // Schedule for deletion on a GL thread.
                ESGraphicsBackend.Current.Invoke(() =>
                {
                    Log.Debug($"[Dispose] {GetType().Name} ({Handle}).");
                    GLES.DeleteBuffer(Handle);
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
    }
}
