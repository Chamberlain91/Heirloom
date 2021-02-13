using System.Collections.Generic;

namespace Heirloom.Extras.Anim2D
{
    /// <summary>
    /// Represents an armature package, containing one or more armature definitions. <para/>
    /// See <see cref="ArmatureFactory.CreateArmature(string)"/> to instantiate armatures.
    /// </summary>
    /// <seealso cref="ArmatureFactory.LoadDragonBones(string)"/>
    /// <seealso cref="ArmatureFactory.CreateArmature(string)"/>
    /// <seealso cref="ArmatureFactory.Unload(ArmaturePackage)"/>
    public abstract class ArmaturePackage
    {
        /// <summary>
        /// The names of the armatures in this package.
        /// </summary>
        public abstract ICollection<string> ArmatureNames { get; }

        internal protected abstract Armature CreateArmature(string name);

        internal abstract void Unload();
    }
}
