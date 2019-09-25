using System.Collections.Generic;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Gridcannon
{
    internal class GridcannonGame : GameWindow
    // rules: https://www.pentadact.com/2019-08-20-gridcannon-a-single-player-game-with-regular-playing-cards/
    {
        public Card[] Cards;

        public Image[] CardImages;
        public Image CardBack;

        public GridcannonGame()
            : base("Gridcannon", multisample: MultisampleQuality.High)
        {
            ShowFPSOverlay = true;

            // == Load Graphics

            // Load card back
            CardBack = new Image(Files.ReadBytes("files/cardBack_green5.png"));

            // Load cards
            CardImages = new Image[53];
            for (var index = 0; index < 53; index++)
            {
                var card = (Card) index;
                var name = card.Suit == Suit.Joker ? "joker" : $"{card.Suit}{Card.GetValueName(card.Value)}";
                CardImages[index] = new Image(Files.ReadBytes($"files/card{name}.png"));
            }

            // 
            Cards = Card.GenerateDeck(true);
            Shuffle(Cards);
        }

        protected override void Update(float dt)
        {
            // 
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            ctx.Clear(Colors.FlatUI.WetAshphalt);

            var i = 0;
            foreach (var card in Cards)
            {
                ctx.DrawImage(GetCardImage(card), new Vector(10 + (i % 13) * 40, 10 + (i / 13) * 100));
                i++;
            }

            ctx.DrawImage(CardBack, new Vector(50, 50));
        }

        public static void Shuffle<T>(IList<T> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                var r = Calc.Random.Next(items.Count);
                Calc.Swap(items, i, r);
            }
        }

        public Image GetCardImage(Card card)
        {
            return CardImages[(int) card];
        }

        private static void Main(string[] _)
        {
            Application.Run(() => new GridcannonGame());
        }
    }
}
