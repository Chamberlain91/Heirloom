using System;
using System.Net;
using System.Threading;

using Heirloom.IO;
using Heirloom.Sound;
using Heirloom.Sound.Filters;

namespace Examples.MusicPlayer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // using var stream = new WebClient().OpenRead("http://uk5.internet-radio.com:8185/stream");
            using var stream = Files.OpenStream("files/wholesome-by-kevin-macleod.mp3");

            /**
             * Embedded MP3 Licenese:
             * 
             * Music from https://filmmusic.io
             * "Wholesome" by Kevin MacLeod(https://incompetech.com)
             * License: CC BY(http://creativecommons.org/licenses/by/4.0/)
             */

            // Initialize audio mixer
            AudioMixer.Initialize(false, 44100);
            // AudioMixer.AddEffect(new HighPassFilter(8000));
            // AudioMixer.AddEffect(new LowPassFilter(300));

            // Construct a new streaming audio source
            var source = new AudioSource(stream);
            source.Play();

            Console.WriteLine("Playing music!");

            // Block until song is complete
            var finished = false;
            source.PlaybackEnded += () => finished = true;
            SpinWait.SpinUntil(() => finished);

            Console.WriteLine("Thanks for listening!");
        }
    }
}
