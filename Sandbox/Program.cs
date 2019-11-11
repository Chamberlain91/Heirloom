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
        private static List<Shape> _shapes;

        private static void Main(string[] args)
        {
            // 
            _mouseButtonEvents = new Queue<MouseButtonEvent>();
            _mouseScrollEvents = new Queue<MouseScrollEvent>();
            _mouseMoveEvents = new Queue<MouseMoveEvent>();

            // 
            _shapes = new List<Shape>();

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
                var loop = RenderLoop.Create(_window.RenderContext, OnUpdate);
                loop.Start();

                // Add objects
                Add(new Shape(3, 48, 200));
                Add(new Shape(4, 64, 400));
                Add(new Shape(5, 96, 600));
            });
        }

        internal static void Add(Shape obj)
        {
            _shapes.Add(obj);
        }

        private static void ProcessMouseButtonEvent(MouseButtonEvent e)
        {
            if (e.Action == ButtonAction.Press)
            {
                for (var i = _shapes.Count - 1; i >= 0; i--)
                {
                    // Apply mouse press to object
                    if (_shapes[i].OnMousePress(e)) { return; }
                }
            }
            else
            {
                for (var i = _shapes.Count - 1; i >= 0; i--)
                {
                    // Apply mouse release to object
                    if (_shapes[i].OnMouseRelease(e)) { return; }
                }
            }
        }

        private static void ProcessMouseMoveEvent(MouseMoveEvent e)
        {
            for (var i = _shapes.Count - 1; i >= 0; i--)
            {
                // Apply mouse release to object
                if (_shapes[i].OnMouseMove(e)) { return; }
            }
        }

        private static void ProcessMouseScrollEvent(MouseScrollEvent e)
        {
            for (var i = _shapes.Count - 1; i >= 0; i--)
            {
                // Apply mouse release to object
                if (_shapes[i].OnMouseScroll(e)) { return; }
            }
        }

        private static void OnUpdate(RenderContext ctx, float dt)
        {
            lock (_window)
            {
                // Process mouse input events
                while (_mouseMoveEvents.Count > 0) { ProcessMouseMoveEvent(_mouseMoveEvents.Dequeue()); }
                while (_mouseButtonEvents.Count > 0) { ProcessMouseButtonEvent(_mouseButtonEvents.Dequeue()); }
                while (_mouseScrollEvents.Count > 0) { ProcessMouseScrollEvent(_mouseScrollEvents.Dequeue()); }
            }

            ctx.Clear(Color.DarkGray);

            // 
            for (var i = 0; i < _shapes.Count; i++)
            {
                var s0 = _shapes[i];

                // 
                for (var j = i + 1; j < _shapes.Count; j++)
                {
                    var s1 = _shapes[j];

                    // 
                    foreach (var c0 in s0.Polygon.ConvexFragments)
                    {
                        foreach (var c1 in s1.Polygon.ConvexFragments)
                        {
                            if (c0.CheckCollision(c1, out var contacts))
                            {
                                foreach (var contact in contacts)
                                {
                                    // Compute opposite contact
                                    var contactInverse = new RayContact(contact.Position, contact.Normal, -contact.Depth);

                                    s0.Contacts.Add(contact);
                                    s1.Contacts.Add(contactInverse);
                                }
                            }
                        }
                    }
                }

                // 
                _shapes[i].Process(ctx, dt);
                _shapes[i].Contacts.Clear();
            }
        }
    }

    public class Shape
    {
        private bool _isDragging;

        private readonly Vector[] _localPolygon;

        public List<RayContact> Contacts { get; }

        public Shape(int n, float r, float x)
        {
            _localPolygon = Polygon.CreateStar(n, r).Vertices.ToArray(); // ehh...

            var trans = Matrix.CreateTranslation(x, 200);
            Polygon = new Polygon(_localPolygon);
            for (var i = 0; i < Polygon.Count; i++)
            {
                Polygon[i] = trans * _localPolygon[i];
            }

            Contacts = new List<RayContact>();
        }

        public Polygon Polygon { get; }

        public void Process(RenderContext ctx, float dt)
        {
            // Draw filled convex fragments
            for (var i = 0; i < Polygon.ConvexFragments.Count; i++)
            {
                var convex = Polygon.ConvexFragments[i];
                var hue = i / (float) Polygon.ConvexFragments.Count * 360;

                ctx.Color = Color.FromHSV(hue, 0.75F, 1.0F, 0.5F);
                ctx.DrawPolygon(convex);
            }

            // Draw black outline
            ctx.Color = Contacts.Count > 0 ? Color.Gray : Color.Black;
            ctx.DrawPolygonOutline(Polygon, 2);

            if (Contacts.Count > 0)
            {
                // 
                var C = default(RayContact);
                var D = float.MaxValue;

                foreach (var c in Contacts)
                {
                    ctx.Color = Color.Yellow;
                    ctx.DrawCross(c.Position, 4);

                    ctx.Color = Color.Cyan;
                    ctx.DrawLine(c.Position, c.Position + c.Normal * c.Distance);

                    if (c.Distance < D)
                    {
                        C = c;
                        D = c.Distance;
                    }
                }

                // Polygon.Transform(Matrix.CreateTranslation(C.Normal * C.Depth));
            }
        }

        internal bool OnMousePress(MouseButtonEvent ev)
        {
            if (Polygon.ContainsPoint(ev.Position))
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
                var trans = Matrix.CreateTranslation(ev.Delta);
                for (var i = 0; i < Polygon.Count; i++)
                {
                    Polygon[i] = trans * Polygon[i];
                }
            }

            return false;
        }
    }
}
