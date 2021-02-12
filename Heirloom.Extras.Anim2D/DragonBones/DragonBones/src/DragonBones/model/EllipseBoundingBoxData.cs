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
    /// - The ellipse bounding box data.
    /// </summary>
    /// <version>DragonBones 5.1</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 椭圆边界框数据。
    /// </summary>
    /// <version>DragonBones 5.1</version>
    /// <language>zh_CN</language>
    internal class EllipseBoundingBoxData : BoundingBoxData
    {
        /// <private/>
        public static int EllipseIntersectsSegment(float xA, float yA, float xB, float yB,
                                                    float xC, float yC, float widthH, float heightH,
                                                    Point intersectionPointA = null,
                                                    Point intersectionPointB = null,
                                                    Point normalRadians = null)
        {
            var d = widthH / heightH;
            var dd = d * d;

            yA *= d;
            yB *= d;

            var dX = xB - xA;
            var dY = yB - yA;
            var lAB = (float)Math.Sqrt(dX * dX + dY * dY);
            var xD = dX / lAB;
            var yD = dY / lAB;
            var a = (xC - xA) * xD + (yC - yA) * yD;
            var aa = a * a;
            var ee = xA * xA + yA * yA;
            var rr = widthH * widthH;
            var dR = rr - ee + aa;
            var intersectionCount = 0;

            if (dR >= 0.0f)
            {
                var dT = (float)Math.Sqrt(dR);
                var sA = a - dT;
                var sB = a + dT;
                var inSideA = sA < 0.0 ? -1 : (sA <= lAB ? 0 : 1);
                var inSideB = sB < 0.0 ? -1 : (sB <= lAB ? 0 : 1);
                var sideAB = inSideA * inSideB;

                if (sideAB < 0)
                {
                    return -1;
                }
                else if (sideAB == 0)
                {
                    if (inSideA == -1)
                    {
                        intersectionCount = 2; // 10
                        xB = xA + sB * xD;
                        yB = (yA + sB * yD) / d;

                        if (intersectionPointA != null)
                        {
                            intersectionPointA.X = xB;
                            intersectionPointA.Y = yB;
                        }

                        if (intersectionPointB != null)
                        {
                            intersectionPointB.X = xB;
                            intersectionPointB.Y = yB;
                        }

                        if (normalRadians != null)
                        {
                            normalRadians.X = (float)Math.Atan2(yB / rr * dd, xB / rr);
                            normalRadians.Y = normalRadians.X + (float)Math.PI;
                        }
                    }
                    else if (inSideB == 1)
                    {
                        intersectionCount = 1; // 01
                        xA = xA + sA * xD;
                        yA = (yA + sA * yD) / d;

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
                            normalRadians.X = (float)Math.Atan2(yA / rr * dd, xA / rr);
                            normalRadians.Y = normalRadians.X + (float)Math.PI;
                        }
                    }
                    else
                    {
                        intersectionCount = 3; // 11

                        if (intersectionPointA != null)
                        {
                            intersectionPointA.X = xA + sA * xD;
                            intersectionPointA.Y = (yA + sA * yD) / d;

                            if (normalRadians != null)
                            {
                                normalRadians.X = (float)Math.Atan2(intersectionPointA.Y / rr * dd, intersectionPointA.X / rr);
                            }
                        }

                        if (intersectionPointB != null)
                        {
                            intersectionPointB.X = xA + sB * xD;
                            intersectionPointB.Y = (yA + sB * yD) / d;

                            if (normalRadians != null)
                            {
                                normalRadians.Y = (float)Math.Atan2(intersectionPointB.Y / rr * dd, intersectionPointB.X / rr);
                            }
                        }
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

            type = BoundingBoxType.Ellipse;
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
                    pY *= widthH / heightH;
                    return Math.Sqrt(pX * pX + pY * pY) <= widthH;
                }
            }

            return false;
        }

        /// <inheritDoc/>
        public override int IntersectsSegment(float xA, float yA, float xB, float yB,
                                                Point intersectionPointA,
                                                Point intersectionPointB,
                                                Point normalRadians)
        {
            var intersectionCount = EllipseBoundingBoxData.EllipseIntersectsSegment(xA, yA, xB, yB,
                                                                                    0.0f, 0.0f, width * 0.5f, height * 0.5f,
                                                                                    intersectionPointA, intersectionPointB, normalRadians);

            return intersectionCount;
        }
    }
}
