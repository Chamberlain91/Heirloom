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
}
