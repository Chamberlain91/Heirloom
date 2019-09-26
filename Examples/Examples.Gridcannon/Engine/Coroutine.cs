using System;
using System.Collections;
using System.Collections.Generic;

namespace Examples.Gridcannon.Engine
{
    public class Coroutine
    {
        private bool _terminate;

        internal IEnumerator Enumerator;

        internal Stack<IEnumerator> Stack;

        internal Func<bool> Condition;

        internal int DelayFrames;

        internal float Delay;

        internal Coroutine(IEnumerator enumerator, float delay)
        {
            Enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));

            Stack = new Stack<IEnumerator>();

            Condition = null;
            DelayFrames = 0;
            Delay = delay;
        }

        /// <summary>
        /// Terminate this coroutine.
        /// </summary>
        public void Stop()
        {
            _terminate = true;
        }

        internal bool Update(float dt)
        {
            // 
            if (_terminate) { return false; }

            // Wait Seconds
            if (Delay > 0)
            {
                Delay -= dt;
                return true;
            }
            // Wait Frames
            else if (DelayFrames > 0)
            {
                DelayFrames--;
                return true;
            }
            // Wait Condition
            else if (Condition != null)
            {
                if (Condition()) { Condition = null; }
                return true;
            }
            // Process Next Step
            else
            {
                // Move along current enumerator
                if (Enumerator.MoveNext())
                {
                    // Configure Wait Seconds
                    if (Enumerator.Current is CoroutineAction.WaitSeconds waitSeconds)
                    {
                        Delay = waitSeconds.Duration;
                    }
                    // Configure Wait Frames
                    else if (Enumerator.Current is CoroutineAction.WaitFrames waitFrames)
                    {
                        DelayFrames = waitFrames.Duration;
                    }
                    // Configure Wait Condition
                    else if (Enumerator.Current is CoroutineAction.WaitCondition waitCondition)
                    {
                        Condition = waitCondition.Condition;
                    }
                    // Nested Coroutine (as enumerator)
                    else if (Enumerator.Current is IEnumerator enumerator)
                    {
                        Stack.Push(Enumerator);
                        Enumerator = enumerator;
                    }
                    // Nested Coroutine (as enumerable)
                    else if (Enumerator.Current is IEnumerable enumerable)
                    {
                        Stack.Push(Enumerator);
                        Enumerator = enumerable.GetEnumerator();
                    }
                    // Unknown coroutine action
                    else
                    {
                        throw new InvalidOperationException("Unknown yield type in coroutine.");
                    }

                    // Was able to process current step
                    return true;
                }
                // Current enumerator expired, try next enumerator on stack
                else if (Stack.Count > 0)
                {
                    // Pop off stack and attempt to move next there
                    Enumerator = Stack.Pop();
                    return true;
                }
                else
                {
                    // Coroutine expired
                    return false;
                }
            }
        }

        private static readonly CoroutineAction _waitOneFrame = new CoroutineAction.WaitFrames(1);

        /// <summary>
        /// Wait until the start of the next frame to proceed.
        /// </summary>
        public static CoroutineAction WaitNextFrame()
        {
            return WaitFrames(1);
        }

        /// <summary>
        /// Wait some time in seconds before proceeding.
        /// </summary>
        public static CoroutineAction WaitSeconds(float seconds)
        {
            return new CoroutineAction.WaitSeconds(seconds);
        }

        /// <summary>
        /// Wait some number of frames before proceeding.
        /// </summary>
        public static CoroutineAction WaitFrames(int frames)
        {
            if (frames <= 0) { throw new ArgumentOutOfRangeException(nameof(frames)); }

            // Note: returns the constant to avoid allocations
            return frames == 1 ? _waitOneFrame : new CoroutineAction.WaitFrames(frames);
        }

        /// <summary>
        /// Wait until for a certain condition before proceeding.
        /// </summary>
        public static CoroutineAction WaitUtil(Func<bool> condition)
        {
            return new CoroutineAction.WaitCondition(condition);
        }
    }
}
