namespace Heirloom.Desktop.Game
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
        /// A shortcut property to get the transform component of the attached entity.
        /// </summary>
        public Transform Transform => Entity.Transform;

        protected internal abstract void Update(float dt);
    }
}
