using System;
using System.Collections;
using System.Collections.Generic;

using Heirloom.Collections;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game
{
    public static class Scene
    {
        // Game Logic
        private static readonly TypeDictionary<Entity> _entities = new TypeDictionary<Entity>();
        private static readonly List<Entity> _updatableEntities = new List<Entity>();

        private static readonly TypeDictionary<Component> _components = new TypeDictionary<Component>();
        private static readonly List<Component> _updatableComponents = new List<Component>();

        // Game Drawing
        private static readonly List<Camera> _cameras = new List<Camera>();
        private static readonly List<DrawableComponent> _drawbles = new List<DrawableComponent>();
        private static bool _hasDrawableDepthChange = true;

        private static readonly Queue<Action> _futureActions = new Queue<Action>();

        private static readonly CoroutineRunner _coroutineRunner = new CoroutineRunner();

        #region Properties

        /// <summary>
        /// Background color used if no camera components exist in the scene.
        /// </summary>
        public static Color BackgroundColor { get; set; } = Colors.FlatUI.WetAshphalt;

        #endregion

        #region Add / Remove Entities

        public static void AddEntity(Entity entity)
        {
            // Add to entities list
            // todo: schedule addition to prevent concurrent mutation?
            _entities.Add(entity);

            // If update method is implemented, put onto update list
            if (entity.IsUpdateImplemented) { _updatableEntities.Add(entity); }

            // Was the entity a camera?
            if (entity is Camera camera) { _cameras.Add(camera); }

            // Add known components
            foreach (var component in entity.GetComponents<Component>())
            {
                AddComponent(component);
            }

            // Inform entity it is now part of the scene
            entity.OnAddedToScene();
        }

        public static void RemoveEntity(Entity entity)
        {
            // Add to entities list
            // todo: schedule removal to prevent concurrent mutation?
            _entities.Remove(entity);

            // If update method is implemented, remove from update list
            if (entity.IsUpdateImplemented) { _updatableEntities.Remove(entity); }

            // Was the entity a camera?
            if (entity is Camera camera) { _cameras.Remove(camera); }

            // Add known components
            foreach (var component in entity.GetComponents<Component>())
            {
                RemoveComponent(component);
            }

            // Inform entity it was removed from the scene
            entity.OnRemovedFromScene();
        }

        public static IEnumerable<T> FindEntities<T>() where T : Entity
        {
            return _entities.GetItemsByType<T>();
        }

        public static IEnumerable<Entity> FindEntities(Predicate<Entity> predicate)
        {
            foreach (var entity in _entities)
            {
                if (predicate(entity))
                {
                    yield return entity;
                }
            }
        }

        internal static void AddComponent(Component component)
        {
            if (_components.Add(component))
            {
                // If update method is implemented, add to update list
                if (component.IsUpdateImplemented) { _updatableComponents.Add(component); }

                // 
                if (component is DrawableComponent drawable)
                {
                    _drawbles.Add(drawable);
                    NotifyDrawableDepthChange();
                }
            }
            else
            {
                throw new ArgumentException($"Unable to add component to scene tracking, already tracked.");
            }
        }

        internal static void RemoveComponent(Component component)
        {
            if (_components.Remove(component))
            {
                // If update method is implemented, remove from update list
                if (component.IsUpdateImplemented) { _updatableComponents.Remove(component); }

                // 
                if (component is DrawableComponent drawable)
                {
                    _drawbles.Remove(drawable);
                    NotifyDrawableDepthChange();
                }
            }
            else
            {
                throw new ArgumentException($"Unable to remove component from scene tracking, already untracked.");
            }
        }

        #endregion

        #region Coroutines

        public static Coroutine StartCoroutine(float delay, IEnumerator enumerator)
        {
            return _coroutineRunner.Run(delay, enumerator);
        }

        public static Coroutine StartCoroutine(float delay, Func<IEnumerator> coroutine)
        {
            return StartCoroutine(delay, coroutine());
        }

        public static Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return StartCoroutine(0, enumerator);
        }

        public static Coroutine StartCoroutine(Func<IEnumerator> coroutine)
        {
            return StartCoroutine(0, coroutine());
        }

        #endregion

        internal static void NotifyDrawableDepthChange()
        {
            _hasDrawableDepthChange = true;
        }

        internal static void Update(RenderContext ctx, float dt)
        {
            // Process all scheduled actions
            while (_futureActions.Count > 0)
            {
                var action = _futureActions.Dequeue();
                action();
            }

            ProcessLogic(dt);
            ProcessDrawing(ctx);
        }

        private static void ProcessLogic(float dt)
        {
            // Update coroutines
            _coroutineRunner.Update(dt);

            // Update Entities 
            // todo: Not all entities will need to update, curate a "updatable entity list" with some sort of flag / detection.
            //       For example, An entity used to act a hierarchical node does not need to update.
            foreach (var entity in _entities)
            {
                entity.Update(dt);
            }

            // Update each component
            // todo: Not all components will need to update, curate a "updatable component list" with some sort of flag / detection.
            //       For example, ImageComponent does not need update, but SpriteComponent does to track time for animations.
            foreach (var c in _components)
            {
                // todo: Instead remove from _components when disabled to prevent even having to loop over it?
                if (c.IsEnabled)
                {
                    c.Update(dt);
                }
            }
        }

        private static void SortDrawables()
        {
            if (_hasDrawableDepthChange)
            {
                _hasDrawableDepthChange = false;

                // Sort entities by depth
                // todo: sort each curated list too!
                // todo: possibly only sort DrawableComponents once curated?
                _drawbles.StableSort((a, b) => a.Depth.CompareTo(b.Depth));
            }
        }

        private static void ProcessDrawing(RenderContext ctx)
        {
            //  
            SortDrawables();

            // If there are cameras existing in the scene a default '2D canvas' approach is used.
            // If at least one camera does exist in the scene then we will render the scene from each enabled camera's point of view.
            // todo: remove LINQ, use TypeDictionary or some other type curated structure
            if (_cameras.Count > 0)
            {
                // Iterate over each camera, only drawing from enabled cameras
                foreach (var camera in _cameras)
                {
                    var surface = camera.Surface ?? ctx.DefaultSurface;
                    DrawEntities(surface, camera.Matrix, camera.Viewport, camera.BackgroundColor);
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

                // Process drawbles
                foreach (var drawable in _drawbles)
                {
                    // todo: instead remove from _drawbles when disabled to prevent even having to loop over it
                    if (drawable.IsEnabled)
                    {
                        drawable.InternalDraw(ctx);
                    }
                }
            }
        }
    }
}
