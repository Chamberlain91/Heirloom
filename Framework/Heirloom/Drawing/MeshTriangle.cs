using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public struct MeshTriangle
    {
        public MeshVertex A;

        public MeshVertex B;

        public MeshVertex C;

        public MeshTriangle(MeshVertex a, MeshVertex b, MeshVertex c)
        {
            A = a;
            B = b;
            C = c;
        }

        public IEnumerable<MeshTriangle> Clip(Rectangle rect)
        {
            return Clip(GetRectPoints());

            IEnumerable<Vector> GetRectPoints()
            {
                yield return rect.TopLeft;
                yield return rect.TopRight;
                yield return rect.BottomRight;
                yield return rect.BottomLeft;
            }
        }

        public IEnumerable<MeshTriangle> Clip(IEnumerable<Vector> clipPolygon)
        {
            return Clip(PolygonTools.GetEdges(clipPolygon));
        }

        public IEnumerable<MeshTriangle> Clip(IEnumerable<(Vector, Vector)> clipEdges)
        {
            var outputList = new List<MeshVertex> { A, B, C };
            var inputList = new List<MeshVertex>();

            foreach (var (clipA, clipB) in clipEdges)
            {
                inputList.Clear();
                inputList.AddRange(outputList);

                outputList.Clear();

                for (var i = 0; i < inputList.Count; i += 1)
                {
                    var current = inputList[i];
                    var previous = inputList[(i + inputList.Count - 1) % inputList.Count];

                    // Determine inside/outside via assumption of clockwise ordering
                    var clipEdge = clipB - clipA;
                    var isCurrentInside = Vector.Cross(clipEdge, current.Position - clipA) >= 0;

                    if (isCurrentInside)
                    {
                        if (Vector.Cross(clipEdge, previous.Position - clipA) < 0)
                        {
                            // Edge is clipped by prior vertex, insert interpolated vertex.
                            LineSegment.Intersects(previous.Position, current.Position, clipA, clipB, out float intersectionTime, clampSegment: false);
                            var intersection = MeshVertex.Lerp(previous, current, intersectionTime);
                            outputList.Add(intersection);
                        }

                        // Current vertex is contained, insert.
                        outputList.Add(current);
                    }
                    else if (Vector.Cross(clipEdge, previous.Position - clipA) >= 0)
                    {
                        // Edge is clipped by current vertex, insert interpolated vertex
                        LineSegment.Intersects(previous.Position, current.Position, clipA, clipB, out float intersectionTime, clampSegment: false);
                        var intersection = MeshVertex.Lerp(previous, current, intersectionTime);
                        outputList.Add(intersection);
                    }
                }
            }

            // Emit clipped polygon as triangle fan
            for (var i = 1; i < outputList.Count - 1; i++)
            {
                var a = outputList[0];
                var b = outputList[i + 0];
                var c = outputList[i + 1];

                yield return new MeshTriangle(a, b, c);
            }
        }

        public void Deconstruct(out MeshVertex a, out MeshVertex b, out MeshVertex c)
        {
            a = A;
            b = B;
            c = C;
        }
    }
}
