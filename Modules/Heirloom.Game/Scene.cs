using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game
{
    public static class Scene
    {
        // Game Logic
        private static readonly TypeDictionary<Entity> _entities = new TypeDictionary<Entity>();
        private static readonly List<Entity> _fixedUpdatableEntities = new List<Entity>();
        private static readonly List<Entity> _updatableEntities = new List<Entity>();

        private static readonly TypeDictionary<Component> _components = new TypeDictionary<Component>();
        private static readonly List<Component> _fixedUpdatableComponents = new List<Component>();
        private static readonly List<Component> _updatableComponents = new List<Component>();

        // Game Drawing
        private static readonly List<Camera> _cameras = new List<Camera>();
        private static readonly List<DrawableComponent> _drawbles = new List<DrawableComponent>();
        private static bool _hasDrawableDepthChange = true;

        private static readonly Queue<Action> _futureActions = new Queue<Action>();

        private static readonly LoadScreen _loadScreen = new DefaultLoadScreen();
        private static bool _isLoadScreenVisible = false;

        private static float _fixedUpdateTime;

        #region Properties

        /// <summary>
        /// The fixed duration in seconds between calls to <see cref="Entity.FixedUpdate(float)"/> or <see cref="Component.FixedUpdate(float)"/>. <para/>
        /// Set to 1/30th a second by default.
        /// </summary>
        public static float FixedDeltaTime { get; internal set; } = 1 / 30F;

        /// <summary>
        /// Background color used if no camera components exist in the scene.
        /// </summary>
        public static Color BackgroundColor { get; set; } = Color.DarkGray;

        #endregion

        #region Add/Remove Entities/Components

        public static E AddEntity<E>(E entity) where E : Entity
        {
            // Add to entities list
            // todo: schedule addition to prevent concurrent mutation?
            _entities.Add(entity);

            // If update method is implemented, put onto update list
            if (entity.IsUpdateImplemented) { _updatableEntities.Add(entity); }

            // If fixed update method is implemented, put onto fixed update list
            if (entity.IsFixedUpdateImplemented) { _fixedUpdatableEntities.Add(entity); }

            // Was the entity a camera?
            if (entity is Camera camera) { _cameras.Add(camera); }

            // Add known components
            foreach (var component in entity.GetComponents<Component>())
            {
                AddComponent(component);
            }

            // Inform entity it is now part of the scene
            entity.OnAddedInternal();

            return entity;
        }

        public static void RemoveEntity(Entity entity)
        {
            // Add to entities list
            // todo: schedule removal to prevent concurrent mutation?
            _entities.Remove(entity);

            // If update method is implemented, remove from update list
            if (entity.IsUpdateImplemented) { _updatableEntities.Remove(entity); }

            // If fixed update method is implemented, remove from fixed update list
            if (entity.IsFixedUpdateImplemented) { _fixedUpdatableEntities.Remove(entity); }

            // Was the entity a camera?
            if (entity is Camera camera) { _cameras.Remove(camera); }

            // Add known components
            foreach (var component in entity.GetComponents<Component>())
            {
                RemoveComponent(component);
            }

            // Inform entity it was removed from the scene
            entity.OnRemovedInternal();
        }

        internal static void AddComponent(Component component)
        {
            if (_components.Add(component))
            {
                // If update method is implemented, add to update list
                if (component.IsUpdateImplemented) { _updatableComponents.Add(component); }

                // If fixed update method is implemented, put onto fixed update list
                if (component.IsFixedUpdateImplemented) { _fixedUpdatableComponents.Add(component); }

                // 
                if (component is DrawableComponent drawable)
                {
                    _drawbles.Add(drawable);
                    NotifyDrawableDepthChange();
                }

                // 
                component.OnAdded();
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

                // If fixed update method is implemented, remove from fixed update list
                if (component.IsFixedUpdateImplemented) { _fixedUpdatableComponents.Remove(component); }

                // 
                if (component is DrawableComponent drawable)
                {
                    _drawbles.Remove(drawable);
                    NotifyDrawableDepthChange();
                }

                // 
                component.OnRemoved();
            }
            else
            {
                throw new ArgumentException($"Unable to remove component from scene tracking, already untracked.");
            }
        }

        #endregion

        public static T GetEntity<T>() where T : Entity
        {
            return GetEntities<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetEntities<T>() where T : Entity
        {
            return _entities.GetItemsByType<T>();
        }

        public static T GetComponent<T>() where T : Component
        {
            return GetComponents<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetComponents<T>() where T : Component
        {
            return _components.GetItemsByType<T>();
        }

        internal static void NotifyDrawableDepthChange()
        {
            _hasDrawableDepthChange = true;
        }

        public static void ShowLoadScreen()
        {
            LoadScreen.Progress.Message = string.Empty;
            LoadScreen.Progress.Percent = 0F;

            _isLoadScreenVisible = true;
        }

        public static void HideLoadScreen()
        {
            _isLoadScreenVisible = false;
        }

        internal static void Update(Graphics ctx, float dt)
        {
            // Process input
            Input.Update();

            // Process all scheduled actions
            while (_futureActions.Count > 0)
            {
                var action = _futureActions.Dequeue();
                action();
            }

            // Update coroutines
            Coroutine.Runner.Update(dt);

            // Update scene
            ProcessSceneLogic(dt);
            ProcessSceneDrawing(ctx);

            // Overlay loading screen
            if (_isLoadScreenVisible)
            {
                ctx.PushState();
                ctx.ResetState();
                _loadScreen.Draw(ctx, dt);
                ctx.PopState();
            }
        }

        private static void ProcessSceneLogic(float dt)
        {
            _fixedUpdateTime += dt;

            // For each entity with Update
            foreach (var en in _updatableEntities)
            {
                en.Update(dt);
            }

            // Update each component with Update
            foreach (var co in _updatableComponents)
            {
                // todo: Instead remove from list when disabled to prevent even having to loop over it?
                if (co.IsEnabled)
                {
                    co.Update(dt);
                }
            }

            // For each elapsed fixed timestep
            while (_fixedUpdateTime >= FixedDeltaTime)
            {
                // 
                _fixedUpdateTime -= FixedDeltaTime;

                // For each entity with FixedUpdate
                foreach (var en in _fixedUpdatableEntities)
                {
                    en.FixedUpdate(FixedDeltaTime);
                }

                // For each component with FixedUpdate
                foreach (var co in _fixedUpdatableComponents)
                {
                    // todo: Instead remove from list when disabled to prevent even having to loop over it?
                    if (co.IsEnabled)
                    {
                        co.FixedUpdate(FixedDeltaTime);
                    }
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

        private static void ProcessSceneDrawing(Graphics ctx)
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

        private class DefaultLoadScreen : LoadScreen
        {
            protected internal override void Draw(Graphics ctx, float dt)
            {
                ctx.Clear(BackgroundColor);

                var text = $"LOADING\n{Progress.Message}";
                var textSize = TextLayout.Measure(text, (400, ctx.Surface.Height), Font.Default, 32).Size;
                textSize.Width = Calc.Max(400, textSize.Width);

                // Compute centered text rectangle
                var x = (ctx.Surface.Bounds.Width - textSize.Width) / 2F;
                var y = (ctx.Surface.Bounds.Height - textSize.Height) / 2F;
                var textRect = new Rectangle((x, y), textSize);
                textRect = Rectangle.Inflate(textRect, 8);

                var progRect = textRect;
                progRect.Y += 4 + textRect.Height;
                progRect.Height = 4;

                // Draw text container
                ctx.Color = Color.White;
                ctx.DrawRect(textRect);

                // Draw text
                ctx.Color = Color.DarkGray;
                ctx.DrawText(text, Rectangle.Inflate(textRect, -8), Font.Default, 32, TextAlign.Center);

                // Draw percent bar container
                ctx.Color = Color.Gray;
                ctx.DrawRect(progRect);

                // Draw percent bar
                ctx.Color = Color.Pink;
                progRect.Width = Progress.Percent * progRect.Width;
                ctx.DrawRect(progRect);
            }
        }
    }
}
