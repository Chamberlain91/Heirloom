using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Desktop;
using Heirloom.Game.Desktop;
using Heirloom.Math;

using static Examples.Gridcannon.Assets;

namespace Examples.Gridcannon
{
    public sealed class GridcannonGame : DesktopGameContext
    // rules: https://www.pentadact.com/2019-08-20-gridcannon-a-single-player-game-with-regular-playing-cards/
    {
        public static int Padding = 12;

        private const int JACK = 11;
        private const int QUEEN = 12;
        private const int KING = 13;

        public CardStack[] Royals; // Wound clockwise from top left 
        public CardStack[] Grid;   // Raster order from top left

        public CardStack ShamePile;
        public CardStack Deck;

        public static readonly IntVector[] RoyalOffsets = new IntVector[12]
        {
            (1, 0), (2, 0), (3, 0),
            (4, 1), (4, 2), (4, 3),
            (3, 4), (2, 4), (1, 4),
            (0, 3), (0, 2), (0, 1),
        };

        public static readonly IntVector[] GridOffsets = new IntVector[9]
        {
            (1, 1), (2, 1), (3, 1),
            (1, 2), (2, 2), (3, 2),
            (1, 3), (2, 3), (3, 3),
        };

        public GridcannonGame()
            : base("Gridcannon")
        {
            RenderContext.ShowFPSOverlay = true;

            // == Load Assets

            LoadAssets();

            // == Size Window

            Window.Size = new IntSize(Padding + (GfxCardBack.Width + Padding) * 5, (GfxCardBack.Height + Padding) * 6);
            Window.IsResizable = false;
            Window.MoveToCenter();

            // == Initialize Game State 

            Grid = CreateStacks(9);
            Royals = CreateStacks(12);
            ShamePile = new CardStack();
            Deck = new CardStack();

            // Populate deck (inserts at bottom technically)
            Deck.Insert(0, CreateStandardDeck(true));

            // Compute a scale to position cards
            var scale = (IntVector) (GfxCardBack.Size + (12, 12));

            // Postion Stacks
            for (var i = 0; i < RoyalOffsets.Length; i++)
            {
                //Royals[i].Transform.Position = RoyalOffsets[i] * scale;
            }
        }

        public Card CurrentCard => !Deck.IsEmpty ? Deck.TopCard : null;

        private static CardStack[] CreateStacks(int n)
        {
            var stacks = new CardStack[n];

            for (var i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new CardStack();
            }

            return stacks;
        }

        public static IEnumerable<Card> CreateStandardDeck(bool shuffle = true)
        {
            if (shuffle)
            {
                // Collect cards into an array and shuffle first
                var cards = EmitCards().ToArray();
                cards.Shuffle(Calc.Random);
                return cards;
            }
            else
            {
                // Emit generator directly
                return EmitCards();
            }

            IEnumerable<Card> EmitCards()
            {
                // For each suit
                foreach (var suit in (Suit[]) Enum.GetValues(typeof(Suit)))
                {
                    // For each card value
                    for (var i = 0; i < 13; i++)
                    {
                        yield return new Card(suit, 1 + i);
                    }
                }

                // Supplement 2 jokers
                yield return new Card(Suit.Joker, 0);
                yield return new Card(Suit.Joker, 0);
            }
        }

        public IEnumerable<Payload> GetPayloads(int position)
        {
            foreach (var (a, b, r) in GetPayloadIndices(position))
            {
                yield return new Payload(Grid[a], Grid[b], Royals[r]);
            }
        }

        public static IEnumerable<(int a, int b, int r)> GetPayloadIndices(int position)
        {
            switch (position)
            {
                case 0:
                    yield return (1, 2, 3); // East
                    yield return (3, 6, 8); // South
                    break;

                case 1:
                    yield return (4, 7, 7); // South
                    break;

                case 2:
                    yield return (2, 1, 11); // West
                    yield return (5, 8, 6);  // South
                    break;

                case 3:
                    yield return (4, 5, 4);  // East
                    break;

                case 4:
                    // Nothing, but here because center is playable
                    yield break;

                case 5:
                    yield return (3, 4, 10);  // West
                    break;

                case 6:
                    yield return (3, 0, 0);  // North
                    yield return (7, 8, 5);  // East
                    break;

                case 7:
                    yield return (4, 1, 1);  // North
                    break;

                case 8:
                    yield return (5, 2, 2);  // North
                    yield return (8, 7, 9);  // West
                    break;

                default:
                    // Error: Position must be one of the 9 grid indices
                    throw new ArgumentOutOfRangeException(nameof(position));
            }
        }

        public static int SumCards(IEnumerable<Card> cards)
        {
            var sum = 0;
            foreach (var card in cards)
            {
                // Joker cards have zero value in Gridcannon
                if (card.Suit == Suit.Joker) { continue; }
                else
                {
                    // The remaining cards have a value
                    // Ace cards are valued as a 1
                    sum += card.Value;
                }
            }

            return sum;
        }

        public static int GetAttackPower(Card card, Card royal)
        {
            if (royal.Value == JACK
            || (royal.Value == QUEEN && card.IsSameColor(royal))
            || (royal.Value == KING && card.IsSameSuit(royal)))
            {
                // Card was effective against royal
                return card.Value;
            }

            // Not card effective
            return 0;
        }

        public struct Payload
        {
            public CardStack R;
            public CardStack A;
            public CardStack B;

            public bool IsValid => !R.IsEmpty;

            public Payload(CardStack a, CardStack b, CardStack royal)
            {
                A = a ?? throw new ArgumentNullException(nameof(a));
                B = b ?? throw new ArgumentNullException(nameof(b));
                R = royal ?? throw new ArgumentNullException(nameof(royal));
            }

            public bool WillDefeatRoyal()
            {
                // Get Royal Defense
                var defense = SumCards(R.Cards);

                // Get Attack Power
                var attack = 0;
                if (!A.IsEmpty) { attack += GetAttackPower(A.TopCard, R.TopCard); }
                if (!B.IsEmpty) { attack += GetAttackPower(B.TopCard, R.TopCard); }

                return attack >= defense;
            }
        }

        private static void Main(string[] _)
        {
            Application.Run(() =>
            {
                // Begin game
                var game = new GridcannonGame();
                game.Start();
            });
        }
    }
}
