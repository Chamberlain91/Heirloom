using System;
using System.IO;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Sound;

namespace SharpDoc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assembly = typeof(Graphics).Assembly;

            // Delete directory (if exists) and regenerate
            var dirName = Path.GetFileNameWithoutExtension(assembly.Location);
            if (Directory.Exists(dirName)) { Directory.Delete(dirName, true); }
            Directory.CreateDirectory(dirName);

            // Emit files for each type
            foreach (var type in assembly.ExportedTypes)
            {
                var markdown = $"{type.Namespace}\n";
                markdown += $"{new string('-', 60)}\n\n";

                // 
                markdown += MarkdownGenerator.GenerateMarkdown(type);

                // Write to disk
                var filePath = SanitizePath($"{type.Namespace}.{type.GetHumanName()}.md");
                File.WriteAllText(Path.Join(dirName, filePath), markdown);
            }
        }

        private static string SanitizePath(string path)
        {
            path = path.Replace('<', '[');
            path = path.Replace('>', ']');
            return path;
        }
    }
}
