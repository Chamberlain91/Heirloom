using System.Collections.Generic;
using System.IO;
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

        // 
        public Image[] CardImages;
        public Image CardBack;

        // 
        public Card[] Cards;

        // 
        private Vector _cardPosition;

        // 
        private Vector _mousePosition;
        private Vector _mouseDelta;
        private bool _mouseDown;

        public GridcannonGame()
            : base("Gridcannon")
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
                CardImages[index] = new Image(Files.ReadBytes($"files/{$"card{name}.png"}"));
            }

            // == Size Window

            Size = new IntSize((CardBack.Width + Padding) * 5, (CardBack.Height + Padding) * 6);

            // == Initialize Game State

            Cards = Card.GenerateDeck(true);
            Shuffle(Cards);
        }

        protected override void OnMousePressed(int button, ButtonAction action, KeyModifiers modifiers)
        {
            if (button == 0)
            {
                if (action == ButtonAction.Press) { _mouseDown = true; }
                if (action == ButtonAction.Release) { _mouseDown = false; }
            }
        }

        protected override void OnMouseMove(float x, float y)
        {
            var mousePosition = new Vector(x, y);
            _mouseDelta += mousePosition - _mousePosition;
            _mousePosition = mousePosition;
        }

        protected override void Update(float dt)
        {
            // 
        }

        protected override void Draw(RenderContext ctx, float dt)
        {
            ctx.Clear(Colors.FlatUI.WetAshphalt);

            var paddingOffset = Padding / 2;

            // 
            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    var image = GetCardImage((Card) (y * 5 + x));
                    ctx.DrawImage(image, (paddingOffset + (x * (image.Width + Padding)), paddingOffset + ((y + 1) * (image.Height + Padding))));
                }
            }

            ctx.DrawImage(CardBack, _cardPosition);

            // == Clear accumulated mouse drag

            if (_mouseDown) { _cardPosition += _mouseDelta; }
            _mouseDelta.Set(0, 0);
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
