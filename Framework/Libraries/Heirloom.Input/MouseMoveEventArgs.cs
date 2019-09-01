using System;

using Heirloom.Math;

namespace Heirloom.Input
{
    public class MouseMoveEventArgs : EventArgs
    {
        public readonly float X;

        public readonly float Y;

        public Vector Position => new Vector(X, Y);

        public MouseMoveEventArgs(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
