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
    internal abstract class Constraint : BaseObject
    {
        protected static readonly Matrix _helpMatrix = new Matrix();
        protected static readonly Transform _helpTransform = new Transform();
        protected static readonly Point _helpPoint = new Point();

        /// <summary>
        /// - For timeline state.
        /// </summary>
        /// <internal/>
        internal ConstraintData _constraintData;
        protected Armature _armature;

        /// <summary>
        /// - For sort bones.
        /// </summary>
        /// <internal/>
        internal Bone _target;
        /// <summary>
        /// - For sort bones.
        /// </summary>
        /// <internal/>
        internal Bone _root;
        internal Bone _bone;

        protected override void _OnClear()
        {
            _armature = null;
            _target = null; //
            _root = null; //
            _bone = null; //
        }

        public abstract void Init(ConstraintData constraintData, Armature armature);
        public abstract void Update();
        public abstract void InvalidUpdate();

        public string name
        {
            get { return _constraintData.name; }
        }
    }
}
