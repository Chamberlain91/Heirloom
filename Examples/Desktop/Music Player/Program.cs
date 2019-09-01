using System;
using System.IO;
using System.Net;

using Heirloom.Sound;

namespace Heirloom.Examples.MusicPlayer
{
    public static class Program
    {
        private static void Main(string[] _)
        {
            HighlevelExample();
            // LowLevelExample();
        }

        private static void HighlevelExample()
        {
            using (var sRadio = CreateInternetRadioStream())
            {
                var radio = new AudioSource(sRadio);
                radio.Play();

                // 
                Console.WriteLine("You're listening to 'https://www.pureradio.eu/trance/'");
                Console.WriteLine("Press any key to terminate.");
                Console.ReadKey(true);
            }
        }

        private static void LowLevelExample()
        {
            using (var stream = CreateInternetRadioStream())
            using (var device = new InternetRadioDevice(stream))
            {
                // Begin processing audio
                device.Start();

                // 
                Console.WriteLine("You're listening to 'https://www.pureradio.eu/trance/'");
                Console.WriteLine("Press any key to terminate.");
                Console.ReadKey(true);
            }
        }

        // This stream is to an endless mp3 source, but just as easily
        // could have been a mp3, or ogg on disk. Additionally, you
        // may pass in an array of bytes of the in-memory copy of a
        // supported file format as well!
        private static Stream CreateInternetRadioStream()
        {
            var client = new WebClient();
            return client.OpenRead("http://93.158.201.101:8027/high");
        }
    }
}
