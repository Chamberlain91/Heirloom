using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SharpDoc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var directory = ".";
            var error = false;

            //
            if (args.Length == 0)
            {
                // Scan current directory
            }
            else if (args.Length == 1)
            {
                // Get full path to directory
                directory = Path.GetFullPath(args[0]);

                // 
                if (!Directory.Exists(directory))
                {
                    Console.WriteLine("Directory does not exist.");
                    Environment.Exit(-1);
                }
            }
            else
            {
                // Invalid argument count
                error = true;
            }

            if (error)
            {
                Console.WriteLine("Usage: [dir path]");
                return;
            }

            var assemblies = new HashSet<Assembly>();
            foreach (var path in Directory.EnumerateFiles(directory, "*.xml", SearchOption.AllDirectories))
            {
                var assemblyPath = Path.ChangeExtension(path, "dll");
                if (File.Exists(assemblyPath))
                {
                    var assembly = Assembly.LoadFrom(assemblyPath);
                    if (assemblies.Add(assembly))
                    {
                        Console.WriteLine(Path.GetFileName(path));
                    }
                }
            }

            // 
            foreach (var assembly in assemblies)
            {
                // Delete directory (if exists) and regenerate
                var dir = assembly.GetName().Name; // Path.GetFileNameWithoutExtension(assembly.Location);
                if (Directory.Exists(dir)) { Directory.Delete(dir, true); }
                Directory.CreateDirectory(dir);

                // Generate Markdown Documentation
                var generator = new MarkdownGenerator();
                generator.Generate(assembly, dir);
            }
        }
    }
}
