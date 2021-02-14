using System;
using System.Threading.Tasks;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Extras.Anim2D;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.Anim2D
{
    public class Program : GameWrapper
    {
        public const int NumberOfArmatures = 1;
        public const float ScatterRadius = 0;

        public Armature[] Armatures;
        public Matrix[] Matrices;

        private bool _flipX;
        private bool _flipY;

        public Program()
            : base(CreateWindowGraphics())
        {
            // Load armature data from DragonBones 5.5 json format.
            // note: This function automatically assumes texture atlas '*_tex.json'
            // note: This step may load multiple armatures in the package
            ArmatureFactory.LoadDragonBones("files/dragon_ske.json");

            // Construct armature instances
            Armatures = new Armature[NumberOfArmatures];
            Matrices = new Matrix[Armatures.Length];
            for (var i = 0; i < Armatures.Length; i++)
            {
                // Create an instance of the "dragon" armature
                Armatures[i] = ArmatureFactory.CreateArmature("dragon");
                Armatures[i].Animation.PlayAtTime("stand", Calc.Random.NextFloat(), 0);
                Armatures[i].EnableDebug = true;

                // Position dragon randomly.
                Matrices[i] = Matrix.CreateTranslation(Calc.Random.NextVectorDisk(ScatterRadius));
            }
        }

        protected override void Update(float dt)
        {
            // 
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);
            Graphics.Performance.ShowOverlay = true;

            // Zoom camera to "fit" the dragons
            var zoom = Calc.Max(1F, ScatterRadius / (Graphics.Surface.Height / 2));
            Graphics.SetCamera(Vector.Up * 250, 1F / zoom);

            if (Input.IsKeyPressed(Key.Q)) { _flipX = !_flipX; }
            if (Input.IsKeyPressed(Key.E)) { _flipY = !_flipY; }

            // Draw each armature
            for (var i = 0; i < Armatures.Length; i++)
            {
                Armatures[i].FlipX = _flipX;
                Armatures[i].FlipY = _flipY;

                Armatures[i].Draw(Graphics, Matrices[i]);
            }

            // Place picture on screen &
            // Update armatures and process events
            var task = Task.Run(() => WorldClock.AdvanceTime(dt));
            Graphics.Screen.Refresh();
            task.Wait();
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Graph Visualization", MultisampleQuality.Low);
            // window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;
            window.Maximize();

            return window.Graphics;
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
