using System.IO;
using System.Xml.Linq;

namespace Heirloom.Runtime.Loaders
{
    internal static class XElementExtensions
    {
        public static XElement ParseXML(this Stream stream, string rootElement)
        {
            return XDocument.Load(stream).Element(rootElement);
        }

        public static int GetInteger(this XElement xe, string attribute)
        {
            return int.Parse(GetString(xe, attribute, "0"));
        }

        public static string GetString(this XElement xe, string attribute, string @default = default)
        {
            var attr = xe.Attribute(attribute);
            return attr != null ? attr.Value : @default;
        }
    }
}
