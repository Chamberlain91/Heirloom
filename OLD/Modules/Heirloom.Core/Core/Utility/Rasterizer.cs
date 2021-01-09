using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// Contains rasterization methods for iterating over pixel positions.
    /// </summary>
    /// <remarks>
    /// This is useful beyond drawing images. For example, in a city builder the <see cref="Rectangle(IntRectangle)"/>
    /// or <see cref="Line(IntVector, IntVector)"/> commands can be used to compute positions to place tiles when drawing
    /// building plots or roads.
    /// </remarks>
    /// <category>Utility</category>
    public static class Rasterizer
    {
        #region Rectangle

        /// <summary>
        /// Rasterize a rectangular region.
        /// </summary>
        public static IEnumerable<IntVector> Rectangle(int x, int y, int width, int height)
        {
            // 
            for (var yy = 0; yy < height; yy++)
            {
                for (var xx = 0; xx < width; xx++)
                {
                    yield return new IntVector(xx + x, yy + y);
                }
            }
        }

        /// <summary>
        /// Rasterize a rectangular region.
        /// </summary>
        public static IEnumerable<IntVector> Rectangle(IntRectangle rect)
        {
            return Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// Rasterize a rectangular region.
        /// </summary>
        public static IEnumerable<IntVector> Rectangle(IntVector position, IntSize size)
        {
            return Rectangle(position.X, position.Y, size.Width, size.Height);
        }

        /// <summary>
        /// Rasterize a rectangular region.
        /// </summary>
        public static IEnumerable<IntVector> Rectangle(IntSize size)
        {
            return Rectangle(0, 0, size.Width, size.Height);
        }

        /// <summary>
        /// Rasterize the outline of a rectangular region.
        /// </summary>
        public static IEnumerable<IntVector> RectangleOutline(int x, int y, int width, int height)
        {
            // Vertical
            for (var yy = 0; yy < height; yy++)
            {
                yield return new IntVector(x, yy + y);
                yield return new IntVector(x + width - 1, yy + y);
            }

            // Horizontal
            for (var xx = 0; xx < width; xx++)
            {
                yield return new IntVector(xx + x, y);
                yield return new IntVector(xx + x, y + height - 1);
            }
        }

        #endregion

        #region Circle

        /// <summary>
        /// Rasterizes a filled circle.
        /// </summary>
        /// <param name="cx">The center x coordinate of the circle.</param>
        /// <param name="cy">The center y coordinate of the circle.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <returns>Generated points within the circle.</returns>
        public static IEnumerable<IntVector> Circle(int cx, int cy, int r)
        {
            var ox = 0;
            var oy = r;

            var d = 3 - 2 * r;

            // 
            foreach (var co in HLine(cx - ox, cx + ox, cy - oy)) { yield return co; }
            foreach (var co in HLine(cx - ox, cx + ox, cy + oy)) { yield return co; }
            foreach (var co in HLine(cx - oy, cx + oy, cy - ox)) { yield return co; }
            foreach (var co in HLine(cx - oy, cx + oy, cy + ox)) { yield return co; }

            while (oy >= ox)
            {
                // for each pixel we will 
                // draw all eight pixels 

                ox++;

                // check for decision parameter 
                // and correspondingly  
                // update d, x, y 
                if (d > 0)
                {
                    oy--;
                    d = d + 4 * (ox - oy) + 10;
                }
                else
                {
                    d = d + 4 * ox + 6;
                }

                // Rasterizer 4 layers
                foreach (var co in HLine(cx - ox, cx + ox, cy - oy)) { yield return co; }
                foreach (var co in HLine(cx - ox, cx + ox, cy + oy)) { yield return co; }
                foreach (var co in HLine(cx - oy, cx + oy, cy - ox)) { yield return co; }
                foreach (var co in HLine(cx - oy, cx + oy, cy + ox)) { yield return co; }
            }
        }

        /// <summary>
        /// Rasterizes a circle outline.
        /// </summary>
        /// <param name="cx">The center x coordinate of the circle.</param>
        /// <param name="cy">The center y coordinate of the circle.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <returns>Generated points on the shell of the circle.</returns>
        public static IEnumerable<IntVector> CircleOutline(int cx, int cy, int r)
        {
            var ox = 0;
            var oy = r;

            var d = 3 - 2 * r;

            yield return (cx + ox, cy + oy);
            yield return (cx - ox, cy + oy);
            yield return (cx + ox, cy - oy);
            yield return (cx - ox, cy - oy);
            yield return (cx + oy, cy + ox);
            yield return (cx - oy, cy + ox);
            yield return (cx + oy, cy - ox);
            yield return (cx - oy, cy - ox);

            while (oy >= ox)
            {
                // for each pixel we will 
                // draw all eight pixels 

                ox++;

                // check for decision parameter 
                // and correspondingly  
                // update d, x, y 
                if (d > 0)
                {
                    oy--;
                    d = d + 4 * (ox - oy) + 10;
                }
                else
                {
                    d = d + 4 * ox + 6;
                }

                yield return (cx + ox, cy + oy);
                yield return (cx - ox, cy + oy);
                yield return (cx + ox, cy - oy);
                yield return (cx - ox, cy - oy);
                yield return (cx + oy, cy + ox);
                yield return (cx - oy, cy + ox);
                yield return (cx + oy, cy - ox);
                yield return (cx - oy, cy - ox);
            }
        }

        #endregion

        #region Triangle

        /// <summary>
        /// Rasterize a triangle.
        /// </summary>
        public static IEnumerable<IntVector> Triangle(IntVector p0, IntVector p1, IntVector p2)
        // https://github.com/ssloy/tinyrenderer/wiki/Lesson-2:-Triangle-rasterization-and-back-face-culling
        {
            // Degenerate point triangle case
            if (p0.Y == p1.Y && p0.Y == p2.Y) { yield break; }

            // Sort vertices
            if (p0.Y > p1.Y) { Calc.Swap(ref p0, ref p1); }
            if (p0.Y > p2.Y) { Calc.Swap(ref p0, ref p2); }
            if (p1.Y > p2.Y) { Calc.Swap(ref p1, ref p2); }

            var total_height = p2.Y - p0.Y;
            for (var i = 0; i < total_height; i++)
            {
                var second_half = i > p1.Y - p0.Y || p1.Y == p0.Y;
                var segment_height = second_half ? p2.Y - p1.Y : p1.Y - p0.Y;
                var offset = i - (second_half ? p1.Y - p0.Y : 0);

                var A = p0 + (p2 - p0) * i / total_height;
                var B = second_half
                    ? p1 + (p2 - p1) * offset / segment_height
                    : p0 + (p1 - p0) * offset / segment_height;

                // Swap line coordinates
                if (A.X > B.X) { Calc.Swap(ref A, ref B); }

                // Draw horizontal line
                for (var j = A.X; j < B.X; j++)
                {
                    yield return new IntVector(j, p0.Y + i);
                }
            }
        }

        #endregion

        // todo: Curves

        #region Line

        /// <summary>
        /// Rasterize along a line.
        /// </summary>
        /// <param name="p0">Starting point.</param>
        /// <param name="p1">Ending point.</param>
        public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1)
        {
            return Line(p0, p1, uint.MaxValue);
        }

        /// <summary>
        /// Rasterize along a line.
        /// </summary>
        /// <param name="p0">Starting point.</param>
        /// <param name="p1">Ending point.</param>
        /// <param name="pattern">Sequence of bits to mask drawing the line.</param>
        public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1, byte pattern)
        {
            var _pattern = (uint) (pattern | (pattern << 8) | (pattern << 16) | (pattern << 24));
            return Line(p0, p1, _pattern);
        }

        /// <summary>
        /// Rasterize along a line.
        /// </summary>
        /// <param name="p0">Starting point.</param>
        /// <param name="p1">Ending point.</param>
        /// <param name="pattern">Sequence of bits to mask drawing the line.</param>
        public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1, ushort pattern)
        {
            var _pattern = (uint) (pattern | (pattern << 16));
            return Line(p0, p1, _pattern);
        }

        /// <summary>
        /// Rasterize along a line.
        /// </summary>
        /// <param name="p0">Starting point.</param>
        /// <param name="p1">Ending point.</param>
        /// <param name="pattern">Sequence of bits to mask drawing the line.</param>
        public static IEnumerable<IntVector> Line(IntVector p0, IntVector p1, uint pattern)
        // http://members.chello.at/~easyfilter/bresenham.html
        {
            var x0 = p0.X;
            var x1 = p1.X;

            var y0 = p0.Y;
            var y1 = p1.Y;

            int dx = Calc.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Calc.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2; /* error value e_xy */

            while (true)
            {
                pattern = (pattern << 1) | (pattern >> 31);
                if ((pattern & 1) == 1) { yield return new IntVector(x0, y0); }

                if (x0 == x1 && y0 == y1) { break; }
                e2 = 2 * err;
                if (e2 >= dy) { err += dy; x0 += sx; } /* e_xy + e_x > 0 */
                if (e2 <= dx) { err += dx; y0 += sy; } /* e_xy + e_y < 0 */
            }
        }

        /// <summary>
        /// Iterate over a perfectly horizontal line.
        /// </summary>
        /// <param name="x1">Line start x coordinate.</param>
        /// <param name="x2">Line end x coordinate.</param>
        /// <param name="y">Line y coordinate.</param>
        /// <returns>Generated points along the line.</returns>
        public static IEnumerable<IntVector> HLine(int x1, int x2, int y)
        {
            Calc.Order(ref x1, ref x2);

            for (var x = x1; x < x2; x++)
            {
                yield return new IntVector(x, y);
            }
        }

        /// <summary>
        /// Iterate over a perfectly vertical line.
        /// </summary>
        /// <param name="y1">Line start y coordinate.</param>
        /// <param name="y2">Line end y coordinate.</param>
        /// <param name="x">Line x coordinate.</param>
        /// <returns>Generated points along the line.</returns>
        public static IEnumerable<IntVector> VLine(int y1, int y2, int x)
        {
            Calc.Order(ref y1, ref y2);

            for (var y = y1; y < y2; y++)
            {
                yield return new IntVector(x, y);
            }
        }

        #endregion
    }
}
