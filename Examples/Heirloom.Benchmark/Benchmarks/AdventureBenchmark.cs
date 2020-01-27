namespace Heirloom.Benchmark
{
    public sealed class AdventureBenchmark : ParticleBenchmark
    {
        public AdventureBenchmark()
            : base("Adventure Sprites", Assets.LoadImages("files/adventure", true))
        { }
    }
}
