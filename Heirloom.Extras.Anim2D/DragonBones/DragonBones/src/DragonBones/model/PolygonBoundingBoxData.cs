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
using System.Collections.Generic;

namespace DragonBones
{
    /// <summary>
    /// - The polygon bounding box data.
    /// </summary>
    /// <version>DragonBones 5.1</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 多边形边界框数据。
    /// </summary>
    /// <version>DragonBones 5.1</version>
    /// <language>zh_CN</language>
    internal class PolygonBoundingBoxData : BoundingBoxData
    {
        /// <private/>
        public static int PolygonIntersectsSegment(float xA, float yA, float xB, float yB,
                                                    List<float> vertices,
                                                    Point intersectionPointA = null,
                                                    Point intersectionPointB = null,
                                                    Point normalRadians = null)
        {
            if (xA == xB)
            {
                xA = xB + 0.01f;
            }

            if (yA == yB)
            {
                yA = yB + 0.01f;
            }

            var l = vertices.Count;
            var dXAB = xA - xB;
            var dYAB = yA - yB;
            var llAB = xA * yB - yA * xB;
            int intersectionCount = 0;
            var xC = vertices[l - 2];
            var yC = vertices[l - 1];
            var dMin = 0.0f;
            var dMax = 0.0f;
            var xMin = 0.0f;
            var yMin = 0.0f;
            var xMax = 0.0f;
            var yMax = 0.0f;

            for (int i = 0; i < l; i += 2)
            {
                var xD = vertices[i];
                var yD = vertices[i + 1];

                if (xC == xD)
                {
                    xC = xD + 0.01f;
                }

                if (yC == yD)
                {
                    yC = yD + 0.01f;
                }

                var dXCD = xC - xD;
                var dYCD = yC - yD;
                var llCD = xC * yD - yC * xD;
                var ll = dXAB * dYCD - dYAB * dXCD;
                var x = (llAB * dXCD - dXAB * llCD) / ll;

                if (((x >= xC && x <= xD) || (x >= xD && x <= xC)) && (dXAB == 0 || (x >= xA && x <= xB) || (x >= xB && x <= xA)))
                {
                    var y = (llAB * dYCD - dYAB * llCD) / ll;
                    if (((y >= yC && y <= yD) || (y >= yD && y <= yC)) && (dYAB == 0 || (y >= yA && y <= yB) || (y >= yB && y <= yA)))
                    {
                        if (intersectionPointB != null)
                        {
                            var d = x - xA;
                            if (d < 0.0f)
                            {
                                d = -d;
                            }

                            if (intersectionCount == 0)
                            {
                                dMin = d;
                                dMax = d;
                                xMin = x;
                                yMin = y;
                                xMax = x;
                                yMax = y;

                                if (normalRadians != null)
                                {
                                    normalRadians.X = (float)Math.Atan2(yD - yC, xD - xC) - (float)Math.PI * 0.5f;
                                    normalRadians.Y = normalRadians.X;
                                }
                            }
                            else
                            {
                                if (d < dMin)
                                {
                                    dMin = d;
                                    xMin = x;
                                    yMin = y;

                                    if (normalRadians != null)
                                    {
                                        normalRadians.X = (float)Math.Atan2(yD - yC, xD - xC) - (float)Math.PI * 0.5f;
                                    }
                                }

                                if (d > dMax)
                                {
                                    dMax = d;
                                    xMax = x;
                                    yMax = y;

                                    if (normalRadians != null)
                                    {
                                        normalRadians.Y = (float)Math.Atan2(yD - yC, xD - xC) - (float)Math.PI * 0.5f;
                                    }
                                }
                            }

                            intersectionCount++;
                        }
                        else
                        {
                            xMin = x;
                            yMin = y;
                            xMax = x;
                            yMax = y;
                            intersectionCount++;

                            if (normalRadians != null)
                            {
                                normalRadians.X = (float)Math.Atan2(yD - yC, xD - xC) - (float)Math.PI * 0.5f;
                                normalRadians.Y = normalRadians.X;
                            }
                            break;
                        }
                    }
                }

                xC = xD;
                yC = yD;
            }

            if (intersectionCount == 1)
            {
                if (intersectionPointA != null)
                {
                    intersectionPointA.X = xMin;
                    intersectionPointA.Y = yMin;
                }

                if (intersectionPointB != null)
                {
                    intersectionPointB.X = xMin;
                    intersectionPointB.Y = yMin;
                }

                if (normalRadians != null)
                {
                    normalRadians.Y = normalRadians.X + (float)Math.PI;
                }
            }
            else if (intersectionCount > 1)
            {
                intersectionCount++;

                if (intersectionPointA != null)
                {
                    intersectionPointA.X = xMin;
                    intersectionPointA.Y = yMin;
                }

                if (intersectionPointB != null)
                {
                    intersectionPointB.X = xMax;
                    intersectionPointB.Y = yMax;
                }
            }

            return intersectionCount;
        }

        /// <private/>
        public float x;
        /// <private/>
        public float y;
        /// <summary>
        /// - The polygon vertices.
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 多边形顶点。
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>zh_CN</language>
        public readonly List<float> vertices = new List<float>();

        /// <inheritDoc/>
        /// <private/>
        protected override void _OnClear()
        {
            base._OnClear();

            type = BoundingBoxType.Polygon;
            x = 0.0f;
            y = 0.0f;
            vertices.Clear();
        }

        /// <inheritDoc/>
        public override bool ContainsPoint(float pX, float pY)
        {
            var isInSide = false;
            if (pX >= x && pX <= width && pY >= y && pY <= height)
            {
                for (int i = 0, l = vertices.Count, iP = l - 2; i < l; i += 2)
                {
                    var yA = vertices[iP + 1];
                    var yB = vertices[i + 1];
                    if ((yB < pY && yA >= pY) || (yA < pY && yB >= pY))
                    {
                        var xA = vertices[iP];
                        var xB = vertices[i];
                        if ((pY - yB) * (xA - xB) / (yA - yB) + xB < pX)
                        {
                            isInSide = !isInSide;
                        }
                    }

                    iP = i;
                }
            }

            return isInSide;
        }

        /// <inheritDoc/>
        public override int IntersectsSegment(float xA, float yA, float xB, float yB,
                                                Point intersectionPointA = null,
                                                Point intersectionPointB = null,
                                                Point normalRadians = null)
        {
            var intersectionCount = 0;
            if (RectangleBoundingBoxData.RectangleIntersectsSegment(xA, yA, xB, yB, x, y, x + width, y + height, null, null, null) != 0)
            {
                intersectionCount = PolygonBoundingBoxData.PolygonIntersectsSegment
                                                            (
                                                             xA, yA, xB, yB,
                                                             vertices,
                                                             intersectionPointA, intersectionPointB, normalRadians
                                                            );
            }

            return intersectionCount;
        }
    }
}
