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
    /// - The slot attached to the armature, controls the display status and properties of the display object.
    /// A bone can contain multiple slots.
    /// A slot can contain multiple display objects, displaying only one of the display objects at a time,
    /// but you can toggle the display object into frame animation while the animation is playing.
    /// The display object can be a normal texture, or it can be a display of a child armature, a grid display object,
    /// and a custom other display object.
    /// </summary>
    /// <see cref="DragonBones.Armature"/>
    /// <see cref="DragonBones.Bone"/>
    /// <see cref="DragonBones.SlotData"/>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 插槽附着在骨骼上，控制显示对象的显示状态和属性。
    /// 一个骨骼上可以包含多个插槽。
    /// 一个插槽中可以包含多个显示对象，同一时间只能显示其中的一个显示对象，但可以在动画播放的过程中切换显示对象实现帧动画。
    /// 显示对象可以是普通的图片纹理，也可以是子骨架的显示容器，网格显示对象，还可以是自定义的其他显示对象。
    /// </summary>
    /// <see cref="DragonBones.Armature"/>
    /// <see cref="DragonBones.Bone"/>
    /// <see cref="DragonBones.SlotData"/>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    internal abstract class Slot : TransformObject
    {
        /// <summary>
        /// - Displays the animated state or mixed group name controlled by the object, set to null to be controlled by all animation states.
        /// </summary>
        /// <default>null</default>
        /// <see cref="DragonBones.AnimationState.displayControl"/>
        /// <see cref="DragonBones.AnimationState.name"/>
        /// <see cref="DragonBones.AnimationState.group"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 显示对象受到控制的动画状态或混合组名称，设置为 null 则表示受所有的动画状态控制。
        /// </summary>
        /// <default>null</default>
        /// <see cref="DragonBones.AnimationState.displayControl"/>
        /// <see cref="DragonBones.AnimationState.name"/>
        /// <see cref="DragonBones.AnimationState.group"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public string displayController;
        /// <private/>
        protected bool _displayDirty;
        /// <private/>
        protected bool _zOrderDirty;
        /// <private/>
        protected bool _visibleDirty;
        /// <private/>
        protected bool _blendModeDirty;
        /// <internal/>
        /// <private/>
        internal bool _colorDirty;
        /// <private/>
        internal bool _transformDirty;
        /// <private/>
        protected bool _visible;
        /// <private/>
        internal BlendMode _blendMode;
        /// <private/>
        protected int _displayIndex;
        /// <private/>
        protected int _animationDisplayIndex;
        /// <internal/>
        /// <private/>
        internal int _zOrder;
        /// <private/>
        protected int _cachedFrameIndex;
        /// <internal/>
        /// <private/>
        internal float _pivotX;
        /// <internal/>
        /// <private/>
        internal float _pivotY;
        /// <private/>
        protected readonly Matrix _localMatrix = new Matrix();
        /// <internal/>
        /// <private/>
        internal readonly ColorTransform _colorTransform = new ColorTransform();
        /// <private/>
        internal readonly List<DisplayData> _displayDatas = new List<DisplayData>();
        /// <private/>
        protected readonly List<object> _displayList = new List<object>();
        /// <internal/>
        /// <private/>
        internal SlotData _slotData;
        /// <private/>
        protected List<DisplayData> _rawDisplayDatas;
        /// <internal/>
        /// <private/>
        protected DisplayData _displayData;
        /// <private/>
        protected BoundingBoxData _boundingBoxData;
        /// <private/>
        protected TextureData _textureData;
        /// <internal/>
        public DeformVertices _deformVertices;
        /// <private/>
        protected object _rawDisplay;
        /// <private/>
        protected object _meshDisplay;
        /// <private/>
        protected object _display;
        /// <private/>
        protected Armature _childArmature;
        /// <private/>
        protected Bone _parent;
        /// <internal/>
        /// <private/>
        internal List<int> _cachedFrameIndices = new List<int>();

        public Slot()
        {
        }

        /// <inheritDoc/>
        protected override void _OnClear()
        {
            base._OnClear();

            var disposeDisplayList = new List<object>();
            for (int i = 0, l = _displayList.Count; i < l; ++i)
            {
                var eachDisplay = _displayList[i];
                if (eachDisplay != _rawDisplay && eachDisplay != _meshDisplay && !disposeDisplayList.Contains(eachDisplay))
                {
                    disposeDisplayList.Add(eachDisplay);
                }
            }

            for (int i = 0, l = disposeDisplayList.Count; i < l; ++i)
            {
                var eachDisplay = disposeDisplayList[i];
                if (eachDisplay is Armature)
                {
                    (eachDisplay as Armature).Dispose();
                }
                else
                {
                    _DisposeDisplay(eachDisplay, true);
                }
            }

            if (_deformVertices != null)
            {
                _deformVertices.ReturnToPool();
            }

            if (_meshDisplay != null && _meshDisplay != _rawDisplay)
            {
                // May be _meshDisplay and _rawDisplay is the same one.
                _DisposeDisplay(_meshDisplay, false);
            }

            if (_rawDisplay != null)
            {
                _DisposeDisplay(_rawDisplay, false);
            }

            displayController = null;

            _displayDirty = false;
            _zOrderDirty = false;
            _blendModeDirty = false;
            _colorDirty = false;
            _transformDirty = false;
            _visible = true;
            _blendMode = BlendMode.Normal;
            _displayIndex = -1;
            _animationDisplayIndex = -1;
            _zOrder = 0;
            _cachedFrameIndex = -1;
            _pivotX = 0.0f;
            _pivotY = 0.0f;
            _localMatrix.Identity();
            _colorTransform.Identity();
            _displayList.Clear();
            _displayDatas.Clear();
            _slotData = null; //
            _rawDisplayDatas = null; //
            _displayData = null;
            _boundingBoxData = null;
            _textureData = null;
            _deformVertices = null;
            _rawDisplay = null;
            _meshDisplay = null;
            _display = null;
            _childArmature = null;
            _parent = null;
            _cachedFrameIndices = null;
        }

        /// <private/>
        protected abstract void _InitDisplay(object value, bool isRetain);
        /// <private/>
        protected abstract void _DisposeDisplay(object value, bool isRelease);
        /// <private/>
        protected abstract void _OnUpdateDisplay();
        /// <private/>
        protected abstract void _AddDisplay();
        /// <private/>
        protected abstract void _ReplaceDisplay(object value);
        /// <private/>
        protected abstract void _RemoveDisplay();
        /// <private/>
        protected abstract void _UpdateZOrder();
        /// <private/>
        internal abstract void _UpdateVisible();
        /// <private/>
        internal abstract void _UpdateBlendMode();
        /// <private/>
        protected abstract void _UpdateColor();
        /// <private/>
        protected abstract void _UpdateFrame();
        /// <private/>
        protected abstract void _UpdateMesh();
        /// <private/>
        protected abstract void _UpdateTransform();
        /// <private/>
        protected abstract void _IdentityTransform();

        /// <summary>
        /// - Support default skin data.
        /// </summary>
        /// <private/>
        protected DisplayData _GetDefaultRawDisplayData(int displayIndex)
        {
            var defaultSkin = _armature._armatureData.defaultSkin;
            if (defaultSkin != null)
            {
                var defaultRawDisplayDatas = defaultSkin.GetDisplays(_slotData.name);
                if (defaultRawDisplayDatas != null)
                {
                    return displayIndex < defaultRawDisplayDatas.Count ? defaultRawDisplayDatas[displayIndex] : null;
                }
            }

            return null;
        }

        /// <private/>
        protected void _UpdateDisplayData()
        {
            var prevDisplayData = _displayData;
            var prevVerticesData = _deformVertices != null ? _deformVertices.verticesData : null;
            var prevTextureData = _textureData;

            DisplayData rawDisplayData = null;
            VerticesData currentVerticesData = null;

            _displayData = null;
            _boundingBoxData = null;
            _textureData = null;

            if (_displayIndex >= 0)
            {
                if (_rawDisplayDatas != null)
                {
                    rawDisplayData = _displayIndex < _rawDisplayDatas.Count ? _rawDisplayDatas[_displayIndex] : null;
                }

                if (rawDisplayData == null)
                {
                    rawDisplayData = _GetDefaultRawDisplayData(_displayIndex);
                }

                if (_displayIndex < _displayDatas.Count)
                {
                    _displayData = _displayDatas[_displayIndex];
                }
            }

            // Update texture and mesh data.
            if (_displayData != null)
            {
                if (_displayData.type == DisplayType.Mesh)
                {
                    currentVerticesData = (_displayData as MeshDisplayData).vertices;
                }
                else if (_displayData.type == DisplayType.Path)
                {
                    currentVerticesData = (_displayData as PathDisplayData).vertices;
                }
                else if (rawDisplayData != null)
                {
                    if (rawDisplayData.type == DisplayType.Mesh)
                    {
                        currentVerticesData = (rawDisplayData as MeshDisplayData).vertices;
                    }
                    else if (rawDisplayData.type == DisplayType.Path)
                    {
                        currentVerticesData = (rawDisplayData as PathDisplayData).vertices;
                    }
                }

                if (_displayData.type == DisplayType.BoundingBox)
                {
                    _boundingBoxData = (_displayData as BoundingBoxDisplayData).boundingBox;
                }
                else if (rawDisplayData != null)
                {
                    if (rawDisplayData.type == DisplayType.BoundingBox)
                    {
                        _boundingBoxData = (rawDisplayData as BoundingBoxDisplayData).boundingBox;
                    }
                }

                if (_displayData.type == DisplayType.Image)
                {
                    _textureData = (_displayData as ImageDisplayData).texture;
                }
                else if (_displayData.type == DisplayType.Mesh)
                {
                    _textureData = (_displayData as MeshDisplayData).texture;
                }
            }

            if (_displayData != prevDisplayData || currentVerticesData != prevVerticesData || _textureData != prevTextureData)
            {
                // Update pivot offset.
                if (currentVerticesData == null && _textureData != null)
                {
                    var imageDisplayData = _displayData as ImageDisplayData;
                    var scale = _textureData.Parent.Scale * _armature._armatureData.scale;
                    var frame = _textureData.Frame;

                    _pivotX = imageDisplayData.pivot.X;
                    _pivotY = imageDisplayData.pivot.Y;

                    var rect = frame != null ? frame : _textureData.Region;
                    var width = rect.Width;
                    var height = rect.Height;

                    if (_textureData.Rotated && frame == null)
                    {
                        width = rect.Height;
                        height = rect.Width;
                    }

                    _pivotX *= width * scale;
                    _pivotY *= height * scale;

                    if (frame != null)
                    {
                        _pivotX += frame.x * scale;
                        _pivotY += frame.y * scale;
                    }

                    // Update replace pivot. TODO
                    if (_displayData != null && rawDisplayData != null && _displayData != rawDisplayData)
                    {
                        rawDisplayData.transform.ToMatrix(Slot._helpMatrix);
                        Slot._helpMatrix.Invert();
                        Slot._helpMatrix.TransformPoint(0.0f, 0.0f, Slot._helpPoint);
                        _pivotX -= Slot._helpPoint.X;
                        _pivotY -= Slot._helpPoint.Y;

                        _displayData.transform.ToMatrix(Slot._helpMatrix);
                        Slot._helpMatrix.Invert();
                        Slot._helpMatrix.TransformPoint(0.0f, 0.0f, Slot._helpPoint);
                        _pivotX += Slot._helpPoint.X;
                        _pivotY += Slot._helpPoint.Y;
                    }

                    //if (!DragonBones.YDown)
                    //{
                    //    _pivotY = (_textureData.Rotated ? _textureData.Region.Width : _textureData.Region.Height) * scale - _pivotY;
                    //}
                }
                else
                {
                    _pivotX = 0.0f;
                    _pivotY = 0.0f;
                }

                // Update original transform.
                if (rawDisplayData != null)
                {
                    // Compatible.
                    origin = rawDisplayData.transform;
                }
                else if (_displayData != null)
                {
                    // Compatible.
                    origin = _displayData.transform;
                }
                else
                {
                    origin = null;
                }

                // Update vertices.
                if (currentVerticesData != prevVerticesData)
                {
                    if (_deformVertices == null)
                    {
                        _deformVertices = BaseObject.BorrowObject<DeformVertices>();
                    }

                    _deformVertices.init(currentVerticesData, _armature);
                }
                else if (_deformVertices != null && _textureData != prevTextureData)
                {
                    // Update mesh after update frame.
                    _deformVertices.verticesDirty = true;
                }

                _displayDirty = true;
                _transformDirty = true;
            }
        }

        /// <private/>
        protected void _UpdateDisplay()
        {
            var prevDisplay = _display != null ? _display : _rawDisplay;
            var prevChildArmature = _childArmature;

            // Update display and child armature.
            if (_displayIndex >= 0 && _displayIndex < _displayList.Count)
            {
                _display = _displayList[_displayIndex];
                if (_display != null && _display is Armature)
                {
                    _childArmature = _display as Armature;
                    _display = _childArmature.Display;
                }
                else
                {
                    _childArmature = null;
                }
            }
            else
            {
                _display = null;
                _childArmature = null;
            }

            // Update display.
            var currentDisplay = _display != null ? _display : _rawDisplay;
            if (currentDisplay != prevDisplay)
            {
                _OnUpdateDisplay();
                _ReplaceDisplay(prevDisplay);

                _transformDirty = true;
                _visibleDirty = true;
                _blendModeDirty = true;
                _colorDirty = true;
            }

            // Update frame.
            if (currentDisplay == _rawDisplay || currentDisplay == _meshDisplay)
            {
                _UpdateFrame();
            }

            // Update child armature.
            if (_childArmature != prevChildArmature)
            {
                if (prevChildArmature != null)
                {
                    // Update child armature parent.
                    prevChildArmature._parent = null;
                    prevChildArmature.clock = null;
                    if (prevChildArmature.InheritAnimation)
                    {
                        prevChildArmature.Animation.Reset();
                    }
                }

                if (_childArmature != null)
                {
                    // Update child armature parent.
                    _childArmature._parent = this;
                    _childArmature.clock = _armature.clock;
                    if (_childArmature.InheritAnimation)
                    {
                        // Set child armature cache frameRate.
                        if (_childArmature.CacheFrameRate == 0)
                        {
                            var cacheFrameRate = _armature.CacheFrameRate;
                            if (cacheFrameRate != 0)
                            {
                                _childArmature.CacheFrameRate = cacheFrameRate;
                            }
                        }

                        // Child armature action.
                        List<ActionData> actions = null;
                        if (_displayData != null && _displayData.type == DisplayType.Armature)
                        {
                            actions = (_displayData as ArmatureDisplayData).actions;
                        }
                        else if (_displayIndex >= 0 && _rawDisplayDatas != null)
                        {
                            var rawDisplayData = _displayIndex < _rawDisplayDatas.Count ? _rawDisplayDatas[_displayIndex] : null;

                            if (rawDisplayData == null)
                            {
                                rawDisplayData = _GetDefaultRawDisplayData(_displayIndex);
                            }

                            if (rawDisplayData != null && rawDisplayData.type == DisplayType.Armature)
                            {
                                actions = (rawDisplayData as ArmatureDisplayData).actions;
                            }
                        }

                        if (actions != null && actions.Count > 0)
                        {
                            foreach (var action in actions)
                            {
                                var eventObject = BaseObject.BorrowObject<EventObject>();
                                EventObject.ActionDataToInstance(action, eventObject, _armature);
                                eventObject.slot = this;
                                _armature._BufferAction(eventObject, false);
                            }
                        }
                        else
                        {
                            _childArmature.Animation.Play();
                        }
                    }
                }
            }
        }

        /// <private/>
        protected void _UpdateGlobalTransformMatrix(bool isCache)
        {
            GlobalTransformMatrix.CopyFrom(_localMatrix);
            GlobalTransformMatrix.Concat(_parent.GlobalTransformMatrix);
            if (isCache)
            {
                Global.FromMatrix(GlobalTransformMatrix);
            }
            else
            {
                _globalDirty = true;
            }
        }
        /// <internal/>
        /// <private/>
        internal bool _SetDisplayIndex(int value, bool isAnimation = false)
        {
            if (isAnimation)
            {
                if (_animationDisplayIndex == value)
                {
                    return false;
                }

                _animationDisplayIndex = value;
            }

            if (_displayIndex == value)
            {
                return false;
            }

            _displayIndex = value;
            _displayDirty = true;

            _UpdateDisplayData();

            return _displayDirty;
        }

        /// <internal/>
        /// <private/>
        internal bool _SetZorder(int value)
        {
            if (_zOrder == value)
            {
                //return false;
            }

            _zOrder = value;
            _zOrderDirty = true;

            return _zOrderDirty;
        }

        /// <internal/>
        /// <private/>
        internal bool _SetColor(ColorTransform value)
        {
            _colorTransform.CopyFrom(value);
            _colorDirty = true;

            return _colorDirty;
        }
        /// <internal/>
        /// <private/>
        internal bool _SetDisplayList(List<object> value)
        {
            if (value != null && value.Count > 0)
            {
                if (_displayList.Count != value.Count)
                {
                    _displayList.ResizeList(value.Count);
                }

                for (int i = 0, l = value.Count; i < l; ++i)
                {
                    // Retain input render displays.
                    var eachDisplay = value[i];
                    if (eachDisplay != null &&
                        eachDisplay != _rawDisplay &&
                        eachDisplay != _meshDisplay &&
                        !(eachDisplay is Armature) && _displayList.IndexOf(eachDisplay) < 0)
                    {
                        _InitDisplay(eachDisplay, true);
                    }

                    _displayList[i] = eachDisplay;
                }
            }
            else if (_displayList.Count > 0)
            {
                _displayList.Clear();
            }

            if (_displayIndex >= 0 && _displayIndex < _displayList.Count)
            {
                _displayDirty = _display != _displayList[_displayIndex];
            }
            else
            {
                _displayDirty = _display != null;
            }

            _UpdateDisplayData();

            return _displayDirty;
        }

        /// <internal/>
        /// <private/>
        internal virtual void Init(SlotData slotData, Armature armatureValue, object rawDisplay, object meshDisplay)
        {
            if (_slotData != null)
            {
                return;
            }

            _slotData = slotData;
            //
            _visibleDirty = true;
            _blendModeDirty = true;
            _colorDirty = true;
            _blendMode = _slotData.blendMode;
            _zOrder = _slotData.zOrder;
            _colorTransform.CopyFrom(_slotData.color);
            _rawDisplay = rawDisplay;
            _meshDisplay = meshDisplay;

            _armature = armatureValue;

            var slotParent = _armature.GetBone(_slotData.parent.name);
            if (slotParent != null)
            {
                _parent = slotParent;
            }
            else
            {
                // Never;
            }

            _armature._AddSlot(this);

            //
            _InitDisplay(_rawDisplay, false);
            if (_rawDisplay != _meshDisplay)
            {
                _InitDisplay(_meshDisplay, false);
            }

            _OnUpdateDisplay();
            _AddDisplay();

            //
            // this.rawDisplayDatas = displayDatas; // TODO
        }

        /// <internal/>
        /// <private/>
        internal void Update(int cacheFrameIndex)
        {
            if (_displayDirty)
            {
                _displayDirty = false;
                _UpdateDisplay();

                // TODO remove slot
                if (_transformDirty)
                {
                    // Update local matrix. (Only updated when both display and transform are dirty.)
                    if (origin != null)
                    {
                        Global.CopyFrom(origin).Add(offset).ToMatrix(_localMatrix);
                    }
                    else
                    {
                        Global.CopyFrom(offset).ToMatrix(_localMatrix);
                    }
                }
            }

            if (_zOrderDirty)
            {
                _zOrderDirty = false;
                _UpdateZOrder();
            }

            if (cacheFrameIndex >= 0 && _cachedFrameIndices != null)
            {
                var cachedFrameIndex = _cachedFrameIndices[cacheFrameIndex];

                if (cachedFrameIndex >= 0 && _cachedFrameIndex == cachedFrameIndex)
                {
                    // Same cache.
                    _transformDirty = false;
                }
                else if (cachedFrameIndex >= 0)
                {
                    // Has been Cached.
                    _transformDirty = true;
                    _cachedFrameIndex = cachedFrameIndex;
                }
                else if (_transformDirty || _parent._childrenTransformDirty)
                {
                    // Dirty.
                    _transformDirty = true;
                    _cachedFrameIndex = -1;
                }
                else if (_cachedFrameIndex >= 0)
                {
                    // Same cache, but not set index yet.
                    _transformDirty = false;
                    _cachedFrameIndices[cacheFrameIndex] = _cachedFrameIndex;
                }
                else
                {
                    // Dirty.
                    _transformDirty = true;
                    _cachedFrameIndex = -1;
                }
            }
            else if (_transformDirty || _parent._childrenTransformDirty)
            {
                // Dirty.
                cacheFrameIndex = -1;
                _transformDirty = true;
                _cachedFrameIndex = -1;
            }

            if (_display == null)
            {
                return;
            }

            if (_visibleDirty)
            {
                _visibleDirty = false;
                _UpdateVisible();
            }

            if (_blendModeDirty)
            {
                _blendModeDirty = false;
                _UpdateBlendMode();
            }

            if (_colorDirty)
            {
                _colorDirty = false;
                _UpdateColor();
            }

            if (_deformVertices != null && _deformVertices.verticesData != null && _display == _meshDisplay)
            {
                var isSkinned = _deformVertices.verticesData.weight != null;

                if (_deformVertices.verticesDirty ||
                    (isSkinned && _deformVertices.isBonesUpdate()))
                {
                    _deformVertices.verticesDirty = false;
                    _UpdateMesh();
                }

                if (isSkinned)
                {
                    // Compatible.
                    return;
                }
            }

            if (_transformDirty)
            {
                _transformDirty = false;

                if (_cachedFrameIndex < 0)
                {
                    var isCache = cacheFrameIndex >= 0;
                    _UpdateGlobalTransformMatrix(isCache);

                    if (isCache && _cachedFrameIndices != null)
                    {
                        _cachedFrameIndex = _cachedFrameIndices[cacheFrameIndex] = _armature._armatureData.SetCacheFrame(GlobalTransformMatrix, Global);
                    }
                }
                else
                {
                    _armature._armatureData.GetCacheFrame(GlobalTransformMatrix, Global, _cachedFrameIndex);
                }

                _UpdateTransform();
            }
        }

        /// <private/>
        public void UpdateTransformAndMatrix()
        {
            if (_transformDirty)
            {
                _transformDirty = false;
                _UpdateGlobalTransformMatrix(false);
            }
        }

        /// <private/>
        internal void ReplaceDisplayData(DisplayData value, int displayIndex = -1)
        {
            if (displayIndex < 0)
            {
                if (_displayIndex < 0)
                {
                    displayIndex = 0;
                }
                else
                {
                    displayIndex = _displayIndex;
                }
            }

            if (_displayDatas.Count <= displayIndex)
            {
                _displayDatas.ResizeList(displayIndex + 1);

                for (int i = 0, l = _displayDatas.Count; i < l; ++i)
                {
                    // Clean undefined.
                    _displayDatas[i] = null;
                }
            }

            _displayDatas[displayIndex] = value;
        }

        /// <summary>
        /// - Check whether a specific point is inside a custom bounding box in the slot.
        /// The coordinate system of the point is the inner coordinate system of the armature.
        /// Custom bounding boxes need to be customized in Dragonbones Pro.
        /// </summary>
        /// <param name="x">- The horizontal coordinate of the point.</param>
        /// <param name="y">- The vertical coordinate of the point.</param>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查特定点是否在插槽的自定义边界框内。
        /// 点的坐标系为骨架内坐标系。
        /// 自定义边界框需要在 DragonBones Pro 中自定义。
        /// </summary>
        /// <param name="x">- 点的水平坐标。</param>
        /// <param name="y">- 点的垂直坐标。</param>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public bool ContainsPoint(float x, float y)
        {
            if (_boundingBoxData == null)
            {
                return false;
            }

            UpdateTransformAndMatrix();

            Slot._helpMatrix.CopyFrom(GlobalTransformMatrix);
            Slot._helpMatrix.Invert();
            Slot._helpMatrix.TransformPoint(x, y, Slot._helpPoint);

            return _boundingBoxData.ContainsPoint(Slot._helpPoint.X, Slot._helpPoint.Y);
        }

        /// <summary>
        /// - Check whether a specific segment intersects a custom bounding box for the slot.
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
        /// <returns>Intersection situation. [1: Disjoint and segments within the bounding box, 0: Disjoint, 1: Intersecting and having a nodal point and ending in the bounding box, 2: Intersecting and having a nodal point and starting at the bounding box, 3: Intersecting and having two intersections, N: Intersecting and having N intersections]</returns>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 检查特定线段是否与插槽的自定义边界框相交。
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
        /// <returns>相交的情况。 [-1: 不相交且线段在包围盒内, 0: 不相交, 1: 相交且有一个交点且终点在包围盒内, 2: 相交且有一个交点且起点在包围盒内, 3: 相交且有两个交点, N: 相交且有 N 个交点]</returns>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public int IntersectsSegment(float xA, float yA, float xB, float yB,
                                    Point intersectionPointA = null,
                                    Point intersectionPointB = null,
                                    Point normalRadians = null)
        {
            if (_boundingBoxData == null)
            {
                return 0;
            }

            UpdateTransformAndMatrix();
            Slot._helpMatrix.CopyFrom(GlobalTransformMatrix);
            Slot._helpMatrix.Invert();
            Slot._helpMatrix.TransformPoint(xA, yA, Slot._helpPoint);
            xA = Slot._helpPoint.X;
            yA = Slot._helpPoint.Y;
            Slot._helpMatrix.TransformPoint(xB, yB, Slot._helpPoint);
            xB = Slot._helpPoint.X;
            yB = Slot._helpPoint.Y;

            var intersectionCount = _boundingBoxData.IntersectsSegment(xA, yA, xB, yB, intersectionPointA, intersectionPointB, normalRadians);
            if (intersectionCount > 0)
            {
                if (intersectionCount == 1 || intersectionCount == 2)
                {
                    if (intersectionPointA != null)
                    {
                        GlobalTransformMatrix.TransformPoint(intersectionPointA.X, intersectionPointA.Y, intersectionPointA);

                        if (intersectionPointB != null)
                        {
                            intersectionPointB.X = intersectionPointA.X;
                            intersectionPointB.Y = intersectionPointA.Y;
                        }
                    }
                    else if (intersectionPointB != null)
                    {
                        GlobalTransformMatrix.TransformPoint(intersectionPointB.X, intersectionPointB.Y, intersectionPointB);
                    }
                }
                else
                {
                    if (intersectionPointA != null)
                    {
                        GlobalTransformMatrix.TransformPoint(intersectionPointA.X, intersectionPointA.Y, intersectionPointA);
                    }

                    if (intersectionPointB != null)
                    {
                        GlobalTransformMatrix.TransformPoint(intersectionPointB.X, intersectionPointB.Y, intersectionPointB);
                    }
                }

                if (normalRadians != null)
                {
                    GlobalTransformMatrix.TransformPoint((float) Math.Cos(normalRadians.X), (float) Math.Sin(normalRadians.X), Slot._helpPoint, true);
                    normalRadians.X = (float) Math.Atan2(Slot._helpPoint.Y, Slot._helpPoint.X);

                    GlobalTransformMatrix.TransformPoint((float) Math.Cos(normalRadians.Y), (float) Math.Sin(normalRadians.Y), Slot._helpPoint, true);
                    normalRadians.Y = (float) Math.Atan2(Slot._helpPoint.Y, Slot._helpPoint.X);
                }
            }

            return intersectionCount;
        }

        /// <summary>
        /// - Forces the slot to update the state of the display object in the next frame.
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 强制插槽在下一帧更新显示对象的状态。
        /// </summary>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public void InvalidUpdate()
        {
            _displayDirty = true;
            _transformDirty = true;
        }

        /// <summary>
        /// - The visible of slot's display object.
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 5.6</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 插槽的显示对象的可见。
        /// </summary>
        /// <default>true</default>
        /// <version>DragonBones 5.6</version>
        /// <language>zh_CN</language>
        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible == value)
                {
                    return;
                }

                _visible = value;
                _UpdateVisible();
            }
        }

        /// <summary>
        /// - The index of the display object displayed in the display list.
        /// </summary>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     let slot = armature.getSlot("weapon");
        ///     slot.displayIndex = 3;
        ///     slot.displayController = "none";
        /// </pre>
        /// </example>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 此时显示的显示对象在显示列表中的索引。
        /// </summary>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     let slot = armature.getSlot("weapon");
        ///     slot.displayIndex = 3;
        ///     slot.displayController = "none";
        /// </pre>
        /// </example>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public int DisplayIndex
        {
            get => _displayIndex;
            set
            {
                if (_SetDisplayIndex(value))
                {
                    Update(-1);
                }
            }
        }

        /// <summary>
        /// - The slot name.
        /// </summary>
        /// <see cref="DragonBones.SlotData.name"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 插槽名称。
        /// </summary>
        /// <see cref="DragonBones.SlotData.name"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public string Name => _slotData.name;

        /// <summary>
        /// - Contains a display list of display objects or child armatures.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 包含显示对象或子骨架的显示列表。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public List<object> DisplayList
        {
            get => new List<object>(_displayList.ToArray());
            set
            {
                var backupDisplayList = _displayList.ToArray(); // Copy.
                var disposeDisplayList = new List<object>();

                if (_SetDisplayList(value))
                {
                    Update(-1);
                }

                // Release replaced displays.
                foreach (var eachDisplay in backupDisplayList)
                {
                    if (eachDisplay != null &&
                        eachDisplay != _rawDisplay &&
                        eachDisplay != _meshDisplay &&
                        _displayList.IndexOf(eachDisplay) < 0 &&
                        disposeDisplayList.IndexOf(eachDisplay) < 0)
                    {
                        disposeDisplayList.Add(eachDisplay);
                    }
                }

                foreach (var eachDisplay in disposeDisplayList)
                {
                    if (eachDisplay is Armature)
                    {
                        // (eachDisplay as Armature).Dispose();
                    }
                    else
                    {
                        _DisposeDisplay(eachDisplay, true);
                    }
                }
            }
        }

        /// <summary>
        /// - The slot data.
        /// </summary>
        /// <see cref="DragonBones.SlotData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 插槽数据。
        /// </summary>
        /// <see cref="DragonBones.SlotData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public SlotData SlotData => _slotData;

        /// <private/>
        public List<DisplayData> RawDisplayDatas
        {
            get => _rawDisplayDatas;
            set
            {
                if (_rawDisplayDatas == value)
                {
                    return;
                }

                _displayDirty = true;
                _rawDisplayDatas = value;

                if (_rawDisplayDatas != null)
                {
                    _displayDatas.ResizeList(_rawDisplayDatas.Count);
                    for (int i = 0, l = _displayDatas.Count; i < l; ++i)
                    {
                        var rawDisplayData = _rawDisplayDatas[i];

                        if (rawDisplayData == null)
                        {
                            rawDisplayData = _GetDefaultRawDisplayData(i);
                        }

                        _displayDatas[i] = rawDisplayData;
                    }
                }
                else
                {
                    _displayDatas.Clear();
                }
            }
        }

        /// <summary>
        /// - The custom bounding box data for the slot at current time.
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 插槽此时的自定义包围盒数据。
        /// </summary>
        /// <version>DragonBones 5.0</version>
        /// <language>zh_CN</language>
        public BoundingBoxData BoundingBoxData => _boundingBoxData;

        /// <private/>
        public object RawDisplay => _rawDisplay;

        /// <private/>
        public object MeshDisplay => _meshDisplay;

        /// <summary>
        /// - The display object that the slot displays at this time.
        /// </summary>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     let slot = armature.getSlot("text");
        ///     slot.display = new yourEngine.TextField();
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 插槽此时显示的显示对象。
        /// </summary>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     let slot = armature.getSlot("text");
        ///     slot.display = new yourEngine.TextField();
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public object display
        {
            get => _display;
            set
            {
                if (_display == value)
                {
                    return;
                }

                var displayListLength = _displayList.Count;
                if (_displayIndex < 0 && displayListLength == 0)
                {
                    // Emprty.
                    _displayIndex = 0;
                }

                if (_displayIndex < 0)
                {
                    return;
                }
                else
                {
                    var replaceDisplayList = DisplayList; // Copy.
                    if (displayListLength <= _displayIndex)
                    {
                        replaceDisplayList.ResizeList(_displayIndex + 1);
                    }

                    replaceDisplayList[_displayIndex] = value;
                    DisplayList = replaceDisplayList;
                }
            }
        }

        /// <summary>
        /// - The child armature that the slot displayed at current time.
        /// </summary>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     let slot = armature.getSlot("weapon");
        /// let prevChildArmature = slot.childArmature;
        /// if (prevChildArmature) {
        /// prevChildArmature.dispose();
        ///     }
        ///     slot.childArmature = factory.buildArmature("weapon_blabla", "weapon_blabla_project");
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 插槽此时显示的子骨架。
        /// 注意，被替换的对象并不会被回收，根据语言和引擎的不同，需要额外处理。
        /// </summary>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     let slot = armature.getSlot("weapon");
        /// let prevChildArmature = slot.childArmature;
        /// if (prevChildArmature) {
        /// prevChildArmature.dispose();
        ///     }
        ///     slot.childArmature = factory.buildArmature("weapon_blabla", "weapon_blabla_project");
        /// </pre>
        /// </example>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public Armature childArmature
        {
            get => _childArmature;

            set
            {
                if (_childArmature == value)
                {
                    return;
                }

                display = value;
            }
        }

        /// <summary>
        /// - The parent bone to which it belongs.
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 所属的父骨骼。
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public Bone parent => _parent;
    }
}
