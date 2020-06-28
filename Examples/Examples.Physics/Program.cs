using System.Collections.Generic;

using Heirloom;
using Heirloom.Desktop;
using Heirloom.Geometry;

namespace Examples.Physics
{
    internal class Program : GameLoop
    {
        public static readonly Color Background = Color.Parse("222f3E");

        public readonly Simulation Simulation = new Simulation((0, 200));

        private const float FixedTimeStep = 1 / 60F;
        private float _fixedTime = 0;

        public Program()
            : base(new Window("Physics Example", MultisampleQuality.High), 60)
        {
            Graphics.Performance.OverlayMode = PerformanceOverlayMode.Simple;

            var floor = new Rectangle(-212.5F, -12.5F, 425, 25);

            // Add Scene Borders
            const float StaticBody = float.PositiveInfinity; // Infinite density/mass
            Simulation.Add(new RigidBody(floor, (250, 475), StaticBody));
            Simulation.Add(new RigidBody(floor, (50F, 250F), StaticBody) { Rotation = Calc.HalfPi });
            Simulation.Add(new RigidBody(floor, (450F, 250F), StaticBody) { Rotation = Calc.HalfPi });

            // Add shapes
            for (var i = 0; i < 80; i++)
            {
                if (i % 3 == 0)
                {
                    var position = new Vector(350, 200 - (i * 30));
                    var shape = new Circle(Vector.Zero, 16);
                    Simulation.Add(new RigidBody(shape, position));
                }
                else
                if (i % 3 == 1)
                {
                    var position = new Vector(250, 200 - (i * 30));
                    var shape = new Rectangle(-16, -16, 32, 32);
                    Simulation.Add(new RigidBody(shape, position));
                }
                else
                {
                    var points = new List<Vector>();
                    for (var q = 0; q < 10; q++)
                    {
                        points.Add(Calc.Random.NextVectorDisk() * 32);
                    }
                    var position = new Vector(150, 200 - (i * 30));
                    var shape = Polygon.CreateConvexHull(points);
                    Simulation.Add(new RigidBody(shape, position));
                }
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
