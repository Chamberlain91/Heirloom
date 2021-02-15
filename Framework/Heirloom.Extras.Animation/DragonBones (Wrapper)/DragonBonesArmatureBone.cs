using System;
using System.Collections.Generic;

using Heirloom.Mathematics;

using DragonBone = DragonBones.Bone;

namespace Heirloom.Extras.Animation
{
    internal sealed class DragonBonesArmatureBone : ArmatureBone
    {
        private readonly DragonBonesArmature _armature;
        private readonly List<ArmatureSlot> _slots;

        internal readonly DragonBone DragonBone;

        public DragonBonesArmatureBone(DragonBonesArmature armature, DragonBone bone) : base(armature)
        {
            _armature = armature ?? throw new ArgumentNullException(nameof(armature));
            DragonBone = bone ?? throw new ArgumentNullException(nameof(bone));

            // Discover slots
            _slots = new List<ArmatureSlot>();
            foreach (var slot in DragonBone.Armature.GetSlots())
            {
                if (slot.parent == DragonBone)
                {
                    _slots.Add(_armature.GetSlot(slot as DragonSlot));
                }
            }
        }

        public override IReadOnlyList<ArmatureSlot> Slots => _slots;

        public override string Name => DragonBone.Name;

        public override ArmatureBone Parent => _armature.GetBone(DragonBone.Parent);

        public override float Length => DragonBone.BoneData.length;

        public override Matrix Transform => Helper.GetHeirloomMatrix(DragonBone.GlobalTransformMatrix);

        public override Vector Base
        {
            get
            {
                var global = DragonBone.Global;
                return new Vector(global.x, global.y);
            }
        }

        public override Vector Tip
        {
            get
            {
                var pos = new Vector(Length, 0);
                Matrix.Multiply(Transform, pos, ref pos);
                return pos;
            }
        }
    }
}
