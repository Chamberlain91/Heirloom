using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

using Heirloom.IO;
using Heirloom.Text;

using static StbTrueTypeSharp.StbTrueType;

namespace Heirloom.Drawing
{
    /// <summary>
    /// An object to represent a truetype font.
    /// Provides functionality to query and measure aspects of the font.
    /// </summary>
    /// <category>Text</category>
    public unsafe class TrueTypeFont : Font
    {
        internal stbtt_fontinfo Info;
        private readonly byte* _inMemoryFile; // Don't need technically if using StbSharp
        private bool _isDisposed = false;

        private readonly Dictionary<UnicodeCharacter, TrueTypeGlyph> _glyphLookup;
        private readonly TrueTypeGlyph[] _glyphs;

        private readonly int _ascent;
        private readonly int _descent;
        private readonly int _lineGap;

        #region Constructors

        /// <summary>
        /// Loads a font specified by path resolved by <see cref="Files.OpenStream(string)"/>.
        /// </summary>
        public TrueTypeFont(string path)
            : this(Files.OpenStream(path))
        { }

        /// <summary>
        /// Loads a font from a stream.
        /// </summary>
        public TrueTypeFont(Stream stream)
            : this(ReadAllBytes(stream))
        { }

        /// <summary>
        /// Loads a font from a block of bytes.
        /// </summary>
        public TrueTypeFont(byte[] file)
        {
            // Keep file around in memory, since the native points directly to it.
            // We will clone the data using unmanaged memory (🙊)
            _inMemoryFile = CloneUnmanaged(file);

            Info = new stbtt_fontinfo();

            // Initialize the font, populating the stb info structure
            if (stbtt_InitFont(Info, _inMemoryFile, 0) == 0)
            {
                throw new InvalidOperationException("Unable to load font");
            }

            // Extract vertical metrics (at raw scale)
            int ascent, descent, lineGap;
            stbtt_GetFontVMetrics(Info, &ascent, &descent, &lineGap);

            // Store raw face metrics
            _ascent = ascent;
            _descent = descent;
            _lineGap = lineGap;

            // 
            _glyphLookup = new Dictionary<UnicodeCharacter, TrueTypeGlyph>();
            _glyphs = new TrueTypeGlyph[Info.numGlyphs];
        }

        /// <summary>
        /// Performs cleanup of unmanaged resources before garbage collection.
        /// </summary>
        ~TrueTypeFont()
        {
            Dispose(false);
        }

        #endregion

        /// <inheritdoc/>
        public override FontMetrics GetMetrics(float size)
        {
            if (size < 1) { throw new ArgumentException("Font size must be greater than zero.", nameof(size)); }

            // Compute scaling factor
            var scale = ComputeScale(size);

            // Compute metrics
            var ascent = _ascent * scale;
            var descent = _descent * scale;
            var lineGap = _lineGap * scale;

            // Return metrics to user
            return new FontMetrics(ascent, descent, lineGap);
        }

        /// <inheritdoc/>
        public override float GetKerning(UnicodeCharacter cp1, UnicodeCharacter cp2, float size)
        {
            // Compute scaling factor
            var scale = ComputeScale(size);

            // Get the pair of glyphs
            var g1 = GetGlyph(cp1) as TrueTypeGlyph;
            var g2 = GetGlyph(cp2) as TrueTypeGlyph;

            // Get kerning advance between this glyph an another
            return stbtt_GetGlyphKernAdvance(Info, g1.Index, g2.Index) * scale;
        }

        internal float ComputeScale(float height)
        {
            return stbtt_ScaleForMappingEmToPixels(Info, height);
        }

        #region Get Glyph

        /// <inheritdoc/>
        public override Glyph GetGlyph(UnicodeCharacter ch)
        {
            // Attempt to lookup glyph from dictionary
            if (!_glyphLookup.TryGetValue(ch, out var glyph))
            {
                // Get the index for the specified codepoint
                var index = stbtt_FindGlyphIndex(Info, (int) ch);

                // Gets the glyph by its index
                glyph = GetGlyphByIndex(index);

                // Glyph doesn't know its codepoint, so we will set it here since we know now.
                // This is because stb doesn't have a way for mapping index -> codpoint (yet?)
                if (!glyph.HasCodepoint)
                {
                    glyph.SetCodepoint(ch);
                }

                // Store glyph by codepoint
                _glyphLookup[ch] = glyph;
            }

            // Return the glyph data for the specified codepoint
            return glyph;

            TrueTypeGlyph GetGlyphByIndex(int index)
            {
                // Glyph is not yet known
                if (_glyphs[index] == null)
                {
                    // Create glyph (codepoint is unknown)
                    _glyphs[index] = new TrueTypeGlyph(this, index);
                }

                return _glyphs[index];
            }
        }

        #endregion

        /// <summary>
        /// Renders a glyph into the target image.
        /// </summary>
        internal void RenderTo(int glyph, float scale, Image image, int x, int y)
        {
            // Render glyph to stb bitmap
            int w, h, ox, oy;
            var pBitmap = stbtt_GetGlyphBitmap(Info, scale, scale, glyph, &w, &h, &ox, &oy);

            var pixel = ColorBytes.White;

            // Copy stb bitmap into image
            for (var sY = 0; sY < h; sY++)
            {
                for (var sX = 0; sX < w; sX++)
                {
                    var i = sX + sY * w;

                    // Target image coordinates
                    var tX = x + sX;
                    var tY = y + sY;

                    // Adjust pixel alpha to match bitmap
                    pixel.A = pBitmap[i];
                    image.SetPixel(tX, tY, pixel);
                }
            }

            // Free stb bitmap
            stbtt_FreeBitmap(pBitmap, null);
        }

        #region Helper Functions

        private static byte* CloneUnmanaged(byte[] file)
        {
            var addr = Marshal.AllocHGlobal(file.Length);
            Marshal.Copy(file, 0, addr, file.Length);
            return (byte*) addr;
        }

        private static byte[] ReadAllBytes(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }

        #endregion

        #region Dispose

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    Info = default;
                }

                // Free font file clone on heap
                Marshal.FreeHGlobal((IntPtr) _inMemoryFile);

                _isDisposed = true;
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
