using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Heirloom.Input;
using Heirloom.IO.Networking;
using Heirloom.Platforms.Desktop;
using Heirloom.Runtime;

namespace OneBitHero
{
    public class OneBitHeroGame : Game
    {
        private static Window _window;

        private static void Main(string[] _)
        {
            // Create window
            _window = new Window(800, 600, "One Bit Hero");
            // _window.SetSwapInterval(0);
            _window.Maximize(); // 

            // Create game and loop until window is closed
            using (var game = new OneBitHeroGame())
            {
                while (_window.IsClosed == false)
                {
                    // Process window events
                    Window.PollEvents();

                    // Game tick (update, render, etc)
                    GameTick(_window.RenderContext);

                    // Update window
                    _window.RenderContext.SwapBuffers();

                    // Sleep to help reduce too tight of a game loop
                    Thread.Sleep(0);
                }
            }
        }

        private static void AttemptConnection(int number)
        {
            // Connect to server
            var conn = new NetworkConnection("127.0.0.1", 8080);

            if (!conn.IsConnected)
            {
                // Unable to acquire connection
                Console.WriteLine($"Connection {number} could not connect");
            }
            else
            {
                // 
                conn.Disconnected += () => Console.WriteLine($"Connection {number} Disconnected");
                conn.SendMessage(0, Encoding.UTF8.GetBytes("Hello"));
                conn.SendMessage(1, Encoding.UTF8.GetBytes("World"));
            }
        }

        protected override Scene Startup()
        {
            return new MainScene();
        }

        protected override void RequestInputDevices(out Keyboard keyboard, out Mouse mouse, out Gamepad gamepad, out Touch touch)
        {
            keyboard = _window.Keyboard;
            mouse = _window.Mouse;
            gamepad = _window.Gamepad;
            touch = null;
        }
    }
}
