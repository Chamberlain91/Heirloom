using System;
using System.IO;
using System.Xml;
using Heirloom.Sound;

namespace SharpDoc
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = typeof(AudioClip).Assembly;

            // 
            var xmlPath = Path.ChangeExtension(assembly.Location, "xml");
            Console.WriteLine(xmlPath);

            var text = File.ReadAllText(xmlPath);

            // Load XML Document
            var doc = new XmlDocument();
            doc.LoadXml(text);

            var type = default(Type);

            var members = doc["doc"]["members"];
            foreach (XmlElement member in members.ChildNodes)
            {
                var parts = member.Attributes["name"].Value.Split(":");
                var name = parts[1];

                switch (parts[0])
                {
                    case "T":
                        type = assembly.GetType(name);
                        Console.WriteLine($"Type: {type.FullName}");
                        break;

                    case "M":
                        var mname = name.Substring(type.FullName.Length + 1);
                        var method = type.GetMethod(mname);
                        Console.WriteLine($"Method: {name} [{mname}] ({method})");
                        break;

                    case "E":
                        Console.WriteLine($"Event: {name}");
                        break;

                    case "F":
                        Console.WriteLine($"Field: {name}");
                        break;

                    case "P":
                        Console.WriteLine($"Proprty: {name}");
                        break;

                    default:
                        Console.WriteLine($"----: {name}");
                        break;
                }
            }
        }
    }
}
