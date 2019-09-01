using System;

namespace Heirloom.Examples.CardMark
{
    internal sealed class Heartbeat
    {
        public Action Action;

        public float Interval;

        public float Time;

        public Heartbeat(Action action, float interval)
        {
            Action = action;
            Interval = interval;
            Time = 0;
        }

        public void Update(float delta)
        {
            Time += delta;

            // Process actions for elapsed time
            while (Time > Interval)
            {
                Time -= Interval;
                Action();
            }
        }
    }
}
