using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Gets the known cost between two values.
    /// </summary>
    /// <category>Utility</category>
    public delegate float ActualCost<T>(T a, T b);

    /// <summary>
    /// Gets the estimated cost of some value.
    /// </summary>
    /// <category>Utility</category>
    public delegate float HeuristicCost<T>(T a);

    /// <summary>
    /// Contains search related features.
    /// </summary>
    /// <category>Utility</category>
    public static class Search
    {
        // todo: other search or other search related functions?
        // optimization methods, gradient descent / hill climbing?
        // fringe search (better memory usage than A*, may revisit nodes)
        // theta* (line of sight paths)

        #region Heuristic Search (A*)

        /// <summary>
        /// Finds the path in some problem space from a starting state until a target state is reached.
        /// If unable to find a path, null is returned.
        /// </summary>
        /// <remarks>
        /// This search implements the A* algorithm.
        /// </remarks>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state</param>
        /// <param name="target">Search predicate</param>
        /// <param name="getSuccessors">Returns sucessors</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive) or null if unable to determine a path.</returns>
        public static IReadOnlyList<T> HeuristicSearch<T>(T start, T target, Func<T, IEnumerable<T>> getSuccessors, ActualCost<T> cost)
        {
            return HeuristicSearch(start, target, getSuccessors, cost, v => cost(v, target));
        }

        /// <summary>
        /// Finds the path in some problem space from a starting state until a target state is reached.
        /// If unable to find a path, null is returned.
        /// </summary>
        /// <remarks>
        /// This search implements the A* algorithm.
        /// </remarks>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state</param>
        /// <param name="target">Search predicate</param>
        /// <param name="getSuccessors">Returns sucessors</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive) or null if unable to determine a path.</returns>
        public static IReadOnlyList<T> HeuristicSearch<T>(T start, T target, Func<T, IEnumerable<T>> getSuccessors, ActualCost<T> cost, HeuristicCost<T> heuristic)
        {
            return HeuristicSearch(start, v => Equals(v, target), getSuccessors, cost, heuristic);
        }

        /// <summary>
        /// Finds the path in some problem space from a starting state until a target condition is satified.
        /// If unable to find a path, null is returned.
        /// </summary>
        /// <remarks>
        /// This search implements the A* algorithm.
        /// </remarks>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state</param>
        /// <param name="targetCondition">Search predicate</param>
        /// <param name="getSuccessors">Returns sucessors</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive) or null if unable to determine a path.</returns>
        public static IReadOnlyList<T> HeuristicSearch<T>(T start, Func<T, bool> targetCondition, Func<T, IEnumerable<T>> getSuccessors, ActualCost<T> cost, HeuristicCost<T> heuristic)
        {
            // todo: Optimize, prevent allocation of these structures, thread static? (micro optimization)
            var groundScore = new Dictionary<T, float> { [start] = 0F };
            var futureScore = new Dictionary<T, float> { [start] = heuristic(start) };
            var frontier = new Heap<T>((a, b) => futureScore[a] < futureScore[b] ? -1 : 1) { start };
            var ancestors = new Dictionary<T, T>();

            while (frontier.Count > 0)
            {
                var current = frontier.Remove();

                if (targetCondition(current))
                {
                    // Found target, return path
                    return ReconstructPath(current);
                }
                else
                {
                    foreach (var neighbor in getSuccessors(current))
                    {
                        var tentative_score = groundScore[current] + cost(current, neighbor);
                        if (!groundScore.ContainsKey(neighbor) || tentative_score < groundScore[neighbor])
                        {
                            // Mark prior
                            ancestors[neighbor] = current;

                            // Update scores
                            groundScore[neighbor] = tentative_score;
                            futureScore[neighbor] = tentative_score + heuristic(neighbor);

                            // If unseen, add to open set otherwise bubble into proper place
                            if (!frontier.Contains(neighbor)) { frontier.Add(neighbor); }
                            else { frontier.Update(neighbor); }
                        }
                    }
                }
            }

            // Unable to find path
            return null;

            IReadOnlyList<T> ReconstructPath(T current)
            {
                var path = new List<T>() { current };
                while (ancestors.ContainsKey(current))
                {
                    current = ancestors[current];
                    path.Add(current);
                }

                // Flip path to sequence from the starting node
                path.Reverse();
                return path;
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
            // todo: Optimize, prevent allocation of this structure, thread static? (micro optimization)
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
            // todo: Optimize, prevent allocation of this structure, thread static? (micro optimization)
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
        /// Determines if the specified graph is cyclic.
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <param name="start">Starting element.</param>
        /// <param name="graph">Some graph.</param>
        /// <returns>True if the graph is determined to be cycle free, otherwise false.</returns>
        public static bool DetectCyclicGraph<T>(IGraph<T> graph, T start)
        {
            return DetectCyclicGraph(start, graph.GetNeighbors);
        }

        /// <summary>
        /// Determines if a graph is cyclic. Do not use on infinite graphs.
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <param name="start">Starting element.</param>
        /// <param name="getSuccessors">A function to return the successor/children of the current element.</param>
        /// <returns>True if the graph is determined to be cycle free, otherwise false.</returns>
        public static bool DetectCyclicGraph<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
        {
            // todo: Optimize, prevent allocation of this structure, thread static? (micro optimization)
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
                    if (!IsAcyclicGraphHelper(node)) { return true; }
                }
            }

            return false;

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
