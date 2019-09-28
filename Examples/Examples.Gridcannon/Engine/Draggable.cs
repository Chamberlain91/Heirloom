using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Gridcannon.Engine
{
    public class Draggable : Entity
    {
        private bool _isMouseDown = false;

        public Draggable(Image image)
            : base(image)
        { }

        internal override bool OnMouseClick(int button, bool isDown, Vector position)
        {
            if (button == 0)
            {
                // Assume the card was not clicked on
                _isMouseDown = false;
                var consume = false;

                // Check if the click was within bounds
                if (Bounds.Contains(position))
                {
                    // Was clicked on
                    _isMouseDown = isDown;
                    consume = true;
                }

                // Changing the depth to one temporarily causes card entity to reorder and
                // be on top of the other cards. When depth is set back to zero, it remains 
                // in the same order because of the stable depth ordering
                Depth = _isMouseDown ? 1 : 0;

                return consume;
            }

            return false; // Did not consume the click
        }

        internal override bool OnMouseMove(Vector position, Vector delta)
        {
            if (_isMouseDown)
            {
                Transform.Position += delta;
            }

            return false;
        }
    }
}
