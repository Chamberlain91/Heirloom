// using Transform = Heirloom.Mathematics.Transform;

namespace Heirloom.Extras.Anim2D
{
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
}
