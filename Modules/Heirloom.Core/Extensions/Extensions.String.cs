namespace Heirloom
{
    /// <summary>
    /// Provides extensions to strings.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts this string into a standardized "identifier".
        /// </summary>
        public static string ToIdentifier(this string path)
        {
            return EmbeddedFiles.NormalizeManifestPath(path);
        }
    }
}
