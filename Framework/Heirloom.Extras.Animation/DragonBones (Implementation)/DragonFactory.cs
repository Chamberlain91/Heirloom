using DragonBones;

using Heirloom.Drawing;

using DBArmature = DragonBones.Armature;

namespace Heirloom.Extras.Animation
{
    internal sealed class DragonFactory : BaseFactory
    {
        private readonly DragonEventDispatcher _eventDispatcher;

        private static DragonFactory _factory;

        public static DragonFactory Factory
        {
            get
            {
                if (_factory == null) { _factory = new DragonFactory(); }
                return _factory;
            }
        }

        private DragonFactory(DataParser dataParser = null)
            : base(dataParser)
        {
            _eventDispatcher = new DragonEventDispatcher();
            DragonBones = new DragonBones.DragonBones(_eventDispatcher);
        }

        protected override TextureAtlasData _BuildTextureAtlasData(TextureAtlasData textureAtlasData, object textureAtlas)
        {
            if (textureAtlasData is DragonTextureAtlasData data)
            {
                if (textureAtlas is Image image)
                {
                    // ...
                    data.Image = image;
                }
            }
            else
            {
                textureAtlasData = BaseObject.BorrowObject<DragonTextureAtlasData>();
            }

            return textureAtlasData;
        }

        protected override DBArmature _BuildArmature(BuildArmaturePackage dataPackage)
        {
            var armature = BaseObject.BorrowObject<DBArmature>();
            var proxy = new DragonArmatureProxy
            {
                Armature = armature
            };

            armature.Init(dataPackage.Armature, proxy, proxy, DragonBones);
            return armature;
        }

        protected override Slot _BuildSlot(BuildArmaturePackage dataPackage, SlotData slotData, DBArmature armature)
        {
            var slot = BaseObject.BorrowObject<DragonSlot>();

            var _armature = armature.Display as DragonArmatureProxy;
            _armature.Slots.Add(slot);

            slot.Init(slotData, armature, slot, slot);
            return slot;
        }
    }
}
