using System;

namespace Heirloom.Extras.Animation
{
    public abstract class ClockService : IComparable<ClockService>
    {
        internal readonly int Priority;

        public float TimeScale { get; internal set; }

        #region Constructors

        /// <summary>
        /// Creates a clock environment with the specified priority.
        /// </summary> 
        protected ClockService(ClockServicePriority priority = ClockServicePriority.Default)
            : this((int) priority)
        { }

        /// <summary>
        /// Creates a clock environment with the specified priority.
        /// </summary>
        /// <param name="priority">Higher value are processed before lower values.</param>
        protected ClockService(int priority)
        {
            Priority = priority;
        }

        #endregion

        protected internal abstract void Update(float dt);

        protected internal virtual void OnTimeScaleChanged()
        {
            // Does nothing by default
        }

        int IComparable<ClockService>.CompareTo(ClockService other)
        {
            return -Priority.CompareTo(other.Priority);
        }
    }
}
