using System.Collections.Generic;
using System.IO;

namespace Heirloom.IO
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads the entire contents of the stream as a block of text.
        /// </summary>
        public static string ReadAllText(this Stream stream)
        {
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Reads the entire contents of the stream line by line.
        /// </summary>
        public static IEnumerable<string> ReadLines(this Stream stream)
        {
            using var reader = new StreamReader(stream);
            var line = null as string;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Reads the entire contents of the stream as blob of bytes.
        /// </summary>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            using var memory = new MemoryStream();
            stream.CopyTo(memory);
            return memory.ToArray();
        }
    }
}
