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
        private static readonly List<Component> _updatableComponents = new List<Component>();

        // Game Drawing
        private static readonly List<Camera> _cameras = new List<Camera>();
        private static readonly List<DrawableComponent> _drawbles = new List<DrawableComponent>();
        private static bool _hasDrawableDepthChange = true;

        private static readonly Queue<Action> _futureActions = new Queue<Action>();

        private static readonly LoadScreen _loadScreen = new DefaultLoadScreen();
        private static bool _isLoadScreenVisible = false;

        private static float _fixedUpdateTime;
        private static readonly float _fixedUpdateDuration = 1 / 60F;

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
            entity.OnAddedToScene();
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
            entity.OnRemovedFromScene();
        }

        public static T GetEntity<T>() where T : Entity
        {
            return GetEntities<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetEntities<T>() where T : Entity
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

        internal static void Update(RenderContext ctx, float dt)
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
                ctx.SaveState();
                ctx.ResetState();
                _loadScreen.Draw(ctx, dt);
                ctx.RestoreState();
            }
        }

        private static void ProcessSceneLogic(float dt)
        {
            _fixedUpdateTime += dt;

            // For each entity with Update
            foreach (var entity in _updatableEntities)
            {
                entity.Update(dt);
            }

            // Update each entity with FixedUpdate
            while (_fixedUpdateTime >= _fixedUpdateDuration)
            {
                _fixedUpdateTime -= _fixedUpdateDuration;

                // For each entity with FixedUpdate
                foreach (var entity in _fixedUpdatableEntities)
                {
                    entity.FixedUpdate();
                }
            }

            // Update each component with Update
            foreach (var c in _updatableComponents)
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

        private static void ProcessSceneDrawing(RenderContext ctx)
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
            protected internal override void Draw(RenderContext ctx, float dt)
            {
                ctx.Clear(BackgroundColor);

                var text = $"LOADING\n{Progress.Message}";
                var textSize = Font.Default.MeasureText(text, (400, ctx.Surface.Height), 32);
                textSize.Width = Calc.Max(400, textSize.Width);

                // Compute centered text rectangle
                var x = (ctx.Surface.Bounds.Width - textSize.Width) / 2F;
                var y = (ctx.Surface.Bounds.Height - textSize.Height) / 2F;
                var textRect = new Rectangle((x, y), textSize);
                textRect = textRect.Inflate(8);

                var progRect = textRect;
                progRect.Y += 4 + textRect.Height;
                progRect.Height = 4;

                // Draw text container
                ctx.Color = Color.White;
                ctx.DrawRect(textRect);

                // Draw text
                ctx.Color = Color.DarkGray;
                ctx.DrawText(text, textRect.Inflate(-8), Font.Default, 32, TextAlign.Center);

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
