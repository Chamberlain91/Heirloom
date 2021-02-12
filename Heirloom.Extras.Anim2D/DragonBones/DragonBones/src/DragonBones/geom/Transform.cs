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
﻿using System;

namespace DragonBones
{
    /// <summary>
    /// - 2D Transform.
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 2D 变换。
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    public class Transform
    {
        /// <private/>
        public static readonly float PI = 3.141593f;
        /// <private/>
        public static readonly float PI_D = PI * 2.0f;
        /// <private/>
        public static readonly float PI_H = PI / 2.0f;
        /// <private/>
        public static readonly float PI_Q = PI / 4.0f;
        /// <private/>
        public static readonly float RAD_DEG = 180.0f / PI;
        /// <private/>
        public static readonly float DEG_RAD = PI / 180.0f;

        /// <private/>
        public static float NormalizeRadian(float value)
        {
            value = (value + PI) % (PI * 2.0f);

           
            value += value > 0.0f ? -PI : PI;

            return value;
        }

        /// <summary>
        /// - Horizontal translate.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 水平位移。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float x = 0.0f;
        /// <summary>
        /// - Vertical translate.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 垂直位移。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float y = 0.0f;
        /// <summary>
        /// - Skew. (In radians)
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 倾斜。 （以弧度为单位）
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float skew = 0.0f;
        /// <summary>
        /// - rotation. (In radians)
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 旋转。 （以弧度为单位）
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float rotation = 0.0f;
        /// <summary>
        /// - Horizontal Scaling.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 水平缩放。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float scaleX = 1.0f;
        /// <summary>
        /// - Vertical scaling.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 垂直缩放。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float scaleY = 1.0f;

        /// <private/>
        public Transform()
        {
            
        }

        public override string ToString()
        {
            return "[object dragonBones.Transform] x:" + x + " y:" + y + " skew:" + skew* 180.0 / PI + " rotation:" + rotation* 180.0 / PI + " scaleX:" + scaleX + " scaleY:" + scaleY;
        }

        /// <private/>
        public Transform CopyFrom(Transform value)
        {
            x = value.x;
            y = value.y;
            skew = value.skew;
            rotation = value.rotation;
            scaleX = value.scaleX;
            scaleY = value.scaleY;

            return this;
        }

        /// <private/>
        public Transform Identity()
        {
            x = y = 0.0f;
            skew = rotation = 0.0f;
            scaleX = scaleY = 1.0f;

            return this;
        }

        /// <private/>
        public Transform Add(Transform value)
        {
            x += value.x;
            y += value.y;
            skew += value.skew;
            rotation += value.rotation;
            scaleX *= value.scaleX;
            scaleY *= value.scaleY;

            return this;
        }

        /// <private/>
        public Transform Minus(Transform value)
        {
            x -= value.x;
            y -= value.y;
            skew -= value.skew;
            rotation -= value.rotation;
            scaleX /= value.scaleX;
            scaleY /= value.scaleY;

            return this;
        }

        /// <private/>
        public Transform FromMatrix(Matrix matrix)
        {
            var backupScaleX = scaleX;
            var backupScaleY = scaleY;

            x = matrix.Tx;
            y = matrix.Ty;

            var skewX = (float)Math.Atan(-matrix.C / matrix.D);
            rotation = (float)Math.Atan(matrix.B / matrix.A);

            if(float.IsNaN(skewX))
            {
                skewX = 0.0f;
            }

            if(float.IsNaN(rotation))
            {
                rotation = 0.0f; 
            }

            scaleX = (float)((rotation > -PI_Q && rotation < PI_Q) ? matrix.A / Math.Cos(rotation) : matrix.B / Math.Sin(rotation));
            scaleY = (float)((skewX > -PI_Q && skewX < PI_Q) ? matrix.D / Math.Cos(skewX) : -matrix.C / Math.Sin(skewX));

            if (backupScaleX >= 0.0f && scaleX < 0.0f)
            {
                scaleX = -scaleX;
                rotation = rotation - PI;
            }

            if (backupScaleY >= 0.0f && scaleY < 0.0f)
            {
                scaleY = -scaleY;
                skewX = skewX - PI;
            }

            skew = skewX - rotation;

            return this;
        }

        /// <private/>
        public Transform ToMatrix(Matrix matrix)
        {
            if(rotation == 0.0f)
            {
                matrix.A = 1.0f;
                matrix.B = 0.0f;
            }
            else
            {
                matrix.A = (float)Math.Cos(rotation);
                matrix.B = (float)Math.Sin(rotation);
            }

            if(skew == 0.0f)
            {
                matrix.C = -matrix.B;
                matrix.D = matrix.A;
            }
            else
            {
                matrix.C = -(float)Math.Sin(skew + rotation);
                matrix.D = (float)Math.Cos(skew + rotation);
            }

            if(scaleX != 1.0f)
            {
                matrix.A *= scaleX;
                matrix.B *= scaleX;
            }

            if(scaleY != 1.0f)
            {
                matrix.C *= scaleY;
                matrix.D *= scaleY;
            }

            matrix.Tx = x;
            matrix.Ty = y;

            return this;
        }
    }
}
