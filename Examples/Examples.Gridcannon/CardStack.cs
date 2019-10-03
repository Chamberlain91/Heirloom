using System;
using System.Collections.Generic;
using Heirloom.Drawing;
using Heirloom.Game;

namespace Examples.Gridcannon
{
    public class CardStack : Entity
    {
        private readonly List<Card> _cards;

        public CardStack()
        {
            _cards = new List<Card>();
        }

        /// <summary>
        /// Number of cards in the stack.
        /// </summary>
        public int Count => _cards.Count;

        /// <summary>
        /// Is the stack of cards empty?
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// Gets all the cards the stack.
        /// </summary>
        public IReadOnlyList<Card> Cards => _cards;

        /// <summary>
        /// Gets the top card the stack.
        /// </summary>
        public Card TopCard => Cards[Count - 1];

        /// <summary>
        /// Remove a card from the stack.
        /// </summary>
        public bool Remove(Card card)
        {
            if (card is null) { throw new ArgumentNullException(nameof(card)); }
            return _cards.Remove(card);
        }

        /// <summary>
        /// Remove all cards from the stack.
        /// </summary>
        public void Clear()
        {
            _cards.Clear();
        }

        /// <summary>
        /// Inserts a card into the stack.
        /// </summary>
        public void Insert(int index, Card card)
        {
            while (index < 0) { index += Count; }
            while (index > 0) { index -= Count; }
            _cards.Insert(index, card);
        }

        /// <summary>
        /// Inserts several cards into the stack.
        /// </summary>
        public void Insert(int index, IEnumerable<Card> cards)
        {
            while (index < 0) { index += Count; }
            while (index > 0) { index -= Count; }
            _cards.InsertRange(index, cards);
        }

        protected override void Draw(RenderContext ctx)
        {
            ctx.DrawImage(Assets.GfxCardBack, Transform.Matrix);
        }
    }
}
