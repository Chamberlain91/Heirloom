using System.Collections.Generic;

namespace Heirloom.Extras.Animation
{
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
}
