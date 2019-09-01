using Heirloom.Math;

namespace Heirloom.Drawing
{
    public enum NineSliceStyle
    {
        /// <summary>
        /// Middle segments are stretched to accomodate panel size.
        /// </summary>
        Stretch,

        /// <summary>
        /// Middle segments are repeated to accomodate panel size.
        /// </summary>
        Repeat
    }

    public sealed class NineSlice
    {
        public Image TopLeftImage { get; }

        public Image TopRightImage { get; }

        public Image TopMiddleImage { get; }

        public Image MiddleLeftImage { get; }

        public Image MiddleImage { get; }

        public Image MiddleRightImage { get; }

        public Image BottomLeftImage { get; }

        public Image BottomMiddleImage { get; }

        public Image BottomRightImage { get; }

        public NineSliceStyle Style { get; }

        public NineSlice(
            Image topLeft, Image topMiddle, Image topRight,
            Image middleLeft, Image middleMiddle, Image middleRight,
            Image bottomLeft, Image bottomMiddle, Image bottomRight, NineSliceStyle style = NineSliceStyle.Stretch)
        {
            TopLeftImage = topLeft;
            TopMiddleImage = topMiddle;
            TopRightImage = topRight;

            MiddleLeftImage = middleLeft;
            MiddleImage = middleMiddle;
            MiddleRightImage = middleRight;

            BottomLeftImage = bottomLeft;
            BottomMiddleImage = bottomMiddle;
            BottomRightImage = bottomRight;

            Style = style;
        }

        public static NineSlice Create(Image image, IntRectangle center, NineSliceStyle style = NineSliceStyle.Stretch)
        {
            var x0 = 0;
            var x1 = center.Left;
            var x2 = center.Right;
            var x3 = image.Width;

            var w0 = x1 - x0;
            var w1 = x2 - x1;
            var w2 = x3 - x2;

            var y0 = 0;
            var y1 = center.Top;
            var y2 = center.Bottom;
            var y3 = image.Height;

            var h0 = y1 - y0;
            var h1 = y2 - y1;
            var h2 = y3 - y2;

            // Create slices

            var TL = new Image(image, (x0, y0, w0, h0));
            var TM = new Image(image, (x1, y0, w1, h0));
            var TR = new Image(image, (x2, y0, w2, h0));

            var ML = new Image(image, (x0, y1, w0, h1));
            var MM = new Image(image, (x1, y1, w1, h1));
            var MR = new Image(image, (x2, y1, w2, h1));

            var BL = new Image(image, (x0, y2, w0, h2));
            var BM = new Image(image, (x1, y2, w1, h2));
            var BR = new Image(image, (x2, y2, w2, h2));

            // 
            return new NineSlice(TL, TM, TR, ML, MM, MR, BL, BM, BR, style);
        }
    }
}
