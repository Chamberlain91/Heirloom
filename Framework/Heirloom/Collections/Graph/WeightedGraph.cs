namespace Heirloom.Collections
{
    public sealed class WeightedGraph<V> : Graph<V, float>
    {
        public WeightedGraph(bool directed = false)
            : base(directed, 1F)
        { }
    }
}
