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
    /// <internal/>
    /// <private/>
    internal enum TweenState
    {
        None,
        Once,
        Always
    }
    /// <internal/>
    /// <private/>
    internal abstract class TimelineState : BaseObject
    {
        public int playState; // -1: start, 0: play, 1: complete;
        public int currentPlayTimes;
        public float currentTime;

        protected TweenState _tweenState;
        protected uint _frameRate;
        protected int _frameValueOffset;
        protected uint _frameCount;
        protected uint _frameOffset;
        protected int _frameIndex;
        protected float _frameRateR;
        protected float _position;
        protected float _duration;
        protected float _timeScale;
        protected float _timeOffset;
        protected DragonBonesData _dragonBonesData;
        protected AnimationData _animationData;
        protected TimelineData _timelineData;
        protected Armature _armature;
        protected AnimationState _animationState;
        protected TimelineState _actionTimeline;

        protected short[] _frameArray;
        protected short[] _frameIntArray;
        protected float[] _frameFloatArray;
        protected ushort[] _timelineArray;
        protected List<uint> _frameIndices;

        protected override void _OnClear()
        {
            playState = -1;
            currentPlayTimes = -1;
            currentTime = -1.0f;

            _tweenState = TweenState.None;
            _frameRate = 0;
            _frameValueOffset = 0;
            _frameCount = 0;
            _frameOffset = 0;
            _frameIndex = -1;
            _frameRateR = 0.0f;
            _position = 0.0f;
            _duration = 0.0f;
            _timeScale = 1.0f;
            _timeOffset = 0.0f;
            _dragonBonesData = null; //
            _animationData = null; //
            _timelineData = null; //
            _armature = null; //
            _animationState = null; //
            _actionTimeline = null; //
            _frameArray = null; //
            _frameIntArray = null; //
            _frameFloatArray = null; //
            _timelineArray = null; //
            _frameIndices = null; //
        }

        protected abstract void _OnArriveAtFrame();
        protected abstract void _OnUpdateFrame();

        protected bool _SetCurrentTime(float passedTime)
        {
            var prevState = playState;
            var prevPlayTimes = currentPlayTimes;
            var prevTime = currentTime;

            if (_actionTimeline != null && _frameCount <= 1)
            {
                // No frame or only one frame.
                playState = _actionTimeline.playState >= 0 ? 1 : -1;
                currentPlayTimes = 1;
                currentTime = _actionTimeline.currentTime;
            }
            else if (_actionTimeline == null || _timeScale != 1.0f || _timeOffset != 0.0f)
            {
                var playTimes = _animationState.playTimes;
                var totalTime = playTimes * _duration;

                passedTime *= _timeScale;
                if (_timeOffset != 0.0f)
                {
                    passedTime += _timeOffset * _animationData.duration;
                }

                if (playTimes > 0 && (passedTime >= totalTime || passedTime <= -totalTime))
                {
                    if (playState <= 0 && _animationState._playheadState == 3)
                    {
                        playState = 1;
                    }

                    currentPlayTimes = playTimes;
                    if (passedTime < 0.0f)
                    {
                        currentTime = 0.0f;
                    }
                    else
                    {
                        currentTime = _duration + 0.000001f; // Precision problem
                    }
                }
                else
                {
                    if (playState != 0 && _animationState._playheadState == 3)
                    {
                        playState = 0;
                    }

                    if (passedTime < 0.0f)
                    {
                        passedTime = -passedTime;
                        currentPlayTimes = (int)(passedTime / _duration);
                        currentTime = _duration - (passedTime % _duration);
                    }
                    else
                    {
                        currentPlayTimes = (int)(passedTime / _duration);
                        currentTime = passedTime % _duration;
                    }
                }

                currentTime += _position;
            }
            else
            {
                // Multi frames.
                playState = _actionTimeline.playState;
                currentPlayTimes = _actionTimeline.currentPlayTimes;
                currentTime = _actionTimeline.currentTime;
            }

            if (currentPlayTimes == prevPlayTimes && currentTime == prevTime)
            {
                return false;
            }

            // Clear frame flag when timeline start or loopComplete.
            if ((prevState < 0 && playState != prevState) || (playState <= 0 && currentPlayTimes != prevPlayTimes))
            {
                _frameIndex = -1;
            }

            return true;
        }

        public virtual void Init(Armature armature, AnimationState animationState, TimelineData timelineData)
        {
            _armature = armature;
            _animationState = animationState;
            _timelineData = timelineData;
            _actionTimeline = _animationState._actionTimeline;

            if (this == _actionTimeline)
            {
                _actionTimeline = null; //
            }

            _frameRate = _armature.ArmatureData.frameRate;
            _frameRateR = 1.0f / _frameRate;
            _position = _animationState._position;
            _duration = _animationState._duration;
            _dragonBonesData = _armature.ArmatureData.parent;
            _animationData = _animationState._animationData;

            if (_timelineData != null)
            {
                _frameIntArray = _dragonBonesData.frameIntArray;
                _frameFloatArray = _dragonBonesData.frameFloatArray;
                _frameArray = _dragonBonesData.frameArray;
                _timelineArray = _dragonBonesData.timelineArray;
                _frameIndices = _dragonBonesData.frameIndices;

                _frameCount = _timelineArray[_timelineData.offset + (int)BinaryOffset.TimelineKeyFrameCount];
                _frameValueOffset = _timelineArray[_timelineData.offset + (int)BinaryOffset.TimelineFrameValueOffset];
                var timelineScale = _timelineArray[_timelineData.offset + (int)BinaryOffset.TimelineScale];
                _timeScale = 100.0f / (timelineScale == 0 ? 100.0f : timelineScale);
                _timeOffset = _timelineArray[_timelineData.offset + (int)BinaryOffset.TimelineOffset] * 0.01f;
            }
        }

        public virtual void FadeOut()
        {

        }

        public virtual void Update(float passedTime)
        {
            if (_SetCurrentTime(passedTime))
            {
                if (_frameCount > 1)
                {
                    int timelineFrameIndex = (int)Math.Floor(currentTime * _frameRate); // uint
                    var frameIndex = _frameIndices[(int)(_timelineData as TimelineData).frameIndicesOffset + timelineFrameIndex];
                    if (_frameIndex != frameIndex)
                    {
                        _frameIndex = (int)frameIndex;
                        _frameOffset = _animationData.frameOffset + _timelineArray[(_timelineData as TimelineData).offset + (int)BinaryOffset.TimelineFrameOffset + _frameIndex];

                        _OnArriveAtFrame();
                    }
                }
                else if (_frameIndex < 0)
                {
                    _frameIndex = 0;
                    if (_timelineData != null)
                    {
                        // May be pose timeline.
                        _frameOffset = _animationData.frameOffset + _timelineArray[_timelineData.offset + (int)BinaryOffset.TimelineFrameOffset];
                    }

                    _OnArriveAtFrame();
                }

                if (_tweenState != TweenState.None)
                {
                    _OnUpdateFrame();
                }
            }
        }
    }

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
    /// <internal/>
    /// <private/>
    internal abstract class BoneTimelineState : TweenTimelineState
    {
        public Bone bone;
        public BonePose bonePose;

        protected override void _OnClear()
        {
            base._OnClear();

            bone = null; //
            bonePose = null; //
        }

        public void Blend(int state)
        {
            var blendWeight = bone._blendState.blendWeight;
            var animationPose = bone.animationPose;
            var result = bonePose.result;

            if (state == 2)
            {
                animationPose.x += result.x * blendWeight;
                animationPose.y += result.y * blendWeight;
                animationPose.rotation += result.rotation * blendWeight;
                animationPose.skew += result.skew * blendWeight;
                animationPose.scaleX += (result.scaleX - 1.0f) * blendWeight;
                animationPose.scaleY += (result.scaleY - 1.0f) * blendWeight;
            }
            else if (blendWeight != 1.0f)
            {
                animationPose.x = result.x * blendWeight;
                animationPose.y = result.y * blendWeight;
                animationPose.rotation = result.rotation * blendWeight;
                animationPose.skew = result.skew * blendWeight;
                animationPose.scaleX = (result.scaleX - 1.0f) * blendWeight + 1.0f;
                animationPose.scaleY = (result.scaleY - 1.0f) * blendWeight + 1.0f;
            }
            else
            {
                animationPose.x = result.x;
                animationPose.y = result.y;
                animationPose.rotation = result.rotation;
                animationPose.skew = result.skew;
                animationPose.scaleX = result.scaleX;
                animationPose.scaleY = result.scaleY;
            }

            if (_animationState._fadeState != 0 || _animationState._subFadeState != 0)
            {
                bone._transformDirty = true;
            }
        }
    }
    /// <internal/>
    /// <private/>
    internal abstract class SlotTimelineState : TweenTimelineState
    {
        public Slot slot;

        protected override void _OnClear()
        {
            base._OnClear();

            slot = null; //
        }
    }

    /// <internal/>
    /// <private/>
    internal abstract class ConstraintTimelineState : TweenTimelineState
    {
        public Constraint constraint;

        protected override void _OnClear()
        {
            base._OnClear();

            constraint = null; //
        }
    }
}
