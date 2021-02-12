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
    internal class ActionTimelineState : TimelineState
    {
        private void _OnCrossFrame(int frameIndex)
        {
            var eventDispatcher = _armature.Proxy;
            if (_animationState.actionEnabled)
            {
                var frameOffset = _animationData.frameOffset + _timelineArray[(_timelineData as TimelineData).offset + (int)BinaryOffset.TimelineFrameOffset + frameIndex];
                var actionCount = _frameArray[frameOffset + 1];
                var actions = _animationData.parent.actions; // May be the animaton data not belong to this armature data.

                for (var i = 0; i < actionCount; ++i)
                {
                    var actionIndex = _frameArray[frameOffset + 2 + i];
                    var action = actions[actionIndex];

                    if (action.type == ActionType.Play)
                    {
                        var eventObject = BaseObject.BorrowObject<EventObject>();
                        // eventObject.time = this._frameArray[frameOffset] * this._frameRateR; // Precision problem
                        eventObject.time = _frameArray[frameOffset] / _frameRate;
                        eventObject.animationState = _animationState;
                        EventObject.ActionDataToInstance(action, eventObject, _armature);
                        _armature._BufferAction(eventObject, true);
                    }
                    else
                    {
                        var eventType = action.type == ActionType.Frame ? EventObject.FRAME_EVENT : EventObject.SOUND_EVENT;
                        if (action.type == ActionType.Sound || eventDispatcher.HasDBEventListener(eventType))
                        {
                            var eventObject = BaseObject.BorrowObject<EventObject>();
                            // eventObject.time = this._frameArray[frameOffset] * this._frameRateR; // Precision problem
                            eventObject.time = (float)_frameArray[frameOffset] / (float)_frameRate;
                            eventObject.animationState = _animationState;
                            EventObject.ActionDataToInstance(action, eventObject, _armature);
                            _armature._dragonBones.BufferEvent(eventObject);
                        }
                    }
                }
            }
        }

        protected override void _OnArriveAtFrame() { }
        protected override void _OnUpdateFrame() { }

        public override void Update(float passedTime)
        {
            var prevState = playState;
            var prevPlayTimes = currentPlayTimes;
            var prevTime = currentTime;

            if (_SetCurrentTime(passedTime))
            {
                var eventDispatcher = _armature.Proxy;
                if (prevState < 0)
                {
                    if (playState != prevState)
                    {
                        if (_animationState.displayControl && _animationState.resetToPose)
                        {
                            // Reset zorder to pose.
                            _armature._SortZOrder(null, 0);
                        }

                        prevPlayTimes = currentPlayTimes;

                        if (eventDispatcher.HasDBEventListener(EventObject.START))
                        {
                            var eventObject = BaseObject.BorrowObject<EventObject>();
                            eventObject.type = EventObject.START;
                            eventObject.armature = _armature;
                            eventObject.animationState = _animationState;
                            _armature._dragonBones.BufferEvent(eventObject);
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                var isReverse = _animationState.timeScale < 0.0f;
                EventObject loopCompleteEvent = null;
                EventObject completeEvent = null;

                if (currentPlayTimes != prevPlayTimes)
                {
                    if (eventDispatcher.HasDBEventListener(EventObject.LOOP_COMPLETE))
                    {
                        loopCompleteEvent = BaseObject.BorrowObject<EventObject>();
                        loopCompleteEvent.type = EventObject.LOOP_COMPLETE;
                        loopCompleteEvent.armature = _armature;
                        loopCompleteEvent.animationState = _animationState;
                    }

                    if (playState > 0)
                    {
                        if (eventDispatcher.HasDBEventListener(EventObject.COMPLETE))
                        {
                            completeEvent = BaseObject.BorrowObject<EventObject>();
                            completeEvent.type = EventObject.COMPLETE;
                            completeEvent.armature = _armature;
                            completeEvent.animationState = _animationState;
                        }
                    }
                }

                if (_frameCount > 1)
                {
                    var timelineData = _timelineData as TimelineData;
                    var timelineFrameIndex = (int)(currentTime * _frameRate); // uint
                    var frameIndex = (int)_frameIndices[timelineData.frameIndicesOffset + timelineFrameIndex];
                    if (_frameIndex != frameIndex)
                    {
                        // Arrive at frame.                   
                        var crossedFrameIndex = _frameIndex;
                        _frameIndex = frameIndex;
                        if (_timelineArray != null)
                        {
                            _frameOffset = _animationData.frameOffset + _timelineArray[timelineData.offset + (int)BinaryOffset.TimelineFrameOffset + _frameIndex];
                            if (isReverse)
                            {
                                if (crossedFrameIndex < 0)
                                {
                                    var prevFrameIndex = (int)(prevTime * _frameRate);
                                    crossedFrameIndex = (int)_frameIndices[timelineData.frameIndicesOffset + prevFrameIndex];
                                    if (currentPlayTimes == prevPlayTimes)
                                    {
                                        // Start.
                                        if (crossedFrameIndex == frameIndex)
                                        { // Uncrossed.
                                            crossedFrameIndex = -1;
                                        }
                                    }
                                }

                                while (crossedFrameIndex >= 0)
                                {
                                    var frameOffset = _animationData.frameOffset + _timelineArray[timelineData.offset + (int)BinaryOffset.TimelineFrameOffset + crossedFrameIndex];
                                    // const framePosition = this._frameArray[frameOffset] * this._frameRateR; // Precision problem
                                    var framePosition = (float)_frameArray[frameOffset] / (float)_frameRate;

                                    if (_position <= framePosition && framePosition <= _position + _duration)
                                    {
                                        // Support interval play.
                                        _OnCrossFrame(crossedFrameIndex);
                                    }

                                    if (loopCompleteEvent != null && crossedFrameIndex == 0)
                                    {
                                        // Add loop complete event after first frame.
                                        _armature._dragonBones.BufferEvent(loopCompleteEvent);
                                        loopCompleteEvent = null;
                                    }

                                    if (crossedFrameIndex > 0)
                                    {
                                        crossedFrameIndex--;
                                    }
                                    else
                                    {
                                        crossedFrameIndex = (int)_frameCount - 1;
                                    }

                                    if (crossedFrameIndex == frameIndex)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (crossedFrameIndex < 0)
                                {
                                    var prevFrameIndex = (int)(prevTime * _frameRate);
                                    crossedFrameIndex = (int)_frameIndices[timelineData.frameIndicesOffset + prevFrameIndex];
                                    var frameOffset = _animationData.frameOffset + _timelineArray[timelineData.offset + (int)BinaryOffset.TimelineFrameOffset + crossedFrameIndex];
                                    // const framePosition = this._frameArray[frameOffset] * this._frameRateR; // Precision problem
                                    var framePosition = (float)_frameArray[frameOffset] / (float)_frameRate;
                                    if (currentPlayTimes == prevPlayTimes)
                                    {
                                        // Start.
                                        if (prevTime <= framePosition)
                                        {
                                            // Crossed.
                                            if (crossedFrameIndex > 0)
                                            {
                                                crossedFrameIndex--;
                                            }
                                            else
                                            {
                                                crossedFrameIndex = (int)_frameCount - 1;
                                            }
                                        }
                                        else if (crossedFrameIndex == frameIndex)
                                        {
                                            // Uncrossed.
                                            crossedFrameIndex = -1;
                                        }
                                    }
                                }

                                while (crossedFrameIndex >= 0)
                                {
                                    if (crossedFrameIndex < _frameCount - 1)
                                    {
                                        crossedFrameIndex++;
                                    }
                                    else
                                    {
                                        crossedFrameIndex = 0;
                                    }

                                    var frameOffset = _animationData.frameOffset + _timelineArray[timelineData.offset + (int)BinaryOffset.TimelineFrameOffset + crossedFrameIndex];
                                    // const framePosition = this._frameArray[frameOffset] * this._frameRateR; // Precision problem
                                    var framePosition = (float)_frameArray[frameOffset] / (float)_frameRate;
                                    if (_position <= framePosition && framePosition <= _position + _duration)
                                    {
                                        // Support interval play.
                                        _OnCrossFrame(crossedFrameIndex);
                                    }

                                    if (loopCompleteEvent != null && crossedFrameIndex == 0)
                                    {
                                        // Add loop complete event before first frame.
                                        _armature._dragonBones.BufferEvent(loopCompleteEvent);
                                        loopCompleteEvent = null;
                                    }

                                    if (crossedFrameIndex == frameIndex)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (_frameIndex < 0)
                {
                    _frameIndex = 0;
                    if (_timelineData != null)
                    {
                        _frameOffset = _animationData.frameOffset + _timelineArray[_timelineData.offset + (int)BinaryOffset.TimelineFrameOffset];
                        // Arrive at frame.
                        var framePosition = (float)_frameArray[_frameOffset] / (float)_frameRate;
                        if (currentPlayTimes == prevPlayTimes)
                        {
                            // Start.
                            if (prevTime <= framePosition)
                            {
                                _OnCrossFrame(_frameIndex);
                            }
                        }
                        else if (_position <= framePosition)
                        {
                            // Loop complete.
                            if (!isReverse && loopCompleteEvent != null)
                            {
                                // Add loop complete event before first frame.
                                _armature._dragonBones.BufferEvent(loopCompleteEvent);
                                loopCompleteEvent = null;
                            }

                            _OnCrossFrame(_frameIndex);
                        }
                    }
                }

                if (loopCompleteEvent != null)
                {
                    _armature._dragonBones.BufferEvent(loopCompleteEvent);
                }

                if (completeEvent != null)
                {
                    _armature._dragonBones.BufferEvent(completeEvent);
                }
            }
        }

        public void SetCurrentTime(float value)
        {
            _SetCurrentTime(value);
            _frameIndex = -1;
        }
    }
    /// <internal/>
    /// <private/>
    internal class ZOrderTimelineState : TimelineState
    {
        protected override void _OnArriveAtFrame()
        {
            if (playState >= 0)
            {
                var count = _frameArray[_frameOffset + 1];
                if (count > 0)
                {
                    _armature._SortZOrder(_frameArray, (int)_frameOffset + 2);
                }
                else
                {
                    _armature._SortZOrder(null, 0);
                }
            }
        }

        protected override void _OnUpdateFrame() { }
    }
    /// <internal/>
    /// <private/>
    internal class BoneAllTimelineState : BoneTimelineState
    {
        protected override void _OnArriveAtFrame()
        {
            base._OnArriveAtFrame();

            if (_timelineData != null)
            {
                var valueOffset = (int)_animationData.frameFloatOffset + _frameValueOffset + _frameIndex * 6; // ...(timeline value offset)|xxxxxx|xxxxxx|(Value offset)xxxxx|(Next offset)xxxxx|xxxxxx|xxxxxx|...
                var scale = _armature._armatureData.scale;
                var frameFloatArray = _dragonBonesData.frameFloatArray;
                var current = bonePose.current;
                var delta = bonePose.delta;

                current.x = frameFloatArray[valueOffset++] * scale;
                current.y = frameFloatArray[valueOffset++] * scale;
                current.rotation = frameFloatArray[valueOffset++];
                current.skew = frameFloatArray[valueOffset++];
                current.scaleX = frameFloatArray[valueOffset++];
                current.scaleY = frameFloatArray[valueOffset++];

                if (_tweenState == TweenState.Always)
                {
                    if (_frameIndex == _frameCount - 1)
                    {
                        valueOffset = (int)_animationData.frameFloatOffset + _frameValueOffset;
                    }

                    delta.x = frameFloatArray[valueOffset++] * scale - current.x;
                    delta.y = frameFloatArray[valueOffset++] * scale - current.y;
                    delta.rotation = frameFloatArray[valueOffset++] - current.rotation;
                    delta.skew = frameFloatArray[valueOffset++] - current.skew;
                    delta.scaleX = frameFloatArray[valueOffset++] - current.scaleX;
                    delta.scaleY = frameFloatArray[valueOffset++] - current.scaleY;
                }
                else
                {
                    delta.x = 0.0f;
                    delta.y = 0.0f;
                    delta.rotation = 0.0f;
                    delta.skew = 0.0f;
                    delta.scaleX = 0.0f;
                    delta.scaleY = 0.0f;
                }
            }
            else
            {
                // Pose.
                var current = bonePose.current;
                var delta = bonePose.delta;
                current.x = 0.0f;
                current.y = 0.0f;
                current.rotation = 0.0f;
                current.skew = 0.0f;
                current.scaleX = 1.0f;
                current.scaleY = 1.0f;
                delta.x = 0.0f;
                delta.y = 0.0f;
                delta.rotation = 0.0f;
                delta.skew = 0.0f;
                delta.scaleX = 0.0f;
                delta.scaleY = 0.0f;
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
            result.rotation = current.rotation + delta.rotation * _tweenProgress;
            result.skew = current.skew + delta.skew * _tweenProgress;
            result.scaleX = current.scaleX + delta.scaleX * _tweenProgress;
            result.scaleY = current.scaleY + delta.scaleY * _tweenProgress;
        }

        public override void FadeOut()
        {
            var result = bonePose.result;
            result.rotation = Transform.NormalizeRadian(result.rotation);
            result.skew = Transform.NormalizeRadian(result.skew);
        }
    }
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
    /// <internal/>
    /// <private/>
    internal class BoneRotateTimelineState : BoneTimelineState
    {
        protected override void _OnArriveAtFrame()
        {
            base._OnArriveAtFrame();

            if (_timelineData != null)
            {
                var valueOffset = _animationData.frameFloatOffset + _frameValueOffset + _frameIndex * 2;
                var frameFloatArray = _dragonBonesData.frameFloatArray;
                var current = bonePose.current;
                var delta = bonePose.delta;

                current.rotation = frameFloatArray[valueOffset++];
                current.skew = frameFloatArray[valueOffset++];

                if (_tweenState == TweenState.Always)
                {
                    if (_frameIndex == _frameCount - 1)
                    {
                        valueOffset = _animationData.frameFloatOffset + _frameValueOffset;
                        delta.rotation = Transform.NormalizeRadian(frameFloatArray[valueOffset++] - current.rotation);
                    }
                    else
                    {
                        delta.rotation = frameFloatArray[valueOffset++] - current.rotation;
                    }

                    delta.skew = frameFloatArray[valueOffset++] - current.skew;
                }
                else
                {
                    delta.rotation = 0.0f;
                    delta.skew = 0.0f;
                }
            }
            else
            {
                // Pose.
                var current = bonePose.current;
                var delta = bonePose.delta;
                current.rotation = 0.0f;
                current.skew = 0.0f;
                delta.rotation = 0.0f;
                delta.skew = 0.0f;
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

            result.rotation = current.rotation + delta.rotation * _tweenProgress;
            result.skew = current.skew + delta.skew * _tweenProgress;
        }

        public override void FadeOut()
        {
            var result = bonePose.result;
            result.rotation = Transform.NormalizeRadian(result.rotation);
            result.skew = Transform.NormalizeRadian(result.skew);
        }
    }
    /// <internal/>
    /// <private/>
    internal class BoneScaleTimelineState : BoneTimelineState
    {
        protected override void _OnArriveAtFrame()
        {
            base._OnArriveAtFrame();

            if (_timelineData != null)
            {
                var valueOffset = _animationData.frameFloatOffset + _frameValueOffset + _frameIndex * 2;
                var frameFloatArray = _dragonBonesData.frameFloatArray;
                var current = bonePose.current;
                var delta = bonePose.delta;

                current.scaleX = frameFloatArray[valueOffset++];
                current.scaleY = frameFloatArray[valueOffset++];

                if (_tweenState == TweenState.Always)
                {
                    if (_frameIndex == _frameCount - 1)
                    {
                        valueOffset = _animationData.frameFloatOffset + _frameValueOffset;
                    }

                    delta.scaleX = frameFloatArray[valueOffset++] - current.scaleX;
                    delta.scaleY = frameFloatArray[valueOffset++] - current.scaleY;
                }
                else
                {
                    delta.scaleX = 0.0f;
                    delta.scaleY = 0.0f;
                }
            }
            else
            {
                // Pose.
                var current = bonePose.current;
                var delta = bonePose.delta;
                current.scaleX = 1.0f;
                current.scaleY = 1.0f;
                delta.scaleX = 0.0f;
                delta.scaleY = 0.0f;
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

            result.scaleX = current.scaleX + delta.scaleX * _tweenProgress;
            result.scaleY = current.scaleY + delta.scaleY * _tweenProgress;
        }
    }
    /// <internal/>
    /// <private/>
    internal class SlotDislayTimelineState : SlotTimelineState
    {
        protected override void _OnArriveAtFrame()
        {
            if (playState >= 0)
            {
                var displayIndex = _timelineData != null ? _frameArray[_frameOffset + 1] : slot.SlotData.displayIndex;
                if (slot.DisplayIndex != displayIndex)
                {
                    slot._SetDisplayIndex(displayIndex, true);
                }
            }
        }
    }
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
    /// <internal/>
    /// <private/>
    internal class DeformTimelineState : SlotTimelineState
    {
        public int vertexOffset;

        private bool _dirty;
        private int _frameFloatOffset;
        private int _valueCount;
        private int _deformCount;
        private int _valueOffset;
        private readonly List<float> _current = new List<float>();
        private readonly List<float> _delta = new List<float>();
        private readonly List<float> _result = new List<float>();

        //QQ
        public bool test = false;

        protected override void _OnClear()
        {
            base._OnClear();

            vertexOffset = 0;

            _dirty = false;
            _frameFloatOffset = 0;
            _valueCount = 0;
            _deformCount = 0;
            _valueOffset = 0;
            _current.Clear();
            _delta.Clear();
            _result.Clear();
        }

        protected override void _OnArriveAtFrame()
        {
            base._OnArriveAtFrame();
            if (_timelineData != null)
            {
                var valueOffset = _animationData.frameFloatOffset + _frameValueOffset + _frameIndex * _valueCount;
                var scale = _armature._armatureData.scale;
                var frameFloatArray = _dragonBonesData.frameFloatArray;

                if (_tweenState == TweenState.Always)
                {
                    var nextValueOffset = valueOffset + _valueCount;
                    if (_frameIndex == _frameCount - 1)
                    {
                        nextValueOffset = _animationData.frameFloatOffset + _frameValueOffset;
                    }

                    for (var i = 0; i < _valueCount; ++i)
                    {
                        _delta[i] = frameFloatArray[nextValueOffset + i] * scale - (_current[i] = frameFloatArray[valueOffset + i] * scale);
                    }
                }
                else
                {
                    for (var i = 0; i < _valueCount; ++i)
                    {
                        _current[i] = frameFloatArray[valueOffset + i] * scale;
                    }
                }
            }
            else
            {
                for (var i = 0; i < _valueCount; ++i)
                {
                    _current[i] = 0.0f;
                }
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

            for (var i = 0; i < _valueCount; ++i)
            {
                _result[i] = _current[i] + _delta[i] * _tweenProgress;
            }
        }

        public override void Init(Armature armature, AnimationState animationState, TimelineData timelineData)
        {
            base.Init(armature, animationState, timelineData);

            if (_timelineData != null)
            {
                var frameIntOffset = _animationData.frameIntOffset + _timelineArray[_timelineData.offset + (int)BinaryOffset.TimelineFrameValueCount];
                vertexOffset = _frameIntArray[frameIntOffset + (int)BinaryOffset.DeformVertexOffset];
                if (vertexOffset < 0)
                {
                    vertexOffset += 65536; // Fixed out of bouds bug. 
                }

                _deformCount = _frameIntArray[frameIntOffset + (int)BinaryOffset.DeformCount];
                _valueCount = _frameIntArray[frameIntOffset + (int)BinaryOffset.DeformValueCount];
                _valueOffset = _frameIntArray[frameIntOffset + (int)BinaryOffset.DeformValueOffset];
                _frameFloatOffset = _frameIntArray[frameIntOffset + (int)BinaryOffset.DeformFloatOffset] + (int)_animationData.frameFloatOffset;
            }
            else
            {
                _deformCount = slot._deformVertices != null ? slot._deformVertices.vertices.Count : 0;
                _valueCount = _deformCount;
                _valueOffset = 0;
                _frameFloatOffset = 0;
            }

            _current.ResizeList(_valueCount);
            _delta.ResizeList(_valueCount);
            _result.ResizeList(_valueCount);

            for (var i = 0; i < _valueCount; ++i)
            {
                _delta[i] = 0.0f;
            }
        }

        public override void FadeOut()
        {
            _tweenState = TweenState.None;
            _dirty = false;
        }

        public override void Update(float passedTime)
        {
            var deformVertices = slot._deformVertices;
            if (deformVertices == null || deformVertices.verticesData == null || deformVertices.verticesData.offset != vertexOffset)
            {
                return;
            }
            else if(_timelineData != null && _dragonBonesData != deformVertices.verticesData.data)
            {
                return;
            }

            base.Update(passedTime);

            // Fade animation.
            if (_tweenState != TweenState.None || _dirty)
            {
                var result = deformVertices.vertices;

                if (_animationState._fadeState != 0 || _animationState._subFadeState != 0)
                {
                    var fadeProgress = (float)Math.Pow(_animationState._fadeProgress, 2);

                    if (_timelineData != null)
                    {
                        for (var i = 0; i < _deformCount; ++i)
                        {
                            if (i < _valueOffset)
                            {
                                result[i] += (_frameFloatArray[_frameFloatOffset + i] - result[i]) * fadeProgress;
                            }
                            else if (i < _valueOffset + _valueCount)
                            {
                                result[i] += (_result[i - _valueOffset] - result[i]) * fadeProgress;
                            }
                            else
                            {
                                result[i] += (_frameFloatArray[_frameFloatOffset + i - _valueCount] - result[i]) * fadeProgress;
                            }
                        }
                    }
                    else
                    {
                        _deformCount = result.Count;

                        for (var i = 0; i < _deformCount; i++)
                        {
                            result[i] += (0.0f - result[i]) * fadeProgress;
                        }
                    }

                    deformVertices.verticesDirty = true;
                }
                else if (_dirty)
                {
                    _dirty = false;

                    if (_timelineData != null)
                    {
                        for (var i = 0; i < _deformCount; ++i)
                        {
                            if (i < _valueOffset)
                            {
                                result[i] = _frameFloatArray[_frameFloatOffset + i];
                            }
                            else if (i < _valueOffset + _valueCount)
                            {
                                result[i] = _result[i - _valueOffset];
                            }
                            else
                            {
                                result[i] = _frameFloatArray[_frameFloatOffset + i - _valueCount];
                            }
                        }
                    }
                    else
                    {
                        _deformCount = result.Count;

                        for (var i = 0; i < _deformCount; i++)
                        {
                            result[i] = 0.0f;
                        }
                    }

                    deformVertices.verticesDirty = true;
                }
            }
        }
    }

    /// <internal/>
    /// <private/>
    internal class IKConstraintTimelineState : ConstraintTimelineState
    {
        private float _current;
        private float _delta;

        protected override void _OnClear()
        {
            base._OnClear();

            _current = 0.0f;
            _delta = 0.0f;
        }

        protected override void _OnArriveAtFrame()
        {
            base._OnArriveAtFrame();

            var ikConstraint = constraint as IKConstraint;

            if (_timelineData != null)
            {
                var valueOffset = _animationData.frameIntOffset + _frameValueOffset + _frameIndex * 2;
                var frameIntArray = _frameIntArray;
                var bendPositive = frameIntArray[valueOffset++] != 0;
                _current = frameIntArray[valueOffset++] * 0.01f;

                if (_tweenState == TweenState.Always)
                {
                    if (_frameIndex == _frameCount - 1)
                    {
                        valueOffset = _animationData.frameIntOffset + _frameValueOffset; // + 0 * 2
                    }

                    _delta = frameIntArray[valueOffset + 1] * 0.01f - _current;
                }
                else
                {
                    _delta = 0.0f;
                }

                ikConstraint._bendPositive = bendPositive;
            }
            else
            {
                var ikConstraintData = ikConstraint._constraintData as IKConstraintData;
                _current = ikConstraintData.weight;
                _delta = 0.0f;
                ikConstraint._bendPositive = ikConstraintData.bendPositive;
            }

            ikConstraint.InvalidUpdate();
        }

        protected override void _OnUpdateFrame()
        {
            base._OnUpdateFrame();

            if (_tweenState != TweenState.Always)
            {
                _tweenState = TweenState.None;
            }

            var ikConstraint = constraint as IKConstraint;
            ikConstraint._weight = _current + _delta * _tweenProgress;
            ikConstraint.InvalidUpdate();
        }
    }
}
