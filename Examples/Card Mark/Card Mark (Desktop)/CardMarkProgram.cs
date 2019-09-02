using Heirloom.Drawing;
using Heirloom.Input;
using Heirloom.Platforms.Desktop;

namespace Heirloom.Examples.CardMark
{
    public class CardMarkProgram : GameWindow
    {
        public CardMarkApplication Application;

        public CardMarkProgram()
            : base("Card Mark", vsync: false)
        {
            // SetFullscreen(Screen.GetPrimaryScreen());
            Maximize();

            // Display FPS
            ShowDiagnostics = true;

            // 
            Keyboard.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    ExitFullscreen();
                }
            };
             
            // 
            Application = new CardMarkApplication(60, 10, 20000);
        }

        protected override void Update()
        {
            Application.Update(Delta);
        }

        protected override void Render(RenderContext ctx)
        {
            Application.Render(ctx, Delta);
        }

        private static void Main(string[] _)
        {
            Run(new CardMarkProgram());
        }
    }
}
