using Heirloom.Desktop.Game;

namespace Examples.Gridcannon
{
    public class Card : Entity
    {
        private readonly ImageRenderer _renderer;
        private bool _isFaceDown = true;

        public readonly CardInfo CardInfo;

        public Card(CardInfo cardInfo)
        {
            CardInfo = cardInfo;

            // Create and attch renderer
            _renderer = new ImageRenderer(Assets.GfxCardBack);
            AttachComponent(_renderer);
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
                    _renderer.Image = IsFaceDown ? Assets.GfxCardBack : CardInfo.Image;
                }
            }
        }
    }
}
