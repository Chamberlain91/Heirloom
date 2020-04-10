using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Heirloom.GenDoc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 
            var searchDirectory = ParseInputArgs(args);

            // 
            Console.WriteLine("Discovering assemblies and generating documentation!");
            var assemblies = FindAndLoadDocumentedAssemblies(searchDirectory);

            // Load xml documentation for assemblies
            foreach (var assembly in assemblies)
            {
                // Populate Types
                var assemblyTypes = TypeDatabase.GetAssemblyTypes(assembly);
                TypeDatabase.PopulateTypes(assemblyTypes);

                // Load XML Documentation
                Documentation.LoadDocumentation(assembly);
            }

            // Generate documentation per assembly
            var generator = new MarkdownGenerator();
            foreach (var assembly in assemblies)
            {
                Console.WriteLine(" - " + Path.GetFileName(assembly.Location));

                // Delete directory (if exists) and regenerate
                var dir = assembly.GetName().Name;
                if (Directory.Exists(dir)) { Directory.Delete(dir, true); }
                Directory.CreateDirectory(dir);

                // Generate Markdown Documentation
                generator.Generate(assembly, dir);
            }

            // Generate Main TOC
            generator.GenerateIndex(assemblies, ".");

            Console.WriteLine("Complete!");
        }

        private static HashSet<Assembly> FindAndLoadDocumentedAssemblies(string directory)
        {
            var assemblies = new HashSet<Assembly>();
            foreach (var path in Directory.EnumerateFiles(directory, "*.xml", SearchOption.AllDirectories))
            {
                var assemblyPath = Path.ChangeExtension(path, "dll");
                if (File.Exists(assemblyPath))
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(assemblyPath);
                        assemblies.Add(assembly);
                    }
                    catch (FileLoadException)
                    {
                        // Eh, this is ok...
                    }
                }
            }

            return assemblies;
        }

        private static string ParseInputArgs(string[] args)
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
                Environment.Exit(0);
            }

            return directory;
        }
    }
}
