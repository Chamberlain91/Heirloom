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
    /// - The animation state is generated when the animation data is played.
    /// </summary>
    /// <see cref="DragonBones.Animation"/>
    /// <see cref="DragonBones.AnimationData"/>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 动画状态由播放动画数据时产生。
    /// </summary>
    /// <see cref="DragonBones.Animation"/>
    /// <see cref="DragonBones.AnimationData"/>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    internal class AnimationState : BaseObject
    {
        /// <private/>
        public bool actionEnabled;
        /// <private/>
        public bool additiveBlending;
        /// <summary>
        /// - Whether the animation state has control over the display object properties of the slots.
        /// Sometimes blend a animation state does not want it to control the display object properties of the slots,
        /// especially if other animation state are controlling the display object properties of the slots.
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 动画状态是否对插槽的显示对象属性有控制权。
        /// 有时混合一个动画状态并不希望其控制插槽的显示对象属性，
        /// 尤其是其他动画状态正在控制这些插槽的显示对象属性时。
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public bool displayControl;
        /// <summary>
        /// - Whether to reset the objects without animation to the armature pose when the animation state is start to play.
        /// This property should usually be set to false when blend multiple animation states.
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 5.1</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 开始播放动画状态时是否将没有动画的对象重置为骨架初始值。
        /// 通常在混合多个动画状态时应该将该属性设置为 false。
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 5.1</version>
        /// <language>zh_CN</language>
        public bool resetToPose;
        /// <summary>
        /// - The play times. [0: Loop play, [1~N]: Play N times]
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 播放次数。 [0: 无限循环播放, [1~N]: 循环播放 N 次]
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public int playTimes;
        /// <summary>
        /// - The blend layer.
        /// High layer animation state will get the blend weight first.
        /// When the blend weight is assigned more than 1, the remaining animation states will no longer get the weight assigned.
        /// </summary>
        /// <readonly/>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 混合图层。
        /// 图层高的动画状态会优先获取混合权重。
        /// 当混合权重分配超过 1 时，剩余的动画状态将不再获得权重分配。
        /// </summary>
        /// <readonly/>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public int layer;
        /// <summary>
        /// - The play speed.
        /// The value is an overlay relationship with {@link dragonBones.Animation#timeScale}.
        /// [(-N~0): Reverse play, 0: Stop play, (0~1): Slow play, 1: Normal play, (1~N): Fast play]
        /// </summary>
        /// <default>1.0</default>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 播放速度。
        /// 该值与 {@link dragonBones.Animation#timeScale} 是叠加关系。
        /// [(-N~0): 倒转播放, 0: 停止播放, (0~1): 慢速播放, 1: 正常播放, (1~N): 快速播放]
        /// </summary>
        /// <default>1.0</default>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float timeScale;
        /// <summary>
        /// - The blend weight.
        /// </summary>
        /// <default>1.0</default>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 混合权重。
        /// </summary>
        /// <default>1.0</default>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public float weight;
        /// <summary>
        /// - The auto fade out time when the animation state play completed.
        /// [-1: Do not fade out automatically, [0~N]: The fade out time] (In seconds)
        /// </summary>
        /// <default>-1.0</default>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 动画状态播放完成后的自动淡出时间。
        /// [-1: 不自动淡出, [0~N]: 淡出时间] （以秒为单位）
        /// </summary>
        /// <default>-1.0</default>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public float autoFadeOutTime;
        /// <private/>
        public float fadeTotalTime;
        /// <summary>
        /// - The name of the animation state. (Can be different from the name of the animation data)
        /// </summary>
        /// <readonly/>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 动画状态名称。 （可以不同于动画数据）
        /// </summary>
        /// <readonly/>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public string name;
        /// <summary>
        /// - The blend group name of the animation state.
        /// This property is typically used to specify the substitution of multiple animation states blend.
        /// </summary>
        /// <readonly/>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 混合组名称。
        /// 该属性通常用来指定多个动画状态混合时的相互替换关系。
        /// </summary>
        /// <readonly/>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public string group;

        private int _timelineDirty;
        /// <summary>
        /// - xx: Play Enabled, Fade Play Enabled
        /// </summary>
        /// <internal/>
        /// <private/>
        internal int _playheadState;
        /// <summary>
        /// -1: Fade in, 0: Fade complete, 1: Fade out;
        /// </summary>
        /// <internal/>
        /// <private/>
        internal int _fadeState;
        /// <summary>
        /// -1: Fade start, 0: Fading, 1: Fade complete;
        /// </summary>
        /// <internal/>
        /// <private/>
        internal int _subFadeState;
        /// <internal/>
        /// <private/>
        internal float _position;
        /// <internal/>
        /// <private/>
        internal float _duration;
        private float _fadeTime;
        private float _time;
        /// <internal/>
        /// <private/>
        internal float _fadeProgress;
        /// <internal/>
        /// <private/>
        private float _weightResult;
        /// <internal/>
        /// <private/>
        internal readonly BlendState _blendState = new BlendState();
        private readonly List<string> _boneMask = new List<string>();
        private readonly List<BoneTimelineState> _boneTimelines = new List<BoneTimelineState>();
        private readonly List<SlotTimelineState> _slotTimelines = new List<SlotTimelineState>();
        private readonly List<ConstraintTimelineState> _constraintTimelines = new List<ConstraintTimelineState>();
        private readonly List<TimelineState> _poseTimelines = new List<TimelineState>();
        private readonly Dictionary<string, BonePose> _bonePoses = new Dictionary<string, BonePose>();
        /// <internal/>
        /// <private/>
        public AnimationData _animationData;
        private Armature _armature;
        /// <internal/>
        /// <private/>
        internal ActionTimelineState _actionTimeline = null; // Initial value.
        private ZOrderTimelineState _zOrderTimeline = null; // Initial value.
        /// <internal/>
        /// <private/>
        public AnimationState _parent = null; // Initial value.
        /// <private/>
        protected override void _OnClear()
        {
            foreach (var timeline in _boneTimelines)
            {
                timeline.ReturnToPool();
            }

            foreach (var timeline in _slotTimelines)
            {
                timeline.ReturnToPool();
            }

            foreach (var timeline in _constraintTimelines)
            {
                timeline.ReturnToPool();
            }

            foreach (var bonePose in _bonePoses.Values)
            {
                bonePose.ReturnToPool();
            }

            if (_actionTimeline != null)
            {
                _actionTimeline.ReturnToPool();
            }

            if (_zOrderTimeline != null)
            {
                _zOrderTimeline.ReturnToPool();
            }

            actionEnabled = false;
            additiveBlending = false;
            displayControl = false;
            resetToPose = false;
            playTimes = 1;
            layer = 0;

            timeScale = 1.0f;
            weight = 1.0f;
            autoFadeOutTime = 0.0f;
            fadeTotalTime = 0.0f;
            name = string.Empty;
            group = string.Empty;

            _timelineDirty = 2;
            _playheadState = 0;
            _fadeState = -1;
            _subFadeState = -1;
            _position = 0.0f;
            _duration = 0.0f;
            _fadeTime = 0.0f;
            _time = 0.0f;
            _fadeProgress = 0.0f;
            _weightResult = 0.0f;
            _blendState.Clear();
            _boneMask.Clear();
            _boneTimelines.Clear();
            _slotTimelines.Clear();
            _constraintTimelines.Clear();
            _bonePoses.Clear();
            _animationData = null; //
            _armature = null; //
            _actionTimeline = null; //
            _zOrderTimeline = null;
            _parent = null;
        }

        private void _UpdateTimelines()
        {
            { // Update constraint timelines.
                foreach (var constraint in _armature._constraints)
                {
                    var timelineDatas = _animationData.GetConstraintTimelines(constraint.name);

                    if (timelineDatas != null)
                    {
                        foreach (var timelineData in timelineDatas)
                        {
                            switch (timelineData.type)
                            {
                                case TimelineType.IKConstraint:
                                    {
                                        var timeline = BaseObject.BorrowObject<IKConstraintTimelineState>();
                                        timeline.constraint = constraint;
                                        timeline.Init(_armature, this, timelineData);
                                        _constraintTimelines.Add(timeline);
                                        break;
                                    }

                                default:
                                    break;
                            }
                        }
                    }
                    else if (resetToPose)
                    { // Pose timeline.
                        var timeline = BaseObject.BorrowObject<IKConstraintTimelineState>();
                        timeline.constraint = constraint;
                        timeline.Init(_armature, this, null);
                        _constraintTimelines.Add(timeline);
                        _poseTimelines.Add(timeline);
                    }
                }
            }
        }

        private void _UpdateBoneAndSlotTimelines()
        {
            { // Update bone timelines.
                Dictionary<string, List<BoneTimelineState>> boneTimelines = new Dictionary<string, List<BoneTimelineState>>();

                foreach (var timeline in _boneTimelines)
                {
                    // Create bone timelines map.
                    var timelineName = timeline.bone.Name;
                    if (!(boneTimelines.ContainsKey(timelineName)))
                    {
                        boneTimelines[timelineName] = new List<BoneTimelineState>();
                    }

                    boneTimelines[timelineName].Add(timeline);
                }

                foreach (var bone in _armature.GetBones())
                {
                    var timelineName = bone.Name;
                    if (!ContainsBoneMask(timelineName))
                    {
                        continue;
                    }

                    var timelineDatas = _animationData.GetBoneTimelines(timelineName);
                    if (boneTimelines.ContainsKey(timelineName))
                    {
                        // Remove bone timeline from map.
                        boneTimelines.Remove(timelineName);
                    }
                    else
                    {
                        // Create new bone timeline.
                        var bonePose = _bonePoses.ContainsKey(timelineName) ? _bonePoses[timelineName] : (_bonePoses[timelineName] = BaseObject.BorrowObject<BonePose>());
                        if (timelineDatas != null)
                        {
                            foreach (var timelineData in timelineDatas)
                            {
                                switch (timelineData.type)
                                {
                                    case TimelineType.BoneAll:
                                        {
                                            var timeline = BaseObject.BorrowObject<BoneAllTimelineState>();
                                            timeline.bone = bone;
                                            timeline.bonePose = bonePose;
                                            timeline.Init(_armature, this, timelineData);
                                            _boneTimelines.Add(timeline);
                                            break;
                                        }
                                    case TimelineType.BoneTranslate:
                                        {
                                            var timeline = BaseObject.BorrowObject<BoneTranslateTimelineState>();
                                            timeline.bone = bone;
                                            timeline.bonePose = bonePose;
                                            timeline.Init(_armature, this, timelineData);
                                            _boneTimelines.Add(timeline);
                                            break;
                                        }
                                    case TimelineType.BoneRotate:
                                        {
                                            var timeline = BaseObject.BorrowObject<BoneRotateTimelineState>();
                                            timeline.bone = bone;
                                            timeline.bonePose = bonePose;
                                            timeline.Init(_armature, this, timelineData);
                                            _boneTimelines.Add(timeline);
                                            break;
                                        }
                                    case TimelineType.BoneScale:
                                        {
                                            var timeline = BaseObject.BorrowObject<BoneScaleTimelineState>();
                                            timeline.bone = bone;
                                            timeline.bonePose = bonePose;
                                            timeline.Init(_armature, this, timelineData);
                                            _boneTimelines.Add(timeline);
                                            break;
                                        }

                                    default:
                                        break;
                                }
                            }
                        }
                        else if (resetToPose)
                        { // Pose timeline.
                            var timeline = BaseObject.BorrowObject<BoneAllTimelineState>();
                            timeline.bone = bone;
                            timeline.bonePose = bonePose;
                            timeline.Init(_armature, this, null);
                            _boneTimelines.Add(timeline);
                            _poseTimelines.Add(timeline);
                        }
                    }
                }

                foreach (var timelines in boneTimelines.Values)
                {
                    // Remove bone timelines.
                    foreach (var timeline in timelines)
                    {
                        _boneTimelines.Remove(timeline);
                        timeline.ReturnToPool();
                    }
                }
            }

            { // Update slot timelines.
                Dictionary<string, List<SlotTimelineState>> slotTimelines = new Dictionary<string, List<SlotTimelineState>>();
                List<int> ffdFlags = new List<int>();

                foreach (var timeline in _slotTimelines)
                {
                    // Create slot timelines map.
                    var timelineName = timeline.slot.Name;
                    if (!(slotTimelines.ContainsKey(timelineName)))
                    {
                        slotTimelines[timelineName] = new List<SlotTimelineState>();
                    }

                    slotTimelines[timelineName].Add(timeline);
                }

                foreach (var slot in _armature.GetSlots())
                {
                    var boneName = slot.parent.Name;
                    if (!ContainsBoneMask(boneName))
                    {
                        continue;
                    }

                    var timelineName = slot.Name;
                    var timelineDatas = _animationData.GetSlotTimelines(timelineName);

                    if (slotTimelines.ContainsKey(timelineName))
                    {
                        // Remove slot timeline from map.
                        slotTimelines.Remove(timelineName);
                    }
                    else
                    {
                        // Create new slot timeline.
                        var displayIndexFlag = false;
                        var colorFlag = false;
                        ffdFlags.Clear();

                        if (timelineDatas != null)
                        {
                            foreach (var timelineData in timelineDatas)
                            {
                                switch (timelineData.type)
                                {
                                    case TimelineType.SlotDisplay:
                                        {
                                            var timeline = BaseObject.BorrowObject<SlotDislayTimelineState>();
                                            timeline.slot = slot;
                                            timeline.Init(_armature, this, timelineData);
                                            _slotTimelines.Add(timeline);
                                            displayIndexFlag = true;
                                            break;
                                        }
                                    case TimelineType.SlotColor:
                                        {
                                            var timeline = BaseObject.BorrowObject<SlotColorTimelineState>();
                                            timeline.slot = slot;
                                            timeline.Init(_armature, this, timelineData);
                                            _slotTimelines.Add(timeline);
                                            colorFlag = true;
                                            break;
                                        }
                                    case TimelineType.SlotDeform:
                                        {
                                            var timeline = BaseObject.BorrowObject<DeformTimelineState>();
                                            timeline.slot = slot;
                                            timeline.Init(_armature, this, timelineData);
                                            _slotTimelines.Add(timeline);
                                            ffdFlags.Add((int)timeline.vertexOffset);
                                            break;
                                        }

                                    default:
                                        break;
                                }
                            }
                        }

                        if (resetToPose)
                        {
                            // Pose timeline.
                            if (!displayIndexFlag)
                            {
                                var timeline = BaseObject.BorrowObject<SlotDislayTimelineState>();
                                timeline.slot = slot;
                                timeline.Init(_armature, this, null);
                                _slotTimelines.Add(timeline);
                                _poseTimelines.Add(timeline);
                            }

                            if (!colorFlag)
                            {
                                var timeline = BaseObject.BorrowObject<SlotColorTimelineState>();
                                timeline.slot = slot;
                                timeline.Init(_armature, this, null);
                                _slotTimelines.Add(timeline);
                                _poseTimelines.Add(timeline);
                            }

                            if (slot.RawDisplayDatas != null)
                            {
                                foreach (var displayData in slot.RawDisplayDatas)
                                {
                                    if (displayData != null && displayData.type == DisplayType.Mesh)
                                    {
                                        var meshOffset = (displayData as MeshDisplayData).vertices.offset;
                                        if (!ffdFlags.Contains(meshOffset))
                                        {
                                            var timeline = BaseObject.BorrowObject<DeformTimelineState>();
                                            timeline.vertexOffset = meshOffset; //
                                            timeline.slot = slot;
                                            timeline.Init(_armature, this, null);
                                            _slotTimelines.Add(timeline);
                                            _poseTimelines.Add(timeline);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var timelines in slotTimelines.Values)
                {
                    // Remove slot timelines.
                    foreach (var timeline in timelines)
                    {
                        _slotTimelines.Remove(timeline);
                        timeline.ReturnToPool();
                    }
                }
            }

            // {
            //     // Update constraint timelines.
            //     Dictionary<string, List<ConstraintTimelineState>> constraintTimelines = new Dictionary<string, List<ConstraintTimelineState>>();
            //     foreach (var timeline in this._constraintTimelines)
            //     { // Create constraint timelines map.
            //         var timelineName = timeline.constraint.name;
            //         if (!(constraintTimelines.ContainsKey(timelineName)))
            //         {
            //             constraintTimelines[timelineName] = new List<ConstraintTimelineState>();
            //         }

            //         constraintTimelines[timelineName].Add(timeline);
            //     }

            //     foreach (var constraint in this._armature._constraints)
            //     {
            //         var timelineName = constraint.name;
            //         var timelineDatas = this._animationData.GetConstraintTimelines(timelineName);

            //         if (constraintTimelines.ContainsKey(timelineName))
            //         {
            //             // Remove constraint timeline from map.
            //             constraintTimelines.Remove(timelineName);
            //         }
            //         else
            //         {
            //             // Create new constraint timeline.
            //             if (timelineDatas != null)
            //             {
            //                 foreach (var timelineData in timelineDatas)
            //                 {
            //                     switch (timelineData.type)
            //                     {
            //                         case TimelineType.IKConstraint:
            //                             {
            //                                 var timeline = BaseObject.BorrowObject<IKConstraintTimelineState>();
            //                                 timeline.constraint = constraint;
            //                                 timeline.Init(this._armature, this, timelineData);
            //                                 this._constraintTimelines.Add(timeline);
            //                                 break;
            //                             }

            //                         default:
            //                             break;
            //                     }
            //                 }
            //             }
            //             else if (this.resetToPose)
            //             {
            //                 // Pose timeline.
            //                 var timeline = BaseObject.BorrowObject<IKConstraintTimelineState>();
            //                 timeline.constraint = constraint;
            //                 timeline.Init(this._armature, this, null);
            //                 this._constraintTimelines.Add(timeline);
            //                 this._poseTimelines.Add(timeline);
            //             }
            //         }
            //     }

            //     foreach (var timelines in constraintTimelines.Values)
            //     { // Remove constraint timelines.
            //         foreach (var timeline in timelines)
            //         {
            //             this._constraintTimelines.Remove(timeline);
            //             timeline.ReturnToPool();
            //         }
            //     }
            // }
        }

        private void _AdvanceFadeTime(float passedTime)
        {
            var isFadeOut = _fadeState > 0;

            if (_subFadeState < 0)
            {
                // Fade start event.
                _subFadeState = 0;

                var eventType = isFadeOut ? EventObject.FADE_OUT : EventObject.FADE_IN;
                if (_armature.EventDispatcher.HasDBEventListener(eventType))
                {
                    var eventObject = BaseObject.BorrowObject<EventObject>();
                    eventObject.type = eventType;
                    eventObject.armature = _armature;
                    eventObject.animationState = this;
                    _armature._dragonBones.BufferEvent(eventObject);
                }
            }

            if (passedTime < 0.0f)
            {
                passedTime = -passedTime;
            }

            _fadeTime += passedTime;

            if (_fadeTime >= fadeTotalTime)
            {
                // Fade complete.
                _subFadeState = 1;
                _fadeProgress = isFadeOut ? 0.0f : 1.0f;
            }
            else if (_fadeTime > 0.0f)
            {
                // Fading.
                _fadeProgress = isFadeOut ? (1.0f - _fadeTime / fadeTotalTime) : (_fadeTime / fadeTotalTime);
            }
            else
            {
                // Before fade.
                _fadeProgress = isFadeOut ? 1.0f : 0.0f;
            }

            if (_subFadeState > 0)
            {
                // Fade complete event.
                if (!isFadeOut)
                {
                    _playheadState |= 1; // x1
                    _fadeState = 0;
                }

                var eventType = isFadeOut ? EventObject.FADE_OUT_COMPLETE : EventObject.FADE_IN_COMPLETE;
                if (_armature.EventDispatcher.HasDBEventListener(eventType))
                {
                    var eventObject = BaseObject.BorrowObject<EventObject>();
                    eventObject.type = eventType;
                    eventObject.armature = _armature;
                    eventObject.animationState = this;
                    _armature._dragonBones.BufferEvent(eventObject);
                }
            }
        }

        /// <internal/>
        /// <private/>
        internal void Init(Armature armature, AnimationData animationData, AnimationConfig animationConfig)
        {
            if (_armature != null)
            {
                return;
            }

            _armature = armature;

            _animationData = animationData;
            resetToPose = animationConfig.resetToPose;
            additiveBlending = animationConfig.additiveBlending;
            displayControl = animationConfig.displayControl;
            actionEnabled = animationConfig.actionEnabled;
            layer = animationConfig.layer;
            playTimes = animationConfig.playTimes;
            timeScale = animationConfig.timeScale;
            fadeTotalTime = animationConfig.fadeInTime;
            autoFadeOutTime = animationConfig.autoFadeOutTime;
            weight = animationConfig.weight;
            name = animationConfig.name.Length > 0 ? animationConfig.name : animationConfig.animation;
            group = animationConfig.group;

            if (animationConfig.pauseFadeIn)
            {
                _playheadState = 2; // 10
            }
            else
            {
                _playheadState = 3; // 11
            }

            if (animationConfig.duration < 0.0f)
            {
                _position = 0.0f;
                _duration = _animationData.duration;
                if (animationConfig.position != 0.0f)
                {
                    if (timeScale >= 0.0f)
                    {
                        _time = animationConfig.position;
                    }
                    else
                    {
                        _time = animationConfig.position - _duration;
                    }
                }
                else
                {
                    _time = 0.0f;
                }
            }
            else
            {
                _position = animationConfig.position;
                _duration = animationConfig.duration;
                _time = 0.0f;
            }

            if (timeScale < 0.0f && _time == 0.0f)
            {
                _time = -0.000001f; // Turn to end.
            }

            if (fadeTotalTime <= 0.0f)
            {
                _fadeProgress = 0.999999f; // Make different.
            }

            if (animationConfig.boneMask.Count > 0)
            {
                _boneMask.ResizeList(animationConfig.boneMask.Count);
                for (int i = 0, l = _boneMask.Count; i < l; ++i)
                {
                    _boneMask[i] = animationConfig.boneMask[i];
                }
            }

            _actionTimeline = BaseObject.BorrowObject<ActionTimelineState>();
            _actionTimeline.Init(_armature, this, _animationData.actionTimeline);
            _actionTimeline.currentTime = _time;
            if (_actionTimeline.currentTime < 0.0f)
            {
                _actionTimeline.currentTime = _duration - _actionTimeline.currentTime;
            }

            if (_animationData.zOrderTimeline != null)
            {
                _zOrderTimeline = BaseObject.BorrowObject<ZOrderTimelineState>();
                _zOrderTimeline.Init(_armature, this, _animationData.zOrderTimeline);
            }
        }
        /// <internal/>
        /// <private/>
        internal void AdvanceTime(float passedTime, float cacheFrameRate)
        {
            _blendState.dirty = true;

            // Update fade time.
            if (_fadeState != 0 || _subFadeState != 0)
            {
                _AdvanceFadeTime(passedTime);
            }

            // Update time.
            if (_playheadState == 3)
            {
                // 11
                if (timeScale != 1.0f)
                {
                    passedTime *= timeScale;
                }

                _time += passedTime;
            }

            // Update timeline.
            if (_timelineDirty != 0)
            {
                if (_timelineDirty == 2)
                {
                    _UpdateTimelines();
                }

                _timelineDirty = 0;
                _UpdateBoneAndSlotTimelines();
            }

            if (weight == 0.0f)
            {
                return;
            }

            var isCacheEnabled = _fadeState == 0 && cacheFrameRate > 0.0f;
            var isUpdateTimeline = true;
            var isUpdateBoneTimeline = true;
            var time = _time;
            _weightResult = weight * _fadeProgress;

            if (_parent != null)
            {
                _weightResult *= _parent._weightResult / _parent._fadeProgress;
            }

            if (_actionTimeline.playState <= 0)
            {
                // Update main timeline.
                _actionTimeline.Update(time);
            }

            if (isCacheEnabled)
            {
                // Cache time internval.
                var internval = cacheFrameRate * 2.0f;
                _actionTimeline.currentTime = (float)Math.Floor(_actionTimeline.currentTime * internval) / internval;
            }

            if (_zOrderTimeline != null && _zOrderTimeline.playState <= 0)
            {
                // Update zOrder timeline.
                _zOrderTimeline.Update(time);
            }

            if (isCacheEnabled)
            {
                // Update cache.
                var cacheFrameIndex = (int)Math.Floor(_actionTimeline.currentTime * cacheFrameRate); // uint
                if (_armature._cacheFrameIndex == cacheFrameIndex)
                {
                    // Same cache.
                    isUpdateTimeline = false;
                    isUpdateBoneTimeline = false;
                }
                else
                {
                    _armature._cacheFrameIndex = cacheFrameIndex;
                    if (_animationData.cachedFrames[cacheFrameIndex])
                    {
                        // Cached.
                        isUpdateBoneTimeline = false;
                    }
                    else
                    {
                        // Cache.
                        _animationData.cachedFrames[cacheFrameIndex] = true;
                    }
                }
            }

            if (isUpdateTimeline)
            {
                if (isUpdateBoneTimeline)
                {
                    for (int i = 0, l = _boneTimelines.Count; i < l; ++i)
                    {
                        var timeline = _boneTimelines[i];

                        if (timeline.playState <= 0)
                        {
                            timeline.Update(time);
                        }

                        if (i == l - 1 || timeline.bone != _boneTimelines[i + 1].bone)
                        {
                            var state = timeline.bone._blendState.Update(_weightResult, layer);
                            if (state != 0)
                            {
                                timeline.Blend(state);
                            }
                        }
                    }
                }

                if (displayControl)
                {
                    for (int i = 0, l = _slotTimelines.Count; i < l; ++i)
                    {
                        var timeline = _slotTimelines[i];
                        if (timeline.slot != null)
                        {
                            var displayController = timeline.slot.displayController;

                            if (
                                displayController == null ||
                                displayController == name ||
                                displayController == group
                            )
                            {
                                if (timeline.playState <= 0)
                                {
                                    timeline.Update(time);
                                }
                            }
                        }
                    }
                }

                for (int i = 0, l = _constraintTimelines.Count; i < l; ++i)
                {
                    var timeline = _constraintTimelines[i];
                    if (timeline.playState <= 0)
                    {
                        timeline.Update(time);
                    }
                }
            }

            if (_fadeState == 0)
            {
                if (_subFadeState > 0)
                {
                    _subFadeState = 0;

                    if (_poseTimelines.Count > 0)
                    {
                        foreach (var timeline in _poseTimelines)
                        {
                            if (timeline is BoneTimelineState)
                            {
                                _boneTimelines.Remove(timeline as BoneTimelineState);
                            }
                            else if (timeline is SlotTimelineState)
                            {
                                _slotTimelines.Remove(timeline as SlotTimelineState);
                            }
                            else if (timeline is ConstraintTimelineState)
                            {
                                _constraintTimelines.Remove(timeline as ConstraintTimelineState);
                            }

                            timeline.ReturnToPool();
                        }

                        _poseTimelines.Clear();
                    }
                }

                if (_actionTimeline.playState > 0)
                {
                    if (autoFadeOutTime >= 0.0f)
                    {
                        // Auto fade out.
                        FadeOut(autoFadeOutTime);
                    }
                }
            }
        }

        /// <summary>
        /// - Continue play.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 继续播放。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void Play()
        {
            _playheadState = 3; // 11
        }
        /// <summary>
        /// - Stop play.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 暂停播放。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void Stop()
        {
            _playheadState &= 1; // 0x
        }
        /// <summary>
        /// - Fade out the animation state.
        /// </summary>
        /// <param name="fadeOutTime">- The fade out time. (In seconds)</param>
        /// <param name="pausePlayhead">- Whether to pause the animation playing when fade out.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 淡出动画状态。
        /// </summary>
        /// <param name="fadeOutTime">- 淡出时间。 （以秒为单位）</param>
        /// <param name="pausePlayhead">- 淡出时是否暂停播放。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void FadeOut(float fadeOutTime, bool pausePlayhead = true)
        {
            if (fadeOutTime < 0.0f)
            {
                fadeOutTime = 0.0f;
            }

            if (pausePlayhead)
            {
                _playheadState &= 2; // x0
            }

            if (_fadeState > 0)
            {
                if (fadeOutTime > fadeTotalTime - _fadeTime)
                {
                    // If the animation is already in fade out, the new fade out will be ignored.
                    return;
                }
            }
            else
            {
                _fadeState = 1;
                _subFadeState = -1;

                if (fadeOutTime <= 0.0f || _fadeProgress <= 0.0f)
                {
                    _fadeProgress = 0.000001f; // Modify fade progress to different value.
                }

                foreach (var timeline in _boneTimelines)
                {
                    timeline.FadeOut();
                }

                foreach (var timeline in _slotTimelines)
                {
                    timeline.FadeOut();
                }

                foreach (var timeline in _constraintTimelines)
                {
                    timeline.FadeOut();
                }
            }

            displayControl = false; //
            fadeTotalTime = _fadeProgress > 0.000001f ? fadeOutTime / _fadeProgress : 0.0f;
            _fadeTime = fadeTotalTime * (1.0f - _fadeProgress);
        }

        /// <summary>
        /// - Check if a specific bone mask is included.
        /// </summary>
        /// <param name="boneName">- The bone name.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查是否包含特定骨骼遮罩。
        /// </summary>
        /// <param name="boneName">- 骨骼名称。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public bool ContainsBoneMask(string boneName)
        {
            return _boneMask.Count == 0 || _boneMask.IndexOf(boneName) >= 0;
        }
        /// <summary>
        /// - Add a specific bone mask.
        /// </summary>
        /// <param name="boneName">- The bone name.</param>
        /// <param name="recursive">- Whether or not to add a mask to the bone's sub-bone.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 添加特定的骨骼遮罩。
        /// </summary>
        /// <param name="boneName">- 骨骼名称。</param>
        /// <param name="recursive">- 是否为该骨骼的子骨骼添加遮罩。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void AddBoneMask(string boneName, bool recursive = true)
        {
            var currentBone = _armature.GetBone(boneName);
            if (currentBone == null)
            {
                return;
            }

            if (_boneMask.IndexOf(boneName) < 0)
            {
                // Add mixing
                _boneMask.Add(boneName);
            }

            if (recursive)
            {
                // Add recursive mixing.
                foreach (var bone in _armature.GetBones())
                {
                    if (_boneMask.IndexOf(bone.Name) < 0 && currentBone.Contains(bone))
                    {
                        _boneMask.Add(bone.Name);
                    }
                }
            }

            _timelineDirty = 1;
        }
        /// <summary>
        /// - Remove the mask of a specific bone.
        /// </summary>
        /// <param name="boneName">- The bone name.</param>
        /// <param name="recursive">- Whether to remove the bone's sub-bone mask.</param>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 删除特定骨骼的遮罩。
        /// </summary>
        /// <param name="boneName">- 骨骼名称。</param>
        /// <param name="recursive">- 是否删除该骨骼的子骨骼遮罩。</param>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void RemoveBoneMask(string boneName, bool recursive = true)
        {
            if (_boneMask.Contains(boneName))
            {
                _boneMask.Remove(boneName);
            }

            if (recursive)
            {
                var currentBone = _armature.GetBone(boneName);
                if (currentBone != null)
                {
                    var bones = _armature.GetBones();
                    if (_boneMask.Count > 0)
                    {
                        // Remove recursive mixing.
                        foreach (var bone in bones)
                        {
                            if (_boneMask.Contains(bone.Name) && currentBone.Contains(bone))
                            {
                                _boneMask.Remove(bone.Name);
                            }
                        }
                    }
                    else
                    {
                        // Add unrecursive mixing.
                        foreach (var bone in bones)
                        {
                            if (bone == currentBone)
                            {
                                continue;
                            }

                            if (!currentBone.Contains(bone))
                            {
                                _boneMask.Add(bone.Name);
                            }
                        }
                    }
                }
            }

            _timelineDirty = 1;
        }
        /// <summary>
        /// - Remove all bone masks.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 删除所有骨骼遮罩。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void RemoveAllBoneMask()
        {
            _boneMask.Clear();
            _timelineDirty = 1;
        }
        /// <summary>
        /// - Whether the animation state is fading in.
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否正在淡入。
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>zh_CN</language>
        public bool isFadeIn
        {
            get { return _fadeState < 0; }
        }
        /// <summary>
        /// - Whether the animation state is fading out.
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否正在淡出。
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>zh_CN</language>
        public bool isFadeOut
        {
            get { return _fadeState > 0; }
        }
        /// <summary>
        /// - Whether the animation state is fade completed.
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否淡入或淡出完毕。
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>zh_CN</language>
        public bool isFadeComplete
        {
            get { return _fadeState == 0; }
        }
        /// <summary>
        /// - Whether the animation state is playing.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否正在播放。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public bool isPlaying
        {
            get { return (_playheadState & 2) != 0 && _actionTimeline.playState <= 0; }
        }
        /// <summary>
        /// - Whether the animation state is play completed.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 是否播放完毕。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public bool isCompleted
        {
            get { return _actionTimeline.playState > 0; }
        }
        /// <summary>
        /// - The times has been played.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 已经循环播放的次数。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public int currentPlayTimes
        {
            get { return _actionTimeline.currentPlayTimes; }
        }

        /// <summary>
        /// - The total time. (In seconds)
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 总播放时间。 （以秒为单位）
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float totalTime
        {
            get { return _duration; }
        }
        /// <summary>
        /// - The time is currently playing. (In seconds)
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 当前播放的时间。 （以秒为单位）
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float currentTime
        {
            get { return _actionTimeline.currentTime; }
            set
            {
                var currentPlayTimes = _actionTimeline.currentPlayTimes - (_actionTimeline.playState > 0 ? 1 : 0);
                if (value < 0.0f || _duration < value)
                {
                    value = (value % _duration) + currentPlayTimes * _duration;
                    if (value < 0.0f)
                    {
                        value += _duration;
                    }
                }

                if (playTimes > 0 && currentPlayTimes == playTimes - 1 && value == _duration)
                {
                    value = _duration - 0.000001f;
                }

                if (_time == value)
                {
                    return;
                }

                _time = value;
                _actionTimeline.SetCurrentTime(_time);

                if (_zOrderTimeline != null)
                {
                    _zOrderTimeline.playState = -1;
                }

                foreach (var timeline in _boneTimelines)
                {
                    timeline.playState = -1;
                }

                foreach (var timeline in _slotTimelines)
                {
                    timeline.playState = -1;
                }
            }
        }
    }

    /// <internal/>
    /// <private/>
    internal class BonePose : BaseObject
    {
        public readonly Transform current = new Transform();
        public readonly Transform delta = new Transform();
        public readonly Transform result = new Transform();

        protected override void _OnClear()
        {
            current.Identity();
            delta.Identity();
            result.Identity();
        }
    }

    /// <internal/>
    /// <private/>
    internal class BlendState
    {
        public bool dirty;
        public int layer;
        public float leftWeight;
        public float layerWeight;
        public float blendWeight;

        /// <summary>
        /// -1: First blending, 0: No blending, 1: Blending.
        /// </summary>
        public int Update(float weight, int p_layer)
        {
            if (dirty)
            {
                if (leftWeight > 0.0f)
                {
                    if (layer != p_layer)
                    {
                        if (layerWeight >= leftWeight)
                        {
                            leftWeight = 0.0f;

                            return 0;
                        }
                        else
                        {
                            layer = p_layer;
                            leftWeight -= layerWeight;
                            layerWeight = 0.0f;
                        }
                    }
                }
                else
                {
                    return 0;
                }

                weight *= leftWeight;
                layerWeight += weight;
                blendWeight = weight;

                return 2;
            }

            dirty = true;
            layer = p_layer;
            layerWeight = weight;
            leftWeight = 1.0f;
            blendWeight = weight;

            return 1;
        }

        public void Clear()
        {
            dirty = false;
            layer = 0;
            leftWeight = 0.0f;
            layerWeight = 0.0f;
            blendWeight = 0.0f;
        }
    }
}
