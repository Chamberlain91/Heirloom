using System;
using System.Collections;

namespace Heirloom.Game
{
    public abstract class Component
    {
        /// <summary>
        /// Gets or sets the enabled state of this component.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets the entity this component is attached to (may be null if not attached to an entity).
        /// </summary>
        public Entity Entity { get; internal set; }

        /// <summary>
        /// Gets the scene this component is contained within (may be null if not attached to an entity or entity is not contained by a scene).
        /// </summary>
        public Scene Scene => Entity?.Scene;

        /// <summary>
        /// Gets the transform component of the attached entity (may be null if not attached to an entity).
        /// </summary>
        public Transform Transform => Entity?.Transform;

        protected internal abstract void Update(float dt);

        #region Coroutines

        public Coroutine StartCoroutine(float delay, IEnumerator enumerator)
        {
            return Entity?.StartCoroutine(delay, enumerator)
                ?? throw new InvalidOperationException("Unable to start coroutine, component was not attached to an entity.");
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
    }
}
