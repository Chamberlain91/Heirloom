using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game
{
    public sealed class TextComponent : DrawableComponent
    {
        private string _text = string.Empty;
        private RichTextParser _parser;
        private RichText _richText;

        private bool _needParse = false;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get => _text;

            set
            {
                _needParse = true;
                _text = value;
            }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment.
        /// </summary>
        public TextAlign TextAlign { get; set; } = TextAlign.Left;

        /// <summary>
        /// Gets or sets the dired font.
        /// </summary>
        public Font Font { get; set; } = Font.Default;

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        public int FontSize { get; set; } = 16;

        /// <summary>
        /// Gets or sets the size of the text layout box. <para/>
        /// If set to <see cref="Size.Zero"/> then text is anchored around <see cref="Transform.Position"/> without word wrapping.
        /// </summary>
        public Size LayoutSize { get; set; } = Size.Zero;

        /// <summary>
        /// Gets or sets the rich text markup parser. Setting this to <c>null</c> will simply render as plain text.
        /// </summary>
        public RichTextParser RichTextParser
        {
            get => _parser;

            set
            {
                _needParse = true;
                _parser = value;
            }
        }

        protected override void Draw(RenderContext ctx)
        {
            DetectAndParseRichText();

            // Do we have a valid layout box?
            if (LayoutSize.Width > 0 && LayoutSize.Height > 0)
            {
                // Draw text in layout box
                if (_richText == null) { ctx.DrawText(_text, (Transform.Position, LayoutSize), Font, FontSize, TextAlign); }
                else { ctx.DrawText(_richText, (Transform.Position, LayoutSize), Font, FontSize, TextAlign); }
            }
            else
            {
                // Draw text anchored on position
                if (_richText == null) { ctx.DrawText(_text, Transform.Position, Font, FontSize, TextAlign); }
                else { ctx.DrawText(_richText, Transform.Position, Font, FontSize, TextAlign); }
            }
        }

        private void DetectAndParseRichText()
        {
            if (_needParse)
            {
                _richText = RichTextParser?.Parse(_text);
                _needParse = false;
            }
        }
    }
}

