using Heirloom;
using Heirloom.Desktop;

namespace Examples.SplitScreen
{
    internal class SplitScreen : GameLoop
    {
        // Stage
        public static readonly Color StageColor = Color.Parse("AA555555");
        public const float StageRadius = 256;

        // Players
        public readonly Player[] Players;

        public SplitScreen(Window window)
            : base(window.Graphics)
        {
            // Bind keyboard event
            window.KeyReleased += OnKeyEvent;
            window.KeyPressed += OnKeyEvent;

            // Set window size
            window.IsResizable = false;
            window.Size = (900, 500);

            // 
            Players = new Player[]
            {
                new Player((-64, 0), Calc.Pi, Color.Red),
                new Player((+64, 0), 0, Color.Green)
            };
        }

        protected override void Update(GraphicsContext gfx, float dt)
        {
            // Update Players
            foreach (var player in Players)
            {
                player.Update(dt);
            }

            var w = gfx.Surface.Width / 2;
            var h = gfx.Surface.Height;

            /*
              _   _                _              _  _     _        _ 
             | \ |_)  /\ \    /   |_) |   /\ \_/ |_ |_)   / \ |\ | |_ 
             |_/ | \ /--\ \/\/    |   |_ /--\ |  |_ | \   \_/ | \| |_ 

             */

            gfx.Viewport = (0, 0, w, h);

            // Set "camera" to follow Player 1
            gfx.SetCameraTransform(Players[0].SmoothPosition);
            gfx.Clear(Color.DarkGray * Players[0].Color);
            DrawWorld(gfx);

            // Draw Player 1 HUD
            gfx.GlobalTransform = Matrix.Identity;
            gfx.Color = Players[0].Color;
            gfx.DrawText("<WASD> to control Red", (16, 16), Font.Default, 32);

            /*
              _   _                _              _  _    ___        _  
             | \ |_)  /\ \    /   |_) |   /\ \_/ |_ |_)    | \    / / \ 
             |_/ | \ /--\ \/\/    |   |_ /--\ |  |_ | \    |  \/\/  \_/ 

            */

            gfx.Viewport = (w, 0, w, h);

            // Set "camera" to follow Player 2
            gfx.SetCameraTransform(Players[1].SmoothPosition);
            gfx.Clear(Color.DarkGray * Players[1].Color);
            DrawWorld(gfx);

            // Draw Player 2 HUD
            gfx.GlobalTransform = Matrix.Identity;
            gfx.Color = Players[1].Color;
            gfx.DrawText("<ARROW> to control Green", (gfx.Viewport.Width - 16, 16), Font.Default, 32, TextAlign.Right);
        }

        private void DrawWorld(GraphicsContext gfx)
        {
            // Draw Stage
            gfx.Color = StageColor;
            gfx.DrawCircle(Vector.Zero, StageRadius);

            // Draw Players
            foreach (var player in Players)
            {
                player.Draw(gfx);
            }
        }

        private void OnKeyEvent(Screen s, KeyEvent e)
        {
            var isDown = e.State.HasFlag(ButtonState.Down);

            // WASD for player 1
            if (e.Key == Key.W) { Players[0].OnInput(isDown, Player.Input.Forward); }
            if (e.Key == Key.A) { Players[0].OnInput(isDown, Player.Input.SpinLeft); }
            if (e.Key == Key.D) { Players[0].OnInput(isDown, Player.Input.SpinRight); }
            if (e.Key == Key.S) { Players[0].OnInput(isDown, Player.Input.Reverse); }

            // Arrow for player 2
            if (e.Key == Key.Up) { Players[1].OnInput(isDown, Player.Input.Forward); }
            if (e.Key == Key.Left) { Players[1].OnInput(isDown, Player.Input.SpinLeft); }
            if (e.Key == Key.Right) { Players[1].OnInput(isDown, Player.Input.SpinRight); }
            if (e.Key == Key.Down) { Players[1].OnInput(isDown, Player.Input.Reverse); }
        }

        private static void Main(string[] _)
        {
            Application.Run(() =>
            {
                var window = new Window("Split Screen", multisample: MultisampleQuality.High);

                // Create game and run main loop
                var program = new SplitScreen(window);
                program.Start();
            });
        }
    }
}
