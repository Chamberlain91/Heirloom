using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a simple pool of integer identifiers.
    /// </summary>
    public sealed class IdentifierPool
    {
        private readonly List<int> _recycle = new List<int>();
        private int _counter;

        /// <summary>
        /// Gets the next available identifier in the pool.
        /// </summary>
        /// <exception cref="InvalidOperationException">Raised when no more identifiers can be generated (int.MaxValue).</exception>
        public int GetNextIdentifier()
        {
            if (_recycle.Count == 0)
            {
                if (_counter == int.MaxValue)
                {
                    throw new InvalidOperationException($"{nameof(IdentifierPool)} exceeded int.MaxValue number of identifiers.");
                }

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
        public void Recycle(int identifier)
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
