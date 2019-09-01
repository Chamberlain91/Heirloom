using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;

namespace Heirloom.Runtime
{
    public class Entity
    {
        public bool Enabled { get; set; } = true;

        public uint RenderMask { get; set; } = ~0u;

        public int Depth { get; set; } = 0;

        public IEnumerable<Component> Components => _components.Items;

        public Scene Scene { get; internal set; }

        public Transform Transform { get; }

        public GameInput Input => Game.Input;

        private readonly DelayedMutateSet<Component> _components;

        public Entity()
        {
            _components = new DelayedMutateSet<Component>();

            Transform = new Transform();
        }

        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }

        protected virtual void Render(RenderContext ctx) { }

        internal virtual void UpdateInternal()
        {
            // Process added items
            // Notify scene that this entity has added components
            foreach (var component in _components.ProcessAddedItems())
            {
                Scene.NotifyComponentAdded(this, component);
            }

            // Process removed items
            // Notify scene that this entity has removed components
            foreach (var component in _components.ProcessRemovedItems())
            {
                Scene.NotifyComponentRemoved(this, component);
            }

            // Update each component
            foreach (var component in Components)
            {
                if (component.Enabled)
                {
                    // Start component (if needed)
                    // todo: start list before entire update phase?
                    if (!component.HasStarted)
                    {
                        component.HasStarted = true;
                        component.Start();
                    }

                    // Update component
                    component.Update();
                }
            }

            // 
            Update();
        }

        internal virtual void FixedUpdateInternal()
        {
            // Update each component
            foreach (var component in Components)
            {
                if (component.Enabled && component.HasStarted)
                {
                    // Update component
                    component.FixedUpdate();
                }
            }

            // 
            FixedUpdate();
        }

        internal void RenderInternal(RenderContext ctx)
        {
            // todo: specialized render list to reduce interation
            foreach (var component in Components)
            {
                if (component.Enabled)
                {
                    component.Render(ctx);
                }
            }

            //
            Render(ctx);
        }

        #region Components

        public TComponent AddComponent<TComponent>() where TComponent : Component, new()
        {
            return AddComponent(new TComponent());
        }

        public TComponent AddComponent<TComponent>(TComponent component) where TComponent : Component
        {
            if (component.Entity != null)
            {
                throw new ArgumentException("Unable to add component to entity, already contained by another entity.");
            }

            // 
            _components.Add(component);
            component.Entity = this;

            return component;
        }

        public void RemoveComponent<TComponent>(TComponent component) where TComponent : Component
        {
            if (component.Entity == null) { throw new ArgumentException("Unable to remove component from entity, component not contained by any entity."); }
            if (component.Entity != this) { throw new ArgumentException("Unable to remove component from entity, component not contained by this entity."); }

            // 
            _components.Remove(component);
            component.Entity = null;
        }

        public bool HasComponent<TComponent>() where TComponent : Component
        {
            return Components.Any(c => c is TComponent);
        }

        public bool HasComponent(Type type)
        {
            if (type == null) { throw new ArgumentNullException(nameof(type)); }
            if (!type.IsSubclassOf(typeof(Component))) { throw new ArgumentException($"Type '{type.Name}' was not a component."); }

            return Components.Any(c => c.GetType().IsAssignableFrom(type));
        }

        public TComponent GetComponent<TComponent>() where TComponent : Component
        {
            return GetComponents<TComponent>().FirstOrDefault();
        }

        public IEnumerable<TComponent> GetComponents<TComponent>() where TComponent : Component
        {
            return Components.Where(c => c is TComponent).Cast<TComponent>();
        }

        #endregion

        internal IEnumerable<I> GetInherited<I>() where I : class
        {
            // Inherited on Entity
            if (this is I entityInterface) { yield return entityInterface; }

            // Inherited on Component
            foreach (var component in Components)
            {
                if (component is I componentInterface)
                {
                    yield return componentInterface;
                }
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {string.Join(", ", Components.Select(c => c.GetType().Name))}";
        }
    }
}
