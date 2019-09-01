using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public sealed class SparseGridBroadPhase<B> : BroadPhase<B> where B : class, ISpatialObject
    {
        private readonly Dictionary<B, IntRectangle> _bodies;
        private readonly SparseGrid<List<B>> _grid;

        // todo: automatically determine with average body size (diagonal)
        // and recreate grid object when average exceeds some error threshold
        private readonly float _scalingFactor;

        public SparseGridBroadPhase(float cellSize)
        {
            _scalingFactor = 1F / cellSize;

            _bodies = new Dictionary<B, IntRectangle>();
            _grid = new SparseGrid<List<B>>();
        }

        #region Structure

        protected override void InsertStructure(B body)
        {
            if (_bodies.ContainsKey(body))
            {
                // body already contained?
                throw new InvalidOperationException();
            }
            else
            {
                // 
                var ibounds = ComputeIntegerBounds(body.Bounds);

                // Store integer bounds
                _bodies[body] = ibounds;

                // Place body onto grid
                for (var y = ibounds.Top; y < ibounds.Bottom; y++)
                {
                    for (var x = ibounds.Left; x < ibounds.Right; x++)
                    {
                        var co = new IntVector(x, y);

                        // Try to get cell set
                        if (!_grid.TryGetValue(co, out var cell))
                        {
                            cell = new List<B>();
                            _grid[co] = cell;
                        }

                        // Add cell to set
                        cell.Add(body);
                    }
                }
            }
        }

        protected override void RemoveStructure(B body)
        {
            if (_bodies.TryGetValue(body, out var ibounds))
            {
                // Remove from active list
                _bodies.Remove(body);

                // Remove from each cell
                for (var y = ibounds.Top; y < ibounds.Bottom; y++)
                {
                    for (var x = ibounds.Left; x < ibounds.Right; x++)
                    {
                        // 
                        var co = new IntVector(x, y);

                        // Try to get cell set
                        if (_grid.TryGetValue(co, out var cell))
                        {
                            cell.Remove(body);
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                }
            }
        }

        protected override void UpdateStructure(B obj)
        {
            // todo: some sort of delta to prevent operations on the overlap regions?
            // Would the optimization actualyl provide benifits?
            RemoveStructure(obj);
            InsertStructure(obj);
        }

        #endregion

        #region Query

        public override IEnumerable<B> Query(B body)
        {
            // Try to get neighbors of body
            if (_bodies.TryGetValue(body, out var ibounds))
            {
                // Enumerate distinct bodies in the coarse region
                foreach (var other in GatherRegion(ibounds).Distinct())
                {
                    // Do the bounding boxes overlap? (coarse elimination)
                    if (!Equals(body, other) && body.Bounds.Overlaps(other.Bounds))
                    {
                        yield return other;
                    }
                }
            }
        }

        public override IEnumerable<B> Query(Rectangle bounds)
        {
            var ibounds = ComputeIntegerBounds(bounds);

            // Enumerate distinct bodies in the coarse region
            return GatherRegion(ibounds).Distinct()
                  // Do the bounding boxes overlap? (coarse elimination)
                  .Where(b => b.Bounds.Overlaps(bounds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<B> Query(Ray ray, float maxDistance)
        {
            var p0 = ComputeIntegerVector(ray.Origin);
            var p1 = ComputeIntegerVector(ray.GetPoint(maxDistance));

            return GatherLine(p0, p1).Distinct();
        }

        public override IEnumerable<B> Query(Vector point)
        {
            var co = ComputeIntegerVector(point);

            // Do entities exist in the cells at all?
            if (_grid.TryGetValue(co, out var items))
            {
                foreach (var other in items)
                {
                    yield return other;
                }
            }
        }

        #region Query Utilities

        private IEnumerable<B> GatherRegion(IntRectangle ibounds)
        {
            for (var y = ibounds.Top; y < ibounds.Bottom; y++)
            {
                for (var x = ibounds.Left; x < ibounds.Right; x++)
                {
                    // 
                    var co = new IntVector(x, y);

                    // Do entities exist in the cells at all?
                    if (_grid.TryGetValue(co, out var items))
                    {
                        foreach (var other in items)
                        {
                            yield return other;
                        }
                    }
                }
            }
        }

        private IEnumerable<B> GatherLine(IntVector p0, IntVector p1)
        // http://members.chello.at/~easyfilter/bresenham.html
        {
            var x0 = p0.X;
            var x1 = p1.X;

            var y0 = p0.Y;
            var y1 = p1.Y;

            int dx = Calc.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Calc.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2; /* error value e_xy */

            while (true)
            {
                var co = new IntVector(x0, y0);

                // Do entities exist in the cells at all?
                if (_grid.TryGetValue(co, out var items))
                {
                    foreach (var other in items)
                    {
                        yield return other;
                    }
                }

                // Reached end of line
                if (x0 == x1 && y0 == y1) { break; }

                e2 = 2 * err;
                if (e2 >= dy) { err += dy; x0 += sx; } /* e_xy + e_x > 0 */
                if (e2 <= dx) { err += dx; y0 += sy; } /* e_xy + e_y < 0 */
            }
        }

        #endregion

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IntVector ComputeIntegerVector(Vector point)
        {
            var x1 = Calc.Floor(point.X * _scalingFactor);
            var y1 = Calc.Floor(point.Y * _scalingFactor);

            return new IntVector(x1, y1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IntRectangle ComputeIntegerBounds(Rectangle bounds)
        {
            var x1 = Calc.Floor(bounds.Left * _scalingFactor);
            var y1 = Calc.Floor(bounds.Top * _scalingFactor);
            var x2 = Calc.Ceil(bounds.Right * _scalingFactor);
            var y2 = Calc.Ceil(bounds.Bottom * _scalingFactor);

            return new IntRectangle(x1, y1, x2 - x1, y2 - y1);
        }
    }
}
