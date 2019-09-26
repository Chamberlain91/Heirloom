using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.Gridcannon
{
    /// <summary>
    /// Represents a single playing card.
    /// </summary>
    public readonly struct Card : IEquatable<Card>
    {
        public const int CardsPerSuit = 13;
        public const int NumberOfSuits = 4;

        /// <summary>
        /// The card number (1 through 13, 0 for Joker)
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// The card suit.
        /// </summary>
        public readonly Suit Suit;

        #region Constructors

        /// <summary>
        /// Constructs a new playing card.
        /// </summary>
        public Card(int value, Suit suit)
        {
            if (suit != Suit.Joker && (value <= 0 || value > CardsPerSuit)) { throw new ArgumentOutOfRangeException(nameof(value)); }
            if (suit == Suit.Joker && value != 0) { throw new ArgumentException($"Joker must have value of zero."); }

            Value = value;
            Suit = suit;
        }

        #endregion

        #region Equality

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

        public static bool operator ==(Card left, Card right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Card left, Card right)
        {
            return !(left == right);
        }

        #endregion

        #region Conversion Operators

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

        #endregion

        public override string ToString()
        {
            if (Suit == Suit.Joker) { return "Joker"; }
            return $"{GetValueName(Value)} of {Suit}";
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

        /// <summary>
        /// Gets the name of the card value (ie, 1 is 'A' or 11 is 'J')
        /// </summary>
        public static string GetValueName(int value)
        {
            // standard 13
            if (value == 1) { return "A"; }
            if (value >= 2 && value <= 10) { return $"{value}"; }
            if (value == 11) { return "J"; }
            if (value == 12) { return "Q"; }
            if (value == 13) { return "K"; }

            // joker
            if (value == 0) { return "0"; }

            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}
