using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a simple pool of integer identifiers.
    /// </summary>
    public sealed class IdentifierPool
    {
        private readonly List<uint> _recycle = new List<uint>();
        private uint _counter;

        /// <summary>
        /// Gets the next available identifier in the pool.
        /// </summary>
        public uint GetNextIdentifier()
        {
            if (_recycle.Count == 0)
            {
                // Generate a new identifier
                return _counter++;
            }
            else
            {
                // Get the next available identifier
                var last = _recycle.Count - 1;
                var identifier = _recycle[last];
                _recycle.RemoveAt(last);
                return identifier;
            }
        }

        /// <summary>
        /// Recycles an identifier, placing it back into the pool.
        /// </summary>
        public void RecycleIdentifier(uint identifier)
        {
            if (_recycle.Contains(identifier))
            {
                throw new ArgumentException($"Identifier '{identifier}' already has been recycled.");
            }
            if (identifier >= _counter)
            {
                throw new ArgumentException($"Identifier '{identifier}' was never generated.");
            }
            else
            {
                _recycle.Add(identifier);
            }
        }
    }
}
