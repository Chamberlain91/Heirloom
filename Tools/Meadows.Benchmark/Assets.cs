using System.Collections.Generic;
using System.IO;
using System.Linq;

using Meadows.Drawing;
using Meadows.IO;

namespace Meadows.Benchmark
{
    public static class Assets
    {
        public static IEnumerable<Image> LoadImages(string directory)
        {
            var images = Files.EnumerateFiles(Files.Join(directory, "*"))
                              .Where(HasImageExtension)
                              .Select(p => new Image(p))
                              .ToArray();

            // Something went wrong couldn't find files
            if (images.Length == 0)
            {
                throw new FileNotFoundException($"Discovery of embedded with prefix '{directory}' failed.");
            }

            return images;
        }

        private static bool HasImageExtension(string p)
        {
            var ext = Path.GetExtension(p);
            return ext == ".png" || ext == ".jpg";
        }
    }
}
