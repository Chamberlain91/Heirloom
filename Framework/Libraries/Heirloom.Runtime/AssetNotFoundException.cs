using System;

namespace Heirloom.Runtime
{
    [Serializable]
    internal class AssetNotFoundException : Exception
    {
        public AssetNotFoundException(string identifier)
            : base($"Unable to find asset '{identifier}'.")
        { }
    }
}
