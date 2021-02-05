using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Collections
{
    /// <summary>
    /// Gets the known cost between two values.
    /// </summary>
    /// <category>Utility</category>
    public delegate float ActualCost<T>(T a, T b);

    /// <summary>
    /// Gets the estimated cost of the some value.
    /// </summary>
    /// <category>Utility</category>
    public delegate float HeuristicCost<T>(T a);

    /// <summary>
    /// Contains search related features.
    /// </summary>
    /// <category>Utility</category>
    public static class Search
    {
        #region Heuristic Search

        /// <summary>
        /// Finds the path in some problem space from a starting state until a goal state has been reached.
        /// </summary>
        /// <remarks>
        /// This search implements the A* algorithm.
        /// </remarks>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state</param>
        /// <param name="goal">Terminating state.</param>
        /// <param name="getSuccessors">Returns sucessors</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, ActualCost<T> cost, HeuristicCost<T> heuristic)
        {
            return HeuristicSearch(start, x => Equals(x, goal), getSuccessors, cost, heuristic);
        }

        /// <summary>
        /// Finds the path in some problem space from a starting state until a goal condition is satified.
        /// </summary>
        /// <remarks>
        /// This search implements the A* algorithm.
        /// </remarks>
        /// <typeparam name="T">Type of the state elements.</typeparam>
        /// <param name="start">Starting state</param>
        /// <param name="goalCondition">Search predicate</param>
        /// <param name="getSuccessors">Returns sucessors</param>
        /// <param name="cost">Cost function between two states.</param>
        /// <param name="heuristic">Estimate cost between state and goal state.</param>
        /// <returns>A sequence of states from start (inclusive) to goal (inclusive).</returns>
        public static IReadOnlyList<T> HeuristicSearch<T>(T start, Func<T, bool> goalCondition, Func<T, IEnumerable<T>> getSuccessors, ActualCost<T> cost, HeuristicCost<T> heuristic)
        {
            // todo: Optimize, prevent allocation of these structures, thread static? (micro optimization)
            var groundScore = new Dictionary<T, float> { [start] = 0F };
            var futureScore = new Dictionary<T, float> { [start] = heuristic(start) };
            var frontier = new Heap<T>((a, b) => futureScore[a] < futureScore[b] ? -1 : 1) { start };
            var ancestors = new Dictionary<T, T>();

            while (frontier.Count > 0)
            {
                var current = frontier.Remove();

                if (goalCondition(current))
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
        /// Determines if a graph is cyclic. Do not use on infinite graphs.
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <param name="start">Starting element.</param>
        /// <param name="getSuccessors">A function to return the successor/children of the current element.</param>
        /// <returns>True if the graph is determined to be cycle free, otherwise false.</returns>
        public static bool DetectCyclicGraph<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
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

        #region Topological Order

        /// <summary>
        /// Produces a topological ordering of some directed acyclic graph.
        /// </summary>
        /// <returns>The topological ordering or <see langword="null"/> if the graph is cyclic.</returns>
        public static IReadOnlyList<T> GetTopologicalOrder<T>(IEnumerable<T> nodes, Func<T, IEnumerable<T>> getSuccessors)
        {
            var marks = new Dictionary<T, bool>();
            var output = new List<T>();

            var items = new Bag<T>(nodes);
            while (items.TryRemove(out var start))
            {
                // Node has been marked, skip
                // todo: this feels potentially inefficient, somehow implement bag to have O(1) remove intead of O(n) skips?
                if (marks.ContainsKey(start)) { continue; }

                // Visit this node
                if (!Visit(start))
                {
                    return null; // cyclic
                }
            }

            output.Reverse();
            return output;

            bool Visit(T node)
            {
                if (marks.ContainsKey(node))
                {
                    if (marks[node]) { return true; } // leaf node
                    else { return false; } // cyclic
                }

                marks[node] = false; // temporary mark

                foreach (var successor in getSuccessors(node))
                {
                    if (!Visit(successor)) { return false; }
                }

                marks[node] = true; // permanent mark

                // 
                output.Add(node);
                return true;
            }
        }

        #endregion

        /// <summary>
        /// Finds distinct components (union find) of a graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="getSuccessors"></param>
        /// <returns></returns>
        public static IEnumerable<IReadOnlyList<T>> UnionFind<T>(IEnumerable<T> items, Func<T, IEnumerable<T>> getSuccessors)
        {
            if (items is null) { throw new ArgumentNullException(nameof(items)); }
            if (getSuccessors is null) { throw new ArgumentNullException(nameof(getSuccessors)); }

            var free = new HashSet<T>(items);

            // While we have possible components
            while (free.Count > 0)
            {
                // Construct new frontier
                var frontier = new Queue<T>();

                // Populate the first vertex in the frontier
                var first = free.First();
                frontier.Enqueue(first);
                free.Remove(first);

                // 
                var component = new List<T>();

                // While we have a frontier to explore
                while (frontier.Count > 0)
                {
                    // Get next element from frontier
                    var v = frontier.Dequeue();

                    // Insert vertex into component
                    component.Add(v);

                    // Enqueue neighbors
                    foreach (var n in getSuccessors(v))
                    {
                        // Enqueue neighbor into frontier and remove from the free set
                        if (free.Remove(n))
                        {
                            frontier.Enqueue(n);
                        }
                    }
                }

                // Emit component
                yield return component;
            }
        }
    }
}
