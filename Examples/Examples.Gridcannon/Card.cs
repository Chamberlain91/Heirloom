using Heirloom.Game;

namespace Examples.Gridcannon
{
    public class Card : Entity
    {
        private readonly ImageRenderer _renderer;
        private bool _isFaceDown = true;

        public readonly CardInfo Info;

        public Card(CardInfo cardInfo)
        {
            Info = cardInfo;

            // Create and attch renderer
            _renderer = new ImageRenderer(Assets.GfxCardBack);
            AddComponent(_renderer);
        }

        /// <summary>
        /// Is this card facing down?
        /// </summary>
        public bool IsFaceDown
        {
            get => _isFaceDown;

            set
            {
                if (_isFaceDown != value)
                {
                    _isFaceDown = value;
                    _renderer.Image = IsFaceDown ? Assets.GfxCardBack : Info.Image;
                }
            }
        }
    }
}
