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
}
