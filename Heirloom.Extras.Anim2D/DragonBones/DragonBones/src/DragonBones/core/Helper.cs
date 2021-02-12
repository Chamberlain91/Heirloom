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
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace DragonBones
{
    internal static class Helper
    {
        public static readonly int INT16_SIZE = 2;
        public static readonly int UINT16_SIZE = 2;
        public static readonly int FLOAT_SIZE = 4;

        internal static void Assert(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }

        internal static void ResizeList<T>(this List<T> list, int count, T value = default)
        {
            if (list.Count == count)
            {
                return;
            }

            if (list.Count > count)
            {
                list.RemoveRange(count, list.Count - count);
            }
            else
            {
                //fixed gc,may be memory will grow
                //list.Capacity = count;
                for (int i = list.Count, l = count; i < l; ++i)
                {
                    list.Add(value);
                }
            }
        }

        internal static List<float> Convert(this List<object> list)
        {
            var res = new List<float>();

            for (var i = 0; i < list.Count; i++)
            {
                res[i] = float.Parse(list[i].ToString());
            }

            return res;
        }
        internal static bool FloatEqual(float f0, float f1)
        {
            var f = Math.Abs(f0 - f1);

            return (f < 0.000000001f);
        }
    }
}
