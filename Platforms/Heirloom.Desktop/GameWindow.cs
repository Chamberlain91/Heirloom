using System;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public abstract class GameWindow : Window
    {
        private const float FpsSampleDuration = 1F;

        private float _fpsTime;
        private int _fpsCount;

        protected GameWindow(string title, bool vsync = true, bool transparent = false)
            : this(800, 600, title, vsync, transparent)
        { }

        protected GameWindow(int width, int height, string title, bool vsync = true, bool transparent = false)
            : base(width, height, title, vsync, transparent)
        { }

        /// <summary>
        /// Enable or disable drawing the FPS overlay.
        /// </summary>
        public bool ShowFPSOverlay { get; set; } = false;

        /// <summary>
        /// The FPS of the game loop.
        /// </summary>
        public float FrameRate { get; private set; }

        /// <summary>
        /// Is the game thread running?
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Run the game thread.
        /// </summary>
        public void Run()
        {
            if (IsRunning)
            {
                throw new InvalidOperationException("Unable to run the game thread a second time.");
            }

            RunBackgroundThread(() =>
            {
                IsRunning = true;

                var time = Glfw.GetTime();

                // 
                while (!IsClosed)
                {
                    // Compute frame time
                    var now = Glfw.GetTime();
                    var delta = (float) (now - time);
                    time = now;

                    // == Update Phase

                    Update(delta);

                    // == Render Phase

                    RenderContext.ResetState();
                    Draw(RenderContext, delta);

                    // == Render FPS Overlay

                    if (ShowFPSOverlay)
                    {
                        RenderContext.ResetState();

                        var text = $"FPS: {FrameRate.ToString("0.00")}";
                        var size = Font.Default.MeasureText(text, 16);
                        RenderContext.DrawRect(new Rectangle(FramebufferSize.Width - 16 - size.Width - 3, 16, size.Width + 4, size.Height + 1), Color.DarkGray);
                        RenderContext.DrawText(text, new Vector(FramebufferSize.Width - 16, 16), TextAlign.Right, Font.Default, 16, Color.Pink);
                    }

                    // == Swap Buffers

                    RenderContext.SwapBuffers();

                    // == Compute Timing Metrics

                    ComputeFPS(delta);
                }

                // 
                IsRunning = false;
            });
        }

        private static void RunBackgroundThread(ThreadStart threadStart)
        {
            var thread = new Thread(threadStart) { Name = "Game Thread", IsBackground = true };
            thread.Start();
        }

        private void ComputeFPS(float delta)
        {
            _fpsTime += delta;
            _fpsCount++;

            if (_fpsTime >= FpsSampleDuration)
            {
                // hz, events/time
                FrameRate = _fpsCount / _fpsTime;

                _fpsCount = 0;
                _fpsTime = 0;
            }
        }

        protected abstract void Update(float dt);

        protected abstract void Draw(RenderContext renderContext, float dt);
    }
}
