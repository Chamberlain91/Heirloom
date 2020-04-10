namespace Heirloom.Benchmark
{
    public sealed class AdventureBenchmark : ParticleBenchmark
    {
        public AdventureBenchmark()
            : base("Adventure Sprites", 512, Assets.LoadImages("files/adventure", true))
        { }
    }
}
