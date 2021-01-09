using System.Collections.Generic;

using Meadows.Mathematics;

namespace Meadows.UI
{
    public sealed class ClipStack
    {
        private static readonly Stack<Polygon> _stack = new();
        private Polygon _current;

        public Polygon Current => _current;

        public void Clear()
        {
            _current = null;
            _stack.Clear();
        }

        public void Push(IShape shape)
        {
            var polygon = new Polygon(shape.GetVertices());
            if (_current == null) { _current = polygon; }
            else
            {
                // Push old clip shape onto stack
                _stack.Push(_current);

                // Clip new shape against prior shape
                var clipped = Polygon.Clip(polygon.Vertices, _current.Vertices);
                _current = new Polygon(clipped);
            }
        }

        public void Pop()
        {
            // Either recover prior shape or none
            if (_stack.Count > 0) { _current = _stack.Pop(); }
            else { _current = null; }
        }
    }
}
