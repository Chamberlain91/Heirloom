using System;
using System.Collections.Generic;

using DBAnimation = DragonBones.Animation;
using DBAnimationState = DragonBones.AnimationState;

namespace Heirloom.Extras.Anim2D
{
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
}
