using System;
using System.Collections.Generic;
using System.Threading;

using Heirloom.IO;
using Heirloom.Sound;

namespace Examples.MusicPlayer
{
    internal class Program
    {
        private static readonly ConsoleColor[] _funkColors = { ConsoleColor.Magenta, ConsoleColor.Green, ConsoleColor.Cyan };

        private static void Main(string[] args)
        {
            /**
             * Licenses for Embedded MP3 Files:
             * 
             * Music from https://filmmusic.io
             * "Easy Lemon" by Kevin MacLeod (https://incompetech.com)
             * "Funk Game Loop" by Kevin MacLeod (https://incompetech.com)
             * "Too Cool" by Kevin MacLeod (https://incompetech.com)
             * License: CC BY(http://creativecommons.org/licenses/by/4.0/)
             */

            //
            var songData = new[] {
                new SongInfo("Easy Lemon", "files/easy-lemon-by-kevin-macleod.mp3"),
                new SongInfo("Funk Game Loop", "files/funk-game-loop-by-kevin-macleod.mp3"),
                new SongInfo("Too Cool", "files/too-cool-by-kevin-macleod.mp3")
            };

            // Construc song lookup
            var songLookup = new Dictionary<string, SongInfo>();
            foreach (var info in songData) { songLookup.Add(CreateIdentifier(info.Name), info); }

            // Add "using Heirloom.Sound.Effects;" above and uncomment
            // lines below to listen to different effects and filters.
            // AudioMixer.Master.Effects.Add(new HighPassFilter(2000));
            // AudioMixer.Master.Effects.Add(new LowPassFilter(80));
            // AudioMixer.Master.Effects.Add(new DelayEffect(0.8F));

            AudioSource currentSource = null;

            while (true)
            {
                DrawSongMenu();

                // Ask for selection
                Console.Write("Enter song name (or 'exit'): ");
                var choice = CreateIdentifier(Console.ReadLine());

                // Was the choice valid?
                if (songLookup.ContainsKey(choice))
                {
                    var info = songLookup[choice];

                    // Stop previous source
                    currentSource?.Stop();

                    // Create new source and play
                    currentSource = new AudioSource(Files.OpenStream(info.Path)) { Looping = true };
                    currentSource.Play();

                    var funkIndex = 0;
                    while (!Console.KeyAvailable)
                    {
                        Console.CursorVisible = false;
                        Console.Clear();

                        // Draw song name
                        Console.ForegroundColor = _funkColors[funkIndex % _funkColors.Length];
                        var nowPlaying = $"Now Playing \"{info.Name}\" by Kevin MacLeod";
                        Console.WriteLine($"\n  {nowPlaying}");

                        // Draw progress bar
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var prog = (int) (currentSource.Time / currentSource.Duration * (nowPlaying.Length - 2));
                        Console.Write($"  [");
                        for (var i = 0; i < (nowPlaying.Length - 2); i++) { Console.Write(i < prog ? '-' : ' '); }
                        Console.WriteLine($"]");

                        // Draw menu hint
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"\n  Press any key to show menu");

                        // 
                        Thread.Sleep(200);
                        funkIndex++;
                    }

                    // eat "key press to show menu" above
                    Console.ReadKey(true);

                    // Reset state for menu
                    Console.CursorVisible = true;
                    Console.ResetColor();
                    Console.Clear();
                }
                else
                {
                    // Exit commands (
                    if (choice == "exit") { break; }
                    else if (choice == "quit") { break; }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Unknown choice.\n");
                    }
                }
            }

            void DrawSongMenu()
            {
                Console.ResetColor();
                Console.WriteLine("Song Choices\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var info in songData)
                {
                    Console.WriteLine($"  {info.Name}");
                }

                Console.ResetColor();
                Console.WriteLine();
            }

            static string CreateIdentifier(string str)
            {
                str = str.Trim().ToLower();
                str = str.Replace(" ", "");
                return str;
            }
        }

        private struct SongInfo
        {
            public string Name;
            public string Path;

            public SongInfo(string name, string path)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Path = path ?? throw new ArgumentNullException(nameof(path));
            }
        }
    }
}
