using System;

namespace Meadows.Drawing
{
    public abstract class NativeResource
    {
        private object[] _objects = Array.Empty<object>();

        internal uint Version { get; private set; }

        internal T GetNativeResource<T>(GraphicsContext context) where T : class
        {
            if (_objects.Length <= context.Id) { return null; }
            return _objects[context.Id] as T;
        }

        internal void SetNativeResource<T>(GraphicsContext context, T native) where T : class
        {
            // Resize storage to accomodate context id. This should be a fairly small number, as a typical application
            // will only have at most 10s of contexts thus only tens of ids.
            if (_objects.Length <= context.Id) { Array.Resize(ref _objects, context.Id + 1); }
            // Store the native resource
            _objects[context.Id] = native;
        }

        protected internal void IncrementVersion()
        {
            Version++;

            if (Version == uint.MaxValue)
            {
                Version = 0;
            }
        }
    }
}
