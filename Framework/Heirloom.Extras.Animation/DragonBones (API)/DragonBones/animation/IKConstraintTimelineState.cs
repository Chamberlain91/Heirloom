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
