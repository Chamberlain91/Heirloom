using System;
using System.Collections.Generic;

using Heirloom.Drawing;

namespace Heirloom.Runtime
{
    public abstract class Component
    {
        private bool _enabled = true;

        protected internal bool HasStarted { get; internal set; } = false;

        public bool Enabled
        {
            get => _enabled;

            set
            {
                if (_enabled != value)
                {
                    OnSetEnabled(_enabled);
                    _enabled = value;
                }
            }
        }

        public Entity Entity { get; internal set; }

        public Transform Transform => Entity.Transform;

        public GameInput Input => Game.Input;

        public IEnumerable<C> GetComponents<C>() where C : Component
        {
            return Entity.GetComponents<C>();
        }

        public C GetComponent<C>() where C : Component
        {
            return Entity.GetComponent<C>();
        }

        protected C RequireComponent<C>() where C : Component
        {
            var component = GetComponent<C>();

            if (component == null)
            {
                throw new InvalidOperationException($"Unable to get required component '{typeof(C)}'.");
            }

            return component;
        }

        internal IEnumerable<I> GetInherited<I>() where I : class
        {
            return Entity.GetInherited<I>();
        }

        internal protected virtual void Start() { }

        internal protected virtual void Update() { }

        internal protected virtual void Render(RenderContext ctx) { }

        internal protected virtual void FixedUpdate() { }

        internal protected virtual void OnSetEnabled(bool enabled)
        {
            // nothing special
        }
    }
}
