using DragonBones;

using Heirloom.Drawing;

namespace Heirloom.Examples.Anim2D.Anim2D
{
    public sealed class AnimFactory : BaseFactory
    {
        private readonly AnimEventDispatcher _eventDispatcher;

        private static AnimFactory _factory;

        public static AnimFactory Factory
        {
            get
            {
                if (_factory == null) { _factory = new AnimFactory(); }
                return _factory;
            }
        }

        private AnimFactory(DataParser dataParser = null)
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

        protected override Armature _BuildArmature(BuildArmaturePackage dataPackage)
        {
            var armature = BaseObject.BorrowObject<Armature>();
            var proxy = new AnimArmature
            {
                Armature = armature
            };

            // ...
            Factory.Clock.Add(armature);

            armature.Init(dataPackage.Armature, proxy, proxy, DragonBones);
            return armature;
        }

        protected override Slot _BuildSlot(BuildArmaturePackage dataPackage, SlotData slotData, Armature armature)
        {
            var slot = BaseObject.BorrowObject<AnimSlot>();

            var _armature = armature.display as AnimArmature;
            _armature.Slots.Add(slot);

            slot.Init(slotData, armature, slot, slot);
            return slot;
        }
    }
}
