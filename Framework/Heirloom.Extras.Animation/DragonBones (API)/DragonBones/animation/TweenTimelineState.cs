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
    internal abstract class TweenTimelineState : TimelineState
    {
        private static float _GetEasingValue(TweenType tweenType, float progress, float easing)
        {
            var value = progress;

            switch (tweenType)
            {
                case TweenType.QuadIn:
                    value = (float)Math.Pow(progress, 2.0f);
                    break;

                case TweenType.QuadOut:
                    value = 1.0f - (float)Math.Pow(1.0f - progress, 2.0f);
                    break;

                case TweenType.QuadInOut:
                    value = 0.5f * (1.0f - (float)Math.Cos(progress * Math.PI));
                    break;
            }

            return (value - progress) * easing + progress;
        }

        private static float _GetEasingCurveValue(float progress, short[] samples, int count, int offset)
        {
            if (progress <= 0.0f)
            {
                return 0.0f;
            }
            else if (progress >= 1.0f)
            {
                return 1.0f;
            }

            var segmentCount = count + 1; // + 2 - 1
            var valueIndex = (int)Math.Floor(progress * segmentCount);
            var fromValue = valueIndex == 0 ? 0.0f : samples[offset + valueIndex - 1];

            var toValue = (valueIndex == segmentCount - 1) ? 10000.0f : samples[offset + valueIndex];

            return (fromValue + (toValue - fromValue) * (progress * segmentCount - valueIndex)) * 0.0001f;
        }

        protected TweenType _tweenType;
        protected int _curveCount;
        protected float _framePosition;
        protected float _frameDurationR;
        protected float _tweenProgress;
        protected float _tweenEasing;

        protected override void _OnClear()
        {
            base._OnClear();

            _tweenType = TweenType.None;
            _curveCount = 0;
            _framePosition = 0.0f;
            _frameDurationR = 0.0f;
            _tweenProgress = 0.0f;
            _tweenEasing = 0.0f;
        }

        protected override void _OnArriveAtFrame()
        {
            if (_frameCount > 1 &&
                (_frameIndex != _frameCount - 1 ||
                _animationState.playTimes == 0 ||
                _animationState.currentPlayTimes < _animationState.playTimes - 1))
            {
                _tweenType = (TweenType)_frameArray[_frameOffset + (int)BinaryOffset.FrameTweenType]; // TODO recode ture tween type.
                _tweenState = _tweenType == TweenType.None ? TweenState.Once : TweenState.Always;

                if (_tweenType == TweenType.Curve)
                {
                    _curveCount = _frameArray[_frameOffset + (int)BinaryOffset.FrameTweenEasingOrCurveSampleCount];
                }
                else if (_tweenType != TweenType.None && _tweenType != TweenType.Line)
                {
                    _tweenEasing = _frameArray[_frameOffset + (int)BinaryOffset.FrameTweenEasingOrCurveSampleCount] * 0.01f;
                }

                _framePosition = _frameArray[_frameOffset] * _frameRateR;
                if (_frameIndex == _frameCount - 1)
                {
                    _frameDurationR = 1.0f / (_animationData.duration - _framePosition);
                }
                else
                {
                    var nextFrameOffset = _animationData.frameOffset + _timelineArray[(_timelineData as TimelineData).offset + (int)BinaryOffset.TimelineFrameOffset + _frameIndex + 1];
                    var frameDuration = _frameArray[nextFrameOffset] * _frameRateR - _framePosition;

                    if (frameDuration > 0.0f)
                    {
                        _frameDurationR = 1.0f / frameDuration;
                    }
                    else
                    {
                        _frameDurationR = 0.0f;
                    }
                }
            }
            else
            {
                _tweenState = TweenState.Once;
            }
        }

        protected override void _OnUpdateFrame()
        {
            if (_tweenState == TweenState.Always)
            {
                _tweenProgress = (currentTime - _framePosition) * _frameDurationR;
                if (_tweenType == TweenType.Curve)
                {
                    _tweenProgress = TweenTimelineState._GetEasingCurveValue(_tweenProgress, _frameArray, _curveCount, (int)_frameOffset + (int)BinaryOffset.FrameCurveSamples);
                }
                else if (_tweenType != TweenType.Line)
                {
                    _tweenProgress = TweenTimelineState._GetEasingValue(_tweenType, _tweenProgress, _tweenEasing);
                }
            }
            else
            {
                _tweenProgress = 0.0f;
            }
        }
    }
}
