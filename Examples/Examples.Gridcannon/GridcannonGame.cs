using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Game;
using Heirloom.Game.Desktop;
using Heirloom.Math;

using static Examples.Gridcannon.Assets;

namespace Examples.Gridcannon
{
    internal class GridcannonGame : GameWindow
    // rules: https://www.pentadact.com/2019-08-20-gridcannon-a-single-player-game-with-regular-playing-cards/
    {
        public static int Padding = 12;

        // 
        private readonly SceneManager _sceneManager;
        private readonly Scene _scene;

        //  
        private readonly CardStack _deck;
        private readonly CardStack[,] _grid;
        private readonly CardStack _pile;

        // 
        private static readonly IntVector[] _gridPositions = new IntVector[] {
            (0, 0), (1, 0), (2, 0),
            (0, 1), (1, 1), (2, 1),
            (0, 2), (1, 2), (2, 2),
        };

        public GridcannonGame()
            : base("Gridcannon")
        {
            ShowFPSOverlay = true;

            // == Load Assets

            LoadAssets();

            // == Size Window

            var mode = Monitor.Default.CurrentVideoMode;
            Size = new IntSize(Padding + (GfxCardBack.Width + Padding) * 5, (GfxCardBack.Height + Padding) * 6);
            Position = new IntVector(mode.Width - Size.Width, mode.Height - Size.Height) / 2;
            IsResizable = false;

            // == Initialize Game State

            // Track input from the window w/ basic mapping
            Input.AddInputSource(new StandardDesktopInput(this));
            _sceneManager = new SceneManager();
            _sceneManager.Add(_scene = new Scene());

            // Create card stacks 
            _scene.Add(_deck = new CardStack());
            _deck.Transform.Position = GetCardPosition(0, 0);

            _scene.Add(_pile = new CardStack());
            _pile.Transform.Position = GetCardPosition(4, 0);

            // Create grid
            _grid = new CardStack[3, 3];
            for (var y = 0; y < 3; y++)
            {
                for (var x = 0; x < 3; x++)
                {
                    var stack = _grid[x, y] = new CardStack();
                    stack.Transform.Position = GetCardPosition(1 + x, 2 + y);
                    _scene.Add(stack);
                }
            }

            // Create standard deck and put into deck stack
            foreach (var card in CreateStandardDeck())
            {
                // Position cards onto deck
                card.Transform.Position = _deck.Transform.Position;
                card.IsFaceDown = true;

                // Add to game deck
                _deck.AddTopCard(card);
                _scene.Add(card);
            }

            // == Setup game field
            var royalCards = new Queue<Card>();
            var gridCards = new Queue<Card>();

            // Draw cards, sorting grid and royals
            while (gridCards.Count < 8)
            {
                // Draw a card, face up
                var card = _deck.DrawTopCard();
                card.IsFaceDown = false;

                // Sort drawn cards into royals and the grid cards
                if (card.CardInfo.IsRoyal) { royalCards.Enqueue(card); }
                else { gridCards.Enqueue(card); }
            }

            // Put cards onto grid
            foreach (var (xi, yi) in _gridPositions)
            {
                // Skip center
                if (xi == 1 && yi == 1) { continue; }

                // Place card into grid
                var card = gridCards.Dequeue();
                _grid[xi, yi].AddTopCard(card);

                var delay = (xi + (yi * 3)) / 9F;
                card.StartCoroutine(delay, CoAnimateToPosition(card, _grid[xi, yi].Transform.Position));
            }

            //for (var i = 0; i < royalCards.Count; i++)
            //{
            //    var card = royalCards[i];

            //    var w = GfxCardBack.Width;
            //    var h = GfxCardBack.Height;

            //    var x = Padding + w / 2 + (1) * (w + Padding);
            //    var y = Padding + h / 2 + (1) * (h + Padding);
            //    _scene.StartCoroutine(CoAnimateCard(card, (x, y)));
            //}

            // todo: properly do game...
            var firstCard = _deck.DrawTopCard();
            firstCard.IsFaceDown = false;
            firstCard.StartCoroutine(1.5F, CoAnimateToPosition(firstCard, GetCardPosition(1, 0)));

            // 
            _deck.ReorderEntities();
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
                // Generate standard 52 cards
                for (var i = 1; i <= 52; i++)
                {
                    yield return new Card(i);
                }

                // Supplement 2 jokers
                yield return new Card(0);
                yield return new Card(0);
            }
        }

        private static Vector GetCardPosition(int xi, int yi)
        {
            var w = GfxCardBack.Width;
            var h = GfxCardBack.Height;

            var x = Padding + (w / 2) + (xi * (w + Padding));
            var y = Padding + (h / 2) + (yi * (h + Padding));

            return new Vector(x, y);
        }

        private IEnumerator CoAnimateToPosition(Card card, Vector target)
        {
            var timer = Timer.StartNew(0.8F);
            var start = card.Transform.Position;

            while (timer.Remaining > 0)
            {
                var t = timer.Elapsed / timer.Duration;
                t = Calc.SmootherStep(0, 1, t);

                card.Transform.Position = Vector.Lerp(start, target, t);
                yield return Coroutine.WaitNextFrame();
            }

            // Snap to final position
            card.Transform.Position = target;
        }

        protected override void Update(RenderContext ctx, float dt)
        {
            _sceneManager.Update(dt, ctx);
        }

        private static void Main(string[] _)
        {
            Application.Run<GridcannonGame>();
        }
    }
}
