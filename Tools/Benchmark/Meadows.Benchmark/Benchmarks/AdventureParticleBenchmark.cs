namespace Meadows.Benchmark
{
    public sealed class AdventureParticleBenchmark : ParticleBenchmark
    {
        public AdventureParticleBenchmark()
            : base("Adventure Sprites", LoadImages("files/adventure/*"))
        { }
    }
}
