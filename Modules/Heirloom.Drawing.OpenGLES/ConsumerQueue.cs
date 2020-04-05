using System;
using System.Collections.Generic;
using System.Threading;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ConsumerQueue
    {
        private readonly Queue<Action> _queue;
        private readonly Thread _thread;

        public ConsumerQueue()
            : this(Thread.CurrentThread)
        { }

        public ConsumerQueue(Thread thread)
        {
            _queue = new Queue<Action>();
            _thread = thread;
        }

        public int Pending => _queue.Count;

        public void ProcessJobs()
        {
            if (Thread.CurrentThread != _thread)
            {
                throw new InvalidOperationException("Unable to process jobs on a different thread.");
            }

            lock (_queue)
            {
                // Process all jobs
                while (_queue.Count > 0)
                {
                    var action = _queue.Dequeue();

                    lock (action)
                    {
                        action(); // 
                        Monitor.PulseAll(action);
                    }
                }
            }
        }

        public void Invoke(Action action)
        {
            // Execute now if already on the thread
            if (Thread.CurrentThread == _thread) { action(); }
            else
            {
                lock (action)
                {
                    // Schedule action
                    InvokeLater(action);

                    // Wait for action to complete
                    Monitor.Wait(action);
                }
            }
        }

        public T Invoke<T>(Func<T> action)
        {
            // Execute now if already on the thread
            if (Thread.CurrentThread == _thread) { return action(); }
            else
            {
                var retval = default(T);

                Invoke(() =>
                {
                    retval = action();
                    return;
                });

                return retval;
            }
        }

        public void InvokeLater(Action action)
        {
            // Execute now if already on the thread
            if (Thread.CurrentThread == _thread) { action(); }
            else
            {
                lock (_queue)
                {
                    // Schedule for later
                    _queue.Enqueue(action);
                }
            }
        }
    }
}
