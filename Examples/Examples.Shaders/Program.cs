using Heirloom;
using Heirloom.Desktop;

namespace Examples.Shaders
{
    public static class Program
    {
        private const int Padding = 8;

        public static DistortionShader DistortionShader;
        public static GrayscaleShader GrayscaleShader;
        public static InvertShader InvertShader;

        public static Image[] Images;

        public static float Time;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Generate noise image
                var noiseImage = Image.CreateNoise(32, 32, 10, 3);
                noiseImage.Interpolation = InterpolationMode.Linear;
                noiseImage.Repeat = RepeatMode.Repeat;

                // Constructs instances of shaders
                DistortionShader = new DistortionShader(noiseImage) { Strength = 0.05F };
                GrayscaleShader = new GrayscaleShader();
                InvertShader = new InvertShader();

                // Load queen of hearts image
                Images = new[]{
                    new Image("files/cardJoker.png"),
                    new Image("files/cardHearts2.png"),
                    new Image("files/cardClubsA.png"),
                    new Image("files/cardHeartsQ.png")
                };

                // Create Window and fits it around 3 cards
                var window = new Window("Custom Shader Effects", MultisampleQuality.High)
                {
                    Size = (Padding + (Images[0].Width + Padding) * 4, Images[0].Height + Padding * 2),
                    IsResizable = false
                };

                // 
                var loop = GameLoop.Create(window.Graphics, Update);
                loop.Start();
            });
        }

        private static void Update(GraphicsContext gfx, float dt)
        {
            Time += dt;

            // Clear the frame
            gfx.Clear(Color.DarkGray);

            // Here we update some shader uniforms.
            DistortionShader.Offset = new Vector(Time / 5, Time / 2);
            GrayscaleShader.Blend = Calc.Osc(Time);
            InvertShader.Blend = Calc.Osc(Time);

            // Its important to note that updating a uniform effectively interrupts the batching 
            // mechanism (once the shader is actually used), causing an unreconcilable state and 
            // forces a flush (see the documentation on github for further description). Thus it
            // is good practice to try to engineer your shaders in a way that you can avoid
            // updating your uniforms multiple times within a single frame. However, sometime
            // this is difficult to avoid.

            // Draws w/ grayscale shader
            gfx.Shader = DistortionShader;
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
