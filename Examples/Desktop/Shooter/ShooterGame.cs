using System;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.Input;
using Heirloom.Platforms.Desktop;
using Heirloom.Runtime;

namespace Shooter
{
    public class ShooterGame : Game
    {
        private static Window _window;

        protected override void RequestInputDevices(out Keyboard keyboard, out Mouse mouse, out Gamepad gamepad, out Touch touch)
        {
            keyboard = _window.Keyboard;
            mouse = _window.Mouse;
            gamepad = _window.Gamepad;
            touch = null;
        }

        protected override Scene Startup()
        {
            return new MainScene();
        }

        private class MainScene : Scene
        {
            public Font Font;

            public PlayerShip Player;

            protected override void Enter()
            {
                foreach (var ident in AssetManifest.Identifiers)
                {
                    Console.WriteLine(ident);
                }

                Font = Assets.Get<Font>("fonts/kenvector_future.ttf");

                //// Create an audio player streaming a mp3 file
                //var music = new AudioStreamPlayer(AssetManager.OpenStream("audio/searching_the_cosmos.mp3"));
                //music.Looping = true;
                //music.Play();

                // 
                Scene.AddEntity(new Environment());
                // Camera.Follow = Scene.AddEntity(Player = new PlayerShip()).Transform;
            }

            protected override void Exit()
            {
            }
        }

        private static void Main(string[] args)
        {
            // Create window
            _window = new Window(800, 600, "Shooter");
            //_window.SetSwapInterval(0);
            _window.Maximize(); // 

            // Create game and loop until window is closed
            using (var game = new ShooterGame())
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
    }
}
