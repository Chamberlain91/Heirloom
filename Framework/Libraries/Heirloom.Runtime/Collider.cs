using System;
using System.Diagnostics;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Collections.Spatial;

namespace Heirloom.Runtime
{
    public class Collider : Component, ISpatialObject
    {
        private IPolygon _shape;

        /// <summary>
        /// The collision shape.
        /// </summary>
        public IPolygon Shape
        {
            get => _shape;

            set
            {
                _shape = value;
                WorldShape = _shape.ToArray();
            }
        }

        public Rectangle Bounds { get; private set; }

        public bool IsDynamic => RigidBody != null;

        public RigidBody RigidBody { get; private set; }

        internal Vector[] WorldShape;

        protected internal override void Start()
        {
            RigidBody = GetComponent<RigidBody>();
        }

        internal void UpdateBroadPhase(BroadPhase<Collider> broadphase)
        {
            // Skip if no shape is assigned
            if (Shape == null) { return; }

            ComputeShape();
            ComputeBounds();

            // Update broad phase structure 
            broadphase.Update(this);
        }

        private void ComputeShape()
        {
            // Compute world position of each vertex
            for (var i = 0; i < Shape.Count; i++)
            {
                WorldShape[i] = Transform.Matrix * Shape[i];
            }

            // If the computed polygon has incorrect winding reverse,
            // this assumes the winding is incorrect from negative scale.
            // If the polygon itself is not convex, we have a different problem!
            if (!Polygon.IsConvex(WorldShape, 0))
            {
                Array.Reverse(WorldShape);
            }
        }

        private void ComputeBounds()
        {
            var bounds = Rectangle.InvertedInfinite;

            for (var i = 0; i < Shape.Count; i++)
            {
                bounds.Merge(WorldShape[i]);
            }

            Bounds = bounds;
        }

        protected internal override void Render(RenderContext ctx)
        {
            DrawDebug(ctx);
        }

        [Conditional("DEBUG")]
        private void DrawDebug(RenderContext ctx)
        {
            if (Shape is IPolygon ply) { ctx.DrawPolygon(ply, Transform.Matrix, Color.Cyan); }
            else { ctx.DrawRectOutline(Bounds, Color.Cyan); }
        }
    }
}
