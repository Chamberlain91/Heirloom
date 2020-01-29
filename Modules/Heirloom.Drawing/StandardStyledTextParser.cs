using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Provides implementation of a BBCode-esque text markup parser.
    /// </summary>
    public abstract class StandardStyledTextParser : StyledTextParser
    {
        private const char TagBegin = '[';
        private const char TagEnd = ']';
        private const char TagEndMarker = '/';

        private readonly Dictionary<string, DrawTextCallback> _callbacks;

        protected StandardStyledTextParser()
        {
            _callbacks = new Dictionary<string, DrawTextCallback>();
        }

        protected void AddKeyword(string keyword, DrawTextCallback callback)
        {
            _callbacks.Add(keyword, callback);
        }

        /// <summary>
        /// Parse the input text and returns a <see cref="StyledText"/> object.
        /// </summary>
        public override StyledText Parse(string markup)
        {
            if (string.IsNullOrWhiteSpace(markup)) { throw new ArgumentException("Must not be a blank or null string.", nameof(markup)); }

            var modeStartMap = new Dictionary<string, int>();
            var modes = new List<TextMode>();

            var cleanText = "";

            for (var i = 0; i < markup.Length; i++)
            {
                var c = markup[i];

                // Found start of tag
                if (c == TagBegin)
                {
                    // Move to next character (after '[')
                    c = markup[++i];

                    // Disallow whitespace immediately after opening
                    if (char.IsWhiteSpace(c)) { throw new FormatException($"Unable to parse standard rich text, invalid syntax."); }

                    // Determine if a closing tag (ie, '[/b]')
                    var isCloseTag = c == TagEndMarker;
                    if (isCloseTag) { i++; }

                    var modeName = "";

                    // Try to find more character to tag name
                    for (; i < markup.Length; i++)
                    {
                        c = markup[i];

                        // Is this the tag end?
                        if (c == TagEnd)
                        {
                            if (isCloseTag)
                            {
                                // Don't allow "[/b]*"
                                if (!modeStartMap.ContainsKey(modeName))
                                {
                                    throw new FormatException("Unable to parse standard rich text, closing tag without opening tag.");
                                }

                                // Record span
                                modes.Add(new TextMode
                                {
                                    Name = modeName,
                                    Range = new IntRange(modeStartMap[modeName], cleanText.Length - 1)
                                });

                                // Remove mode
                                modeStartMap.Remove(modeName);
                            }
                            else
                            {
                                // Don't allow "*[b][b]*"
                                if (modeStartMap.ContainsKey(modeName))
                                {
                                    throw new FormatException("Unable to parse standard rich text, duplicate opening tag before closing tag.");
                                }

                                // Record when this span begins
                                modeStartMap.Add(modeName, cleanText.Length);
                            }

                            break;
                        }
                        else
                        {
                            // Disallow whitespace within tag name
                            if (char.IsWhiteSpace(c)) { throw new FormatException($"Unable to parse standard rich text, invalid syntax."); }

                            // Tag was not complete
                            modeName += c;
                        }
                    }
                }
                else
                {
                    // Append to clean text
                    cleanText += c;
                }
            }

            return new StandardStyledText(this, cleanText, modes);
        }

        private struct TextMode
        {
            public IntRange Range;
            public string Name;
        }

        private sealed class StandardStyledText : StyledText
        {
            internal StandardStyledTextParser Processor;

            internal IEnumerable<TextMode> Modes;

            internal StandardStyledText(StandardStyledTextParser markupProcessor, string text, IEnumerable<TextMode> modes)
            {
                Processor = markupProcessor ?? throw new ArgumentNullException(nameof(markupProcessor));
                Modes = modes ?? throw new ArgumentNullException(nameof(modes));
                Text = text ?? throw new ArgumentNullException(nameof(text));
            }

            protected internal override DrawTextCallback Callback => OnStyleCharacter;

            public override string Text { get; }

            internal IEnumerable<string> GetKeywords(int index)
            {
                // todo: Possibly replace with more efficient method of index to keywords
                foreach (var mode in Modes)
                {
                    if (mode.Range.Contains(index))
                    {
                        yield return mode.Name;
                    }
                }
            }

            private void OnStyleCharacter(string text, int index, ref TextDrawState state)
            {
                foreach (var keyword in GetKeywords(index))
                {
                    var callback = Processor._callbacks[keyword];
                    callback(text, index, ref state);
                }
            }
        }
    }
}
