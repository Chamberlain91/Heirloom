namespace Heirloom.Collections
{
    internal sealed class GraphEdge<TVertex>
    {
        public readonly TVertex A;

        public readonly TVertex B;

        public float Weight;

        public GraphEdge(TVertex a, TVertex b, float weight)
        {
            A = a;
            B = b;

            Weight = weight;
        }

        public TVertex GetOther(TVertex x)
        {
            return Equals(A, x) ? B : A;
        }
    }
}
