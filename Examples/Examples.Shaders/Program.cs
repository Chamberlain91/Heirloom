using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Shaders
{
    public static class Program
    {
        private const int Padding = 8;

        public static Shader GrayscaleShader;
        public static Shader DistortShader;
        public static Shader InvertShader;

        public static Image[] Images;
        public static Image Noise;

        public static float Time;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Loads the inverted color shader
                GrayscaleShader = new Shader("files/grayscale.frag");
                DistortShader = new Shader("files/distort.frag");
                InvertShader = new Shader("files/invert.frag");

                // Load queen of hearts image
                Images = new[]{
                    new Image("files/cardJoker.png"),
                    new Image("files/cardHearts2.png"),
                    new Image("files/cardClubsA.png"),
                    new Image("files/cardHeartsQ.png")
                };

                // Generate noise image
                Noise = Image.CreateNoise(32, 32, 8, 6);

                // Set noise image
                DistortShader.SetUniform("uNoiseImage", Noise);

                // Create Window and fits it around 3 cards
                var window = new Window("Custom Shader Effects")
                {
                    Size = (Padding + (Images[0].Width + Padding) * 4, Images[0].Height + Padding * 2),
                    IsResizable = false
                };

                // 
                var loop = RenderLoop.Create(window.Graphics, Update);
                loop.Start();
            });
        }

        private static void Update(Graphics gfx, float dt)
        {
            Time += dt;

            // Clear the frame
            gfx.Clear(Color.DarkGray);

            // Here we update some shader uniforms.
            GrayscaleShader.SetUniform("uStrength", 0.5F + Calc.Cos(Time * Calc.Pi) * 0.5F);
            DistortShader.SetUniform("uTime", Time * 0.1F);

            // Its important to note that updating a uniform effectively interrupts the batching 
            // mechanism (once the shader is actually used), causing an unreconcilable state and 
            // forces a flush (see the documentation on github for further description). Thus it
            // is good practice to try to engineer your shaders in a way that you can avoid
            // updating your uniforms multiple times within a single frame. However, sometime
            // this is difficult to avoid.

            // Draws w/ grayscale shader
            gfx.Shader = DistortShader;
            gfx.DrawImage(Images[0], Matrix.CreateTranslation(Padding + (Padding + Images[0].Width) * 3, Padding));

            // Draws w/ grayscale shader
            gfx.Shader = GrayscaleShader;
            gfx.DrawImage(Images[1], Matrix.CreateTranslation(Padding + (Padding + Images[0].Width) * 2, Padding));

            // Draws w/ inversion shader
            gfx.Shader = InvertShader;
            gfx.DrawImage(Images[2], Matrix.CreateTranslation(Padding + (Padding + Images[0].Width) * 1, Padding));

            // Draws w/ default shader
            gfx.Shader = Shader.Default;
            gfx.DrawImage(Images[3], Matrix.CreateTranslation(Padding + (Padding + Images[0].Width) * 0, Padding));
        }
    }
}
