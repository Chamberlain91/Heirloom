namespace Meadows.Benchmark
{
    public sealed class EmoteIconBenchmark : ParticleBenchmark
    {
        public EmoteIconBenchmark()
            : base("Emote Icons", 4096, Assets.LoadImages("files/emotes", true))
        { }
    }
}
