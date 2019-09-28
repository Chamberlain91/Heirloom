using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Gridcannon.Engine
{
    public abstract class Entity
    {
        private int _depth = 0;

        private readonly List<Component> _components;

        public Entity(Image image)
        {
            _components = new List<Component>();

            Image = image ?? throw new ArgumentNullException(nameof(image));

            // Default Components
            Transform = new Transform(this);
        }

        public Transform Transform { get; }

        public Image Image { get; set; }

        public Scene Scene { get; internal set; }

        public Rectangle Bounds { get; private set; }

        public int Depth
        {
            get => _depth;

            set
            {
                if (_depth != value)
                {
                    _depth = value;
                    Scene.MarkDepthChange();
                }
            }
        }

        public C GetComponent<C>() where C : Component
        {
            return GetComponents<C>()
                  .FirstOrDefault();
        }

        public IEnumerable<C> GetComponents<C>() where C : Component
        {
            // todo: could this cause a concurrent modification exception?
            foreach (var component in _components)
            {
                if (component is C c)
                {
                    yield return c;
                }
            }
        }

        protected abstract void Update(float dt);

        protected abstract void Draw(RenderContext ctx);

        internal void InternalUpdate(float dt)
        {
            UpdateBounds();

            // Update each component and entity
            foreach (var c in GetComponents<Component>()) { c.Update(dt); }
            Update(dt);
        }

        internal void InternalDraw(RenderContext ctx)
        {
            // Draw each component and entity
            foreach (var c in GetComponents<DrawableComponent>()) { c.Draw(ctx); }
            Draw(ctx);

            // TODO: Make into SpriteComponent/ImageComponent?
            if (Image == null) { return; }
            ctx.DrawImage(Image, Transform.Matrix);
            DebugDraw(ctx);
        }

        [Conditional("DEBUG")]
        private void DebugDraw(RenderContext ctx)
        {
            ctx.Color = Color.Green;
            ctx.DrawRectOutline(Bounds);
        }

        private void UpdateBounds()
        {
            // Compute bounds (rotation?)
            var bounds = Image.Bounds;
            bounds.Position += Transform.Position;
            Bounds = bounds;
        }

        internal virtual bool OnMouseClick(int button, bool isDown, Vector position)
        {
            return false;
        }

        internal virtual bool OnMouseMove(Vector position, Vector delta)
        {
            return false;
        }
    }
}

