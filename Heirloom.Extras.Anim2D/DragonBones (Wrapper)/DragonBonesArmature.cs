using System.Collections.Generic;

using DragonBones;

using Heirloom.Drawing;
using Heirloom.Mathematics;

using DBArmature = DragonBones.Armature;
using Matrix = Heirloom.Mathematics.Matrix;

namespace Heirloom.Extras.Anim2D
{
    public enum EventType
    {
        Start,
        LoopComplete,
        Complete,
        FrameEvent,
        SoundEvent
    }

    internal sealed class DragonBonesArmature : Armature
    {
        private static readonly Point _normalPoint = new Point();
        private static readonly Point _nearPoint = new Point();
        private static readonly Point _farPoint = new Point();

        private readonly DBArmature _armature;
        private readonly Dictionary<DragonSlot, DragonBonesArmatureSlot> _slots;

        public DragonBonesArmature(DragonBonesArmaturePackage armature, string name)
            : base(armature)
        {
            // Build dragon bones armature representation
            _armature = DragonFactory.Factory.BuildArmature(name, armature.Identifier);

            // Register event listeners
            _armature.EventDispatcher.AddDBEventListener(EventObject.START, OnEvent);
            _armature.EventDispatcher.AddDBEventListener(EventObject.COMPLETE, OnEvent);
            _armature.EventDispatcher.AddDBEventListener(EventObject.LOOP_COMPLETE, OnEvent);
            _armature.EventDispatcher.AddDBEventListener(EventObject.FRAME_EVENT, OnEvent);
            _armature.EventDispatcher.AddDBEventListener(EventObject.SOUND_EVENT, OnEvent);

            // Map internal slots public counterparts
            _slots = new Dictionary<DragonSlot, DragonBonesArmatureSlot>();
            foreach (DragonSlot slot in _armature.GetSlots())
            {
                _slots[slot] = new DragonBonesArmatureSlot(slot);
            }

            // Create animation wrapper
            Animation = new DragonBonesAnimationPlayer(_armature.Animation);
        }

        private void OnEvent(string type, EventObject ev)
        {
            Log.Warning($"Armature Event: {type}");

            switch (type)
            {
                case EventObject.START:
                    OnStartEvent(ev);
                    break;

                case EventObject.COMPLETE:
                    OnCompleteEvent(ev, false);
                    break;

                case EventObject.LOOP_COMPLETE:
                    OnCompleteEvent(ev, true);
                    break;

                case EventObject.FRAME_EVENT:
                    OnFrameEvent(ev);
                    break;

                case EventObject.SOUND_EVENT:
                    OnSoundEvent(ev);
                    break;
            }
        }

        private void OnStartEvent(EventObject ev)
        {
            // Nothing
        }

        private void OnCompleteEvent(EventObject ev, bool isLoop)
        {
            // Nothing
        }

        private void OnFrameEvent(EventObject ev)
        {
            // Nothing
        }

        private void OnSoundEvent(EventObject ev)
        {
            // Nothing
        }

        public override IReadOnlyCollection<ArmatureSlot> Slots => _slots.Values;

        public override AnimationPlayer Animation { get; }

        public override bool FlipX
        {
            get => _armature.FlipX;
            set => _armature.FlipX = value;
        }

        public override bool FlipY
        {
            get => _armature.FlipY;
            set => _armature.FlipY = value;
        }

        public override void AdvanceTime(float dt)
        {
            _armature.AdvanceTime(dt);
        }

        public override void Draw(GraphicsContext gfx, Matrix matrix)
        {
            foreach (var slot in _slots.Keys)
            {
                slot.Draw(gfx, matrix);
            }
        }

        #region Contains Point

        public override bool ContainsPoint(Vector point, out ArmatureSlot slot)
        {
            var touch = _armature.ContainsPoint(point.X, point.Y);
            if (touch == null)
            {
                slot = default;
                return false;
            }
            else
            {
                slot = _slots[touch as DragonSlot];
                return true;
            }
        }

        #endregion

        #region Intersects Segment

        public override bool IntersectsSegment(Vector start, Vector end, out RayContact near, out RayContact far, out ArmatureSlot slot)
        {
            var touch = _armature.IntersectsSegment(start.X, start.Y, end.X, end.Y, _nearPoint, _farPoint, _normalPoint);
            if (touch == null)
            {
                near = default;
                far = default;
                slot = default;
                return false;
            }
            else
            {
                var nearPos = new Vector(_nearPoint.X, _nearPoint.Y);
                near = new RayContact(nearPos, Vector.FromAngle(_normalPoint.X), Vector.Distance(start, nearPos));

                var farPos = new Vector(_farPoint.X, _farPoint.Y);
                far = new RayContact(farPos, Vector.FromAngle(_normalPoint.Y), Vector.Distance(start, farPos));

                slot = _slots[touch as DragonSlot];
                return true;
            }
        }

        public override bool IntersectsSegment(Vector start, Vector end, out RayContact near, out ArmatureSlot slot)
        {
            var touch = _armature.IntersectsSegment(start.X, start.Y, end.X, end.Y, _nearPoint, null, _normalPoint);
            if (touch == null)
            {
                near = default;
                slot = default;
                return false;
            }
            else
            {
                var nearPos = new Vector(_nearPoint.X, _nearPoint.Y);
                near = new RayContact(nearPos, Vector.FromAngle(_normalPoint.X), Vector.Distance(start, nearPos));

                slot = _slots[touch as DragonSlot];
                return true;
            }
        }

        public override bool IntersectsSegment(Vector start, Vector end, out ArmatureSlot slot)
        {
            var touch = _armature.IntersectsSegment(start.X, start.Y, end.X, end.Y, _nearPoint, null, _normalPoint);
            if (touch == null)
            {
                slot = default;
                return false;
            }
            else
            {
                slot = _slots[touch as DragonSlot];
                return true;
            }
        }

        #endregion

        public override void Dispose()
        {
            _armature.Dispose();
        }
    }
}
