using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Examples.SimpleDrawing
{
    internal class Program : GameWindow
    {
        public Program()
            : base("Example Game")
        { }

        protected override void Update()
        {
            // 
        }

        protected override void Draw(RenderContext ctx)
        {
            ctx.Clear(Color.DarkGray);
        }

        private static void Main(string[] _)
        {
            Application.Run(() =>
            {
                var game = new Program();
                game.SetFullscreen(Monitor.Default);
                game.Run(); // begin game
            });
        }
    }
}
