using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game
{
    public sealed class Scene
    {
        private readonly List<Entity> _entities;
        private readonly List<Camera> _cameras;
        private bool _hasDepthChange = true;

        public Scene()
        {
            _entities = new List<Entity>();
        }

        /// <summary>
        /// Background color used if no camera components exist in the scene.
        /// </summary>
        public Color BackgroundColor { get; set; } = Colors.FlatUI.WetAshphalt;

        /// <summary>
        /// Gets a read-only list of entities contained in the scene.
        /// </summary>
        public IReadOnlyList<Entity> Entities => _entities;

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
            if (entity.Scene != null)
            {
                throw new InvalidOperationException("Unable to add entity to scene, already contained by another scene");
            }

            // Add to entities list
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

        internal void MarkDepthChange()
        {
            _hasDepthChange = true;
        }

        internal void Update(RenderContext ctx, float dt)
        {
            // Update input
            Input.Update();

            //  
            SortEntities();

            // Update Entities 
            foreach (var entity in _entities)
            {
                entity.InternalUpdate(dt);
            }

            // If there are cameras existing in the scene a default '2D canvas' approach is used.
            // If at least one camera does exist in the scene then we will render the scene from each enabled camera's point of view.
            // todo: remove LINQ, use TypeDictionary or some other type curated structure
            var cameras = _entities.SelectMany(e => e.GetComponents<Camera>());
            if (cameras.Any())
            {
                // Iterate over each camera, only drawing from enabled cameras
                foreach (var camera in cameras)
                {
                    if (camera.IsEnabled)
                    {
                        var surface = camera.Surface ?? ctx.DefaultSurface;
                        DrawEntities(surface, camera.CameraMatrix, camera.Viewport, camera.BackgroundColor);
                    }
                }
            }
            else
            {
                // Draw entities with the default '2D canvas' view state
                DrawEntities(ctx.DefaultSurface, Matrix.Identity, Rectangle.One, BackgroundColor);
            }

            void DrawEntities(Surface surface, Matrix globalTransform, Rectangle viewport, Color clearColor)
            {
                // Set view state
                ctx.Transform = globalTransform;
                ctx.Viewport = viewport;
                ctx.Surface = surface;

                // Clear
                ctx.Clear(clearColor);

                // Draw Entities
                foreach (var entity in _entities)
                {
                    ctx.SaveState();
                    entity.InternalDraw(ctx);
                    ctx.RestoreState();
                }
            }
        }

        private void SortEntities()
        {
            if (_hasDepthChange)
            {
                _hasDepthChange = false;

                // Sort entities by depth
                // todo: sort each curated list too!
                // todo: possibly only sort DrawableComponents once curated?
                _entities.StableSort((a, b) => a.Depth.CompareTo(b.Depth));
            }
        }
    }
}
