using System;

namespace Heirloom.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = Hardware.GetProcessorInfo();
            Console.WriteLine($"Name: {info.Name}");
            Console.WriteLine($"{info.ProcessorCount} Logical Processors @ {info.ClockSpeed / 1000:N1}GHz");
        }
    }
}
