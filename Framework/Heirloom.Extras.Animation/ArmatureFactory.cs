using System;
using System.Collections.Generic;
using System.IO;

using Heirloom.IO;

namespace Heirloom.Extras.Animation
{
    public static class ArmatureFactory
    {
        private static readonly HashSet<ArmaturePackage> _packages = new HashSet<ArmaturePackage>();
        private static readonly Dictionary<string, ArmaturePackage> _lookup = new Dictionary<string, ArmaturePackage>();

        #region Load DragonBones (5.6.3)

        /// <summary>
        /// Loads armature data from DragonBones (5.6.3) format. This assumes there is a sibling <c>*_tex.json</c> texture atlas file.
        /// </summary>
        /// <param name="bonesPath">The path to a <c>*_ske.json</c> or <c>*_ske.dbbin</c>.</param>
        /// <param name="atlasPath">The path to a <c>*_tex.json</c>.</param>
        /// <returns>The armature package reference object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bonesPath"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="atlasPath"/> is null.</exception>
        public static ArmaturePackage LoadDragonBones(string path)
        {
            if (path is null) { throw new ArgumentNullException(nameof(path)); }

            if (path.EndsWith("_ske.json"))
            {
                var root = path[0..^9];
                var atlasPath = $"{root}_tex.json";

                if (!Files.Exists(atlasPath)) { throw new FileNotFoundException($"Unable to find required dragonbones file: '{atlasPath}'"); }

                return LoadDragonBones(path, atlasPath);
            }
            else
            {
                throw new ArgumentException($"Unable to load DragonBones package.");
            }
        }

        /// <summary>
        /// Loads armature data from DragonBones (5.6.3) format.
        /// </summary>
        /// <param name="bonesPath">The path to a <c>*_ske.json</c> or <c>*_ske.dbbin</c>.</param>
        /// <param name="atlasPath">The path to a <c>*_tex.json</c>.</param>
        /// <returns>The armature package reference object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bonesPath"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="atlasPath"/> is null.</exception>
        public static ArmaturePackage LoadDragonBones(string bonesPath, string atlasPath)
        {
            if (bonesPath is null) { throw new ArgumentNullException(nameof(bonesPath)); }
            if (atlasPath is null) { throw new ArgumentNullException(nameof(atlasPath)); }

            var package = DragonBonesArmaturePackage.Load(bonesPath, atlasPath);
            StoreArmatures(package);
            return package;
        }

        private static void StoreArmatures(ArmaturePackage package)
        {
            foreach (var armatureName in package.ArmatureNames)
            {
                if (FindPackage(armatureName) != null)
                {
                    // Warn, name conflict
                    Log.Warning($"Armature with '{armatureName}' already exists and was replaced by a load.");
                }

                // Store package by armature name
                _lookup[armatureName] = package;
            }

            _packages.Add(package);
        }

        #endregion

        /// <summary>
        /// Unloads the specific armature package.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="package"/> is null.</exception>
        public static void Unload(ArmaturePackage package)
        {
            if (package is null) { throw new ArgumentNullException(nameof(package)); }

            // Remove entries for package
            _packages.Remove(package);
            foreach (var name in package.ArmatureNames)
            {
                _lookup.Remove(name);
            }

            // Unload package
            package.Unload();
        }

        /// <summary>
        /// Unloads all armature packages.
        /// </summary>
        public static void UnloadAll()
        {
            // Unload all packages
            foreach (var package in _packages)
            {
                package.Unload();
            }

            // Clear entries
            _packages.Clear();
            _lookup.Clear();
        }

        /// <summary>
        /// Construct an instance of the specified armature.
        /// </summary>
        /// <param name="name">The name of some armature.</param>
        /// <returns>A newly created instance of the specified armature.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null.</exception>
        public static Armature CreateArmature(string name)
        {
            var package = FindPackage(name);
            if (package != null)
            {
                return package.CreateArmature(name);
            }
            else
            {
                throw new KeyNotFoundException($"Unable to find armature named '{name}'.");
            }
        }

        /// <summary>
        /// Finds the armature package containing the specified armature.
        /// </summary>
        /// <param name="name">The name of some armature.</param>
        /// <returns>The respective package or <see langword="null"/> if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null.</exception>
        public static ArmaturePackage FindPackage(string name)
        {
            if (name is null) { throw new ArgumentNullException(nameof(name)); }

            // Try to get package by armature name
            if (_lookup.TryGetValue(name, out var package)) { return package; }
            else { return null; }
        }
    }
}
