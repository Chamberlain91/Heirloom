using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public static class Search
    {
        public delegate float CostFunction<T>(T a, T b);

        public delegate float HeuristicFunction<T>(T a);

        #region Heuristic Search

        /// <summary>
        /// Finds a path between two known states.
        /// </summary>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state.</param>
        /// <param name="goal">Terminating state.</param>
        /// <param name="getSuccessors">Returns sucessors.</param>
        /// <param name="cost">Cost function between two states. If neighbors, should return actual, if non-neighbors estimate.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, CostFunction<T> cost)
        // Assumes cost(...) will return actual cost for neighbors and estimated for non-neighbors
        {
            return HeuristicSearch(start, goal, getSuccessors, cost, cost);
        }

        /// <summary>
        /// Finds a path between two known states.
        /// </summary>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state.</param>
        /// <param name="goal">Terminating state.</param>
        /// <param name="getSuccessors">Returns sucessors.</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, CostFunction<T> cost, CostFunction<T> heuristic)
        {
            return HeuristicSearch(start, x => Equals(x, goal), getSuccessors, cost, s => heuristic(s, goal));
        }

        /// <summary>
        /// Search a problem space from some starting state until the search predicate is satified. <para/>
        /// A common application of a heuristic search is implementing path finding.
        /// </summary>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state</param>
        /// <param name="targetPredicate">Search predicate</param>
        /// <param name="getSuccessors">Returns sucessors</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IList<T> HeuristicSearch<T>(T start, Func<T, bool> targetPredicate, Func<T, IEnumerable<T>> getSuccessors, CostFunction<T> cost, HeuristicFunction<T> heuristic)
        {
            // 
            var nodes = new Dictionary<T, Node<T>>();

            // 
            var frontier = new Heap<Node<T>>
            {
                new Node<T>(start)
                {
                    State = start,
                    FScore = heuristic(start),
                    GScore = 0,
                }
            };

            Node<T> GetNode(T state)
            {
                if (!nodes.TryGetValue(state, out var node))
                {
                    node = new Node<T>(state);
                    nodes[state] = node;
                }

                return node;
            }

            static IList<TState> ConstructPath<TState>(Node<TState> node)
            {
                // Reconstruct path!
                var path = new List<TState>();

                do
                {
                    path.Add(node.State);
                    node = node.Ancestor;
                } while (node != null);

                // Return path
                path.Reverse();
                return path;
            }

            // While we have items to process
            while (frontier.Count > 0)
            {
                // Get lowest cost node
                var current = frontier.Remove();

                // Is the current state the target state?
                if (targetPredicate(current.State))
                {
                    // We have found what we want
                    return ConstructPath(current);
                }

                // 
                current.Visited = true;

                // For each adjacent state
                foreach (var adjacent in getSuccessors(current.State))
                {
                    var neighbor = GetNode(adjacent);

                    // If neighboring node has not been visited...
                    if (neighbor.Visited == false)
                    {
                        // Compute the estimated G score
                        var tentativeScore = current.GScore + cost(current.State, adjacent);

                        // Node was not visited, is it on the frontier?
                        if (frontier.Contains(neighbor) == false)
                        {
                            // Have not considered this state yet, add node.
                            frontier.Add(neighbor);
                        }
                        else if (tentativeScore >= neighbor.GScore)
                        {
                            // Was not a better option for this state.
                            continue;
                        }

                        // Best path (shortest), so we update the node.
                        neighbor.GScore = tentativeScore;
                        neighbor.FScore = tentativeScore + heuristic(adjacent);
                        neighbor.Ancestor = current;

                        // Update node within heap
                        frontier.Update(neighbor);
                    }
                }
            }

            // No path to target state!
            return null;
        }

        private class Node<T> : IComparable<Node<T>>, IEquatable<Node<T>>
        // TODO: Use finalizer to recycle these node objects into a object pool to reduce allocations?
        {
            public Node<T> Ancestor;

            public float GScore;

            public float FScore;

            public T State;

            public bool Visited;

            public Node(T state)
            {
                GScore = float.PositiveInfinity;
                FScore = float.PositiveInfinity;
                Ancestor = null;
                Visited = false;
                State = state;
            }

            public override bool Equals(object obj)
            {
                if (obj is Node<T> node)
                {
                    return Equals(node);
                }

                return false;
            }

            public bool Equals(Node<T> other)
            {
                return Equals(State, other.State);
            }

            public override int GetHashCode()
            {
                return State.GetHashCode();
            }

            public int CompareTo(Node<T> other)
            {
                var compare = FScore.CompareTo(other.FScore);
                return compare == 0 ? GScore.CompareTo(other.GScore) : compare;
            }
        }

        #endregion

        #region Traversal

        /// <summary>
        /// Traverses elements in depth-first ordering.
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <param name="start">Starting element</param>
        /// <param name="getSuccessors">A function to return the successor/children of the current element</param>
        /// <returns>A depth first traversal of elements</returns>
        public static IEnumerable<T> DepthFirst<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
        {
            var stack = new Stack<T>();
            stack.Push(start);

            // Useful Traversal Info:
            // https://www.geeksforgeeks.org/tree-traversals-inorder-preorder-and-postorder/

            while (stack.Count > 0)
            {
                var item = stack.Pop();

                // Visit this node
                yield return item;

                // Add children to frontier
                foreach (var child in getSuccessors(item))
                {
                    stack.Push(child);
                }
            }
        }

        /// <summary>
        /// Traverse elements in breadth-first ordering.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="start">Starting element.</param>
        /// <param name="getSuccessors">A function to return the successor/children of the current element.</param>
        /// <returns>A breadth first traversal of elements.</returns>
        public static IEnumerable<T> BreadthFirst<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
        {
            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();

                // Visit this node
                yield return item;

                // Add children to frontier
                foreach (var child in getSuccessors(item))
                {
                    queue.Enqueue(child);
                }
            }
        }

        #endregion

        #region Detect Cycles

        private enum AcyclicStatus { New, Active, Finished };

        /// <summary>
        /// Determines if the specified graph is acyclic. Do not use on infinite graphs.
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <param name="start">Starting element.</param>
        /// <param name="getSuccessors">A function to return the successor/children of the current element.</param>
        /// <returns>True if the graph is determined to be cycle free, otherwise false.</returns>
        public static bool IsAcyclicGraph<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
        {
            var statusTable = new Dictionary<T, AcyclicStatus>();

            // todo: see about using my own DepthFirst traversal defined above to 'remove duplication'
            // foreach (var node in DepthFirst(start, getSuccessors))
            // { ... }

            // For each unvisited node
            foreach (var node in getSuccessors(start))
            {
                if (GetStatus(node) == AcyclicStatus.New)
                {
                    // If subgraph is not a DAG, then entire graph is not a DAG
                    if (!IsAcyclicGraphHelper(node)) { return false; }
                }
            }

            return true;

            AcyclicStatus GetStatus(T node)
            {
                if (statusTable.TryGetValue(node, out var status)) { return status; }
                return statusTable[node] = AcyclicStatus.New;
            }

            bool IsAcyclicGraphHelper(T node)
            {
                statusTable[node] = AcyclicStatus.Active;

                // Check each successor
                foreach (var child in getSuccessors(node))
                {
                    // If child is active, we have detected a cycle (thus, not a DAG).
                    if (GetStatus(child) == AcyclicStatus.Active) { return false; }
                    // If the child is new (unvisited) we need to recurse and keep checking
                    else if (GetStatus(child) == AcyclicStatus.New)
                    {
                        // If subgraph is not a DAG, then the graph is not a DAG
                        if (!IsAcyclicGraphHelper(child)) { return false; }
                    }
                }

                // Was a source node or was determined to be cycle-free
                statusTable[node] = AcyclicStatus.Finished;
                return true;
            }
        }

        #endregion
    }
}
