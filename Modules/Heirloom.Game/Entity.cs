using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Heirloom.Collections;
using Heirloom.Drawing;

namespace Heirloom.Game
{
    public abstract class Entity
    {
        private int _depth = 0;

        private readonly TypeDictionary<Component> _components;

        private readonly CoroutineRunner _coroutineRunner;

        protected Entity()
        {
            _coroutineRunner = new CoroutineRunner();
            _components = new TypeDictionary<Component>();

            // Default Components
            Transform = AddComponent(new Transform());
        }

        /// <summary>
        /// Gets the transform of this entity.
        /// </summary>
        public Transform Transform { get; }

        /// <summary>
        /// Gets which scene this entity exists (may be null if not contained by a scene).
        /// </summary>
        public Scene Scene { get; internal set; }

        public int Depth
        {
            get => _depth;

            set
            {
                if (_depth != value)
                {
                    _depth = value;
                    Scene.MarkDepthChange();
                }
            }
        }

        #region Coroutines

        public Coroutine StartCoroutine(float delay, IEnumerator enumerator)
        {
            return _coroutineRunner.Run(delay, enumerator);
        }

        public Coroutine StartCoroutine(float delay, Func<IEnumerator> coroutine)
        {
            return StartCoroutine(delay, coroutine());
        }

        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return StartCoroutine(0, enumerator);
        }

        public Coroutine StartCoroutine(Func<IEnumerator> coroutine)
        {
            return StartCoroutine(0, coroutine());
        }

        #endregion

        #region Components

        public C AddComponent<C>() where C : Component, new()
        {
            return AddComponent(new C());
        }

        public C AddComponent<C>(C component) where C : Component
        {
            if (component.Entity == null)
            {
                _components.Add(component);
                component.Entity = this;
                return component;
            }
            else
            {
                throw new InvalidOperationException("Unable to add component to entity, component already attached to another entity.");
            }
        }

        public void RemoveComponent(Component component)
        {
            if (component.Entity == null)
            {
                _components.Add(component);
                component.Entity = this;
            }
            else
            {
                throw new InvalidOperationException("Unable to add component to entity, component already attached to another entity.");
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

        #endregion

        protected virtual void Update(float dt) { }

        protected virtual void Draw(RenderContext ctx) { }

        [Conditional("DEBUG")]
        protected virtual void DrawDebug(RenderContext ctx) { }

        internal void InternalUpdate(float dt)
        {
            // Update coroutines
            _coroutineRunner.Update(dt);

            // Update entity
            Update(dt);

            // Update each component
            foreach (var c in GetComponents<Component>())
            {
                if (c.IsEnabled)
                {
                    c.Update(dt);
                }
            }
        }

        internal void InternalDraw(RenderContext ctx)
        {
            // Draw entity
            Draw(ctx);

            // Draw each component
            foreach (var c in GetComponents<DrawableComponent>())
            {
                if (c.IsEnabled && c.IsVisible)
                {
                    c.Draw(ctx);
                }
            }

            // Draw debug mode visuals
            DrawDebug(ctx);
        }
    }
}

