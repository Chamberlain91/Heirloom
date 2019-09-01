using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public interface IReadOnlyGrid<T>
    {
        T this[IntVector co] { get; set; }

        T this[int x, int y] { get; set; }

        IEnumerable<IntVector> FindNeighbors(IntVector co, GridNeighbors neighbors = GridNeighbors.FourAxis);

        IEnumerable<IntVector> FindNeighbors(IntVector co, Predicate<T> predicate, GridNeighbors neighbors = GridNeighbors.FourAxis);
    }

    public interface IGrid<T> : IReadOnlyGrid<T>
    {
        void Clear();
    }
}
