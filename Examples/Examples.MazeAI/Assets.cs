using System.Collections.Generic;

using Heirloom;

namespace Examples.MazeAI
{
    public static class Assets
    {
        private static readonly Dictionary<int, Image> _images = new Dictionary<int, Image>();

        public static void LoadAssets()
        {
            LoadImages();

            static void LoadImages()
            {
                _images.Clear();

                for (var i = 0; i < 12; i++)
                {
                    // Load and store image
                    var image = new Image($"files/tile_{i:0000}.png");
                    _images[i] = image;
                }
            }
        }

        public static Image GetImage(int n)
        {
            return _images[n];
        }
    }
}
