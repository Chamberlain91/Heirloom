using System;

using Heirloom.Drawing;

using Matrix = Heirloom.Mathematics.Matrix;

namespace Heirloom.Extras.Anim2D
{
    internal sealed class DragonBonesArmatureSlot : ArmatureSlot
    {
        private readonly DragonBonesArmature _armature;
        private readonly DragonSlot _slot;

        public DragonBonesArmatureSlot(DragonBonesArmature armature, DragonSlot slot)
            : base(armature)
        {
            _armature = armature ?? throw new ArgumentNullException(nameof(armature));
            _slot = slot ?? throw new ArgumentNullException(nameof(slot));
        }

        public override ArmatureBone Bone => _armature.GetBone(_slot.parent);

        public override bool IsVisible
        {
            get => _slot.Visible;
            set => _slot.Visible = value;
        }

        public override void Draw(GraphicsContext gfx, Matrix matrix)
        {
            _slot.Draw(gfx, matrix);
        }
    }
}
