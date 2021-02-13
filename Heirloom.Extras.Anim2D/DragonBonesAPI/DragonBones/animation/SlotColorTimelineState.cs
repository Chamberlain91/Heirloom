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
    /// <internal/>
    /// <private/>
    internal class SlotColorTimelineState : SlotTimelineState
    {
        private bool _dirty;
        private readonly int[] _current = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        private readonly int[] _delta = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        private readonly float[] _result = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        protected override void _OnClear()
        {
            base._OnClear();

            _dirty = false;
        }

        protected override void _OnArriveAtFrame()
        {
            base._OnArriveAtFrame();

            if (_timelineData != null)
            {
                var intArray = _dragonBonesData.intArray;
                var frameIntArray = _dragonBonesData.frameIntArray;
                var valueOffset = _animationData.frameIntOffset + _frameValueOffset + _frameIndex * 1; // ...(timeline value offset)|x|x|(Value offset)|(Next offset)|x|x|...
                int colorOffset = frameIntArray[valueOffset];

                if (colorOffset < 0)
                {
                    colorOffset += 65536;// Fixed out of bouds bug. 
                }

                _current[0] = intArray[colorOffset++];
                _current[1] = intArray[colorOffset++];
                _current[2] = intArray[colorOffset++];
                _current[3] = intArray[colorOffset++];
                _current[4] = intArray[colorOffset++];
                _current[5] = intArray[colorOffset++];
                _current[6] = intArray[colorOffset++];
                _current[7] = intArray[colorOffset++];

                if (_tweenState == TweenState.Always)
                {
                    if (_frameIndex == _frameCount - 1)
                    {
                        colorOffset = frameIntArray[_animationData.frameIntOffset + _frameValueOffset];
                    }
                    else
                    {
                        colorOffset = frameIntArray[valueOffset + 1 * 1];
                    }

                    if (colorOffset < 0)
                    {
                        colorOffset += 65536;
                    }

                    _delta[0] = intArray[colorOffset++] - _current[0];
                    _delta[1] = intArray[colorOffset++] - _current[1];
                    _delta[2] = intArray[colorOffset++] - _current[2];
                    _delta[3] = intArray[colorOffset++] - _current[3];
                    _delta[4] = intArray[colorOffset++] - _current[4];
                    _delta[5] = intArray[colorOffset++] - _current[5];
                    _delta[6] = intArray[colorOffset++] - _current[6];
                    _delta[7] = intArray[colorOffset++] - _current[7];
                }
            }
            else
            {
                // Pose.
                var color = slot._slotData.color;
                _current[0] = (int)(color.AlphaMultiplier * 100.0f);
                _current[1] = (int)(color.RedMultiplier * 100.0f);
                _current[2] = (int)(color.GreenMultiplier * 100.0f);
                _current[3] = (int)(color.BlueMultiplier * 100.0f);
                _current[4] = color.AlphaOffset;
                _current[5] = color.RedOffset;
                _current[6] = color.GreenOffset;
                _current[7] = color.BlueOffset;
            }
        }

        protected override void _OnUpdateFrame()
        {
            base._OnUpdateFrame();

            _dirty = true;
            if (_tweenState != TweenState.Always)
            {
                _tweenState = TweenState.None;
            }

            _result[0] = (_current[0] + _delta[0] * _tweenProgress) * 0.01f;
            _result[1] = (_current[1] + _delta[1] * _tweenProgress) * 0.01f;
            _result[2] = (_current[2] + _delta[2] * _tweenProgress) * 0.01f;
            _result[3] = (_current[3] + _delta[3] * _tweenProgress) * 0.01f;
            _result[4] = _current[4] + _delta[4] * _tweenProgress;
            _result[5] = _current[5] + _delta[5] * _tweenProgress;
            _result[6] = _current[6] + _delta[6] * _tweenProgress;
            _result[7] = _current[7] + _delta[7] * _tweenProgress;
        }

        public override void FadeOut()
        {
            _tweenState = TweenState.None;
            _dirty = false;
        }

        public override void Update(float passedTime)
        {
            base.Update(passedTime);

            // Fade animation.
            if (_tweenState != TweenState.None || _dirty)
            {
                var result = slot._colorTransform;

                if (_animationState._fadeState != 0 || _animationState._subFadeState != 0)
                {
                    if (result.AlphaMultiplier != _result[0] ||
                        result.RedMultiplier != _result[1] ||
                        result.GreenMultiplier != _result[2] ||
                        result.BlueMultiplier != _result[3] ||
                        result.AlphaOffset != _result[4] ||
                        result.RedOffset != _result[5] ||
                        result.GreenOffset != _result[6] ||
                        result.BlueOffset != _result[7])
                    {
                        var fadeProgress = (float)Math.Pow(_animationState._fadeProgress, 4);

                        result.AlphaMultiplier += (_result[0] - result.AlphaMultiplier) * fadeProgress;
                        result.RedMultiplier += (_result[1] - result.RedMultiplier) * fadeProgress;
                        result.GreenMultiplier += (_result[2] - result.GreenMultiplier) * fadeProgress;
                        result.BlueMultiplier += (_result[3] - result.BlueMultiplier) * fadeProgress;
                        result.AlphaOffset += (int)((_result[4] - result.AlphaOffset) * fadeProgress);
                        result.RedOffset += (int)((_result[5] - result.RedOffset) * fadeProgress);
                        result.GreenOffset += (int)((_result[6] - result.GreenOffset) * fadeProgress);
                        result.BlueOffset += (int)((_result[7] - result.BlueOffset) * fadeProgress);

                        slot._colorDirty = true;
                    }
                }
                else if (_dirty)
                {
                    _dirty = false;
                    if (result.AlphaMultiplier != _result[0] ||
                        result.RedMultiplier != _result[1] ||
                        result.GreenMultiplier != _result[2] ||
                        result.BlueMultiplier != _result[3] ||
                        result.AlphaOffset != (int)_result[4] ||
                        result.RedOffset != (int)_result[5] ||
                        result.GreenOffset != (int)_result[6] ||
                        result.BlueOffset != (int)_result[7])
                    {
                        result.AlphaMultiplier = _result[0];
                        result.RedMultiplier = _result[1];
                        result.GreenMultiplier = _result[2];
                        result.BlueMultiplier = _result[3];
                        result.AlphaOffset = (int)_result[4];
                        result.RedOffset = (int)_result[5];
                        result.GreenOffset = (int)_result[6];
                        result.BlueOffset = (int)_result[7];

                        slot._colorDirty = true;
                    }
                }
            }
        }
    }
}
