using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.Drawing.Software;
using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Example.Sandbox
{
    internal sealed class Program : Application
    {
        public readonly GameLoop Loop;

        public readonly Window Window;

        public Image[] Images;

        public Particle[] Particles;

        public Rectangle Bounds; 

        public struct Particle
        {
            public Vector Position;
            public Vector Velocity;

            public Image Image;
        }

        public Program()
        {
            // At this point desktop window, graphics and audio systems have been initialized.
            Window = new Window("Meadows Example", (1280, 320), MultisampleQuality.None);
            RenderStencilTest(Window.Graphics, "hardware.jpg");

            //
            Window.Maximize();
            Bounds = new Rectangle(Vector.Zero, Window.Size);

            // 
            Images = new Image[500];
            for (var i = 0; i < Images.Length; i++)
            {
                var w = Calc.Random.Choose(16, 32, 64, 128);
                var h = Calc.Random.Choose(16, 32, 64, 128);

                Images[i] = new Image(w, h);
                Images[i].Clear(Calc.Random.NextColorHue());
            }

            // 
            Particles = new Particle[138000];
            for (var i = 0; i < Particles.Length; i++)
            {
                Particles[i] = new Particle
                {
                    Position = Calc.Random.NextVector(Bounds),
                    Velocity = Calc.Random.NextUnitVector(),
                    Image = Calc.Random.Choose(Images)
                };
            }

            // Create and start render loop
            Loop = new GameLoop(Update);
            Window.Closed += win => Loop.Stop();
            Loop.Start();
        }

        private void Update(float dt)
        {
            // ...?
            Window.Graphics.SetRenderTarget(Window.Surface);
            Window.Graphics.Color = Color.White;

            Window.Graphics.Clear(Color.DarkGray);

            for (var i = 0; i < Particles.Length; i++)
            {
                var image = Particles[i].Image;

                ref var vel = ref Particles[i].Velocity;
                ref var pos = ref Particles[i].Position;
                pos += vel;

                // 
                if (pos.X <= Bounds.Left || (pos.X + image.Width) >= Bounds.Right) { vel.X *= -1; }
                if (pos.Y <= Bounds.Top || (pos.Y + image.Height) >= Bounds.Bottom) { vel.Y *= -1; }

                Window.Graphics.DrawImage(image, Matrix.CreateTranslation(pos));
            }

            Window.Graphics.DrawText($"FPS: {Window.Graphics.Performance.FrameRate:0.00}", (10, 10), Font.Default, 32);
            Window.Refresh();
        }

        private static void Main(string[] args)
        {
            var renderHardware = true;

            // If running on hardware, we will run the application.
            // Otherwise we will use a software renderer.
            if (renderHardware) { Run<Program>(); }
            else
            {
                // Initialize graphics system with a software backend.
                // Only one backend can be established at a time.
                using var backend = new SoftwareGraphicsBackend();
                var context = backend.CreateContext(1280, 320);
                RenderStencilTest(context, "software.jpg");
            }
        }

        private static void RenderStencilTest(GraphicsContext gfx, string fileName)
        {
            // Load image and set its origin to the center...
            var image = new Image("colorful.jpg");
            var imageCenter = (IntVector) (image.Size / 2F);

            // 
            gfx.Clear(Color.Yellow * Color.DarkGray);
            gfx.InterpolationMode = InterpolationMode.Linear;

            // Draw base image (darkened)
            gfx.Color = Color.DarkGray;
            var imageTransform = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - image.Size) / 2) * ComputeCenteredRotation(imageCenter, 10 * Calc.ToRadians);
            gfx.DrawImage(image, imageTransform);

            // Draw a stencil mask
            gfx.BeginStencil();
            gfx.PushState();
            {
                // Set camera pointed at zero and tilted.
                // Then draw text at zero to populate the stencil.
                gfx.SetCamera(Vector.Down * 66F, rotation: 6 * Calc.ToRadians);
                gfx.DrawText("Stencil Text", Vector.Zero, Font.Default, 256, TextAlign.Center | TextAlign.Middle);
            }
            gfx.PopState();
            gfx.EndStencil();

            // Draw image (uses above stencil)
            gfx.Color = Color.White;
            gfx.DrawImage(image, imageTransform);

            // Clear the stencil, back to regular drawing.
            gfx.ClearStencil();

            // Draw regular text again
            gfx.Color = Color.Red;
            gfx.DrawText("Heirloom 2D Graphics", (gfx.Surface.Width - 8, 0), Font.Default, 24, TextAlign.Top | TextAlign.Right);

            // Write rendered image to disk
            var screenshot = gfx.GrabPixels();
            screenshot.Write(fileName);
        }

        private static Matrix ComputeCenteredRotation(Vector center, float rotation)
        {
            return Matrix.CreateTranslation(center)
                 * Matrix.CreateRotation(rotation)
                 * Matrix.CreateTranslation(-center);
        }
    }
}
