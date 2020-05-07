using System;
using System.Collections.Generic;

using Heirloom;
using Heirloom.Desktop;

namespace Examples.Input
{
    using Input = Heirloom.Input;

    internal class Program
    {
        public static Screen Screen;

        public static readonly List<Box> Boxes = new List<Box>();
        public static Chatbox Chatbox;

        public static object Focus { get; set; }

        private static Vector _focusOffset;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                Screen = new Window("Game Input Example", (800, 480)) { IsResizable = false };

                // Create chatbox
                Chatbox = new Chatbox((8, Screen.Surface.Height - 100 - 8, 300, 100));

                CreateBoxes(20, 24);
                CreateBoxes(10, 48);
                CreateBoxes(5, 72);
                CreateBoxes(1, 128);

                // Begin Render Loop
                var loop = GameLoop.Create(Screen.Graphics, OnUpdate);
                loop.Start();
            });
        }

        public static void CreateBoxes(int count, float size)
        {
            var siz = new Size(size, size);

            // Generate boxes
            for (var i = 0; i < count; i++)
            {
                // Random position
                var pos = Calc.Random.NextVector((Vector.Zero, Screen.Size - siz));

                // Add box to scene
                Boxes.Add(new Box(pos, siz, Calc.Random.Choose(Palette.BoxColors)));
            }
        }

        private static void OnUpdate(GraphicsContext gfx, float dt)
        {
            if (Focus != Chatbox)
            {
                if (Focus is Box focusBox)
                {
                    focusBox.Position = Input.MousePosition + _focusOffset;
                    focusBox.Velocity.Set(0, 0);

                    // Released
                    if (Input.CheckMouse(MouseButton.Left, ButtonState.Released))
                    {
                        focusBox.Velocity += Input.MouseDelta;
                        Focus = null;
                    }
                }
                // Check if we clicked on a box
                else if (Input.CheckMouse(MouseButton.Left, ButtonState.Pressed))
                {
                    foreach (var box in Boxes)
                    {
                        if (box.Bounds.Contains(Input.MousePosition))
                        {
                            _focusOffset = box.Position - Input.MousePosition;
                            Focus = box;
                            break;
                        }
                    }
                }
            }

            // Clear background color
            gfx.Clear(Palette.Background);

            // Draw boxes
            foreach (var box in Boxes)
            {
                box.Update(gfx);
            }

            // Handle Box-Box Collision
            for (var i = 0; i < Boxes.Count; i++)
            {
                for (var j = 0; j < Boxes.Count; j++)
                {
                    if (i == j) { continue; }

                    // If boxes are colliding, respond to that collision
                    if (Boxes[i].CheckCollision(gfx, Boxes[j], out var push))
                    {
                        // We require some overlap...
                        // todo: Somehow push is 0,0 when there is a collision detected
                        //       This check avoids that by requiring push by a small threshold
                        if (push.LengthSquared > Calc.Epsilon)
                        {
                            // Get weight by area
                            var m1 = Boxes[i].Size.Area;
                            var m2 = Boxes[j].Size.Area;

                            // Push out, correcting overlap
                            Boxes[i].Position -= push / 2;
                            Boxes[j].Position += push / 2;

                            // Get velocity references
                            ref var v1 = ref Boxes[i].Velocity;
                            ref var v2 = ref Boxes[j].Velocity;

                            // Material Properties
                            var restitution = .5F; // somewhat bouncy

                            // Compute normal 
                            var normal = Vector.Normalize(push);

                            // Compute magnitude of velocity along collision normal
                            var u1 = Vector.Project(v1, normal);
                            var u2 = Vector.Project(v2, normal);

                            // Compute normal impulse
                            // https://en.wikipedia.org/wiki/Inelastic_collision
                            var normalImpulse = m1 * m2 / (m1 + m2) * (1 + restitution) * (u2 - u1);

                            // Apply impulse to velocities
                            v1 += normalImpulse / m1 * normal;
                            v2 -= normalImpulse / m2 * normal;
                        }
                    }
                }
            }

            // Update chatbox
            Chatbox.Update(gfx, dt);
        }

        public class Box
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
                }

                if (Bounds.Top < 0)
                {
                    Position.Y -= Bounds.Top;
                    Velocity.Y *= -1 * 0.2F;
                }

                if (Bounds.Right >= gfx.Screen.Width)
                {
                    Position.X -= Bounds.Right - gfx.Screen.Width;
                    Velocity.X *= -1 * 0.2F;
                }

                if (Bounds.Bottom >= gfx.Screen.Height)
                {
                    Position.Y -= Bounds.Bottom - gfx.Screen.Height;
                    Velocity.Y *= -1 * 0.2F;
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
}
