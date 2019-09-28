using System;
using System.Collections;
using System.Collections.Generic;

using Examples.Gridcannon.Engine;

using Heirloom.Collections;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.IO;
using Heirloom.Math;

namespace Examples.Gridcannon
{
    internal class GridcannonGame : GameWindow
    // rules: https://www.pentadact.com/2019-08-20-gridcannon-a-single-player-game-with-regular-playing-cards/
    {
        public static int Padding = 12;

        private Vector _latestMousePosition;

        // 
        public Image[] CardImages;
        public Image CardBack;

        // 
        public Scene Scene;

        private List<CardEntity> _cards;

        public GridcannonGame()
            : base("Gridcannon")
        {
            ShowFPSOverlay = true;

            // == Load Graphics

            // Load card back
            CardBack = new Image(Files.ReadBytes("files/cardBack_green5.png"));
            CardBack.Origin = CardBack.Bounds.Center;

            // Load cards
            CardImages = new Image[53];
            for (var index = 0; index < 53; index++)
            {
                var card = (Card) index;
                var name = card.Suit == Suit.Joker ? "joker" : $"{card.Suit}{Card.GetValueName(card.Value)}";

                // Load card image, set origin to center
                CardImages[index] = new Image(Files.ReadBytes($"files/{$"card{name}.png"}"));
                CardImages[index].Origin = CardImages[index].Bounds.Center;
            }

            // == Size Window

            var mode = Monitor.Default.CurrentVideoMode;
            Size = new IntSize((CardBack.Width + Padding) * 5, (CardBack.Height + Padding) * 6);
            Position = new IntVector(mode.Width - Size.Width, mode.Height - Size.Height) / 2;
            IsResizable = false;

            // == Initialize Game State

            Scene = new Scene();

            // Create card entities
            _cards = new List<CardEntity>();
            foreach (var card in Card.GenerateDeck(true))
            {
                var entity = new CardEntity(card, GetCardImage(card), CardBack);
                entity.Transform.Position = new Vector(Padding + CardBack.Width / 2, Padding + CardBack.Height / 2);
                _cards.Add(entity);
            }

            // Shuffle cards
            _cards.Shuffle(Calc.Random);

            var i = 0;
            foreach (var card in _cards)
            {
                var w = CardBack.Width;
                var h = CardBack.Height;

                var xi = i % 3;
                var yi = i / 3;

                var x = Padding + w / 2 + (xi + 1) * (w + Padding);
                var y = Padding + h / 2 + (yi + 2) * (h + Padding);
                Scene.StartCoroutine(CoAnimateCard(card, (x, y)));

                // 
                if (++i >= 9) { break; }
            }

            // Insert cards into scene
            Scene.Add(_cards);
        }

        private IEnumerator CoAnimateCard(CardEntity card, Vector target)
        {
            var timer = Timer.StartNew(Calc.Random.NextFloat(0.8F, 1.2F));
            var start = card.Transform.Position;

            while (timer.Remaining > 0)
            {
                var t = timer.Elapsed / timer.Duration;
                t = Calc.SmootherStep(0, 1, t);

                card.Transform.Position = Vector.Lerp(start, target, t);
                yield return Coroutine.WaitNextFrame();
            }

            // 
            yield return CoWaitRandomTime(0.2F, 0.3F);
            card.Flip();

            // Snap to final position
            card.Transform.Position = target;
        }

        private IEnumerator CoWaitRandomTime(float min, float max)
        {
            var seconds = Calc.Random.NextFloat(min, max);
            yield return Coroutine.WaitSeconds(seconds);
        }

        protected override void OnMousePressed(int button, ButtonAction action, KeyModifiers modifiers)
        {
            // Inform the scene of a click event
            Scene.NotifyMouseClick(button, action == ButtonAction.Press, _latestMousePosition);
        }

        protected override void OnMouseMove(float x, float y)
        {
            // Compute delta position
            var mousePosition = new Vector(x, y);
            var deltaPosition = mousePosition - _latestMousePosition;
            _latestMousePosition = mousePosition;

            // Inform the scene of a mouse motion event
            Scene.NotifyMouseMove(_latestMousePosition, deltaPosition);
        }

        protected override void Update(RenderContext ctx, float dt)
        {
            // ctx.DrawImage(CardBack, _cardPosition);
            Scene.Update(ctx, dt);
        }

        public Image GetCardImage(Card card)
        {
            return CardImages[(int) card];
        }

        private static void Main(string[] _)
        {
            Application.Run<GridcannonGame>();
        }

        public class CardEntity : Draggable
        {
            public bool IsFaceDown = true;

            private readonly Image _cardImage;
            private readonly Image _backImage;

            public CardEntity(Card card, Image cardImage, Image backImage)
                : base(backImage)
            {
                _cardImage = cardImage;
                _backImage = backImage;

                Card = card;
            }

            public Card Card { get; }

            internal override bool OnMouseClick(int button, bool isDown, Vector position)
            {
                if (button == 1 && isDown)
                {
                    if (Bounds.Contains(position))
                    {
                        Flip();
                        return true;
                    }
                }

                return base.OnMouseClick(button, isDown, position);
            }

            public void Flip()
            {
                // Toggle if the card is facing down
                IsFaceDown = !IsFaceDown;
                if (IsFaceDown) { Image = _backImage; }
                else { Image = _cardImage; }
            }
        }
    }
}
