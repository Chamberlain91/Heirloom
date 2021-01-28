using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Implementation of various algorithms implemented against <see cref="Graph{V, E}"/>.
    /// Most of the methods here are implemented as extension methods.
    /// </summary>
    public static class GraphTools
    {
        // todo: somehow hook smoothly against IReadOnlyGraph<V,E>?

        #region Page Rank

        //public static IReadOnlyDictionary<V, float> ComputePageRank<V>(this WeightedGraph<V> graph)
        //{
        //    return ComputePageRank(graph, weight => weight);
        //}

        //public static IReadOnlyDictionary<V, float> ComputePageRank<V, E>(this Graph2<V, E> graph, Func<E, float> getEdgeWeight)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region Community Detection

        //public abstract IEnumerable<ICollection<V>> DetectCommunities(); // streaming?, what algorithm?

        #endregion

        #region Find Path (Heuristic Search, A*)

        /// <summary>
        /// Find a path from some starting vertex until a goal condition is reached.
        /// </summary>
        /// <typeparam name="V">Some vertex type.</typeparam>
        /// <typeparam name="E">Some edge property type.</typeparam>
        /// <param name="graph">Some graph to find a path within.</param>
        /// <param name="start">Some vertex within the graph to start.</param>
        /// <param name="goalCondition">Some condition to accept goal state.</param>
        /// <param name="cost">The cost between neighboring vertices.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <returns>A sequence of vertices from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<V> FindPath<V, E>(this IReadOnlyGraph<V, E> graph,
            V start,
            Func<V, bool> goalCondition,
            ActualCost<V> cost,
            HeuristicCost<V> heuristic) where E : struct
        {
            return Search.HeuristicSearch(start, goalCondition, graph.GetSuccessors, cost, heuristic);
        }

        /// <summary>
        /// Find a path from some starting vertex until a goal condition is reached.
        /// </summary>
        /// <typeparam name="V">Some vertex type.</typeparam>
        /// <typeparam name="E">Some edge property type.</typeparam>
        /// <param name="graph">Some graph to find a path within.</param>
        /// <param name="start">Some vertex within the graph to start.</param>
        /// <param name="goalCondition">Some condition to accept goal state.</param>
        /// <param name="getCost">Gets the edge cost from <typeparamref name="E"/>.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <returns>A sequence of vertices from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<V> FindPath<V, E>(this IReadOnlyGraph<V, E> graph,
            V start,
            Func<V, bool> goalCondition,
            Func<E, float> getCost,
            HeuristicCost<V> heuristic) where E : struct
        {
            return FindPath(graph, start, goalCondition, (a, b) => getCost(graph.GetEdgeProperty(a, b)), heuristic);
        }

        /// <summary>
        /// Find a path from some starting vertex until a goal condition is reached.
        /// </summary>
        /// <typeparam name="V">Some vertex type.</typeparam>
        /// <typeparam name="E">Some edge property type.</typeparam>
        /// <param name="graph">Some graph to find a path within.</param>
        /// <param name="start">Some vertex within the graph to start.</param>
        /// <param name="goalCondition">Some condition to accept goal state.</param>
        /// <param name="getCost">Gets the edge cost from <typeparamref name="E"/>.</param>
        /// <returns>A sequence of vertices from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<V> FindPath<V, E>(this IReadOnlyGraph<V, E> graph,
            V start,
            Func<V, bool> goalCondition,
            Func<E, float> getCost) where E : struct
        {
            // degenerates to Djikstra's?
            return FindPath(graph, start, goalCondition, getCost, x => 0);
        }

        /// <summary>
        /// Find a path between to vertices in the graph.
        /// </summary>
        /// <typeparam name="V">Some vertex type.</typeparam>
        /// <typeparam name="E">Some edge property type.</typeparam>
        /// <param name="graph">Some graph to find a path within.</param>
        /// <param name="start">Some vertex within the graph to act as the start vertex.</param>
        /// <param name="target">Some vertex within the graph to act as the goal vertex.</param>
        /// <param name="cost">The cost between neighboring vertices.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <returns>A sequence of vertices from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<V> FindPath<V, E>(this IReadOnlyGraph<V, E> graph,
            V start,
            V target,
            ActualCost<V> cost,
            HeuristicCost<V> heuristic) where E : struct
        {
            return FindPath(graph, start, vtx => Equals(vtx, target), cost, heuristic);
        }

        /// <summary>
        /// Find a path between to vertices in the graph.
        /// </summary>
        /// <typeparam name="V">Some vertex type.</typeparam>
        /// <typeparam name="E">Some edge property type.</typeparam>
        /// <param name="graph">Some graph to find a path within.</param>
        /// <param name="start">Some vertex within the graph to act as the start vertex.</param>
        /// <param name="target">Some vertex within the graph to act as the goal vertex.</param>
        /// <param name="getCost">Gets the edge cost from <typeparamref name="E"/>.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <returns>A sequence of vertices from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<V> FindPath<V, E>(this IReadOnlyGraph<V, E> graph,
            V start,
            V target,
            Func<E, float> getCost,
            HeuristicCost<V> heuristic) where E : struct
        {
            return FindPath(graph, start, vtx => Equals(vtx, target), getCost, heuristic);
        }

        /// <summary>
        /// Find a path between to vertices in the graph.
        /// </summary>
        /// <typeparam name="V">Some vertex type.</typeparam>
        /// <typeparam name="E">Some edge property type.</typeparam>
        /// <param name="graph">Some graph to find a path within.</param>
        /// <param name="start">Some vertex within the graph to act as the start vertex.</param>
        /// <param name="target">Some vertex within the graph to act as the goal vertex.</param>
        /// <param name="getCost">Gets the edge cost from <typeparamref name="E"/>.</param>
        /// <returns>A sequence of vertices from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<V> FindPath<V, E>(this IReadOnlyGraph<V, E> graph,
            V start,
            V target,
            Func<E, float> getCost) where E : struct
        {
            // degenerates to Djikstra's?
            return FindPath(graph, start, vtx => Equals(vtx, target), getCost, x => 0);
        }

        #endregion

        #region Components (Strong & Weak)

        private class TarjanNode<V>
        {
            public readonly V Vertex;

            public bool OnStack;
            public int Index;
            public int LowLink;

            public TarjanNode(V vertex)
            {
                Vertex = vertex;
            }
        }

        /// <summary>
        /// Finds the strong components and returns the vertices. <para/>
        /// This is the first step of <see cref="GetStrongComponents"/>, and useful to skip generating the subgraph instances if they are not needed.
        /// </summary>
        /// <remarks>
        /// Tarjan's strongly connected components algorithm.
        /// </remarks>
        public static IReadOnlyCollection<IReadOnlyCollection<V>> GetStrongComponentVertices<V, E>(this Graph<V, E> graph) where E : struct
        {
            var nodes = new Dictionary<V, TarjanNode<V>>();

            var output = new List<IReadOnlyCollection<V>>();

            var index = 0;
            var S = new Stack<TarjanNode<V>>();
            foreach (var v in graph.Vertices)
            {
                if (HasNode(v) == false) // "index is undefined"
                {
                    StrongConnect(v);
                }
            }

            return output;

            void StrongConnect(V v_)
            {
                var v = GetNode(v_);

                // Set the depth index for v to the smallest unused index
                v.Index = index;
                v.LowLink = index;
                index++;
                S.Push(v);
                v.OnStack = true;

                // Consider successors of v
                foreach (var w_ in graph.GetSuccessors(v.Vertex))
                {
                    if (HasNode(w_) == false) // "index is undefined"
                    {
                        // Successor w has not yet been visited; recurse on it
                        StrongConnect(w_);
                        v.LowLink = Math.Min(v.LowLink, GetNode(w_).LowLink);
                    }
                    else
                    {
                        var w = GetNode(w_);
                        if (w.OnStack)
                        {
                            // Successor w is in stack S and hence in the current SCC
                            // If w is not on stack, then (v, w) is an edge pointing to an SCC already found and must be ignored
                            // Note: The next line may look odd - but is correct.
                            // It says w.index not w.lowlink; that is deliberate and from the original paper
                            v.LowLink = Math.Min(v.LowLink, w.Index);
                        }
                    }
                }

                // If v is a root node, pop the stack and generate an SCC
                if (v.LowLink == v.Index)
                {
                    var component = new HashSet<V>();

                    var w = default(TarjanNode<V>);
                    do
                    {
                        w = S.Pop();
                        w.OnStack = false;
                        component.Add(w.Vertex);
                    } while (!Equals(w, v));

                    output.Add(component);
                }
            }

            bool HasNode(V v)
            {
                return nodes.ContainsKey(v);
            }

            TarjanNode<V> GetNode(V v)
            {
                if (nodes.TryGetValue(v, out var node))
                {
                    return node;
                }
                else
                {
                    node = new TarjanNode<V>(v);
                    nodes[v] = node;
                    return node;
                }
            }
        }

        /// <summary>
        /// Finds the strong components and returns each subgraph. <para/>
        /// This is the first step of <see cref="GetStrongComponents"/>, and useful to skip generating the subgraph instances if they are not needed.
        /// </summary>
        /// <remarks>
        /// This will throw an exception in an undirected graph. 
        /// </remarks>
        public static IEnumerable<Graph<V, E>> GetStrongComponents<V, E>(this Graph<V, E> graph) where E : struct
        {
            if (!graph.IsDirected) { throw new InvalidOperationException("Unable to find strongly connected components in a directed graph."); }

            foreach (var vertices in GetStrongComponentVertices(graph))
            {
                yield return graph.CreateSubgraph(vertices);
            }
        }

        /// <summary>
        /// Finds the weak components (union find) and returns the vertices. <para/>
        /// This is the first step of <see cref="GetStrongComponents"/>, and useful to skip generating the subgraph instances if they are not needed.
        /// </summary>
        /// <remarks>
        /// This will ignore edge direction in a directed graph. 
        /// </remarks>
        public static IEnumerable<IReadOnlyCollection<V>> GetComponentVertices<V, E>(this Graph<V, E> graph) where E : struct
        {
            foreach (var union in Search.UnionFind(graph.Vertices, graph.GetNeighbors))
            {
                yield return union;
            }
        }

        /// <summary>
        /// Finds the weakly connected components (union find) and returns each respective subgraph. <para/>
        /// This will ignore edge direction in a directed graph.
        /// </summary>
        public static IEnumerable<Graph<V, E>> GetComponents<V, E>(this Graph<V, E> graph) where E : struct
        {
            foreach (var vertices in GetComponentVertices(graph))
            {
                yield return graph.CreateSubgraph(vertices);
            }
        }

        #endregion

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

        /// <summary>
        /// Implements Prim/Jarnik' MST Algorthim. (https://en.wikipedia.org/wiki/Prim%27s_algorithm)
        /// </summary>
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
