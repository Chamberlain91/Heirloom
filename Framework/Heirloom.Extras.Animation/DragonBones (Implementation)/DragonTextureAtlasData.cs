using DragonBones;

using Heirloom.Drawing;

namespace Heirloom.Extras.Animation
{
    internal sealed class DragonTextureAtlasData : TextureAtlasData
    {
        public Image Image { get; set; }

        public override TextureData CreateTexture()
        {
            return BorrowObject<DragonTextureData>();
        }

        protected override void _OnClear()
        {
            base._OnClear();

            // Dispose texture
            Image?.Dispose();
            Image = null;
        }
    }
}
