using System.Collections.Generic;

using Heirloom;
using Heirloom.Desktop;

namespace Examples.Physics
{
    internal class Program : GameLoop
    {
        public static readonly Color Background = Color.Parse("222F3E");

        public readonly Simulation Simulation = new Simulation((0, 200));

        private const float FixedTimeStep = 1 / 60F;
        private float _fixedTime = 0;

        public Program()
            : base(new Window("Physics Example", MultisampleQuality.High))
        {
            Graphics.Performance.OverlayMode = PerformanceOverlayMode.Full;

            var floor = new Rectangle(-212.5F, -12.5F, 425, 25);

            // Add Scene Borders
            const float StaticBody = float.PositiveInfinity; // Infinite density/mass
            Simulation.Add(new RigidBody(floor, (250, 475), Vector.One, StaticBody));
            Simulation.Add(new RigidBody(floor, (50F, 250F), Vector.One, StaticBody) { Rotation = Calc.HalfPi });
            Simulation.Add(new RigidBody(floor, (450F, 250F), Vector.One, StaticBody) { Rotation = Calc.HalfPi });

            // Add shapes
            for (var i = 0; i < 50; i++)
            {
                var x = Calc.Random.NextFloat(250, 350);

                if (i % 2 == 0)
                {
                    var position = new Vector(x, 200 - (i * 30));
                    var shape = new Circle(Vector.Zero, 16);
                    Simulation.Add(new RigidBody(shape, position, RandomScale()));
                }
                else
                {
                    var points = new List<Vector>();
                    for (var q = 0; q < 10; q++)
                    {
                        points.Add(Calc.Random.NextVectorDisk() * 32);
                    }
                    var position = new Vector(x, 200 - (i * 30));
                    var shape = Polygon.CreateConvexHull(points);
                    Simulation.Add(new RigidBody(shape, position, RandomScale()));
                }
            }

            static Vector RandomScale()
            {
                var sx = Calc.Random.NextFloat(1F, 2F);
                var sy = Calc.Random.NextFloat(1F, 2F);
                return new Vector(sx, sy);
            }
        }

        protected override void Update(float dt)
        {
            Graphics.Clear(Background);

            _fixedTime += dt;
            while (_fixedTime >= FixedTimeStep)
            {
                _fixedTime -= FixedTimeStep;
                Simulation.Step(FixedTimeStep);
            }

            Simulation.Render(Graphics);
        }

        private static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
