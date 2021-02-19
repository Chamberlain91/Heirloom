using System;
using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Mathematics;

using Matrix = Heirloom.Mathematics.Matrix;

namespace Heirloom.Extras.Animation
{
    public abstract class Armature : IDisposable
    {
        internal Armature(ArmaturePackage armature)
        {
            ArmaturePackage = armature ?? throw new ArgumentNullException(nameof(armature));
        }

        /// <summary>
        /// Enable or disable drawing armature debug information.
        /// </summary>
        public bool EnableDebug { get; set; }

        public ArmaturePackage ArmaturePackage { get; }

        public abstract AnimationPlayer Animation { get; }

        public abstract IReadOnlyCollection<ArmatureSlot> Slots { get; }

        public abstract IReadOnlyCollection<ArmatureBone> Bones { get; }

        public abstract bool FlipX { get; set; }

        public abstract bool FlipY { get; set; }

        public abstract void Draw(GraphicsContext gfx, Matrix matrix);

        public abstract ArmatureSlot GetSlot(string name);

        public abstract ArmatureBone GetBone(string name);

        public abstract void Update(float dt);

        #region Contains

        public abstract bool ContainsPoint(Vector point, out ArmatureSlot slot);

        public bool Contains(Vector point)
        {
            return ContainsPoint(point, out _);
        }

        #endregion

        #region Intersects Segment

        public abstract bool IntersectsSegment(Vector start, Vector end, out RayContact near, out RayContact far, out ArmatureSlot slot);

        public abstract bool IntersectsSegment(Vector start, Vector end, out RayContact near, out ArmatureSlot slot);

        public abstract bool IntersectsSegment(Vector start, Vector end, out ArmatureSlot slot);

        public bool IntersectsSegment(LineSegment segment, out RayContact near, out RayContact far, out ArmatureSlot slot)
        {
            return IntersectsSegment(segment.A, segment.B, out near, out far, out slot);
        }

        public bool IntersectsSegment(LineSegment segment, out RayContact near, out ArmatureSlot slot)
        {
            return IntersectsSegment(segment.A, segment.B, out near, out slot);
        }

        public bool IntersectsSegment(LineSegment segment, out ArmatureSlot slot)
        {
            return IntersectsSegment(segment.A, segment.B, out slot);
        }

        public bool IntersectsSegment(LineSegment segment, out RayContact near, out RayContact far)
        {
            return IntersectsSegment(segment.A, segment.B, out near, out far, out _);
        }

        public bool IntersectsSegment(LineSegment segment, out RayContact near)
        {
            return IntersectsSegment(segment.A, segment.B, out near, out _);
        }

        public bool IntersectsSegment(LineSegment segment)
        {
            return IntersectsSegment(segment.A, segment.B, out _);
        }

        #endregion

        #region Raycast

        public bool Raycast(Ray ray, out RayContact contact, out ArmatureSlot slot)
        {
            var a = ray.Origin;
            var b = ray.GetPoint(float.MaxValue);

            if (IntersectsSegment(a, b, out contact, out slot))
            {
                return true;
            }
            else
            {
                // Failed to contact the armature
                contact = default;
                slot = default;

                return false;
            }
        }

        public bool Raycast(Ray ray, out RayContact contact)
        {
            return Raycast(ray, out contact);
        }

        #endregion

        public abstract void Dispose();
    }
}