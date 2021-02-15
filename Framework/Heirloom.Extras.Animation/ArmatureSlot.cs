using System;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Extras.Animation
{
    public abstract class ArmatureSlot
    {
        protected ArmatureSlot(Armature armature)
        {
            Armature = armature ?? throw new ArgumentNullException(nameof(armature));
        }

        public Armature Armature { get; }

        public abstract void Draw(GraphicsContext gfx, Matrix matrix);

        public abstract bool IsVisible { get; set; }

        public abstract ArmatureBone Bone { get; }
    }
}
