using System;

using Heirloom.Drawing;

namespace Examples.Gridcannon
{
    public readonly struct CardInfo
    {
        public readonly int Index;

        public readonly Suit Suit;

        public readonly SuitColor SuitColor;

        public readonly int Value;

        #region Constructors

        public CardInfo(int index)
        {
            Index = index;
            Value = GetValue(index);
            Suit = GetSuit(index);

            SuitColor = GetColor(Suit);
        }

        public CardInfo(Suit suit, int value)
        {
            Index = GetIndex(suit, value);
            Value = value;
            Suit = suit;

            SuitColor = GetColor(Suit);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if this card is a royal (jack, queen or king).
        /// </summary>
        public bool IsRoyal => Value > 10;

        /// <summary>
        /// Gets the associated image (static lookup from <see cref="Assets"/>).
        /// </summary>
        public Image Image => Assets.GfxCards[Index];

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

        public static SuitColor GetColor(Suit suit)
        {
            if (suit == Suit.Joker) { return SuitColor.Any; }
            else
            {
                if (suit == Suit.Diamonds || suit == Suit.Hearts) { return SuitColor.Red; }
                else { return SuitColor.Black; }
            }
        }

        #endregion

        #region Conversion Operators

        public static explicit operator int(CardInfo card)
        {
            return card.Index;
        }

        public static implicit operator CardInfo(int index)
        {
            return new CardInfo(index);
        }

        #endregion

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
