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
}
