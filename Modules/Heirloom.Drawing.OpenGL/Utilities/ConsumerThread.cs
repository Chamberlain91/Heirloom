using System;
using System.Collections.Generic;
using System.Threading;

namespace Heirloom.Drawing.OpenGL.Utilities
{
    /// <summary>
    /// Object to schedule actions onto a dedicated thread, useful for OpenGL as
    /// all operations must be called on a dedicated thread.
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

        public int Pending => _queue.Count;

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

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} Exit.");
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
            // Start task thread 
            _isAlive = true;
            _thread.Start();
        }

        public void Stop(bool falseCompletion = false)
        {
            // Mark thead for death
            lock (_queue)
            {
                _isAlive = false;
                Monitor.Pulse(_queue);
            }

            // Wait for death
            _thread.Join();

            // 
            if (_queue.Count > 0)
            {
                if (falseCompletion)
                {
                    foreach (var action in _queue)
                    {
                        lock (action)
                        {
                            Monitor.PulseAll(action);
                        }
                    }

                    Console.WriteLine($"WARNING: False notification sent to {_queue.Count} jobs that have not yet executed.");
                }
                else
                {
                    Console.WriteLine($"WARNING: {_queue.Count} jobs that have not yet executed.");
                }

                // Empty queue (to release references)
                _queue.Clear();
            }
        }
    }
}
