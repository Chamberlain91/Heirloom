using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    /// <summary>
    /// Manages objects by their type hierarchy up to the base type, allowing access by enumeration of objects by type.
    /// </summary>
    /// <typeparam name="TBase">Some base type</typeparam>
    public class TypeDictionary<TBase> : ITypeDictionary<TBase>
    {
        private readonly Dictionary<Type, List<TBase>> _typeLists;
        private readonly HashSet<TBase> _items;

        public TypeDictionary()
        {
            _typeLists = new Dictionary<Type, List<TBase>>();
            _items = new HashSet<TBase>();
        }

        public void Add(TBase item)
        {
            if (item == null) { throw new ArgumentNullException(nameof(item)); }

            // Try to add item to bucket, was it already present?
            if (_items.Add(item))
            {
                // Was not present, add association with each ancestor type
                foreach (var type in GetAncestorTypes(item.GetType()))
                {
                    var list = GetListByType(type);
                    list.Add(item);
                }
            }
        }

        public bool Remove(TBase item)
        {
            // Try to remove item from bucket
            if (_items.Remove(item))
            {
                // Was removed, remove associate from each ancestor type
                foreach (var type in GetAncestorTypes(item.GetType()))
                {
                    var list = GetListByType(type);
                    list.Remove(item);
                }

                return true;
            }
            else
            {
                // Couldn't remove, never contained it.
                return false;
            }
        }

        public bool Contains(TBase item)
        {
            return _items.Contains(item);
        }

        public bool ContainsType<X>() where X : TBase
        {
            return _typeLists.ContainsKey(typeof(X));
        }

        public IEnumerable<X> Enumerate<X>() where X : TBase
            // TODO: Better name?
        {
            if (ContainsType<X>())
            {
                // Specific type list
                return _typeLists[typeof(X)] as List<X>;
            }
            else
            {
                // Nothing
                return Enumerable.Empty<X>();
            }
        }

        private List<TBase> GetListByType(Type type)
        {
            // Create type list if not already present
            if (!_typeLists.ContainsKey(type))
            {
                // TODO: Actually create polymorphic List<X>
                _typeLists[type] = new List<TBase>();
            }

            // Return appropriate type list
            return _typeLists[type];
        }

        private static IEnumerable<Type> GetAncestorTypes(Type type)
        {
            while (type != typeof(TBase))
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}
