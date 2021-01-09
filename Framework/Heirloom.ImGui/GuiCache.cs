using System.Collections.Generic;

namespace Heirloom.UI
{
    internal sealed class GuiCache
    {
        private readonly Dictionary<int, Record> _cache = new();
        // todo: LRU/TimeOut cache to prevent "memory leak" behaviour

        public void Update()
        {
            // Tick!
            // todo: Remove elements that have timed out
        }

        public T GetState<T>(GuiIdentifier identifier) where T : class, new()
        {
            if (_cache.TryGetValue(identifier, out var record))
            {
                return (T) record.Object;
            }

            // Record did not exist, create record and 
            record = new Record { Object = new T() };
            _cache[identifier] = record;
            return (T) record.Object;
        }

        private sealed class Record
        {
            public object Object;
            // todo: LastModified for LRU cache stuff
        }
    }
}
