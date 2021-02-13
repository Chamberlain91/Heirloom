using DragonBones;

using Heirloom.Drawing;

using DBArmature = DragonBones.Armature;

namespace Heirloom.Extras.Anim2D
{
    internal sealed class DragoneBonesFactory : BaseFactory
    {
        private readonly AnimEventDispatcher _eventDispatcher;

        private static DragoneBonesFactory _factory;

        public static DragoneBonesFactory Factory
        {
            get
            {
                if (_factory == null) { _factory = new DragoneBonesFactory(); }
                return _factory;
            }
        }

        private DragoneBonesFactory(DataParser dataParser = null)
            : base(dataParser)
        {
            _eventDispatcher = new AnimEventDispatcher();
            DragonBones = new DragonBones.DragonBones(_eventDispatcher);
        }

        protected override TextureAtlasData _BuildTextureAtlasData(TextureAtlasData textureAtlasData, object textureAtlas)
        {
            if (textureAtlasData is AnimTextureAtlasData data)
            {
                if (textureAtlas is Image image)
                {
                    // ...
                    data.Image = image;
                }
            }
            else
            {
                textureAtlasData = BaseObject.BorrowObject<AnimTextureAtlasData>();
            }

            return textureAtlasData;
        }

        protected override DBArmature _BuildArmature(BuildArmaturePackage dataPackage)
        {
            var armature = BaseObject.BorrowObject<DBArmature>();
            var proxy = new AnimArmature
            {
                Armature = armature
            };

            // ...
            Factory.Clock.Add(armature);

            armature.Init(dataPackage.Armature, proxy, proxy, DragonBones);
            return armature;
        }

        protected override Slot _BuildSlot(BuildArmaturePackage dataPackage, SlotData slotData, DBArmature armature)
        {
            var slot = BaseObject.BorrowObject<AnimSlot>();

            var _armature = armature.Display as AnimArmature;
            _armature.Slots.Add(slot);

            slot.Init(slotData, armature, slot, slot);
            return slot;
        }
    }
}
