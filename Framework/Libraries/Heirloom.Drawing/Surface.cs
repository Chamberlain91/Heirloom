using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public abstract class Surface : ImageSource, IDisposable
    {
        #region Constructors

        protected Surface(IntSize size)
        {
            Size = size;
        }

        ~Surface()
        {
            DisposeInternal(false);
        }

        #endregion

        #region Properties

        public bool IsDisposed { get; private set; } = false;

        public override IntSize Size { get; protected set; }

        public int Width => Size.Width;

        public int Height => Size.Height;

        #endregion

        protected abstract void Dispose(bool managed);

        #region Dispose

        protected virtual void DisposeInternal(bool managed)
        {
            if (!IsDisposed)
            {
                Dispose(managed);
                IsDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
