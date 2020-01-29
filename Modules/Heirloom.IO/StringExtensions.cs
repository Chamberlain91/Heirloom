namespace Heirloom.IO
{
    public static class StringExtensions
    {
        public static string ToIdentifier(this string path)
        {
            return EmbeddedFiles.NormalizeManifestPath(path);
        }
    }
}
