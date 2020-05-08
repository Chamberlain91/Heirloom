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
                Chatbox = new Chatbox((8, 8, 300, 100));

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
                    focusBox.Velocity = Input.MouseDelta;

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
                // box.Velocity += (0, 1 / 8F); // Gravity
                box.Update(gfx);
            }

            // Handle Box-Box Collision
            // https://gamedevelopment.tutsplus.com/series/how-to-create-a-custom-physics-engine--gamedev-12715
            // https://en.wikipedia.org/wiki/Inelastic_collision
            for (var i = 0; i < Boxes.Count; i++)
            {
                for (var j = 0; j < Boxes.Count; j++)
                {
                    // Skip self
                    if (i == j) { continue; }

                    var b1 = Boxes[i];
                    var b2 = Boxes[j];

                    // If boxes are colliding, respond to that collision
                    if (b1.CheckCollision(gfx, b2, out var push))
                    {
                        // We require some overlap...
                        // todo: Somehow push is 0,0 when there is a collision detected
                        //       This check avoids that by requiring push by a small threshold
                        if (push.LengthSquared > Calc.Epsilon)
                        {
                            // Remove overlap due to collision
                            PositionalCorrection(b1, b2, push);

                            // Resolve collisions
                            ResolveCollision(b1, b2, push);
                        }
                    }
                }
            }

            // Update chatbox
            Chatbox.Update(gfx, dt);
        }

        private static void PositionalCorrection(Box b1, Box b2, Vector push)
        {
            const float THRESHOLD = 0.1F;

            // Only push if enough overlap has occurred to help prevent jitter.
            if (push.LengthSquared > THRESHOLD * THRESHOLD)
            {
                // Get mass (todo, for now use by area)
                var m1 = b1.Size.Area;
                var m2 = b2.Size.Area;

                // Get inverse mass (todo)
                var invM1 = (m1 > 0F) ? 1F / m1 : 0F;
                var invM2 = (m2 > 0F) ? 1F / m2 : 0F;

                var correction = push / (invM1 + invM2);

                // Push out, correcting overlap
                b1.Position -= invM1 * correction;
                b2.Position += invM2 * correction;
            }
        }

        private static void ResolveCollision(Box b1, Box b2, Vector push)
        {
            // Get mass (todo, for now use by area)
            var m1 = b1.Size.Area;
            var m2 = b2.Size.Area;

            // Get inverse mass (todo)
            var invM1 = (m1 > 0F) ? 1F / m1 : 0F;
            var invM2 = (m2 > 0F) ? 1F / m2 : 0F;

            // Get velocity references
            ref var v1 = ref b1.Velocity;
            ref var v2 = ref b2.Velocity;

            // TODO: GET FROM PHYSICS MATERIAL
            var restitution = .25F; // somewhat bouncy   (use lowest from material pair)

            // Compute normal 
            var normal = Vector.Normalize(push);

            // Compute magnitude of relative velocity along collision normal
            var normalRelVel = Vector.Project(v2 - v1, normal);
            if (normalRelVel > 0) { return; } // shapes moving apart should be ignored

            // Compute normal impulse
            var normalImpulse = m1 * m2 / (m1 + m2) * normalRelVel;

            // Apply impulse to velocities
            v1 += normalImpulse / m1 * normal * (1 + restitution);
            v2 -= normalImpulse / m2 * normal * (1 + restitution);

            // Compute magnitude of relative velocity along collision normal
            var rv = v2 - v1;
            var tangent = Vector.Normalize(rv - Vector.Dot(rv, normal) * normal);
            var frictionRelVel = Vector.Project(v2 - v1, tangent);

            // TODO: GET FROM PHYSICS MATERIAL
            var staticFrictionA = 0.1F;
            var staticFrictionB = 0.1F;
            var dynamicFrictionA = 0.25F;
            var dynamicFrictionB = 0.25F;

            // Compute normal impulse
            var frictionImpulse = m1 * m2 / (m1 + m2) * frictionRelVel;
            var mu = PythagoreanSolve(staticFrictionA, staticFrictionB);

            // Dynamic friction threshold...?
            if (Calc.Abs(frictionImpulse) >= normalImpulse * mu)
            {
                var dynamicFriction = PythagoreanSolve(dynamicFrictionA, dynamicFrictionB);
                frictionImpulse = frictionImpulse * dynamicFriction;
            }

            // Apply impulse to velocities
            v1 += tangent * frictionImpulse * invM1;
            v2 -= tangent * frictionImpulse * invM2;

            static float PythagoreanSolve(float a, float b)
            {
                return Calc.Sqrt((a * a) + (b * b));
            }
        }
    }
}
