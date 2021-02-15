using System;
using System.Collections.Generic;
using System.IO;

using DragonBones;

using Heirloom.Drawing;
using Heirloom.IO;

namespace Heirloom.Extras.Animation
{
    internal sealed class DragonBonesArmaturePackage : ArmaturePackage
    {
        private static uint _counter;

        internal readonly string Identifier;

        private DragonBonesData _package;

        public override ICollection<string> ArmatureNames => (ICollection<string>) (_package?.armatureNames) ?? Array.Empty<string>();

        public DragonBonesArmaturePackage(string identifer, DragonBonesData package)
        {
            Identifier = identifer ?? throw new ArgumentNullException(nameof(identifer));
            _package = package;
        }

        protected internal override Armature CreateArmature(string name)
        {
            return new DragonBonesArmature(this, name);
        }

        internal override void Unload()
        {
            if (_package != null)
            {
                // Remove from dragonbones cache
                DragonFactory.Factory.RemoveTextureAtlasData(Identifier);
                DragonFactory.Factory.RemoveDragonBonesData(Identifier);

                // 
                _package = null;
            }
        }

        internal static ArmaturePackage Load(string skeletonPath, string atlasPath)
        {
            // Generate a unique name for this armature
            var identifier = $"dragonbones_{_counter++}";

            // Load and parse dragon bones data
            var dragonFile = LoadDragonBoneFile(skeletonPath);
            var dragonData = DragonFactory.Factory.ParseDragonBonesData(dragonFile, identifier);

            // Load and parse atlas data
            var atlasJson = Files.ReadText(atlasPath);
            var atlasJsonData = (Dictionary<string, object>) Json.Deserialize(atlasJson);
            var atlasImagePath = Path.Combine(Path.GetDirectoryName(atlasPath), atlasJsonData["imagePath"] as string);
            if (!Files.Exists(atlasImagePath)) { throw new FileNotFoundException($"Unable to find dragonbones image file: '{atlasImagePath}'"); }
            DragonFactory.Factory.ParseTextureAtlasData(atlasJsonData, new Image(atlasImagePath), identifier);

            // ...
            return new DragonBonesArmaturePackage(identifier, dragonData);

            static object LoadDragonBoneFile(string path)
            {
                // Ensure target file exists
                if (!Files.Exists(path)) { throw new FileNotFoundException($"Unable to find DragonBones package '{path}'"); }

                if (path.EndsWith(".json"))
                {
                    // load and parse json
                    var bonesJson = Files.ReadText(path);
                    return Json.Deserialize(bonesJson);
                }
                else if (path.EndsWith(".dbbin"))
                {
                    // load bytes
                    return Files.ReadBytes(path);
                }
                else
                {
                    throw new ArgumentException("Unknown file extension. Dragonbones armature data is either .json or .dbbin");
                }
            }
        }
    }
}
