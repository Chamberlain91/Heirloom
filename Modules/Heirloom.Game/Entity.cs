using System;
using System.Collections.Generic;

using Heirloom.Collections;

namespace Heirloom.Game
{
    public class Entity
    {
        private readonly TypeDictionary<Component> _components;
        private bool _isContainedByScene = false;

        public Entity()
        {
            _components = new TypeDictionary<Component>();

            // 
            Transform = AddComponent(new Transform());
        }

        /// <summary>
        /// Gets the entity's transform.
        /// </summary>
        public Transform Transform { get; }

        /// <summary>
        /// Has the update method been implemented?
        /// </summary>
        internal bool IsUpdateImplemented => OverrideChecker.IsMethodOverridden(typeof(Entity), GetType(), nameof(Update));

        /// <summary>
        /// Has the fixed update method been implemented?
        /// </summary>
        internal bool IsFixedUpdateImplemented => OverrideChecker.IsMethodOverridden(typeof(Entity), GetType(), nameof(FixedUpdate));

        protected internal virtual void Update(float dt) { }

        protected internal virtual void FixedUpdate() { }

        public C AddComponent<C>(C component) where C : Component
        {
            if (component.Entity == null)
            {
                // todo: could this cause a concurrent modification exception
                if (_components.Add(component))
                {
                    // Track component
                    if (_isContainedByScene) { Scene.AddComponent(component); }
                    component.Entity = this;

                    return component;
                }
                else
                {
                    throw new CriticalStateException("Added component already known to entity but not component.");
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to add component to entity, component already attached to another entity.");
            }
        }

        public void RemoveComponent(Component component)
        {
            if (component == Transform)
            {
                throw new InvalidOperationException("Removal of transform component from entity is prohibited.");
            }

            if (component.Entity == this)
            {
                // todo: could this cause a concurrent modification exception
                if (_components.Remove(component))
                {
                    // Untrack component
                    if (_isContainedByScene) { Scene.RemoveComponent(component); }
                    component.Entity = null;
                }
                else
                {
                    throw new CriticalStateException("Target component for removal already unknown to entity but component configured for entity.");
                }
            }
            else if (component.Entity == null)
            {
                throw new InvalidOperationException("Unable to remove component from entity, component is not attached to an entity.");
            }
            else
            {
                throw new InvalidOperationException("Unable to remove component from entity, component is attached to a different entity.");
            }
        }

        public C GetComponent<C>() where C : Component
        {
            // todo: could this cause a concurrent modification exception
            foreach (var component in _components.GetItemsByType<C>())
            {
                return component;
            }

            return default;
        }

        public IEnumerable<C> GetComponents<C>() where C : Component
        {
            // todo: could this cause a concurrent modification exception
            return _components.GetItemsByType<C>();
        }

        internal void OnAddedToScene()
        {
            _isContainedByScene = true;
        }

        internal void OnRemovedFromScene()
        {
            _isContainedByScene = false;
        }
    }
}

