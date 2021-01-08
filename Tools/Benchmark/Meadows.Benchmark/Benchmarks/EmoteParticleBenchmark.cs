namespace Meadows.Benchmark
{
    public sealed class EmoteParticleBenchmark : ParticleBenchmark
    {
        public EmoteParticleBenchmark()
            : base("Emote Icons", LoadImages("files/emotes/*"))
        { }
    }
}
