using System;

using Heirloom.Drawing;

namespace Examples.Gridcannon
{
    public sealed class Card
    {
        private readonly int _imageIndex;

        #region Constructors

        public Card(Suit suit, int value)
        {
            _imageIndex = GetIndex(suit, value);

            Value = value;
            Suit = suit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated image (static lookup from <see cref="Assets"/>).
        /// </summary>
        public Image Image => Assets.GfxCards[_imageIndex];

        /// <summary>
        /// Gets this cards suit.
        /// </summary>
        public Suit Suit { get; }

        /// <summary>
        /// Gets thus cards value (Joker, Ace... King is mapped to 0, 1 ... 13).
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets if this card is a royal (jack, queen or king).
        /// </summary>
        public bool IsRoyal => Value > 10;

        #endregion

        #region Index to Suit/Value Conversions

        private static int GetIndex(Suit suit, int value)
        {
            if (suit == Suit.Joker) { return 0; }
            return value + (((int) suit) * 13);
        }

        private static Suit GetSuit(int index)
        {
            if (index == 0) { return Suit.Joker; }
            return (Suit) ((index - 1) / 13);
        }

        private static int GetValue(int index)
        {
            return 1 + ((index - 1) % 13);
        }

        #endregion

        /// <summary>
        /// Is the card given the same suit as this card?
        /// </summary>
        public bool IsSameSuit(Card card)
        {
            if (card is null) { throw new ArgumentNullException(nameof(card)); }
            return card.Suit == Suit;
        }

        /// <summary>
        /// Is the card given the same suit color as this card?
        /// </summary>
        public bool IsSameColor(Card card)
        {
            if (card is null) { throw new ArgumentNullException(nameof(card)); }
            return ((int) card.Suit % 2) == ((int) Suit % 2);
        }

        /// <summary>
        /// Gets the name of the card value (ie, 1 is 'A' or 11 is 'J')
        /// </summary>
        public static string GetValueName(int value)
        {
            // Standard 13
            if (value == 1) { return "A"; }
            if (value >= 2 && value <= 10) { return $"{value}"; }
            if (value == 11) { return "J"; }
            if (value == 12) { return "Q"; }
            if (value == 13) { return "K"; }

            // Joker
            if (value == 0) { return "?"; }

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public override string ToString()
        {
            if (Suit == Suit.Joker) { return "Joker"; }
            return $"{GetValueName(Value)} of {Suit}";
        }
    }
}
