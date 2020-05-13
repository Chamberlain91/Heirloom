using System;
using System.Collections.Generic;
using System.IO;

using Heirloom.Extras;
using Heirloom.IO;

namespace Heirloom
{
    /// <summary>
    /// A representation of an animated sprite.
    /// May also contains per-frame and animation sequence information for animating the sprite.
    /// </summary>
    /// <category>Drawing</category>
    public sealed class Sprite
    {
        private readonly Dictionary<string, SpriteAnimation> _animations;

        #region Constructors

        /// <summary>
        /// Constructs a new blank sprite.
        /// </summary>
        public Sprite()
        {
            _animations = new Dictionary<string, SpriteAnimation>();
        }

        /// <summary>
        /// Constructs a new sprite from the specified file path resolved by <see cref="Files.OpenStream(string)"/>.
        /// </summary>
        /// <remarks>
        /// Currently only Asesprite files are supported.
        /// </remarks>
        public Sprite(string path)
            : this(Files.OpenStream(path))
        { }

        /// <summary>
        /// Constructs a new sprite from a stream (ie, an Aseprite file or another supported format).
        /// </summary>
        /// <remarks>
        /// Currently only Asesprite files are supported.
        /// </remarks>
        /// <param name="stream">Some stream to a known sprite data format.</param>
        public Sprite(Stream stream) : this()
        {
            // todo: more asset types, somehow automagically determining what to do.
            BuildFromAsepsrite(stream);
        }

        #endregion

        #region Build From Resources

        private void BuildFromAsepsrite(Stream stream)
        {
            // Load Asesprite File
            using var ase = new AsepriteFile(stream);

            // For each named animation
            foreach (var tag in ase.Tags)
            {
                // Collect frames for specific sequence
                var animation = new SpriteAnimation(tag.Name, tag.Direction);
                for (var i = tag.From; i <= tag.To; i++)
                {
                    var frame = ase.Frames[i];
                    animation.Add(frame.Image, frame.Duration);
                }

                // Store animation
                _animations[tag.Name] = animation;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only view of the animations table.
        /// </summary>
        public IReadOnlyDictionary<string, SpriteAnimation> Animations => _animations;

        #endregion

        #region Animation

        /// <summary>
        /// Adds an animation to this sprite.
        /// </summary>
        public void AddAnimation(SpriteAnimation animation)
        {
            // 
            if (_animations.ContainsKey(animation.Name))
            {
                throw new InvalidOperationException($"Unable to add animation, an animation named '{animation.Name}' already exists.");
            }

            _animations[animation.Name] = animation;
        }

        /// <summary>
        /// Gets an animation contained by this sprite.
        /// </summary>
        public SpriteAnimation GetAnimation(string name)
        {
            if (_animations.TryGetValue(name, out var animation))
            {
                return animation;
            }

            throw new KeyNotFoundException($"Unable to find animation named \"{name}\" in sprite.");
        }

        /// <summary>
        /// Removes an animation from this sprite.
        /// </summary>
        public bool RemoveAnimation(SpriteAnimation animation)
        {
            return _animations.Remove(animation.Name);
        }

        /// <summary>
        /// Determines if this sprite contains the specified animation.
        /// </summary>
        public bool ContainsAnimation(string name)
        {
            return _animations.ContainsKey(name);
        }

        #endregion
    }
}
