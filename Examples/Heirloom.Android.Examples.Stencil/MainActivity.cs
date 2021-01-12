using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Android.App;
using Android.Content.PM;
using Android.Media;
using Android.OS;

using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Mathematics;
using Heirloom.Sound;
using Heirloom.Sound.Android;
using Heirloom.Utilities;

using static StbVorbisSharp.StbVorbis;

using Image = Heirloom.Drawing.Image;

namespace Heirloom.Android.Examples.Stencil
{
    [Activity(Label = "Stencil Example",
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public sealed class MainActivity : GraphicsActivity
    {
        private float _time;

        public static readonly Image Image = new Image("zelda.jpg");

        public readonly GameLoop Loop;

        public MainActivity()
        {
            Loop = new GameLoop(Update);
        }

        protected override void GraphicsResume()
        {
            Loop.Start();

            // Begin audio pump
            var thread = new System.Threading.Thread(AudioThread);
            thread.Start();
        }

        private void AudioThread()
        {
            //global::Android.OS.Process.SetThreadPriority(ThreadPriority.Audio);

            var formatBuilder = new AudioFormat.Builder();
            formatBuilder.SetChannelMask(ChannelOut.Stereo);
            formatBuilder.SetEncoding(Encoding.Pcm16bit);
            formatBuilder.SetSampleRate(44100);

            var trackBuilder = new AudioTrack.Builder();
            trackBuilder.SetAudioFormat(formatBuilder.Build());
            trackBuilder.SetPerformanceMode(AudioTrackPerformanceMode.LowLatency);
            trackBuilder.SetTransferMode(AudioTrackMode.Stream);

            var track = trackBuilder.Build();
            track.Play();

            Log.Warning("BEGIN DECODE");
            //var rawBytes = Files.OpenStream("music.ogg").ReadAllBytes();
            //var audio = DecodeOgg(rawBytes);
            var rawBytes = Files.OpenStream("music.mp3").ReadAllBytes();

            var decoder = new Mp3Decoder(rawBytes);
            var audio = new short[decoder.Length];
            decoder.Decode(new Span<short>(audio));

            Log.Warning("END DECODE");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var samples = new short[track.BufferSizeInFrames];
            var time = 0;

            var elapsedError = 0;
            while (Loop.IsRunning)
            {
                var elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000F;
                var elapsedSamples = elapsedError + (int) (elapsedSeconds * 44100 * 2);

                if (elapsedSamples >= samples.Length)
                {
                    elapsedError = elapsedSamples - samples.Length;
                    stopwatch.Restart();

                    // Gather output samples
                    for (var i = 0; i < samples.Length; i++)
                    {
                        samples[i] = audio[(time + i) % audio.Length];
                    }

                    // Submit samples to stream
                    track.Write(samples, 0, samples.Length);
                    time += samples.Length;
                }

                // is this important?
                System.Threading.Thread.Yield();
            }

            track.Dispose();
        }

        private unsafe short[] DecodeOgg(byte[] data)
        {
            stb_vorbis vorbis;
            fixed (byte* b = data)
            {
                vorbis = stb_vorbis_open_memory(b, data.Length, null, null);
            }

            var info = stb_vorbis_get_info(vorbis);
            var length = stb_vorbis_stream_length_in_samples(vorbis);
            Log.Warning($"OGG is {length} samples.");

            var samples = new short[length];
            fixed (short* p_samples = samples)
            {
                var count = stb_vorbis_get_samples_short_interleaved(vorbis, 2, p_samples, (int) length);
                Log.Warning($"OGG read {count} samples.");
            }

            return samples;
        }

        protected override void GraphicsPause()
        {
            Loop.Stop();
        }

        public void Update(float dt)
        {
            // Enable the performance overlay
            Graphics.Performance.ShowOverlay = true;

            // Advance time
            _time += dt;

            // Render stencil example
            RenderStencilTest(Graphics, Calc.Sin(_time) * 15);

            // Present graphics to screen
            Graphics.Screen.Refresh();
        }

        private static void RenderStencilTest(GraphicsContext gfx, float angle)
        {
            gfx.ResetState();
            gfx.Clear(Color.Yellow * Color.DarkGray);

            // Draw base image (darkened)
            gfx.Color = Color.Gray;
            DrawBackgroundImages(gfx, Image, angle);

            // Draw a stencil mask
            gfx.BeginStencil();
            gfx.PushState();
            {
                var center = (Vector) gfx.Surface.Size / 2F;
                gfx.Transform = CreateRotationCenter(angle / 2F * Calc.ToRadians, center);
                gfx.DrawText("Princess Zelda", center, Font.Default, 200, TextAlign.Center | TextAlign.Middle);
            }
            gfx.PopState();
            gfx.EndStencil();

            // White for full brightness
            gfx.Color = Color.White;

            // Draw image (uses above stencil)
            DrawBackgroundImages(gfx, Image, angle);

            // Clear the stencil, back to regular drawing.
            gfx.ClearStencil();

            // Draw regular text again
            gfx.DrawText($"Heirloom 2D Graphics", (gfx.Surface.Width - 8, 8), Font.Default, 16, TextAlign.Top | TextAlign.Right);
        }

        private static void DrawBackgroundImages(GraphicsContext gfx, Image image, float angle)
        {
            var center = (IntVector) (image.Size / 2F);

            var tranformA = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - (image.Size * 10)) / 2) * CreateRotationCenter(angle / 10F * Calc.ToRadians, center);
            gfx.DrawImage(image, tranformA * Matrix.CreateScale(10));

            var transformB = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - image.Size) / 2) * CreateRotationCenter(angle * Calc.ToRadians, center);
            gfx.DrawImage(image, transformB);
        }

        private static Matrix CreateRotationCenter(float angle, Vector center)
        {
            return Matrix.CreateTranslation(center)
                 * Matrix.CreateRotation(angle)
                 * Matrix.CreateTranslation(-center);
        }
    }
}
