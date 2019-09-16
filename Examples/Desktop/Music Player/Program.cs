using System;
using System.Threading;

using Heirloom.IO;
using Heirloom.Sound;

namespace Examples.MusicPlayer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var stream = Files.OpenStream("files/wholesome-by-kevin-macleod.mp3"))
            {
                // Construct a new streaming audio source
                var source = new AudioSource(stream);
                source.Play();

                // Write Attributution (because CC4)
                Console.WriteLine("Music from https://filmmusic.io");
                Console.WriteLine("\"Wholesome\" by Kevin MacLeod(https://incompetech.com)");
                Console.WriteLine("License: CC BY(http://creativecommons.org/licenses/by/4.0/)");
                Console.WriteLine("\nPlaying music!");

                // Block until song is complete
                var finished = false;
                source.PlaybackEnded += () => finished = true;
                SpinWait.SpinUntil(() => finished);

                Console.WriteLine("Thanks for listening!");
            }
        }
    }
}
