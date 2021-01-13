using System;
using System.Threading;

using Heirloom.IO;
using Heirloom.Sound;

namespace Heirloom.Examples.PlayMusic
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            // Please refer to: https://www.bensound.com/licensing for license of the mp3 file
            Console.WriteLine("Now Playing: Dubstep from https://www.bensound.com/");

            // Create an (streaming) audio source and play
            var source = new AudioSource(Files.OpenStream("bensound-dubstep.mp3"));
            source.Play();

            // Sleep thread for duration of the song
            Thread.Sleep((int) (source.Duration * 1000));
        }
    }
}
