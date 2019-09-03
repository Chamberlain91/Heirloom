using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Runtime
{
    public class SpriteRenderer : Renderer
    {
        private float _frameTime;
        private Sprite _sprite;
        private int _frame;

        public Sprite Sprite
        {
            get => _sprite;

            set
            {
                _sprite = value;
                _frameTime = 0;
                _frame = 0;
            }
        }

        public int Frame
        {
            get => _frame;

            set
            {
                _frameTime = 0;
                _frame = value % _sprite.Frames.Count;
            }
        }

        internal protected override void Update()
        {
            // No sprite set, exit
            if (_sprite == null) { return; }

            // If sprite has more than one frame
            else if (_sprite.Frames.Count > 1)
            {
                // Get current frame
                var frame = _sprite.Frames[_frame];

                // Advance time
                _frameTime += Time.Delta;

                // If enough time has elapsed
                if (_frameTime > frame.Delay)
                {
                    // Remove elapsed time from animation progress
                    _frameTime -= frame.Delay;

                    // Advance frame (wrapping around to zero)
                    _frame++;
                    if (_frame >= _sprite.Frames.Count)
                    {
                        _frame = 0;
                    }
                }
            }
        }

        internal protected override void Render(RenderContext ctx)
        {
            if (_sprite == null) { return; }
            else
            {
                // Draw sprite
                ctx.Draw(_sprite, _frame, Transform.Matrix, Color);
            }
        }
    }

    public static class SpriteDrawingExtensions
    {
        public static void Draw(this RenderContext ctx, Sprite sprite, int frameNumber, Matrix matrix, Color color)
        {
            var frame = sprite.Frames[frameNumber]; // wrap frame range?

            // If sprite has a non-zero origin
            if (sprite.HasCustomOrigin)
            {
                // Move about origin, transform as given and move back
                // todo: can cache translation matrices in sprite class
                matrix *= Matrix.CreateTranslation(-sprite.Origin);
            }

            // 
            ctx.Draw(frame.Image, matrix, color);
        }
    }
}
