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
        /// <param name="getSucessors">Returns sucessors.</param>
        /// <param name="cost">Cost function between two states. If neighbors, should return actual, if non-neighbors estimate.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSucessors, CostFunction<T> cost)
        // Assumes cost(...) will return actual cost for neighbors and estimated for non-neighbors
        {
            return HeuristicSearch(start, goal, getSucessors, cost, cost);
        }

        /// <summary>
        /// Finds a path between two known states.
        /// </summary>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state.</param>
        /// <param name="goal">Terminating state.</param>
        /// <param name="getSucessors">Returns sucessors.</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSucessors, CostFunction<T> cost, CostFunction<T> heuristic)
        {
            return HeuristicSearch(start, x => Equals(x, goal), getSucessors, cost, s => heuristic(s, goal));
        }

        /// <summary>
        /// Search a problem space from some starting state until the search predicate is satified. <para/>
        /// A common application of a heuristic search is implementing path finding.
        /// </summary>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state</param>
        /// <param name="targetPredicate">Search predicate</param>
        /// <param name="getSucessors">Returns sucessors</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IList<T> HeuristicSearch<T>(T start, Func<T, bool> targetPredicate, Func<T, IEnumerable<T>> getSucessors, CostFunction<T> cost, HeuristicFunction<T> heuristic)
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

            IList<TState> ConstructPath<TState>(Node<TState> node)
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
                foreach (var adjacent in getSucessors(current.State))
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
        /// <param name="getSucessors">A function to return the successor/children of the current element</param>
        /// <returns>A depth first traversal of elements</returns>
        public static IEnumerable<T> DepthFirst<T>(T start, Func<T, IEnumerable<T>> getSucessors)
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
                foreach (var child in getSucessors(item))
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
        /// <param name="getSucessors">A function to return the successor/children of the current element.</param>
        /// <returns>A breadth first traversal of elements.</returns>
        public static IEnumerable<T> BreadthFirst<T>(T start, Func<T, IEnumerable<T>> getSucessors)
        {
            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();

                // Visit this node
                yield return item;

                // Add children to frontier
                foreach (var child in getSucessors(item))
                {
                    queue.Enqueue(child);
                }
            }
        }

        #endregion
    }
}
