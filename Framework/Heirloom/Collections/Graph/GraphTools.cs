using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public static class GraphTools
    {
        //#region Page Rank

        //public static IReadOnlyDictionary<V, float> ComputePageRank<V>(this WeightedGraph<V> graph)
        //{
        //    return ComputePageRank(graph, weight => weight);
        //}

        //public static IReadOnlyDictionary<V, float> ComputePageRank<V, E>(this Graph2<V, E> graph, Func<E, float> getEdgeWeight)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region Community Detection

        //public abstract IEnumerable<ICollection<V>> DetectCommunities(); // streaming?, what algorithm?

        //#endregion

        //#region Find Path (Heuristic Search, A*)

        //public IReadOnlyList<V> FindPath(V start, Func<V, bool> goalCondition, HeuristicCost<V> heuristic)
        //{
        //    return FindPath(start, goalCondition, GetEdgeProperty, heuristic);
        //}

        //public IReadOnlyList<V> FindPath(V start, V goal, HeuristicCost<V> heuristic)
        //{
        //    return FindPath(start, c => Equals(goal), GetEdgeProperty, heuristic);
        //}

        //public IReadOnlyList<V> FindPath(V start, V goal)
        //{
        //    // Causes A* to degenerate to Djikstras?
        //    return FindPath(start, goal, GetEdgeProperty, x => 0F);
        //}

        //#endregion

        #region Minimum Spanning (Tree/Arborescence)

        /// <summary>
        /// Computes the minimum spanning tree/arborescence of the graph.
        /// </summary>
        /// <returns>Implemented with ??? (for directed) or Prims/Jarniks (for undirected) algorithm.</returns>
        public static Graph<V, W> GetMinimumSpanning<V, W>(this Graph<V, W> graph) where W : struct, IComparable
        {
            if (graph.IsDirected)
            {
                return GetMinimumSpanningArborescence(graph);
            }
            else
            {
                return GetMinimumSpanningTree(graph);
            }
        }

        private static Graph<V, W> GetMinimumSpanningArborescence<V, W>(Graph<V, W> graph) where W : struct, IComparable
        {
            throw new NotImplementedException();
        }

        // todo: confirm is Jarniks (and time complexity)
        private static Graph<V, W> GetMinimumSpanningTree<V, W>(Graph<V, W> graph) where W : struct, IComparable
        {
            // todo: recycle/optimize use of C E and Q?
            var C = new Dictionary<V, W>();
            var E = new Dictionary<V, V>();
            var Q = new Heap<V>((a, b) => C[a].CompareTo(C[b]));

            // Construct output graph (ie, the forest)
            var F = new Graph<V, W>();

            // Initialize
            foreach (var v in graph.Vertices)
            {
                // C[v] = float.PositiveInfinity;
                Q.Add(v);
            }

            while (Q.Count > 0)
            {
                var v = Q.Remove();

                // Insert vertex into tree
                F.AddVertex(v);

                // Add best edge to tree, if a minimum weight is known.
                if (E.TryGetValue(v, out var e))
                {
                    F.AddEdge(e, v, C[v]);
                }

                // 
                foreach (var w in graph.GetSuccessors(v))
                {
                    if (Q.Contains(w))
                    {
                        var weight = graph.GetEdgeProperty(v, w);
                        if (!C.ContainsKey(w) || weight.CompareTo(C[w]) < 0)
                        {
                            C[w] = weight;
                            E[w] = v;

                            // Notify the queue the priority of the item has changed
                            Q.Update(w);
                        }
                    }
                }
            }

            return F;
        }

        #endregion

        #region Topological Ordering

        /// <summary>
        /// Produces a topological ordering of the graph.
        /// </summary>
        /// <exception cref="InvalidOperationException">Unable to produce a topological ordering of a undirected graph.</exception>
        /// <exception cref="InvalidOperationException">Unable to produce a topological ordering of a cyclic graph.</exception>
        public static IEnumerable<T> GetTopologicalOrder<T, E>(this Graph<T, E> graph) where E : struct
        {
            if (graph.IsDirected)
            {
                var ordering = Search.GetTopologicalOrder(graph.Vertices, graph.GetSuccessors);
                if (ordering == null) { throw new InvalidOperationException("Unable to determine topological order of an cyclic graph."); }
                return ordering;
            }
            else
            {
                throw new InvalidOperationException("Unable to determine topological order of an undirected graph.");
            }
        }

        #endregion
    }
}
