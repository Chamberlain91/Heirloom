using System;

using Heirloom.Drawing;

using Matrix = Heirloom.Mathematics.Matrix;
using DragonBone = DragonBones.Bone;

namespace Heirloom.Extras.Anim2D
{
    internal sealed class DragonBonesArmatureSlot : ArmatureSlot
    {
        private readonly DragonSlot _slot;

        public DragonBonesArmatureSlot(DragonSlot slot)
        {
            _slot = slot ?? throw new ArgumentNullException(nameof(slot));
        }

        public override void Draw(GraphicsContext gfx, Matrix matrix)
        {
            _slot.Draw(gfx, matrix);
        }

        public override bool IsVisible
        {
            get => _slot.Visible;
            set => _slot.Visible = value;
        }
    }

    internal sealed class DragonBonesArmatureBone : ArmatureBone
    {
        internal readonly DragonBone DragonBone;

        public DragonBonesArmatureBone(Armature armature, DragonBone bone) : base(armature)
        {
            DragonBone = bone ?? throw new ArgumentNullException(nameof(bone));
        }

        public override string Name => DragonBone.Name;
    }
}
