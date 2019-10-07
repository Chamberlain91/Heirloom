using System;
using System.Collections.Generic;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game
{
    public sealed class TextComponent : DrawableComponent
    {
        private string _text = string.Empty;
        private RichTextParser _markup;

        // 
        private RichText _markupResult;

        private bool _needParseMarkup = false;

        public string Text
        {
            get => _text;

            set
            {
                _needParseMarkup = true;
                _text = value;
            }
        }

        public TextAlign Align { get; set; } = TextAlign.Left;

        public Font Font { get; set; } = Font.Default;

        public int FontSize { get; set; } = 16;

        /// <summary>
        /// The size of the text layout box, if set to <see cref="Size.Zero"/> then text is anchored around <see cref="Transform.Position"/>.
        /// </summary>
        public Size LayoutSize { get; set; } = Size.Zero;

        /// <summary>
        /// Gets or sets the text markup processor.
        /// </summary>
        public RichTextParser MarkupProcessor
        {
            get => _markup;

            set
            {
                _needParseMarkup = true;
                _markup = value;
            }
        }

        private bool HasLayoutBox => LayoutSize.Width > 0 && LayoutSize.Height > 0;

        protected override void Draw(RenderContext ctx)
        {
            if (_needParseMarkup)
            {
                _needParseMarkup = false;

                // If a markup processor was given, process text with it
                if (!MarkupProcessor?.TryParse(_text, out _markupResult) ?? true)
                {
                    // Failed to parse, just use plain text
                    _markupResult = new PlainTextMarkupResult(_text);
                }
            }

            // 
            if (HasLayoutBox)
            {
                // Draw text in layout box
                ctx.DrawText(_markupResult.Text, (Transform.Position, LayoutSize), Font, FontSize, Align, _markupResult.Callback);
            }
            else
            {
                // Draw text anchored on position
                ctx.DrawText(_markupResult.Text, Transform.Position, Font, FontSize, Align, _markupResult.Callback);
            }
        }

        private class PlainTextMarkupResult : RichText
        {
            public PlainTextMarkupResult(string text)
            {
                Text = text ?? throw new ArgumentNullException(nameof(text));
            }

            public override TextRenderer.DrawTextCallback Callback { get; }

            public override string Text { get; }
        }
    }

