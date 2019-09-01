using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;

namespace Heirloom.Runtime
{
    public abstract class Scene
    {
        private readonly DelayedMutateSet<Entity> _entities;
        private readonly List<ComponentSystem> _systems;

        private static float _fixedTime = 0F;

        protected Scene()
        {
            // Create systems list with default systems
            _systems = new List<ComponentSystem>();

            // 
            _entities = new DelayedMutateSet<Entity>();

            // 
            Camera = new Camera();

            // Add default systems
            AddSystem(Collisions = new CollisionSystem());
        }

        public GameInput Input => Game.Input;

        public CollisionSystem Collisions { get; }

        public IEnumerable<Entity> Entities => _entities.Items;

        public Camera Camera { get; }

        #region Add / Remove Entity

        public E AddEntity<E>(E entity) where E : Entity
        {
            if (entity.Scene == this) { throw new System.ArgumentException("Unable to add entity to scene, already contained by this scene."); }
            if (entity.Scene != null) { throw new System.ArgumentException("Unable to add entity to scene, already contained by another scene."); }

            // Set scene and add to scene list
            entity.Scene = this;
            _entities.Add(entity);

            return entity;
        }

        public void RemoveEntity(Entity entity)
        {
            if (entity.Scene != this) { throw new System.ArgumentException("Unable to remove entity from scene, entity not contained by this scene."); }
            if (entity.Scene == null) { throw new System.ArgumentException("Unable to remove entity from scene, entity not contained by any scene."); }

            _entities.Remove(entity);
            entity.Scene = null;
        }

        #region Entity Component Mutate Events

        private void NotifyEntityAdded(Entity entity)
        {
            foreach (var system in _systems)
            {
                system.AddEntity(entity);
            }
        }

        private void NotifyEntityRemoved(Entity entity)
        {
            foreach (var system in _systems)
            {
                system.RemoveEntity(entity);
            }
        }

        internal void NotifyComponentAdded(Entity entity, Component component)
        {
            // Alert systems of entity changing component structure
            foreach (var system in _systems)
            {
                system.EntityChanged(entity);
            }
        }

        internal void NotifyComponentRemoved(Entity entity, Component component)
        {
            // Alert systems of entity changing component structure
            foreach (var system in _systems)
            {
                system.EntityChanged(entity);
            }
        }

        #endregion

        #endregion

        #region Add / Remove Component Systems

        public void AddSystem(ComponentSystem system)
        {
            _systems.Add(system);
        }

        public void RemoveSystem(ComponentSystem system)
        {
            _systems.Remove(system);
        }

        #endregion

        protected internal abstract void Enter();

        protected internal abstract void Exit();

        protected internal void Update()
        {
            // todo: start entities?

            // Process added items
            // Notify scene that this entity has added components
            foreach (var entity in _entities.ProcessAddedItems())
            {
                // todo: start components?
                NotifyEntityAdded(entity);
            }

            // Process removed items
            // Notify scene that this entity has been removed
            foreach (var entity in _entities.ItemsToRemove)
            {
                // todo: stop components?
                NotifyEntityRemoved(entity);
            }

            // Update each entity
            foreach (var entity in Entities)
            {
                if (entity.Enabled)
                {
                    entity.UpdateInternal();
                }
            }

            // Update each system
            foreach (var system in _systems)
            {
                system.Update();
            }

            FixedUpdate();
        }

        private void FixedUpdate()
        {
            // Accumulate
            _fixedTime += Time.Delta;

            //// Critically low FPS! We don't want to experience run away accumulation of time,
            //// so we will set a hard value on 1 simulated step. The entire simulation
            //// will slow down, but it won't progressively accumulate more and more work as it falls behind!
            //if (Time.Delta > Time.FixedDelta && _fixedTime > Time.FixedDelta)
            //{
            //    _fixedTime = Time.FixedDelta;
            //}

            // Process accumulated time steps
            while (_fixedTime >= Time.FixedDelta)
            {
                _fixedTime -= Time.FixedDelta;

                // Update each entity
                foreach (var entity in Entities)
                {
                    if (entity.Enabled)
                    {
                        entity.FixedUpdateInternal();
                    }
                }

                // Update each system
                // Events CollisionSystem, etc
                foreach (var system in _systems)
                {
                    system.FixedUpdate();
                }
            }
        }

        protected internal void Render(RenderContext ctx)
        {
            // Configure camera
            // todo: multiple cameras
            ctx.Viewport = (0, 0, 1, 1);
            ctx.Transform = Camera.ComputeMatrix(ctx);
            ctx.Clear(Camera.Color);

            // Draw world (todo: improve OrderBy with less frequent sorting by update/notifydepthchanged)
            foreach (var entity in Entities.OrderBy(x => x.Depth))
            {
                if (entity.Enabled)
                {
                    entity.RenderInternal(ctx);
                }
            }
        }
    }
}
