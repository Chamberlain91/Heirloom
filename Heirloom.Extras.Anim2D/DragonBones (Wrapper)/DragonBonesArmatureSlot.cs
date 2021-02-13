using System;

using Heirloom.Drawing;
// using Transform = Heirloom.Mathematics.Transform;
using Matrix = Heirloom.Mathematics.Matrix;

namespace Heirloom.Extras.Anim2D
{
    #region Armature Slot

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

#endregion
}
