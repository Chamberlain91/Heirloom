using System;

using Heirloom.Drawing;
using Heirloom.Input;
using Heirloom.Math;
using Heirloom.Collections.Spatial;

namespace Heirloom.Runtime
{
    public abstract class Game : IDisposable
    {
        protected Game()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Attempting to create multiple instances of the game. One must shutdown first.");
            }

            // 
            Instance = this;
            InitializeInputDevices();
            Initialize();

            // Goto first scene
            var firstScene = Startup();
            Goto(firstScene);
        }

        protected abstract void RequestInputDevices(out Keyboard keyboard, out Mouse mouse, out Gamepad gamepad, out Touch touch);

        protected abstract Scene Startup();

        public virtual void Dispose()
        {
            Shutdown();
        }

        // == STATIC API

        private static Scene _nextScene;
        private static Scene _scene;

        public static Game Instance { get; internal set; }

        public static Scene Scene => _nextScene ?? _scene;

        public static LoadingScreen LoadingScreen { get; } = new DefaultLoadingScreen();

        public static Keyboard Keyboard { get; private set; }

        public static Mouse Mouse { get; private set; }

        public static Gamepad Gamepad { get; private set; }

        public static Touch Touch { get; private set; }

        public static GameInput Input { get; private set; }

        private static void Initialize()
        {
            // Detects all assets and loaders
            AssetManifest.Initialize();

            // Initialize asset database
            Assets.Initialize();

            // Initialize time
            Time.Initialize();
        }

        private static void InitializeInputDevices()
        {
            // Get input devices
            Instance.RequestInputDevices(
                out var keyboard, out var mouse, out var gamepad, out var touch);

            // Store input devices
            Keyboard = keyboard ?? Keyboard.Null;
            Mouse = mouse ?? Mouse.Null;
            Gamepad = gamepad ?? Gamepad.Null;
            Touch = touch ?? Touch.Null;

            //
            Input = new GameInput();
        }

        private static void Shutdown()
        {
            Instance = null;
        }

        public static void Goto(Scene scene)
        {
            _nextScene = scene ?? throw new ArgumentNullException(nameof(scene));
        }

        protected static void GameTick(RenderContext ctx)
        {
            // Did we request to change scenes?
            if (_nextScene != null)
            {
                // Exit scene
                _nextScene?.Exit();

                // Set new scene
                _scene = _nextScene;
                _nextScene = null;

                // Enter scene
                _scene.Enter();
            }

            // 
            Time.BeginFrame();

            // Update input system
            Input.Update();

            // Update the scene
            _scene.Update();

            // Render the scene
            _scene.Render(ctx);

            // 
            DrawFPSOverlay(ctx);
        }

        private static void DrawFPSOverlay(RenderContext context)
        {
            context.ResetState();

            // 
            var delta = StringExtras.GetHumanTime(Time.Delta);
            var average = StringExtras.GetHumanTime(1F / Time.FrameRate);

            // 
            var text = $"{average} ({Time.FrameRate.ToString("0.00")} fps)\n{delta} ({(1F / Time.Delta).ToString("0.00")} fps)";
            var size = Font.Default.MeasureText(text, 16) + (4, 2);
            var left = context.Surface.Width - size.Width - 4;

            // Draw's an rectangle
            context.Draw(Image.White, Matrix.CreateTransform((left, 4), 0, (Vector) size));
            context.DrawText(text, (left + 4, 4), TextAlign.Left, Font.Default, 16, Color.DarkGray);
        }

        private sealed class DefaultLoadingScreen : LoadingScreen
        {
            protected internal override void Render(RenderContext ctx)
            {
                const int PADDING = 4;
                var font = Font.Default;

                // Measure text box, from 1/4 to 1/2 surface width
                var maxSize = new Size(ctx.Surface.Width / 2, float.MaxValue);
                var textSize = font.MeasureText(Message, maxSize, 16);
                textSize.Width = Calc.Max(textSize.Width, ctx.Surface.Width / 4);

                // Measure background panel
                var position = new Vector((ctx.Surface.Width - textSize.Width) / 2F, (ctx.Surface.Height - textSize.Height) / 2F);
                var box = new Rectangle(position, textSize + (0, 3 * PADDING));

                // Measure progress bar and bar recess
                var progressBar = new Rectangle(position + (0, PADDING + textSize.Height), new Size(textSize.Width, 2 * PADDING));
                var progressNow = new Rectangle(position + (0, PADDING + textSize.Height), new Size(textSize.Width * Progress, 2 * PADDING));

                // Clear background to dark cyan
                ctx.Clear(Color.Cyan * Color.DarkGray);

                // Render progress bar
                ctx.DrawRect(box.Inflate(PADDING), Color.LightGray);
                ctx.DrawText(Message, position, TextAlign.Left, font, 16, Color.DarkGray);
                ctx.DrawRect(progressBar, Color.Gray);
                ctx.DrawRect(progressNow, Color.DarkGray);
            }
        }
    }
}
