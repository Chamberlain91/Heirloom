using System.Collections.Generic;
using System.Linq;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Sandbox
{
    internal class Program
    {
        private static Queue<MouseButtonEvent> _mouseButtonEvents;
        private static Queue<MouseScrollEvent> _mouseScrollEvents;
        private static Queue<MouseMoveEvent> _mouseMoveEvents;

        private static Window _window;
        private static List<Collider> _colliders;

        private static void Main(string[] args)
        {
            // 
            _mouseButtonEvents = new Queue<MouseButtonEvent>();
            _mouseScrollEvents = new Queue<MouseScrollEvent>();
            _mouseMoveEvents = new Queue<MouseMoveEvent>();

            // 
            _colliders = new List<Collider>();

            Application.Run(() =>
            {
                // Create window
                _window = new Window("Sandbox", MultisampleQuality.High);

                // Bind events
                _window.MouseMove += (w, e) => _mouseMoveEvents.Enqueue(e);
                _window.MousePress += (w, e) => _mouseButtonEvents.Enqueue(e);
                _window.MouseRelease += (w, e) => _mouseButtonEvents.Enqueue(e);
                _window.MouseScroll += (w, e) => _mouseScrollEvents.Enqueue(e);

                // Create render/update loop
                var loop = RenderLoop.Create(_window.Graphics, OnUpdate);
                loop.Start();

                // Add objects 
                _colliders.Add(new PolygonCollider(3, 0x40));
                _colliders.Add(new PolygonCollider(4, 0x60));
                _colliders.Add(new PolygonCollider(5, 0x80));

                _colliders.Add(new RectangleCollider(0x50));
                _colliders.Add(new TriangleCollider(0x50));
                _colliders.Add(new CircleCollider(0x60));

                _colliders.Add(new RectangleCollider(0x40));
                _colliders.Add(new TriangleCollider(0x40));
                _colliders.Add(new CircleCollider(0x50));

                // Position shapes
                for (var i = 0; i < _colliders.Count; i++)
                {
                    var offset = new Vector(200 + (i % 4 * 200), 200 + (i / 4 * 200));
                    _colliders[i].Translate(offset);
                }
            });
        }

        private static void ProcessMouseButtonEvent(MouseButtonEvent e)
        {
            if (e.Action == ButtonAction.Press)
            {
                for (var i = _colliders.Count - 1; i >= 0; i--)
                {
                    // Apply mouse press to object
                    if (_colliders[i].OnMousePress(e)) { return; }
                }
            }
            else
            {
                for (var i = _colliders.Count - 1; i >= 0; i--)
                {
                    // Apply mouse release to object
                    if (_colliders[i].OnMouseRelease(e)) { return; }
                }
            }
        }

        private static void ProcessMouseMoveEvent(MouseMoveEvent e)
        {
            for (var i = _colliders.Count - 1; i >= 0; i--)
            {
                // Apply mouse release to object
                if (_colliders[i].OnMouseMove(e)) { return; }
            }
        }

        private static void ProcessMouseScrollEvent(MouseScrollEvent e)
        {
            for (var i = _colliders.Count - 1; i >= 0; i--)
            {
                // Apply mouse release to object
                if (_colliders[i].OnMouseScroll(e)) { return; }
            }
        }

        private static void OnUpdate(Graphics ctx, float dt)
        {
            lock (_window)
            {
                // Process mouse input events
                while (_mouseMoveEvents.Count > 0) { ProcessMouseMoveEvent(_mouseMoveEvents.Dequeue()); }
                while (_mouseButtonEvents.Count > 0) { ProcessMouseButtonEvent(_mouseButtonEvents.Dequeue()); }
                while (_mouseScrollEvents.Count > 0) { ProcessMouseScrollEvent(_mouseScrollEvents.Dequeue()); }
            }

            ctx.Clear(Color.DarkGray);

            // Clear collision state
            for (var i = 0; i < _colliders.Count; i++)
            {
                _colliders[i].IsCollide = false;
            }

            // 
            for (var i = 0; i < _colliders.Count; i++)
            {
                var colliderA = _colliders[i].Shape;

                for (var j = i + 1; j < _colliders.Count; j++)
                {
                    var colliderB = _colliders[j].Shape;

                    // Test overlap between A and B
                    if (colliderA.Overlaps(colliderB))
                    {
                        // Overlapping, store result and break inner loop.
                        _colliders[i].IsCollide = true;
                        _colliders[j].IsCollide = true;
                    }
                }

                // Process (update and draw)
                _colliders[i].Process(ctx, dt);
            }
        }
    }

    internal abstract class Collider
    {
        private bool _isDragging;

        public abstract IShape Shape { get; }

        public bool IsCollide { get; set; }

        public abstract void Process(Graphics ctx, float dt);

        public abstract void Translate(Vector offset);

        internal bool OnMousePress(MouseButtonEvent ev)
        {
            if (Shape.ContainsPoint(ev.Position))
            {
                _isDragging = true;
                return true;
            }

            return false;
        }

        internal bool OnMouseRelease(MouseButtonEvent ev)
        {
            _isDragging = false;
            return false;
        }

        internal bool OnMouseScroll(MouseScrollEvent ev)
        {
            return false;
        }

        internal bool OnMouseMove(MouseMoveEvent ev)
        {
            if (_isDragging)
            {
                Translate(ev.Delta);
            }

            return false;
        }
    }

    internal sealed class PolygonCollider : Collider
    {
        private readonly Vector[] _localPolygon;
        private readonly Polygon _polygon;

        public PolygonCollider(int n, float r)
        {
            _localPolygon = Polygon.CreateStar(n, r).Vertices.ToArray(); // ehh...
            _polygon = new Polygon(_localPolygon);
        }

        public override IShape Shape => _polygon;

        public override void Process(Graphics ctx, float dt)
        {
            // Draw filled convex fragments
            for (var i = 0; i < _polygon.ConvexPartitions.Count; i++)
            {
                var convex = _polygon.ConvexPartitions[i];
                var hue = i / (float) _polygon.ConvexPartitions.Count * 360;

                ctx.Color = Color.FromHSV(hue, 0.75F, 1.0F, 0.5F);
                ctx.DrawPolygon(convex);
            }

            // Draw black outline
            ctx.Color = IsCollide ? Color.White : Color.Black;
            ctx.DrawPolygonOutline(_polygon, 2);
        }

        public override void Translate(Vector offset)
        {
            var m = Matrix.CreateTranslation(offset);

            for (var i = 0; i < _polygon.Count; i++)
            {
                _polygon[i] = m * _polygon[i];
            }
        }
    }

    internal sealed class CircleCollider : Collider
    {
        private Circle _circle;

        public CircleCollider(float radius)
        {
            _circle = new Circle(Vector.Zero, radius);
        }

        public override IShape Shape => _circle;

        public override void Process(Graphics ctx, float dt)
        {
            // Draw filled
            ctx.Color = Color.FromHSV(0, 0.75F, 1.0F, 0.5F);
            ctx.DrawCircle(_circle.Position, _circle.Radius);

            // Draw outline
            ctx.Color = IsCollide ? Color.White : Color.Black;
            ctx.DrawCircleOutline(_circle.Position, _circle.Radius, 2);
        }

        public override void Translate(Vector offset)
        {
            _circle.Position += offset;
        }
    }

    internal sealed class TriangleCollider : Collider
    {
        private Triangle _triangle;

        public TriangleCollider(float radius)
        {
            var a = Vector.FromAngle(Calc.TwoPi / 3 * 0) * radius;
            var b = Vector.FromAngle(Calc.TwoPi / 3 * 1) * radius;
            var c = Vector.FromAngle(Calc.TwoPi / 3 * 2) * radius;

            _triangle = new Triangle(a, b, c);
        }

        public override IShape Shape => _triangle;

        public override void Process(Graphics ctx, float dt)
        {
            // Draw filled
            ctx.Color = Color.FromHSV(30, 0.75F, 1.0F, 0.5F);
            ctx.DrawTriangle(_triangle.A, _triangle.B, _triangle.C);

            // Draw outline
            ctx.Color = IsCollide ? Color.White : Color.Black;
            ctx.DrawTriangleOutline(_triangle.A, _triangle.B, _triangle.C, 2);
        }

        public override void Translate(Vector offset)
        {
            _triangle.A += offset;
            _triangle.B += offset;
            _triangle.C += offset;
        }
    }

    internal sealed class RectangleCollider : Collider
    {
        private Rectangle _rect;

        public RectangleCollider(float radius)
        {
            _rect = new Rectangle(0, 0, 2 * radius, radius);
        }

        public override IShape Shape => _rect;

        public override void Process(Graphics ctx, float dt)
        {
            // Draw filled
            ctx.Color = Color.FromHSV(60, 0.75F, 1.0F, 0.5F);
            ctx.DrawRect(_rect);

            // Draw outline
            ctx.Color = IsCollide ? Color.White : Color.Black;
            ctx.DrawRectOutline(_rect, 2);
        }

        public override void Translate(Vector offset)
        {
            _rect.Position += offset;
        }
    }
}
