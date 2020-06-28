using Heirloom;
using Heirloom.Desktop;

namespace Examples.GameInput
{
    internal class Program
    {
        public static Screen Screen;

        public static Chatbox Chatbox;

        public static Player Player;

        public static Image GroundTop;
        public static Image GroundBottom;

        public static Image[] Clouds;
        public static Image Rock, Bush;

        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                Screen = new Window("Game Input Example", (800, 480)) { IsResizable = false };
                Screen.Graphics.Performance.OverlayMode = PerformanceOverlayMode.Full;

                // 
                GroundTop = new Image("assets/tiles/grassMid.png");
                GroundBottom = new Image("assets/tiles/grassCenter.png");

                Bush = new Image("assets/tiles/bush.png");
                Rock = new Image("assets/tiles/rock.png");
                Clouds = new[] {
                    new Image("assets/tiles/cloud1.png"),
                    new Image("assets/tiles/cloud2.png"),
                    new Image("assets/tiles/cloud3.png")
                };

                // Create player
                Player = new Player();

                // 
                Chatbox = new Chatbox((8, Screen.Height - 8 - 100, 400, 100));
                Chatbox.EmoteDetected += emote => Player.SubmitEmote(emote);

                // Begin Render Loop
                var loop = new DefaultGameLoop(Screen, OnUpdate);
                loop.Start();
            });
        }

        private static void OnUpdate(GraphicsContext gfx, float dt)
        {
            Player.Update(dt, !Chatbox.HasFocus);
            OnDraw(gfx, dt);
        }

        private static void OnDraw(GraphicsContext gfx, float dt)
        {
            gfx.Clear(Palette.Background);

            // Draw World
            gfx.PushState(true);
            {
                var camera = Player.Position - (0, 100);
                gfx.SetCameraTransform(camera);

                // Draw environment
                for (var i = -12; i < 12; i++)
                {
                    gfx.DrawImage(GroundTop, new Vector(i * 70, 0));
                    gfx.DrawImage(GroundBottom, new Vector(i * 70, 70));
                }

                gfx.DrawImage(Rock, new Vector(20, -70));
                gfx.DrawImage(Bush, new Vector(-20, -70));

                var paralax = camera / 3F;
                gfx.DrawImage(Clouds[0], new Vector(+220, -170) + paralax);
                gfx.DrawImage(Clouds[1], new Vector(-70, -270) + paralax);
                gfx.DrawImage(Clouds[2], new Vector(-220, -170) + paralax);

                // Draw player
                Player.Draw(gfx, dt);
            }
            gfx.PopState();

            // Draw chatbox
            Chatbox.Draw(gfx, dt);
        }
    }
}
