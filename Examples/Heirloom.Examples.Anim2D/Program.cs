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
        public Armature[] Armatures;
        public Matrix[] Matrices;

        public Program()
            : base(CreateWindowGraphics())
        {
            // Load bicycle
            var armatureData = ArmatureData.LoadDragonBones("files/Dragon_ske.json");

            // construct armature
            Armatures = new Armature[1];
            Matrices = new Matrix[Armatures.Length];
            for (var i = 0; i < Armatures.Length; i++)
            {
                Armatures[i] = armatureData.CreateArmature();
                Armatures[i].Animation.PlayAtTime("stand", Calc.Random.NextFloat(), 0);

                Matrices[i] = Matrix.CreateTranslation(Calc.Random.NextVectorDisk(0));
            }
        }

        protected override void Update(float dt)
        {
            // 
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);
            Graphics.Performance.ShowOverlay = true;
            Graphics.SetCamera(Vector.Up * 200, 1 / 1F);

            // Draw each armature
            for (var i = 0; i < Armatures.Length; i++)
            {
                Armatures[i].Draw(Graphics, Matrices[i]);
            }

            // Update animation system (in background thread, to allow screen refresh to block)
            var animUpdate = Task.Run(() => { foreach (var arm in Armatures) { arm.AdvanceTime(dt); } });

            // Place picture on screen
            Graphics.Screen.Refresh();

            // Wait for animation task to complete
            animUpdate.Wait();
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
