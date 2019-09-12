using System;
using System.Diagnostics;
using System.IO;

namespace Heirloom.Drawing.OpenGL.Utilities
{
    internal static class EmbeddedFiles
    {
        public static string ReadText(string path)
        {
            using (var stream = OpenStream(path))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream OpenStream(string path)
        {
            // Make path suitable for embedded files
            path = path.Replace('\\', '/');
            path = path.Replace('/', '.');

            // Get the assembly relative to this embedded files class
            var assembly = typeof(EmbeddedFiles).Assembly;

            // Look at all embedded assets, if part of an Embedded folder
            // attempt to resolve this was the resource.
            foreach (var name in assembly.GetManifestResourceNames())
            {
                if (name.Contains("Embedded.") && name.Contains(path))
                {
                    return assembly.GetManifestResourceStream(name);
                }
            }

            Print($"Did not find '{path}'");
            throw new InvalidOperationException($"Unable to find file named '{path}' embedded manifest.");
        }

        [Conditional("DEBUG")]
        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
