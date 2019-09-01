using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Math;
using Heirloom.Runtime;

namespace Shooter
{
    internal class Trail : Component
    {
        public Image Image { get; set; }

        public Vector Position { get; set; }

        private readonly List<Vector> _points;
        private float _time = 0F;

        private Mesh _mesh;

        public Trail()
            : base()
        {
            _points = new List<Vector>();
            _mesh = new Mesh();
        }

        protected override void Update()
        {
            // 
            var distance = float.MaxValue;

            // If we have points, get the distance from the player to the trail
            if (_points.Count > 0)
            {
                distance = Vector.Distance(Position, _points[0]);
            }

            // If too far away from trail position or fade over time
            // TODO: Expose distance as parameter?
            if (distance > 20 || CheckElapsedTime(Time.Delta))
            {
                // Too many points
                // TODO: Expose as parameter?
                if (_points.Count > 30)
                {
                    _points.RemoveAt(_points.Count - 1);
                }

                // Insert an up to date point
                _points.Insert(0, Position);
                Compute();
            }
        }

        protected override void Render(RenderContext ctx)
        {
            // Do we have vertices to draw + a texture
            if (_mesh.Vertices.Count > 0 && Image != null)
            {
                ctx.BlendMode = BlendMode.Additive;
                ctx.Draw(Image, _mesh, Matrix.Identity, Color.White);
                ctx.BlendMode = BlendMode.Alpha;
            }
        }

        private bool CheckElapsedTime(float delta)
        {
            // TODO: Expose DURATION as parameter?
            const float DURATION = 0.1F;

            // Accumulate time (in seconds)
            _time += delta;

            // Has enough time passed?
            if (_time > DURATION)
            {
                _time -= DURATION;
                return true;
            }

            return false;
        }

        private void Compute()
        {
            // 
            _mesh.Vertices.Clear();
            _mesh.Indices.Clear();

            if (Image != null)
            {
                // 
                for (var i = 0; i < _points.Count - 1; i++)
                {
                    var p0 = _points[i + 0];
                    var p1 = _points[i + 1];

                    // TODO: Expose width as parameter
                    var d = (p1 - p0).Perpendicular.Normalized;
                    d = d * Image.Width / 2F;

                    // Texture coordinates
                    var v0 = 1F - ((i + 0) / ((float) _points.Count - 1));
                    var v1 = 1F - ((i + 1) / ((float) _points.Count - 1));

                    // -d --/ -d as 0 --/ 2
                    // p0  /  p1    |  /  |
                    // +d /-- +d    1 /-- 3

                    var vertexOffset = _mesh.Vertices.Count;

                    // 
                    _mesh.Vertices.Add(new Vertex(p0 - d, (0, v0)));
                    _mesh.Vertices.Add(new Vertex(p0 + d, (1, v0)));
                    _mesh.Vertices.Add(new Vertex(p1 - d, (0, v1)));
                    _mesh.Vertices.Add(new Vertex(p1 + d, (1, v1)));

                    // 
                    _mesh.Indices.Add((ushort) (vertexOffset + 0));
                    _mesh.Indices.Add((ushort) (vertexOffset + 1));
                    _mesh.Indices.Add((ushort) (vertexOffset + 2));

                    _mesh.Indices.Add((ushort) (vertexOffset + 2));
                    _mesh.Indices.Add((ushort) (vertexOffset + 1));
                    _mesh.Indices.Add((ushort) (vertexOffset + 3));
                }
            }
        }
    }
}
