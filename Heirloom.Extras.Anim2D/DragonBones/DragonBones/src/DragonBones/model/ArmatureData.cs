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
    /// - The armature data.
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 骨架数据。
    /// </summary>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    internal class ArmatureData : BaseObject
    {
        /// <private/>
        public ArmatureType type;
        /// <summary>
        /// - The animation frame rate.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 动画帧率。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public uint frameRate;
        /// <private/>
        public uint cacheFrameRate;
        /// <private/>
        public float scale;
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
        public string name;
        /// <private/>
        public readonly Rectangle aabb = new Rectangle();
        /// <summary>
        /// - The names of all the animation data.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 所有的动画数据名称。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public readonly List<string> animationNames = new List<string>();
        /// <private/>
        public readonly List<BoneData> sortedBones = new List<BoneData>();
        /// <private/>
        public readonly List<SlotData> sortedSlots = new List<SlotData>();
        /// <private/>
        public readonly List<ActionData> defaultActions = new List<ActionData>();
        /// <private/>
        public readonly List<ActionData> actions = new List<ActionData>();
        /// <private/>
        public readonly Dictionary<string, BoneData> bones = new Dictionary<string, BoneData>();
        /// <private/>
        public readonly Dictionary<string, SlotData> slots = new Dictionary<string, SlotData>();

        /// <private/>
        public readonly Dictionary<string, ConstraintData> constraints = new Dictionary<string, ConstraintData>();
        /// <private/>
        public readonly Dictionary<string, SkinData> skins = new Dictionary<string, SkinData>();
        /// <private/>
        public readonly Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();

        /// <summary>
        /// - The default skin data.
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 默认插槽数据。
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public SkinData defaultSkin = null;
        /// <summary>
        /// - The default animation data.
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 默认动画数据。
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationData defaultAnimation = null;
        /// <private/>
        public CanvasData canvas = null; // Initial value.
        /// <private/>
        public UserData userData = null; // Initial value.
        /// <private/>
        public DragonBonesData parent;
        /// <inheritDoc/>
        protected override void _OnClear()
        {
            foreach (var action in defaultActions)
            {
                action.ReturnToPool();
            }

            foreach (var action in actions)
            {
                action.ReturnToPool();
            }

            foreach (var k in bones.Keys)
            {
                bones[k].ReturnToPool();
            }

            foreach (var k in slots.Keys)
            {
                slots[k].ReturnToPool();
            }

            foreach (var k in constraints.Keys)
            {
                constraints[k].ReturnToPool();
            }

            foreach (var k in skins.Keys)
            {
                skins[k].ReturnToPool();
            }

            foreach (var k in animations.Keys)
            {
                animations[k].ReturnToPool();
            }

            if (canvas != null)
            {
                canvas.ReturnToPool();
            }

            if (userData != null)
            {
                userData.ReturnToPool();
            }

            type = ArmatureType.Armature;
            frameRate = 0;
            cacheFrameRate = 0;
            scale = 1.0f;
            name = "";
            aabb.Clear();
            animationNames.Clear();
            sortedBones.Clear();
            sortedSlots.Clear();
            defaultActions.Clear();
            actions.Clear();
            bones.Clear();
            slots.Clear();
            constraints.Clear();
            skins.Clear();
            animations.Clear();
            defaultSkin = null;
            defaultAnimation = null;
            canvas = null;
            userData = null;
            parent = null; //
        }

        /// <internal/>
        /// <private/>
        public void SortBones()
        {
            var total = sortedBones.Count;
            if (total <= 0)
            {
                return;
            }

            var sortHelper = sortedBones.ToArray();
            var index = 0;
            var count = 0;
            sortedBones.Clear();
            while (count < total)
            {
                var bone = sortHelper[index++];
                if (index >= total)
                {
                    index = 0;
                }

                if (sortedBones.Contains(bone))
                {
                    continue;
                }

                var flag = false;
                foreach (var constraint in constraints.Values)
                {
                    // Wait constraint.
                    if (constraint.root == bone && !sortedBones.Contains(constraint.target))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    continue;
                }
                if (bone.parent != null && !sortedBones.Contains(bone.parent))
                {
                    // Wait parent.
                    continue;
                }

                sortedBones.Add(bone);
                count++;
            }
        }

        /// <internal/>
        /// <private/>
        public void CacheFrames(uint frameRate)
        {
            if (cacheFrameRate > 0)
            {
                // TODO clear cache.
                return;
            }

            cacheFrameRate = frameRate;
            foreach (var k in animations.Keys)
            {
                animations[k].CacheFrames(cacheFrameRate);
            }
        }

        /// <internal/>
        /// <private/>
        public int SetCacheFrame(Matrix globalTransformMatrix, Transform transform)
        {
            var dataArray = parent.cachedFrames;
            var arrayOffset = dataArray.Count;

            dataArray.ResizeList(arrayOffset + 10, 0.0f);

            dataArray[arrayOffset] = globalTransformMatrix.A;
            dataArray[arrayOffset + 1] = globalTransformMatrix.B;
            dataArray[arrayOffset + 2] = globalTransformMatrix.C;
            dataArray[arrayOffset + 3] = globalTransformMatrix.D;
            dataArray[arrayOffset + 4] = globalTransformMatrix.Tx;
            dataArray[arrayOffset + 5] = globalTransformMatrix.Ty;
            dataArray[arrayOffset + 6] = transform.rotation;
            dataArray[arrayOffset + 7] = transform.skew;
            dataArray[arrayOffset + 8] = transform.scaleX;
            dataArray[arrayOffset + 9] = transform.scaleY;

            return arrayOffset;
        }

        /// <internal/>
        /// <private/>
        public void GetCacheFrame(Matrix globalTransformMatrix, Transform transform, int arrayOffset)
        {
            var dataArray = parent.cachedFrames;
            globalTransformMatrix.A = dataArray[arrayOffset];
            globalTransformMatrix.B = dataArray[arrayOffset + 1];
            globalTransformMatrix.C = dataArray[arrayOffset + 2];
            globalTransformMatrix.D = dataArray[arrayOffset + 3];
            globalTransformMatrix.Tx = dataArray[arrayOffset + 4];
            globalTransformMatrix.Ty = dataArray[arrayOffset + 5];
            transform.rotation = dataArray[arrayOffset + 6];
            transform.skew = dataArray[arrayOffset + 7];
            transform.scaleX = dataArray[arrayOffset + 8];
            transform.scaleY = dataArray[arrayOffset + 9];
            transform.x = globalTransformMatrix.Tx;
            transform.y = globalTransformMatrix.Ty;
        }

        /// <internal/>
        /// <private/>
        public void AddBone(BoneData value)
        {
            if (value != null && !string.IsNullOrEmpty(value.name))
            {
                if (bones.ContainsKey(value.name))
                {
                    Helper.Assert(false, "Same bone: " + value.name);
                    bones[value.name].ReturnToPool();
                }

                bones[value.name] = value;
                sortedBones.Add(value);
            }
        }

        /// <internal/>
        /// <private/>
        public void AddSlot(SlotData value)
        {
            if (value != null && !string.IsNullOrEmpty(value.name))
            {
                if (slots.ContainsKey(value.name))
                {
                    Helper.Assert(false, "Same slot: " + value.name);
                    slots[value.name].ReturnToPool();
                }

                slots[value.name] = value;
                sortedSlots.Add(value);
            }
        }

        /// <internal/>
        /// <private/>
        public void AddConstraint(ConstraintData value)
        {
            if (value != null && !string.IsNullOrEmpty(value.name))
            {
                if (constraints.ContainsKey(value.name))
                {
                    Helper.Assert(false, "Same constraint: " + value.name);
                    slots[value.name].ReturnToPool();
                }

                constraints[value.name] = value;
            }
        }

        /// <internal/>
        /// <private/>
        public void AddSkin(SkinData value)
        {
            if (value != null && !string.IsNullOrEmpty(value.name))
            {
                if (skins.ContainsKey(value.name))
                {
                    Helper.Assert(false, "Same slot: " + value.name);
                    skins[value.name].ReturnToPool();
                }

                value.parent = this;
                skins[value.name] = value;
                if (defaultSkin == null)
                {
                    defaultSkin = value;
                }

                if (value.name == "default")
                {
                    defaultSkin = value;
                }
            }
        }

        /// <internal/>
        /// <private/>
        public void AddAnimation(AnimationData value)
        {
            if (value != null && !string.IsNullOrEmpty(value.name))
            {
                if (animations.ContainsKey(value.name))
                {
                    Helper.Assert(false, "Same animation: " + value.name);
                    animations[value.name].ReturnToPool();
                }

                value.parent = this;
                animations[value.name] = value;
                animationNames.Add(value.name);
                if (defaultAnimation == null)
                {
                    defaultAnimation = value;
                }
            }
        }

        /// <internal/>
        /// <private/>
        internal void AddAction(ActionData value, bool isDefault)
        {
            if (isDefault)
            {
                defaultActions.Add(value);
            }
            else
            {
                actions.Add(value);
            }
        }

        /// <summary>
        /// - Get a specific done data.
        /// </summary>
        /// <param name="boneName">- The bone name.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的骨骼数据。
        /// </summary>
        /// <param name="boneName">- 骨骼名称。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public BoneData GetBone(string boneName)
        {
            return (!string.IsNullOrEmpty(boneName) && bones.ContainsKey(boneName)) ? bones[boneName] : null;
        }

        /// <summary>
        /// - Get a specific slot data.
        /// </summary>
        /// <param name="slotName">- The slot name.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的插槽数据。
        /// </summary>
        /// <param name="slotName">- 插槽名称。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public SlotData GetSlot(string slotName)
        {
            return (!string.IsNullOrEmpty(slotName) && slots.ContainsKey(slotName)) ? slots[slotName] : null;
        }

        /// <private/>
        public ConstraintData GetConstraint(string constraintName)
        {
            return constraints.ContainsKey(constraintName) ? constraints[constraintName] : null;
        }

        /// <summary>
        /// - Get a specific skin data.
        /// </summary>
        /// <param name="skinName">- The skin name.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定皮肤数据。
        /// </summary>
        /// <param name="skinName">- 皮肤名称。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public SkinData GetSkin(string skinName)
        {
            return !string.IsNullOrEmpty(skinName) ? (skins.ContainsKey(skinName) ? skins[skinName] : null) : defaultSkin;
        }

        /// <private/>
        public MeshDisplayData GetMesh(string skinName, string slotName, string meshName)
        {
            var skin = GetSkin(skinName);
            if (skin == null)
            {
                return null;
            }

            return skin.GetDisplay(slotName, meshName) as MeshDisplayData;
        }

        /// <summary>
        /// - Get a specific animation data.
        /// </summary>
        /// <param name="animationName">- The animation animationName.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的动画数据。
        /// </summary>
        /// <param name="animationName">- 动画名称。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public AnimationData GetAnimation(string animationName)
        {
            return !string.IsNullOrEmpty(animationName) ? (animations.ContainsKey(animationName) ? animations[animationName] : null) : defaultAnimation;
        }
    }
}
