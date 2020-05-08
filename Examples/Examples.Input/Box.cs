
using Heirloom;

namespace Examples.Input
{
    public sealed class Box
    {
        public readonly (Color, Color) Colors;

        public readonly Size Size;

        public Vector Position;

        public Vector Velocity;

        public Box(Vector position, Size size, (Color, Color) colors)
        {
            Colors = colors;
            Position = position;
            Size = size;
        }

        public Rectangle Bounds => new Rectangle(Position, Size);

        public void Update(GraphicsContext gfx)
        {
            Position += Velocity;

            // 
            Bounce(gfx);

            // Draw Box
            gfx.Color = Colors.Item2;
            gfx.DrawRect(Bounds);
            gfx.Color = Colors.Item1;
            gfx.DrawRectOutline(Bounds, 3);
        }

        private void Bounce(GraphicsContext gfx)
        {
            // Bounce...
            if (Bounds.Left < 0)
            {
                Position.X -= Bounds.Left;
                Velocity.X *= -1 * 0.2F;
                Velocity.Y *= 0.5F;
            }

            if (Bounds.Top < 0)
            {
                Position.Y -= Bounds.Top;
                Velocity.Y *= -1 * 0.2F;
                Velocity.X *= 0.5F;
            }

            if (Bounds.Right >= gfx.Screen.Width)
            {
                Position.X -= Bounds.Right - gfx.Screen.Width;
                Velocity.X *= -1 * 0.2F;
                Velocity.Y *= 0.5F;
            }

            if (Bounds.Bottom >= gfx.Screen.Height)
            {
                Position.Y -= Bounds.Bottom - gfx.Screen.Height;
                Velocity.Y *= -1 * 0.2F;
                Velocity.X *= 0.5F;
            }
        }

        public bool CheckCollision(GraphicsContext gfx, Box other, out Vector push)
        {
            if (Bounds.Overlaps(other.Bounds))
            {
                push = Vector.Zero;

                if (Bounds.Top <= other.Bounds.Top && Bounds.Bottom > other.Bounds.Top)
                {
                    // Collision Top
                    push.Y += Bounds.Bottom - other.Bounds.Top;
                }
                else
                if (Bounds.Bottom >= other.Bounds.Bottom && Bounds.Top < other.Bounds.Bottom)
                {
                    // Collison Bottom
                    push.Y += Bounds.Top - other.Bounds.Bottom;
                }

                if (Bounds.Right > other.Bounds.Left && Bounds.Left < other.Bounds.Left)
                {
                    // Collision Left
                    push.X += Bounds.Right - other.Bounds.Left;
                }
                else
                if (Bounds.Right >= other.Bounds.Right && Bounds.Left < other.Bounds.Right)
                {
                    // Collison Right
                    push.X += Bounds.Left - other.Bounds.Right;
                }

                Vector point;

                // Push along smaller penetration
                if (Calc.Abs(push.X) < Calc.Abs(push.Y))
                {
                    push.Y = 0;

                    if (push.X < 0) { point = (Bounds.TopLeft + Bounds.BottomLeft) / 2F; }
                    else { point = (Bounds.TopRight + Bounds.BottomRight) / 2F; }
                }
                else
                {
                    push.X = 0;

                    if (push.Y < 0) { point = (Bounds.TopLeft + Bounds.TopRight) / 2F; }
                    else { point = (Bounds.BottomLeft + Bounds.BottomRight) / 2F; }
                }

                gfx.Color = Color.Yellow;
                gfx.DrawLine(point - push, point + push, 4);
                gfx.Color = Color.White;

                return true;
            }
            else
            {
                push = default;
                return false;
            }
        }
    }
}
