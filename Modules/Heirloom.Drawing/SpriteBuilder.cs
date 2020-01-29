using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Utility object for manually constructing a sprite and its animations from images.
    /// </summary>
    public sealed class SpriteBuilder : IEnumerable
    {
        internal readonly List<Sprite.FrameInfo> Frames;

        internal readonly Dictionary<string, Sprite.Animation> Animations;

        /// <summary>
        /// Construct a new <see cref="SpriteBuilder"/>.
        /// </summary>
        public SpriteBuilder()
        {
            Animations = new Dictionary<string, Sprite.Animation>();
            Frames = new List<Sprite.FrameInfo>();
        }

        /// <summary>
        /// Clears all frames and animations.
        /// </summary>
        public void Clear()
        {
            Animations.Clear();
            Frames.Clear();
        }

        /// <summary>
        /// Add a single image animation.
        /// </summary>
        /// <param name="name">The animation name.</param>
        /// <param name="frame">Some image.</param>
        public void Add(string name, Image frame)
        {
            if (name is null) { throw new ArgumentNullException(nameof(name)); }
            if (frame is null) { throw new ArgumentNullException(nameof(frame)); }

            Add(name, 1F, Sprite.Direction.Forward, (IEnumerable<Image>) new[] { frame });
        }

        /// <summary>
        /// Add an animation from several images.
        /// </summary>
        /// <param name="name">The animation name.</param>
        /// <param name="frameDelay">The delay between frames in seconds.</param>
        /// <param name="frames">The image sequence to animate with.</param>
        public void Add(string name, float frameDelay, params Image[] frames)
        {
            if (frames.Length == 0) { throw new ArgumentException($"Must provide at least one image.", nameof(frames)); }
            Add(name, frameDelay, Sprite.Direction.Forward, (IEnumerable<Image>) frames);
        }

        /// <summary>
        /// Adds a new animation to the builder from multiple images.
        /// </summary>
        /// <param name="name">The animation name.</param>
        /// <param name="frameDelay">The delay between frames in seconds.</param>
        /// <param name="frames">The image sequence to animate with.</param>
        public void Add(string name, float frameDelay, IEnumerable<Image> frames)
        {
            Add(name, frameDelay, Sprite.Direction.Forward, frames);
        }

        /// <summary>
        /// Adds a new animation to the builder from multiple images.
        /// </summary>
        /// <param name="name">The animation name.</param>
        /// <param name="frameDelay">The delay between frames in seconds.</param>
        /// <param name="direction">Which way the images are cycled.</param>
        /// <param name="frames">The image sequence to animate with.</param>
        public void Add(string name, float frameDelay, Sprite.Direction direction, params Image[] frames)
        {
            if (frames.Length == 0) { throw new ArgumentException($"Must provide at least one image.", nameof(frames)); }
            Add(name, frameDelay, direction, (IEnumerable<Image>) frames);
        }

        /// <summary>
        /// Adds a new animation to the builder from multiple images.
        /// </summary>
        /// <param name="name">The animation name.</param>
        /// <param name="frameDelay">The delay between frames in seconds.</param>
        /// <param name="direction">Which way the images are cycled.</param>
        /// <param name="frames">The image sequence to animate with.</param>
        public void Add(string name, float frameDelay, Sprite.Direction direction, IEnumerable<Image> frames)
        {
            if (name is null) { throw new ArgumentNullException(nameof(name)); }
            if (frames is null) { throw new ArgumentNullException(nameof(frames)); }

            var start = Frames.Count;
            var count = 0;

            // Append frames
            foreach (var image in frames)
            {
                Frames.Add(new Sprite.FrameInfo(image, frameDelay));
                count++;
            }

            // Define animation
            Animations[name] = new Sprite.Animation(name, start, start + count, direction);
        }

        /// <summary>
        /// Create a sprite the current state of the builder.
        /// </summary>
        public Sprite CreateSprite()
        {
            return new Sprite(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
