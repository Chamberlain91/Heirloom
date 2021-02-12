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

namespace DragonBones
{
    /// <summary>
    /// - Animation fade out mode.
    /// </summary>
    /// <version>DragonBones 4.5</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 动画淡出模式。
    /// </summary>
    /// <version>DragonBones 4.5</version>
    /// <language>zh_CN</language>
    internal enum AnimationFadeOutMode
    {
        /// <summary>
        /// - Do not fade out of any animation states.
        /// </summary>
        /// <language>en_US</language>

        /// <summary>
        /// - 不淡出任何的动画状态。
        /// </summary>
        /// <language>zh_CN</language>
        None = 0,
        /// <summary>
        /// - Fade out the animation states of the same layer.
        /// </summary>
        /// <language>en_US</language>

        /// <summary>
        /// - 淡出同层的动画状态。
        /// </summary>
        /// <language>zh_CN</language>
        SameLayer = 1,
        /// <summary>
        /// - Fade out the animation states of the same group.
        /// </summary>
        /// <language>en_US</language>

        /// <summary>
        /// - 淡出同组的动画状态。
        /// </summary>
        /// <language>zh_CN</language>
        SameGroup = 2,
        /// <summary>
        /// - Fade out the animation states of the same layer and group.
        /// </summary>
        /// <language>en_US</language>

        /// <summary>
        /// - 淡出同层并且同组的动画状态。
        /// </summary>
        /// <language>zh_CN</language>
        SameLayerAndGroup = 3,
        /// <summary>
        /// - Fade out of all animation states.
        /// </summary>
        /// <language>en_US</language>

        /// <summary>
        /// - 淡出所有的动画状态。
        /// </summary>
        /// <language>zh_CN</language>
        All = 4,
        /// <summary>
        /// - Does not replace the animation state with the same name.
        /// </summary>
        /// <language>en_US</language>

        /// <summary>
        /// - 不替换同名的动画状态。
        /// </summary>
        /// <language>zh_CN</language>
        Single = 5
    }
}
