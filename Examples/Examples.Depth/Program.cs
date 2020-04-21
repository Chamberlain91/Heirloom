using System.Linq;

using Heirloom.Platforms.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;
using Heirloom;

namespace Examples.Depth
{
    public class Program : GameContext
    {
        public readonly EffectLayer Background;

        public readonly EffectLayer Foreground;

        public readonly Image[] Images;

        public readonly BlurSurfaceEffect Effect;
        public readonly EffectLayer EffectLayer;

        private float _time;

        public Program()
            : base(new Window("Post Processing Effects", MultisampleQuality.None, vsync: false), MultisampleQuality.None)
        {
            Window.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Standard;
            Window.Maximize();

            // Load all images (.png)
            Images = Files.GetEmbeddedFiles()
                          .Where(f => f.Assembly == typeof(Program).Assembly)
                          .Select(f => f.Identifiers.First())
                          .Where(i => i.EndsWith(".png"))
                          .Select(i => new Image(i))
                          .ToArray();

            // Set image origin to center
            foreach (var image in Images)
            {
                image.Origin = (IntVector) image.Size / 2;
            }

            // Create blur effect layer
            EffectLayer = new EffectLayer(0, Effect = new BlurSurfaceEffect(2, 25), 2);

            // Add blur effect layer (entities below the layer get the effect)
            Renderer.EffectLayers.Add(EffectLayer);
            Renderer.BackgroundColor = Color.Black;

            // Insert 200 cards with random position and depth
            for (var i = 0; i < 200; i++)
            {
                // Create a randomly positioned card
                var card = Create<Card>(Calc.Random.NextVector((Vector.Zero, Window.Size)));
                card.Depth = Calc.Random.Next(-10, +10);
                card.Image = Calc.Random.Choose(Images);
            }
        }

        protected override void Update(float dt)
        {
            _time += dt;
            Effect.Strength = 5 + Calc.Osc(_time) * 10;
        }

        public class Card : Entity
        {
            public Image Image;

            public float Angle = Calc.Random.NextFloat(0, Calc.Pi);

            public Vector Velocity = Calc.Random.NextUnitVector() * 10;

            internal override void Draw(Graphics gfx, float dt)
            {
                gfx.Color = Color.Lerp(Color.DarkGray, Color.White, Calc.Between(Depth, -10, +10));
                gfx.DrawImage(Image, Position, Angle, Vector.One);
            }

            internal override void Update(float dt)
            {
                var parallax = Calc.Lerp(0.5F, 1.5F, Calc.Between(Depth, -10, +10));
                Position += Velocity * dt * parallax;

                var bounds = new Rectangle(Vector.Zero, Game.Window.Size);
                if (Position.X < bounds.Left && Velocity.X < 0) { Velocity.X *= -1; }
                if (Position.Y < bounds.Top && Velocity.Y < 0) { Velocity.Y *= -1; }
                if (Position.X > bounds.Right && Velocity.X > 0) { Velocity.X *= -1; }
                if (Position.Y > bounds.Bottom && Velocity.Y > 0) { Velocity.Y *= -1; }
            }
        }

        private static void Main(string[] args)
        {
            Log.SetVerbosity(LogVerbosity.Debug, "Heirloom.Drawing.OpenGLES");
            Log.SetVerbosity(LogVerbosity.Debug, "Heirloom.Drawing");
            Log.SetVerbosity(LogVerbosity.Debug);

            Application.Run(() =>
            {
                // Create program and begin render loop
                var game = new Program();
                game.Loop.Start();
            });
        }
    }
}
