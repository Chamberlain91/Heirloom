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
    /// <internal/>
    /// <private/>
    public class BuildArmaturePackage
    {
        public string DataName = "";
        public string TextureAtlasName = "";
        public DragonBonesData Data;
        public ArmatureData Armature;
        public SkinData Skin;
    }

    /// <summary>
    /// - Base class for the factory that create the armatures. (Typically only one global factory instance is required)
    /// The factory instance create armatures by parsed and added DragonBonesData instances and TextureAtlasData instances.
    /// Once the data has been parsed, it has been cached in the factory instance and does not need to be parsed again until it is cleared by the factory instance.
    /// </summary>
    /// <see cref="DragonBones.DragonBonesData"/>
    /// <see cref="DragonBones.TextureAtlasData"/>
    /// <see cref="DragonBones.ArmatureData"/>
    /// <see cref="DragonBones.Armature"/>
    /// <version>DragonBones 3.0</version>
    /// <language>en_US</language>

    /// <summary>
    /// - 创建骨架的工厂基类。 （通常只需要一个全局工厂实例）
    /// 工厂通过解析并添加的 DragonBonesData 实例和 TextureAtlasData 实例来创建骨架。
    /// 当数据被解析过之后，已经添加到工厂中，在没有被工厂清理之前，不需要再次解析。
    /// </summary>
    /// <see cref="DragonBones.DragonBonesData"/>
    /// <see cref="DragonBones.TextureAtlasData"/>
    /// <see cref="DragonBones.ArmatureData"/>
    /// <see cref="DragonBones.Armature"/>
    /// <version>DragonBones 3.0</version>
    /// <language>zh_CN</language>
    public abstract class BaseFactory
    {
        /// <private/>
        protected static ObjectDataParser ObjectParser = null;
        /// <private/>
        protected static BinaryDataParser BinaryParser = null;
        /// <private/>
        public bool AutoSearch;
        /// <private/>
        protected readonly Dictionary<string, DragonBonesData> DragonBonesDataMap = new Dictionary<string, DragonBonesData>();
        /// <private/>
        protected readonly Dictionary<string, List<TextureAtlasData>> TextureAtlasDataMap = new Dictionary<string, List<TextureAtlasData>>();
        /// <private/>
        public DragonBones DragonBones;
        /// <private/>
        protected DataParser DataParser;
        /// <summary>
        /// - Create a factory instance. (typically only one global factory instance is required)
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 创建一个工厂实例。 （通常只需要一个全局工厂实例）
        /// </summary>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public BaseFactory(DataParser dataParser = null)
        {
            if (ObjectParser == null)
            {
                ObjectParser = new ObjectDataParser();
            }

            if (BinaryParser == null)
            {
                BinaryParser = new BinaryDataParser();
            }

            DataParser = dataParser ?? ObjectParser;
        }
        /// <private/>
        protected bool _IsSupportMesh()
        {
            return true;
        }
        /// <private/>
        protected TextureData _GetTextureData(string textureAtlasName, string textureName)
        {
            if (TextureAtlasDataMap.ContainsKey(textureAtlasName))
            {
                foreach (var textureAtlasData in TextureAtlasDataMap[textureAtlasName])
                {
                    var textureData = textureAtlasData.GetTexture(textureName);
                    if (textureData != null)
                    {
                        return textureData;
                    }
                }
            }

            if (AutoSearch)
            {
                // Will be search all data, if the autoSearch is true.
                foreach (var values in TextureAtlasDataMap.Values)
                {
                    foreach (var textureAtlasData in values)
                    {
                        if (textureAtlasData.AutoSearch)
                        {
                            var textureData = textureAtlasData.GetTexture(textureName);
                            if (textureData != null)
                            {
                                return textureData;
                            }
                        }
                    }
                }
            }

            return null;
        }
        /// <private/>
        protected bool _FillBuildArmaturePackage(BuildArmaturePackage dataPackage,
                                                string dragonBonesName,
                                                string armatureName,
                                                string skinName,
                                                string textureAtlasName)
        {
            DragonBonesData dragonBonesData = null;
            ArmatureData armatureData = null;

            var isAvailableName = !string.IsNullOrEmpty(dragonBonesName);
            if (isAvailableName)
            {
                if (DragonBonesDataMap.ContainsKey(dragonBonesName))
                {
                    dragonBonesData = DragonBonesDataMap[dragonBonesName];
                    armatureData = dragonBonesData.GetArmature(armatureName);
                }
            }

            if (armatureData == null && (!isAvailableName || AutoSearch))
            {
                // Will be search all data, if do not give a data name or the autoSearch is true.
                foreach (var key in DragonBonesDataMap.Keys)
                {
                    dragonBonesData = DragonBonesDataMap[key];
                    if (!isAvailableName || dragonBonesData.autoSearch)
                    {
                        armatureData = dragonBonesData.GetArmature(armatureName);
                        if (armatureData != null)
                        {
                            dragonBonesName = key;
                            break;
                        }
                    }
                }
            }

            if (armatureData != null)
            {
                dataPackage.DataName = dragonBonesName;
                dataPackage.TextureAtlasName = textureAtlasName;
                dataPackage.Data = dragonBonesData;
                dataPackage.Armature = armatureData;
                dataPackage.Skin = null;

                if (!string.IsNullOrEmpty(skinName))
                {
                    dataPackage.Skin = armatureData.GetSkin(skinName);
                    if (dataPackage.Skin == null && AutoSearch)
                    {
                        foreach (var k in DragonBonesDataMap.Keys)
                        {
                            var skinDragonBonesData = DragonBonesDataMap[k];
                            var skinArmatureData = skinDragonBonesData.GetArmature(skinName);
                            if (skinArmatureData != null)
                            {
                                dataPackage.Skin = skinArmatureData.defaultSkin;
                                break;
                            }
                        }
                    }
                }

                if (dataPackage.Skin == null)
                {
                    dataPackage.Skin = armatureData.defaultSkin;
                }

                return true;
            }

            return false;
        }
        /// <private/>
        protected void _BuildBones(BuildArmaturePackage dataPackage, Armature armature)
        {
            var bones = dataPackage.Armature.sortedBones;
            for (int i = 0, l = bones.Count; i < l; ++i)
            {
                var boneData = bones[i];
                var bone = BaseObject.BorrowObject<Bone>();
                bone.Init(boneData, armature);
            }
        }
        /// <private/>
        protected void _BuildSlots(BuildArmaturePackage dataPackage, Armature armature)
        {
            var currentSkin = dataPackage.Skin;
            var defaultSkin = dataPackage.Armature.defaultSkin;
            if (currentSkin == null || defaultSkin == null)
            {
                return;
            }

            var skinSlots = new Dictionary<string, List<DisplayData>>();
            foreach (var key in defaultSkin.displays.Keys)
            {
                var displays = defaultSkin.GetDisplays(key);
                skinSlots[key] = displays;
            }

            if (currentSkin != defaultSkin)
            {
                foreach (var k in currentSkin.displays.Keys)
                {
                    var displays = currentSkin.GetDisplays(k);
                    skinSlots[k] = displays;
                }
            }

            foreach (var slotData in dataPackage.Armature.sortedSlots)
            {
                var displayDatas = skinSlots.ContainsKey(slotData.name) ? skinSlots[slotData.name] : null;
                var slot = _BuildSlot(dataPackage, slotData, armature);
                slot.rawDisplayDatas = displayDatas;

                if (displayDatas != null)
                {
                    var displayList = new List<object>();
                    for (int i = 0, l = displayDatas.Count; i < l; ++i)
                    {
                        var displayData = displayDatas[i];

                        if (displayData != null)
                        {
                            displayList.Add(_GetSlotDisplay(dataPackage, displayData, null, slot));
                        }
                        else
                        {
                            displayList.Add(null);
                        }
                    }

                    slot._SetDisplayList(displayList);
                }

                slot._SetDisplayIndex(slotData.displayIndex, true);
            }
        }

        /// <private/>
        protected void _BuildConstraints(BuildArmaturePackage dataPackage, Armature armature)
        {
            var constraints = dataPackage.Armature.constraints;
            foreach (var constraintData in constraints.Values)
            {
                // TODO more constraint type.
                var constraint = BaseObject.BorrowObject<IKConstraint>();
                constraint.Init(constraintData, armature);
                armature._AddConstraint(constraint);
            }
        }

        /// <private/>
        protected virtual Armature _BuildChildArmature(BuildArmaturePackage dataPackage, Slot slot, DisplayData displayData)
        {
            return BuildArmature(displayData.path, dataPackage != null ? dataPackage.DataName : "", "", dataPackage != null ? dataPackage.TextureAtlasName : "");
        }
        /// <private/>
        protected object _GetSlotDisplay(BuildArmaturePackage dataPackage, DisplayData displayData, DisplayData rawDisplayData, Slot slot)
        {
            var dataName = dataPackage != null ? dataPackage.DataName : displayData.parent.parent.parent.name;
            object display = null;
            switch (displayData.type)
            {
                case DisplayType.Image:
                    {
                        var imageDisplayData = displayData as ImageDisplayData;
                        if (imageDisplayData.texture == null)
                        {
                            imageDisplayData.texture = _GetTextureData(dataName, displayData.path);
                        }
                        else if (dataPackage != null && !string.IsNullOrEmpty(dataPackage.TextureAtlasName))
                        {
                            imageDisplayData.texture = _GetTextureData(dataPackage.TextureAtlasName, displayData.path);
                        }

                        if (rawDisplayData != null && rawDisplayData.type == DisplayType.Mesh && _IsSupportMesh())
                        {
                            display = slot.meshDisplay;
                        }
                        else
                        {
                            display = slot.rawDisplay;
                        }
                    }
                    break;
                case DisplayType.Mesh:
                    {
                        var meshDisplayData = displayData as MeshDisplayData;
                        if (meshDisplayData.texture == null)
                        {
                            meshDisplayData.texture = _GetTextureData(dataName, meshDisplayData.path);
                        }
                        else if (dataPackage != null && !string.IsNullOrEmpty(dataPackage.TextureAtlasName))
                        {
                            meshDisplayData.texture = _GetTextureData(dataPackage.TextureAtlasName, meshDisplayData.path);
                        }

                        if (_IsSupportMesh())
                        {
                            display = slot.meshDisplay;
                        }
                        else
                        {
                            display = slot.rawDisplay;
                        }
                    }
                    break;
                case DisplayType.Armature:
                    {
                        var armatureDisplayData = displayData as ArmatureDisplayData;
                        var childArmature = _BuildChildArmature(dataPackage, slot, displayData);
                        if (childArmature != null)
                        {
                            childArmature.inheritAnimation = armatureDisplayData.inheritAnimation;
                            if (!childArmature.inheritAnimation)
                            {
                                var actions = armatureDisplayData.actions.Count > 0 ? armatureDisplayData.actions : childArmature.armatureData.defaultActions;
                                if (actions.Count > 0)
                                {
                                    foreach (var action in actions)
                                    {
                                        var eventObject = BaseObject.BorrowObject<EventObject>();
                                        EventObject.ActionDataToInstance(action, eventObject, slot.armature);
                                        eventObject.slot = slot;
                                        slot.armature._BufferAction(eventObject, false);
                                    }
                                }
                                else
                                {
                                    childArmature.animation.Play();
                                }
                            }

                            armatureDisplayData.armature = childArmature.armatureData; // 
                        }

                        display = childArmature;
                    }
                    break;
                case DisplayType.BoundingBox:
                    break;
            }

            return display;
        }
        /// <private/>
        protected abstract TextureAtlasData _BuildTextureAtlasData(TextureAtlasData textureAtlasData, object textureAtlas);
        /// <private/>
        protected abstract Armature _BuildArmature(BuildArmaturePackage dataPackage);
        /// <private/>
        protected abstract Slot _BuildSlot(BuildArmaturePackage dataPackage, SlotData slotData, Armature armature);
        /// <summary>
        /// - Parse the raw data to a DragonBonesData instance and cache it to the factory.
        /// </summary>
        /// <param name="rawData">- The raw data.</param>
        /// <param name="name">- Specify a cache name for the instance so that the instance can be obtained through this name. (If not set, use the instance name instead)</param>
        /// <param name="scale">- Specify a scaling value for all armatures. (Default: 1.0)</param>
        /// <returns>DragonBonesData instance</returns>
        /// <see cref="GetDragonBonesData()"/>
        /// <see cref="AddDragonBonesData()"/>
        /// <see cref="RemoveDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 将原始数据解析为 DragonBonesData 实例，并缓存到工厂中。
        /// </summary>
        /// <param name="rawData">- 原始数据。</param>
        /// <param name="name">- 为该实例指定一个缓存名称，以便可以通过此名称获取该实例。 （如果未设置，则使用该实例中的名称）</param>
        /// <param name="scale">- 为所有的骨架指定一个缩放值。 （默认: 1.0）</param>
        /// <returns>DragonBonesData 实例</returns>
        /// <see cref="GetDragonBonesData()"/>
        /// <see cref="AddDragonBonesData()"/>
        /// <see cref="RemoveDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public DragonBonesData ParseDragonBonesData(object rawData, string name = null, float scale = 1.0f)
        {
            var dataParser = rawData is byte[] ? BinaryParser : DataParser;
            var dragonBonesData = dataParser.ParseDragonBonesData(rawData, scale);

            while (true)
            {
                var textureAtlasData = _BuildTextureAtlasData(null, null);
                if (dataParser.ParseTextureAtlasData(null, textureAtlasData, scale))
                {
                    AddTextureAtlasData(textureAtlasData, name);
                }
                else
                {
                    textureAtlasData.ReturnToPool();
                    break;
                }
            }

            if (dragonBonesData != null)
            {
                AddDragonBonesData(dragonBonesData, name);
            }

            return dragonBonesData;
        }
        /// <summary>
        /// - Parse the raw texture atlas data and the texture atlas object to a TextureAtlasData instance and cache it to the factory.
        /// </summary>
        /// <param name="rawData">- The raw texture atlas data.</param>
        /// <param name="textureAtlas">- The texture atlas object.</param>
        /// <param name="name">- Specify a cache name for the instance so that the instance can be obtained through this name. (If not set, use the instance name instead)</param>
        /// <param name="scale">- Specify a scaling value for the map set. (Default: 1.0)</param>
        /// <returns>TextureAtlasData instance</returns>
        /// <see cref="GetTextureAtlasData()"/>
        /// <see cref="AddTextureAtlasData()"/>
        /// <see cref="RemoveTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 将原始贴图集数据和贴图集对象解析为 TextureAtlasData 实例，并缓存到工厂中。
        /// </summary>
        /// <param name="rawData">- 原始贴图集数据。</param>
        /// <param name="textureAtlas">- 贴图集对象。</param>
        /// <param name="name">- 为该实例指定一个缓存名称，以便可以通过此名称获取该实例。 （如果未设置，则使用该实例中的名称）</param>
        /// <param name="scale">- 为贴图集指定一个缩放值。 （默认: 1.0）</param>
        /// <returns>TextureAtlasData 实例</returns>
        /// <see cref="GetTextureAtlasData()"/>
        /// <see cref="AddTextureAtlasData()"/>
        /// <see cref="RemoveTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public TextureAtlasData ParseTextureAtlasData(Dictionary<string, object> rawData, object textureAtlas, string name = null, float scale = 1.0f)
        {
            var textureAtlasData = _BuildTextureAtlasData(null, null);
            DataParser.ParseTextureAtlasData(rawData, textureAtlasData, scale);
            _BuildTextureAtlasData(textureAtlasData, textureAtlas);
            AddTextureAtlasData(textureAtlasData, name);

            return textureAtlasData;
        }
        /// <private/>
        public void UpdateTextureAtlasData(string name, List<object> textureAtlases)
        {
            var textureAtlasDatas = GetTextureAtlasData(name);
            if (textureAtlasDatas != null)
            {
                for (int i = 0, l = textureAtlasDatas.Count; i < l; ++i)
                {
                    if (i < textureAtlases.Count)
                    {
                        _BuildTextureAtlasData(textureAtlasDatas[i], textureAtlases[i]);
                    }
                }
            }
        }
        /// <summary>
        /// - Get a specific DragonBonesData instance.
        /// </summary>
        /// <param name="name">- The DragonBonesData instance cache name.</param>
        /// <returns>DragonBonesData instance</returns>
        /// <see cref="ParseDragonBonesData()"/>
        /// <see cref="AddDragonBonesData()"/>
        /// <see cref="RemoveDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的 DragonBonesData 实例。
        /// </summary>
        /// <param name="name">- DragonBonesData 实例的缓存名称。</param>
        /// <returns>DragonBonesData 实例</returns>
        /// <see cref="ParseDragonBonesData()"/>
        /// <see cref="AddDragonBonesData()"/>
        /// <see cref="RemoveDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public DragonBonesData GetDragonBonesData(string name)
        {
            return DragonBonesDataMap.ContainsKey(name) ? DragonBonesDataMap[name] : null;
        }
        /// <summary>
        /// - Cache a DragonBonesData instance to the factory.
        /// </summary>
        /// <param name="data">- The DragonBonesData instance.</param>
        /// <param name="name">- Specify a cache name for the instance so that the instance can be obtained through this name. (if not set, use the instance name instead)</param>
        /// <see cref="ParseDragonBonesData()"/>
        /// <see cref="GetDragonBonesData()"/>
        /// <see cref="RemoveDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 将 DragonBonesData 实例缓存到工厂中。
        /// </summary>
        /// <param name="data">- DragonBonesData 实例。</param>
        /// <param name="name">- 为该实例指定一个缓存名称，以便可以通过此名称获取该实例。 （如果未设置，则使用该实例中的名称）</param>
        /// <see cref="ParseDragonBonesData()"/>
        /// <see cref="GetDragonBonesData()"/>
        /// <see cref="RemoveDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void AddDragonBonesData(DragonBonesData data, string name = null)
        {
            name = !string.IsNullOrEmpty(name) ? name : data.name;
            if (DragonBonesDataMap.ContainsKey(name))
            {
                if (DragonBonesDataMap[name] == data)
                {
                    return;
                }

                Helper.Assert(false, "Can not add same name data: " + name);
                return;
            }

            DragonBonesDataMap[name] = data;
        }
        /// <summary>
        /// - Remove a DragonBonesData instance.
        /// </summary>
        /// <param name="name">- The DragonBonesData instance cache name.</param>
        /// <param name="disposeData">- Whether to dispose data. (Default: true)</param>
        /// <see cref="ParseDragonBonesData()"/>
        /// <see cref="GetDragonBonesData()"/>
        /// <see cref="AddDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 移除 DragonBonesData 实例。
        /// </summary>
        /// <param name="name">- DragonBonesData 实例缓存名称。</param>
        /// <param name="disposeData">- 是否释放数据。 （默认: true）</param>
        /// <see cref="ParseDragonBonesData()"/>
        /// <see cref="GetDragonBonesData()"/>
        /// <see cref="AddDragonBonesData()"/>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public virtual void RemoveDragonBonesData(string name, bool disposeData = true)
        {
            if (DragonBonesDataMap.ContainsKey(name))
            {
                if (disposeData)
                {
                    DragonBones.BufferObject(DragonBonesDataMap[name]);
                }

                DragonBonesDataMap.Remove(name);
            }
        }
        /// <summary>
        /// - Get a list of specific TextureAtlasData instances.
        /// </summary>
        /// <param name="name">- The TextureAtlasData cahce name.</param>
        /// <see cref="ParseTextureAtlasData()"/>
        /// <see cref="AddTextureAtlasData()"/>
        /// <see cref="RemoveTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的 TextureAtlasData 实例列表。
        /// </summary>
        /// <param name="name">- TextureAtlasData 实例缓存名称。</param>
        /// <see cref="ParseTextureAtlasData()"/>
        /// <see cref="AddTextureAtlasData()"/>
        /// <see cref="RemoveTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public List<TextureAtlasData> GetTextureAtlasData(string name)
        {
            return TextureAtlasDataMap.ContainsKey(name) ? TextureAtlasDataMap[name] : null;
        }
        /// <summary>
        /// - Cache a TextureAtlasData instance to the factory.
        /// </summary>
        /// <param name="data">- The TextureAtlasData instance.</param>
        /// <param name="name">- Specify a cache name for the instance so that the instance can be obtained through this name. (if not set, use the instance name instead)</param>
        /// <see cref="ParseTextureAtlasData()"/>
        /// <see cref="GetTextureAtlasData()"/>
        /// <see cref="RemoveTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 将 TextureAtlasData 实例缓存到工厂中。
        /// </summary>
        /// <param name="data">- TextureAtlasData 实例。</param>
        /// <param name="name">- 为该实例指定一个缓存名称，以便可以通过此名称获取该实例。 （如果未设置，则使用该实例中的名称）</param>
        /// <see cref="ParseTextureAtlasData()"/>
        /// <see cref="GetTextureAtlasData()"/>
        /// <see cref="RemoveTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public void AddTextureAtlasData(TextureAtlasData data, string name = null)
        {
            name = !string.IsNullOrEmpty(name) ? name : data.Name;
            var textureAtlasList = (TextureAtlasDataMap.ContainsKey(name)) ?
                                    TextureAtlasDataMap[name] :
                                    (TextureAtlasDataMap[name] = new List<TextureAtlasData>());
            if (!textureAtlasList.Contains(data))
            {
                textureAtlasList.Add(data);
            }
        }
        /// <summary>
        /// - Remove a TextureAtlasData instance.
        /// </summary>
        /// <param name="name">- The TextureAtlasData instance cache name.</param>
        /// <param name="disposeData">- Whether to dispose data.</param>
        /// <see cref="ParseTextureAtlasData()"/>
        /// <see cref="GetTextureAtlasData()"/>
        /// <see cref="AddTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 移除 TextureAtlasData 实例。
        /// </summary>
        /// <param name="name">- TextureAtlasData 实例的缓存名称。</param>
        /// <param name="disposeData">- 是否释放数据。</param>
        /// <see cref="ParseTextureAtlasData()"/>
        /// <see cref="GetTextureAtlasData()"/>
        /// <see cref="AddTextureAtlasData()"/>
        /// <see cref="DragonBones.TextureAtlasData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public virtual void RemoveTextureAtlasData(string name, bool disposeData = true)
        {
            if (TextureAtlasDataMap.ContainsKey(name))
            {
                var textureAtlasDataList = TextureAtlasDataMap[name];
                if (disposeData)
                {
                    foreach (var textureAtlasData in textureAtlasDataList)
                    {
                        DragonBones.BufferObject(textureAtlasData);
                    }
                }

                TextureAtlasDataMap.Remove(name);
            }
        }
        /// <summary>
        /// - Get a specific armature data.
        /// </summary>
        /// <param name="name">- The armature data name.</param>
        /// <param name="dragonBonesName">- The cached name for DragonbonesData instance.</param>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 5.1</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 获取特定的骨架数据。
        /// </summary>
        /// <param name="name">- 骨架数据名称。</param>
        /// <param name="dragonBonesName">- DragonBonesData 实例的缓存名称。</param>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 5.1</version>
        /// <language>zh_CN</language>
        public virtual ArmatureData GetArmatureData(string name, string dragonBonesName = "")
        {
            var dataPackage = new BuildArmaturePackage();
            if (!_FillBuildArmaturePackage(dataPackage, dragonBonesName, name, "", ""))
            {
                return null;
            }

            return dataPackage.Armature;
        }
        /// <summary>
        /// - Clear all cached DragonBonesData instances and TextureAtlasData instances.
        /// </summary>
        /// <param name="disposeData">- Whether to dispose data.</param>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 清除缓存的所有 DragonBonesData 实例和 TextureAtlasData 实例。
        /// </summary>
        /// <param name="disposeData">- 是否释放数据。</param>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public virtual void Clear(bool disposeData = true)
        {
            if (disposeData)
            {
                foreach (var dragonBoneData in DragonBonesDataMap.Values)
                {
                    DragonBones.BufferObject(dragonBoneData);
                }

                foreach (var textureAtlasDatas in TextureAtlasDataMap.Values)
                {
                    foreach (var textureAtlasData in textureAtlasDatas)
                    {
                        DragonBones.BufferObject(textureAtlasData);
                    }
                }
            }

            DragonBonesDataMap.Clear();
            TextureAtlasDataMap.Clear();
        }
        /// <summary>
        /// - Create a armature from cached DragonBonesData instances and TextureAtlasData instances.
        /// Note that when the created armature that is no longer in use, you need to explicitly dispose {@link #dragonBones.Armature#dispose()}.
        /// </summary>
        /// <param name="armatureName">- The armature data name.</param>
        /// <param name="dragonBonesName">- The cached name of the DragonBonesData instance. (If not set, all DragonBonesData instances are retrieved, and when multiple DragonBonesData instances contain a the same name armature data, it may not be possible to accurately create a specific armature)</param>
        /// <param name="skinName">- The skin name, you can set a different ArmatureData name to share it's skin data. (If not set, use the default skin data)</param>
        /// <returns>The armature.</returns>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     let armature = factory.buildArmature("armatureName", "dragonBonesName");
        ///     armature.clock = factory.clock;
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 通过缓存的 DragonBonesData 实例和 TextureAtlasData 实例创建一个骨架。
        /// 注意，创建的骨架不再使用时，需要显式释放 {@link #dragonBones.Armature#dispose()}。
        /// </summary>
        /// <param name="armatureName">- 骨架数据名称。</param>
        /// <param name="dragonBonesName">- DragonBonesData 实例的缓存名称。 （如果未设置，将检索所有的 DragonBonesData 实例，当多个 DragonBonesData 实例中包含同名的骨架数据时，可能无法准确的创建出特定的骨架）</param>
        /// <param name="skinName">- 皮肤名称，可以设置一个其他骨架数据名称来共享其皮肤数据。（如果未设置，则使用默认的皮肤数据）</param>
        /// <returns>骨架。</returns>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     let armature = factory.buildArmature("armatureName", "dragonBonesName");
        ///     armature.clock = factory.clock;
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.DragonBonesData"/>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 3.0</version>
        /// <language>zh_CN</language>
        public virtual Armature BuildArmature(string armatureName, string dragonBonesName = "", string skinName = null, string textureAtlasName = null)
        {
            var dataPackage = new BuildArmaturePackage();
            if (!_FillBuildArmaturePackage(dataPackage, dragonBonesName, armatureName, skinName, textureAtlasName))
            {
                Helper.Assert(false, "No armature data: " + armatureName + ", " + (dragonBonesName != "" ? dragonBonesName : ""));
                return null;
            }

            var armature = _BuildArmature(dataPackage);
            _BuildBones(dataPackage, armature);
            _BuildSlots(dataPackage, armature);
            _BuildConstraints(dataPackage, armature);
            armature.InvalidUpdate(null, true);
            // Update armature pose.
            armature.AdvanceTime(0.0f);

            return armature;
        }
        /// <private/>
        public virtual void ReplaceDisplay(Slot slot, DisplayData displayData, int displayIndex = -1)
        {
            if (displayIndex < 0)
            {
                displayIndex = slot.displayIndex;
            }

            if (displayIndex < 0)
            {
                displayIndex = 0;
            }

            slot.ReplaceDisplayData(displayData, displayIndex);

            var displayList = slot.displayList; // Copy.
            if (displayList.Count <= displayIndex)
            {
                displayList.ResizeList(displayIndex + 1);

                for (int i = 0, l = displayList.Count; i < l; ++i)
                {
                    // Clean undefined.
                    displayList[i] = null;
                }
            }

            if (displayData != null)
            {
                var rawDisplayDatas = slot.rawDisplayDatas;
                DisplayData rawDisplayData = null;

                if (rawDisplayDatas != null)
                {
                    if (displayIndex < rawDisplayDatas.Count)
                    {
                        rawDisplayData = rawDisplayDatas[displayIndex];
                    }
                }

                displayList[displayIndex] = _GetSlotDisplay(null, displayData, rawDisplayData, slot);
            }
            else
            {
                displayList[displayIndex] = null;
            }

            slot.displayList = displayList;
        }
        /// <summary>
        /// - Replaces the current display data for a particular slot with a specific display data.
        /// Specify display data with "dragonBonesName/armatureName/slotName/displayName".
        /// </summary>
        /// <param name="dragonBonesName">- The DragonBonesData instance cache name.</param>
        /// <param name="armatureName">- The armature data name.</param>
        /// <param name="slotName">- The slot data name.</param>
        /// <param name="displayName">- The display data name.</param>
        /// <param name="slot">- The slot.</param>
        /// <param name="displayIndex">- The index of the display data that is replaced. (If it is not set, replaces the current display data)</param>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     let slot = armature.getSlot("weapon");
        ///     factory.replaceSlotDisplay("dragonBonesName", "armatureName", "slotName", "displayName", slot);
        /// </pre>
        /// </example>
        /// <version>DragonBones 4.5</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 用特定的显示对象数据替换特定插槽当前的显示对象数据。
        /// 用 "dragonBonesName/armatureName/slotName/displayName" 指定显示对象数据。
        /// </summary>
        /// <param name="dragonBonesName">- DragonBonesData 实例的缓存名称。</param>
        /// <param name="armatureName">- 骨架数据名称。</param>
        /// <param name="slotName">- 插槽数据名称。</param>
        /// <param name="displayName">- 显示对象数据名称。</param>
        /// <param name="slot">- 插槽。</param>
        /// <param name="displayIndex">- 被替换的显示对象数据的索引。 （如果未设置，则替换当前的显示对象数据）</param>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     let slot = armature.getSlot("weapon");
        ///     factory.replaceSlotDisplay("dragonBonesName", "armatureName", "slotName", "displayName", slot);
        /// </pre>
        /// </example>
        /// <version>DragonBones 4.5</version>
        /// <language>zh_CN</language>
        public bool ReplaceSlotDisplay(string dragonBonesName,
                                        string armatureName,
                                        string slotName,
                                        string displayName,
                                        Slot slot, int displayIndex = -1)
        {
            var armatureData = GetArmatureData(armatureName, dragonBonesName);
            if (armatureData == null || armatureData.defaultSkin == null)
            {
                return false;
            }

            var displayData = armatureData.defaultSkin.GetDisplay(slotName, displayName);
            if (displayData == null)
            {
                return false;
            }

            ReplaceDisplay(slot, displayData, displayIndex);

            return true;
        }
        /// <private/>
        public bool ReplaceSlotDisplayList(string dragonBonesName, string armatureName, string slotName, Slot slot)
        {
            var armatureData = GetArmatureData(armatureName, dragonBonesName);
            if (armatureData == null || armatureData.defaultSkin == null)
            {
                return false;
            }

            var displays = armatureData.defaultSkin.GetDisplays(slotName);
            if (displays == null)
            {
                return false;
            }

            var displayIndex = 0;
            // for (const displayData of displays) 
            for (int i = 0, l = displays.Count; i < l; ++i)
            {
                var displayData = displays[i];
                ReplaceDisplay(slot, displayData, displayIndex++);
            }

            return true;
        }
        /// <summary>
        /// - Share specific skin data with specific armature.
        /// </summary>
        /// <param name="armature">- The armature.</param>
        /// <param name="skin">- The skin data.</param>
        /// <param name="isOverride">- Whether it completely override the original skin. (Default: false)</param>
        /// <param name="exclude">- A list of slot names that do not need to be replace.</param>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     let armatureA = factory.buildArmature("armatureA", "dragonBonesA");
        ///     let armatureDataB = factory.getArmatureData("armatureB", "dragonBonesB");
        ///     if (armatureDataB && armatureDataB.defaultSkin) {
        ///     factory.replaceSkin(armatureA, armatureDataB.defaultSkin, false, ["arm_l", "weapon_l"]);
        ///     }
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.Armature"/>
        /// <see cref="DragonBones.SkinData"/>
        /// <version>DragonBones 5.6</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 将特定的皮肤数据共享给特定的骨架使用。
        /// </summary>
        /// <param name="armature">- 骨架。</param>
        /// <param name="skin">- 皮肤数据。</param>
        /// <param name="isOverride">- 是否完全覆盖原来的皮肤。 （默认: false）</param>
        /// <param name="exclude">- 不需要被替换的插槽名称列表。</param>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     let armatureA = factory.buildArmature("armatureA", "dragonBonesA");
        ///     let armatureDataB = factory.getArmatureData("armatureB", "dragonBonesB");
        ///     if (armatureDataB && armatureDataB.defaultSkin) {
        ///     factory.replaceSkin(armatureA, armatureDataB.defaultSkin, false, ["arm_l", "weapon_l"]);
        ///     }
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.Armature"/>
        /// <see cref="DragonBones.SkinData"/>
        /// <version>DragonBones 5.6</version>
        /// <language>zh_CN</language>
        public bool ReplaceSkin(Armature armature, SkinData skin, bool isOverride = false, List<string> exclude = null)
        {
            var success = false;
            var defaultSkin = skin.parent.defaultSkin;

            foreach (var slot in armature.GetSlots())
            {
                if (exclude != null && exclude.Contains(slot.name))
                {
                    continue;
                }

                var displays = skin.GetDisplays(slot.name);
                if (displays == null)
                {
                    if (defaultSkin != null && skin != defaultSkin)
                    {
                        displays = defaultSkin.GetDisplays(slot.name);
                    }

                    if (displays == null)
                    {
                        if (isOverride)
                        {
                            slot.rawDisplayDatas = null;
                            slot.displayList.Clear(); //
                        }

                        continue;
                    }
                }
                var displayCount = displays.Count;
                var displayList = slot.displayList; // Copy.
                displayList.ResizeList(displayCount); // Modify displayList length.
                for (int i = 0, l = displayCount; i < l; ++i)
                {
                    var displayData = displays[i];
                    if (displayData != null)
                    {
                        displayList[i] = _GetSlotDisplay(null, displayData, null, slot);
                    }
                    else
                    {
                        displayList[i] = null;
                    }
                }

                success = true;
                slot.rawDisplayDatas = displays;
                slot.displayList = displayList;
            }

            return success;
        }
        /// <summary>
        /// - Replaces the existing animation data for a specific armature with the animation data for the specific armature data.
        /// This enables you to make a armature template so that other armature without animations can share it's animations.
        /// </summary>
        /// <param name="armature">- The armtaure.</param>
        /// <param name="armatureData">- The armature data.</param>
        /// <param name="isOverride">- Whether to completely overwrite the original animation. (Default: false)</param>
        /// <example>
        /// TypeScript style, for reference only.
        /// <pre>
        ///     let armatureA = factory.buildArmature("armatureA", "dragonBonesA");
        ///     let armatureDataB = factory.getArmatureData("armatureB", "dragonBonesB");
        ///     if (armatureDataB) {
        ///     factory.replaceAnimation(armatureA, armatureDataB);
        ///     }
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.Armature"/>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 5.6</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 用特定骨架数据的动画数据替换特定骨架现有的动画数据。
        /// 这样就能实现制作一个骨架动画模板，让其他没有制作动画的骨架共享该动画。
        /// </summary>
        /// <param name="armature">- 骨架。</param>
        /// <param name="armatureData">- 骨架数据。</param>
        /// <param name="isOverride">- 是否完全覆盖原来的动画。（默认: false）</param>
        /// <example>
        /// TypeScript 风格，仅供参考。
        /// <pre>
        ///     let armatureA = factory.buildArmature("armatureA", "dragonBonesA");
        ///     let armatureDataB = factory.getArmatureData("armatureB", "dragonBonesB");
        ///     if (armatureDataB) {
        ///     factory.replaceAnimation(armatureA, armatureDataB);
        ///     }
        /// </pre>
        /// </example>
        /// <see cref="DragonBones.Armature"/>
        /// <see cref="DragonBones.ArmatureData"/>
        /// <version>DragonBones 5.6</version>
        /// <language>zh_CN</language>
        public bool ReplaceAnimation(Armature armature,
                                    ArmatureData armatureData,
                                    bool isOverride = true)
        {

            var skinData = armatureData.defaultSkin;
            if (skinData == null)
            {
                return false;
            }

            if (isOverride)
            {
                armature.animation.animations = armatureData.animations;
            }
            else
            {
                var rawAnimations = armature.animation.animations;
                var animations = new Dictionary<string, AnimationData>();

                foreach (var k in rawAnimations.Keys)
                {
                    animations[k] = rawAnimations[k];
                }

                foreach (var k in armatureData.animations.Keys)
                {
                    animations[k] = armatureData.animations[k];
                }

                armature.animation.animations = animations;
            }

            foreach (var slot in armature.GetSlots())
            {
                var index = 0;
                foreach (var display in slot.displayList)
                {
                    if (display is Armature)
                    {
                        var displayDatas = skinData.GetDisplays(slot.name);
                        if (displayDatas != null && index < displayDatas.Count)
                        {
                            var displayData = displayDatas[index];
                            if (displayData != null && displayData.type == DisplayType.Armature)
                            {
                                var childArmatureData = GetArmatureData(displayData.path, displayData.parent.parent.parent.name);

                                if (childArmatureData != null)
                                {
                                    ReplaceAnimation(display as Armature, childArmatureData, isOverride);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <private/>
        public Dictionary<string, DragonBonesData> GetAllDragonBonesData()
        {
            return DragonBonesDataMap;
        }
        /// <private/>
        public Dictionary<string, List<TextureAtlasData>> GetAllTextureAtlasData()
        {
            return TextureAtlasDataMap;
        }

        /// <summary>
        /// - An Worldclock instance updated by engine.
        /// </summary>
        /// <version>DragonBones 5.7</version>
        /// <language>en_US</language>

        /// <summary>
        /// - 由引擎驱动的 WorldClock 实例。
        /// </summary>
        /// <version>DragonBones 5.7</version>
        /// <language>zh_CN</language>
        public WorldClock Clock => DragonBones.Clock;

        /// <summary>
        /// - Deprecated, please refer to {@link #replaceSkin}.
        /// </summary>
        /// <language>en_US</language>

        /// <summary>
        /// - 已废弃，请参考 {@link #replaceSkin}。
        /// </summary>
        /// <language>zh_CN</language>
        [System.Obsolete("")]
        public bool ChangeSkin(Armature armature, SkinData skin, List<string> exclude = null)
        {
            return ReplaceSkin(armature, skin, false, exclude);
        }
    }
}
