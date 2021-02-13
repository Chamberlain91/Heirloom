using DBAnimationState = DragonBones.AnimationState;
// using Transform = Heirloom.Mathematics.Transform;

namespace Heirloom.Extras.Anim2D
{
    internal sealed class DragonBonesAnimationState : AnimationState
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
}
