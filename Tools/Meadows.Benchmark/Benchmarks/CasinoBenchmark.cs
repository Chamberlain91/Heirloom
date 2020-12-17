namespace Meadows.Benchmark
{
    public sealed class CasinoBenchmark : ParticleBenchmark
    {
        public CasinoBenchmark()
            : base("Casino", 512, Assets.LoadImages("files/casino"))
        { }
    }
}
