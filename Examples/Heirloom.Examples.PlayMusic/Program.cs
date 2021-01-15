using System;
using System.Threading;

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
            var clip = new AudioClip("bensound-dubstep.ogg");
            var source = new AudioSource(clip);
            source.Play();

            // Sleep thread for duration of the song
            Thread.Sleep((int) (source.Duration * 1000));
        }
    }
}
