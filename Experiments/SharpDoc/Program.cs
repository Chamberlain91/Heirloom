using System.IO;

using Heirloom.Drawing;

namespace SharpDoc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // todo: get from args and use Assembly.ReflectionOnlyLoad?
            var assembly = typeof(Graphics).Assembly;

            // Delete directory (if exists) and regenerate
            var dir = Path.GetFileNameWithoutExtension(assembly.Location);
            if (Directory.Exists(dir)) { Directory.Delete(dir, true); }
            Directory.CreateDirectory(dir);

            // Generate Markdown Documentation
            var generator = new MarkdownGenerator();
            foreach (var (path, text) in generator.Generate(assembly, dir))
            {
                File.WriteAllText(path, text);
            }
        }
    }
}
