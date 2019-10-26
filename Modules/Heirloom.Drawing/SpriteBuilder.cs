using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Drawing
{
    public sealed class SpriteBuilder : IEnumerable
    {
        internal readonly List<Sprite.FrameInfo> Frames;

        internal readonly Dictionary<string, Sprite.Animation> Animations;

        public SpriteBuilder()
        {
            Animations = new Dictionary<string, Sprite.Animation>();
            Frames = new List<Sprite.FrameInfo>();
        }

        public void Clear()
        {
            Animations.Clear();
            Frames.Clear();
        }

        public void Add(string name, Image frame)
        {
            Add(name, 1F, Sprite.Direction.Forward, (IEnumerable<Image>) new[] { frame });
        }

        public void Add(string name, float frameDelay, params Image[] frames)
        {
            Add(name, frameDelay, Sprite.Direction.Forward, (IEnumerable<Image>) frames);
        }

        public void Add(string name, float frameDelay, IEnumerable<Image> frames)
        {
            Add(name, frameDelay, Sprite.Direction.Forward, frames);
        }

        public void Add(string name, float frameDelay, Sprite.Direction direction, params Image[] frames)
        {
            Add(name, frameDelay, direction, (IEnumerable<Image>) frames);
        }

        public void Add(string name, float frameDelay, Sprite.Direction direction, IEnumerable<Image> frames)
        {
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
