using System;

namespace Heirloom.Game
{
    public abstract class CoroutineAction
    {
        internal CoroutineAction() { }

        internal class WaitFrames : CoroutineAction
        {
            public readonly int Duration;

            internal WaitFrames(int duration)
            {
                Duration = duration;
            }
        }

        internal class WaitSeconds : CoroutineAction
        {
            public readonly float Duration;

            internal WaitSeconds(float duration)
            {
                Duration = duration;
            }
        }

        internal class WaitCondition : CoroutineAction
        {
            public readonly Func<bool> Condition;

            internal WaitCondition(Func<bool> condition)
            {
                Condition = condition;
            }
        }
    }
}
