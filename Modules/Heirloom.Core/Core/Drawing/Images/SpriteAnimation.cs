using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom
{
    /// <summary>
    /// Represents an image based per frame animation.
    /// </summary>
    public class SpriteAnimation : IReadOnlyList<SpriteFrame>
    {
        private List<SpriteFrame> _frames;
        private float _duration = 0;

        /// <summary>
        /// Constructs a new sprite animation.
        /// </summary>
        /// <param name="name">The name of the animation.</param>
        /// <param name="direction">The direction behaviour of the animation.</param>
        public SpriteAnimation(string name, AnimationDirection direction = AnimationDirection.Forward)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Direction = direction;

            _frames = new List<SpriteFrame>();
        }

        /// <summary>
        /// Gets the nth frame of this animation.
        /// </summary>
        /// <param name="index">The nth frame of the animation.</param>
        /// <returns>The corresponding frame data.</returns>
        /// <exception cref="IndexOutOfRangeException">Index must be greater than or equal to zero.</exception>
        /// <exception cref="IndexOutOfRangeException">Index must be less than the number of frames in the animation.</exception>
        public SpriteFrame this[int index] => _frames[index];

        /// <summary>
        /// The name of the animation sequence.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public int Length => _frames.Count;

        /// <summary>
        /// Gets the duration of the animation in seconds.
        /// </summary>
        public float Duration
        {
            get
            {
                // If duration is negative, we need to recompute the duration
                if (_duration < 0) { _duration = _frames.Sum(f => f.Delay); }
                return _duration;
            }
        }

        /// <summary>
        /// Gets or sets the intended animation direction.
        /// </summary>
        public AnimationDirection Direction { get; set; }

        /// <summary>
        /// Adds a new frame to the end of the animation.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="delay">The frame delay in seconds.</param>
        public void Add(Image image, float delay)
        {
            // Insert at the end
            Insert(Length, image, delay);
        }

        /// <summary>
        /// Inserts a new frame to the animation before the specified frame number.
        /// </summary>
        /// <param name="index">The frame number to insert before.</param>
        /// <param name="image">Some image.</param>
        /// <param name="delay">The frame delay in seconds.</param>
        public void Insert(int index, Image image, float delay)
        {
            var frame = new SpriteFrame(image, delay);
            _frames.Insert(index, frame);
            _duration = -1;
        }

        /// <summary>
        /// Removes a frame at the specified frame number.
        /// </summary>
        /// <param name="index">The frame number.</param>
        public void RemoveAt(int index)
        {
            _frames.RemoveAt(index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the frames of the animation.
        /// </summary>
        public IEnumerator<SpriteFrame> GetEnumerator()
        {
            return _frames.GetEnumerator();
        }

        int IReadOnlyCollection<SpriteFrame>.Count => Length;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
