using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public class Mesh
    {
        public List<Vertex> Vertices { get; }

        public List<ushort> Indices { get; }

        public Mesh()
        {
            Vertices = new List<Vertex>();
            Indices = new List<ushort>();
        }

        #region Create Standard Shapes

        public static Mesh CreateFromConvexPolygon(IEnumerable<Vector> polygon)
        {
            var mesh = new Mesh();

            var count = polygon.Count();

            // Add vertices
            // todo: UV?
            mesh.Vertices.AddRange(polygon.Select(v => new Vertex(v, Vector.Zero)));

            // Add triangles (triangle fan style)
            for (var i = 1; i < count; i++)
            {
                mesh.Indices.Add(0);
                mesh.Indices.Add((ushort) i);
                mesh.Indices.Add((ushort) ((i + 1) % count));
            }

            return mesh;
        }

        /// <summary>
        /// Constructs a mesh from the given polygon via <see cref="Polygon.Triangulate()"/>.
        /// </summary>
        /// <param name="polygon">Some polygon</param>
        /// <returns></returns>
        public static Mesh CreateFromPolygon(Polygon polygon)
        {
            var mesh = new Mesh();

            foreach (var (a, b, c) in Polygon.DecomposeTrianglesIndices(polygon))
            {
                var vA = polygon[a];
                var vB = polygon[b];
                var vC = polygon[c];

                var uA = (vA - polygon.Bounds.Min) / polygon.Bounds.Width;
                var uB = (vB - polygon.Bounds.Min) / polygon.Bounds.Width;
                var uC = (vC - polygon.Bounds.Min) / polygon.Bounds.Width;

                var i = mesh.Vertices.Count;

                mesh.Vertices.Add(new Vertex(vA, uA));
                mesh.Vertices.Add(new Vertex(vB, uB));
                mesh.Vertices.Add(new Vertex(vC, uC));

                mesh.Indices.Add((ushort) (i + 0));
                mesh.Indices.Add((ushort) (i + 1));
                mesh.Indices.Add((ushort) (i + 2));
            }

            return mesh;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mesh CreateRegularPolygon(float radius, int sides)
        {
            var regularPolygon = Polygon.GetRegularPolygonPoints(Vector.Zero, sides, radius);
            return CreateFromConvexPolygon(regularPolygon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mesh CreateQuad()
        {
            return CreateQuad(1, 1);
        }

        public static Mesh CreateQuad(float w, float h)
        {
            var mesh = new Mesh();
            mesh.Indices.AddRange(new ushort[] { 0, 1, 2, 0, 2, 3 });
            mesh.Vertices.AddRange(new[] {
                new Vertex((0, 0), (0, 0)),
                new Vertex((w, 0), (1, 0)),
                new Vertex((w, h), (1, 1)),
                new Vertex((0, h), (0, 1))
            });

            return mesh;
        }

        #endregion
    }
}
