using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.IO;

using static StbTrueTypeSharp.StbTrueType;

namespace Heirloom
{
    /// <summary>
    /// An object to represent a truetype font.
    /// Provides functionality to query and measure aspects of the font.
    /// </summary>
    public unsafe class Font : IDisposable
    {
        static Font()
        {
            // Load default pixel font
            using var stream = Files.OpenStream("embedded/monogram_extended.ttf");
            Default = new Font(stream);
        }

        internal stbtt_fontinfo Info;
        private readonly byte* _inMemoryFile; // Don't need technically if using StbSharp
        private bool _isDisposed = false;

        private readonly Dictionary<UnicodeCharacter, Glyph> _glyphLookup;
        private readonly Glyph[] _glyphs;

        private readonly int _ascent;
        private readonly int _descent;
        private readonly int _lineGap;

        /// <summary>
        /// A default pixel font for easily rendering text to debug, show metrics, etc.
        /// Recommended size is 16px.
        /// </summary>
        /// <remarks>https://datagoblin.itch.io/monogram</remarks>
        public static Font Default { get; }

        #region Constructors

        /// <summary>
        /// Loads a font specified by path resolved by <see cref="Files.OpenStream(string)"/>.
        /// </summary>
        public Font(string path)
            : this(Files.OpenStream(path))
        { }

        /// <summary>
        /// Loads a font from a stream.
        /// </summary>
        public Font(Stream stream)
            : this(ReadAllBytes(stream))
        { }

        /// <summary>
        /// Loads a font from a block of bytes.
        /// </summary>
        public Font(byte[] file)
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
            _glyphLookup = new Dictionary<UnicodeCharacter, Glyph>();
            _glyphs = new Glyph[Info.numGlyphs];
        }

        ~Font()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// Get the vertical metrics of the this font at the specified size.
        /// </summary>
        /// <param name="size">The size of the font.</param>
        public FontMetrics GetMetrics(float size)
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

        /// <summary>
        /// Gets the spacing adjustment (ie, kerning) between any two characters.
        /// </summary>
        public float GetKerning(UnicodeCharacter cp1, UnicodeCharacter cp2, float size)
        {
            // Compute scaling factor
            var scale = ComputeScale(size);

            // Get the pair of glyphs
            var g1 = GetGlyph(cp1);
            var g2 = GetGlyph(cp2);

            // Get kerning advance between this glyph an another
            return stbtt_GetGlyphKernAdvance(Info, g1.Index, g2.Index) * scale;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal float ComputeScale(float height)
        {
            return stbtt_ScaleForMappingEmToPixels(Info, height);
        }

        #region Get Glyph

        /// <summary>
        /// Gets the information about a particular glyph in this font.
        /// </summary>
        /// <param name="ch">Some character.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Glyph GetGlyph(char ch)
        {
            return GetGlyph((UnicodeCharacter) ch);
        }

        /// <summary>
        /// Gets the information about a particular glyph in this font.
        /// </summary>
        /// <param name="ch">Some character.</param>
        public Glyph GetGlyph(UnicodeCharacter ch)
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

            Glyph GetGlyphByIndex(int index)
            {
                // Glyph is not yet known
                if (_glyphs[index] == null)
                {
                    // Create glyph (codepoint is unknown)
                    _glyphs[index] = new Glyph(this, index);
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
            stbtt_FreeBitmap(pBitmap);
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

        /// <summary>
        /// Dispose the current font, freeing unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
