using System;

namespace Meadows.Drawing
{

    public abstract class GraphicsResource : IDisposable
    {
        private IDisposable[] _contextObjects = Array.Empty<IDisposable>();
        private IDisposable _backendObject;

        private readonly Version _version = new();

        /// <summary>
        /// The version of this resource. It is incremented when changes are made to the object.
        /// </summary>
        public uint Version => _version;

        ~GraphicsResource()
        {
            Dispose(disposing: false);
        }

        protected internal void IncrementVersion()
        {
            _version.Increment();
        }

        #region Backend Associated Native Objects

        internal IDisposable GetBackendNativeObject()
        {
            return _backendObject;
        }

        internal void SetBackendNativeObject(IDisposable obj)
        {
            _backendObject = obj;
        }

        #endregion

        #region Context Associated Native Objects

        internal IDisposable GetContextNativeObject(GraphicsContext context)
        {
            if (_contextObjects.Length <= context.Id) { return default; }
            else
            {
                return _contextObjects[context.Id];
            }
        }

        internal void SetContextNativeObject(GraphicsContext context, IDisposable obj)
        {
            // Resize storage to accomodate context id. This should be a fairly small number, as applications
            // usually do not have hundreds of windows/displays. For example a game would likely have only one.
            if (_contextObjects.Length <= context.Id) { Array.Resize(ref _contextObjects, context.Id + 1); }

            // Store the context resource
            _contextObjects[context.Id] = obj;
        }

        #endregion

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Nothing to do...?
                }

                // Dispose each context associated object
                for (var id = 0; id < _contextObjects.Length; id++)
                {
                    _contextObjects[id]?.Dispose();
                }

                // Dispose backend associated object 
                _backendObject?.Dispose();

                // Clear context objects
                _contextObjects = Array.Empty<IDisposable>();
                _backendObject = null;

                // 
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
