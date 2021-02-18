using System;
using System.Collections;
using System.Collections.Generic;

using Heirloom.Utilities;

namespace Heirloom.Extras.Animation
{
    public static class Clock
    {
        private static readonly List<ClockService> _services = new List<ClockService>();

        private static readonly CoroutineClockService _coroutine;
        private static readonly IntervalClockService _interval;
        private static readonly TimeoutClockService _timeout;

        private static float _timeScale;

        static Clock()
        {
            // Allocate all discoverable clock environments
            foreach (var type in ReflectionHelper.GetSubclassTypes<ClockService>())
            {
                var instance = Activator.CreateInstance(type) as ClockService;
                _services.Add(instance);
            }

            // Sort environments by priority
            _services.StableSort();

            // Get static references for faster access
            _coroutine = GetClockEnvironment<CoroutineClockService>();
            _interval = GetClockEnvironment<IntervalClockService>();
            _timeout = GetClockEnvironment<TimeoutClockService>();
        }

        /// <summary>
        /// The time rate multiplier, used to control global animation speed and other clock features.
        /// </summary>
        public static float TimeScale
        {
            get => _timeScale;
            set
            {
                _timeScale = value;
                foreach (var service in _services)
                {
                    service.TimeScale = value;
                    service.OnTimeScaleChanged();
                }
            }
        }

        /// <summary>
        /// The time (in seconds) between subsequent calls to <see cref="Update(float)"/>.
        /// </summary>
        public static float Delta { get; private set; }

        /// <summary>
        /// The time since application start.
        /// </summary>
        public static float Time { get; private set; }

        /// <summary>
        /// Advances the clock by <paramref name="delta"/> seconds and all instances <see cref="ClockService"/> (in priority order).
        /// </summary>
        public static void Update(float delta)
        {
            // Accumulate time
            Delta = delta * TimeScale;
            Time += Delta;

            // Update clock services
            foreach (var clock in _services)
            {
                clock.Update(delta);
            }
        }

        /// <summary>
        /// Gets the instance of some <see cref="ClockService"/>.
        /// </summary>
        public static TClockService GetClockEnvironment<TClockService>() where TClockService : ClockService
        {
            foreach (var clock in _services)
            {
                if (clock is TClockService service)
                {
                    return service;
                }
            }

            return default;
        }

        #region Coroutine Service

        public static uint StartCoroutine(IEnumerator coroutine)
        {
            return _coroutine.Start(coroutine);
        }

        public static void StopCoroutine(uint handle)
        {
            _coroutine.Stop(handle);
        }

        private sealed class CoroutineClockService : ClockService
        {
            public CoroutineClockService()
                : base(ClockServicePriority.Coroutines - 0)
            { }

            protected internal override void Update(float dt)
            {
                // Nothing (Yet)
            }

            internal uint Start(IEnumerator coroutine)
            {
                throw new NotImplementedException();
            }

            internal void Stop(uint handle)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Interval Service

        public static uint StartInterval(IEnumerator coroutine, float duration)
        {
            return _interval.Start(coroutine, duration);
        }

        public static void StopInterval(uint handle)
        {
            _interval.Stop(handle);
        }

        private sealed class IntervalClockService : ClockService
        {
            public IntervalClockService()
                : base(ClockServicePriority.Coroutines - 1)
            { }

            protected internal override void Update(float dt)
            {
                // Nothing (Yet)
            }

            internal uint Start(IEnumerator coroutine, float duration)
            {
                throw new NotImplementedException();
            }

            internal void Stop(uint handle)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Timeout Service

        public static uint StartTimeout(IEnumerator coroutine, float delay)
        {
            return _timeout.Start(coroutine);
        }

        public static void StopTimeout(uint handle)
        {
            _timeout.Stop(handle);
        }

        private sealed class TimeoutClockService : ClockService
        {
            public TimeoutClockService()
                : base(ClockServicePriority.Coroutines - 2)
            { }

            protected internal override void Update(float dt)
            {
                // Nothing (Yet)
            }

            internal uint Start(IEnumerator coroutine)
            {
                throw new NotImplementedException();
            }

            internal void Stop(uint handle)
            {
                throw new NotImplementedException();
            }
        }

        #endregion 
    }
}
