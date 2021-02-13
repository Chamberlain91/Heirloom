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
    internal class IKConstraint : Constraint
    {
        internal bool _scaleEnabled; // TODO
        /// <summary>
        /// - For timeline state.
        /// </summary>
        /// <internal/>
        internal bool _bendPositive;
        /// <summary>
        /// - For timeline state.
        /// </summary>
        /// <internal/>
        internal float _weight;

        protected override void _OnClear()
        {
            base._OnClear();

            _scaleEnabled = false;
            _bendPositive = false;
            _weight = 1.0f;
            _constraintData = null;
        }

        private void _ComputeA()
        {
            var ikGlobal = _target.Global;
            var global = _root.Global;
            var globalTransformMatrix = _root.GlobalTransformMatrix;

            var radian = (float) Math.Atan2(ikGlobal.y - global.y, ikGlobal.x - global.x);
            if (global.scaleX < 0.0f)
            {
                radian += (float) Math.PI;
            }

            global.rotation += Transform.NormalizeRadian(radian - global.rotation) * _weight;
            global.ToMatrix(globalTransformMatrix);
        }

        private void _ComputeB()
        {
            var boneLength = _bone.BoneData.length;
            var parent = _root as Bone;
            var ikGlobal = _target.Global;
            var parentGlobal = parent.Global;
            var global = _bone.Global;
            var globalTransformMatrix = _bone.GlobalTransformMatrix;

            var x = globalTransformMatrix.A * boneLength;
            var y = globalTransformMatrix.B * boneLength;

            var lLL = x * x + y * y;
            var lL = (float) Math.Sqrt(lLL);

            var dX = global.x - parentGlobal.x;
            var dY = global.y - parentGlobal.y;
            var lPP = dX * dX + dY * dY;
            var lP = (float) Math.Sqrt(lPP);
            var rawRadian = global.rotation;
            var rawParentRadian = parentGlobal.rotation;
            var rawRadianA = (float) Math.Atan2(dY, dX);

            dX = ikGlobal.x - parentGlobal.x;
            dY = ikGlobal.y - parentGlobal.y;
            var lTT = dX * dX + dY * dY;
            var lT = (float) Math.Sqrt(lTT);

            var radianA = 0.0f;
            if (lL + lP <= lT || lT + lL <= lP || lT + lP <= lL)
            {
                radianA = (float) Math.Atan2(ikGlobal.y - parentGlobal.y, ikGlobal.x - parentGlobal.x);
                if (lL + lP <= lT)
                {
                }
                else if (lP < lL)
                {
                    radianA += (float) Math.PI;
                }
            }
            else
            {
                var h = (lPP - lLL + lTT) / (2.0f * lTT);
                var r = (float) Math.Sqrt(lPP - h * h * lTT) / lT;
                var hX = parentGlobal.x + (dX * h);
                var hY = parentGlobal.y + (dY * h);
                var rX = -dY * r;
                var rY = dX * r;

                var isPPR = false;
                var parentParent = parent.Parent;
                if (parentParent != null)
                {
                    var parentParentMatrix = parentParent.GlobalTransformMatrix;
                    isPPR = parentParentMatrix.A * parentParentMatrix.D - parentParentMatrix.B * parentParentMatrix.C < 0.0f;
                }

                if (isPPR != _bendPositive)
                {
                    global.x = hX - rX;
                    global.y = hY - rY;
                }
                else
                {
                    global.x = hX + rX;
                    global.y = hY + rY;
                }

                radianA = (float) Math.Atan2(global.y - parentGlobal.y, global.x - parentGlobal.x);
            }

            var dR = Transform.NormalizeRadian(radianA - rawRadianA);
            parentGlobal.rotation = rawParentRadian + dR * _weight;
            parentGlobal.ToMatrix(parent.GlobalTransformMatrix);
            //
            var currentRadianA = rawRadianA + dR * _weight;
            global.x = parentGlobal.x + (float) Math.Cos(currentRadianA) * lP;
            global.y = parentGlobal.y + (float) Math.Sin(currentRadianA) * lP;
            //
            var radianB = (float) Math.Atan2(ikGlobal.y - global.y, ikGlobal.x - global.x);
            if (global.scaleX < 0.0f)
            {
                radianB += (float) Math.PI;
            }

            global.rotation = parentGlobal.rotation + rawRadian - rawParentRadian + Transform.NormalizeRadian(radianB - dR - rawRadian) * _weight;
            global.ToMatrix(globalTransformMatrix);
        }

        public override void Init(ConstraintData constraintData, Armature armature)
        {
            if (_constraintData != null)
            {
                return;
            }

            _constraintData = constraintData;
            _armature = armature;
            _target = _armature.GetBone(_constraintData.target.name);
            _root = _armature.GetBone(_constraintData.root.name);
            _bone = _constraintData.bone != null ? _armature.GetBone(_constraintData.bone.name) : null;

            {
                var ikConstraintData = _constraintData as IKConstraintData;
                //
                _scaleEnabled = ikConstraintData.scaleEnabled;
                _bendPositive = ikConstraintData.bendPositive;
                _weight = ikConstraintData.weight;
            }

            _root._hasConstraint = true;
        }

        public override void Update()
        {
            _root.UpdateByConstraint();

            if (_bone != null)
            {
                _bone.UpdateByConstraint();
                _ComputeB();
            }
            else
            {
                _ComputeA();
            }
        }

        public override void InvalidUpdate()
        {
            _root.InvalidUpdate();

            if (_bone != null)
            {
                _bone.InvalidUpdate();
            }
        }
    }
}
