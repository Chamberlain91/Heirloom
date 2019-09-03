using System;

using Heirloom.Math;

namespace Heirloom.Input
{
    public class MouseScrollEventArgs : EventArgs
    {
        public readonly float XOffset;

        public readonly float YOffset;

        public Vector Offset => new Vector(XOffset, YOffset);

        public MouseScrollEventArgs(float x, float y)
        {
            XOffset = x;
            YOffset = y;
        }
    }
}
