using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.Physics
{
    public sealed class Example : GameWindow
    {
        private readonly Simulation _simulation;

        private readonly Body _platform;

        private readonly Dictionary<IPolygon, Graphic> _graphics;

        private const float TimeStep = 1 / 60F;
        private float _accumTime = 0F;

        private Vector _mousePosition;

        // https://terminal.sexy/#HR8hxcjGKCoupUJCjJRA3pNfX4GdhWePXo2HcHiANztBzGZmtb1o8MZ0gaK-spS7ir63xcjG
        private static readonly Color _background = (Color) Pixel.Parse("1D1F21");
        private static readonly Color _foreground = (Color) Pixel.Parse("C5C8C6");
        private static readonly Color[] _colors = new[]
        {
            (Color) Pixel.Parse("A54242"),
            (Color) Pixel.Parse("5F819D"),
            (Color) Pixel.Parse("8C9440"),
            (Color) Pixel.Parse("8ABEB7"),
            (Color) Pixel.Parse("F0C674"),
            (Color) Pixel.Parse("5E8D87"),
            (Color) Pixel.Parse("CC6666"),
            (Color) Pixel.Parse("85678F"),
            (Color) Pixel.Parse("B294BB"),
            (Color) Pixel.Parse("B5BD68"),
            (Color) Pixel.Parse("DE935F"),
            (Color) Pixel.Parse("81A2BE"),
        };

        public Example()
            : base("Heirloom Physics")
        {
            // SetSwapInterval(0);
            Maximize();

            // 
            ShowDiagnostics = true;

            // Get mouse motion from event
            Mouse.Moved += (o, e) => _mousePosition = e.Position;

            // 
            _graphics = new Dictionary<IPolygon, Graphic>();

            // Trapezoid (platform) shape
            var trapezoid = new Polygon(new[]
            {
                new Vector(-3, -1) * 0.3F,
                new Vector(+3, -1) * 0.3F,
                new Vector(+1, +1) * 0.3F,
                new Vector(-1, +1) * 0.3F
            });

            // Floor (world borders) shape
            var floor = new Polygon(new[]
            {
                new Vector(-6, -0.2F),
                new Vector(+6, -0.2F),
                new Vector(+6, +0.2F),
                new Vector(-6, +0.2F)
            });

            // Large (blocker) rectangle
            var blocker = new Polygon(new[]
            {
                new Vector(-3, -1) * 0.5F,
                new Vector(+3, -1) * 0.5F,
                new Vector(+3, +1) * 0.5F,
                new Vector(-3, +1) * 0.5F,
            });

            // Collection of shapes chosen for dynamic bodies
            var shapes = new IPolygon[] {
                Polygon.CreateRegularPolygon(Vector.Zero, 4, 0.3F),
                Polygon.CreateRegularPolygon(Vector.Zero, 4, 0.2F),
                Polygon.CreateRegularPolygon(Vector.Zero, 3, 0.3F),
                Polygon.CreateRegularPolygon(Vector.Zero, 3, 0.2F),
            };

            // Create the physics world
            _simulation = new Simulation();

            // Create world (floor, walls, etc)
            _simulation.Add(new Body(floor, (+0.0F, +1F), false));
            _simulation.Add(new Body(floor, (-6.2F, -3F), false) { Rotation = Calc.HalfPi });
            _simulation.Add(new Body(floor, (+6.2F, -3F), false) { Rotation = Calc.HalfPi });
            _simulation.Add(new Body(floor, (+0.0F, -9F), false));

            // Platform that animates around the map to toss dynamic objects
            _platform = new Body(trapezoid, Vector.Zero, false);
            _simulation.Add(_platform);

            // Create large blockers get in the way of the dynamic objects
            _simulation.Add(new Body(blocker, (-2, -2), false) { Rotation = +2 });
            _simulation.Add(new Body(blocker, (+0, -4), false) { Rotation = 0 });
            _simulation.Add(new Body(blocker, (+2, -2), false) { Rotation = -2 });

            // Create field of random objects
            for (var y = 0; y < 14; y++)
            {
                for (var x = 0; x < 40; x++)
                {
                    var shape = Calc.Random.Choose(shapes);

                    var body = new Body(shape, ((x - 20) / 4F, -1 - y / 2F), true);
                    // todo: fix non uniform negative scaled collision shape bug
                    // todo: fix circles not scaling bug
                    //body.Scale = (2 / 3F, 1F);

                    _simulation.Add(body);
                }
            }
        }

        protected override void Update()
        {
            // Animates the trapezoid in a pretzel pattern
            _platform.Rotation = Time;
            _platform.Position = (Calc.Sin(Time * 0.4F) * 4, -4F - Calc.Cos(Time * 0.8F) * 3);

            // 
            _accumTime += Delta;

            //// Critically low FPS! We don't want to experience run away accumulation of time,
            //// so we will set a hard value on 1 simulated step. The entire simulation
            //// will slow down, but it won't progressively accumulate more and more work as it falls behind!
            //if (Delta > TimeStep && _accumTime > TimeStep) { _accumTime = TimeStep; }

            // Process accumulated time steps
            while (_accumTime >= TimeStep)
            {
                // Process time step (ie, 1/60th of a second)
                _simulation.Simulate(TimeStep);
                _accumTime -= TimeStep;
            }
        }

        protected override void Render(RenderContext ctx)
        {
            ctx.ResetState();
            var surface = ctx.Surface;

            ctx.Clear(_background);

            // Compute 'camera' matrix, centered bottom aligned creating a 10x10 world
            var scale = Calc.Min(surface.Width, surface.Height) / 10F;
            ctx.Transform = Matrix.CreateTransform(
                position: new Vector((surface.Width - scale) / 2F, surface.Height - scale),
                scale: new Vector(scale, scale),
                angle: 0);

            // == Draw Bodies

            {
                var i = 0;
                foreach (var body in _simulation.Bodies)
                {
                    var transform = body.Transform;
                    var shape = body.Shape;

                    var color = _colors[i++ % _colors.Length];
                    if (body.IsStatic) { color = _foreground; }

                    // 
                    if (!_graphics.TryGetValue(shape, out var g))
                    {
                        g = new Graphic(ctx, shape);
                        _graphics[shape] = g;
                    }

                    // 
                    ctx.Draw(g.Image, transform * g.Matrix, color);
                }
            }

            // == Visualize Broad Phase Neighbors

            {
                // Compute mouse position in world space
                var mousePos = ctx.InverseTransform * _mousePosition;

                // Get nearest body to mouse or platform
                var near = _simulation.Bodies.Where(b => Vector.Distance(mousePos, b.Position) < 0.2F).FirstOrDefault();
                if (near == null) { near = _platform; }

                // Draw marks on relevant bodies
                ctx.DrawCross(near.Position, Color.Magenta, 4, 2);
                foreach (var q in _simulation.Query(near))
                {
                    ctx.DrawCross(q.Position, Color.Magenta, 4, 1);
                }
            }

            // Draw World Origin
            ctx.DrawCross(Vector.Zero, Color.Yellow);

            // Draw statistics
            ctx.ResetState();
            ctx.DrawText($"{_simulation.NumberOfCollisions} Collisions ({_simulation.NumberOfCollisionTests})\n{_simulation.Bodies.Count} Bodies", (20, 20), TextAlign.Left, Font.Default, 16, Color.Yellow);
        }

        private class Graphic
        {
            public Image Image;

            public Matrix Matrix;

            public Graphic(RenderContext ctx, IPolygon shape)
            {
                // Approximates how big a screen pixel is in world units
                var pixelScale = 1F / ctx.ApproximatePixelScale;

                // 
                Image = CreateImage(ctx, shape, pixelScale, out var matrix);
                Matrix = matrix;
            }

            private static Image CreateImage(RenderContext ctx, IPolygon polygon, float objectToPixelScale, out Matrix imageToObject)
            {
                // 
                var edgeWidth = 2;
                var edgeOffset = edgeWidth / objectToPixelScale;
                var edgeExtra = edgeWidth * 2;

                // 
                var bounds = Rectangle.FromPoints(polygon);

                // Compute transformation matrices
                var inverseScale = 1F / objectToPixelScale;
                imageToObject = Matrix.CreateTransform(bounds.Min - (edgeOffset, edgeOffset), 0, (inverseScale, inverseScale));
                var objectToImage = Matrix.Inverse(imageToObject);

                // Draw polygon to surface and capture image
                using (var surface = ctx.CreateSurface((IntSize) (bounds.Size * objectToPixelScale) + (edgeExtra, edgeExtra)))
                using (ctx.PushState(reset: true))
                {
                    ctx.Surface = surface;

                    var mesh = Mesh.CreateFromConvexPolygon(polygon);
                    ctx.Draw(Image.White, mesh, objectToImage);
                    ctx.DrawPolygon(polygon, objectToImage, Color.Gray, edgeWidth);

                    // Constructs an image from this surface
                    return ctx.GrabPixels();
                }
            }
        }

        private static void Main(string[] _)
        {
            Run(new Example());
        }
    }
}
