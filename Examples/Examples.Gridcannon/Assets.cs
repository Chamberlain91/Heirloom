using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.IO;

namespace Examples.Gridcannon
{
    public static class Assets
    {
        public static Image GfxCardBack { get; private set; }

        public static IReadOnlyList<Image> GfxCards { get; private set; }

        public static bool AreAssetsLoaded { get; private set; }

        public static void LoadAssets()
        {
            if (!AreAssetsLoaded)
            {
                AreAssetsLoaded = true;

                // 
                LoadGraphics();
                LoadSounds();
            }
        }

        private static void LoadGraphics()
        {
            GfxCardBack = LoadCard("back");
            GfxCards = LoadCards();

            // loads a single card image
            Image LoadCard(string name)
            {
                var image = new Image(Files.ReadBytes($"files/card{name}.png"));
                image.Origin = image.Bounds.Center;
                return image;
            }

            // loads the entire deck
            List<Image> LoadCards()
            {
                // Load cards, starting with the joker and then the remaining 52 images
                var cards = new List<Image> { LoadCard("joker") };
                cards.AddRange(LoadSuit(Suit.Clubs));
                cards.AddRange(LoadSuit(Suit.Diamonds));
                cards.AddRange(LoadSuit(Suit.Hearts));
                cards.AddRange(LoadSuit(Suit.Spades));
                return cards;

                // loads the 13 cards of a suit
                IEnumerable<Image> LoadSuit(Suit suit)
                {
                    for (var val = 1; val <= 13; val++)
                    {
                        yield return LoadCard($"{suit}{CardInfo.GetValueName(val)}");
                    }
                }
            }
        }

        private static void LoadSounds()
        {
            // Nothing Yet
        }
    }
}
