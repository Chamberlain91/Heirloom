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
    /// <summary>
    /// - Armature is the core of the skeleton animation system.
    /// </summary>
    /// <see cref="DragonBones.ArmatureData"/>
    /// <see cref="DragonBones.Bone"/>
    /// <see cref="DragonBones.Slot"/>
    /// <see cref="DragonBones.Animation"/>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 骨架是骨骼动画系统的核心。
    /// </summary>
    /// <see cref="DragonBones.ArmatureData"/>
    /// <see cref="DragonBones.Bone"/>
    /// <see cref="DragonBones.Slot"/>
    /// <see cref="DragonBones.Animation"/>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    public class Armature : BaseObject, IAnimatable
    {
        private static int _OnSortSlots(Slot a, Slot b)
        {
            if (a._zOrder > b._zOrder)
            {
                return 1;
            }
            else if (a._zOrder < b._zOrder)
            {
                return -1;
            }

            return 0;//fixed slots sort error
        }

        /// <summary>
        /// - Whether to inherit the animation control of the parent armature.
        /// True to try to have the child armature play an animation with the same name when the parent armature play the animation.
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否继承父骨架的动画控制。
        /// 如果该值为 true，当父骨架播放动画时，会尝试让子骨架播放同名动画。
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public bool inheritAnimation;
        /// <private/>
        public object userData;

        private bool _lockUpdate;
        private bool _slotsDirty;
        private bool _zOrderDirty;
        private bool _flipX;
        private bool _flipY;

        /// <internal/>
        /// <private/>
        internal int _cacheFrameIndex;
        private readonly List<Bone> _bones = new List<Bone>();
        private readonly List<Slot> _slots = new List<Slot>();
        /// <internal/>
        /// <private/>
        internal readonly List<Constraint> _constraints = new List<Constraint>();
        private readonly List<EventObject> _actions = new List<EventObject>();
        /// <internal/>
        /// <private/>
        public ArmatureData _armatureData;
        private Animation _animation = null; // Initial value.
        private IArmatureProxy _proxy = null; // Initial value.
        private object _display;
        /// <internal/>
        /// <private/>
        internal TextureAtlasData _replaceTextureAtlasData = null; // Initial value.
        private object _replacedTexture;
        /// <internal/>
        /// <private/>
        internal DragonBones _dragonBones;
        private WorldClock _clock = null; // Initial value.

        /// <internal/>
        /// <private/>
        internal Slot _parent;
        /// <private/>
        protected override void _OnClear()
        {
            if (_clock != null)
            {
                // Remove clock first.
                _clock.Remove(this);
            }

            foreach (var bone in _bones)
            {
                bone.ReturnToPool();
            }

            foreach (var slot in _slots)
            {
                slot.ReturnToPool();
            }

            foreach (var constraint in _constraints)
            {
                constraint.ReturnToPool();
            }

            if (_animation != null)
            {
                _animation.ReturnToPool();
            }

            if (_proxy != null)
            {
                _proxy.DBClear();
            }

            if (_replaceTextureAtlasData != null)
            {
                _replaceTextureAtlasData.ReturnToPool();
            }

            inheritAnimation = true;
            userData = null;

            _lockUpdate = false;
            _slotsDirty = false;
            _zOrderDirty = false;
            _flipX = false;
            _flipY = false;
            _cacheFrameIndex = -1;
            _bones.Clear();
            _slots.Clear();
            _constraints.Clear();
            _actions.Clear();
            _armatureData = null; //
            _animation = null; //
            _proxy = null; //
            _display = null;
            _replaceTextureAtlasData = null;
            _replacedTexture = null;
            _dragonBones = null; //
            _clock = null;
            _parent = null;
        }

        /// <internal/>
        /// <private/>
        internal void _SortZOrder(short[] slotIndices, int offset)
        {
            var slotDatas = _armatureData.sortedSlots;
            var isOriginal = slotIndices == null;

            if (_zOrderDirty || !isOriginal)
            {
                for (int i = 0, l = slotDatas.Count; i < l; ++i)
                {
                    var slotIndex = isOriginal ? i : slotIndices[offset + i];
                    if (slotIndex < 0 || slotIndex >= l)
                    {
                        continue;
                    }

                    var slotData = slotDatas[slotIndex];
                    var slot = GetSlot(slotData.name);
                    if (slot != null)
                    {
                        slot._SetZorder(i);
                    }
                }

                _slotsDirty = true;
                _zOrderDirty = !isOriginal;
            }
        }
        /// <internal/>
        /// <private/>
        internal void _AddBone(Bone value)
        {
            if (!_bones.Contains(value))
            {
                _bones.Add(value);
            }
        }
        /// <internal/>
        /// <private/>
        internal void _AddSlot(Slot value)
        {
            if (!_slots.Contains(value))
            {
                _slotsDirty = true;
                _slots.Add(value);
            }
        }

        /// <internal/>
        /// <private/>
        internal void _AddConstraint(Constraint value)
        {
            if (!_constraints.Contains(value))
            {
                _constraints.Add(value);
            }
        }
        /// <internal/>
        /// <private/>
        internal void _BufferAction(EventObject action, bool append)
        {
            if (!_actions.Contains(action))
            {
                if (append)
                {
                    _actions.Add(action);
                }
                else
                {
                    _actions.Insert(0, action);                    
                }
            }
        }
        /// <summary>
        /// - Dispose the armature. (Return to the object pool)
        /// </summary>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     removeChild(armature.display);
        ///     armature.dispose();
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 释放骨架。 （回收到对象池）
        /// </summary>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     removeChild(armature.display);
        ///     armature.dispose();
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void Dispose()
        {
            if (_armatureData != null)
            {
                _lockUpdate = true;

                if (_dragonBones != null)
                {
                    _dragonBones.BufferObject(this);
                }
            }
        }
        /// <internal/>
        /// <private/>
        internal void Init(ArmatureData armatureData, IArmatureProxy proxy, object display, DragonBones dragonBones)
        {
            if (_armatureData != null)
            {
                return;
            }

            _armatureData = armatureData;
            _animation = BorrowObject<Animation>();
            _proxy = proxy;
            _display = display;
            _dragonBones = dragonBones;

            _proxy.DBInit(this);
            _animation.Init(this);
            _animation.animations = _armatureData.animations;
        }
        /// <inheritDoc/>
        public void AdvanceTime(float passedTime)
        {
            if (_lockUpdate)
            {
                return;
            }

            if (_armatureData == null)
            {
                Helper.Assert(false, "The armature has been disposed.");
                return;
            }
            else if (_armatureData.parent == null)
            {
                Helper.Assert(false, "The armature data has been disposed.\nPlease make sure dispose armature before call factory.clear().");
                return;
            }

            var prevCacheFrameIndex = _cacheFrameIndex;

            // Update animation.
            _animation.AdvanceTime(passedTime);

            if (_slotsDirty)
            {
                _slotsDirty = false;
                _slots.Sort(_OnSortSlots);
            }

            // Update bones and slots.
            if (_cacheFrameIndex < 0 || _cacheFrameIndex != prevCacheFrameIndex)
            {
                int i = 0, l = 0;
                for (i = 0, l = _bones.Count; i < l; ++i)
                {
                    _bones[i].Update(_cacheFrameIndex);
                }

                for (i = 0, l = _slots.Count; i < l; ++i)
                {
                    _slots[i].Update(_cacheFrameIndex);
                }
            }

            if (_actions.Count > 0)
            {
                _lockUpdate = true;
                foreach (var action in _actions)
                {
                    var actionData = action.actionData;
                    if (actionData != null)
                    {
                        if (actionData.type == ActionType.Play)
                        {
                            if (action.slot != null)
                            {
                                var childArmature = action.slot.childArmature;
                                if (childArmature != null)
                                {
                                    childArmature.animation.FadeIn(actionData.name);
                                }
                            }
                            else if (action.bone != null)
                            {
                                foreach (var slot in GetSlots())
                                {
                                    if (slot.parent == action.bone)
                                    {
                                        var childArmature = slot.childArmature;
                                        if (childArmature != null)
                                        {
                                            childArmature.animation.FadeIn(actionData.name);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _animation.FadeIn(actionData.name);
                            }
                        }
                    }

                    action.ReturnToPool();
                }

                _actions.Clear();
                _lockUpdate = false;
            }

            _proxy.DBUpdate();
        }
        /// <summary>
        /// - Forces a specific bone or its owning slot to update the transform or display property in the next frame.
        /// </summary>
        /// <param name="boneName">- The bone name. (If not set, all bones will be update)</param>
        /// <param name="updateSlot">- Whether to update the bone's slots. (Default: false)</param>
        /// <see cref="DragonBones.Bone.InvalidUpdate()"/>
        /// <see cref="DragonBones.Slot.InvalidUpdate()"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 强制特定骨骼或其拥有的插槽在下一帧更新变换或显示属性。
        /// </summary>
        /// <param name="boneName">- 骨骼名称。 （如果未设置，将更新所有骨骼）</param>
        /// <param name="updateSlot">- 是否更新骨骼的插槽。 （默认: false）</param>
        /// <see cref="DragonBones.Bone.InvalidUpdate()"/>
        /// <see cref="DragonBones.Slot.InvalidUpdate()"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void InvalidUpdate(string boneName = null, bool updateSlot = false)
        {
            if (!string.IsNullOrEmpty(boneName))
            {
                Bone bone = GetBone(boneName);
                if (bone != null)
                {
                    bone.InvalidUpdate();

                    if (updateSlot)
                    {
                        foreach (var slot in _slots)
                        {
                            if (slot.parent == bone)
                            {
                                slot.InvalidUpdate();
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var bone in _bones)
                {
                    bone.InvalidUpdate();
                }

                if (updateSlot)
                {
                    foreach (var slot in _slots)
                    {
                        slot.InvalidUpdate();
                    }
                }
            }
        }
        /// <summary>
        /// - Check whether a specific point is inside a custom bounding box in a slot.
        /// The coordinate system of the point is the inner coordinate system of the armature.
        /// Custom bounding boxes need to be customized in Dragonbones Pro.
        /// </summary>
        /// <param name="x">- The horizontal coordinate of the point.</param>
        /// <param name="y">- The vertical coordinate of the point.</param>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查特定点是否在某个插槽的自定义边界框内。
        /// 点的坐标系为骨架内坐标系。
        /// 自定义边界框需要在 DragonBones Pro 中自定义。
        /// </summary>
        /// <param name="x">- 点的水平坐标。</param>
        /// <param name="y">- 点的垂直坐标。</param>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public Slot ContainsPoint(float x, float y)
        {
            foreach (var slot in _slots)
            {
                if (slot.ContainsPoint(x, y))
                {
                    return slot;
                }
            }

            return null;
        }
        /// <summary>
        /// - Check whether a specific segment intersects a custom bounding box for a slot in the armature.
        /// The coordinate system of the segment and intersection is the inner coordinate system of the armature.
        /// Custom bounding boxes need to be customized in Dragonbones Pro.
        /// </summary>
        /// <param name="xA">- The horizontal coordinate of the beginning of the segment.</param>
        /// <param name="yA">- The vertical coordinate of the beginning of the segment.</param>
        /// <param name="xB">- The horizontal coordinate of the end point of the segment.</param>
        /// <param name="yB">- The vertical coordinate of the end point of the segment.</param>
        /// <param name="intersectionPointA">- The first intersection at which a line segment intersects the bounding box from the beginning to the end. (If not set, the intersection point will not calculated)</param>
        /// <param name="intersectionPointB">- The first intersection at which a line segment intersects the bounding box from the end to the beginning. (If not set, the intersection point will not calculated)</param>
        /// <param name="normalRadians">- The normal radians of the tangent of the intersection boundary box. [x: Normal radian of the first intersection tangent, y: Normal radian of the second intersection tangent] (If not set, the normal will not calculated)</param>
        /// <returns>The slot of the first custom bounding box where the segment intersects from the start point to the end point.</returns>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查特定线段是否与骨架的某个插槽的自定义边界框相交。
        /// 线段和交点的坐标系均为骨架内坐标系。
        /// 自定义边界框需要在 DragonBones Pro 中自定义。
        /// </summary>
        /// <param name="xA">- 线段起点的水平坐标。</param>
        /// <param name="yA">- 线段起点的垂直坐标。</param>
        /// <param name="xB">- 线段终点的水平坐标。</param>
        /// <param name="yB">- 线段终点的垂直坐标。</param>
        /// <param name="intersectionPointA">- 线段从起点到终点与边界框相交的第一个交点。 （如果未设置，则不计算交点）</param>
        /// <param name="intersectionPointB">- 线段从终点到起点与边界框相交的第一个交点。 （如果未设置，则不计算交点）</param>
        /// <param name="normalRadians">- 交点边界框切线的法线弧度。 [x: 第一个交点切线的法线弧度, y: 第二个交点切线的法线弧度] （如果未设置，则不计算法线）</param>
        /// <returns>线段从起点到终点相交的第一个自定义边界框的插槽。</returns>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public Slot IntersectsSegment(float xA, float yA, float xB, float yB,
                                       Point intersectionPointA = null,
                                       Point intersectionPointB = null,
                                       Point normalRadians = null)
        {
            var isV = xA == xB;
            var dMin = 0.0f;
            var dMax = 0.0f;
            var intXA = 0.0f;
            var intYA = 0.0f;
            var intXB = 0.0f;
            var intYB = 0.0f;
            var intAN = 0.0f;
            var intBN = 0.0f;
            Slot intSlotA = null;
            Slot intSlotB = null;

            foreach (var slot in _slots)
            {
                var intersectionCount = slot.IntersectsSegment(xA, yA, xB, yB, intersectionPointA, intersectionPointB, normalRadians);
                if (intersectionCount > 0)
                {
                    if (intersectionPointA != null || intersectionPointB != null)
                    {
                        if (intersectionPointA != null)
                        {
                            var d = isV ? intersectionPointA.y - yA : intersectionPointA.x - xA;
                            if (d < 0.0f)
                            {
                                d = -d;
                            }

                            if (intSlotA == null || d < dMin)
                            {
                                dMin = d;
                                intXA = intersectionPointA.x;
                                intYA = intersectionPointA.y;
                                intSlotA = slot;

                                if (normalRadians != null)
                                {
                                    intAN = normalRadians.x;
                                }
                            }
                        }

                        if (intersectionPointB != null)
                        {
                            var d = intersectionPointB.x - xA;
                            if (d < 0.0f)
                            {
                                d = -d;
                            }

                            if (intSlotB == null || d > dMax)
                            {
                                dMax = d;
                                intXB = intersectionPointB.x;
                                intYB = intersectionPointB.y;
                                intSlotB = slot;

                                if (normalRadians != null)
                                {
                                    intBN = normalRadians.y;
                                }
                            }
                        }
                    }
                    else
                    {
                        intSlotA = slot;
                        break;
                    }
                }
            }

            if (intSlotA != null && intersectionPointA != null)
            {
                intersectionPointA.x = intXA;
                intersectionPointA.y = intYA;

                if (normalRadians != null)
                {
                    normalRadians.x = intAN;
                }
            }

            if (intSlotB != null && intersectionPointB != null)
            {
                intersectionPointB.x = intXB;
                intersectionPointB.y = intYB;

                if (normalRadians != null)
                {
                    normalRadians.y = intBN;
                }
            }

            return intSlotA;
        }
        /// <summary>
        /// - Get a specific bone.
        /// </summary>
        /// <param name="name">- The bone name.</param>
        /// <see cref="DragonBones.Bone"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的骨骼。
        /// </summary>
        /// <param name="name">- 骨骼名称。</param>
        /// <see cref="DragonBones.Bone"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public Bone GetBone(string name)
        {
            foreach (var bone in _bones)
            {
                if (bone.name == name)
                {
                    return bone;
                }
            }

            return null;
        }
        /// <summary>
        /// - Get a specific bone by the display.
        /// </summary>
        /// <param name="display">- The display object.</param>
        /// <see cref="DragonBones.Bone"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 通过显示对象获取特定的骨骼。
        /// </summary>
        /// <param name="display">- 显示对象。</param>
        /// <see cref="DragonBones.Bone"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public Bone GetBoneByDisplay(object display)
        {
            var slot = GetSlotByDisplay(display);

            return slot != null ? slot.parent : null;
        }
        /// <summary>
        /// - Get a specific slot.
        /// </summary>
        /// <param name="name">- The slot name.</param>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的插槽。
        /// </summary>
        /// <param name="name">- 插槽名称。</param>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public Slot GetSlot(string name)
        {
            foreach (var slot in _slots)
            {
                if (slot.name == name)
                {
                    return slot;
                }
            }

            return null;
        }
        /// <summary>
        /// - Get a specific slot by the display.
        /// </summary>
        /// <param name="display">- The display object.</param>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 通过显示对象获取特定的插槽。
        /// </summary>
        /// <param name="display">- 显示对象。</param>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public Slot GetSlotByDisplay(object display)
        {
            if (display != null)
            {
                foreach (var slot in _slots)
                {
                    if (slot.display == display)
                    {
                        return slot;
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// - Get all bones.
        /// </summary>
        /// <see cref="DragonBones.Bone"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取所有的骨骼。
        /// </summary>
        /// <see cref="DragonBones.Bone"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public List<Bone> GetBones()
        {
            return _bones;
        }
        /// <summary>
        /// - Get all slots.
        /// </summary>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取所有的插槽。
        /// </summary>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public List<Slot> GetSlots()
        {
            return _slots;
        }

        /// <summary>
        /// - Whether to flip the armature horizontally.
        /// </summary>
        /// <version>DragonBones 5.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否将骨架水平翻转。
        /// </summary>
        /// <version>DragonBones 5.5</version>
        /// <language>zh_CN</language>
        public bool flipX
        {
            get { return _flipX; }
            set
            {
                if (_flipX == value)
                {
                    return;
                }

                _flipX = value;
                InvalidUpdate();
            }
        }
        /// <summary>
        /// - Whether to flip the armature vertically.
        /// </summary>
        /// <version>DragonBones 5.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否将骨架垂直翻转。
        /// </summary>
        /// <version>DragonBones 5.5</version>
        /// <language>zh_CN</language>
        public bool flipY
        {
            get { return _flipY; }
            set
            {
                if (_flipY == value)
                {
                    return;
                }

                _flipY = value;
                InvalidUpdate();
            }
        }
        /// <summary>
        /// - The animation cache frame rate, which turns on the animation cache when the set value is greater than 0.
        /// There is a certain amount of memory overhead to improve performance by caching animation data in memory.
        /// The frame rate should not be set too high, usually with the frame rate of the animation is similar and lower than the program running frame rate.
        /// When the animation cache is turned on, some features will fail, such as the offset property of bone.
        /// </summary>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     armature.cacheFrameRate = 24;
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.DragonBonesData.frameRate"/>
        /// <see cref="DragonBones.ArmatureData.frameRate"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 动画缓存帧率，当设置的值大于 0 的时，将会开启动画缓存。
        /// 通过将动画数据缓存在内存中来提高运行性能，会有一定的内存开销。
        /// 帧率不宜设置的过高，通常跟动画的帧率相当且低于程序运行的帧率。
        /// 开启动画缓存后，某些功能将会失效，比如骨骼的 offset 属性等。
        /// </summary>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     armature.cacheFrameRate = 24;
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.DragonBonesData.frameRate"/>
        /// <see cref="DragonBones.ArmatureData.frameRate"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public uint cacheFrameRate
        {
            get { return _armatureData.cacheFrameRate; }
            set
            {
                if (_armatureData.cacheFrameRate != value)
                {
                    _armatureData.CacheFrames(value);

                    // Set child armature frameRate.
                    foreach (var slot in _slots)
                    {
                        var childArmature = slot.childArmature;
                        if (childArmature != null)
                        {
                            childArmature.cacheFrameRate = value;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// - The armature name.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 骨架名称。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public string name
        {
            get { return _armatureData.name; }
        }
        /// <summary>
        /// - The armature data.
        /// </summary>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 骨架数据。
        /// </summary>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public ArmatureData armatureData
        {
            get { return _armatureData; }
        }
        /// <summary>
        /// - The animation player.
        /// </summary>
        /// <see cref="DragonBones.Animation"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 动画播放器。
        /// </summary>
        /// <see cref="DragonBones.Animation"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public Animation animation
        {
            get { return _animation; }
        }
        /// <pivate/>
        public IArmatureProxy proxy
        {
            get { return _proxy; }
        }

        /// <summary>
        /// - The EventDispatcher instance of the armature.
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 该骨架的 EventDispatcher 实例。
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public IEventDispatcher<EventObject> eventDispatcher
        {
            get { return _proxy; }
        }
        /// <summary>
        /// - The display container.
        /// The display of the slot is displayed as the parent.
        /// Depending on the rendering engine, the type will be different, usually the DisplayObjectContainer type.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 显示容器实例。
        /// 插槽的显示对象都会以此显示容器为父级。
        /// 根据渲染引擎的不同，类型会不同，通常是 DisplayObjectContainer 类型。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public object display
        {
            get { return _display; }
        }
        /// <private/>
        public object replacedTexture
        {
            get { return _replacedTexture; }
            set
            {
                if (_replacedTexture == value)
                {
                    return;
                }

                if (_replaceTextureAtlasData != null)
                {
                    _replaceTextureAtlasData.ReturnToPool();
                    _replaceTextureAtlasData = null;
                }

                _replacedTexture = value;

                foreach (var slot in _slots)
                {
                    slot.InvalidUpdate();
                    slot.Update(-1);
                }
            }
        }
        /// <inheritDoc/>
        public WorldClock clock
        {
            get { return _clock; }
            set
            {
                if (_clock == value)
                {
                    return;
                }

                if (_clock != null)
                {
                    _clock.Remove(this);
                }

                _clock = value;

                if (_clock != null)
                {
                    _clock.Add(this);
                }

                // Update childArmature clock.
                foreach (var slot in _slots)
                {
                    var childArmature = slot.childArmature;
                    if (childArmature != null)
                    {
                        childArmature.clock = _clock;
                    }
                }
            }
        }
        /// <summary>
        /// - Get the parent slot which the armature belongs to.
        /// </summary>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 该骨架所属的父插槽。
        /// </summary>
        /// <see cref="DragonBones.Slot"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public Slot parent
        {
            get { return _parent; }
        }
    }
}
