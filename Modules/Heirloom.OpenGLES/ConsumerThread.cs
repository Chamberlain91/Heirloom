using System;
using System.Collections.Generic;
using System.Threading;

namespace Heirloom.OpenGLES
{
    /// <summary>
    /// This object processes jobs on a its own specific thread.
    /// </summary>
    internal sealed class ConsumerThread
    {
        private readonly Queue<Action> _queue;
        private readonly Thread _thread;
        private bool _isAlive;

        public ConsumerThread(string name)
        {
            // 
            _queue = new Queue<Action>();

            // Create new background thread
            _thread = new Thread(ThreadAction)
            {
                IsBackground = true,
                Name = name
            };
        }

        /// <summary>
        /// Gets the number of pending jobs.
        /// </summary>
        public int Pending => _queue.Count;

        public event Action Exiting;

        private void ThreadAction()
        {
            // Set to highest priority
            // Thread.CurrentThread.Priority = ThreadPriority.Highest;

            // 
            while (_isAlive)
            {
                lock (_queue)
                {
                    // Wait for jobs (and still alive)
                    while (_queue.Count == 0 && _isAlive)
                    {
                        Monitor.Wait(_queue);
                    }

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

            Exiting?.Invoke();
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

                    // Signal we have work (unblocks the waiting thread)
                    if (_queue.Count == 1)
                    {
                        Monitor.Pulse(_queue);
                    }
                }
            }
        }

        public void Start()
        {
            _isAlive = true;
            _thread.Start();
        }

        public void Stop()
        {
            lock (_queue)
            {
                _isAlive = false;
                Monitor.Pulse(_queue);
            }
        }
    }
}
