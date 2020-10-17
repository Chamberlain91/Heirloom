using System;

namespace Meadows.Drawing
{
    public abstract class NativeResource
    {
        private object[] _contextObjects = Array.Empty<object>();
        private object _globalObject;

        private bool _isVersionDirty;
        private uint _version;

        /// <summary>
        /// The version of this resource. It is incremented when changes are made to the object.
        /// </summary>
        public uint Version
        {
            get
            {
                if (_isVersionDirty)
                {
                    // Increment version number
                    if (_version == uint.MaxValue) { _version = 0; }
                    else { _version++; }

                    // Mark as no longer dirty
                    _isVersionDirty = false;
                }

                return _version;
            }
        }

        internal T GetGlobalNativeObject<T>() where T : class
        {
            return _globalObject as T;
        }

        internal void SetGlobalNativeObject<T>(T obj) where T : class
        {
            _globalObject = obj;
        }

        internal T GetContextNativeObject<T>(GraphicsContext context) where T : class
        {
            if (_contextObjects.Length <= context.Id) { return null; }
            return _contextObjects[context.Id] as T;
        }

        internal void SetContextNativeObject<T>(GraphicsContext context, T native) where T : class
        {
            // Resize storage to accomodate context id. This should be a fairly small number, as a typical application
            // will only have at most 10s of contexts thus only tens of ids.
            if (_contextObjects.Length <= context.Id) { Array.Resize(ref _contextObjects, context.Id + 1); }
            // Store the native resource
            _contextObjects[context.Id] = native;
        }

        protected internal void IncrementVersion()
        {
            _isVersionDirty = true;
        }
    }
}
