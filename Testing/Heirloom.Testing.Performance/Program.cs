using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

using Heirloom.Mathematics;

namespace Heirloom.Testing.Performance
{
    internal sealed class Program
    {
        internal static void Main(string[] args)
        {
            PerformTests(Assembly.GetEntryAssembly());
        }

        private static void PerformTests(Assembly assembly)
        {
            // Set maximum thread priority
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            foreach (var type in GetSubclassTypes<PerformanceTest>(assembly))
            {
                // Allocate instance
                using (var instance = Activator.CreateInstance(type) as PerformanceTest)
                {
                    Console.WriteLine($"- {type.Name} -");

                    // Scan for methods
                    var actions = new List<(string, Action)>();
                    foreach (var method in type.GetMethods())
                    {
                        var attr = method.GetCustomAttribute<TestAttribute>();
                        if (attr != null && method.GetParameters().Length == 0)
                        {
                            // Append action to action list
                            actions.Add((method.Name, method.CreateDelegate<Action>(instance)));
                        }
                    }

                    var results = new List<float>();

                    // For each test action...
                    foreach (var (name, action) in actions)
                    {
                        const int IterationLimit = 100_000;
                        const int RunLimit = 10;

                        // Clear prior action results
                        results.Clear();

                        // Warm Up
                        WarmUp(action, IterationLimit);

                        // Perform Test
                        for (var c = 0; c < RunLimit; c++)
                        {
                            // Perform action many many times
                            var stopwatch = Stopwatch.StartNew();
                            RepeatAction(action, IterationLimit);
                            stopwatch.Stop();

                            // Compute average time
                            var averageTime = (float) (stopwatch.Elapsed.TotalSeconds / IterationLimit);
                            results.Add(averageTime);

                            // Force absolute collection, now. We don't want GC to wander into the next iteration.
                            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
                        }

                        // Report
                        var statistics = Statistics.Compute(results);
                        Console.WriteLine($"{name,-30}: {Time.GetEnglishTime(statistics.Mean)} ± {Time.GetEnglishTime(statistics.Deviation)}");
                    }

                    // Just to space out test groups
                    Console.WriteLine();
                }

                // Force absolute collection, now. We don't want GC to wander into the next test.
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            }
        }

        private static void WarmUp(Action action, int iterationLimit)
        {
            // Perform the action many times
            RepeatAction(action, iterationLimit);

            // Force absolute collection, now. We don't want GC to wander into the next iteration.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);

            // Give some time for JIT
            Thread.Sleep(200);
        }

        private static void RepeatAction(Action action, int count)
        {
            for (var i = 0; i < count; i++)
            {
                action();
            }
        }

        private static IEnumerable<TypeInfo> GetSubclassTypes<T>(Assembly assembly)
        {
            var baseType = typeof(T);
            foreach (var type in assembly.DefinedTypes)
            {
                if (type.IsSubclassOf(baseType))
                {
                    yield return type;
                }
            }
        }
    }

    public abstract class PerformanceTest : IDisposable
    {
        public virtual void Dispose() { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class TestAttribute : Attribute { }
}
