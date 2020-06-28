using System;

namespace Heirloom
{
    /// <summary>
    /// A default non-abstract implementation of a game loop.
    /// </summary>
    public sealed class DefaultGameLoop : GameLoop
    {
        private readonly UpdateFunction _update;

        /// <summary>
        /// Creates a default loop instance from the given context and method reference.
        /// </summary>
        /// <param name="screen">The relevant screen to provide graphics context.</param>
        /// <param name="update">The relevant update function.</param>
        /// <param name="frameRate">The desired fixed frame rate or -1 for unlimited.</param>
        public DefaultGameLoop(Screen screen, UpdateFunction update, int frameRate = -1)
            : this(screen.Graphics, update, frameRate)
        { }

        /// <summary>
        /// Creates a default loop instance from the given context and method reference.
        /// </summary>
        /// <param name="graphics">The relevant graphics context.</param>
        /// <param name="update">The relevant update function.</param>
        /// <param name="frameRate">The desired fixed frame rate or -1 for unlimited.</param>
        public DefaultGameLoop(GraphicsContext graphics, UpdateFunction update, int frameRate = -1)
            : base(graphics, frameRate)
        {
            _update = update ?? throw new ArgumentNullException(nameof(update));
        }

        /// <inheritdoc/>
        protected override void Update(float delta)
        {
            _update(Graphics, delta);
        }
    }
}
