/**
 * The MIT License (MIT)
 *
 * Copyright (c) 2012-2017 DragonBones team and other contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;

namespace DragonBones
{
    /// <summary>
    /// - The rectangle bounding box data.
    /// </summary>
    /// <version>DragonBones 5.1</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 矩形边界框数据。
    /// </summary>
    /// <version>DragonBones 5.1</version>
    /// <language>zh_CN</language>
    internal class RectangleBoundingBoxData : BoundingBoxData
    {
        /// <summary>
        /// - Cohen–Sutherland algorithm https://en.wikipedia.org/wiki/Cohen%E2%80%93Sutherland_algorithm
        /// ----------------------
        /// | 0101 | 0100 | 0110 |
        /// ----------------------
        /// | 0001 | 0000 | 0010 |
        /// ----------------------
        /// | 1001 | 1000 | 1010 |
        /// ----------------------
        /// </summary>
        private enum OutCode
        {
            InSide = 0, // 0000
            Left = 1,   // 0001
            Right = 2,  // 0010
            Top = 4,    // 0100
            Bottom = 8  // 1000
        }

        /// <summary>
        /// - Compute the bit code for a point (x, y) using the clip rectangle
        /// </summary>
        private static int _ComputeOutCode(float x, float y, float xMin, float yMin, float xMax, float yMax)
        {
            var code = OutCode.InSide;  // initialised as being inside of [[clip window]]

            if (x < xMin)
            {             // to the left of clip window
                code |= OutCode.Left;
            }
            else if (x > xMax)
            {        // to the right of clip window
                code |= OutCode.Right;
            }

            if (y < yMin)
            {             // below the clip window
                code |= OutCode.Top;
            }
            else if (y > yMax)
            {        // above the clip window
                code |= OutCode.Bottom;
            }

            return (int) code;
        }
        /// <private/>
        public static int RectangleIntersectsSegment(float xA, float yA, float xB, float yB,
                                                        float xMin, float yMin, float xMax, float yMax,
                                                        Point intersectionPointA = null,
                                                        Point intersectionPointB = null,
                                                        Point normalRadians = null)
        {
            var inSideA = xA > xMin && xA < xMax && yA > yMin && yA < yMax;
            var inSideB = xB > xMin && xB < xMax && yB > yMin && yB < yMax;

            if (inSideA && inSideB)
            {
                return -1;
            }

            var intersectionCount = 0;
            var outcode0 = RectangleBoundingBoxData._ComputeOutCode(xA, yA, xMin, yMin, xMax, yMax);
            var outcode1 = RectangleBoundingBoxData._ComputeOutCode(xB, yB, xMin, yMin, xMax, yMax);

            while (true)
            {
                if ((outcode0 | outcode1) == 0)
                { // Bitwise OR is 0. Trivially accept and get out of loop
                    intersectionCount = 2;
                    break;
                }
                else if ((outcode0 & outcode1) != 0)
                { // Bitwise AND is not 0. Trivially reject and get out of loop
                    break;
                }

                // failed both tests, so calculate the line segment to clip
                // from an outside point to an intersection with clip edge
                var x = 0.0f;
                var y = 0.0f;
                var normalRadian = 0.0f;

                // At least one endpoint is outside the clip rectangle; pick it.
                var outcodeOut = outcode0 != 0 ? outcode0 : outcode1;

                // Now find the intersection point;
                if ((outcodeOut & (int) OutCode.Top) != 0)
                {             // point is above the clip rectangle
                    x = xA + (xB - xA) * (yMin - yA) / (yB - yA);
                    y = yMin;

                    if (normalRadians != null)
                    {
                        normalRadian = -(float) Math.PI * 0.5f;
                    }
                }
                else if ((outcodeOut & (int) OutCode.Bottom) != 0)
                {     // point is below the clip rectangle
                    x = xA + (xB - xA) * (yMax - yA) / (yB - yA);
                    y = yMax;

                    if (normalRadians != null)
                    {
                        normalRadian = (float) Math.PI * 0.5f;
                    }
                }
                else if ((outcodeOut & (int) OutCode.Right) != 0)
                {      // point is to the right of clip rectangle
                    y = yA + (yB - yA) * (xMax - xA) / (xB - xA);
                    x = xMax;

                    if (normalRadians != null)
                    {
                        normalRadian = 0;
                    }
                }
                else if ((outcodeOut & (int) OutCode.Left) != 0)
                {       // point is to the left of clip rectangle
                    y = yA + (yB - yA) * (xMin - xA) / (xB - xA);
                    x = xMin;

                    if (normalRadians != null)
                    {
                        normalRadian = (float) Math.PI;
                    }
                }

                // Now we move outside point to intersection point to clip
                // and get ready for next pass.
                if (outcodeOut == outcode0)
                {
                    xA = x;
                    yA = y;
                    outcode0 = RectangleBoundingBoxData._ComputeOutCode(xA, yA, xMin, yMin, xMax, yMax);

                    if (normalRadians != null)
                    {
                        normalRadians.X = normalRadian;
                    }
                }
                else
                {
                    xB = x;
                    yB = y;
                    outcode1 = RectangleBoundingBoxData._ComputeOutCode(xB, yB, xMin, yMin, xMax, yMax);

                    if (normalRadians != null)
                    {
                        normalRadians.Y = normalRadian;
                    }
                }
            }

            if (intersectionCount > 0)
            {
                if (inSideA)
                {
                    intersectionCount = 2; // 10

                    if (intersectionPointA != null)
                    {
                        intersectionPointA.X = xB;
                        intersectionPointA.Y = yB;
                    }

                    if (intersectionPointB != null)
                    {
                        intersectionPointB.X = xB;
                        intersectionPointB.Y = xB;
                    }

                    if (normalRadians != null)
                    {
                        normalRadians.X = normalRadians.Y + (float) Math.PI;
                    }
                }
                else if (inSideB)
                {
                    intersectionCount = 1; // 01

                    if (intersectionPointA != null)
                    {
                        intersectionPointA.X = xA;
                        intersectionPointA.Y = yA;
                    }

                    if (intersectionPointB != null)
                    {
                        intersectionPointB.X = xA;
                        intersectionPointB.Y = yA;
                    }

                    if (normalRadians != null)
                    {
                        normalRadians.Y = normalRadians.X + (float) Math.PI;
                    }
                }
                else
                {
                    intersectionCount = 3; // 11
                    if (intersectionPointA != null)
                    {
                        intersectionPointA.X = xA;
                        intersectionPointA.Y = yA;
                    }

                    if (intersectionPointB != null)
                    {
                        intersectionPointB.X = xB;
                        intersectionPointB.Y = yB;
                    }
                }
            }

            return intersectionCount;
        }
        /// <inheritDoc/>
        /// <private/>
        protected override void _OnClear()
        {
            base._OnClear();

            type = BoundingBoxType.Rectangle;
        }

        /// <inheritDoc/>
        public override bool ContainsPoint(float pX, float pY)
        {
            var widthH = width * 0.5f;
            if (pX >= -widthH && pX <= widthH)
            {
                var heightH = height * 0.5f;
                if (pY >= -heightH && pY <= heightH)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritDoc/>
        public override int IntersectsSegment(float xA, float yA, float xB, float yB,
                                             Point intersectionPointA = null,
                                             Point intersectionPointB = null,
                                             Point normalRadians = null)
        {
            var widthH = width * 0.5f;
            var heightH = height * 0.5f;
            var intersectionCount = RectangleBoundingBoxData.RectangleIntersectsSegment
            (
                xA, yA, xB, yB,
                -widthH, -heightH, widthH, heightH,
                intersectionPointA, intersectionPointB, normalRadians
            );

            return intersectionCount;
        }
    }
}
