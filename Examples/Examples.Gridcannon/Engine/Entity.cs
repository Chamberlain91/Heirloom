using System;
using System.Diagnostics;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Gridcannon.Engine
{
    public class Entity
    {
        public Vector Position;

        public float Rotation;

        private int _depth = 0;

        public Entity(Image image, Vector position = default, float rotation = default)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
            Position = position;
            Rotation = rotation;
        }

        public Image Image { get; set; }

        public Scene Scene { get; internal set; }

        public Rectangle Bounds { get; private set; }

        public Matrix Matrix { get; private set; }

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

        internal virtual void Update(float dt)
        {
            // 
            Matrix = Matrix.CreateTransform(Position, Rotation, Vector.One);

            // Compute bounds (rotation?)
            var bounds = Image.Bounds;
            bounds.Position += Position;
            Bounds = bounds;
        }

        internal virtual void Draw(RenderContext ctx)
        {
            if (Image == null) { return; }
            ctx.DrawImage(Image, Matrix);
        }

        [Conditional("DEBUG")]
        internal virtual void DrawDebug(RenderContext ctx)
        {
            ctx.Color = Color.Green;
            ctx.DrawRectOutline(Bounds);
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

