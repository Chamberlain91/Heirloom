using System;
using System.Collections.Generic;
using System.Linq;

namespace Meadows.Drawing
{
    public sealed class Mesh : NativeResource
    {
        internal readonly MeshVertex[] VerticesInternal;

        public Mesh(IEnumerable<MeshVertex> vertices)
        {
            VerticesInternal = vertices?.ToArray() ?? throw new ArgumentNullException(nameof(vertices));
        }

        public IReadOnlyList<MeshVertex> Vertices => VerticesInternal;

        internal static readonly Mesh QuadMesh = CreateQuadMesh();

        private static Mesh CreateQuadMesh()
        {
            return new Mesh(new[]
            {
                new MeshVertex((0,0), (0,0), Color.White),
                new MeshVertex((1,0), (1,0), Color.White),
                new MeshVertex((1,1), (1,1), Color.White),
                new MeshVertex((0,1), (0,1), Color.White)
            });
        }
    }
}
