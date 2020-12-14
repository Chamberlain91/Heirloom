using System.Collections.Generic;
using System.IO;
using System.Linq;

using Meadows.Drawing;
using Meadows.IO;

namespace Meadows.Benchmark
{
    public static class Assets
    {
        public static IEnumerable<Image> LoadImages(string prefix, bool center)
        {
            prefix = prefix.Replace("/", "."); // correct slashes to identifier format

            var images = Files.GetEmbeddedFiles()
                              .Where(ef => ef.Identifiers.Any(i => i.StartsWith(prefix)))
                              .Select(ef => ef.Identifiers.First())
                              .Where(HasImageExtension)
                              .Select(p => new Image(p))
                              .ToArray();

            // Something went wrong couldn't find files
            if (images.Length == 0)
            {
                throw new FileNotFoundException($"Discovery of embedded with prefix '{prefix}' failed.");
            }

            if (center)
            {
                // Set the origin of each images to its center
                foreach (var image in images)
                {
                    // todo: do I want the origin thing, probably no.
                    // image.Origin = (IntVector) image.Size / 2;
                }
            }

            // Image.CreateAtlas(images);
            return images;
        }

        private static bool HasImageExtension(string p)
        {
            var ext = Path.GetExtension(p);
            return ext == ".png" || ext == ".jpg";
        }
    }
}
