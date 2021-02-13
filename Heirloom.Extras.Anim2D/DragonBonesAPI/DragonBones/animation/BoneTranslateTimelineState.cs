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
    /// <internal/>
    /// <private/>
    internal class BoneTranslateTimelineState : BoneTimelineState
    {
        protected override void _OnArriveAtFrame()
        {
            base._OnArriveAtFrame();

            if (_timelineData != null)
            {
                var valueOffset = _animationData.frameFloatOffset + _frameValueOffset + _frameIndex * 2;
                var scale = _armature._armatureData.scale;
                var frameFloatArray = _dragonBonesData.frameFloatArray;
                var current = bonePose.current;
                var delta = bonePose.delta;

                current.x = frameFloatArray[valueOffset++] * scale;
                current.y = frameFloatArray[valueOffset++] * scale;

                if (_tweenState == TweenState.Always)
                {
                    if (_frameIndex == _frameCount - 1)
                    {
                        valueOffset = _animationData.frameFloatOffset + _frameValueOffset;
                    }

                    delta.x = frameFloatArray[valueOffset++] * scale - current.x;
                    delta.y = frameFloatArray[valueOffset++] * scale - current.y;
                }
                else
                {
                    delta.x = 0.0f;
                    delta.y = 0.0f;
                }
            }
            else
            {
                // Pose.
                var current = bonePose.current;
                var delta = bonePose.delta;
                current.x = 0.0f;
                current.y = 0.0f;
                delta.x = 0.0f;
                delta.y = 0.0f;
            }
        }
        protected override void _OnUpdateFrame()
        {
            base._OnUpdateFrame();

            var current = bonePose.current;
            var delta = bonePose.delta;
            var result = bonePose.result;

            bone._transformDirty = true;
            if (_tweenState != TweenState.Always)
            {
                _tweenState = TweenState.None;
            }

            result.x = current.x + delta.x * _tweenProgress;
            result.y = current.y + delta.y * _tweenProgress;
        }
    }
}
