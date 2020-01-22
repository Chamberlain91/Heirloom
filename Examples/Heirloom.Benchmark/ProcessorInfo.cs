namespace Heirloom.Benchmark
{
    public readonly struct ProcessorInfo
    {
        public readonly string Name;
     
        public readonly string ClockSpeed;
        
        public readonly int CoreCount;

        public ProcessorInfo(string name, string clockSpeed, int coreCount)
        {
            Name = name;
            ClockSpeed = clockSpeed;
            CoreCount = coreCount;
        }

        public static ProcessorInfo Unknown { get; } = new ProcessorInfo("Unknown", "Unknown", -1);
    }
}
