using System;
using System.Collections.Generic;

using Heirloom;
using Heirloom.Drawing;
using Heirloom.Desktop;

namespace Examples.Depth
{
    public abstract class GameContext
    {
        private List<Entity> _entities;

        protected GameContext(Window window, MultisampleQuality multisample)
        {
            _entities = new List<Entity>();

            Window = window ?? throw new ArgumentNullException(nameof(window));

            Renderer = new Renderer(multisample);
            Loop = GameLoop.Create(Window.Graphics, OnUpdate);
        }

        public Window Window { get; }

        public GameLoop Loop { get; }

        public Renderer Renderer { get; }

        public T Create<T>(Vector position) where T : Entity, new()
        {
            // 
            var entity = Activator.CreateInstance<T>();
            entity.Position = position;
            entity.Game = this;

            // 
            _entities.Add(entity);
            return entity;
        }

        private void OnUpdate(Graphics gfx, float dt)
        {
            // Update the game
            Update(dt);

            // 
            _entities.StableSort();

            // Render the game
            Renderer.Render(gfx, dt, _entities);
        }

        protected abstract void Update(float dt);
    }
}
