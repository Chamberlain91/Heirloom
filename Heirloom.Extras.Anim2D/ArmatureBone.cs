using System;
using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Extras.Anim2D
{
    public abstract class ArmatureBone
    {
        protected ArmatureBone(Armature armature)
        {
            Armature = armature ?? throw new ArgumentNullException(nameof(armature));
        }

        /// <summary>
        /// The armature this bone is part of.
        /// </summary>
        public Armature Armature { get; }

        /// <summary>
        /// The slots contained by this bone.
        /// </summary>
        public abstract IReadOnlyList<ArmatureSlot> Slots { get; }

        /// <summary>
        /// The name of the bone.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The parent of this bone (or null if a root bone).
        /// </summary>
        public abstract ArmatureBone Parent { get; }

        /// <summary>
        /// The length of the bone.
        /// </summary>
        public abstract float Length { get; }

        /// <summary>
        /// The global transform relative to the armature origin.
        /// </summary>
        public abstract Matrix Transform { get; }

        /// <summary>
        /// The position of the base of the bone.
        /// </summary>
        public abstract Vector Base { get; }

        /// <summary>
        /// The position of the tip of the bone.
        /// </summary>
        public abstract Vector Tip { get; }
    }
}
