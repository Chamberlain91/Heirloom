namespace Heirloom.Collections
{
    internal sealed class Edge<T>
    {
        public readonly T A;
        public readonly T B;

        public float Weight;

        public Edge(T a, T b, float weight)
        {
            A = a;
            B = b;

            Weight = weight;
        }

        public T GetOther(T x)
        {
            return Equals(A, x) ? B : A;
        }
    }
}
