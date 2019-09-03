using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Runtime
{
    public abstract class ComponentSystem : IDisposable
    {
        private readonly HashSet<Entity> _entities;
        private readonly Type[] _typeFilter;

        protected ComponentSystem(params Type[] typeFilter)
        {
            if (typeFilter == null) { throw new ArgumentNullException(nameof(typeFilter)); }
            if (typeFilter.Length == 0) { throw new ArgumentException("Must specify at least one type.", nameof(typeFilter)); }

            // Ensure each type is a component
            foreach (var type in typeFilter)
            {
                if (!type.IsSubclassOf(typeof(Component)))
                {
                    throw new InvalidOperationException($"Unable to set type filter for component system, type '{type.Name}' was not a component.");
                }
            }

            // 
            _typeFilter = typeFilter.ToArray();

            // 
            _entities = new HashSet<Entity>();
        }

        internal IEnumerable<Entity> Entities => _entities;

        internal bool CheckTypeFilter(Entity entity)
        {
            // Check the entity against our type filter
            foreach (var type in _typeFilter)
            {
                // If *no* component exists that fits the type, return false.
                if (!entity.HasComponent(type))
                {
                    return false;
                }
            }

            // A component of each type does exist on the entity
            return true;
        }

        internal void AddEntity(Entity entity)
        {
            if (CheckTypeFilter(entity))
            {
                AddEntityInternal(entity);
            }
        }

        internal void RemoveEntity(Entity entity)
        {
            RemoveEntityInternal(entity);
        }

        internal void EntityChanged(Entity entity)
        {
            if (CheckTypeFilter(entity))
            {
                if (_entities.Contains(entity))
                {
                    // Matched, already contained
                }
                else
                {
                    // Matched, but not contained
                    AddEntityInternal(entity);
                }
            }
            else
            {
                if (_entities.Contains(entity))
                {
                    // Not matched, but contained
                    RemoveEntityInternal(entity);
                }
                else
                {
                    // Not matched, but not contained
                }
            }
        }

        private void AddEntityInternal(Entity entity)
        {
            if (_entities.Add(entity))
            {
                OnAddEntity(entity);
            }
        }

        private void RemoveEntityInternal(Entity entity)
        {
            if (_entities.Remove(entity))
            {
                OnRemoveEntity(entity);
            }
        }

        internal protected abstract void Update();

        internal protected abstract void FixedUpdate();

        protected abstract void OnAddEntity(Entity entity);

        protected abstract void OnRemoveEntity(Entity entity);

        public abstract void Dispose();
    }
}
