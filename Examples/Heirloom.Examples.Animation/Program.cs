using System.Threading.Tasks;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Extras.Animation;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.Animation
{
    public class Program : GameWrapper
    {
        public const int NumberOfArmatures = 1000;
        public const float ScatterRadius = 300;

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

                //// Enable debug drawing of armature
                //Armatures[i].EnableDebug = true;

                // Position dragon randomly.
                var p = Vector.GetParabolicSpiralPoint(i) * ScatterRadius;
                Matrices[i] = Matrix.CreateTranslation(p);
            }
        }

        protected override void Update(float dt)
        {
            // 
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);
            Graphics.Performance.ShowOverlay = true;

            // Zoom camera to "fit" the dragons
            var zoom = Calc.Max(1F, ScatterRadius * Calc.Sqrt(NumberOfArmatures) / (Graphics.Surface.Height / 2));
            Graphics.SetCamera(Vector.Up * 250, 1F / zoom);

            // Flip armatures
            if (Input.IsKeyPressed(Key.W)) { _flipY = false; }
            if (Input.IsKeyPressed(Key.A)) { _flipX = true; }
            if (Input.IsKeyPressed(Key.S)) { _flipY = true; }
            if (Input.IsKeyPressed(Key.D)) { _flipX = false; }

            // Draw each armature
            for (var i = 0; i < Armatures.Length; i++)
            {
                // Configure armature flipping
                Armatures[i].FlipX = _flipX;
                Armatures[i].FlipY = _flipY;

                // Draw armature
                Armatures[i].Draw(Graphics, Matrices[i]);
            }

            // Update armatures and present graphics to the screen
            var task = Task.Run(() => { foreach (var armature in Armatures) { armature.Update(dt); } });
            Graphics.Screen.Refresh();
            task.Wait();
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Graph Visualization", MultisampleQuality.Medium);
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
