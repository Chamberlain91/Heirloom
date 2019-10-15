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
        /// Gets the transform component of the attached entity (may be null if not attached to an entity).
        /// </summary>
        public Transform Transform => Entity?.Transform;

        /// <summary>
        /// Has the update method been implemented?
        /// </summary>
        internal bool IsUpdateImplemented => OverrideChecker.IsMethodOverridden(typeof(Component), GetType(), nameof(Update));

        /// <summary>
        /// Has the fixed update method been implemented?
        /// </summary>
        internal bool IsFixedUpdateImplemented => OverrideChecker.IsMethodOverridden(typeof(Entity), GetType(), nameof(FixedUpdate));

        protected internal virtual void Update(float dt) { }

        protected internal virtual void FixedUpdate(float ft) { }

        protected internal virtual void OnAdded() { }

        protected internal virtual void OnRemoved() { }
    }
}
