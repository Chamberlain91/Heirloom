using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Heirloom.Math;

using static StbTrueTypeSharp.StbTrueType;

namespace Heirloom.Drawing
{
    public unsafe class Font : IDisposable
    {
        /// <summary>
        /// A default pixel font for easily rendering text to debug, show metrics, etc.
        /// Recommended size is 16px.
        /// </summary>
        /// <remarks>https://datagoblin.itch.io/monogram</remarks>
        public static Font Default { get; }
         
        static Font()
        {
            // 
            var assembly = typeof(Font).Assembly;

            // Load default pixel font
            using (var stream = assembly.GetManifestResourceStream("Heirloom.Drawing.Embedded.monogram_extended.ttf"))
            {
                Default = new Font(stream);
            } 
        }

        internal stbtt_fontinfo Info;
        private readonly byte* _inMemoryFile; // Don't need technically if using StbSharp
        private bool _isDisposed = false;

        private readonly Dictionary<UnicodeCharacter, Glyph> _glyphByCodepoint;
        private readonly Glyph[] _glyphs;

        private readonly int _ascent;
        private readonly int _descent;
        private readonly int _lineGap;

        #region Constructors

        public Font(Stream stream)
            : this(ReadAllBytes(stream))
        { }

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
            _glyphByCodepoint = new Dictionary<UnicodeCharacter, Glyph>();
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
        /// <param name="mode">The mode to measure the font.</param>
        /// <returns></returns>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float ComputeScale(float height)
        {
            return stbtt_ScaleForMappingEmToPixels(Info, height);
        }

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
        public Size MeasureText(string text, int fontSize)
        {
            return MeasureText(text, Size.Infinite, fontSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size MeasureText(string text, Size layoutSize, int fontSize)
        {
            // Get font atlas
            var atlas = FontManager.GetAtlas(this, fontSize);

            // Layout text, keeping track of the glyph box
            var measure = Rectangle.Zero;
            TextRenderer.LayoutText(text, (Vector.Zero, layoutSize), TextAlign.Left, atlas, (string _, int index, ref CharacterLayoutState state) =>
            {
                // Include extents of glyph box
                measure.Merge(state.Position);
                measure.Merge(state.Position + (state.Metrics.AdvanceWidth, atlas.Metrics.LineAdvance));
            });

            return measure.Size;
        }

        public class Glyph
        {
            private readonly int _advanceWidth;
            private readonly int _bearing;
            internal readonly int Index;
            internal IntRectangle Box;

            public Font Font { get; }

            public UnicodeCharacter Codepoint { get; private set; }

            internal bool HasCodepoint { get; private set; }

            internal Glyph(Font font, int index)
            {
                // Get glyph index (search)
                Index = index;
                Font = font;

                // Get horizontal metrics (at raw scale)
                int advWidth, bearing;
                stbtt_GetGlyphHMetrics(Font.Info, Index, &advWidth, &bearing);
                _advanceWidth = advWidth;
                _bearing = bearing;

                // Get glyph box info
                int x0, x1, y0, y1;
                stbtt_GetGlyphBitmapBox(Font.Info, Index, 1F, 1F, &x0, &y0, &x1, &y1);
                Box = new IntRectangle(x0, y0, x1 - x0, y1 - y0);
            }

            public bool IsVisibleCharacter
            {
                get
                {
                    if (Index == 0) { return false; }
                    return stbtt_IsGlyphEmpty(Font.Info, Index) == 0;
                }
            }

            public GlyphMetrics GetMetrics(float size)
            {
                // Compute scaling factor
                var scale = Font.ComputeScale(size);

                // Compute metrics
                var advanceWidth = _advanceWidth * scale;
                var bearing = _bearing * scale;

                // Compute scaled pixel box
                var x0 = Calc.Floor(Box.X * scale);
                var y0 = Calc.Floor(Box.Y * scale);
                var x1 = Calc.Ceil((Box.X + Box.Width) * scale);
                var y1 = Calc.Ceil((Box.Y + Box.Height) * scale);

                // Return metrics to user
                return new GlyphMetrics(advanceWidth, bearing, new IntRectangle(x0, y0, x1 - x0, y1 - y0));
            }

            internal void SetCodepoint(UnicodeCharacter codepoint)
            {
                if (HasCodepoint) { throw new InvalidOperationException("Unable to set glyph's codpoint, already set"); }
                else
                {
                    Codepoint = codepoint;
                    HasCodepoint = true;
                }
            }

            public void RenderTo(Image image, int x, int y, float size)
            {
                // Compute scaling factor
                var scale = Font.ComputeScale(size);

                // Render into image
                Font.RenderTo(Index, scale, image, x, y);
            }

            public override string ToString()
            {
                var c = char.ConvertFromUtf32((int) Codepoint);
                return $"{nameof(Glyph)} of '{c}'";
            }
        }

        #region Get Glyph

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Glyph GetGlyph(char ch)
        {
            return GetGlyph((UnicodeCharacter) ch);
        }

        public Glyph GetGlyph(UnicodeCharacter ch)
        {
            // Attempt to lookup glyph from dictionary
            if (!_glyphByCodepoint.TryGetValue(ch, out var glyph))
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
                _glyphByCodepoint[ch] = glyph;
            }

            // Return the glyph data for the specified codepoint
            return glyph;
        }

        internal Glyph GetGlyphByIndex(int index)
        {
            // Glyph is not yet known
            if (_glyphs[index] == null)
            {
                // Create glyph (codepoint is unknown)
                _glyphs[index] = new Glyph(this, index);
            }

            return _glyphs[index];
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

            var pixel = Pixel.White;

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
                    // TODO: Might have to adjust for blend mode when decided, pre-mul or not.
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
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        #endregion

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
