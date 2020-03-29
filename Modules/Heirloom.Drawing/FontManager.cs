using System.Collections.Generic;

namespace Heirloom.Drawing
{
    internal static class FontManager
    {
        private static readonly Dictionary<(Font, int), FontAtlas> _fonts;

        static FontManager()
        {
            _fonts = new Dictionary<(Font, int), FontAtlas>();
        }

        // TODO: Dynamic character cache atlas
        public static FontAtlas GetAtlas(Font face, int size)
        {
            lock (_fonts)
            {
                var key = (face, size);

                if (_fonts.ContainsKey(key))
                {
                    return _fonts[key];
                }
                else
                {
                    var font = new FontAtlas(face, size);
                    return _fonts[key] = font;
                }
            }
        }
    }
}
