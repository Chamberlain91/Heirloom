using System;

namespace Heirloom.Drawing
{
    public struct ColorMap
    {
        public Image Image { get; }

        /// <summary>
        /// The default color map, it maps pixels back to itself.
        /// You can save this image to disk, along with a screenshot to tweak the colors as desired.
        /// </summary>
        public static ColorMap Default { get; } = new ColorMap(GenerateDefaultImage());

        public ColorMap(Image image)
        {
            if (image.Width != 256) { throw new ArgumentException("Invalid image width.", nameof(image.Width)); }
            if (image.Height != 16) { throw new ArgumentException("Invalid image height.", nameof(image.Height)); }

            Image = image;
        }

        /// <summary>
        /// Generate the default correction image. Only useful for setting a 
        /// </summary>
        /// <returns></returns>
        internal static Image GenerateDefaultImage()
        {
            var image = new Image(256, 16);

            var z = 0;
            for (var b = 0; b < 16; b++)
            {
                var y = 0;
                for (var g = 0; g < 16; g++)
                {
                    var x = z;
                    for (var r = 0; r < 16; r++)
                    {
                        // Scale up to full byte
                        var r1 = (byte) (r * 16);
                        var g1 = (byte) (g * 16);
                        var b1 = (byte) (b * 16);

                        // Write pixel to image
                        image.SetPixel(x, y, new Pixel(r1, g1, b1, 0xFF));
                        x++;
                    }
                    y++;
                }
                z += 16;
            }

            return image;
        }
    }
}
