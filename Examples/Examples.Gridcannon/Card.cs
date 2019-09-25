using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.Gridcannon
{
    public readonly struct Card : IEquatable<Card>
    {
        public const int CardsPerSuit = 13;
        public const int NumberOfSuits = 4;

        public readonly int Value;

        public readonly Suit Suit;

        public Card(int value, Suit suit)
        {
            if (suit != Suit.Joker && (value <= 0 || value > CardsPerSuit)) { throw new ArgumentOutOfRangeException(nameof(value)); }
            if (suit == Suit.Joker && value != 0) { throw new ArgumentException($"Joker must have value of zero."); }

            Value = value;
            Suit = suit;
        }

        public bool Equals(Card other)
        {
            return Suit == other.Suit
                && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Card card ? Equals(card) : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Suit);
        }

        public override string ToString()
        {
            if (Suit == Suit.Joker) { return "Joker"; }
            return $"{GetValueName(Value)} of {Suit}";
        }

        public static bool operator ==(Card left, Card right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Card left, Card right)
        {
            return !(left == right);
        }

        public static IEnumerable<Suit> Suits
        {
            get
            {
                yield return Suit.Clubs;
                yield return Suit.Diamonds;
                yield return Suit.Hearts;
                yield return Suit.Spades;
            }
        }

        public static explicit operator int(Card card)
        {
            if (card.Suit == Suit.Joker) { return 0; }
            return card.Value + (((int) card.Suit) * CardsPerSuit);
        }

        public static explicit operator Card(int index)
        {
            if (index == 0) { return new Card(0, Suit.Joker); }

            var value = 1 + ((index - 1) % CardsPerSuit);
            var suit = (Suit) ((index - 1) / CardsPerSuit);

            return new Card(value, suit);
        }

        /// <summary>
        /// Generates a deck of cards, with optional jokers.
        /// </summary>
        public static Card[] GenerateDeck(bool includeJokers = true)
        {
            var cards = new List<Card>();

            // Generate standard 52 cards
            foreach (var index in Enumerable.Range(0, CardsPerSuit * NumberOfSuits))
            {
                var card = (Card) (index + 1);
                cards.Add(card);
            }

            if (includeJokers)
            {
                // Supplement 2 jokers
                cards.Add(new Card(0, Suit.Joker));
                cards.Add(new Card(0, Suit.Joker));
            }

            return cards.ToArray();
        }

        public static string GetValueName(int value)
        {
            if (value == 1) { return "A"; }
            if (value >= 2 && value <= 10) { return $"{value}"; }
            if (value == 11) { return "J"; }
            if (value == 12) { return "Q"; }
            if (value == CardsPerSuit) { return "K"; }

            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}
