namespace Heirloom.Benchmark
{
    public sealed class EmoteIconBenchmark : ParticleBenchmark
    {
        public EmoteIconBenchmark()
            : base("Emote Icons", Assets.LoadImages("files/emotes", true))
        { }
    }
}
