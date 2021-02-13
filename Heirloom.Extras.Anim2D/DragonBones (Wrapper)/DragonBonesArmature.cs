using System;
using System.Collections.Generic;

using DragonBones;

using Heirloom.Drawing;
using Heirloom.Mathematics;

using DBArmature = DragonBones.Armature;
using DragonBone = DragonBones.Bone;

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
        private readonly Dictionary<DragonBone, DragonBonesArmatureBone> _bones;

        public DragonBonesArmature(DragonBonesArmaturePackage armature, string name)
            : base(armature)
        {
            // Build dragon bones armature representation
            _armature = DragonFactory.Factory.BuildArmature(name, armature.Identifier);

            // Register event listeners
            // todo: only register (and remove) iif the event properties are used
            _armature.EventDispatcher.AddDBEventListener(EventObject.START, OnEvent);
            _armature.EventDispatcher.AddDBEventListener(EventObject.COMPLETE, OnEvent);
            _armature.EventDispatcher.AddDBEventListener(EventObject.LOOP_COMPLETE, OnEvent);
            _armature.EventDispatcher.AddDBEventListener(EventObject.FRAME_EVENT, OnEvent); // Custom
            _armature.EventDispatcher.AddDBEventListener(EventObject.SOUND_EVENT, OnEvent); // Sound

            // Map internal slots public counterparts
            _slots = new Dictionary<DragonSlot, DragonBonesArmatureSlot>();
            foreach (DragonSlot slot in _armature.GetSlots())
            {
                _slots[slot] = new DragonBonesArmatureSlot(slot);
            }

            // Map internal bones to public counterparts
            _bones = new Dictionary<DragonBone, DragonBonesArmatureBone>();
            foreach (var bone in _armature.GetBones())
            {
                _bones[bone] = new DragonBonesArmatureBone(this, bone);
            }

            // Create animation wrapper
            Animation = new DragonBonesAnimationPlayer(_armature.Animation);
        }

        ~DragonBonesArmature()
        {
            Dispose(false);
        }

        private void OnEvent(string type, EventObject ev)
        {
            LogEvent(ev);
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
            // AnimationStarted?.Invoke(this)
        }

        private void OnCompleteEvent(EventObject ev, bool isLoop)
        {
            // AnimationCompleted?.Invoke(this, isLoop)
        }

        private void OnFrameEvent(EventObject ev)
        {
            // Invoke FrameEvent?.Invoke(ev.name, ...data, bone, time?)
        }

        private void OnSoundEvent(EventObject ev)
        {
            // Invoke SoundEvent?.Invoke(ev.name, ...time?)
        }

        private static void LogEvent(EventObject ev)
        {
            Log.Debug($"EVENT {ev.type.ToUpper()}");
            Log.Debug($"         State: {ev.animationState.name}");
            if (ev.actionData != null) { Log.Debug($"         Event: {ev.name} @ {ev.time:N2} seconds ({ev.actionData.type})"); }
            if (ev.slot != null) { Log.Debug($"          Slot: {ev.slot}"); }
            if (ev.bone != null) { Log.Debug($"          Bone: {ev.bone.BoneData.name}"); }
            if (ev.data != null)
            {
                if (ev.data.strings.Count > 0) { Log.Debug($"          Data: '{ev.data.strings[0]}'"); }
                if (ev.data.floats.Count > 0) { Log.Debug($"          Data: {ev.data.floats[0]}"); }
                if (ev.data.ints.Count > 0) { Log.Debug($"          Data: {ev.data.ints[0]}"); }
            }
        }

        public override IReadOnlyCollection<ArmatureSlot> Slots => _slots.Values;

        public override IReadOnlyCollection<ArmatureBone> Bones => _bones.Values;

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

        public override ArmatureSlot GetSlot(string name)
        {
            if (_armature.GetSlot(name) is DragonSlot slot) { return _slots[slot]; }
            else { return null; }
        }

        public override ArmatureBone GetBone(string name)
        {
            if (_armature.GetBone(name) is DragonBone bone) { return _bones[bone]; }
            else { return null; }
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

        private void Dispose(bool disposing)
        {
            Log.Warning("Dispose!");
            _armature.Proxy.Dispose(true);
            _armature.Dispose();
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
    }
}
