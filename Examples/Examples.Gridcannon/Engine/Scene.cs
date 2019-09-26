using System;
using System.Collections;
using System.Collections.Generic;

using Heirloom.Collections;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Gridcannon.Engine
{
    public class Scene
    {
        private readonly List<Entity> _entities;

        private readonly List<Coroutine> _coroutines;

        private bool _hasDepthChange = true;

        public Scene()
        {
            _coroutines = new List<Coroutine>();
            _entities = new List<Entity>();
        }

        public IReadOnlyList<Entity> Entities => _entities;

        internal void MarkDepthChange()
        {
            _hasDepthChange = true;
        }

        #region Add / Remove Entities

        public void Add(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Add(Entity entity)
        {
            // 
            if (entity.Scene != null) { throw new InvalidOperationException("Unable to add entity to scene, already contained by another scene"); }

            // 
            _entities.Add(entity);
            entity.Scene = this;

            // 
            MarkDepthChange();
        }

        public void Remove(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        public void Remove(Entity entity)
        {
            // 
            if (entity.Scene == null) { throw new InvalidOperationException("Unable to remove entity from scene, not contained by any scene"); }

            // 
            if (_entities.Remove(entity))
            {
                entity.Scene = null;
                MarkDepthChange();
            }
        }

        #endregion

        #region Coroutines

        public Coroutine StartCoroutine(float delay, IEnumerator enumerator)
        {
            var coroutine = new Coroutine(enumerator, delay);
            _coroutines.Add(coroutine);
            return coroutine;
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

        public void Update(RenderContext ctx, float dt)
        {
            // Update coroutines
            for (var i = _coroutines.Count - 1; i >= 0; i--)
            {
                var coroutine = _coroutines[i];

                // Process coroutine. If return false, coroutine is terminated
                if (!coroutine.Update(dt)) { _coroutines.RemoveAt(i); }
            }

            // Update entities
            UpdateEntities(dt);
            RenderEntities(ctx);
        }

        private void UpdateEntities(float dt)
        {
            if (_hasDepthChange)
            {
                // Sort entities by depth
                _entities.InsertionSort((a, b) => a.Depth.CompareTo(b.Depth));
                _hasDepthChange = false;
            }

            // Update Entities 
            foreach (var entity in _entities)
            {
                entity.Update(dt);
            }
        }

        private void RenderEntities(RenderContext ctx)
        {
            // Clear, could be provided by a camera or view system in the future
            ctx.Clear(Colors.FlatUI.WetAshphalt);

            // Draw Entities
            foreach (var entity in _entities)
            {
                ctx.SaveState();
                entity.Draw(ctx);
                entity.DrawDebug(ctx);
                ctx.RestoreState();
            }
        }

        public void NotifyMouseClick(int button, bool isDown, Vector position)
        {
            // Walk over entities in reverse, since the last to be drawn are on top
            for (var i = _entities.Count - 1; i >= 0; i--)
            {
                var entity = _entities[i];

                // 
                if (entity.OnMouseClick(button, isDown, position))
                {
                    break;
                }
            }
        }

        public void NotifyMouseMove(Vector position, Vector delta)
        {
            // Walk over entities in reverse, since the last to be drawn are on top
            for (var i = _entities.Count - 1; i >= 0; i--)
            {
                var entity = _entities[i];

                if (entity.OnMouseMove(position, delta))
                {
                    break;
                }
            }
        }
    }
}
