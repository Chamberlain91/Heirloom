using System;
using System.IO;

namespace Heirloom.Runtime.Loaders
{
    public sealed class AseFileLoader : AssetLoader<SpriteCollection>
    {
        protected override bool Load(string identifier, Stream stream, out SpriteCollection asset)
        {
            try
            {
                using (var aseFile = new AsepriteFile(stream))
                {
                    asset = aseFile.ConstructSpriteCollection();
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Warn(e.Message);

                asset = default;
                return false;
            }
        }
    }
}
