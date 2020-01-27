namespace Heirloom.IO
{
    public static class StringExtensions
    {
        public static string AsIdentifier(this string path)
        {
            return EmbeddedFiles.NormalizeManifestPath(path);
        }
    }
}
