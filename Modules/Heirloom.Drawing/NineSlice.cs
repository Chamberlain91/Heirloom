using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public class NineSlice
    {
        public Image Image;

        public IntRectangle Center;

        public NineSlice(Image frame, IntRectangle center)
        {
            Image = frame ?? throw new ArgumentNullException(nameof(frame));
            Center = center;
        }
    }
}
