using System;
using System.Collections.Generic;
using System.Threading;

namespace Heirloom.Platforms.Desktop
{
    internal sealed class AsyncRunnerThread
    {
        private readonly Queue<Action> _taskQueue;
        private readonly Thread _thread;

        public AsyncRunnerThread()
        {
            _taskQueue = new Queue<Action>();
            _thread = new Thread(ThreadBody);
        }

        public event Action Initialized;

        public event Action Shutdown;

        public bool IsAlive { get; private set; }

        public int ManagedThreadId => _thread.ManagedThreadId;

        public bool IsBackground
        {
            get => _thread.IsBackground;
            set => _thread.IsBackground = value;
        }

        public string Name
        {
            get => _thread.Name;
            set => _thread.Name = value;
        }

        private void ThreadBody()
        {
            // Initialize
            IsAlive = true;

            // Initialize
            lock (_thread)
            {
                Initialized?.Invoke();
                Monitor.PulseAll(_thread);
            }

            // Main Loop
            while (IsAlive)
            {
                var jobsCount = _taskQueue.Count;

                // Process all delegates
                while (jobsCount > 0)
                {
                    var action = _taskQueue.Dequeue();

                    lock (action)
                    {
                        action(); // execute task 
                        Monitor.PulseAll(action);
                    }

                    jobsCount--;
                }

                // Yield to prevent spinning too tightly
                Thread.Yield();
            }

            //
            FalseNotifyRemainingJobs();

            // 
            Shutdown?.Invoke();
        }

        // 
        public void Invoke(Action action)
        {
            // Execute now if already on the thread
            if (Thread.CurrentThread == _thread) { action(); }
            else
            {
                if (!IsAlive)
                {
                    // throw new InvalidOperationException("Unable to schedule invoke, thread not alive.");
                    return; // just can't do it...
                }

                lock (action)
                {
                    // Schedule task
                    _taskQueue.Enqueue(action);

                    // Wait for action to complete
                    Monitor.Wait(action);
                }
            }
        }

        public T Invoke<T>(Func<T> action)
        {
            var retval = default(T);

            Invoke(() =>
            {
                retval = action();
                return;
            });

            return retval;
        }

        // 
        public void InvokeLater(Action action)
        {
            // Execute now if already on the thread
            if (Thread.CurrentThread == _thread) { action(); }
            else
            {
                if (!IsAlive)
                {
                    // throw new InvalidOperationException("Unable to schedule invoke, thread not alive.");
                    return; // just can't do it...
                }

                lock (action)
                {
                    // Schedule task
                    _taskQueue.Enqueue(action);
                }
            }
        }

        // 
        public void Start()
        {
            lock (_thread)
            {
                _thread.Start();
                Monitor.Wait(_thread);
            }
        }

        public void Join(int timeout)
        {
            _thread.Join(timeout);
        }

        public void Join()
        {
            _thread.Join();
        }

        public void Abort()
        {
            // Forceful termination of thread
            _thread.Abort();

            // 
            FalseNotifyRemainingJobs();
        }

        private void FalseNotifyRemainingJobs()
        {
            if (_taskQueue.Count > 0)
            {
                Console.WriteLine($"False notification of {_taskQueue.Count} jobs that have not yet executed.");

                foreach (var action in _taskQueue)
                {
                    lock (action)
                    {
                        Monitor.PulseAll(action);
                    }
                }

                _taskQueue.Clear();
            }
        }

        public void Stop(bool join = true)
        {
            IsAlive = false;

            if (join)
            {
                Join();
            }
        }
    }
}
