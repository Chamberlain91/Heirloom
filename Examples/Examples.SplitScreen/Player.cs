using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.SplitScreen
{
    internal sealed class Player
    {
        private float _speed, _rotation;
        private Vector _position;

        private readonly Dictionary<Input, bool> _inputState = new Dictionary<Input, bool>();

        public enum Input
        {
            SpinLeft, SpinRight,
            Forward, Reverse
        }

        public Player(Vector position, float rotation, Color color)
        {
            _position = position;
            _rotation = rotation;
            Color = color;
        }

        public Vector SmoothPosition { get; private set; }

        public Color Color { get; }

        public void Update(float dt)
        {
            // Set Speed
            _speed = 0F;
            if (CheckInput(Input.Forward)) { _speed += 150F; }
            if (CheckInput(Input.Reverse)) { _speed -= 150F; }

            // Rotate
            if (CheckInput(Input.SpinLeft)) { _rotation -= 135 * Calc.ToRadians * dt; }
            if (CheckInput(Input.SpinRight)) { _rotation += 135 * Calc.ToRadians * dt; }

            // Animate Position
            SmoothPosition = Vector.Lerp(SmoothPosition, _position, dt * 3F);
            _position += Vector.FromAngle(_rotation) * _speed * dt;

            // Hard stop at world bounds
            if (_position.Length > SplitScreen.StageRadius)
            {
                _position.Normalize();
                _position *= SplitScreen.StageRadius;
            }
        }

        public void Draw(Graphics gfx)
        {
            const float TriangleAngle = 120F * Calc.ToRadians;

            // Compute player triangle
            var v0 = _position + Vector.FromAngle(_rotation + TriangleAngle * 0) * 32;
            var v1 = _position + Vector.FromAngle(_rotation + TriangleAngle * 1) * 24;
            var v2 = _position + Vector.FromAngle(_rotation + TriangleAngle * 2) * 24;

            // Draw triangle
            gfx.Color = Color;
            gfx.DrawTriangle(v0, v1, v2);

            // Draw triangle outline
            gfx.Color = Color * Color.DarkGray;
            gfx.DrawTriangleOutline(v0, v1, v2, 3);
        }

        internal void OnInput(bool isDown, Input key)
        {
            _inputState[key] = isDown;
        }

        internal bool CheckInput(Input key)
        {
            // Try to get most recent state
            if (_inputState.TryGetValue(key, out var isDown)) { return isDown; }
            // Key was never pressed
            else { return false; }
        }
    }
}
