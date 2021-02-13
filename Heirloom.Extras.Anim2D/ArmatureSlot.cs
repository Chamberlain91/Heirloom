using System;

using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Extras.Anim2D
{
    public abstract class ArmatureSlot
    {
        public abstract void Draw(GraphicsContext gfx, Matrix matrix);

        public abstract bool IsVisible { get; set; }
    }

    public abstract class ArmatureBone
    {
        protected ArmatureBone(Armature armature)
        {
            Armature = armature ?? throw new ArgumentNullException(nameof(armature));
        }

        public Armature Armature { get; }

        public abstract string Name { get; }
    }
}
