using System.Threading;

using Heirloom.IO;
using Heirloom.Sound;

namespace Heirloom.Examples.PlayMusic
{
    static class Program
    {
        static void Main(string[] args)
        {
            var source = new AudioSource(Files.OpenStream("music.mp3"));
            source.Play();

            Thread.Sleep((int) (source.Duration * 1000));
        }
    }
}
