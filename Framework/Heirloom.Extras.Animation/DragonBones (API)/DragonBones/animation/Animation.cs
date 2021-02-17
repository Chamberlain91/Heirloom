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
using System.Collections.Generic;

namespace DragonBones
{
    /// <summary>
    /// - The animation player is used to play the animation data and manage the animation states.
    /// </summary>
    /// <see cref="DragonBones.AnimationData"/>
    /// <see cref="DragonBones.AnimationState"/>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 动画播放器用来播放动画数据和管理动画状态。
    /// </summary>
    /// <see cref="DragonBones.AnimationData"/>
    /// <see cref="DragonBones.AnimationState"/>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    internal class Animation : BaseObject
    {
        /// <summary>
        /// - The play speed of all animations. [0: Stop, (0~1): Slow, 1: Normal, (1~N): Fast]
        /// </summary>
        /// <default>1.0</default>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 所有动画的播放速度。 [0: 停止播放, (0~1): 慢速播放, 1: 正常播放, (1~N): 快速播放]
        /// </summary>
        /// <default>1.0</default>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public float TimeScale;

        private bool _lockUpdate;

        // Update bones and slots cachedFrameIndices.
        private bool _animationDirty;
        private float _inheritTimeScale;
        private readonly List<string> _animationNames = new List<string>();
        private readonly List<AnimationState> _animationStates = new List<AnimationState>();
        private readonly Dictionary<string, AnimationData> _animations = new Dictionary<string, AnimationData>();
        private Armature _armature;
        private AnimationConfig _animationConfig = null; // Initial value.
        private AnimationState _lastAnimationState;

        /// <private/>
        protected override void _OnClear()
        {
            foreach (var animationState in _animationStates)
            {
                animationState.ReturnToPool();
            }

            if (_animationConfig != null)
            {
                _animationConfig.ReturnToPool();
            }

            TimeScale = 1.0f;

            _lockUpdate = false;

            _animationDirty = false;
            _inheritTimeScale = 1.0f;
            _animationNames.Clear();
            _animationStates.Clear();
            _animations.Clear();
            _armature = null; //
            _animationConfig = null; //
            _lastAnimationState = null;
        }

        private void _FadeOut(AnimationConfig animationConfig)
        {
            switch (animationConfig.fadeOutMode)
            {
                case AnimationFadeOutMode.SameLayer:
                    foreach (var animationState in _animationStates)
                    {
                        if (animationState._parent != null)
                        {
                            continue;
                        }

                        if (animationState.layer == animationConfig.layer)
                        {
                            animationState.FadeOut(animationConfig.fadeOutTime, animationConfig.pauseFadeOut);
                        }
                    }
                    break;
                case AnimationFadeOutMode.SameGroup:
                    foreach (var animationState in _animationStates)
                    {
                        if (animationState._parent != null)
                        {
                            continue;
                        }

                        if (animationState.group == animationConfig.group)
                        {
                            animationState.FadeOut(animationConfig.fadeOutTime, animationConfig.pauseFadeOut);
                        }
                    }
                    break;
                case AnimationFadeOutMode.SameLayerAndGroup:
                    foreach (var animationState in _animationStates)
                    {
                        if (animationState._parent != null)
                        {
                            continue;
                        }

                        if (animationState.layer == animationConfig.layer &&
                            animationState.group == animationConfig.group)
                        {
                            animationState.FadeOut(animationConfig.fadeOutTime, animationConfig.pauseFadeOut);
                        }
                    }
                    break;
                case AnimationFadeOutMode.All:
                    foreach (var animationState in _animationStates)
                    {
                        if (animationState._parent != null)
                        {
                            continue;
                        }

                        animationState.FadeOut(animationConfig.fadeOutTime, animationConfig.pauseFadeOut);
                    }
                    break;
                case AnimationFadeOutMode.None:
                case AnimationFadeOutMode.Single:
                default:
                    break;
            }
        }

        /// <internal/>
        /// <private/>
        internal void Init(Armature armature)
        {
            if (_armature != null)
            {
                return;
            }

            _armature = armature;
            _animationConfig = BaseObject.BorrowObject<AnimationConfig>();
        }

        /// <internal/>
        /// <private/>
        internal void AdvanceTime(float passedTime)
        {
            if (passedTime < 0.0f)
            {
                // Only animationState can reverse play.
                passedTime = -passedTime;
            }

            if (_armature.InheritAnimation && _armature._parent != null)
            {
                // Inherit parent animation timeScale.
                _inheritTimeScale = _armature._parent._armature.Animation._inheritTimeScale * TimeScale;
            }
            else
            {
                _inheritTimeScale = TimeScale;
            }

            if (_inheritTimeScale != 1.0f)
            {
                passedTime *= _inheritTimeScale;
            }

            var animationStateCount = _animationStates.Count;
            if (animationStateCount == 1)
            {
                var animationState = _animationStates[0];
                if (animationState._fadeState > 0 && animationState._subFadeState > 0)
                {
                    animationState.ReturnToPool();

                    _animationStates.Clear();
                    _lastAnimationState = null;
                }
                else
                {
                    var animationData = animationState._animationData;
                    var cacheFrameRate = animationData.cacheFrameRate;

                    if (_animationDirty && cacheFrameRate > 0.0f)
                    {
                        // Update cachedFrameIndices.
                        _animationDirty = false;
                        foreach (var bone in _armature.GetBones())
                        {
                            bone._cachedFrameIndices = animationData.GetBoneCachedFrameIndices(bone.Name);
                        }

                        foreach (var slot in _armature.GetSlots())
                        {
                            var rawDisplayDatas = slot.RawDisplayDatas;
                            if (rawDisplayDatas != null && rawDisplayDatas.Count > 0)
                            {
                                var rawDsplayData = rawDisplayDatas[0];
                                if (rawDsplayData != null)
                                {
                                    if (rawDsplayData.parent == _armature.ArmatureData.defaultSkin)
                                    {
                                        slot._cachedFrameIndices = animationData.GetSlotCachedFrameIndices(slot.Name);
                                        continue;
                                    }
                                }
                            }

                            slot._cachedFrameIndices = null;
                        }
                    }

                    animationState.AdvanceTime(passedTime, cacheFrameRate);
                }
            }
            else if (animationStateCount > 1)
            {
                for (int i = 0, r = 0; i < animationStateCount; ++i)
                {
                    var animationState = _animationStates[i];
                    if (animationState._fadeState > 0 && animationState._subFadeState > 0)
                    {
                        r++;
                        animationState.ReturnToPool();

                        _animationDirty = true;
                        if (_lastAnimationState == animationState)
                        {
                            // Update last animation state.
                            _lastAnimationState = null;
                        }
                    }
                    else
                    {
                        if (r > 0)
                        {
                            _animationStates[i - r] = animationState;
                        }

                        animationState.AdvanceTime(passedTime, 0.0f);
                    }

                    if (i == animationStateCount - 1 && r > 0)
                    {
                        // Modify animation states size.
                        _animationStates.ResizeList(_animationStates.Count - r);
                        if (_lastAnimationState == null && _animationStates.Count > 0)
                        {
                            _lastAnimationState = _animationStates[_animationStates.Count - 1];
                        }
                    }
                }

                _armature._cacheFrameIndex = -1;
            }
            else
            {
                _armature._cacheFrameIndex = -1;
            }
        }

        /// <summary>
        /// - Clear all animations states.
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 清除所有的动画状态。
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public void Reset()
        {
            foreach (var animationState in _animationStates)
            {
                animationState.ReturnToPool();
            }

            _animationDirty = false;
            _animationConfig.Clear();
            _animationStates.Clear();
            _lastAnimationState = null;
        }

        /// <summary>
        /// - Pause a specific animation state.
        /// </summary>
        /// <param name="animationName">- The name of animation state. (If not set, it will pause all animations)</param>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 暂停指定动画状态的播放。
        /// </summary>
        /// <param name="animationName">- 动画状态名称。 （如果未设置，则暂停所有动画）</param>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void Stop(string animationName = null)
        {
            if (animationName != null)
            {
                var animationState = GetState(animationName);
                if (animationState != null)
                {
                    animationState.Stop();
                }
            }
            else
            {
                foreach (var animationState in _animationStates)
                {
                    animationState.Stop();
                }
            }
        }

        /// <summary>
        /// - Play animation with a specific animation config.
        /// The API is still in the experimental phase and may encounter bugs or stability or compatibility issues when used.
        /// </summary>
        /// <param name="animationConfig">- The animation config.</param>
        /// <returns>The playing animation state.</returns>
        /// <see cref="DragonBones.AnimationConfig"/>
        /// <beta/>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 通过指定的动画配置来播放动画。
        /// 该 API 仍在实验阶段，使用时可能遭遇 bug 或稳定性或兼容性问题。
        /// </summary>
        /// <param name="animationConfig">- 动画配置。</param>
        /// <returns>播放的动画状态。</returns>
        /// <see cref="DragonBones.AnimationConfig"/>
        /// <beta/>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public AnimationState PlayConfig(AnimationConfig animationConfig)
        {
            var animationName = animationConfig.animation;
            if (!(_animations.ContainsKey(animationName)))
            {
                Helper.Assert(false,
                    "Non-existent animation.\n" +
                    "DragonBones name: " + _armature.ArmatureData.parent.name +
                    "Armature name: " + _armature.Name +
                    "Animation name: " + animationName
                );

                return null;
            }

            var animationData = _animations[animationName];

            if (animationConfig.fadeOutMode == AnimationFadeOutMode.Single)
            {
                foreach (var aniState in _animationStates)
                {
                    if (aniState._animationData == animationData)
                    {
                        return aniState;
                    }
                }
            }

            if (_animationStates.Count == 0)
            {
                animationConfig.fadeInTime = 0.0f;
            }
            else if (animationConfig.fadeInTime < 0.0f)
            {
                animationConfig.fadeInTime = animationData.fadeInTime;
            }

            if (animationConfig.fadeOutTime < 0.0f)
            {
                animationConfig.fadeOutTime = animationConfig.fadeInTime;
            }

            if (animationConfig.timeScale <= -100.0f)
            {
                animationConfig.timeScale = 1.0f / animationData.scale;
            }

            if (animationData.frameCount > 1)
            {
                if (animationConfig.position < 0.0f)
                {
                    animationConfig.position %= animationData.duration;
                    animationConfig.position = animationData.duration - animationConfig.position;
                }
                else if (animationConfig.position == animationData.duration)
                {
                    animationConfig.position -= 0.000001f; // Play a little time before end.
                }
                else if (animationConfig.position > animationData.duration)
                {
                    animationConfig.position %= animationData.duration;
                }

                if (animationConfig.duration > 0.0f && animationConfig.position + animationConfig.duration > animationData.duration)
                {
                    animationConfig.duration = animationData.duration - animationConfig.position;
                }

                if (animationConfig.playTimes < 0)
                {
                    animationConfig.playTimes = (int) animationData.playTimes;
                }
            }
            else
            {
                animationConfig.playTimes = 1;
                animationConfig.position = 0.0f;
                if (animationConfig.duration > 0.0)
                {
                    animationConfig.duration = 0.0f;
                }
            }

            if (animationConfig.duration == 0.0f)
            {
                animationConfig.duration = -1.0f;
            }

            _FadeOut(animationConfig);

            var animationState = BaseObject.BorrowObject<AnimationState>();
            animationState.Init(_armature, animationData, animationConfig);
            _animationDirty = true;
            _armature._cacheFrameIndex = -1;

            if (_animationStates.Count > 0)
            {
                var added = false;
                for (int i = 0, l = _animationStates.Count; i < l; ++i)
                {
                    if (animationState.layer > _animationStates[i].layer)
                    {
                        added = true;
                        _animationStates.Insert(i, animationState);
                        break;
                    }
                    else if (i != l - 1 && animationState.layer > _animationStates[i + 1].layer)
                    {
                        added = true;
                        _animationStates.Insert(i + 1, animationState);
                        break;
                    }
                }

                if (!added)
                {
                    _animationStates.Add(animationState);
                }
            }
            else
            {
                _animationStates.Add(animationState);
            }

            // Child armature play same name animation.
            foreach (var slot in _armature.GetSlots())
            {
                var childArmature = slot.childArmature;
                if (childArmature != null &&
                    childArmature.InheritAnimation &&
                    childArmature.Animation.HasAnimation(animationName) &&
                    childArmature.Animation.GetState(animationName) == null)
                {
                    childArmature.Animation.FadeIn(animationName); //
                }
            }

            if (!_lockUpdate)
            {
                if (animationConfig.fadeInTime <= 0.0f)
                {
                    // Blend animation state, update armature.
                    _armature.AdvanceTime(0.0f);
                }
            }

            _lastAnimationState = animationState;

            return animationState;
        }

        /// <summary>
        /// - Play a specific animation.
        /// </summary>
        /// <param name="animationName">- The name of animation data. (If not set, The default animation will be played, or resume the animation playing from pause status, or replay the last playing animation)</param>
        /// <param name="playTimes">- Playing repeat times. [-1: Use default value of the animation data, 0: No end loop playing, [1~N]: Repeat N times] (default: -1)</param>
        /// <returns>The playing animation state.</returns>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     armature.animation.play("walk");
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 播放指定动画。
        /// </summary>
        /// <param name="animationName">- 动画数据名称。 （如果未设置，则播放默认动画，或将暂停状态切换为播放状态，或重新播放之前播放的动画）</param>
        /// <param name="playTimes">- 循环播放次数。 [-1: 使用动画数据默认值, 0: 无限循环播放, [1~N]: 循环播放 N 次] （默认: -1）</param>
        /// <returns>播放的动画状态。</returns>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     armature.animation.play("walk");
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public AnimationState Play(string animationName = null, int playTimes = -1)
        {
            _animationConfig.Clear();
            _animationConfig.resetToPose = true;
            _animationConfig.playTimes = playTimes;
            _animationConfig.fadeInTime = 0.0f;
            _animationConfig.animation = animationName ?? "";

            if (animationName != null && animationName.Length > 0)
            {
                PlayConfig(_animationConfig);
            }
            else if (_lastAnimationState == null)
            {
                var defaultAnimation = _armature.ArmatureData.defaultAnimation;
                if (defaultAnimation != null)
                {
                    _animationConfig.animation = defaultAnimation.name;
                    PlayConfig(_animationConfig);
                }
            }
            else if (!_lastAnimationState.isPlaying && !_lastAnimationState.isCompleted)
            {
                _lastAnimationState.Play();
            }
            else
            {
                _animationConfig.animation = _lastAnimationState.name;
                PlayConfig(_animationConfig);
            }

            return _lastAnimationState;
        }

        /// <summary>
        /// - Fade in a specific animation.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <param name="fadeInTime">- The fade in time. [-1: Use the default value of animation data, [0~N]: The fade in time (In seconds)] (Default: -1)</param>
        /// <param name="playTimes">- playing repeat times. [-1: Use the default value of animation data, 0: No end loop playing, [1~N]: Repeat N times] (Default: -1)</param>
        /// <param name="layer">- The blending layer, the animation states in high level layer will get the blending weights with high priority, when the total blending weights are more than 1.0, there will be no more weights can be allocated to the other animation states. (Default: 0)</param>
        /// <param name="group">- The blending group name, it is typically used to specify the substitution of multiple animation states blending. (Default: null)</param>
        /// <param name="fadeOutMode">- The fade out mode, which is typically used to specify alternate mode of multiple animation states blending. (Default: AnimationFadeOutMode.SameLayerAndGroup)</param>
        /// <returns>The playing animation state.</returns>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     armature.animation.fadeIn("walk", 0.3, 0, 0, "normalGroup").resetToPose = false;
        ///     armature.animation.fadeIn("attack", 0.3, 1, 0, "attackGroup").resetToPose = false;
        /// </pre>
        /// </example>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 淡入播放指定的动画。
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <param name="fadeInTime">- 淡入时间。 [-1: 使用动画数据默认值, [0~N]: 淡入时间 (以秒为单位)] （默认: -1）</param>
        /// <param name="playTimes">- 播放次数。 [-1: 使用动画数据默认值, 0: 无限循环播放, [1~N]: 循环播放 N 次] （默认: -1）</param>
        /// <param name="layer">- 混合图层，图层高的动画状态会优先获取混合权重，当混合权重分配总和超过 1.0 时，剩余的动画状态将不能再获得权重分配。 （默认: 0）</param>
        /// <param name="group">- 混合组名称，该属性通常用来指定多个动画状态混合时的相互替换关系。 （默认: null）</param>
        /// <param name="fadeOutMode">- 淡出模式，该属性通常用来指定多个动画状态混合时的相互替换模式。 （默认: AnimationFadeOutMode.SameLayerAndGroup）</param>
        /// <returns>播放的动画状态。</returns>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     armature.animation.fadeIn("walk", 0.3, 0, 0, "normalGroup").resetToPose = false;
        ///     armature.animation.fadeIn("attack", 0.3, 1, 0, "attackGroup").resetToPose = false;
        /// </pre>
        /// </example>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationState FadeIn(string animationName, float fadeInTime = -1.0f, int playTimes = -1,
                                    int layer = 0, string group = null,
                                    AnimationFadeOutMode fadeOutMode = AnimationFadeOutMode.SameLayerAndGroup)
        {
            _animationConfig.Clear();
            _animationConfig.fadeOutMode = fadeOutMode;
            _animationConfig.playTimes = playTimes;
            _animationConfig.layer = layer;
            _animationConfig.fadeInTime = fadeInTime;
            _animationConfig.animation = animationName;
            _animationConfig.group = group ?? "";

            return PlayConfig(_animationConfig);
        }

        /// <summary>
        /// - Play a specific animation from the specific time.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <param name="time">- The start time point of playing. (In seconds)</param>
        /// <param name="playTimes">- Playing repeat times. [-1: Use the default value of animation data, 0: No end loop playing, [1~N]: Repeat N times] (Default: -1)</param>
        /// <returns>The played animation state.</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 从指定时间开始播放指定的动画。
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <param name="time">- 播放开始的时间。 (以秒为单位)</param>
        /// <param name="playTimes">- 循环播放次数。 [-1: 使用动画数据默认值, 0: 无限循环播放, [1~N]: 循环播放 N 次] （默认: -1）</param>
        /// <returns>播放的动画状态。</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationState GotoAndPlayByTime(string animationName, float time = 0.0f, int playTimes = -1)
        {
            _animationConfig.Clear();
            _animationConfig.resetToPose = true;
            _animationConfig.playTimes = playTimes;
            _animationConfig.position = time;
            _animationConfig.fadeInTime = 0.0f;
            _animationConfig.animation = animationName;

            return PlayConfig(_animationConfig);
        }

        /// <summary>
        /// - Play a specific animation from the specific frame.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <param name="frame">- The start frame of playing.</param>
        /// <param name="playTimes">- Playing repeat times. [-1: Use the default value of animation data, 0: No end loop playing, [1~N]: Repeat N times] (Default: -1)</param>
        /// <returns>The played animation state.</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 从指定帧开始播放指定的动画。
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <param name="frame">- 播放开始的帧数。</param>
        /// <param name="playTimes">- 播放次数。 [-1: 使用动画数据默认值, 0: 无限循环播放, [1~N]: 循环播放 N 次] （默认: -1）</param>
        /// <returns>播放的动画状态。</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationState GotoAndPlayByFrame(string animationName, uint frame = 0, int playTimes = -1)
        {
            _animationConfig.Clear();
            _animationConfig.resetToPose = true;
            _animationConfig.playTimes = playTimes;
            _animationConfig.fadeInTime = 0.0f;
            _animationConfig.animation = animationName;

            var animationData = _animations.ContainsKey(animationName) ? _animations[animationName] : null;
            if (animationData != null)
            {
                _animationConfig.position = animationData.duration * frame / animationData.frameCount;
            }

            return PlayConfig(_animationConfig);
        }

        /// <summary>
        /// - Play a specific animation from the specific progress.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <param name="progress">- The start progress value of playing.</param>
        /// <param name="playTimes">- Playing repeat times. [-1: Use the default value of animation data, 0: No end loop playing, [1~N]: Repeat N times] (Default: -1)</param>
        /// <returns>The played animation state.</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 从指定进度开始播放指定的动画。
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <param name="progress">- 开始播放的进度。</param>
        /// <param name="playTimes">- 播放次数。 [-1: 使用动画数据默认值, 0: 无限循环播放, [1~N]: 循环播放 N 次] （默认: -1）</param>
        /// <returns>播放的动画状态。</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationState GotoAndPlayByProgress(string animationName, float progress = 0.0f, int playTimes = -1)
        {
            _animationConfig.Clear();
            _animationConfig.resetToPose = true;
            _animationConfig.playTimes = playTimes;
            _animationConfig.fadeInTime = 0.0f;
            _animationConfig.animation = animationName;

            var animationData = _animations.ContainsKey(animationName) ? _animations[animationName] : null;
            if (animationData != null)
            {
                _animationConfig.position = animationData.duration * (progress > 0.0f ? progress : 0.0f);
            }

            return PlayConfig(_animationConfig);
        }

        /// <summary>
        /// - Stop a specific animation at the specific time.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <param name="time">- The stop time. (In seconds)</param>
        /// <returns>The played animation state.</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 在指定时间停止指定动画播放
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <param name="time">- 停止的时间。 (以秒为单位)</param>
        /// <returns>播放的动画状态。</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationState GotoAndStopByTime(string animationName, float time = 0.0f)
        {
            var animationState = GotoAndPlayByTime(animationName, time, 1);
            if (animationState != null)
            {
                animationState.Stop();
            }

            return animationState;
        }

        /// <summary>
        /// - Stop a specific animation at the specific frame.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <param name="frame">- The stop frame.</param>
        /// <returns>The played animation state.</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 在指定帧停止指定动画的播放
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <param name="frame">- 停止的帧数。</param>
        /// <returns>播放的动画状态。</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationState GotoAndStopByFrame(string animationName, uint frame = 0)
        {
            var animationState = GotoAndPlayByFrame(animationName, frame, 1);
            if (animationState != null)
            {
                animationState.Stop();
            }

            return animationState;
        }

        /// <summary>
        /// - Stop a specific animation at the specific progress.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <param name="progress">- The stop progress value.</param>
        /// <returns>The played animation state.</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 在指定的进度停止指定的动画播放。
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <param name="progress">- 停止进度。</param>
        /// <returns>播放的动画状态。</returns>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public AnimationState GotoAndStopByProgress(string animationName, float progress = 0.0f)
        {
            var animationState = GotoAndPlayByProgress(animationName, progress, 1);
            if (animationState != null)
            {
                animationState.Stop();
            }

            return animationState;
        }

        /// <summary>
        /// - Get a specific animation state.
        /// </summary>
        /// <param name="animationName">- The name of animation state.</param>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     armature.animation.play("walk");
        ///     let walkState = armature.animation.getState("walk");
        ///     walkState.timeScale = 0.5;
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取指定的动画状态
        /// </summary>
        /// <param name="animationName">- 动画状态名称。</param>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     armature.animation.play("walk");
        ///     let walkState = armature.animation.getState("walk");
        ///     walkState.timeScale = 0.5;
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public AnimationState GetState(string animationName)
        {
            var i = _animationStates.Count;
            while (i-- > 0)
            {
                var animationState = _animationStates[i];
                if (animationState.name == animationName)
                {
                    return animationState;
                }
            }

            return null;
        }

        /// <summary>
        /// - Check whether a specific animation data is included.
        /// </summary>
        /// <param name="animationName">- The name of animation data.</param>
        /// <see cref="DragonBones.AnimationData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查是否包含指定的动画数据
        /// </summary>
        /// <param name="animationName">- 动画数据名称。</param>
        /// <see cref="DragonBones.AnimationData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public bool HasAnimation(string animationName)
        {
            return _animations.ContainsKey(animationName);
        }

        /// <summary>
        /// - Get all the animation states.
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取所有的动画状态
        /// </summary>
        /// <version>DragonBones 5.1</version>
        /// <language>zh_CN</language>
        public List<AnimationState> GetStates()
        {
            return _animationStates;
        }

        /// <summary>
        /// - Check whether there is an animation state is playing
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查是否有动画状态正在播放
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public bool IsPlaying
        {
            get
            {
                foreach (var animationState in _animationStates)
                {
                    if (animationState.isPlaying)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// - Check whether all the animation states' playing were finished.
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查是否所有的动画状态均已播放完毕。
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public bool IsCompleted
        {
            get
            {
                foreach (var animationState in _animationStates)
                {
                    if (!animationState.isCompleted)
                    {
                        return false;
                    }
                }

                return _animationStates.Count > 0;
            }
        }

        /// <summary>
        /// - The name of the last playing animation state.
        /// </summary>
        /// <see cref="LastAnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 上一个播放的动画状态名称
        /// </summary>
        /// <see cref="LastAnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public string LastAnimationName => _lastAnimationState != null ? _lastAnimationState.name : "";

        /// <summary>
        /// - The name of all animation data
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 所有动画数据的名称
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public List<string> AnimationNames => _animationNames;

        /// <summary>
        /// - All animation data.
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 所有的动画数据。
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public Dictionary<string, AnimationData> Animations
        {
            get => _animations;
            set
            {
                if (_animations == value)
                {
                    return;
                }

                _animationNames.Clear();

                _animations.Clear();

                foreach (var k in value)
                {
                    _animationNames.Add(k.Key);
                    _animations[k.Key] = value[k.Key];
                }
            }
        }

        /// <summary>
        /// - An AnimationConfig instance that can be used quickly.
        /// </summary>
        /// <see cref="DragonBones.AnimationConfig"/>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 一个可以快速使用的动画配置实例。
        /// </summary>
        /// <see cref="DragonBones.AnimationConfig"/>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public AnimationConfig AnimationConfig
        {
            get
            {
                _animationConfig.Clear();
                return _animationConfig;
            }
        }

        /// <summary>
        /// - The last playing animation state
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 上一个播放的动画状态
        /// </summary>
        /// <see cref="DragonBones.AnimationState"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public AnimationState LastAnimationState => _lastAnimationState;
    }
}
