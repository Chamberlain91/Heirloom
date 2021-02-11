using DragonBones;

using Heirloom.Drawing;

namespace Heirloom.Examples.Anim2D.Anim2D
{
    public sealed class AnimTextureAtlasData : TextureAtlasData
    {
        public Image Image { get; set; }

        public override TextureData CreateTexture()
        {
            return BorrowObject<AnimTextureData>();
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
