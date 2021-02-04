using System;
using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Collections
{
    /// <summary>
    /// Provides extra utilities for interacting with a grid.
    /// </summary>
    /// <category>Grids</category>
    public static class GridTools
    {
        #region GetNeighborCoordinates (IntVector)

        /// <summary>
        /// Gets neighboring grid coordinates relative to the specified input coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates(IntVector co, GridNeighborType neighborType)
        {
            return GetNeighborCoordinates(co.X, co.Y, neighborType);
        }

        /// <summary>
        /// Gets neighboring grid coordinates relative to the specified input coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates(int x, int y, GridNeighborType neighborType)
        {
            if (neighborType == GridNeighborType.Axis)
            {
                // Clockwise from three o'clock
                yield return (x + 1, y + 0);
                yield return (x + 0, y + 1);
                yield return (x - 1, y + 0);
                yield return (x + 0, y - 1);
            }
            else
            if (neighborType == GridNeighborType.Diagonal)
            {
                // Clockwise from half past one
                yield return (x + 1, y - 1);
                yield return (x + 1, y + 1);
                yield return (x - 1, y + 1);
                yield return (x - 1, y - 1);
            }
            else
            {
                // Clockwise from three o'clock
                yield return (x + 1, y + 0);
                yield return (x + 1, y + 1);
                yield return (x + 0, y + 1);
                yield return (x - 1, y + 1);
                yield return (x - 1, y + 0);
                yield return (x - 1, y - 1);
                yield return (x + 0, y - 1);
                yield return (x + 1, y - 1);
            }
        }

        #endregion

        #region GetNeighborCoordinates (Grid<T> Extension)

        /// <summary>
        /// Gets the specified cell's neighbor coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates<T>(this IReadOnlyGrid<T> grid, int x, int y, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            foreach (var neighbor in GetNeighborCoordinates(x, y, neighborType))
            {
                if (grid.IsValidCoordinate(neighbor))
                {
                    yield return neighbor;
                }
            }
        }

        /// <summary>
        /// Gets the specified cell's neighbor coordinates.
        /// </summary>
        public static IEnumerable<IntVector> GetNeighborCoordinates<T>(this IReadOnlyGrid<T> grid, IntVector co, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return GetNeighborCoordinates(grid, co.X, co.Y, neighborType);
        }

        #endregion

        #region GetNeighbors (Grid<T> Extension)

        /// <summary>
        /// Gets the specified cell's neighbors.
        /// </summary>
        public static IEnumerable<T> GetNeighbors<T>(this IReadOnlyGrid<T> grid, IntVector co, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return GetNeighbors(grid, co.X, co.Y, neighborType);
        }

        /// <summary>
        /// Gets the specified cell's neighbors.
        /// </summary>
        public static IEnumerable<T> GetNeighbors<T>(this IReadOnlyGrid<T> grid, int x, int y, GridNeighborType neighborType = GridNeighborType.Axis)
        {
            foreach (var neighbor in GetNeighborCoordinates(grid, x, y, neighborType))
            {
                yield return grid[neighbor];
            }
        }

        #endregion

        #region Find Path (Grid<T> Extension, A*)

        /// <summary>
        /// Find a path on a grid from some starting coordinate until a goal condition is reached.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="goalCondition">Some condition to accept goal state.</param>
        /// <param name="cost">The cost between neighboring cells.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            Func<IntVector, bool> goalCondition,
            ActualCost<IntVector> cost,
            HeuristicCost<IntVector> heuristic,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            // todo: replace with JPS?
            return Search.HeuristicSearch(start, goalCondition, co => grid.GetNeighborCoordinates(co, neighborType), cost, heuristic);
        }

        /// <summary>
        /// Finds the path on a grid from some starting coordinate until a goal condition is reached.
        /// Uses <see cref="IntVector.Distance(IntVector, IntVector)"/> as the cost measure between cells.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="goalCondition">Some condition to accept goal state.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            Func<IntVector, bool> goalCondition,
            HeuristicCost<IntVector> heuristic,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, goalCondition, IntVector.Distance, heuristic, neighborType);
        }

        /// <summary>
        /// Finds the path on a grid from some starting coordinate until a goal condition is reached.
        /// Uses <see cref="IntVector.Distance(IntVector, IntVector)"/> as the cost measure between cells.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="goalCondition">Some condition to accept goal state.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            Func<IntVector, bool> goalCondition,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, goalCondition, IntVector.Distance, co => 0, neighborType);
        }

        /// <summary>
        /// Finds a path between start and target coordinates on a grid.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="target">Some condition to accept goal state.</param>
        /// <param name="cost">The cost between neighboring cells.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            IntVector target,
            ActualCost<IntVector> cost,
            HeuristicCost<IntVector> heuristic,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, co => co == target, cost, heuristic, neighborType);
        }

        /// <summary>
        /// Finds a path between start and target coordinates on a grid.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="target">Some condition to accept goal state.</param>
        /// <param name="getTransitionCost">The cost between neighboring cells.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            IntVector target,
            Func<T, T, float> getTransitionCost,
            HeuristicCost<IntVector> heuristic,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, co => co == target, (a, b) => getTransitionCost(grid[a], grid[b]), heuristic, neighborType);
        }

        /// <summary>
        /// Finds a path between start and target coordinates on a grid using <see cref="IntVector.Distance(IntVector, IntVector)"/> as the heuristic measure.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="target">Some condition to accept goal state.</param>
        /// <param name="getTransitionCost">The cost between neighboring cells.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            IntVector target,
            Func<T, T, float> getTransitionCost,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, target, getTransitionCost, co => IntVector.Distance(co, target), neighborType);
        }

        /// <summary>
        /// Finds a path between start and target coordinates on a grid.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="target">Some condition to accept goal state.</param>
        /// <param name="getCellCost">The cost of transitioning to a cell.</param>
        /// <param name="heuristic">The cost estimation to reach the goal condition.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            IntVector target,
            Func<T, float> getCellCost,
            HeuristicCost<IntVector> heuristic,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, co => co == target, (a, b) => getCellCost(grid[b]), heuristic, neighborType);
        }

        /// <summary>
        /// Finds a path between start and target coordinates on a grid using <see cref="IntVector.Distance(IntVector, IntVector)"/> as the heuristic measure.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="target">Some condition to accept goal state.</param>
        /// <param name="getCellCost">The cost of transitioning to a cell.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            IntVector target,
            Func<T, float> getCellCost,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, target, getCellCost, co => IntVector.Distance(co, target), neighborType);
        }

        /// <summary>
        /// Finds a path between start and target coordinates on a grid using <see cref="IntVector.Distance(IntVector, IntVector)"/> as the heuristic and cost measure.
        /// </summary>
        /// <typeparam name="T">Some type stored in each grid cell.</typeparam> 
        /// <param name="grid">Some grid to find a path on.</param>
        /// <param name="start">The initial position in the grid.</param>
        /// <param name="target">Some condition to accept goal state.</param>
        /// <param name="neighborType">The allowable neighboring positions.</param>
        /// <returns>A sequence of coordinates from start (inclusive) to goal (inclusive) or null if a path cannot be found.</returns>
        public static IReadOnlyList<IntVector> FindPath<T>(this IReadOnlyGrid<T> grid,
            IntVector start,
            IntVector target,
            GridNeighborType neighborType = GridNeighborType.Axis)
        {
            return FindPath(grid, start, target, IntVector.Distance, co => IntVector.Distance(co, target), neighborType);
        }

        #endregion
    }
}
