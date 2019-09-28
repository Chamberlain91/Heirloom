using System;
using System.Collections.Generic;

using Heirloom.Desktop.Game;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Gridcannon
{
    public class CardStack : Entity
    {
        private readonly LinkedList<Card> _cards;

        public CardStack()
        {
            _cards = new LinkedList<Card>();

            // When the transform changes, update the bounds
            Transform.Changed += UpdateBounds;
        }

        public int Count => _cards.Count;

        public bool IsEmpty => Count == 0;

        public Rectangle Bounds { get; private set; }

        private void UpdateBounds()
        {
            // Compute bounds from image
            var bounds = Assets.GfxCardBack.Bounds;
            bounds.Position += Transform.Position;
            Bounds = bounds;
        }

        public Card DrawTopCard()
        {
            if (!IsEmpty)
            {
                var card = _cards.First.Value;

                _cards.RemoveFirst();
                return card;
            }
            else
            {
                throw new InvalidOperationException("Card stack was empty");
            }
        }

        public void ReorderEntities()
        {
            var depth = _cards.Count - 1;
            foreach (var card in _cards)
            {
                card.Depth = depth--;
            }
        }

        public void AddTopCard(Card card)
        {
            _cards.AddFirst(card);
        }

        public void AddBottomCard(Card card)
        {
            _cards.AddLast(card);
        }

        protected override void Draw(RenderContext ctx)
        {
            if (IsEmpty)
            {
                ctx.Color = Colors.FlatUI.BelizeHole;
                ctx.DrawRectOutline(Bounds.Inflate(4), 2);
            }
        }

        protected override void DrawDebug(RenderContext ctx)
        {
            ctx.Color = Colors.FlatUI.Emerald;
            ctx.DrawRectOutline(Bounds, 2);
        }
    }
}
