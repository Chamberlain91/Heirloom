using System;
using System.Collections.Generic;
using System.IO;

using DragonBones;

using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Mathematics;

using DBAnimation = DragonBones.Animation;
using DBAnimationState = DragonBones.AnimationState;
using DBArmature = DragonBones.Armature;
using DBSlot = DragonBones.Slot;
// using Transform = Heirloom.Mathematics.Transform;
using Matrix = Heirloom.Mathematics.Matrix;

namespace Heirloom.Extras.Anim2D
{
    public abstract class Armature : IDisposable
    {
        internal Armature(ArmatureData armature)
        {
            ArmatureData = armature ?? throw new ArgumentNullException(nameof(armature));
        }

        public ArmatureData ArmatureData { get; }

        public abstract AnimationPlayer Animation { get; }

        public abstract IReadOnlyCollection<ArmatureSlot> Slots { get; }

        public abstract bool FlipX { get; set; }

        public abstract bool FlipY { get; set; }

        public abstract void Draw(GraphicsContext gfx, Matrix matrix);

        public abstract void AdvanceTime(float dt);

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

    public abstract class ArmatureData : IDisposable
    {
        private bool _disposedValue;

        ~ArmatureData()
        {
            Dispose(disposing: false);
        }

        public abstract Armature CreateArmature();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // 
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public static ArmatureData LoadDragonBones(string path)
        {
            return DragonBonesArmatureData.Load(path);
        }

        public static ArmatureData LoadDragonBones(string skePath, string texPath, string imgPath)
        {
            return DragonBonesArmatureData.Load(skePath, texPath, imgPath);
        }
    }

    #region Animation State

    public abstract class AnimationState
    {
        public abstract void Play();
        public abstract void Stop();

        public abstract void AddBoneMask(string name, bool recursive = true);
        public abstract void RemoveBoneMask(string name, bool recursive = true);
        public abstract void ClearBoneMasks();
        public abstract bool HasBoneMask(string name);

        public abstract float TimeScale { get; set; }

        public abstract float CurrentTime { get; set; }

        public abstract float Duration { get; }

        public abstract bool IsPlaying { get; }

        public abstract bool IsCompleted { get; }

        public abstract int LoopCount { get; }

        public abstract bool EnableActions { get; set; }
    }

    public sealed class DragonBonesAnimationState : AnimationState
    {
        internal DBAnimationState State;

        public override void Play()
        {
            State.Play();
        }

        public override void Stop()
        {
            State.Stop();
        }

        public override void AddBoneMask(string name, bool recursive = true)
        {
            State.AddBoneMask(name, recursive);
        }

        public override void RemoveBoneMask(string name, bool recursive = true)
        {
            State.RemoveBoneMask(name, recursive);
        }

        public override void ClearBoneMasks()
        {
            State.RemoveAllBoneMask();
        }

        public override bool HasBoneMask(string name)
        {
            return State.ContainsBoneMask(name);
        }

        public override float TimeScale { get => State.timeScale; set => State.timeScale = value; }

        public override float CurrentTime { get => State.currentTime; set => State.currentTime = value; }

        public override float Duration => State._duration;

        public override bool IsPlaying => State.isPlaying;

        public override bool IsCompleted => State.isCompleted;

        public override int LoopCount => State.playTimes;

        public override bool EnableActions { get => State.actionEnabled; set => State.actionEnabled = value; }
    }

    #endregion

    #region Animation Player

    public abstract class AnimationPlayer
    {
        public abstract float TimeScale { get; set; }

        public abstract bool IsPlaying { get; }

        public abstract bool IsCompleted { get; }

        public abstract AnimationState LastAnimation { get; }

        public abstract IReadOnlyList<string> Animations { get; }

        public abstract void Reset();

        public abstract bool HasAnimation(string name);

        // begin playback of the specified animation
        public abstract AnimationState Play(string animation, int loopCount = -1);

        // begin playback of the specified animation at the given time in seconds
        public abstract AnimationState PlayAtTime(string animation, float time, int loopCount = -1);

        // pause the specified animation (or all if null)
        public abstract void Stop(string animation = null);

        // goes to the specific time and then pauses
        public abstract void StopAtTime(string animation, float time);
    }

    internal sealed class DragonBonesAnimationPlayer : AnimationPlayer
    {
        private readonly DBAnimation _animation;
        private readonly Dictionary<string, DragonBonesAnimationState> _states;

        public DragonBonesAnimationPlayer(DBAnimation animation)
        {
            _animation = animation ?? throw new ArgumentNullException(nameof(animation));

            // Map internal states to public animation states
            _states = new Dictionary<string, DragonBonesAnimationState>();
            foreach (var name in _animation.AnimationNames)
            {
                _states[name] = new DragonBonesAnimationState();
            }
        }

        public override float TimeScale { get => _animation.TimeScale; set => _animation.TimeScale = value; }

        public override bool IsPlaying => _animation.IsPlaying;

        public override bool IsCompleted => _animation.IsCompleted;

        public override AnimationState LastAnimation => _states[_animation.LastAnimationName];

        public override IReadOnlyList<string> Animations => _animation.AnimationNames;

        public override void Reset()
        {
            _animation.Reset();
        }

        public override bool HasAnimation(string name)
        {
            return _animation.HasAnimation(name);
        }

        private DragonBonesAnimationState GetState(DBAnimationState state)
        {
            //if (_states.TryGetValue(state._animationData.name, out var _state) == false)
            //{
            //    _state = new DragonBonesAnimationState();
            //}

            var _state = _states[state._animationData.name];
            _state.State = state;
            return _state;
        }

        // loopCount, -1 default, 0 infinite, otherwise finite count
        public override AnimationState Play(string animation, int loopCount = -1)
        {
            var state = _animation.Play(animation, loopCount);
            return GetState(state);
        }

        // loopCount, -1 default, 0 infinite, otherwise finite count
        public override AnimationState PlayAtTime(string animation, float time, int loopCount = -1)
        {
            var state = _animation.GotoAndPlayByTime(animation, time, loopCount);
            return GetState(state);
        }

        // if null, stops all animations
        // without blending, there might always only be one animation.
        public override void Stop(string animation = null)
        {
            _animation.Stop(animation);
        }

        public override void StopAtTime(string animation, float time)
        {
            _animation.GotoAndStopByTime(animation, time);
        }
    }

    #endregion

    #region Armature Slot

    public abstract class ArmatureSlot
    {
        public abstract void Draw(GraphicsContext gfx, Matrix matrix);

        public abstract bool IsVisible { get; set; }
    }

    internal sealed class DragonBonesArmatureSlot : ArmatureSlot
    {
        private readonly AnimSlot _slot;

        public DragonBonesArmatureSlot(AnimSlot slot)
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

    internal sealed class DragonBonesArmature : Armature
    {
        private static readonly Point _normalPoint = new Point();
        private static readonly Point _nearPoint = new Point();
        private static readonly Point _farPoint = new Point();

        private readonly DBArmature _armature;
        private readonly Dictionary<AnimSlot, DragonBonesArmatureSlot> _slots;

        public DragonBonesArmature(DragonBonesArmatureData armature)
            : base(armature)
        {
            // todo: should we error if there is more than one?
            // todo: how to handle more than one properly...
            var armatureName = armature.Data.armatureNames[0];
            _armature = AnimFactory.Factory.BuildArmature(armatureName, armature.Identifier);

            // Map internal slots public counterparts
            _slots = new Dictionary<AnimSlot, DragonBonesArmatureSlot>();
            foreach (AnimSlot slot in _armature.GetSlots())
            {
                _slots[slot] = new DragonBonesArmatureSlot(slot);
            }

            // Create animation wrapper
            Animation = new DragonBonesAnimationPlayer(_armature.Animation);
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
                slot = _slots[touch as AnimSlot];
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

                slot = _slots[touch as AnimSlot];
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

                slot = _slots[touch as AnimSlot];
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
                slot = _slots[touch as AnimSlot];
                return true;
            }
        }

        #endregion

        public override void Dispose()
        {
            _armature.Dispose();
        }
    }

    internal sealed class DragonBonesArmatureData : ArmatureData
    {
        private static uint _counter;

        internal readonly TextureAtlasData AtlasData;
        internal readonly DragonBonesData Data;

        internal readonly string Identifier;

        private bool _disposedValue;

        public DragonBonesArmatureData(string name, DragonBonesData data, TextureAtlasData atlasData)
        {
            Identifier = name ?? throw new ArgumentNullException(nameof(name));
            AtlasData = atlasData ?? throw new ArgumentNullException(nameof(atlasData));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public override Armature CreateArmature()
        {
            return new DragonBonesArmature(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!_disposedValue)
            {
                // Remove animation data
                AnimFactory.Factory.RemoveTextureAtlasData(Identifier);
                AnimFactory.Factory.RemoveDragonBonesData(Identifier);

                // This skeleton has been disposed
                _disposedValue = true;
            }
        }

        #region Load Dragon Bones (Factory)

        internal static ArmatureData Load(string ske_json)
        {
            if (!Files.Exists(ske_json)) { throw new FileNotFoundException($"Unable to find dragonbones json '{ske_json}'"); }

            if (ske_json.EndsWith("_ske.json"))
            {
                var root = ske_json[0..^9];
                var tex_json = $"{root}_tex.json";
                var tex_png = $"{root}_tex.png";

                if (!Files.Exists(tex_json)) { throw new FileNotFoundException($"Unable to find required dragonbones file: '{tex_json}'"); }
                if (!Files.Exists(tex_png)) { throw new FileNotFoundException($"Unable to find required dragonbones file: '{tex_png}'"); }

                return Load(ske_json, tex_json, tex_png);
            }
            else
            {
                throw new ArgumentException($"Unable to load dragonbones asset.");
            }
        }

        internal static ArmatureData Load(string skeletonPath, string atlasPath, string imagePath)
        {
            // Generate a unique name for this armature
            var name = $"dragonbones_{_counter++}";

            // load and parse bone data
            var bonesJson = Files.ReadText(skeletonPath);
            var bonesJsonData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(bonesJson);
            var bonesData = AnimFactory.Factory.ParseDragonBonesData(bonesJsonData, name);

            // load and parse atlas data
            var atlasJson = Files.ReadText(atlasPath);
            var atlasJsonData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(atlasJson);
            var atlasData = AnimFactory.Factory.ParseTextureAtlasData(atlasJsonData, new Image(imagePath), name);

            // Create
            return new DragonBonesArmatureData(name, bonesData, atlasData);
        }

        #endregion
    }
}
